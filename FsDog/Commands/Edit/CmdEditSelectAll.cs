// Decompiled with JetBrains decompiler
// Type: FsDog.CmdEditSelectAll
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using FsDog.Detail;

namespace FsDog.Commands.Edit {
    public class CmdEditSelectAll : CmdFsDogIntern {
        public override void Execute() {
            base.Execute();
            if (this.ActiveFileControl is DetailView) {
                this.CurrentDetailView.SelectAll();
            }
        }
    }
}
