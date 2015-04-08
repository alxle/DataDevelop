using System;
using System.Data.Common;
using Npgsql;

namespace DataDevelop.Data.PostgreSql
{
	public class PgSqlProvider : DbProvider
	{
		public override string Name
		{
			get { return "PgSql"; }
		}

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
