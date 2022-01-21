using FR.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace FsDog.Configuration {
    public class FsDogConfig {
        public OptionsConfig Options { get; set; }
        public ScriptingConfig Scripting { get; set; }
        public List<string> Favorites { get; set; }
        public List<CommandConfig> Commands { get; set; }
        public AppConfig FsApp { get; set; }
        [JsonIgnore]
        public ConfigurationFile Source { get; private set; }

        public static string FileName { get => @"C:\Users\flori\AppData\Local\Florian Reischl\FsDog\1.1.0.0\FsDog.exe.json"; }

        public void Save() {
            Source.Save();
        }

        public static FsDogConfig Load() {
            var source = ConfigurationFile.TryGetUserConfigFile(typeof(FsApp).Assembly);
            ConfigurationProvider.Source = source;
            var dogConfig = source.GetRoot<FsDogConfig>();
            dogConfig.Source = source;
            return dogConfig;
        }
    }
}
