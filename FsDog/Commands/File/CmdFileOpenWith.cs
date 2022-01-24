// Decompiled with JetBrains decompiler
// Type: FsDog.CmdFileOpenWith
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Windows.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Commands.Files {
    public class CmdFileOpenWith : CmdFsDogIntern {
        public override void Execute() {
            if (this.SelectedItems?.Length != 0) {
                if (SelectedItems.Length > 1 && MessageBox.Show(this.Application.MainForm, $"Are you sure to call open with for all {SelectedItems.Length} selected items?", "Execute multiple", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;
                foreach (FileSystemInfo selectedItem in this.SelectedItems) {
                    try {
                        Process.Start(new ProcessStartInfo() {
                            FileName = "rundll32.exe",
                            Arguments = string.Format("shell32.dll,OpenAs_RunDLL \"{0}\"", (object)selectedItem.FullName)
                        });
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
