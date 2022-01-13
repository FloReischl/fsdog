// Decompiled with JetBrains decompiler
// Type: FsDog.NodeNetworkServer
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Net;
using FR.Windows.Forms;
using FsDog.Properties;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Tree {
    public class NodeNetworkServer : NodeBase {
        public NodeNetworkServer(NETRESOURCE server) {
            this.Server = server;
            this.Text = this.ServerName;
            this.SelectedImage = (Image)Resources.NetworkServer;
            this.Image = (Image)Resources.NetworkServer;
        }

        public NETRESOURCE Server { get; private set; }

        public string ServerName => this.Server.lpRemoteName.Trim('\\');

        public NodeNetworkShare GetShare(StringComparer sc, string shareName) {
            int index = 0;
            foreach (NodeNetworkShare node in this.Nodes) {
                if (sc.Compare(node.ShareName, shareName) == 0)
                    return node;
                if (sc.Compare(node.ShareName, shareName) < 0)
                    index = node.Index + 1;
            }
            try {
                NetworkHelper.ConnectNetworkResource(this.TreeView.FindForm().Handle, string.Format("\\\\{0}\\{1}", (object)this.ServerName, (object)shareName), (string)null, (string)null, NETRESOURCE_CONNECT.CONNECT_INTERACTIVE);
                foreach (string shareName1 in NetworkHelper.GetShareNames(this.ServerName)) {
                    if (sc.Compare(shareName1, shareName) == 0) {
                        NodeNetworkShare node = new NodeNetworkShare(this.ServerName, shareName);
                        this.Nodes.Insert(index, (TreeNodeBase)node);
                        return node;
                    }
                }
            }
            catch (Exception ex) {
                FormError.ShowException(ex, (IWin32Window)null);
            }
            return (NodeNetworkShare)null;
        }

        protected override void OnLoadChildren(TreeViewCancelEventArgs e) {
            base.OnLoadChildren(e);
            this.TreeView.FindForm().Cursor = Cursors.WaitCursor;
            try {
                NetworkHelper.ConnectNetworkResource(this.TreeView.FindForm().Handle, string.Format("\\\\{0}", (object)this.ServerName), (string)null, (string)null, NETRESOURCE_CONNECT.CONNECT_INTERACTIVE);
                foreach (string shareName in NetworkHelper.GetShareNames(this.ServerName)) {
                    try {
                        SHARE_INFO_1 shi;
                        NetworkHelper.GetShareInformation(this.ServerName, shareName, out shi);
                        if ((shi.shi1_type & NetworkShareType.STYPE_IPC) != NetworkShareType.STYPE_IPC) {
                            if ((shi.shi1_type & NetworkShareType.STYPE_PRINTQ) != NetworkShareType.STYPE_PRINTQ) {
                                if ((shi.shi1_type & NetworkShareType.STYPE_SPECIAL) != NetworkShareType.STYPE_SPECIAL) {
                                    if ((shi.shi1_type & NetworkShareType.STYPE_TEMPORARY) != NetworkShareType.STYPE_TEMPORARY) {
                                        if (Directory.Exists(string.Format("\\\\{0}\\{1}", (object)this.ServerName, (object)shareName)))
                                            this.Nodes.Add((TreeNodeBase)new NodeNetworkShare(this.ServerName, shareName));
                                    }
                                }
                            }
                        }
                    }
                    catch (NetworkException ex) {
                        if (ex.NetworkErrorCode != NetworkErrorCode.ERROR_ACCESS_DENIED)
                            throw;
                    }
                    catch {
                        throw;
                    }
                }
            }
            catch (Exception ex) {
                FormError.ShowException(ex, (IWin32Window)null);
            }
            finally {
                if (!this.HasDummy())
                    this.Nodes.Sort((IComparer)new TreeNodeTextComparer());
                this.TreeView.FindForm().Cursor = Cursors.Default;
            }
        }
    }
}
