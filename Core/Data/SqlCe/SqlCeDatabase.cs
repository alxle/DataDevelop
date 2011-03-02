using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;

namespace DataDevelop.Data.SqlCe
{
	internal sealed class SqlCeDatabase : Database, IDisposable
	{
		private string name;
		private SqlCeConnection connection;

		public SqlCeDatabase(string name, string connectionString)
		{
			this.name = name;
			this.connection = new SqlCeConnection(connectionString);
		}

		public SqlCeConnection Connection
		{
			get { return this.connection; }
		}
		
		public override string Name
		{
			get { return this.name; }
		}

		public override DbProvider Provider
		{
			get { return SqlCeProvider.Instance; }
		}

		public override string ConnectionString
		{
			get { return this.connection.ConnectionString; }
		}

		public override bool SupportStoredProcedures
		{
			get { return false; }
		}

		public override System.Data.Common.DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			SqlCeTable sqlTable = table as SqlCeTable;
			if (sqlTable == null) {
				throw new InvalidOperationException("Table should be an SqlCeTable");
			}
			SqlCeDataAdapter adapter = new SqlCeDataAdapter(table.GetBaseSelectCommandText(filter), this.connection);
			if (!table.IsReadOnly) {
				SqlCeCommandBuilder builder = new SqlCeCommandBuilder(adapter);
				builder.ConflictOption = ConflictOption.OverwriteChanges;
				try {
					builder.GetUpdateCommand();
				} catch (InvalidOperationException) {
					sqlTable.SetReadOnly(true);
					return adapter;
				}
				adapter.DeleteCommand = builder.GetDeleteCommand();
				adapter.UpdateCommand = builder.GetUpdateCommand();
				adapter.InsertCommand = builder.GetInsertCommand();
			}
			return adapter;
		}

		public override System.Data.Common.DbCommand CreateCommand()
		{
			return this.Connection.CreateCommand();
		}

		public override System.Data.Common.DbTransaction BeginTransaction()
		{
			return this.Connection.BeginTransaction();
		}

		public override int ExecuteNonQuery(string commandText)
		{
			using (SqlCeCommand command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override DataTable ExecuteTable(string commandText)
		{
			using (SqlCeCommand command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				DataTable table = new DataTable();
				using (SqlCeDataAdapter adapter = new SqlCeDataAdapter(command)) {
					adapter.Fill(table);
				}
				return table;
			}
		}

		public override int ExecuteNonQuery(string commandText, System.Data.Common.DbTransaction transaction)
		{
			if ((object)transaction.Connection != (object)this.connection) {
				throw new InvalidOperationException();
			}
			using (SqlCeCommand command = this.connection.CreateCommand()) {
				command.Transaction = (SqlCeTransaction)transaction;
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (this.connection != null) {
				this.connection.Dispose();
				GC.SuppressFinalize(this);
			}
		}

		#endregion

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
			using (DataTable tables = this.Connection.GetSchema("Tables")) {
				foreach (DataRow row in tables.Rows) {
					SqlCeTable table = new SqlCeTable(this);
					table.Schema = row["TABLE_SCHEMA"] as string;
					table.Name = row["TABLE_NAME"] as string;
					tablesCollection.Add(table);
				}
			}
			// TODO: Retrive Views
			////using (DataTable views = this.Connection.GetSchema("Tables")) {
			////    foreach (DataRow row in views.Rows) {
			////        SqlCeTable table = new SqlCeTable(this);
			////        table.Schema = (string)row["TABLE_SCHEMA"];
			////        table.Name = (string)row["TABLE_NAME"];
			////        table.SetView(true);
			////        tablesCollection.Add(table);
			////    }
			////}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			throw new NotSupportedException("Stored Procedures are not supported.");
		}
	}
}
