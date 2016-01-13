using System;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Data.Common;

namespace DataDevelop.Data.SQLite
{
	public sealed class SQLiteDatabase : Database, IDisposable
	{
		private string name;
		private readonly SQLiteConnection connection;
		private SQLiteConnectionStringBuilder connectionStringBuilder;

		public SQLiteDatabase(string name, string connectionString)
		{
			this.name = name;
			this.connection = new SQLiteConnection(connectionString);
			this.connectionStringBuilder = new SQLiteConnectionStringBuilder(connectionString);
		}

		public override string Name
		{
			get { return this.name; }
		}

		public override DbProvider Provider
		{
			get { return SQLiteProvider.Instance; }
		}

		public override string ConnectionString
		{
			get { return this.connection.ConnectionString; }
		}

		internal SQLiteConnection Connection
		{
			get { return this.connection; }
		}

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			if (!(table is SQLiteTable)) {
				throw new ArgumentException("Table must be of type SQLiteTable", "table");
			}
			var adapter = new SQLiteDataAdapter(table.GetBaseSelectCommandText(filter), this.connection);
			if (!table.IsReadOnly) {
				var builder = new SQLiteCommandBuilder(adapter);
				builder.ConflictOption = ConflictOption.OverwriteChanges;

				SQLiteCommand updateCommand = null;
				try {
					updateCommand = builder.GetUpdateCommand();
				} catch (InvalidOperationException) {
					StringBuilder selectSql = new StringBuilder();
					selectSql.Append("SELECT RowId, ");
					filter.WriteColumnsProjection(selectSql);
					selectSql.Append(" FROM ");
					selectSql.Append(table.QuotedName);
					
					adapter = new SQLiteDataAdapter(selectSql.ToString(), this.connection);
					builder = new SQLiteCommandBuilder(adapter);
					updateCommand = builder.GetUpdateCommand();
				}
				var insertCommand = builder.GetInsertCommand();
				
				foreach (var column in table.Columns) {
					if (column.IsIdentity) {
						insertCommand.CommandText = insertCommand.CommandText + "; SELECT @RowId = last_insert_rowid()";
						insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
						var parameter = new SQLiteParameter();
						parameter.ParameterName = "@RowId";
						parameter.Direction = ParameterDirection.Output;
						parameter.SourceColumn = column.Name;
						parameter.SourceVersion = DataRowVersion.Current;
						parameter.Value = DBNull.Value;
						insertCommand.Parameters.Add(parameter);
						break;
					}
				}
				adapter.UpdateCommand = updateCommand;
				adapter.InsertCommand = insertCommand;
				adapter.DeleteCommand = builder.GetDeleteCommand();
				
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
				using (var adapter = new SQLiteDataAdapter(command)) {
					var table = new DataTable();
					adapter.Fill(table);
					return table;
				}
			}
		}

		public override int ExecuteNonQuery(string commandText, DbTransaction transaction)
		{
			if ((object)transaction.Connection != (object)this.connection) {
				throw new InvalidOperationException();
			}
			using (var command = this.connection.CreateCommand()) {
				command.Transaction = (SQLiteTransaction)transaction;
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public void Dispose()
		{
			if (this.connection != null) {
				this.connection.Dispose();
			}
		}

		public override void ChangeConnectionString(string newConnectionString)
		{
			if (this.IsConnected) {
				throw new InvalidOperationException("Database must be disconnected in order to change the ConnectionString");
			} else {
				this.connectionStringBuilder.ConnectionString = newConnectionString;
				this.connection.ConnectionString = newConnectionString;
			}
		}

		protected override void DoConnect()
		{
			var fileName = this.connectionStringBuilder.DataSource;
			if (fileName.Contains("\"")) {
				fileName = fileName.Replace("\"", "");
			}
			if (!File.Exists(fileName)) {
				throw new InvalidOperationException(String.Format("The file '{0}' does not exists.", fileName));
			} else {
				this.connection.Open();
			}
		}

		protected override void DoDisconnect()
		{
			this.connection.Close();
		}

		protected override void PopulateTables(DbObjectCollection<Table> tablesCollection)
		{
			var tables = this.Connection.GetSchema("Tables");
			foreach (DataRow row in tables.Rows) {
				var table = new SQLiteTable(this);
				table.Name = row["TABLE_NAME"].ToString();
				tablesCollection.Add(table);
			}

			var views = this.Connection.GetSchema("Views");
			foreach (DataRow row in views.Rows) {
				var table = new SQLiteTable(this);
				table.Name = row["TABLE_NAME"].ToString();
				table.SetView(true);
				tablesCollection.Add(table);
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			throw new NotSupportedException("Stored Procedures are not supported.");
		}
	}
}
