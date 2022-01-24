using FR;
using FR.Drawing;
using FR.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.FileSystem {
    public class DogFile : DogItem {
        private static readonly ConcurrentDictionary<string, string> _typeNamesByExtension = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, Image> _images = new ConcurrentDictionary<string, Image>(StringComparer.InvariantCultureIgnoreCase);

        public DogFile(string fileName) : this(new FileInfo(fileName)) {
        }

        public DogFile(FileInfo file) {
            FileInfo = file;
        }

        public FileInfo FileInfo {
            get => (FileInfo)base.FileSystemInfo;
            set => base.FileSystemInfo = value;
        }

        public override DogItemType ItemType => DogItemType.File;

        public override Image Image {
            get {
                var ext = Extension.ToLower();
                var key = BaseHelper.InList(ext, ".exe", ".scr", ".lnk", ".ico", ".cur") ? FileInfo.FullName : Extension;
                return _images.GetOrAdd(key, k => ImageHelper.ExtractAssociatedImage(FileInfo.FullName, true));
            }
        }

        public override string Extension => FileInfo.Extension;

        public override long? Size => FileInfo.Length;

        public override string TypeName => _typeNamesByExtension.GetOrAdd(Extension, k => FileHelper.GetTypeName(FileInfo.FullName));
    }
}
