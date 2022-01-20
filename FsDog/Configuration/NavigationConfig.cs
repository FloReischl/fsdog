using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Configuration {
    public class NavigationConfig {
        public bool ShowHiddenFiles { get; set; }
        public bool ShowSystemFiles { get; set; }
        public bool ShowUserFolder { get; set; }
        public bool ShowMyDocumentsFolder { get; set; }
        public bool ResolveDirectoryLinks { get; set; }
    }
}
