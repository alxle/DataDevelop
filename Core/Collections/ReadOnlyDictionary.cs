using System;
using System.Collections.Generic;
using System.Collections;

namespace DataDevelop.Collections
{
	public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private IDictionary<TKey, TValue> dict;

		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
		{
			dict = dictionary;
		}

		public void Add(TKey key, TValue value)
		{
			throw new InvalidOperationException("This dictionary is read-only.");
		}

		public bool ContainsKey(TKey key)
		{
			return dict.ContainsKey(key);
		}

		public ICollection<TKey> Keys => dict.Keys;


		public bool Remove(TKey key)
		{
			throw new InvalidOperationException("This dictionary is read-only.");
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return dict.TryGetValue(key, out value);
		}

		public ICollection<TValue> Values => dict.Values;

		public TValue this[TKey key]
		{
			get => dict[key];
			set => throw new InvalidOperationException("This dictionary is read-only.");
		}

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
			return dict.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			dict.CopyTo(array, arrayIndex);
		}

		public int Count => dict.Count;

		public bool IsReadOnly => true;

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			throw new InvalidOperationException("This dictionary is read-only.");
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return dict.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return dict.GetEnumerator();
		}
	}
}
