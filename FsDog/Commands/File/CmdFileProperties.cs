// Decompiled with JetBrains decompiler
// Type: FsDog.CmdFileProperties
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using FR.IO;

namespace FsDog.Commands.Files {
    public class CmdFileProperties : CmdFsDogIntern {
        public override void Execute() {
            if (this.SelectedItems?.Length != 0) {
                FileHelper.ShowPropertiesDialog(this.SelectedItems[0].FullName);
            }
        }
    }
}
