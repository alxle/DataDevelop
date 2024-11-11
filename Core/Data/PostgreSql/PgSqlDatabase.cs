using System;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace DataDevelop.Data.PostgreSql
{
	internal sealed class PgSqlDatabase : Database, IDisposable
	{
		private readonly string name;

		public PgSqlDatabase(string name, string connectionString)
		{
			this.name = name;
			Connection = new NpgsqlConnection(connectionString);
		}

		public override string Name => name;

		public override bool SupportStoredProcedures => true;

		public override bool SupportUserDefinedFunctions => true;

		public override string ParameterPrefix => ":";

		public override string QuotePrefix => "\"";

		public override string QuoteSuffix => "\"";

		public override DbProvider Provider => PgSqlProvider.Instance;

		public override string ConnectionString => Connection.ConnectionString;

		internal NpgsqlConnection Connection { get; }

		public override int ExecuteNonQuery(string commandText)
		{
			using (var command = Connection.CreateCommand()) {
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override int ExecuteNonQuery(string commandText, DbTransaction transaction)
		{
			using (var command = Connection.CreateCommand()) {
				command.Transaction = (NpgsqlTransaction)transaction;
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override DataTable ExecuteTable(string commandText)
		{
			var data = new DataTable();
			using (var adapter = new NpgsqlDataAdapter(commandText, Connection)) {
				adapter.Fill(data);
			}
			return data;
		}

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			var adapter = new NpgsqlDataAdapter(table.GetBaseSelectCommandText(filter), Connection);
			var builder = new NpgsqlCommandBuilder(adapter);
			try {
				adapter.InsertCommand = builder.GetInsertCommand();
				adapter.UpdateCommand = builder.GetUpdateCommand();
				adapter.DeleteCommand = builder.GetDeleteCommand();
			} catch {
			}
			return adapter;
		}

		public override DbCommand CreateCommand()
		{
			return Connection.CreateCommand();
		}

		public override DbTransaction BeginTransaction()
		{
			return Connection.BeginTransaction();
		}

		public void Dispose()
		{
			if (Connection != null) {
				Connection.Dispose();
				GC.SuppressFinalize(this);
			}
		}

		public override void ChangeConnectionString(string newConnectionString)
		{
			if (IsConnected) {
				throw new InvalidOperationException("Database must be disconnected in order to change the ConnectionString");
			}
			Connection.ConnectionString = newConnectionString;
		}

		protected override void DoConnect()
		{
			Connection.Open();
		}

		protected override void DoDisconnect()
		{
			Connection.Close();
		}

		protected override void PopulateTables(DbObjectCollection<Table> tablesCollection)
		{
			using (var tables = Connection.GetSchema("Tables", new[] { Connection.Database, null })) {
				foreach (DataRow row in tables.Rows) {
					var table = new PgSqlTable(this, (string)row["table_schema"], (string)row["table_name"]);
					tablesCollection.Add(table);
				}
			}

			using (var views = Connection.GetSchema("Views", new[] { Connection.Database, null })) {
				foreach (DataRow row in views.Rows) {
					var table = new PgSqlTable(this, (string)row["table_schema"], (string)row["table_name"],
						isView: true, isReadOnly: (string)row["is_updatable"] == "NO");
					tablesCollection.Add(table);
				}
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			using (var command = Connection.CreateCommand()) {
				command.CommandText =
					"SELECT specific_name, routine_schema, routine_name " +
					"FROM information_schema.routines " +
					"WHERE specific_catalog = :db AND routine_type = 'PROCEDURE' " +
					"ORDER BY routine_name";
				command.Parameters.AddWithValue("db", Connection.Database);
				using (var reader = command.ExecuteReader()) {
					while (reader.Read()) {
						var specificName = reader.GetString(0);
						var schema = reader.GetString(1);
						var name = reader.GetString(2);
						var sp = new PgSqlStoredProcedure(this, schema, specificName, name);
						storedProceduresCollection.Add(sp);
					}
				}
			}
		}

		protected override void PopulateUserDefinedFunctions(DbObjectCollection<UserDefinedFunction> userDefinedFunctionsCollection)
		{
			using (var command = Connection.CreateCommand()) {
				command.CommandText =
					"SELECT specific_name, routine_schema, routine_name, data_type " +
					"FROM information_schema.routines " +
					"WHERE specific_catalog = :db AND routine_type = 'FUNCTION' " +
					"AND routine_schema NOT IN ('pg_catalog', 'information_schema')" +
					"ORDER BY routine_name";
				command.Parameters.AddWithValue("db", Connection.Database);
				using (var reader = command.ExecuteReader()) {
					while (reader.Read()) {
						var specificName = reader.GetString(0);
						var routineSchema = reader.GetString(1);
						var routineName = reader.GetString(2);
						var returnType = reader.GetString(3);
						var fn = new PgSqlUserDefinedFunction(this, routineSchema, specificName, routineName) {
							ReturnType = returnType
						};
						userDefinedFunctionsCollection.Add(fn);
					}
				}
			}
		}
	}
}
