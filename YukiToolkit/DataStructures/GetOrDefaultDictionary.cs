using System;
using System.Collections;
using System.Collections.Generic;

namespace YukiToolkit.DataStructures {
	public class GetOrDefaultDictionary<T, TKey, TValue> : IDictionary<TKey, TValue>
		where T : IDictionary<TKey, TValue> {
		private readonly Func<TKey, TValue> _defaultValue;
		private T _baseDictionary;

		public GetOrDefaultDictionary(T baseDictionary, Func<TKey, TValue> defaultValue) {
			_baseDictionary = baseDictionary;
			_defaultValue = defaultValue;
		}

		public GetOrDefaultDictionary(T baseDictionary) {
			_baseDictionary = baseDictionary;
			_defaultValue = key => default!;
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
			return _baseDictionary.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public void Add(KeyValuePair<TKey, TValue> item) {
			_baseDictionary.Add(item);
		}

		public void Clear() {
			_baseDictionary.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item) {
			return _baseDictionary.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
			_baseDictionary.CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item) {
			return _baseDictionary.Remove(item);
		}

		public int Count => _baseDictionary.Count;
		public bool IsReadOnly => _baseDictionary.IsReadOnly;

		public void Add(TKey key, TValue value) {
			_baseDictionary.Add(key, value);
		}

		public bool ContainsKey(TKey key) {
			return _baseDictionary.ContainsKey(key);
		}

		public bool Remove(TKey key) {
			return _baseDictionary.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value) {
			return _baseDictionary.TryGetValue(key, out value);
		}

		public TValue this[TKey key] {
			get => TryGetValue(key, out var v) ? v : _defaultValue(key);
			set => _baseDictionary[key] = value;
		}

		public ICollection<TKey> Keys => _baseDictionary.Keys;
		public ICollection<TValue> Values => _baseDictionary.Values;
	}
}
