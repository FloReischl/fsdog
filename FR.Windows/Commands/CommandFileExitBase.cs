// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.Commands.CommandFileExitBase
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Commands;
using FR.Logging;
using System;

namespace FR.Windows.Forms.Commands {
    public class CommandFileExitBase : StandardCommandBase {
        public override void Execute() {
            if (this.HandleAlternate()) {
                return;
            }

            WindowsApplication.Exit();
        }
    }
}
