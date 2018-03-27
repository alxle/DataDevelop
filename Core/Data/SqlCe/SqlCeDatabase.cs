using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;

namespace DataDevelop.Data.SqlCe
{
	internal sealed class SqlCeDatabase : Database, IDisposable
	{
		private string name;

		public SqlCeDatabase(string name, string connectionString)
		{
			this.name = name;
			Connection = new SqlCeConnection(connectionString);
		}

		public SqlCeConnection Connection { get; }

		public override string Name => name;

		public override DbProvider Provider => SqlCeProvider.Instance;

		public override string ConnectionString => Connection.ConnectionString;

		public override bool SupportStoredProcedures => false;

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			var sqlTable = table as SqlCeTable;
			if (sqlTable == null) {
				throw new InvalidOperationException("Table should be an SqlCeTable");
			}
			var adapter = new SqlCeDataAdapter(table.GetBaseSelectCommandText(filter), Connection);
			if (!table.IsReadOnly) {
				var builder = new SqlCeCommandBuilder(adapter) {
					ConflictOption = ConflictOption.OverwriteChanges
				};
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
			return Connection.CreateCommand();
		}

		public override DbTransaction BeginTransaction()
		{
			return Connection.BeginTransaction();
		}

		public override int ExecuteNonQuery(string commandText)
		{
			using (var command = Connection.CreateCommand()) {
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override DataTable ExecuteTable(string commandText)
		{
			using (var command = Connection.CreateCommand()) {
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
			if ((object)transaction.Connection != (object)Connection) {
				throw new InvalidOperationException();
			}
			using (var command = Connection.CreateCommand()) {
				command.Transaction = (SqlCeTransaction)transaction;
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (Connection != null) {
				Connection.Dispose();
				GC.SuppressFinalize(this);
			}
		}

		#endregion

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
			using (var tables = Connection.GetSchema("Tables")) {
				foreach (DataRow row in tables.Rows) {
					var table = new SqlCeTable(this) {
						Schema = row["TABLE_SCHEMA"] as string,
						Name = row["TABLE_NAME"] as string
					};
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
