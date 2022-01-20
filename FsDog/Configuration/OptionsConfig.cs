using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Configuration {
    public class OptionsConfig {
        public GeneralConfig General { get; set; }
        public NavigationConfig Navigation { get; set; }
        public DetailViewConfig DetailView { get; set; }
        public PreviewConfig Preview { get; set; }
        public AppearanceConfig Appearance { get; set; }
        public CommandsAppsConfig CommandsApps { get; set; }
        public MenusConfig Menus { get; set; }
    }
}
