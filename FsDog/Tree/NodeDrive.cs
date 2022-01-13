// Decompiled with JetBrains decompiler
// Type: FsDog.NodeDrive
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Drawing;
using FR.IO;
using FR.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Tree {
    public class NodeDrive : NodeBase {
        private FileSystemWatcher _fsw;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DriveInfo _drive;

        public NodeDrive(DriveInfo drive) {
            this._drive = drive;
            this.Image = ImageHelper.ExtractAssociatedImage(drive.Name, true);
            this.SelectedImage = this.Image;
            this.Refresh();
        }

        public DriveInfo Drive {
            [DebuggerNonUserCode]
            get => this._drive;
        }

        public override void Refresh() {
            base.Refresh();
            this.RefreshDrive();
            if (this.HasDummy())
                return;
            FsApp instance = FsApp.Instance;
            Dictionary<string, NodeDirectory> dictionary = new Dictionary<string, NodeDirectory>(this.Nodes.Count);
            foreach (NodeDirectory node in this.Nodes)
                dictionary.Add(node.Directory.Name, node);
            foreach (DirectoryInfo subDirectory in instance.GetSubDirectories(this.Drive.RootDirectory)) {
                NodeDirectory nodeDirectory;
                if (dictionary.TryGetValue(subDirectory.Name, out nodeDirectory)) {
                    dictionary.Remove(subDirectory.Name);
                    nodeDirectory.Refresh();
                }
                else
                    this.Nodes.Add((TreeNodeBase)new NodeDirectory(subDirectory));
            }
            foreach (TreeNode treeNode in dictionary.Values)
                treeNode.Remove();
        }

        public void RefreshDrive() => this.Text = FileHelper.GetDisplayName(this.Drive.Name);

        protected override void OnAfterCollapse(TreeViewEventArgs e) {
            this.RemoveWatcher();
            if (!this.HasDummy()) {
                foreach (TreeNode node in this.Nodes) {
                    if (node is NodeBase nodeBase && nodeBase.IsExpanded)
                        nodeBase.Collapse();
                }
                this.CreateDummy();
            }
            base.OnAfterCollapse(e);
        }

        protected override void OnLoadChildren(TreeViewCancelEventArgs e) {
            base.OnLoadChildren(e);
            foreach (DirectoryInfo subDirectory in FsApp.Instance.GetSubDirectories(this.Drive.RootDirectory))
                this.Nodes.Add((TreeNodeBase)new NodeDirectory(subDirectory));
            this.CreateWatcher();
        }

        private void CreateWatcher() {
            this.RemoveWatcher();
            this._fsw = new FileSystemWatcher();
            this._fsw.IncludeSubdirectories = false;
            this._fsw.Path = this.Drive.RootDirectory.FullName;
            this._fsw.Changed += new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            this._fsw.Created += new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            this._fsw.Deleted += new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            this._fsw.Renamed += new RenamedEventHandler(this.FileSystemWatcher_Renamed);
            this._fsw.EnableRaisingEvents = true;
        }

        private void RemoveWatcher() {
            if (this._fsw == null)
                return;
            this._fsw.Changed -= new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            this._fsw.Created -= new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            this._fsw.Deleted -= new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            this._fsw.Renamed -= new RenamedEventHandler(this.FileSystemWatcher_Renamed);
            this._fsw.Dispose();
            this._fsw = (FileSystemWatcher)null;
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e) {
            if (this.TreeView.InvokeRequired)
                this.TreeView.Invoke((Delegate)new FileSystemEventHandler(this.FileSystemWatcher_Changed), sender, (object)e);
            else if (e.ChangeType == WatcherChangeTypes.All) {
                if (this.IsExpanded)
                    this.Refresh();
                else
                    this.CreateDummy();
            }
            else {
                if (e.ChangeType == WatcherChangeTypes.Changed)
                    return;
                if (e.ChangeType == WatcherChangeTypes.Created) {
                    if (!Directory.Exists(e.FullPath))
                        return;
                    this.Nodes.Add((TreeNodeBase)new NodeDirectory(new DirectoryInfo(e.FullPath)));
                }
                else if (e.ChangeType == WatcherChangeTypes.Deleted) {
                    if (!(this.Nodes[e.Name] is NodeBase node))
                        return;
                    this.Nodes.Remove((TreeNodeBase)node);
                }
                else {
                    int changeType = (int)e.ChangeType;
                }
            }
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e) {
            if (this.TreeView.InvokeRequired) {
                this.TreeView.Invoke((Delegate)new RenamedEventHandler(this.FileSystemWatcher_Renamed), sender, (object)e);
            }
            else {
                if (!(this.Nodes[e.OldName] is NodeDirectory node))
                    return;
                DirectoryInfo dir = new DirectoryInfo(e.FullPath);
                node.SetDirectory(dir);
            }
        }
    }
}
