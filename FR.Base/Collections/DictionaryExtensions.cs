using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace FR.Collections {
    public static class DictionaryExtensions {
        public static object GetOrAdd(this IDictionary dict, object key, Func<object, object> factory) {
            object value;
            if (!dict.Contains(key)) {
                value = factory(key);
                dict.Add(key, value);
            }
            else {
                value = dict[key];
            }
            return value;
        }
    }
}
