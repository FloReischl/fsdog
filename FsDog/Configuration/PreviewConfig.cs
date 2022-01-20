using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Configuration {
    public class PreviewConfig {
        [JsonProperty(PropertyName = "TextFont")]
        public string TextFontName { get; set; }
        public List<string> TextExtensions { get; set; }
        public List<string> ImageExtensions { get; set; }
        [JsonIgnore()]
        public Font TextFont => FsDogConfig.FontFromString(TextFontName);
        public bool TextWrap { get; set; }
    }
}
