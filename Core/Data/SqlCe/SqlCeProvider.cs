using System;
using System.Data.Common;
using System.Data.SqlServerCe;

namespace DataDevelop.Data.SqlCe
{
	internal class SqlCeProvider : DbProvider
	{
		private static SqlCeProvider provider;
		
		private SqlCeProvider()
		{
		}

		public static SqlCeProvider Instance
		{
			get
			{
				if (provider == null) {
					provider = new SqlCeProvider();
				}
				return provider;
			}
		}

		public override string Name
		{
			get { return "SqlCe"; }
		}

		public override bool IsFileBased
		{
			get { return true; }
		}

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new SqlCeDatabase(name, connectionString);
		}

		public override string CreateDatabaseFile(string fileName)
		{
			using (SqlCeEngine engine = new SqlCeEngine("Data Source=" + fileName)) {
				engine.CreateDatabase();
				return engine.LocalConnectionString;
			}
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqlCeConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "Microsoft SQL Server Compact Edition";
		}
	}
}
