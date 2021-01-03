using System;
using System.Collections;
using System.Collections.Generic;

namespace YukiToolkit.DataStructures {
	public class GetOrDefaultDictionary<TKey, TValue> : IDictionary<TKey, TValue> {
		private readonly IDictionary<TKey, TValue> _baseDictionary;

		public GetOrDefaultDictionary(IDictionary<TKey, TValue> baseDictionary, Func<TKey, TValue> defaultValue, bool autoAddIfNotExist = false) {
			_baseDictionary = baseDictionary;
			DefaultValue = defaultValue;
			AutoAddIfNotExist = autoAddIfNotExist;
		}

		public GetOrDefaultDictionary(IDictionary<TKey, TValue> baseDictionary, TValue defaultValue, bool autoAddIfNotExist = false) {
			_baseDictionary = baseDictionary;
			DefaultValue = key => defaultValue;
			AutoAddIfNotExist = autoAddIfNotExist;
		}

		public GetOrDefaultDictionary() {
			_baseDictionary = new SortedDictionary<TKey, TValue>();
			if (typeof(TValue).GetConstructor(Type.EmptyTypes) == null) {
				DefaultValue = key => default!;
				AutoAddIfNotExist = false;
			}
			else {
				DefaultValue = key => Activator.CreateInstance<TValue>();
				AutoAddIfNotExist = true;
			}
		}

		public bool AutoAddIfNotExist {
			get;
			set;
		}

		public Func<TKey, TValue> DefaultValue {
			get;
			set;
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
			get {
				if (TryGetValue(key, out var v)) {
					return v;
				}

				var result = DefaultValue(key);
				if (AutoAddIfNotExist) {
					Add(key, result);
				}

				return result;
			}
			set => _baseDictionary[key] = value;
		}

		public ICollection<TKey> Keys => _baseDictionary.Keys;
		public ICollection<TValue> Values => _baseDictionary.Values;
	}
}
