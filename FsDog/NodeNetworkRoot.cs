// Decompiled with JetBrains decompiler
// Type: FsDog.NodeNetworkRoot
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Net;
using FR.Windows.Forms;
using FsDog.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace FsDog
{
  public class NodeNetworkRoot : NodeBase
  {
    private NETRESOURCE _root;

    public NodeNetworkRoot()
    {
      this.Text = "Network Places";
      this.SelectedImage = (Image) Resources.NetworkRoot;
      this.Image = (Image) Resources.NetworkRoot;
      this._root = NetworkHelper.GetNetworkRoot();
    }

    protected override void OnLoadChildren(TreeViewCancelEventArgs e)
    {
      base.OnLoadChildren(e);
      foreach (NETRESOURCE networkChild in NetworkHelper.GetNetworkChildren(this._root))
        this.Nodes.Add((TreeNodeBase) new NodeNetworkProvider(networkChild));
    }
  }
}
