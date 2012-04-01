using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using DataDevelop.Collections;

namespace DataDevelop.Data
{
	public abstract class DbProvider
	{
		private static Dictionary<string, DbProvider> providers = new Dictionary<string, DbProvider>(StringComparer.OrdinalIgnoreCase);
		private static ReadOnlyDictionary<string, DbProvider> readOnlyCollection;

		public static IDictionary<string, DbProvider> Providers
		{
			get
			{
				if (readOnlyCollection == null) {
					readOnlyCollection = new ReadOnlyDictionary<string, DbProvider>(providers);
					LoadProviders();
				}
				return readOnlyCollection;
			}
		}

		public abstract string Name
		{
			get;
		}

		public virtual bool IsFileBased
		{
			get { return false; }
		}

		public static DbProvider GetProvider(string name)
		{
			if (Providers.ContainsKey(name)) {
				return Providers[name];
			}
			return null;
		}

		public abstract Database CreateDatabase(string name, string connectionString);

		public virtual string CreateDatabaseFile(string fileName)
		{
			if (this.IsFileBased) {
				throw new NotImplementedException();
			}
			throw new InvalidOperationException();
		}
		
		public abstract DbConnectionStringBuilder CreateConnectionStringBuilder();

		private static void LoadProviders()
		{
			providers.Add("SQLite", SQLite.SQLiteProvider.Instance);
			providers.Add("Access", Access.AccessProvider.Instance);
			providers.Add("SqlServer", SqlServer.SqlProvider.Instance);
			providers.Add("MySql", new MySql.MySqlProvider());
			providers.Add("SqlCe", SqlCe.SqlCeProvider.Instance);
			providers.Add("PgSql", new PostgreSql.PgSqlProvider());
		}
	}
}
