using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.FileSystem {
    public abstract class DogItem {
        public FileSystemInfo FileSystemInfo { get; set; }

        public abstract DogItemType ItemType { get; }

        public abstract Image Image { get; }

        public string Name { get => FileSystemInfo.Name; }

        public abstract string Extension { get; }

        public abstract long? Size { get; }

        public abstract string TypeName { get; }

        public DateTime DateModified { get => FileSystemInfo.LastAccessTime; }

        public DateTime DateCreated { get => FileSystemInfo.CreationTime; }

        public string Attributes { get => GetAttributes(FileSystemInfo.Attributes); }

        protected string GetAttributes(FileAttributes attr) {
            return string.Format("{0}{1}{2}{3}",
                attr.HasFlag(FileAttributes.ReadOnly) ? "r" : "-",
                attr.HasFlag(FileAttributes.Archive) ? "a" : "-",
                attr.HasFlag(FileAttributes.Hidden) ? "h" : "-",
                attr.HasFlag(FileAttributes.System) ? "s" : "-"
                );
        }

        public override string ToString() => string.Format("{0}: {1}", GetType().Name, FileSystemInfo?.FullName);
    }
}
