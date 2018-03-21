using System;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace DataDevelop.Data.PostgreSql
{
	internal sealed class PgSqlDatabase : Database, IDisposable
	{
		private string name;
		private NpgsqlConnection connection;

		public PgSqlDatabase(string name, string connectionString)
		{
			this.name = name;
			this.connection = new NpgsqlConnection(connectionString);
		}

		public override string Name
		{
			get { return this.name; }
		}

		public override bool SupportStoredProcedures
		{
			get { return true; }
		}

		public override string ParameterPrefix
		{
			get { return ":"; }
		}

		public override string QuotePrefix
		{
			get { return "\""; }
		}

		public override string QuoteSuffix
		{
			get { return "\""; }
		}

		public override DbProvider Provider
		{
			get { return new PgSqlProvider(); }
		}

		public override string ConnectionString
		{
			get { return this.connection.ConnectionString; }
		}

		internal NpgsqlConnection Connection
		{
			get { return this.connection; }
		}

		public override int ExecuteNonQuery(string commandText)
		{
			using (var command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override int ExecuteNonQuery(string commandText, DbTransaction transaction)
		{
			using (this.CreateConnectionScope()) {
				using (var command = this.connection.CreateCommand()) {
					command.Transaction = (NpgsqlTransaction)transaction;
					command.CommandText = commandText;
					return command.ExecuteNonQuery();
				}
			}
		}

		public override DataTable ExecuteTable(string commandText)
		{
			var data = new DataTable();
			using (var adapter = new NpgsqlDataAdapter(commandText, this.connection)) {
				adapter.Fill(data);
			}
			return data;
		}

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			var adapter = new NpgsqlDataAdapter(table.GetBaseSelectCommandText(filter), this.connection);
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
			return this.connection.CreateCommand();
		}

		public override DbTransaction BeginTransaction()
		{
			return this.connection.BeginTransaction();
		}

		public void Dispose()
		{
			if (this.connection != null) {
				this.connection.Dispose();
				GC.SuppressFinalize(this);
			}
		}

		public override void ChangeConnectionString(string newConnectionString)
		{
			if (this.IsConnected) {
				throw new InvalidOperationException("Database must be disconnected in order to change the ConnectionString");
			} else {
				this.connection.ConnectionString = newConnectionString;
			}
		}

		protected override void DoConnect()
		{
			this.connection.Open();
		}

		protected override void DoDisconnect()
		{
			this.connection.Close();
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
			// TODO
		}
	}
}
