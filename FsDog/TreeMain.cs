// Decompiled with JetBrains decompiler
// Type: FsDog.TreeMain
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.IO;
using FR.Windows.Forms;
using FsDog.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FsDog {
    public class TreeMain : TreeViewBase {
        public override void Refresh() {
            base.Refresh();
            foreach (TreeNode node in this.Nodes) {
                if (node is NodeBase nodeBase && !nodeBase.HasDummy())
                    nodeBase.Refresh();
            }
        }

        protected override void InitLayout() {
            base.InitLayout();
            if (this.DesignMode)
                return;
            FsApp instance = FsApp.Instance;
            this.ImageList = new ImageList();
            this.ImageList.ColorDepth = ColorDepth.Depth32Bit;
            this.ImageList.Images.Add("DirectoryClosed", (Image)Resources.DirectoryClosed);
            this.ImageList.Images.Add("DirectoryOpen", (Image)Resources.DirectoryOpen);
            if (instance.Options.Navigation.ShowUserFolder) {
                string path = Environment.GetEnvironmentVariable("USERPROFILE");
                if (string.IsNullOrEmpty(path)) {
                    string environmentVariable1 = Environment.GetEnvironmentVariable("HOMEDRIVE");
                    string environmentVariable2 = Environment.GetEnvironmentVariable("HOMEPATH");
                    if (!string.IsNullOrEmpty(environmentVariable1) && !string.IsNullOrEmpty(environmentVariable2))
                        path = string.Format("{0}{1}", (object)environmentVariable1, (object)environmentVariable2);
                }
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                    this.Nodes.Add((TreeNodeBase)new NodeDirectory(new DirectoryInfo(path)));
            }
            if (instance.Options.Navigation.ShowMyDocumentsFolder)
                this.Nodes.Add((TreeNodeBase)new NodeDirectory(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal))));
            foreach (DriveInfo drive in DriveInfo.GetDrives())
                this.Nodes.Add((TreeNodeBase)new NodeDrive(drive));
            this.Nodes.Add((TreeNodeBase)new NodeNetworkRoot());
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.KeyCode == Keys.Back && e.Control) {
                TreeNodeBase treeNodeBase = this.SelectedNode;
                while (treeNodeBase != null && treeNodeBase.ParentNode != null)
                    treeNodeBase = treeNodeBase.ParentNode;
                if (treeNodeBase != null)
                    this.SelectedNode = treeNodeBase;
                e.SuppressKeyPress = true;
            }
            base.OnKeyDown(e);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            TreeViewHitTestInfo treeViewHitTestInfo = this.HitTest(e.X, e.Y);
            if (treeViewHitTestInfo.Node != null && e.Button == MouseButtons.Right && treeViewHitTestInfo.Node is NodeDirectory node) {
                ShellContextMenu shellContextMenu = new ShellContextMenu();
                Point screen = this.PointToScreen(new Point(e.X, e.Y));
                Color backColor = node.BackColor;
                Color foreColor = node.ForeColor;
                node.BackColor = SystemColors.Highlight;
                node.ForeColor = SystemColors.HighlightText;
                shellContextMenu.ShowContextMenu(new DirectoryInfo[1]
                {
          node.Directory
                }, screen);
                node.BackColor = backColor;
                node.ForeColor = foreColor;
            }
            base.OnMouseDown(e);
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == 769) {
                if (this.SelectedNode is NodeDirectory selectedNode1)
                    FileHelper.CopyToClipboard(new FileSystemInfo[1]
                    {
            (FileSystemInfo) selectedNode1.Directory
                    }, DragDropEffects.Copy | DragDropEffects.Link);
            }
            else if (m.Msg == 768) {
                if (this.SelectedNode is NodeDirectory selectedNode2)
                    FileHelper.CopyToClipboard(new FileSystemInfo[1]
                    {
            (FileSystemInfo) selectedNode2.Directory
                    }, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);
            }
            else if (m.Msg == 770) {
                string destination = (string)null;
                if (this.SelectedNode is NodeDirectory selectedNode3)
                    destination = selectedNode3.Directory.FullName;
                if (this.SelectedNode is NodeDrive selectedNode4)
                    destination = selectedNode4.Drive.RootDirectory.FullName;
                List<ShellItem> items = new List<ShellItem>();
                DragDropEffects filesFromClipboard = FileHelper.GetFilesFromClipboard(ref items);
                if (filesFromClipboard != DragDropEffects.None && items.Count != 0) {
                    if ((filesFromClipboard & DragDropEffects.Move) == DragDropEffects.Move)
                        FileHelper.MoveTo(this.FindForm().Handle, (IEnumerable<ShellItem>)items, destination, false);
                    else
                        FileHelper.CopyTo(this.FindForm().Handle, (IEnumerable<ShellItem>)items, destination, false);
                }
            }
            base.WndProc(ref m);
        }
    }
}
