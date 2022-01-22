// Decompiled with JetBrains decompiler
// Type: FsDog.CmdApplicationExecute
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using System;
using System.Diagnostics;

namespace FsDog.Commands {
    internal class CmdApplicationExecute : CmdCommandBase {
        public override void Execute() {
            try {
                CommandInfo info = (CommandInfo)this.Context[(object)"CommandInfo"];
                if (this.EnsureRequirements(info)) {
                    string arguments = this.GetArguments(info);
                    new Process() {
                        StartInfo = {
                            FileName = info.Command,
                            Arguments = arguments
                        }
                    }.Start();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
