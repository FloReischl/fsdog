// Decompiled with JetBrains decompiler
// Type: FsDog.CmdFileNewFile
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using System.IO;

namespace FsDog.Commands {
    public class CmdFileNewFile : CmdFsDogIntern {
        public override void Execute() {
            DirectoryInfo parentDirectory = this.CurrentDetailView.ParentDirectory;
            string str = Path.Combine(parentDirectory.FullName, "New File.txt");
            int num = 1;
            while (File.Exists(str) || Directory.Exists(str))
                str = Path.Combine(parentDirectory.FullName, string.Format("New File ({0}).txt", (object)num++));
            File.Create(str).Close();
            this.CurrentDetailView.BeginEditItem(str);
            this.ExecutionState = CommandExecutionState.Ok;
        }
    }
}
