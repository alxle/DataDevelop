using System;

namespace DataDevelop.Data
{
	public class DatabaseEventArgs : EventArgs
	{
		public DatabaseEventArgs(Database database)
		{
			Database = database ?? throw new ArgumentNullException(nameof(database));
		}

		public Database Database { get; }
	}
}
