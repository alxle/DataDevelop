using System.Data.Common;
using System.Data.SqlServerCe;

namespace DataDevelop.Data.SqlCe
{
	internal class SqlCeProvider : DbProvider
	{
		private static SqlCeProvider provider;

		public static SqlCeProvider Instance => provider ?? (provider = new SqlCeProvider());

		private SqlCeProvider() { }

		public override string Name => "SqlCe";

		public override bool IsFileBased => true;

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new SqlCeDatabase(name, connectionString);
		}

		public override string CreateDatabaseFile(string fileName)
		{
			using (var engine = new SqlCeEngine("Data Source=" + fileName)) {
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
