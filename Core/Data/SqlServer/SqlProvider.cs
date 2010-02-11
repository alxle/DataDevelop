using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DataDevelop.Data.SqlServer
{
	internal class SqlProvider : DbProvider
	{
		private static SqlProvider provider;
		
		private SqlProvider()
		{
		}

		public static SqlProvider Instance
		{
			get
			{
				if (provider == null) {
					provider = new SqlProvider();
				}
				return provider;
			}
		}

		public override string Name
		{
			get { return "SqlServer"; }
		}

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new SqlDatabase(name, connectionString);
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqlConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "Microsoft SQL Server";
		}
	}
}
