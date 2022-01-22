// Decompiled with JetBrains decompiler
// Type: FR.Collections.DataContext
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FR.Collections {
    public class DataContext : IDictionary<object, object> {
        private readonly ConcurrentDictionary<object, object> _dict = new ConcurrentDictionary<object, object>();
        private readonly object _syncRoot = new object();

        public object this[object key] {
            get => _dict[key];
            set => _dict[key] = value;
        }

        public ICollection<object> Values => _dict.Values;

        public int Count => _dict.Count;

        public ICollection<object> Keys => _dict.Keys;

        public bool IsReadOnly => ((IDictionary<object, object>)_dict).IsReadOnly;

        public virtual void Add<T>(object key, T value) => _dict.AddOrUpdate(key, value, (k, old) => value);

        public virtual void Clear() => _dict.Clear();

        public virtual bool ContainsKey(object key) => _dict.ContainsKey(key);

        //public virtual void CopyTo(Array array, int index) => ((ICollection)_dict).CopyTo(array, index);

        public virtual IEnumerator<KeyValuePair<object, object>> GetEnumerator() => _dict.GetEnumerator();

        public virtual bool Remove(object key) => _dict.TryRemove(key, out object value);

        public bool TryGetValue<T>(object key, out T value) {
            if (!_dict.TryGetValue(key, out object inner)) {
                value = default(T);
                return false;
            }

            value = (T)inner;
            return true;
        }

        public T TryGetValue<T>(object key) {
            TryGetValue(key, out T value);
            return value;
        }

        public T GetValue<T>(object key) {
            if (!TryGetValue(key, out T value)) {
                throw new ArgumentException($"Key '{key}' does not exist in data context.");
            }

            return value;
        }

        void IDictionary<object, object>.Add(object key, object value) => this.Add(key, value);

        bool IDictionary<object, object>.TryGetValue(object key, out object value) {
            return _dict.TryGetValue(key, out value);
        }

        void ICollection<KeyValuePair<object, object>>.Add(KeyValuePair<object, object> item) {
            ((IDictionary<object, object>)_dict).Add(item);
        }

        bool ICollection<KeyValuePair<object, object>>.Contains(KeyValuePair<object, object> item) {
            return ((IDictionary<object, object>)_dict).Contains(item);
        }

        void ICollection<KeyValuePair<object, object>>.CopyTo(KeyValuePair<object, object>[] array, int arrayIndex) {
            ((IDictionary<object, object>)_dict).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<object, object>>.Remove(KeyValuePair<object, object> item) {
            return ((IDictionary<object, object>)_dict).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IDictionary<object, object>)_dict).GetEnumerator();
        }
    }
}
