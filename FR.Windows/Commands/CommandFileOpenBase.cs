// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.Commands.CommandFileOpenBase
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Commands;
using FR.Logging;
using System;
using System.Windows.Forms;

namespace FR.Windows.Forms.Commands {
    public class CommandFileOpenBase : StandardCommandBase {
        public string FileName {
            get => this.Context.TryGetValue<string>(nameof(FileName));
            set => this.Context.Add(nameof(FileName), value);
        }

        public override void Execute() {
            Log.CallEntry(LogLevel.Info);
            try {
                if (this.AlternateExecute != null) {
                    Log.Info("Executing alternate command");
                    this.AlternateExecute((ICommand)this);
                }
                else {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (!string.IsNullOrEmpty(this.FileName))
                        openFileDialog.FileName = this.FileName;
                    Form owner = (Form)null;
                    if (this.Application != null)
                        owner = ((WindowsApplication)this.Application).MainForm;
                    if (owner != null) {
                        if (openFileDialog.ShowDialog((IWin32Window)owner) == DialogResult.OK) {
                            this.FileName = openFileDialog.FileName;
                        }
                    }
                    else if (openFileDialog.ShowDialog() == DialogResult.OK) {
                        this.FileName = openFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex) {
                this.Log.Ex(ex);
                FormError.ShowException(ex);
            }
            finally {
                Log.CallLeave(LogLevel.Info);
            }
        }
    }
}
