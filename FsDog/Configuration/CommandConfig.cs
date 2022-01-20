using FsDog.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FsDog.Configuration {
    public class CommandConfig {
        public Keys? Key { get; set; }
        public CommandType CommandType { get; set; }
        public string Name { get; set; }
        public string Command { get; set; }
        public string Arguments { get; set; }
        public string ScriptingHost { get; set; }

        public CommandInfo ToCommandInfo() => new CommandInfo {
            Arguments = this.Arguments,
            Command = this.Command,
            CommandType = this.CommandType,
            Key = this.Key,
            Name = this.Name,
            ScriptingHost = this.ScriptingHost
        };

        public static CommandConfig FromInfo(CommandInfo info) => new CommandConfig {
            Key = info.Key,
            CommandType = info.CommandType,
            Name = info.Name,
            Command = info.Command,
            Arguments = info.Arguments
        };
    }
}
