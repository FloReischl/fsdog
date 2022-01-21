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
        private ICommand _command;
        private CommandExecuteHandler _alternateExecute;
        private bool _forceAlternateExecution;

        public ICommand AlternateCommand {
            [DebuggerNonUserCode]
            get => this._command;
            [DebuggerNonUserCode]
            set => this._command = value;
        }

        public CommandExecuteHandler AlternateExecute {
            [DebuggerNonUserCode]
            get => this._alternateExecute;
            [DebuggerNonUserCode]
            set => this._alternateExecute = value;
        }

        protected virtual bool ForceAlternateExecution {
            get => this._forceAlternateExecution;
            set => this._forceAlternateExecution = value;
        }

        public override void Execute() {
            try {
                if (this.HandleAlternate())
                    return;
                if (this.ForceAlternateExecution) {
                    this.Log.Error("No alternate command was defined");
                    this.ExecutionState = CommandExecutionState.Error;
                }
                else {
                    this.Log.Error("Execute was not overridden");
                    this.ExecutionState = CommandExecutionState.Error;
                }
            }
            catch (Exception ex) {
                this.Log.Ex(ex);
                this.ExecutionState = CommandExecutionState.Error;
            }
        }

        protected bool HandleAlternate() {
            if (this.AlternateCommand != null) {
                if (this.AlternateCommand.InstanceState != CommandInstanceState.Executing) {
                    this.Receiver.InitializeCommand(this.AlternateCommand);
                }
                this.AlternateCommand.Execute();
                this.ExecutionState = this.AlternateCommand.ExecutionState;
                if (this.AlternateCommand.InstanceState != CommandInstanceState.Finished)
                    this.Receiver.FinishCommand(this.AlternateCommand);
                return true;
            }
            if (this.AlternateExecute == null) {
                return false;
            }
            this.Log.Info("Executing alternate command");
            this.ExecutionState = this.AlternateExecute((ICommand)this);
            return true;
        }
    }
}
