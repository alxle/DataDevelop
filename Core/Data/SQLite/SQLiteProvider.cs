using System;
using System.Data.Common;
using System.Data.SQLite;

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

		public override Database CreateDatabaseFromFile(string name, string fileName)
		{
			return new SQLiteDatabase(name, "Data Source=" + fileName);
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
			return "SQLite 3.6.16";
		}
	}
}
