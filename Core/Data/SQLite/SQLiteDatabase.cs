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
		private SQLiteConnectionStringBuilder connectionStringBuilder;

		public SQLiteDatabase(string name, string connectionString)
		{
			this.name = name;
			Connection = new SQLiteConnection(connectionString);
			connectionStringBuilder = new SQLiteConnectionStringBuilder(connectionString);
		}

		public override string Name => name;

		public override DbProvider Provider => SQLiteProvider.Instance;

		public override string ConnectionString => Connection.ConnectionString;

		internal SQLiteConnection Connection { get; }

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			if (!(table is SQLiteTable)) {
				throw new ArgumentException("Table must be of type SQLiteTable", "table");
			}
			var adapter = new SQLiteDataAdapter(table.GetBaseSelectCommandText(filter), Connection);
			if (!table.IsReadOnly) {
				var builder = new SQLiteCommandBuilder(adapter) {
					ConflictOption = ConflictOption.OverwriteChanges
				};

				SQLiteCommand updateCommand = null;
				try {
					updateCommand = builder.GetUpdateCommand();
				} catch (InvalidOperationException) {
					var selectSql = new StringBuilder();
					selectSql.Append("SELECT RowId, ");
					filter.WriteColumnsProjection(selectSql);
					selectSql.Append(" FROM ");
					selectSql.Append(table.QuotedName);
					
					adapter = new SQLiteDataAdapter(selectSql.ToString(), Connection);
					builder = new SQLiteCommandBuilder(adapter);
					updateCommand = builder.GetUpdateCommand();
				}
				var insertCommand = builder.GetInsertCommand();
				
				foreach (var column in table.Columns) {
					if (column.IsIdentity) {
						insertCommand.CommandText = insertCommand.CommandText + "; SELECT @RowId = last_insert_rowid()";
						insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
						var parameter = new SQLiteParameter {
							ParameterName = "@RowId",
							Direction = ParameterDirection.Output,
							SourceColumn = column.Name,
							SourceVersion = DataRowVersion.Current,
							Value = DBNull.Value
						};
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
				using (var adapter = new SQLiteDataAdapter(command)) {
					var table = new DataTable();
					adapter.Fill(table);
					return table;
				}
			}
		}

		public override int ExecuteNonQuery(string commandText, DbTransaction transaction)
		{
			if ((object)transaction.Connection != (object)Connection) {
				throw new InvalidOperationException();
			}
			using (var command = Connection.CreateCommand()) {
				command.Transaction = (SQLiteTransaction)transaction;
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public void Dispose()
		{
			if (Connection != null) {
				Connection.Dispose();
			}
		}

		public override void ChangeConnectionString(string newConnectionString)
		{
			if (IsConnected) {
				throw new InvalidOperationException("Database must be disconnected in order to change the ConnectionString");
			} else {
				connectionStringBuilder.ConnectionString = newConnectionString;
				Connection.ConnectionString = newConnectionString;
			}
		}

		protected override void DoConnect()
		{
			var fileName = connectionStringBuilder.DataSource;
			if (fileName.Contains("\"")) {
				fileName = fileName.Replace("\"", "");
			}
			if (!File.Exists(fileName)) {
				throw new InvalidOperationException($"The file '{fileName}' does not exists.");
			} else {
				Connection.Open();
			}
		}

		protected override void DoDisconnect()
		{
			Connection.Close();
		}

		protected override void PopulateTables(DbObjectCollection<Table> tablesCollection)
		{
			var tables = Connection.GetSchema("Tables");
				foreach (DataRow row in tables.Rows) {
				var table = new SQLiteTable(this) {
					Name = row["TABLE_NAME"].ToString()
				};
				tablesCollection.Add(table);
			}

			var views = Connection.GetSchema("Views");
			foreach (DataRow row in views.Rows) {
				var table = new SQLiteTable(this) {
					Name = row["TABLE_NAME"].ToString()
				};
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
