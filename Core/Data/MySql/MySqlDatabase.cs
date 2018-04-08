using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DataDevelop.Data.MySql
{
	internal sealed class MySqlDatabase : Database, IDisposable
	{
		private string name;

		public MySqlDatabase(string name, string connectionString)
		{
			this.name = name;
			Connection = new MySqlConnection(connectionString);
		}

		public override string Name => name;

		public override bool SupportStoredProcedures => true;

		public override string ParameterPrefix => "?";

		public override string QuotePrefix => "`";

		public override string QuoteSuffix => "`";

		public override DbProvider Provider=> MySqlProvider.Instance;

		public override string ConnectionString => Connection.ConnectionString;

		internal MySqlConnection Connection { get; }

		public override int ExecuteNonQuery(string commandText)
		{
			using (var command = Connection.CreateCommand()) {
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override int ExecuteNonQuery(string commandText, DbTransaction transaction)
		{
			var rows = 0;
			using (CreateConnectionScope()) {
				using (var command = Connection.CreateCommand()) {
					command.Transaction = (MySqlTransaction)transaction;
					command.CommandText = commandText;
					rows = command.ExecuteNonQuery();
				}
			}
			return rows;
		}

		public override DataTable ExecuteTable(string commandText)
		{
			var data = new DataTable();
			using (var adapter = new MySqlDataAdapter(commandText, Connection)) {
				adapter.Fill(data);
			}
			return data;
		}

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			var adapter = new MySqlDataAdapter(table.GetBaseSelectCommandText(filter), Connection);
			var builder = new MySqlCommandBuilder(adapter);
			////builder.ReturnGeneratedIdentifiers = true;
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
			var tables = Connection.GetSchema("Tables", new[] { null, Connection.Database });
			foreach (DataRow row in tables.Rows) {
				if ((string)row["TABLE_TYPE"] == "BASE TABLE") {
					var table = new MySqlTable(this) {
						Name = row["TABLE_NAME"].ToString()
					};
					tablesCollection.Add(table);
				}
			}

			var views = Connection.GetSchema("Views", new[] { null, Connection.Database });
			foreach (DataRow row in views.Rows) {
				var table = new MySqlTable(this) {
					Name = row["TABLE_NAME"].ToString()
				};
				table.SetView(true);
				tablesCollection.Add(table);
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			var procedures = Connection.GetSchema("Procedures", new[] { null, Connection.Database });
			foreach (DataRow row in procedures.Rows) {
				var sp = new MySqlStoredProcedure(this, row);
				storedProceduresCollection.Add(sp);
			}
		}
	}
}
