using System;
using System.Collections.Generic;
using System.Text;

namespace DataDevelop.Data
{
	public class DatabaseEventArgs : EventArgs
	{
		private Database database;

		public DatabaseEventArgs(Database database)
		{
			if (database == null) {
				throw new ArgumentNullException("database");
			}
			this.database = database;
		}

		public Database Database
		{
			get { return database; }
		}
	}
}
