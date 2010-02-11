using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataDevelop.Data.SqlServer
{
	internal sealed class SqlDatabase : Database, IDisposable
	{
		private string name;
		private SqlConnection connection;

		public SqlDatabase(string name, string connectionString)
		{
			this.name = name;
			this.connection = new SqlConnection(connectionString);
		}

		public SqlConnection Connection
		{
			get { return this.connection; }
		}
		
		public override string Name
		{
			get { return this.name; }
		}

		public override DbProvider Provider
		{
			get { return SqlProvider.Instance; }
		}

		public override string ConnectionString
		{
			get { return this.connection.ConnectionString; }
		}

		public override bool SupportStoredProcedures
		{
			get { return true; }
		}

		public override System.Data.Common.DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			SqlTable sqlTable = table as SqlTable;
			if (sqlTable == null) {
				throw new InvalidOperationException("Table should be an SqlTable");
			}
			SqlDataAdapter adapter = new SqlDataAdapter(table.GetBaseSelectCommandText(filter), this.connection);
			if (!table.IsReadOnly) {
				SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
				builder.ConflictOption = ConflictOption.OverwriteChanges;
				try {
					builder.GetUpdateCommand();
				} catch (InvalidOperationException) {
					sqlTable.SetReadOnly(true);
					return adapter;
				}
				SqlCommand insertCommand = builder.GetInsertCommand();
				SqlCommand deleteCommand = builder.GetDeleteCommand();
				SqlCommand updateCommand = builder.GetUpdateCommand();
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
			using (SqlCommand command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override DataTable ExecuteTable(string commandText)
		{
			using (SqlCommand command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				DataTable table = new DataTable();
				SqlDataReader reader = command.ExecuteReader();
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
			using (SqlCommand command = this.connection.CreateCommand()) {
				command.Transaction = (SqlTransaction)transaction;
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
			string[] restrictions = new string[4];
			restrictions[3] = "Base Table";
			using (DataTable tables = this.Connection.GetSchema("Tables", restrictions)) {
				foreach (DataRow row in tables.Rows) {
					SqlTable table = new SqlTable(this);
					table.Schema = (string)row["TABLE_SCHEMA"];
					table.Name = (string)row["TABLE_NAME"];
					tablesCollection.Add(table);
				}
			}
			restrictions[3] = "View";
			using (DataTable views = this.Connection.GetSchema("Tables", restrictions)) {
				foreach (DataRow row in views.Rows) {
					SqlTable table = new SqlTable(this);
					table.Schema = (string)row["TABLE_SCHEMA"];
					table.Name = (string)row["TABLE_NAME"];
					table.SetView(true);
					tablesCollection.Add(table);
				}
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			DataTable procedures = this.Connection.GetSchema("Procedures", new string[] { null, null, null, "PROCEDURE" });
			foreach (DataRow row in procedures.Rows) {
				SqlStoredProcedure sp = new SqlStoredProcedure(this);
				sp.Schema = (string)row["SPECIFIC_SCHEMA"];
				sp.Name = (string)row["SPECIFIC_NAME"];
				storedProceduresCollection.Add(sp);
			}
		}
	}
}
