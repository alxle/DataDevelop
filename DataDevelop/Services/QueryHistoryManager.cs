using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using DataDevelop.Data;
using DataDevelop.Data.SQLite;

namespace DataDevelop
{
	internal sealed class QueryHistoryManager : IDisposable
	{
		static QueryHistoryManager instance;
		readonly SQLiteDatabase db;
		readonly SQLiteTable table;
		readonly DbDataAdapter adapter;
		readonly DataTable data;

		private QueryHistoryManager(string databaseFile)
		{
			var provider = SQLiteProvider.Instance;
			if (!File.Exists(databaseFile)) {
				provider.CreateDatabaseFile(databaseFile);
			}
			db = (SQLiteDatabase)provider.CreateDatabase("QueryHistory", $"Data Source=\"{databaseFile}\"");
			db.Connect();
			const string tableName = "QueryHistory";
			table = db.GetTable(tableName);
			if (table == null) {
				db.CreateTable(tableName,
					new Column("Id") { InPrimaryKey = true, IsIdentity = true },
					new Column("DbName", typeof(string)),
					new Column("DbProvider", typeof(string)),
					new Column("QueryText", typeof(string)),
					new Column("QueryDate", typeof(DateTime)),
					new Column("Status", typeof(string)),
					new Column("ElapsedSeconds", typeof(double)),
					new Column("ErrorMessage", typeof(string))
				);
				db.RefreshTables();
				table = db.GetTable(tableName);
			}
			adapter = db.CreateAdapter(table);
			adapter.SelectCommand.CommandText += " ORDER BY [Id] DESC LIMIT 50";
			data = new DataTable();
			adapter.Fill(data);
		}

		public static QueryHistoryManager Instance 
			=> instance ?? (instance = new QueryHistoryManager(Path.Combine(SettingsManager.DataDirectory, "DataDevelop.db")));

		public DataTable Data => data;

		public void Dispose()
		{
			db.DisconnectAll();
			if (db is IDisposable disposable) {
				disposable.Dispose();
			}
		}

		public long Insert(string dbName, string dbProvider, string queryText)
		{
			var row = data.NewRow();
			row["DbName"] = dbName;
			row["DbProvider"] = dbProvider;
			row["QueryText"] = queryText;
			row["QueryDate"] = DateTime.Now;
			row["Status"] = "Executing";
			data.Rows.Add(row);
			adapter.Update(data);
			var id = table.GetLastInsertRowId();
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

		public void Refresh()
		{
			data.Rows.Clear();
			adapter.Fill(data);
		}

		internal void SaveChanges()
		{
			adapter.Update(data);
			data.AcceptChanges();
		}
	}
}
