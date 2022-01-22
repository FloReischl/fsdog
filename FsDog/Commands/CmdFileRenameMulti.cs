// Decompiled with JetBrains decompiler
// Type: FsDog.CmdFileRenameMulti
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using FR.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Commands {
    public class CmdFileRenameMulti : CmdFsDogIntern {
        public override void Execute() {
            try {
                if (new FormRename() {
                    ParentDirectory = this.ParentDirectory,
                    FileItems = ((IList<FileSystemInfo>)this.SelectedItems)
                }.ShowDialog((IWin32Window)this.Application.MainForm) == DialogResult.OK) {
                    this.CurrentDetailView.Refresh();
                }
            }
            catch (Exception ex) {
                FormError.ShowException(ex, (IWin32Window)null);
            }
        }
    }
}
