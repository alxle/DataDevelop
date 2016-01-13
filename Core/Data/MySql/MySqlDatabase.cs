using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DataDevelop.Data.MySql
{
	internal sealed class MySqlDatabase : Database, IDisposable
	{
		private string name;
		private MySqlConnection connection;

		public MySqlDatabase(string name, string connectionString)
		{
			this.name = name;
			this.connection = new MySqlConnection(connectionString);
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
			get { return "?"; }
		}

		public override string QuotePrefix
		{
			get { return "`"; }
		}

		public override string QuoteSuffix
		{
			get { return "`"; }
		}

		public override DbProvider Provider
		{
			get { return new MySqlProvider(); }
		}

		public override string ConnectionString
		{
			get { return this.connection.ConnectionString; }
		}

		internal MySqlConnection Connection
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
			int rows = 0;
			using (this.CreateConnectionScope()) {
				using (var command = this.connection.CreateCommand()) {
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
			using (var adapter = new MySqlDataAdapter(commandText, this.connection)) {
				adapter.Fill(data);
			}
			return data;
		}

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			var adapter = new MySqlDataAdapter(table.GetBaseSelectCommandText(filter), this.connection);
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
			var tables = this.Connection.GetSchema("Tables", new string[] { null, this.connection.Database });
			foreach (DataRow row in tables.Rows) {
				var table = new MySqlTable(this);
				table.Name = row["TABLE_NAME"].ToString();
				tablesCollection.Add(table);
			}

			var views = this.Connection.GetSchema("Views", new string[] { null, this.connection.Database });
			foreach (DataRow row in views.Rows) {
				var table = new MySqlTable(this);
				table.Name = row["TABLE_NAME"].ToString();
				table.SetView(true);
				tablesCollection.Add(table);
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			var procedures = this.Connection.GetSchema("Procedures", new string[] { null, this.connection.Database });
			foreach (DataRow row in procedures.Rows) {
				var sp = new MySqlStoredProcedure(this, row);
				storedProceduresCollection.Add(sp);
			}
		}
	}
}
