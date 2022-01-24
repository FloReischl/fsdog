using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR.Collections {
    public static class ListExtensions {
        public static IList NotNull(this IList list) {
            return list ?? new object[0];
        }
    }
}
