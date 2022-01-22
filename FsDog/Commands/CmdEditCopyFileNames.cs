// Decompiled with JetBrains decompiler
// Type: FsDog.CmdEditCopyFileNames
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Commands {
    public class CmdEditCopyFileNames : CmdFsDogIntern {
        public CmdEditCopyFileNames.CopyNameType CopyType {
            [DebuggerNonUserCode]
            get => (CmdEditCopyFileNames.CopyNameType)this.Context[(object)nameof(CopyType)];
        }

        public override void Execute() {
            if (this.CopyType == CmdEditCopyFileNames.CopyNameType.ParentDirectory) {
                Clipboard.Clear();
                Clipboard.SetText(this.CurrentDetailView.ParentDirectory.FullName);
            }
            else {
                List<FileSystemInfo> selectedSystemInfos = this.CurrentDetailView.GetSelectedSystemInfos();
                StringWriter stringWriter = new StringWriter();
                bool flag = true;
                foreach (FileSystemInfo fileSystemInfo in selectedSystemInfos) {
                    if (flag)
                        flag = false;
                    else
                        stringWriter.WriteLine();
                    if (this.CopyType == CmdEditCopyFileNames.CopyNameType.FullName)
                        stringWriter.Write(fileSystemInfo.FullName);
                    else if (this.CopyType == CmdEditCopyFileNames.CopyNameType.Name)
                        stringWriter.Write(fileSystemInfo.Name);
                }
                Clipboard.Clear();
                Clipboard.SetText(stringWriter.GetStringBuilder().ToString());
            }
        }

        public enum CopyNameType {
            Name,
            FullName,
            ParentDirectory,
        }
    }
}
