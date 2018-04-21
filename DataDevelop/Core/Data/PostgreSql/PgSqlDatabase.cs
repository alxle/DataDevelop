using System;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace DataDevelop.Data.PostgreSql
{
	internal sealed class PgSqlDatabase : Database, IDisposable
	{
		private string name;

		public PgSqlDatabase(string name, string connectionString)
		{
			this.name = name;
			Connection = new NpgsqlConnection(connectionString);
		}

		public override string Name => name;

		public override bool SupportStoredProcedures => false;

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
			using (CreateConnectionScope()) {
				using (var command = Connection.CreateCommand()) {
					command.Transaction = (NpgsqlTransaction)transaction;
					command.CommandText = commandText;
					return command.ExecuteNonQuery();
				}
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
			} else {
				Connection.ConnectionString = newConnectionString;
			}
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
			using (var tables = Connection.GetSchema("Tables", new[] { Connection.Database, "public" })) {
				foreach (DataRow row in tables.Rows) {
					var table = new PgSqlTable(this, (string)row["table_name"]);
					tablesCollection.Add(table);
				}
			}

			using (var views = Connection.GetSchema("Views", new[] { Connection.Database, "public" })) {
				foreach (DataRow row in views.Rows) {
					var table = new PgSqlTable(this, (string)row["table_name"],
						isView: true, isReadOnly: (string)row["is_updatable"] == "NO");
					tablesCollection.Add(table);
				}
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
		}

		protected override void PopulateUserDefinedFunctions(DbObjectCollection<UserDefinedFunction> userDefinedFunctionsCollection)
		{
			using (var command = Connection.CreateCommand()) {
				command.CommandText =
					"SELECT n.nspname as \"Schema\", " +
					" p.proname as \"Name\", " +
					" pg_catalog.pg_get_function_result(p.oid) as \"Result data type\", " +
					" pg_catalog.pg_get_function_arguments(p.oid) as \"Argument data types\", " +
					"CASE " +
					" WHEN p.proisagg THEN 'agg' " +
					" WHEN p.proiswindow THEN 'window' " +
					" WHEN p.prorettype = 'pg_catalog.trigger'::pg_catalog.regtype THEN 'trigger' " +
					" ELSE 'normal' " +
					"END as \"Type\" " +
					"FROM pg_catalog.pg_proc p " +
					" LEFT JOIN pg_catalog.pg_namespace n ON n.oid = p.pronamespace " +
					"WHERE pg_catalog.pg_function_is_visible(p.oid) " +
					"  AND n.nspname <> 'pg_catalog' " +
					"  AND n.nspname <> 'information_schema' " +
					"  AND n.nspname = :schema " +
					"ORDER BY 1, 2, 4; ";
				command.Parameters.AddWithValue(":schema", "public");
				using (var reader = command.ExecuteReader()) {
					while (reader.Read()) {
						var fn = new PgSqlUserDefinedFunction(this, reader.GetString(1));
						fn.ReturnType = reader.GetString(2);
						userDefinedFunctionsCollection.Add(fn);
					}
				}
			}
		}
	}
}
