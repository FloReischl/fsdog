// Decompiled with JetBrains decompiler
// Type: FsDog.CmdHelpAbout
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System.Windows.Forms;

namespace FsDog.Commands.Help {
    public class CmdHelpAbout : CmdFsDogIntern {
        public override void Execute() {
            int num = (int)new FormAbout().ShowDialog((IWin32Window)this.Application.MainForm);
        }
    }
}
