using System.Data.Common;
using System.Data.SqlClient;

namespace DataDevelop.Data.SqlServer
{
	internal class SqlProvider : DbProvider
	{
		private static SqlProvider provider;

		private SqlProvider() { }

		public static SqlProvider Instance => provider ?? (provider = new SqlProvider());

		public override string Name => "SqlServer";

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
