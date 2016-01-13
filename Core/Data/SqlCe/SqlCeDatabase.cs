using System;
using System.Data;
using System.Data.Common;
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

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			var sqlTable = table as SqlCeTable;
			if (sqlTable == null) {
				throw new InvalidOperationException("Table should be an SqlCeTable");
			}
			var adapter = new SqlCeDataAdapter(table.GetBaseSelectCommandText(filter), this.connection);
			if (!table.IsReadOnly) {
				var builder = new SqlCeCommandBuilder(adapter);
				builder.ConflictOption = ConflictOption.OverwriteChanges;
				try {
					adapter.UpdateCommand = builder.GetUpdateCommand();
					adapter.InsertCommand = builder.GetInsertCommand();
					adapter.DeleteCommand = builder.GetDeleteCommand();
				} catch (InvalidOperationException) {
					sqlTable.SetReadOnly(true);
					return adapter;
				}
			}
			return adapter;
		}

		public override DbCommand CreateCommand()
		{
			return this.Connection.CreateCommand();
		}

		public override DbTransaction BeginTransaction()
		{
			return this.Connection.BeginTransaction();
		}

		public override int ExecuteNonQuery(string commandText)
		{
			using (var command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override DataTable ExecuteTable(string commandText)
		{
			using (var command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				var table = new DataTable();
				using (var adapter = new SqlCeDataAdapter(command)) {
					adapter.Fill(table);
				}
				return table;
			}
		}

		public override int ExecuteNonQuery(string commandText, DbTransaction transaction)
		{
			if ((object)transaction.Connection != (object)this.connection) {
				throw new InvalidOperationException();
			}
			using (var command = this.connection.CreateCommand()) {
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
			using (var tables = this.Connection.GetSchema("Tables")) {
				foreach (DataRow row in tables.Rows) {
					var table = new SqlCeTable(this);
					table.Schema = row["TABLE_SCHEMA"] as string;
					table.Name = row["TABLE_NAME"] as string;
					tablesCollection.Add(table);
				}
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			throw new NotSupportedException("Stored Procedures are not supported.");
		}
	}
}
