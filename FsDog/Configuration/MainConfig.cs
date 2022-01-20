using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Configuration {
    public class MainConfig {
        public bool ShowStatusbar { get; set; }
        public bool FullPathInTitle { get; set; }
        public bool RememberSize { get; set; }
        public bool RememberLocation { get; set; }
        public bool RememberWindowState { get; set; }
        public string Location { get; set; }
    }
}
