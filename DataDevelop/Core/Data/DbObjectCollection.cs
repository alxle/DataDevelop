using System;
using System.Collections;
using System.Collections.Generic;

namespace DataDevelop.Data
{
	public class DbObjectCollection<T> : IList<T>
		where T : DbObject
	{
		private SortedList<string, T> objects;

		public DbObjectCollection()
		{
			objects = new SortedList<string, T>(StringComparer.OrdinalIgnoreCase);
		}

		public int Count
		{
			get { return objects.Count; }
		}

		public bool IsReadOnly
		{
			get { return ((IDictionary<string, T>)objects).IsReadOnly; }
		}

		public T this[int index]
		{
			get { return objects.Values[index]; }
			set { throw new InvalidOperationException(); }
		}

		public T this[string name]
		{
			get { return objects[name]; }
			set { throw new InvalidOperationException(); }
		}

		public T GetOrDefault(string name)
		{
			objects.TryGetValue(name, out var value);
			return value;
		}

		public int IndexOf(T item)
		{
			return objects.IndexOfValue(item);
		}

		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException("This is an automatically sorted collection, inserting is not supported.");
		}

		public void RemoveAt(int index)
		{
			objects.RemoveAt(index);
		}

		public void Add(T item)
		{
			objects.Add(item.Name, item);
		}

		public void Clear()
		{
			objects.Clear();
		}

		public bool Contains(T item)
		{
			return objects.Keys.Contains(item.Name);
		}

		public bool Contains(string name)
		{
			return objects.Keys.Contains(name);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			objects.Values.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return objects.Remove(item.Name);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return objects.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
