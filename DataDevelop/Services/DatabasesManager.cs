using System;
using System.Collections.Generic;

namespace DataDevelop.Data
{
	using Collections;

	public static class DatabasesManager
	{
		private static bool isCollectionDirty;

		private static Dictionary<string, Database> collection = new Dictionary<string, Database>(StringComparer.OrdinalIgnoreCase);
		
		private static ReadOnlyDictionary<string, Database> readOnlyCollection;

		public static IDictionary<string, Database> Databases
		{
			get
			{
				if (readOnlyCollection == null) {
					readOnlyCollection = new ReadOnlyDictionary<string, Database>(collection);
				}
				return readOnlyCollection;
			}
		}

		public static bool Contains(string databaseName)
		{
			return collection.ContainsKey(databaseName);
		}

		public static void Add(Database database)
		{
			if (Contains(database.Name)) {
				throw new ArgumentException("Already exists a database with name: " + database.Name, "database");
			}
			collection.Add(database.Name, database);
			isCollectionDirty = true;
			database.Connected += OnDatabaseConnected;
			database.Disconnected += OnDatabaseDisconnected;
			if (DatabaseAdded != null) {
				DatabaseAdded(Databases, new DatabaseEventArgs(database));
			}
		}

		public static void Sort()
		{
			SortedDictionary<string, Database> sorted = new SortedDictionary<string, Database>(collection, StringComparer.OrdinalIgnoreCase);
			collection.Clear();
			foreach (KeyValuePair<string, Database> item in sorted) {
				collection.Add(item.Key, item.Value);
			}
			isCollectionDirty = true;
		}

		public static bool Remove(Database database)
		{
			if (collection.Remove(database.Name)) {
				isCollectionDirty = true;
				database.Connected -= OnDatabaseConnected;
				database.Disconnected -= OnDatabaseDisconnected;
				if (DatabaseRemoved != null) {
					DatabaseRemoved(Databases, new DatabaseEventArgs(database));
				}
				return true;
			}
			return false;
		}

		public static bool IsCollectionDirty
		{
			get { return isCollectionDirty; }
			internal set { isCollectionDirty = value; }
		}

		private static void OnDatabaseConnected(object sender, DatabaseEventArgs e)
		{
			if (DatabaseConnected != null) {
				DatabaseConnected(typeof(DatabasesManager), e);
			}
		}

		private static void OnDatabaseDisconnected(object sender, DatabaseEventArgs e)
		{
			if (DatabaseDisconnected != null) {
				DatabaseDisconnected(typeof(DatabasesManager), e);
			}
		}

		public static event EventHandler<DatabaseEventArgs> DatabaseAdded;

		public static event EventHandler<DatabaseEventArgs> DatabaseRemoved;

		public static event EventHandler<DatabaseEventArgs> DatabaseConnected;

		public static event EventHandler<DatabaseEventArgs> DatabaseDisconnected;
	}
}
