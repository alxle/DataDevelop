using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace DataDevelop.Data.SQLite
{
	public class SQLiteProvider : DbProvider
	{
		private static SQLiteProvider provider;
		
		private SQLiteProvider()
		{
		}
		
		public static SQLiteProvider Instance
		{
			get
			{
				if (provider == null) {
					provider = new SQLiteProvider();
				}
				return provider;
			}
		}

		public override string Name
		{
			get { return "SQLite"; }
		}

		public override bool IsFileBased
		{
			get { return true; }
		}

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
