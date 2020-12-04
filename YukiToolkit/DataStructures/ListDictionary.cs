using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YukiToolkit.DataStructures {
	public class ListDictionary<TKey, TValue> : IDictionary<TKey, TValue> {
		private readonly List<KeyValuePair<TKey, TValue>> _a = new List<KeyValuePair<TKey, TValue>>();

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
			return _a.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public void Add(KeyValuePair<TKey, TValue> item) {
			if (!ContainsKey(item.Key)) {
				_a.Add(item);
			}
		}

		public void Clear() {
			_a.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item) {
			return _a.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
			_a.CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item) {
			return _a.Remove(item);
		}

		public int Count => _a.Count;
		public bool IsReadOnly => false;

		public void Add(TKey key, TValue value) {
			if (!ContainsKey(key)) {
				_a.Add(new KeyValuePair<TKey, TValue>(key, value));
			}
		}

		public bool ContainsKey(TKey key) {
			return Find(key) >= 0;
		}

		public bool Remove(TKey key) {
			int idx = Find(key);
			if (idx >= 0) {
				_a.RemoveAt(idx);
				return true;
			}

			return false;
		}

		public bool TryGetValue(TKey key, out TValue value) {
			int idx = Find(key);
			if (idx >= 0) {
				value = _a[idx].Value;
				return true;
			}

			value = default!;
			return false;
		}

		public TValue this[TKey key] {
			get {
				if (TryGetValue(key, out TValue v)) {
					return v;
				}

				throw new KeyNotFoundException();
			}
			set {
				int idx = Find(key);
				if (idx >= 0) {
					TKey oldKey = _a[idx].Key;
					_a[idx] = new KeyValuePair<TKey, TValue>(oldKey, value);
				}
				else {
					_a.Add(new KeyValuePair<TKey, TValue>(key, value));
				}
			}
		}

		public ICollection<TKey> Keys => _a.Select(kvp => kvp.Key).ToList();
		public ICollection<TValue> Values => _a.Select(kvp => kvp.Value).ToList();

		private int Find(TKey key) {
			for (int i = 0; i < _a.Count; i++) {
				if (EqualityComparer<TKey>.Default.Equals(_a[i].Key, key)) {
					return i;
				}
			}

			return -1;
		}
	}
}
