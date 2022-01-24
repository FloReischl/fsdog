using FsDog.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.FileSystem {
    public class DogDirectory : DogItem {
        public DogDirectory(string fileName) : this(new DirectoryInfo(fileName)) {
        }

        public DogDirectory(DirectoryInfo dir) {
            DirectoryInfo = dir;
        }

        public DirectoryInfo DirectoryInfo {
            get => (DirectoryInfo)base.FileSystemInfo;
            set => base.FileSystemInfo = value;
        }

        public override DogItemType ItemType => DogItemType.Directory;

        public override Image Image => Resources.DirectoryClosed;

        public override string Extension => string.Empty;

        public override long? Size => null;

        public override string TypeName => "Directory";
    }
}
