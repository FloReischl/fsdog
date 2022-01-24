using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FR {
    public static class Check {
        public static void NotNullArg(string paramName, object value) {
            if (value == null) {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
