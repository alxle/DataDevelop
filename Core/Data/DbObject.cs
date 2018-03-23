using System.ComponentModel;

namespace DataDevelop.Data
{
	public class DbObject : IDbObject
	{
		public DbObject(Database database)
		{
			Database = database;
		}

		[Browsable(false)]
		public Database Database { get; }

		public virtual string Name { get; internal set; }
	}
}
