using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Configuration {
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
}
