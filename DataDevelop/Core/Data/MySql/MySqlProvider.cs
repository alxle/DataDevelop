using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DataDevelop.Data.MySql
{
	public class MySqlProvider : DbProvider
	{
		private static MySqlProvider provider;

		public static MySqlProvider Instance => provider ?? (provider = new MySqlProvider());

		private MySqlProvider() { }

		public override string Name => "MySql";

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new MySqlDatabase(name, connectionString);
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new MySqlConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "MySQL";
		}
	}
}
