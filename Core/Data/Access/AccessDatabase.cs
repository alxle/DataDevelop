using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DataDevelop.Data.Access
{
	internal sealed class AccessDatabase : Database, IDisposable
	{
		private string name;
		private OleDbConnection connection;

		public AccessDatabase(string name, string connectionString)
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
			get { return AccessProvider.Instance; }
		}

		public override string ConnectionString
		{
			get { return this.connection.ConnectionString; }
		}

		public override System.Data.Common.DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			AccessTable accessTable = (AccessTable)table;
			OleDbDataAdapter adapter = new OleDbDataAdapter(table.GetBaseSelectCommandText(filter), this.connection);
			if (!table.IsReadOnly) {
				OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
				builder.ConflictOption = ConflictOption.OverwriteChanges;
				try {
					builder.GetUpdateCommand();
				} catch (InvalidOperationException) {
					accessTable.SetReadOnly(true);
					return adapter;
				}
				OleDbCommand insertCommand = builder.GetInsertCommand();
				OleDbCommand deleteCommand = builder.GetDeleteCommand();
				OleDbCommand updateCommand = builder.GetUpdateCommand();
				/*foreach (Column column in table.Columns) {
					if (column.IsIdentity) {
						insertCommand.CommandText = insertCommand.CommandText + "; SELECT @rowId = last_insert_rowid()";
						insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
						SQLiteParameter parameter = new SQLiteParameter();
						parameter.ParameterName = "@rowId";
						//parameter.DbType = column.DbType;
						//parameter.Size = column.Size;
						parameter.Direction = ParameterDirection.Output;
						//parameter.IsNullable = column.AllowNulls;
						parameter.SourceColumn = column.Name;
						parameter.SourceVersion = DataRowVersion.Current;
						parameter.Value = DBNull.Value;
						insertCommand.Parameters.Add(parameter);
						break;
					}
				}*/
				adapter.DeleteCommand = deleteCommand;
				adapter.UpdateCommand = updateCommand;
				adapter.InsertCommand = insertCommand;
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
				AccessTable table = new AccessTable(this);
				table.Name = row["TABLE_NAME"].ToString();
				tablesCollection.Add(table);
			}

			restrictions[3] = "View";
			DataTable views = this.Connection.GetSchema("Tables", restrictions);
			foreach (DataRow row in views.Rows) {
				AccessTable table = new AccessTable(this);
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
