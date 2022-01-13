﻿// Decompiled with JetBrains decompiler
// Type: FsDog.CmdCommandsEdit
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using FsDog.Dialogs;
using System.Windows.Forms;

namespace FsDog.Commands {
    public class CmdCommandsEdit : CmdFsDogIntern {
        public override void Execute() {
            if (new FormCommands().ShowDialog((IWin32Window)this.Application.MainForm) == DialogResult.OK)
                this.ExecutionState = CommandExecutionState.Ok;
            else
                this.ExecutionState = CommandExecutionState.Canceled;
        }
    }
}
