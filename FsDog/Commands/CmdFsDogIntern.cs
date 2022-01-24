// Decompiled with JetBrains decompiler
// Type: FsDog.CmdFsDogIntern
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FsDog.Detail;
using FsDog.Dialogs;
using FsDog.Tree;
using FsDogBase;
using System.Diagnostics;
using System.Windows.Forms;

namespace FsDog.Commands {
    public class CmdFsDogIntern : CmdFsDog {
        public Control ActiveFileControl { get; set; }

        public new FsApp Application => (FsApp)base.Application;

        public DetailView DetailView1 { get; set; }

        public DetailView DetailView2 { get; set; }

        public DetailView CurrentDetailView { get; set; }

        public DetailView OtherDetailView => this.CurrentDetailView == this.DetailView1 ? this.DetailView2 : this.DetailView1;

        public FormMain FormMain { get; set; }

        public TreeMain Tree { get; set; }
    }
}
