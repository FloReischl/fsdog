// Decompiled with JetBrains decompiler
// Type: FsDogBase.CmdFsDog
// Assembly: FsDogBase, Version=1.0.3256.36022, Culture=neutral, PublicKeyToken=null
// MVID: CE448AFC-051D-4FC8-AA6F-17F928525DD0
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDogBase.dll

using FR.Commands;
using System.Diagnostics;
using System.IO;

namespace FsDogBase {
    public abstract class CmdFsDog : StandardCommandBase {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FileSystemInfo[] _selectedItems;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DirectoryInfo _parentDirectory;

        public FileSystemInfo[] SelectedItems {
            [DebuggerNonUserCode]
            get => this._selectedItems;
            [DebuggerNonUserCode]
            set => this._selectedItems = value;
        }

        public DirectoryInfo ParentDirectory {
            [DebuggerNonUserCode]
            get => this._parentDirectory;
            [DebuggerNonUserCode]
            set => this._parentDirectory = value;
        }
    }
}
