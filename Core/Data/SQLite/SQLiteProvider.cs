using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace DataDevelop.Data.SQLite
{
	public class SQLiteProvider : DbProvider
	{
		private static SQLiteProvider provider;

		public static SQLiteProvider Instance => provider ?? (provider = new SQLiteProvider());

		private SQLiteProvider() { }

		public override string Name => "SQLite";

		public override bool IsFileBased => true;

		public override string CreateDatabaseFile(string fileName)
		{
			using (var db = File.Create(fileName)) {
				// let 'using' dispose file
			}
			return "Data Source=" + fileName;
		}

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new SQLiteDatabase(name, connectionString);
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SQLiteConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "SQLite " + SQLiteConnection.SQLiteVersion;
		}
	}
}
