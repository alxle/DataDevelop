using System;

namespace DataDevelop.Data
{
	public struct ConnectionSettings
	{
		private Database database;

		public ConnectionSettings(Database database)
		{
			if (database == null) {
				throw new ArgumentNullException("database");
			}
			this.database = database;
		}

		public string ProviderName
		{
			get { return this.database.Provider.Name; }
		}

		public string DatabaseName
		{
			get { return this.database.Name; }
		}

		public string ConnectionString
		{
			get { return this.database.ConnectionString; }
		}

		public static bool operator ==(ConnectionSettings x, ConnectionSettings y)
		{
			return Equals(x, y);
		}

		public static bool operator !=(ConnectionSettings x, ConnectionSettings y)
		{
			return !Equals(x, y);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ConnectionSettings)) {
				return false;
			}
			return Equals(this, (ConnectionSettings)obj);
		}

		private static bool Equals(ConnectionSettings x, ConnectionSettings y)
		{
			return x.database == y.database;
		}
	}
}
