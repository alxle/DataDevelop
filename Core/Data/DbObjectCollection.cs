using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DataDevelop.Data
{
	public class DbObjectCollection<T> : IList<T>
		where T : DbObject
	{
		private SortedList<string, T> objects;

		public DbObjectCollection()
		{
			this.objects = new SortedList<string, T>(StringComparer.OrdinalIgnoreCase);
		}

		public int Count
		{
			get { return this.objects.Count; }
		}

		public bool IsReadOnly
		{
			get { return ((IDictionary<string, T>)this.objects).IsReadOnly; }
		}

		public T this[int index]
		{
			get { return this.objects.Values[index]; }
			set { throw new InvalidOperationException(); }
		}

		public T this[string name]
		{
			get { return this.objects[name]; }
			set { throw new InvalidOperationException(); }
		}

		public T GetOrDefault(string name)
		{
			T value = default(T);
			this.objects.TryGetValue(name, out value);
			return value;
		}

		public int IndexOf(T item)
		{
			return this.objects.IndexOfValue(item);
		}

		void IList<T>.Insert(int index, T item)
		{
			throw new NotSupportedException("This is an automatically sorted collection, inserting is not supported.");
		}

		public void RemoveAt(int index)
		{
			this.objects.RemoveAt(index);
		}

		public void Add(T item)
		{
			this.objects.Add(item.Name, item);
		}

		public void Clear()
		{
			this.objects.Clear();
		}

		public bool Contains(T item)
		{
			return this.objects.Keys.Contains(item.Name);
		}

		public bool Contains(string name)
		{
			return this.objects.Keys.Contains(name);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			this.objects.Values.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return this.objects.Remove(item.Name);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this.objects.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
