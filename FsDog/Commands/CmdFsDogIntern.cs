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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Control _activeFileControl;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DetailView _detailView1;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DetailView _detailView2;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DetailView _currentDetailView;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FormMain _formMain;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TreeMain _tree;

        public Control ActiveFileControl {
            [DebuggerNonUserCode]
            get => this._activeFileControl;
            [DebuggerNonUserCode]
            set => this._activeFileControl = value;
        }

        public FsApp Application => (FsApp)this.ApplicationInstance;

        public DetailView DetailView1 {
            [DebuggerNonUserCode]
            get => this._detailView1;
            [DebuggerNonUserCode]
            set => this._detailView1 = value;
        }

        public DetailView DetailView2 {
            [DebuggerNonUserCode]
            get => this._detailView2;
            [DebuggerNonUserCode]
            set => this._detailView2 = value;
        }

        public DetailView CurrentDetailView {
            [DebuggerNonUserCode]
            get => this._currentDetailView;
            [DebuggerNonUserCode]
            set => this._currentDetailView = value;
        }

        public DetailView OtherDetailView => this.CurrentDetailView == this.DetailView1 ? this.DetailView2 : this.DetailView1;

        public FormMain FormMain {
            [DebuggerNonUserCode]
            get => this._formMain;
            [DebuggerNonUserCode]
            set => this._formMain = value;
        }

        public TreeMain Tree {
            [DebuggerNonUserCode]
            get => this._tree;
            [DebuggerNonUserCode]
            set => this._tree = value;
        }
    }
}
