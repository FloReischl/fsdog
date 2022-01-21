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
        public FileSystemInfo[] SelectedItems { [DebuggerNonUserCode]
            get; [DebuggerNonUserCode]
            set; }

        public DirectoryInfo ParentDirectory { [DebuggerNonUserCode]
            get; [DebuggerNonUserCode]
            set; }
    }
}
