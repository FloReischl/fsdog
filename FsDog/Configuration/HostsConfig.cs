using FR.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Configuration {
    public class HostsConfig {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Arguments { get; set; }
        public ScriptExecutionLocation ExecuteAt { get; set; }

        public ScriptingHostConfiguration ToScriptingHost() => new ScriptingHostConfiguration {
            Name = this.Name,
            Location = this.Location,
            Arguments = Arguments,
            ExecutionLocation = ExecuteAt
        };

        public static HostsConfig FromHost(ScriptingHostConfiguration host) => new HostsConfig {
            Name = host.Name,
            Location = host.Location,
            Arguments = host.Arguments,
            ExecuteAt = host.ExecutionLocation
        };
    }
}
