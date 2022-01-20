using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Configuration {
    public class ToolStripConfig {
        [JsonProperty(PropertyName = "Location")]
        public string LocationName { get; set; }
        [JsonIgnore]
        public Point Location {
            get => FsDogConfig.PointFromString(LocationName);
            set => LocationName = FsDogConfig.PointToString(value);
        }
    }
}
