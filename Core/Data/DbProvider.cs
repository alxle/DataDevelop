using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DataDevelop.Data
{
	public abstract class DbProvider
	{
		private static Dictionary<string, DbProvider> providers;

		private static IDictionary<string, DbProvider> Providers
		{
			get
			{
				if (providers == null) {
					providers = new Dictionary<string, DbProvider>(StringComparer.OrdinalIgnoreCase);
					LoadProviders();
				}
				return providers;
			}
		}

		public abstract string Name { get; }

		public virtual bool IsFileBased => false;

		public static IEnumerable<DbProvider> GetProviders()
		{
			return Providers.Values;
		}

		public static DbProvider GetProvider(string name)
		{
			if (Providers.TryGetValue(name, out var value)) {
				return value;
			}
			return null;
		}

		public static void RegisterProvider(DbProvider provider)
		{
			Providers.Add(provider.Name, provider);
		}

		public abstract Database CreateDatabase(string name, string connectionString);

		public virtual string CreateDatabaseFile(string fileName)
		{
			if (IsFileBased) {
				throw new NotImplementedException();
			}
			throw new InvalidOperationException();
		}

		public abstract DbConnectionStringBuilder CreateConnectionStringBuilder();

		private static void LoadProviders()
		{
			RegisterProvider(SQLite.SQLiteProvider.Instance);
			RegisterProvider(SqlServer.SqlProvider.Instance);
			RegisterProvider(SqlCe.SqlCeProvider.Instance);
			RegisterProvider(OleDb.OleDbProvider.Instance);
			RegisterProvider(Access.AccessProvider.Instance);
			RegisterProvider(MySql.MySqlProvider.Instance);
			RegisterProvider(PostgreSql.PgSqlProvider.Instance);
			RegisterProvider(Firebird.FbProvider.Instance);
			RegisterProvider(Odbc.OdbcProvider.Instance);
		}
	}
}
