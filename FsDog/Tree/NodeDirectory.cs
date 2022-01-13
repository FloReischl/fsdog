// Decompiled with JetBrains decompiler
// Type: FsDog.NodeDirectory
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Tree {
    public class NodeDirectory : NodeBase {
        private FileSystemWatcher _fsw;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DirectoryInfo _directory;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isRoot;

        public NodeDirectory(DirectoryInfo dir)
          : this(dir, false) {
        }

        public NodeDirectory(DirectoryInfo dir, bool isRoot) {
            ImageKey = "DirectoryClosed";
            SelectedImageKey = "DirectoryOpen";
            if (dir.FullName.EndsWith("\\"))
                dir = new DirectoryInfo(dir.FullName.TrimEnd('\\'));
            _directory = dir;
            _isRoot = isRoot;
            Refresh();
        }

        public DirectoryInfo Directory {
            [DebuggerNonUserCode]
            get => this._directory;
        }

        public bool IsRoot {
            [DebuggerNonUserCode]
            get => this._isRoot;
        }

        public override void Refresh() {
            base.Refresh();
            Image = FsApp.Instance.GetFsiImage((FileSystemInfo)this.Directory);
            if (this.IsRoot)
                Text = this.Directory.FullName;
            else
                Text = this.Directory.Name;
            Name = this.Text;
            if ((this.Directory.Attributes & FileAttributes.System) != (FileAttributes)0)
                ForeColor = Color.Blue;
            else if ((this.Directory.Attributes & FileAttributes.Hidden) != (FileAttributes)0)
                ForeColor = Color.Gray;
            if (!this.IsExpanded) {
                if (this.HasDummy())
                    return;
                CreateDummy();
            }
            else {
                if (this.HasDummy())
                    return;
                FsApp instance = FsApp.Instance;
                Dictionary<string, NodeDirectory> dictionary = new Dictionary<string, NodeDirectory>(this.Nodes.Count);
                foreach (NodeDirectory node in this.Nodes)
                    dictionary.Add(node.Directory.Name, node);
                foreach (DirectoryInfo subDirectory in instance.GetSubDirectories(this.Directory)) {
                    NodeDirectory nodeDirectory;
                    if (dictionary.TryGetValue(subDirectory.Name, out nodeDirectory)) {
                        dictionary.Remove(subDirectory.Name);
                        nodeDirectory.Refresh();
                    }
                    else
                        Nodes.Add((TreeNodeBase)new NodeDirectory(subDirectory));
                }
                foreach (TreeNode treeNode in dictionary.Values)
                    treeNode.Remove();
            }
        }

        public void SetDirectory(DirectoryInfo dir) {
            _directory = dir;
            Text = dir.Name;
            Name = dir.Name;
        }

        protected override void OnAfterCollapse(TreeViewEventArgs e) {
            RemoveWatcher();
            if (!this.HasDummy()) {
                foreach (TreeNode node in this.Nodes) {
                    if (node is NodeBase nodeBase && nodeBase.IsExpanded)
                        nodeBase.Collapse();
                }
                CreateDummy();
            }
            base.OnAfterCollapse(e);
        }

        protected override void OnLoadChildren(TreeViewCancelEventArgs e) {
            base.OnLoadChildren(e);
            foreach (DirectoryInfo subDirectory in FsApp.Instance.GetSubDirectories(this.Directory))
                Nodes.Add((TreeNodeBase)new NodeDirectory(subDirectory));
            CreateWatcher();
        }

        private void CreateWatcher() {
            RemoveWatcher();
            _fsw = new FileSystemWatcher();
            _fsw.IncludeSubdirectories = false;
            _fsw.Path = this.Directory.FullName;
            _fsw.Changed += new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            _fsw.Created += new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            _fsw.Deleted += new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            _fsw.Renamed += new RenamedEventHandler(this.FileSystemWatcher_Renamed);
            _fsw.EnableRaisingEvents = true;
        }

        private void RemoveWatcher() {
            if (this._fsw == null)
                return;
            _fsw.Changed -= new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            _fsw.Created -= new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            _fsw.Deleted -= new FileSystemEventHandler(this.FileSystemWatcher_Changed);
            _fsw.Renamed -= new RenamedEventHandler(this.FileSystemWatcher_Renamed);
            _fsw.Dispose();
            _fsw = (FileSystemWatcher)null;
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e) {
            if (this.TreeView.InvokeRequired)
                TreeView.Invoke((Delegate)new FileSystemEventHandler(this.FileSystemWatcher_Changed), sender, (object)e);
            else if (e.ChangeType == WatcherChangeTypes.All) {
                if (this.IsExpanded)
                    Refresh();
                else
                    CreateDummy();
            }
            else {
                if (e.ChangeType == WatcherChangeTypes.Changed)
                    return;
                if (e.ChangeType == WatcherChangeTypes.Created) {
                    if (!System.IO.Directory.Exists(e.FullPath))
                        return;
                    Nodes.Add((TreeNodeBase)new NodeDirectory(new DirectoryInfo(e.FullPath)));
                }
                else if (e.ChangeType == WatcherChangeTypes.Deleted) {
                    if (!(this.Nodes[e.Name] is NodeBase node))
                        return;
                    Nodes.Remove((TreeNodeBase)node);
                }
                else {
                    int changeType = (int)e.ChangeType;
                }
            }
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e) {
            if (this.TreeView.InvokeRequired) {
                TreeView.Invoke((Delegate)new RenamedEventHandler(this.FileSystemWatcher_Renamed), sender, (object)e);
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
