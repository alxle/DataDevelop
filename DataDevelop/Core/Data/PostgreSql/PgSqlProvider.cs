using System.Data.Common;
using Npgsql;

namespace DataDevelop.Data.PostgreSql
{
	public class PgSqlProvider : DbProvider
	{
		private static PgSqlProvider provider;

		public static PgSqlProvider Instance => provider ?? (provider = new PgSqlProvider());

		private PgSqlProvider() { }

		public override string Name => "PgSql";

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new PgSqlDatabase(name, connectionString);
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new NpgsqlConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "PostgreSQL";
		}
	}
}
