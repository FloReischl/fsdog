﻿// Decompiled with JetBrains decompiler
// Type: FsDog.CmdFilePowerShell
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using System.Diagnostics;

namespace FsDog.Commands {
    public class CmdFilePowerShell : CmdFsDogIntern {
        public override void Execute() {
            if (this.CurrentDetailView != null && this.CurrentDetailView.ParentDirectory != null) {
                Process.Start(new ProcessStartInfo(this.Context.GetAsString((object)"PowerShell")) {
                    WorkingDirectory = this.CurrentDetailView.ParentDirectory.FullName
                });
                this.ExecutionState = CommandExecutionState.Ok;
            }
            else
                this.ExecutionState = CommandExecutionState.Canceled;
        }
    }
}
