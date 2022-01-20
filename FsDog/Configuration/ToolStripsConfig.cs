using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Configuration {
    public class ToolStripsConfig {
        public ToolStripConfig Main { get; set; }
        public ToolStripConfig ApplicationsCtrl { get; set; }
        public ToolStripConfig ApplicationsShift { get; set; }
        public ToolStripConfig ScriptsCtrl { get; set; }
        public ToolStripConfig ScriptsShift { get; set; }

        public ToolStripConfig ToolStrip(string name) {
            var prop = GetType().GetProperty(name);
            return (ToolStripConfig)prop.GetValue(this);
        }
    }
}
