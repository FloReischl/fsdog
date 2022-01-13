// Decompiled with JetBrains decompiler
// Type: FsDog.NodeNetworkShare
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Windows.Forms;
using FsDog.Properties;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Tree {
    public class NodeNetworkShare : NodeDirectory {
        public NodeNetworkShare(string serverName, string shareName)
          : base(new DirectoryInfo(string.Format("\\\\{0}\\{1}", (object)serverName, (object)shareName)), true) {
            this.ServerName = serverName;
            this.ShareName = shareName;
            this.Text = this.ShareName;
            this.Image = (Image)Resources.NetworkShare;
            this.SelectedImage = (Image)Resources.NetworkShare;
        }

        public string ServerName { get; private set; }

        public string ShareName { get; private set; }

        protected override void OnLoadChildren(TreeViewCancelEventArgs e) {
            base.OnLoadChildren(e);
            this.TreeView.FindForm().Cursor = Cursors.WaitCursor;
            NodeNetworkServer parentNode = (NodeNetworkServer)this.ParentNode;
            foreach (DirectoryInfo directory in new DirectoryInfo(string.Format("\\\\{0}\\{1}", (object)this.ServerName, (object)this.ShareName)).GetDirectories())
                this.Nodes.Add((TreeNodeBase)new NodeDirectory(directory));
            this.TreeView.FindForm().Cursor = Cursors.Default;
        }
    }
}
