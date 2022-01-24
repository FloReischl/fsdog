using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FsDog.FileSystem {
    public class ViewFilter {
        private Regex _rx;

        public string PropertyName { get; set; }
        public ExpressionType Expression { get; set; }
        public string Value { get; set; }

        public void Prepare() {
            if (Expression != ExpressionType.Match) {
                throw new NotSupportedException("Only mach expression supported so far.");
            }

            if (PropertyName != "Name") {
                throw new NotSupportedException("Only Name property supported so far.");
            }

            _rx = new Regex(Value, RegexOptions.Compiled);
        }

        public bool IsValid(DogItem item) {
            return _rx.IsMatch(item.Name);
        }

        public string ToJsonString() {
            using (var sw = new StringWriter())
            using (var jw = new JsonTextWriter(sw)) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, this);
                return sw.GetStringBuilder().ToString();
            }
        }

        public static ViewFilter FromJsonString(string json) {
            using (var sr = new StringReader(json))
            using (var jr = new JsonTextReader(sr)) {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<ViewFilter>(jr);
            }
        }
    }

    public enum ExpressionType {
        Match,
        //Equals,
        //GreaterThan,
    }

    public enum LogicalOpeartor {
        And,
        Or,
    }
}
