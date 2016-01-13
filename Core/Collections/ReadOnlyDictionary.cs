using System;
using System.Collections.Generic;
using System.Collections;

namespace DataDevelop.Collections
{
	public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private IDictionary<TKey, TValue> innerList;

		public ReadOnlyDictionary(IDictionary<TKey, TValue> list)
		{
			this.innerList = list;
		}

		#region IDictionary<TKey,TValue> Members

		public void Add(TKey key, TValue value)
		{
			throw new InvalidOperationException("This dictionary is read-only.");
		}

		public bool ContainsKey(TKey key)
		{
			return innerList.ContainsKey(key);
		}

		public ICollection<TKey> Keys
		{
			get { return innerList.Keys; }
		}

		public bool Remove(TKey key)
		{
			throw new InvalidOperationException("This dictionary is read-only.");
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return innerList.TryGetValue(key, out value);
		}

		public ICollection<TValue> Values
		{
			get { return innerList.Values; }
		}

		public TValue this[TKey key]
		{
			get { return innerList[key]; }
			set { throw new InvalidOperationException("This dictionary is read-only."); }
		}

		#endregion

		#region ICollection<KeyValuePair<TKey,TValue>> Members

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			throw new InvalidOperationException("This dictionary is read-only.");
		}

		public void Clear()
		{
			throw new InvalidOperationException("This dictionary is read-only.");
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return innerList.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			innerList.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return innerList.Count; }
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new InvalidOperationException("This dictionary is read-only.");
		}

		#endregion

		#region IEnumerable<KeyValuePair<TKey,TValue>> Members

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return innerList.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return innerList.GetEnumerator();
		}

		#endregion
	}
}
