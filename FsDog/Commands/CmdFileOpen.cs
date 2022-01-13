// Decompiled with JetBrains decompiler
// Type: FsDog.CmdFileOpen
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using FR.IO;
using FR.Windows.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Commands {
    public class CmdFileOpen : CmdFsDogIntern {
        public override void Execute() {
            if (this.SelectedItems == null || this.SelectedItems.Length == 0) {
                this.ExecutionState = CommandExecutionState.Canceled;
            }
            else {
                if (this.SelectedItems.Length == 1) {
                    if (this.SelectedItems[0] is DirectoryInfo selectedItem1) {
                        this.CurrentDetailView.OnRequestParentDirectory(selectedItem1);
                        this.ExecutionState = CommandExecutionState.Ok;
                        return;
                    }
                    if (this.SelectedItems[0] is FileInfo selectedItem2 && string.Compare(selectedItem2.Extension, ".lnk", true) == 0) {
                        if (this.Application.Options.Navigation.ResolveDirectoryLinks) {
                            try {
                                DirectoryInfo dir = new DirectoryInfo(new ShellShortcut(selectedItem2.FullName).Path);
                                if (dir.Exists) {
                                    this.CurrentDetailView.OnRequestParentDirectory(dir);
                                    this.ExecutionState = CommandExecutionState.Ok;
                                    return;
                                }
                            }
                            catch (Exception ex) {
                                FormError.ShowException(ex, (IWin32Window)this.Application.MainForm);
                                this.ExecutionState = CommandExecutionState.Error;
                                return;
                            }
                        }
                    }
                }
                else if (this.SelectedItems.Length > 1) {
                    if (MessageBox.Show((IWin32Window)this.Application.MainForm, string.Format("Are you sure to open/execute all {0} selected items?", (object)this.SelectedItems.Length), "Execute multiple", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        return;
                    this.ExecutionState = CommandExecutionState.Canceled;
                    return;
                }
                foreach (FileSystemInfo selectedItem in this.SelectedItems) {
                    try {
                        FileHelper.ShellExecute(selectedItem.FullName);
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
