// Decompiled with JetBrains decompiler
// Type: FsDog.CmdFileRunAs
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using FR.IO;
using FR.Windows.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Commands.Files {
    public class CmdFileRunAs : CmdFsDogIntern {
        public override void Execute() {
            if (this.SelectedItems.Length != 0) {
                if (this.SelectedItems.Length > 1 && MessageBox.Show((IWin32Window)this.Application.MainForm, string.Format("Are you sure to run all {0} selected items as another user?", (object)this.SelectedItems.Length), "Execute multiple", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;
                foreach (FileSystemInfo selectedItem in this.SelectedItems) {
                    try {
                        FileHelper.ShellExecute(selectedItem.FullName, (string)null, (string)null, (string)null);
                    }
                    catch (Exception ex) {
                        FormError.ShowException(ex, (IWin32Window)this.Application.MainForm);
                    }
                }
                base.Execute();
            }
        }
    }
}
