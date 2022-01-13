// Decompiled with JetBrains decompiler
// Type: FsDog.NodeNetworkDomain
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Net;
using FR.Windows.Forms;
using FsDog.Properties;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FsDog
{
  public class NodeNetworkDomain : NodeBase
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private NETRESOURCE _domain;

    public NodeNetworkDomain(NETRESOURCE domain)
    {
      this._domain = domain;
      this.Text = domain.lpRemoteName;
      this.SelectedImage = (Image) Resources.NetworkDomain;
      this.Image = (Image) Resources.NetworkDomain;
    }

    public NETRESOURCE Domain
    {
      [DebuggerNonUserCode] get => this._domain;
    }

    protected override void OnLoadChildren(TreeViewCancelEventArgs e)
    {
      base.OnLoadChildren(e);
      this.TreeView.FindForm().Cursor = Cursors.WaitCursor;
      foreach (NETRESOURCE networkChild in NetworkHelper.GetNetworkChildren(this._domain))
        this.Nodes.Add((TreeNodeBase) new NodeNetworkServer(networkChild));
      this.TreeView.FindForm().Cursor = Cursors.Default;
    }
  }
}
