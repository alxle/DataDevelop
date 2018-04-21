using System;
using System.Data;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;

namespace DataDevelop.Data.Firebird
{
	internal sealed class FbDatabase : Database, IDisposable
	{
		private string name;
		private FbConnection connection;

		public FbDatabase(string name, string connectionString)
		{
			this.name = name;
			connection = new FbConnection(connectionString);
		}

		public override string Name => name;

		public override bool SupportStoredProcedures => true;

		public override bool SupportUserDefinedFunctions => true;

		public override string ParameterPrefix => "@";

		public override string QuotePrefix => "\"";

		public override string QuoteSuffix => "\"";

		public override DbProvider Provider => FbProvider.Instance;

		public override string ConnectionString => connection.ConnectionString;

		internal FbConnection Connection => connection;

		public void Dispose()
		{
			if (connection != null) {
				connection.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		public override void ChangeConnectionString(string newConnectionString)
		{
			if (IsConnected) {
				throw new InvalidOperationException("Database must be disconnected in order to change the ConnectionString");
			} else {
				connection.ConnectionString = newConnectionString;
			}
		}

		public override int ExecuteNonQuery(string commandText)
		{
			using (var command = connection.CreateCommand()) {
				command.CommandTimeout = 0;
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override int ExecuteNonQuery(string commandText, DbTransaction transaction)
		{
			using (CreateConnectionScope()) {
				var command = connection.CreateCommand();
				command.Transaction = (FbTransaction)transaction;
				command.CommandTimeout = 0;
				command.CommandText = commandText;
				return command.ExecuteNonQuery();
			}
		}

		public override DataTable ExecuteTable(string commandText)
		{
			using (var select = connection.CreateCommand()) {
				var table = new DataTable();
				select.CommandTimeout = 0;
				select.CommandText = commandText;
				using (var reader = select.ExecuteReader()) {
					table.Load(reader);
				}
				return table;
			}
		}

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			var adapter = new FbDataAdapter(table.GetBaseSelectCommandText(filter), connection);
			var builder = new FbCommandBuilder(adapter) {
				SetAllValues = true
			};
			try {
				adapter.InsertCommand = builder.GetInsertCommand(true);
				adapter.UpdateCommand = builder.GetUpdateCommand(true);
				adapter.DeleteCommand = builder.GetDeleteCommand(true);
			} catch {
			}
			return adapter;
		}

		public override DbCommand CreateCommand()
		{
			var command = connection.CreateCommand();
			command.CommandTimeout = 0;
			return command;
		}

		public override DbTransaction BeginTransaction()
		{
			return connection.BeginTransaction();
		}

		protected override void DoConnect()
		{
			connection.Open();
		}

		protected override void DoDisconnect()
		{
			connection.Close();
		}

		protected override void PopulateTables(DbObjectCollection<Table> tablesCollection)
		{
			using (var tables = connection.GetSchema("Tables")) {
				foreach (DataRow row in tables.Rows) {
					if ((short)row["IS_SYSTEM_TABLE"] == 0) {
						var tableType = ((string)row["TABLE_TYPE"]).ToUpper();
						var table = new FbTable(this, tableType == "VIEW") {
							TableName = (string)row["TABLE_NAME"]
						};
						var schema = row["TABLE_SCHEMA"] as string;
						if (!string.IsNullOrEmpty(schema)) {
							table.TableSchema = schema;
						}
						table.Name = table.TableName;
						tablesCollection.Add(table);
					}
				}
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			using (var procedures = connection.GetSchema("Procedures")) {
				foreach (DataRow row in procedures.Rows) {
					if ((short)row["IS_SYSTEM_PROCEDURE"] == 0) {
						var sp = new FbStoredProcedure(this, (string)row["PROCEDURE_NAME"]);
						storedProceduresCollection.Add(sp);
					}
				}
			}
		}

		protected override void PopulateUserDefinedFunctions(DbObjectCollection<UserDefinedFunction> userDefinedFunctions)
		{
			using (var functions = connection.GetSchema("Functions")) {
				foreach (DataRow row in functions.Rows) {
					if ((short)row["IS_SYSTEM_FUNCTION"] == 0) {
						var udf = new FbUserDefinedFunction(this, (string)row["FUNCTION_NAME"]);
						userDefinedFunctions.Add(udf);
					}
				}
			}
		}
	}
}
