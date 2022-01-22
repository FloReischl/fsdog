// Decompiled with JetBrains decompiler
// Type: FR.Commands.StandardCommandBase
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Logging;
using System;
using System.Diagnostics;

namespace FR.Commands {
    public abstract class StandardCommandBase : CommandBase {
        public ICommand AlternateCommand { get; set; }

        public Action<ICommand> AlternateExecute { get; set; }

        protected virtual bool ForceAlternateExecution { get; set; }

        public override void Execute() {
            if (this.HandleAlternate()) {
                return;
            }

            if (this.ForceAlternateExecution) {
                throw new NotImplementedException("No alternate command was defined.");
            }
            else {
                throw new NotImplementedException("Execute was not overridden");
            }
        }

        protected bool HandleAlternate() {
            var altCmd = AlternateCommand;
            if (altCmd != null) {
                Receiver.InitializeCommand(altCmd);
                altCmd.Execute();
                Receiver.FinishCommand(altCmd);
                return true;
            }
            if (this.AlternateExecute == null) {
                return false;
            }
            this.Log.Info("Executing alternate command");
            this.AlternateExecute((ICommand)this);
            return true;
        }
    }
}
