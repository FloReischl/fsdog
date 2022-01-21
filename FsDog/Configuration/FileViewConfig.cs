using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Configuration {
    public class FileViewConfig {
        [Newtonsoft.Json.JsonProperty(PropertyName = "ActiveForeColor")]
        public string ActiveForeColorName { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "ActiveBackgroundColor")]
        public string ActiveBackgroundColorName { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "InactiveForeColor")]
        public string InactiveForeColorName { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "InactiveBackgroundColor")]
        public string InactiveBackgroundColorName { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "HiddenForeColor")]
        public string HiddenForeColorName { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "SystemForeColor")]
        public string SystemForeColorName { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "CompressedForeColor")]
        public string CompressedForeColorName { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "Font")]
        public string FontName { get; set; }

        [JsonIgnore()]
        public Color ActiveForeColor => ConfigConverter.ColorFromString(ActiveForeColorName);
        [JsonIgnore()]
        public Color ActiveBackgroundColor => ConfigConverter.ColorFromString(ActiveBackgroundColorName);
        [JsonIgnore()]
        public Color InactiveForeColor => ConfigConverter.ColorFromString(InactiveForeColorName);
        [JsonIgnore()]
        public Color InactiveBackgroundColor => ConfigConverter.ColorFromString(InactiveBackgroundColorName);
        [JsonIgnore()]
        public Color HiddenForeColor => ConfigConverter.ColorFromString(HiddenForeColorName);
        [JsonIgnore()]
        public Color SystemForeColor => ConfigConverter.ColorFromString(SystemForeColorName);
        [JsonIgnore()]
        public Color CompressedForeColor => ConfigConverter.ColorFromString(CompressedForeColorName);
        [JsonIgnore()]
        public Font Font => ConfigConverter.FontFromString(FontName);
    }
}
