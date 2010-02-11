using System;
using System.Collections.Generic;
using System.Text;

namespace DataDevelop.Data
{
	public class DbObject : IDbObject
	{
		private Database database;
		private string name;

		public DbObject(Database database)
		{
			this.database = database;
		}

		public Database Database
		{
			get { return this.database; }
		}

		public virtual string Name
		{
			get { return this.name; }
			internal set { this.name = value; }
		}

		////internal KeyValuePair<string, DbObject> GetKeyValuePair()
		////{
		////    return new KeyValuePair<string, DbObject>(this.Name, this);
		////}
	}
}
