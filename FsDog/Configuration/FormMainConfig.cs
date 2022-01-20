using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FsDog.Configuration {
    public class FormMainConfig {
        public ToolStripsConfig ToolStrips { get; set; }
        public DetailViewStateConfig DetailView1 { get; set; }
        public DetailViewStateConfig DetailView2 { get; set; }
        [JsonProperty(PropertyName = "Size")]
        public string SizeName { get; set; }
        [JsonProperty(PropertyName = "Location")]
        public string LocationName { get; set; }
        [JsonProperty(PropertyName = "WindowState")]
        public string WindowStateName { get; set; }
        [JsonIgnore]
        public Size Size {
            get => FsDogConfig.SizeFromString(SizeName);
            set => SizeName = FsDogConfig.SizeToString(value);
        }
        [JsonIgnore]
        public Point Location {
            get => FsDogConfig.PointFromString(LocationName);
            set => LocationName = FsDogConfig.PointToString(value);
        }
        [JsonIgnore]
        public FormWindowState WindowState {
            get => (FormWindowState)Enum.Parse(typeof(FormWindowState), WindowStateName);
            set => WindowStateName = value.ToString();
        }
    }
}
