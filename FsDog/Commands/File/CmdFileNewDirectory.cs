// Decompiled with JetBrains decompiler
// Type: FsDog.CmdFileNewDirectory
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System.IO;

namespace FsDog.Commands.Files {
    public class CmdFileNewDirectory : CmdFsDogIntern {
        public override void Execute() {
            DirectoryInfo parentDirectory = this.CurrentDetailView.ParentDirectory;
            string str = Path.Combine(parentDirectory.FullName, "New Directory");
            int num = 1;
            while (File.Exists(str) || Directory.Exists(str))
                str = Path.Combine(parentDirectory.FullName, string.Format("New Directory ({0})", (object)num++));
            Directory.CreateDirectory(str);
            this.CurrentDetailView.BeginEditItem(str);
        }
    }
}
