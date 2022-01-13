// Decompiled with JetBrains decompiler
// Type: FsDog.CmdViewPreview
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FsDog.Detail;

namespace FsDog.Commands {
    public class CmdViewPreview : CmdFsDogIntern {
        public override void Execute() {
            FsApp instance = FsApp.Instance;
            if (instance.MainForm.CurrentPreview != null) {
                instance.MainForm.SetPreview((PreviewContainer)null);
            }
            else {
                PreviewContainer pc = new PreviewContainer();
                instance.MainForm.SetPreview(pc);
            }
        }
    }
}
