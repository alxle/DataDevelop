using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DataDevelop.Data.OleDb
{
	internal sealed class OleDbDatabase : Database, IDisposable
	{
		private string name;
		private OleDbConnection connection;

		public OleDbDatabase(string name, string connectionString)
		{
			this.name = name;
			this.connection = new OleDbConnection(connectionString);
		}

		public OleDbConnection Connection
		{
			get { return this.connection; }
		}

		public override string Name
		{
			get { return this.name; }
		}

		public override DbProvider Provider
		{
			get { return OleDbProvider.Instance; }
		}

		public override string ConnectionString
		{
			get { return this.connection.ConnectionString; }
		}

		public override System.Data.Common.DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			var oleDbTable = (OleDbTable)table;
			OleDbDataAdapter adapter = new OleDbDataAdapter(table.GetBaseSelectCommandText(filter), this.connection);
			if (!table.IsReadOnly) {
				OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
				builder.ConflictOption = ConflictOption.OverwriteChanges;
				try {
					builder.GetUpdateCommand();
				} catch (InvalidOperationException) {
					oleDbTable.SetReadOnly(true);
					return adapter;
				}
				adapter.InsertCommand = builder.GetInsertCommand();
				adapter.UpdateCommand = builder.GetUpdateCommand();
				adapter.DeleteCommand = builder.GetDeleteCommand();
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
			using (OleDbCommand command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override DataTable ExecuteTable(string commandText)
		{
			using (OleDbCommand command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				DataTable table = new DataTable();
				OleDbDataReader reader = command.ExecuteReader();
				table.Load(reader);
				reader.Close();
				return table;
			}
		}

		public override int ExecuteNonQuery(string commandText, System.Data.Common.DbTransaction transaction)
		{
			if ((object)transaction.Connection != (object)this.connection) {
				throw new InvalidOperationException();
			}
			using (OleDbCommand command = this.connection.CreateCommand()) {
				command.Transaction = (OleDbTransaction)transaction;
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (this.connection != null) {
				this.connection.Dispose();
			}
			GC.SuppressFinalize(this);
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
			string[] restrictions = new string[4];
			restrictions[3] = "Table";

			DataTable tables = this.Connection.GetSchema("Tables", restrictions);
			foreach (DataRow row in tables.Rows) {
				var table = new OleDbTable(this);
				table.Schema = row["TABLE_SCHEMA"] as string;
				table.Name = row["TABLE_NAME"] as string;
				tablesCollection.Add(table);
			}

			restrictions[3] = "View";
			DataTable views = this.Connection.GetSchema("Tables", restrictions);
			foreach (DataRow row in views.Rows) {
				var table = new OleDbTable(this);
				table.Name = row["TABLE_NAME"].ToString();
				table.SetView(true);
				tablesCollection.Add(table);
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			throw new NotSupportedException();
		}
	}
}
