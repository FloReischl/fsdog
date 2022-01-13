// Decompiled with JetBrains decompiler
// Type: FR.IO.ShellDirectory
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace FR.IO {
    public sealed class ShellDirectory : ShellItem {
        private bool _cancelGetSize;

        public ShellDirectory(string directory)
          : base(directory) {
            if (!this.FullName.EndsWith("\\"))
                return;
            this.FullName = this.FullName.TrimEnd('\\');
        }

        internal ShellDirectory(string directory, WIN32_FIND_DATA data)
          : base(directory, data) {
        }

        public event ShellDirectorySizeEndHandler GetSizeEnd;

        public override string Name => Path.GetFileName(this.FullName);

        public ShellDirectory Parent {
            get {
                string directoryName = Path.GetDirectoryName(this.FullName);
                return string.IsNullOrEmpty(directoryName) ? (ShellDirectory)null : new ShellDirectory(directoryName);
            }
        }

        public ulong Size { get; private set; }

        public void BeginGetSize() {
            this._cancelGetSize = false;
            new Thread(new ThreadStart(this.GetSizeAsync)).Start();
        }

        public void CancelGetSize() => this._cancelGetSize = true;

        public ReadOnlyCollection<ShellFile> GetFiles() {
            ReadOnlyCollection<ShellItem> items = this.GetItems();
            List<ShellFile> list = new List<ShellFile>(items.Count);
            foreach (ShellItem shellItem in items) {
                if (shellItem is ShellFile shellFile)
                    list.Add(shellFile);
            }
            return new ReadOnlyCollection<ShellFile>((IList<ShellFile>)list);
        }

        public ReadOnlyCollection<ShellDirectory> GetDirectories() {
            ReadOnlyCollection<ShellItem> items = this.GetItems();
            List<ShellDirectory> list = new List<ShellDirectory>(items.Count);
            foreach (ShellItem shellItem in items) {
                if (shellItem is ShellDirectory shellDirectory)
                    list.Add(shellDirectory);
            }
            return new ReadOnlyCollection<ShellDirectory>((IList<ShellDirectory>)list);
        }

        public ReadOnlyCollection<ShellItem> GetItems() {
            WIN32_FIND_DATA wiN32FindData = new WIN32_FIND_DATA();
            IntPtr firstFile = ShellApi.FindFirstFile(Path.Combine(this.FullName, "*"), wiN32FindData);
            List<ShellItem> list = new List<ShellItem>(100);
            if (firstFile != ShellApi.InvalidHandle) {
                try {
                    for (bool flag = true; flag; flag = ShellApi.FindNextFile(firstFile, wiN32FindData)) {
                        if (!string.IsNullOrEmpty(wiN32FindData.cFileName) && wiN32FindData.cFileName != "." && wiN32FindData.cFileName != "..") {
                            if ((wiN32FindData.dwFileAttributes & ShellFileAttributes.Directory) == ShellFileAttributes.Directory)
                                list.Add((ShellItem)new ShellDirectory(Path.Combine(this.FullName, wiN32FindData.cFileName), wiN32FindData));
                            else
                                list.Add((ShellItem)new ShellFile(Path.Combine(this.FullName, wiN32FindData.cFileName), wiN32FindData));
                        }
                        wiN32FindData = new WIN32_FIND_DATA();
                    }
                }
                finally {
                    if (firstFile != ShellApi.InvalidHandle)
                        ShellApi.FindClose(firstFile);
                }
            }
            return new ReadOnlyCollection<ShellItem>((IList<ShellItem>)list);
        }

        public ulong GetSize() {
            this._cancelGetSize = false;
            this.Size = this.GetSizeRecursive(this.GetItems());
            return this.Size;
        }

        public override void Refresh() {
            if (this.Data == null)
                this.Data = new WIN32_FIND_DATA();
            ShellApi.FindClose(ShellApi.FindFirstFile(this.FullName, ((ShellItem)this).Data));
            this.Size = 0UL;
            this.IsLoaded = true;
        }

        private void GetSizeAsync() {
            ulong sizeRecursive = this.GetSizeRecursive(this.GetItems());
            if (this._cancelGetSize)
                return;
            this.Size = sizeRecursive;
            this.OnGetSizeEnd(sizeRecursive);
        }

        private ulong GetSizeRecursive(ReadOnlyCollection<ShellItem> shis) {
            ulong sizeRecursive = 0;
            foreach (ShellItem shi in shis) {
                if (this._cancelGetSize)
                    return 0;
                if (shi is ShellFile shellFile)
                    sizeRecursive += shellFile.Size;
                else if (shi is ShellDirectory shellDirectory)
                    sizeRecursive += this.GetSizeRecursive(shellDirectory.GetItems());
            }
            return sizeRecursive;
        }

        private void OnGetSizeEnd(ulong size) {
            lock (this.GetSizeEnd) {
                if (this.GetSizeEnd == null)
                    return;
                this.GetSizeEnd((object)this, new ShellDirectorySizeEndArgs(size));
            }
        }
    }
}
