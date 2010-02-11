using System;
using System.Collections.Generic;
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
			////if (!connectionString.Contains("Data Source=")) {
			////    connectionString = "Data Source=" + connectionString;
			////}
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
			SQLiteDataAdapter adapter = new SQLiteDataAdapter(table.GetBaseSelectCommandText(filter), this.connection);
			if (!table.IsReadOnly) {
				SQLiteCommandBuilder builder = new SQLiteCommandBuilder(adapter);
				builder.ConflictOption = ConflictOption.OverwriteChanges;
				try {
					builder.GetUpdateCommand();
				} catch (InvalidOperationException) {
					StringBuilder selectSql = new StringBuilder();
					selectSql.Append("SELECT RowId, ");
					filter.WriteColumnsProjection(selectSql);
					selectSql.Append(" FROM ");
					selectSql.Append(table.QuotedName);
					
					adapter = new SQLiteDataAdapter(selectSql.ToString(), this.connection);
					builder = new SQLiteCommandBuilder(adapter);
				}
				SQLiteCommand insertCommand = builder.GetInsertCommand();
				SQLiteCommand deleteCommand = builder.GetDeleteCommand();
				SQLiteCommand updateCommand = builder.GetUpdateCommand();
				foreach (Column column in table.Columns) {
					if (column.IsIdentity) {
						insertCommand.CommandText = insertCommand.CommandText + "; SELECT @RowId = last_insert_rowid()";
						insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
						SQLiteParameter parameter = new SQLiteParameter();
						parameter.ParameterName = "@RowId";
						////parameter.DbType = column.DbType;
						////parameter.Size = column.Size;
						parameter.Direction = ParameterDirection.Output;
						////parameter.IsNullable = column.AllowNulls;
						parameter.SourceColumn = column.Name;
						parameter.SourceVersion = DataRowVersion.Current;
						parameter.Value = DBNull.Value;
						insertCommand.Parameters.Add(parameter);
						break;
					}
				}
				adapter.DeleteCommand = deleteCommand;
				adapter.UpdateCommand = updateCommand;
				adapter.InsertCommand = insertCommand;
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
			////if (commandText.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase)) {
			////    string table = GetTableName(command.CommandText);
			////    if (table != null) {
			////        DbTransaction transaction = Connection.BeginTransaction();
			////        int records = SelectCount(table);
			////        command.ExecuteNonQuery();
			////        records -= SelectCount(table);
			////        TransactionDialog dialog = new TransactionDialog(records);
			////        if (dialog.ShowDialog(this) == DialogResult.OK) {
			////            transaction.Commit();
			////        } else {
			////            transaction.Rollback();
			////        }
			////    } else {
			////        command.ExecuteNonQuery();
			////    }
			////}

			using (DbCommand command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override DataTable ExecuteTable(string commandText)
		{
			using (SQLiteCommand command = this.connection.CreateCommand()) {
				command.CommandText = commandText;
				DataTable table = new DataTable();
				SQLiteDataReader reader = command.ExecuteReader();
				try {
					table.Load(reader);
				} catch (ConstraintException) {
				}
				reader.Close();
				return table;
			}
		}

		public override int ExecuteNonQuery(string commandText, DbTransaction transaction)
		{
			if ((object)transaction.Connection != (object)this.connection) {
				throw new InvalidOperationException();
			}
			using (SQLiteCommand command = this.connection.CreateCommand()) {
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
			string fileName = this.connectionStringBuilder.DataSource;
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
			DataTable tables = this.Connection.GetSchema("Tables");
			foreach (DataRow row in tables.Rows) {
				Table table = new SQLiteTable(this);
				table.Name = row["TABLE_NAME"].ToString();
				tablesCollection.Add(table);
			}

			DataTable views = this.Connection.GetSchema("Views");
			foreach (DataRow row in views.Rows) {
				SQLiteTable table = new SQLiteTable(this);
				table.Name = row["TABLE_NAME"].ToString();
				table.SetView(true);
				tablesCollection.Add(table);
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			throw new NotSupportedException("Stored Procedures are not supported.");
		}

		////private int SelectCount(string fromTable)
		////{
		////    DbCommand count = database.CreateCommand();
		////    count.CommandText = "SELECT COUNT(*) FROM [" + fromTable + "]";
		////    return Convert.ToInt32(count.ExecuteScalar());
		////}

		////private string GetTableName(string sqlStatement)
		////{
		////    string[] statements = sqlStatement.Split(' ', ',');
		////    for (int i = 0; i + 1 < statements.Length; i++) {
		////        if (String.Compare(statements[i], "FROM", true) == 0) {
		////            return statements[i + 1];
		////        }
		////    }
		////    return null;
		////}
	}
}
