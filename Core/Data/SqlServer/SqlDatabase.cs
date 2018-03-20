using System;
using System.Data;
using System.Data.Common;
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

		public override bool SupportUserDefinedFunctions
		{
			get { return true; }
		}

		public override DbDataAdapter CreateAdapter(Table table, TableFilter filter)
		{
			var sqlTable = table as SqlTable;
			if (sqlTable == null) {
				throw new InvalidOperationException("Table should be an SqlTable");
			}
			var adapter = new SqlDataAdapter(table.GetBaseSelectCommandText(filter), this.connection);
			if (!table.IsReadOnly) {
				var builder = new SqlCommandBuilder(adapter);
				builder.ConflictOption = ConflictOption.OverwriteChanges;
				try {
					adapter.UpdateCommand = builder.GetUpdateCommand();
				} catch (InvalidOperationException) {
					sqlTable.SetReadOnly(true);
					return adapter;
				}
				adapter.InsertCommand = builder.GetInsertCommand();
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
				var table = new DataTable();
				using (var reader = command.ExecuteReader()) {
					table.Load(reader);
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
			using (this.CreateConnectionScope()) {
				var restrictions = new string[4];
				restrictions[3] = "Base Table";
				using (var tables = this.Connection.GetSchema("Tables", restrictions)) {
					foreach (DataRow row in tables.Rows) {
						var table = new SqlTable(this, 
							(string)row["TABLE_SCHEMA"],
							(string)row["TABLE_NAME"]);
						tablesCollection.Add(table);
					}
				}

				var dataTable = this.ExecuteTable(@"
SELECT 
    t.NAME AS TableName,
    s.Name AS SchemaName,
    p.rows AS RowCounts,
    (SUM(a.total_pages) * 8) AS TotalSpaceKB, 
    (SUM(a.used_pages) * 8) AS UsedSpaceKB
FROM 
    sys.tables t
INNER JOIN      
    sys.indexes i ON t.OBJECT_ID = i.object_id
INNER JOIN 
    sys.partitions p ON i.object_id = p.OBJECT_ID AND i.index_id = p.index_id
INNER JOIN 
    sys.allocation_units a ON p.partition_id = a.container_id
LEFT OUTER JOIN 
    sys.schemas s ON t.schema_id = s.schema_id
WHERE 
    t.NAME NOT LIKE 'dt%' 
    AND t.is_ms_shipped = 0
    AND i.OBJECT_ID > 255 
GROUP BY 
    t.Name, s.Name, p.Rows");

				foreach (DataRow row in dataTable.Rows) {
					var schema = (string)row["SchemaName"];
					var name = (string)row["TableName"];
					foreach (SqlTable table in tablesCollection) {
						if (table.SchemaName == schema && table.TableName == name) {
							table.TotalRows = Convert.ToInt64(row["RowCounts"]);
							table.TotalSizeKB = Convert.ToDecimal(row["TotalSpaceKB"]);
							table.TotalUsedKB = Convert.ToDecimal(row["UsedSpaceKB"]);
							break;
						}
					}
				}

				restrictions[3] = "View";
				using (var views = this.Connection.GetSchema("Tables", restrictions)) {
					foreach (DataRow row in views.Rows) {
						var table = new SqlTable(this,
							(string)row["TABLE_SCHEMA"],
							(string)row["TABLE_NAME"]);
						table.SetView(true);
						tablesCollection.Add(table);
					}
				}
			}
		}

		protected override void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection)
		{
			var procedures = this.Connection.GetSchema("Procedures", new string[] { null, null, null, "PROCEDURE" });
			foreach (DataRow row in procedures.Rows) {
				var sp = new SqlStoredProcedure(this);
				sp.Schema = (string)row["SPECIFIC_SCHEMA"];
				sp.Name = (string)row["SPECIFIC_NAME"];
				storedProceduresCollection.Add(sp);
			}
		}

		protected override void PopulateUserDefinedFunctions(DbObjectCollection<UserDefinedFunction> userDefinedFunctionsCollection)
		{
			using (var select = this.Connection.CreateCommand()) {
				select.CommandText = 
					"SELECT b.Name AS [Schema], a.Name, type_name(p.system_type_id) " + 
					"FROM sys.objects a " +
					"INNER JOIN sys.schemas b ON a.schema_id = b.schema_id " +
					"INNER JOIN sys.parameters p ON p.object_id = a.object_id " +
					"WHERE a.type in ('FN', 'IF', 'TF') AND p.parameter_id = 0";
				using (var reader = select.ExecuteReader()) {
					while (reader.Read()) {
						var fn = new SqlUserDefinedFunction(this);
						fn.Schema = reader.GetString(0);
						fn.Name = reader.GetString(1);
						fn.ReturnType = reader.GetString(2);
						userDefinedFunctionsCollection.Add(fn);
					}
				}
			}
		}
	}
}
