using System;
using System.Collections.Generic;
using System.Text;

namespace DataDevelop.Data
{
	public class DbObjectCollection<T> : IList<T>
		where T : DbObject
	{
		private SortedList<string, T> objects;
		////private Database database;

		////private DbObjectCollection(/*Database database, */bool sorted)
		////{
		////	//this.database = database;
		////    if (sorted) {
		////        this.objects = new SortedList<string, T>();
		////    } else {
		////        this.objects = new Dictionary<string, T>();
		////    }
		////}

		public DbObjectCollection()
		{
			this.objects = new SortedList<string, T>();
		}

		////public Database Database
		////{
		////    get { return this.database; }
		////}

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

		public int IndexOf(T item)
		{
			return this.objects.IndexOfValue(item);
		}

		public void Insert(int index, T item)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
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
			////this.objects.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return this.objects.Remove(item.Name);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this.objects.Values.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
