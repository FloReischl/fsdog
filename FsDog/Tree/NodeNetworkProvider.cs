// Decompiled with JetBrains decompiler
// Type: FsDog.NodeNetworkProvider
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Net;
using FR.Windows.Forms;
using FsDog.Properties;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FsDog.Tree {
    public class NodeNetworkProvider : NodeBase {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private NETRESOURCE _provider;

        public NodeNetworkProvider(NETRESOURCE provider) {
            this._provider = provider;
            this.Text = provider.lpProvider;
            this.SelectedImage = (Image)Resources.NetworkProvider;
            this.Image = (Image)Resources.NetworkProvider;
        }

        public NETRESOURCE Provider {
            [DebuggerNonUserCode]
            get => this._provider;
        }

        protected override void OnLoadChildren(TreeViewCancelEventArgs e) {
            base.OnLoadChildren(e);
            this.TreeView.FindForm().Cursor = Cursors.WaitCursor;
            foreach (NETRESOURCE networkChild in NetworkHelper.GetNetworkChildren(this._provider))
                this.Nodes.Add((TreeNodeBase)new NodeNetworkDomain(networkChild));
            this.TreeView.FindForm().Cursor = Cursors.Default;
        }
    }
}
