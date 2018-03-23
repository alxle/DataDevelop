using System;

namespace DataDevelop.Data
{
	public struct ConnectionSettings
	{
		private Database database;

		public ConnectionSettings(Database database)
		{
			this.database = database ?? throw new ArgumentNullException(nameof(database));
		}

		public string ProviderName => database.Provider.Name;

		public string DatabaseName => database.Name;

		public string ConnectionString => database.ConnectionString;

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
			return database.Name.GetHashCode();
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
