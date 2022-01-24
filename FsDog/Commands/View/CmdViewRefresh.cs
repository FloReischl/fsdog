// Decompiled with JetBrains decompiler
// Type: FsDog.CmdViewRefresh
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System.IO;
using System.Windows.Forms;

namespace FsDog.Commands.View {
    public class CmdViewRefresh : CmdFsDogIntern {
        public override void Execute() {
            if (this.FormMain != null)
                this.FormMain.Cursor = Cursors.WaitCursor;
            DirectoryInfo directoryInfo1 = this.DetailView1 == null ? (DirectoryInfo)null : this.DetailView1.ParentDirectory;
            DirectoryInfo directoryInfo2 = this.DetailView2 == null ? (DirectoryInfo)null : this.DetailView2.ParentDirectory;
            FsApp.Instance.ClearImageCache();
            if (this.Tree != null)
                this.Tree.Refresh();
            if (this.DetailView1 != null && directoryInfo1.FullName.ToLower() == this.DetailView1.ParentDirectory.FullName.ToLower())
                this.DetailView1.Refresh();
            if (this.DetailView2 != null && directoryInfo2.FullName.ToLower() == this.DetailView2.ParentDirectory.FullName.ToLower())
                this.DetailView2.Refresh();
            if (this.FormMain == null)
                return;
            this.FormMain.Cursor = Cursors.Default;
        }
    }
}
