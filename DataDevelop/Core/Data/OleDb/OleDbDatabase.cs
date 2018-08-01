using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace DataDevelop.Data.OleDb
{
	internal class OleDbDatabase : Database, IDisposable
	{
		private string name;
		protected bool disposed;

		public OleDbDatabase(string name, string connectionString)
		{
			this.name = name;
			Connection = new OleDbConnection(connectionString);
		}

		public OleDbConnection Connection { get; }

		public override string Name => name;

		public override DbProvider Provider => OleDbProvider.Instance;

		public override string ConnectionString => Connection.ConnectionString;

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			var oleDbTable = (OleDbTable)table;
			var adapter = new OleDbDataAdapter(table.GetBaseSelectCommandText(filter), Connection);
			if (!table.IsReadOnly) {
				var builder = new OleDbCommandBuilder(adapter) {
					ConflictOption = ConflictOption.OverwriteChanges
				};
				try {
					adapter.UpdateCommand = builder.GetUpdateCommand();
					adapter.InsertCommand = builder.GetInsertCommand();
					adapter.DeleteCommand = builder.GetDeleteCommand();
				} catch (InvalidOperationException) {
					oleDbTable.SetReadOnly(true);
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
				using (var reader = command.ExecuteReader()) {
					table.Load(reader);
				}
				return table;
			}
		}

		public override int ExecuteNonQuery(string commandText, DbTransaction transaction)
		{
			if (!ReferenceEquals(transaction.Connection, Connection)) {
				throw new InvalidOperationException();
			}
			using (var command = Connection.CreateCommand()) {
				command.Transaction = (OleDbTransaction)transaction;
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed) {
				return;
			}
			if (disposing) {
				if (Connection != null) {
					Connection.Dispose();
				}
			}
			disposed = true;
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

		protected virtual OleDbTable CreateTableInstance(string schema, string name)
		{
			return new OleDbTable(this) {
				Schema = schema,
				Name = name
			};
		}

		protected override void PopulateTables(DbObjectCollection<Table> tablesCollection)
		{
			var tables = Connection.GetSchema("Tables", new[] { null, null, null, "Table" });
			foreach (DataRow row in tables.Rows) {
				var schema = row["TABLE_SCHEMA"] as string;
				var name = row["TABLE_NAME"] as string;
				var table = CreateTableInstance(schema, name);
				tablesCollection.Add(table);
			}

			var views = Connection.GetSchema("Tables", new[] { null, null, null, "View" });
			foreach (DataRow row in views.Rows) {
				var schema = row["TABLE_SCHEMA"] as string;
				var name = row["TABLE_NAME"] as string;
				var view = CreateTableInstance(schema, name);
				view.SetView(true);
				tablesCollection.Add(view);
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			throw new NotSupportedException();
		}
	}
}
