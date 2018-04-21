using System.Data.Common;
using System.Data.OleDb;

namespace DataDevelop.Data.OleDb
{
	internal sealed class OleDbProvider : DbProvider
	{
		private static OleDbProvider provider;

		private OleDbProvider() { }

		public static OleDbProvider Instance => provider ?? (provider = new OleDbProvider());

		public override string Name => "OleDb";

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new OleDbDatabase(name, connectionString);
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new OleDbConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "Microsoft OLE DB";
		}
	}
}
