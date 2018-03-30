using DataDevelop.Data.OleDb;

namespace DataDevelop.Data.Access
{
	internal sealed class AccessDatabase : OleDbDatabase
	{
		public AccessDatabase(string name, string connectionString)
			: base(name, connectionString)
		{
		}

		public override DbProvider Provider => AccessProvider.Instance;

		protected override OleDbTable CreateTableInstance(string schema, string name)
		{
			return new AccessTable(this) { Name = name };
		}
	}
}
