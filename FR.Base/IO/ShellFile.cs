// Decompiled with JetBrains decompiler
// Type: FR.IO.ShellFile
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.IO;

namespace FR.IO {
    public class ShellFile : ShellItem {
        public ShellFile(string fileName)
          : base(fileName) {
        }

        internal ShellFile(string fileName, WIN32_FIND_DATA data)
          : base(fileName, data) {
        }

        public ShellDirectory Directory => new ShellDirectory(Path.GetDirectoryName(this.FullName));

        public string Extension => Path.GetExtension(this.FullName);

        public override string Name => Path.GetFileName(this.FullName);

        public ulong Size {
            get {
                if (!this.IsLoaded)
                    this.Refresh();
                return ShellApi.IntToLong(this.Data.nFileSizeLow, this.Data.nFileSizeHigh);
            }
        }

        public override void Refresh() {
            if (this.Data == null)
                this.Data = new WIN32_FIND_DATA();
            ShellApi.FindClose(ShellApi.FindFirstFile(this.FullName, ((ShellItem)this).Data));
            this.IsLoaded = true;
        }
    }
}
