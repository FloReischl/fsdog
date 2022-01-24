// Decompiled with JetBrains decompiler
// Type: FsDog.CmdEditCopyToOtherView
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using FR.IO;
using System.Collections.Generic;
using System.IO;

namespace FsDog.Commands.Edit {
    public class CmdEditCopyToOtherView : CmdFsDogIntern {
        public override void Execute() {
            DirectoryInfo directoryInfo = this.CurrentDetailView != this.DetailView1 ? this.DetailView1.ParentDirectory : this.DetailView2.ParentDirectory;
            List<FileSystemInfo> selectedSystemInfos = this.CurrentDetailView.GetSelectedSystemInfos();
            List<string> stringList = new List<string>(selectedSystemInfos.Count);
            foreach (FileSystemInfo fileSystemInfo in selectedSystemInfos)
                stringList.Add(fileSystemInfo.FullName);
            FileHelper.CopyTo(this.Application.MainForm.Handle, stringList.ToArray(), directoryInfo.FullName, false);
        }
    }
}
