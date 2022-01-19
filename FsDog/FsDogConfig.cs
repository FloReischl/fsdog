using FR.Windows.Forms;
using FsDog.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FsDog {
    public class FsDogConfig {
        private static Dictionary<string, Color> _colors = new Dictionary<string, Color>();
        private static ColorConverter _colorConverter = new ColorConverter();
        private static Dictionary<string, Font> _fonts = new Dictionary<string, Font>();
        private static FontConverter _fontConverter = new FontConverter();

        public OptionsConfig Options { get; set; }
        public ScriptingConfig Scripting { get; set; }
        public List<string> Favorites { get; set; }
        public List<CommandConfig> Commands { get; set; }
        public FsAppConfig FsApp { get; set; }

        //public RootConfig Root { get; set; }

        //public void Load() {
        //    using (var sr = new StreamReader(@"C:\Users\flori\AppData\Local\Florian Reischl\FsDog\1.1.0.0\FsDog.exe.json"))
        //    using (var jr = new JsonTextReader(sr)) {
        //        JsonSerializer serializer = new JsonSerializer();
        //        Root = serializer.Deserialize<RootConfig>(jr);
        //    }
        //}

        public static string FileName { get => @"C:\Users\flori\AppData\Local\Florian Reischl\FsDog\1.1.0.0\FsDog.exe.json"; }

        public void Save() {
            using (var sw = new StringWriter())
            using (var jw = new JsonTextWriter(sw)) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(jw, this);
                File.WriteAllText(FileName, sw.GetStringBuilder().ToString());
            }
        }

        public static FsDogConfig Load() {
            using (var sr = new StreamReader(FileName))
            using (var jr = new JsonTextReader(sr)) {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<FsDogConfig>(jr);
            }
        }

        private static Color ColorFromString(string name) {
            if (!_colors.TryGetValue(name, out Color color)) {
                color = (Color)_colorConverter.ConvertFromString(name);
                _colors.Add(name, color);
            }
            return color;
        }

        private static Font FontFromString(string name) {
            if (!_fonts.TryGetValue(name, out Font font)) {
                font = (Font)_fontConverter.ConvertFromString(name);
                _fonts.Add(name, font);
            }
            return font;
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class GeneralConfig {
            public bool RestoreDirectories { get; set; }
            public bool RestoreNetworkDirectories { get; set; }
            public bool CacheImages { get; set; }
        }

        public class NavigationConfig {
            public bool ShowHiddenFiles { get; set; }
            public bool ShowSystemFiles { get; set; }
            public bool ShowUserFolder { get; set; }
            public bool ShowMyDocumentsFolder { get; set; }
            public bool ResolveDirectoryLinks { get; set; }
        }

        public class DetailViewConfig {
            public string CustomDateTimeFormat { get; set; }
            public bool SynchronizeColumnWidths { get; set; }
            public bool ShowModificationDateColumn { get; set; }
            public bool ShowCreationDateColumn { get; set; }
            public bool ShowFileExtensionColumn { get; set; }
            public bool ShowAttributesColumn { get; set; }
            public bool ShowGridLines { get; set; }
            public bool ShowDirectorySize { get; set; }
            public bool DirectoriesAlwasOnTop { get; set; }
            public bool LoadImagesAsynchronous { get; set; }
        }

        public class PreviewConfig {
            [JsonProperty(PropertyName = "TextFont")]
            public string TextFontName { get; set; }
            public List<string> TextExtensions { get; set; }
            public List<string> ImageExtensions { get; set; }
            [JsonIgnore()]
            public Font TextFont => FsDogConfig.FontFromString(TextFontName);
            public bool TextWrap { get; set; }
        }

        public class MainConfig {
            public bool ShowStatusbar { get; set; }
            public bool FullPathInTitle { get; set; }
            public bool RememberSize { get; set; }
            public bool RememberLocation { get; set; }
            public bool RememberWindowState { get; set; }
            public string Location { get; set; }
        }

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
            public Color ActiveForeColor => FsDogConfig.ColorFromString(ActiveForeColorName);
            [JsonIgnore()]
            public Color ActiveBackgroundColor => FsDogConfig.ColorFromString(ActiveBackgroundColorName);
            [JsonIgnore()]
            public Color InactiveForeColor => FsDogConfig.ColorFromString(InactiveForeColorName);
            [JsonIgnore()]
            public Color InactiveBackgroundColor => FsDogConfig.ColorFromString(InactiveBackgroundColorName);
            [JsonIgnore()]
            public Color HiddenForeColor => FsDogConfig.ColorFromString(HiddenForeColorName);
            [JsonIgnore()]
            public Color SystemForeColor => FsDogConfig.ColorFromString(SystemForeColorName);
            [JsonIgnore()]
            public Color CompressedForeColor => FsDogConfig.ColorFromString(CompressedForeColorName);
            [JsonIgnore()]
            public Font Font => FsDogConfig.FontFromString(FontName);
        }

        public class AppearanceConfig {
            public MainConfig Main { get; set; }
            public FileViewConfig FileView { get; set; }
        }

        public class CommandsAppsConfig {
            public bool ShowIcon { get; set; }
            public bool ShowName { get; set; }
            public bool ShowShortcut { get; set; }
        }

        public class ApplicationsConfig {
            public bool ShowShiftToolStrip { get; set; }
            public bool ShowShiftImage { get; set; }
            public bool ShowShiftName { get; set; }
            public bool ShowShiftShortcut { get; set; }
            public bool ShowCtrlToolStrip { get; set; }
            public bool ShowCtrlImage { get; set; }
            public bool ShowCtrlName { get; set; }
            public bool ShowCtrlShortcut { get; set; }
        }

        public class ScriptsConfig {
            public bool ShowShiftToolStrip { get; set; }
            public bool ShowShiftImage { get; set; }
            public bool ShowShiftName { get; set; }
            public bool ShowShiftShortcut { get; set; }
            public bool ShowCtrlToolStrip { get; set; }
            public bool ShowCtrlImage { get; set; }
            public bool ShowCtrlName { get; set; }
            public bool ShowCtrlShortcut { get; set; }
        }

        public class MenusConfig {
            public ApplicationsConfig Applications { get; set; }
            public ScriptsConfig Scripts { get; set; }
        }

        public class OptionsConfig {
            public GeneralConfig General { get; set; }
            public NavigationConfig Navigation { get; set; }
            public DetailViewConfig DetailView { get; set; }
            public PreviewConfig Preview { get; set; }
            public AppearanceConfig Appearance { get; set; }
            public CommandsAppsConfig CommandsApps { get; set; }
            public MenusConfig Menus { get; set; }
        }

        //public class ItemConfig {
        //    public string Name { get; set; }
        //    public string Location { get; set; }
        //    public string Arguments { get; set; }
        //    public string ExecuteAt { get; set; }
        //}

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

            public static HostsConfig FromHost(ScriptingHostConfiguration host) => new FsDogConfig.HostsConfig {
                Name = host.Name,
                Location = host.Location,
                Arguments = host.Arguments,
                ExecuteAt = host.ExecutionLocation
            };
        }

        public class ScriptingConfig {
            public List<HostsConfig> Hosts { get; set; }
        }

        //public class FavoriteConfig {
        //    public string Directory { get; set; }
        //}

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

        public class ApplicationsCtrlConfig {
            public string Location { get; set; }
        }

        public class ScriptsCtrlConfig {
            public string Location { get; set; }
        }

        public class ScriptsShiftConfig {
            public string Location { get; set; }
        }

        public class ToolStripsConfig {
            public MainConfig Main { get; set; }
            public ApplicationsCtrlConfig ApplicationsCtrl { get; set; }
            public ScriptsCtrlConfig ScriptsCtrl { get; set; }
            public ScriptsShiftConfig ScriptsShift { get; set; }
        }

        public class ColumnConfig {
            public string Name { get; set; }
            public int Width { get; set; }
        }

        public class DetailViewStateConfig {
            public List<ColumnConfig> Columns { get; set; }
            public string Path { get; set; }
        }

        public class FormMainConfig {
            public ToolStripsConfig ToolStrips { get; set; }
            public DetailViewStateConfig DetailView1 { get; set; }
            public DetailViewStateConfig DetailView2 { get; set; }
            public string Size { get; set; }
            public string Location { get; set; }
            public string WindowState { get; set; }
        }

        public class FsAppConfig {
            public FormMainConfig FormMain { get; set; }
        }

        public class RootConfig {
            public OptionsConfig Options { get; set; }
            public ScriptingConfig Scripting { get; set; }
            public List<string> Favorites { get; set; }
            public List<CommandConfig> Commands { get; set; }
            public FsAppConfig FsApp { get; set; }
        }
    }
}
