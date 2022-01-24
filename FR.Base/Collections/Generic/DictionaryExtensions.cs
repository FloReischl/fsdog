using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FR.Collections.Generic {
    public static class DictionaryExtensions {
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> factory) {
            if (!dict.TryGetValue(key, out TValue value)) {
                value = factory(key);
                dict.Add(key, value);
            }
            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value) {
            return GetOrAdd(dict, key, (k) => value);
        }

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value) {
            if (dict.ContainsKey(key)) {
                return false;
            }
            dict.Add(key, value);
            return true;
        }
    }
}
