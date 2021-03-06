using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR.Collections.Generic {
    public static class ListExtensions {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items) {
            foreach (var item in items) {
                list.Add(item);
            }
        }
    }
}
