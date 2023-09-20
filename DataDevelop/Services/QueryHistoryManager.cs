using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDevelop.Data;

namespace DataDevelop
{
	internal sealed class QueryHistoryManager : IDisposable
	{
		readonly Database db;
		readonly Table table;
		readonly DbDataAdapter adapter;
		readonly DataTable data;

		public QueryHistoryManager(string databaseFile)
		{
			var provider = DbProvider.GetProvider("SQLite");
			if (!File.Exists(databaseFile)) {
				provider.CreateDatabaseFile(databaseFile);
			}
			db = provider.CreateDatabase("QueryHistory", $"Data Source=\"{databaseFile}\"");
			db.Connect();
			table = db.GetTable("QueryHistory");
			if (table == null) {
				db.ExecuteNonQuery("""
					CREATE TABLE QueryHistory (
					  Id integer primary key,
					  DbName text,
					  QueryText text,
					  QueryDate datetime,
					  Status text,
					  ElapsedSeconds real,
					  ErrorMessage text
					);
					""");
				db.RefreshTables();
				table = db.GetTable("QueryHistory");
			}
			adapter = db.CreateAdapter(table);
			data = table.GetData(0, 20);
		}

		public DataTable Data => data;

		public void Dispose()
		{
			db.DisconnectAll();
			if (db is IDisposable disposable) {
				disposable.Dispose();
			}
		}

		public long Insert(string dbName, string queryText)
		{
			var row = data.NewRow();
			row["DbName"] = dbName;
			row["QueryText"] = queryText;
			row["QueryDate"] = DateTime.Now;
			row["Status"] = "Executing";
			data.Rows.Add(row);
			adapter.Update(data);
			var conn = (System.Data.SQLite.SQLiteConnection)adapter.InsertCommand.Connection;
			var id = conn.LastInsertRowId;
			row["Id"] = id;
			row.AcceptChanges();
			return id;
		}

		public int Update(long id, TimeSpan elapsed, string status, string errorMessage = null)
		{
			var row = data.AsEnumerable().FirstOrDefault(r => r.Field<long>(0) == id);
			if (row == null) {
				return 0;
			}
			row.BeginEdit();
			row["Status"] = status;
			row["ElapsedSeconds"] = elapsed.TotalSeconds;
			if (errorMessage != null) {
				row["ErrorMessage"] = errorMessage;
			}
			row.EndEdit();
			return adapter.Update(data);
		}
	}
}
