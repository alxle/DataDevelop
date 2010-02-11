using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DataDevelop.Data.MySql
{
	public class MySqlProvider : DbProvider
	{
		public override string Name
		{
			get { return "MySql"; }
		}

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
