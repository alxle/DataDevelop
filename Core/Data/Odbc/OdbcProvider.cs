using System.Data.Common;
using System.Data.Odbc;

namespace DataDevelop.Data.Odbc
{
	internal sealed class OdbcProvider : DbProvider
	{
		private static OdbcProvider provider;

		private OdbcProvider() { }

		public static OdbcProvider Instance => provider ?? (provider = new OdbcProvider());

		public override string Name => "Odbc";

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new OdbcDatabase(name, connectionString);
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new OdbcConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "Microsoft ODBC";
		}
	}
}
