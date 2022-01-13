// Decompiled with JetBrains decompiler
// Type: FR.IO.ShellItem
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;

namespace FR.IO {
    public abstract class ShellItem {
        internal ShellItem(string fullName) => this.FullName = fullName;

        internal ShellItem(string fullName, WIN32_FIND_DATA data) {
            this.FullName = fullName;
            this.Data = data;
            this.IsLoaded = true;
        }

        public abstract string Name { get; }

        public abstract void Refresh();

        public string FullName { get; internal set; }

        public virtual bool Exists {
            get {
                IntPtr firstFile = ShellApi.FindFirstFile(this.FullName, new WIN32_FIND_DATA());
                if (firstFile == ShellApi.InvalidHandle)
                    return false;
                ShellApi.FindClose(firstFile);
                return true;
            }
        }

        public DateTime CreationTime {
            get {
                if (!this.IsLoaded)
                    this.Refresh();
                return DateTime.FromFileTime((long)ShellApi.IntToLong(this.Data.ftCreationTime.dwLowDateTime, this.Data.ftCreationTime.dwHighDateTime));
            }
        }

        public DateTime LastAccessTime {
            get {
                if (!this.IsLoaded)
                    this.Refresh();
                return DateTime.FromFileTime((long)ShellApi.IntToLong(this.Data.ftLastAccessTime.dwLowDateTime, this.Data.ftLastAccessTime.dwHighDateTime));
            }
        }

        public DateTime LastWriteTime {
            get {
                if (!this.IsLoaded)
                    this.Refresh();
                return DateTime.FromFileTime((long)ShellApi.IntToLong(this.Data.ftLastWriteTime.dwLowDateTime, this.Data.ftLastWriteTime.dwHighDateTime));
            }
        }

        internal bool IsLoaded { get; set; }

        internal WIN32_FIND_DATA Data { get; set; }

        public void CopyTo(string destination, bool overwrite) {
            if (!ShellApi.CopyFile(this.FullName, destination, !overwrite))
                throw new Win32Exception();
        }

        public void MoveTo(string destination) {
            if (!ShellApi.MoveFile(this.FullName, destination))
                throw new Win32Exception();
        }

        public override string ToString() => this.FullName;

        internal DateTime FileTimeToDateTime(FILETIME fileTime) {
            byte[] bytes = BitConverter.GetBytes(fileTime.dwHighDateTime);
            Array.Resize<byte>(ref bytes, 8);
            return DateTime.FromFileTime((long)(BitConverter.ToUInt64(bytes, 0) << 32 | (ulong)(uint)fileTime.dwLowDateTime));
        }
    }
}
