using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

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

		[Browsable(false)]
		public Database Database
		{
			get { return this.database; }
		}

		public virtual string Name
		{
			get { return this.name; }
			internal set { this.name = value; }
		}
	}
}
