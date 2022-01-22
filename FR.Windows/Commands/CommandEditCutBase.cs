// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.Commands.CommandEditCutBase
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Commands;
using FR.Logging;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FR.Windows.Forms.Commands {
    public class CommandEditCutBase : StandardCommandBase {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Control _control;

        public Control Control {
            [DebuggerNonUserCode]
            get => this._control;
            [DebuggerNonUserCode]
            set => this._control = value;
        }

        public override void Execute() {
            Log.CallEntry(LogLevel.Info);
            try {
                if (this.HandleAlternate())
                    return;
                if (this.Control == null)
                    this.Control = FormBase.GetActiveChildControl();
                if (this.Control != null) {
                    SendMessageHelper.SendCut(this.Control);
                }
            }
            catch (Exception ex) {
                Log.Ex(ex);
                FormError.ShowException(ex);
            }
            finally {
                Log.CallLeave(FR.Logging.LogLevel.Info);
            }
        }
    }
}
