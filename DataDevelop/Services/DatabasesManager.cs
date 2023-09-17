using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataDevelop.Data
{
	public static class DatabasesManager
	{
		private static Dictionary<string, Database> collection = new Dictionary<string, Database>(StringComparer.OrdinalIgnoreCase);
		
		private static ReadOnlyDictionary<string, Database> readOnlyCollection;

		public static bool IsCollectionDirty { get; internal set; }

		public static IDictionary<string, Database> Databases
			=> readOnlyCollection ?? (readOnlyCollection = new ReadOnlyDictionary<string, Database>(collection));

		public static bool Contains(string databaseName)
		{
			return collection.ContainsKey(databaseName);
		}

		public static void Add(Database database)
		{
			if (Contains(database.Name)) {
				throw new ArgumentException($"Already exists a database with name: {database.Name}", nameof(database));
			}
			collection.Add(database.Name, database);
			IsCollectionDirty = true;
			database.Connected += OnDatabaseConnected;
			database.Disconnected += OnDatabaseDisconnected;
			DatabaseAdded?.Invoke(Databases, new DatabaseEventArgs(database));
		}

		public static void Sort()
		{
			var sorted = new SortedDictionary<string, Database>(collection, StringComparer.OrdinalIgnoreCase);
			collection.Clear();
			foreach (var item in sorted) {
				collection.Add(item.Key, item.Value);
			}
			IsCollectionDirty = true;
		}

		public static bool Remove(Database database)
		{
			if (collection.Remove(database.Name)) {
				IsCollectionDirty = true;
				database.Connected -= OnDatabaseConnected;
				database.Disconnected -= OnDatabaseDisconnected;
				DatabaseRemoved?.Invoke(Databases, new DatabaseEventArgs(database));
				return true;
			}
			return false;
		}

		private static void OnDatabaseConnected(object sender, DatabaseEventArgs e)
		{
			DatabaseConnected?.Invoke(typeof(DatabasesManager), e);
		}

		private static void OnDatabaseDisconnected(object sender, DatabaseEventArgs e)
		{
			DatabaseDisconnected?.Invoke(typeof(DatabasesManager), e);
		}

		public static event EventHandler<DatabaseEventArgs> DatabaseAdded;

		public static event EventHandler<DatabaseEventArgs> DatabaseRemoved;

		public static event EventHandler<DatabaseEventArgs> DatabaseConnected;

		public static event EventHandler<DatabaseEventArgs> DatabaseDisconnected;
	}
}
