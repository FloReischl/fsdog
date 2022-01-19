using FR.Commands;
using FR.Configuration;
using FR.Drawing;
using FR.IO;
using FR.Net;
using FR.Windows.Forms;
using FR.Windows.Forms.Commands;
using FsDog.Commands;
using FsDog.Detail;
using FsDog.Properties;
using FsDog.Tree;
using FsDogBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FsDog.Dialogs {
    public partial class FormMain : FormBase {
        private const int DROP_ALT = 32;
        private const int DROP_CTRL = 8;
        private const int DROP_SHIFT = 4;
        private CommandToolItem _menu;
        private MenuStrip _menuStrip;
        private DetailView _currentDetailView;
        private TreeNode _dragOverNode;
        private Dictionary<string, ToolStrip> _toolStrips;
        private Point _location;
        private Size _size;
        private static readonly HashSet<Type> MyCommands = new HashSet<Type> {
                typeof(CmdFileNewFile), typeof(CmdFileNewFile), typeof(CmdFileNewDirectory), typeof(CmdFileRenameMulti), typeof(CmdFileOpen), typeof(CmdFileOpenWith), typeof(CmdFileRunAs), typeof(CmdFileProperties), typeof(CmdFileDosShell), typeof(CmdFilePowerShell), typeof(CommandFileExitBase), typeof(CommandEditCutBase), typeof(CommandEditCopyBase), typeof(CommandEditPasteBase), typeof(CmdEditCopyToOtherView), typeof(CmdEditMoveToOtherView), typeof(CmdEditCopyFileNames), typeof(CmdEditDelete), typeof(CmdEditSelectAll), typeof(CmdEditInvertSelection), typeof(CmdViewFind), typeof(CmdViewPreview), typeof(CmdViewGotoFavorite), typeof(CmdViewDirectorySizes), typeof(CmdViewRefresh), typeof(CmdFavorite), typeof(CmdFavoritesEdit), typeof(CmdCommandsEdit), typeof(CmdApplicationExecute), typeof(CmdScriptExecute), typeof(CmdScriptConfigureHosts), typeof(CmdToolsClearImageCache), typeof(CmdToolsOpenConfigFile), typeof(CmdToolsOptions), typeof(CmdHelpAbout)
            };


        public FormMain() {
            InitializeComponent();

            if (DesignMode) {
                return;
            }
            FormClosing += new FormClosingEventHandler(this.FormMain_FormClosing);
            Load += new EventHandler(this.FormMain_Load);
            Move += new EventHandler(this.FormMain_Move);
            Resize += new EventHandler(this.FormMain_Resize);
            ResizeEnd += new EventHandler(this.FormMain_ResizeEnd);
            Shown += new EventHandler(this.FormMain_Shown);
            detailView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.detailView_ColumnWidthChanged);
            detailView1.Enter += new EventHandler(this.detailView_Enter);
            detailView1.SelectionChanged += new EventHandler(this.detailView_SelectionChanged);
            detailView1.RequestParentDirectory += new DetailViewRequestParentDirectoryHandler(this.detailView_RequestParentDirectory);
            detailView2.SelectionChanged += new EventHandler(this.detailView_SelectionChanged);
            detailView2.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.detailView_ColumnWidthChanged);
            detailView2.Enter += new EventHandler(this.detailView_Enter);
            detailView2.RequestParentDirectory += new DetailViewRequestParentDirectoryHandler(this.detailView_RequestParentDirectory);
            sptDetailView.Panel1.Leave += new EventHandler(this.sptPanel_Leave);
            sptDetailView.Panel1.Enter += new EventHandler(this.sptPanel_Enter);
            sptDetailView.Panel2.Leave += new EventHandler(this.sptPanel_Leave);
            sptDetailView.Panel2.Enter += new EventHandler(this.sptPanel_Enter);
            sptMain.Panel1.Leave += new EventHandler(this.sptPanel_Leave);
            sptMain.Panel1.Enter += new EventHandler(this.sptPanel_Enter);
            sptMain.Panel2.Leave += new EventHandler(this.sptPanel_Leave);
            sptMain.Panel2.Enter += new EventHandler(this.sptPanel_Enter);
            tvwMain.AfterSelect += new TreeViewEventHandler(this.tvwMain_AfterSelect);
            tvwMain.Enter += new EventHandler(this.tvwMain_Enter);
            tvwMain.DragDrop += new DragEventHandler(this.tvwMain_DragDrop);
            tvwMain.DragLeave += new EventHandler(this.tvwMain_DragLeave);
            tvwMain.DragOver += new DragEventHandler(this.tvwMain_DragOver);
            tvwMain.Leave += new EventHandler(this.tvwMain_Leave);
            tvwMain.ItemDrag += new ItemDragEventHandler(this.tvwMain_ItemDrag);
            tvwMain.KeyDown += new KeyEventHandler(this.tvwMain_KeyDown);
        }

        public PreviewContainer CurrentPreview { get; private set; }
        public DirectoryInfo CurrentDirectory { get => _currentDetailView?.ParentDirectory; }

        public void GotoNextControl(Control ctrl, bool forward) {
            if (forward) {
                if (ctrl == this.tvwMain)
                    detailView1.Focus();
                else if (ctrl == this.detailView1) {
                    detailView2.Focus();
                }
                else {
                    if (ctrl != this.detailView2)
                        return;
                    tvwMain.Focus();
                }
            }
            else if (ctrl == this.tvwMain)
                detailView2.Focus();
            else if (ctrl == this.detailView1) {
                tvwMain.Focus();
            }
            else {
                if (ctrl != this.detailView2)
                    return;
                detailView1.Focus();
            }
        }

        public override ICommandReceiver GetCommandReceiver(System.Type commandType) {
            return MyCommands.Contains(commandType) ? (ICommandReceiver)this : base.GetCommandReceiver(commandType);
        }

        public override void InitializeCommand(ICommand command) {
            base.InitializeCommand(command);
            Control activeControl = this.ActiveControl;
            while (true) {
                for (; !(activeControl is ToolStripContainer); activeControl = ((ContainerControl)activeControl).ActiveControl) {
                    if (!(activeControl is SplitContainer)) {
                        if (command is CmdFsDog cmdFsDog) {
                            cmdFsDog.SelectedItems = this._currentDetailView.GetSelectedSystemInfos().ToArray();
                            cmdFsDog.ParentDirectory = this._currentDetailView.ParentDirectory;
                        }
                        if (!(command is CmdFsDogIntern cmdFsDogIntern))
                            return;
                        cmdFsDogIntern.ActiveFileControl = activeControl;
                        cmdFsDogIntern.DetailView1 = this.detailView1;
                        cmdFsDogIntern.DetailView2 = this.detailView2;
                        cmdFsDogIntern.CurrentDetailView = this._currentDetailView;
                        cmdFsDogIntern.Tree = this.tvwMain;
                        cmdFsDogIntern.FormMain = this;
                        return;
                    }
                }
                activeControl = ((ContainerControl)activeControl).ActiveControl;
            }
        }

        public override void FinishCommand(ICommand command) {
            base.FinishCommand(command);
            if (command.ExecutionState != CommandExecutionState.Ok)
                return;
            switch (command) {
                case CmdCommandsEdit _:
                    CreateCommandsToolStrips();
                    MenuStrip menuStrip1 = new CommandToolItem(".") {
                        Items = { CommandHelper.GetApplicationsToolItem() }
                    }.CreateMenuStrip();

                    ToolStripItem toolStripItem1 = menuStrip1.Items["Applications"];
                    menuStrip1.Items.Remove(toolStripItem1);
                    int index1 = this._menuStrip.Items.IndexOf(this._menuStrip.Items["Applications"]);
                    _menuStrip.Items.RemoveAt(index1);
                    _menuStrip.Items.Insert(index1, toolStripItem1);
                    menuStrip1.Dispose();
                    MenuStrip menuStrip2 = new CommandToolItem(".") {
                        Items = { CommandHelper.GetScriptsToolItem() }
                    }.CreateMenuStrip();
                    ToolStripItem toolStripItem2 = menuStrip2.Items["Skripts"];
                    menuStrip2.Items.Remove(toolStripItem2);
                    int index2 = this._menuStrip.Items.IndexOf(this._menuStrip.Items["Skripts"]);
                    _menuStrip.Items.RemoveAt(index2);
                    _menuStrip.Items.Insert(index2, toolStripItem2);
                    menuStrip2.Dispose();
                    break;
                case CmdFavoritesEdit _:
                    MenuStrip menuStrip3 = new CommandToolItem(".") {
                        Items = { CmdFavorite.GetFavoritesToolItem() }
                    }.CreateMenuStrip();
                    ToolStripItem toolStripItem3 = menuStrip3.Items["F&avorites"];
                    menuStrip3.Items.Remove(toolStripItem3);
                    int index3 = this._menuStrip.Items.IndexOf(this._menuStrip.Items["F&avorites"]);
                    _menuStrip.Items.RemoveAt(index3);
                    _menuStrip.Items.Insert(index3, toolStripItem3);
                    menuStrip3.Dispose();
                    break;
            }
        }

        public void SetPreview(PreviewContainer pc) {
            if (pc == null) {
                if (this.CurrentPreview != null) {
                    if (this.CurrentPreview.Parent != null)
                        CurrentPreview.Parent.Controls.Remove((Control)this.CurrentPreview);
                    CurrentPreview.Dispose();
                }
            }
            else {
                pc.Dock = DockStyle.Fill;
                if (this._currentDetailView == this.detailView1)
                    sptDetailView.Panel2.Controls.Add((Control)pc);
                else
                    sptDetailView.Panel1.Controls.Add((Control)pc);
                if (this._currentDetailView.CurrentFSI != null)
                    pc.SetFile(this._currentDetailView.CurrentFSI.FullName);
                pc.BringToFront();
            }
            CurrentPreview = pc;
        }

        public void ShowInfoMessage(string format, params object[] args) {
            format = string.Format(format, args);
            tslMainInfo.Text = format;
            tslMainInfo.ToolTipText = format;
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == 537)
                UpdateDevices();
            base.WndProc(ref m);
        }

        private void ClearDragDropNode() {
            if (this._dragOverNode == null)
                return;
            _dragOverNode.BackColor = this.tvwMain.BackColor;
            _dragOverNode = (TreeNode)null;
        }

        private void CreateCommandsToolStrips() {
            PointConverter pointConverter = new PointConverter();
            IConfigurationProperty subProperty = this.ConfigurationRoot.GetSubProperty("ToolStrips", true);
            ToolStripLocationsToConfig();
            foreach (ToolStrip applicationsToolStrip in (IEnumerable<ToolStrip>)CommandHelper.GetApplicationsToolStrips()) {
                Point location = new Point(applicationsToolStrip.Location.X, applicationsToolStrip.Location.Y);
                if (subProperty.ExistsSubProperty(applicationsToolStrip.Name))
                    location = (Point)pointConverter.ConvertFromString(subProperty[applicationsToolStrip.Name]["Location"].ToString(pointConverter.ConvertToString((object)location)));
                tscMain.TopToolStripPanel.Join(applicationsToolStrip, location);
                if (this._toolStrips.ContainsKey(applicationsToolStrip.Name))
                    _toolStrips.Remove(applicationsToolStrip.Name);
                _toolStrips.Add(applicationsToolStrip.Name, applicationsToolStrip);
            }
            foreach (ToolStrip scriptsToolStrip in (IEnumerable<ToolStrip>)CommandHelper.GetScriptsToolStrips()) {
                Point location = new Point(scriptsToolStrip.Location.X, scriptsToolStrip.Location.Y);
                if (subProperty.ExistsSubProperty(scriptsToolStrip.Name))
                    location = (Point)pointConverter.ConvertFromString(subProperty[scriptsToolStrip.Name]["Location"].ToString(pointConverter.ConvertToString((object)location)));
                tscMain.TopToolStripPanel.Join(scriptsToolStrip, location);
                if (this._toolStrips.ContainsKey(scriptsToolStrip.Name))
                    _toolStrips.Remove(scriptsToolStrip.Name);
                _toolStrips.Add(scriptsToolStrip.Name, scriptsToolStrip);
            }
        }

        private NodeDrive GetDriveNode(StringComparer sc, ref string path, ref bool handled) {
            Regex regex = new Regex("^[a-z]:\\\\", RegexOptions.IgnoreCase);
            Match match = regex.Match(path);
            if (match == null || string.IsNullOrEmpty(match.Value))
                return (NodeDrive)null;
            handled = true;
            path = regex.Replace(path, "");
            string y = match.Value;
            int index = 0;
            foreach (NodeBase node in this.tvwMain.Nodes) {
                if (node is NodeDrive driveNode) {
                    if (sc.Compare(driveNode.Drive.Name, y) == 0)
                        return driveNode;
                    if (sc.Compare(driveNode.Drive.Name, y) < 0)
                        index = driveNode.Index + 1;
                }
            }
            foreach (DriveInfo drive in DriveInfo.GetDrives()) {
                if (sc.Compare(drive.Name, y) == 0) {
                    NodeDrive node = new NodeDrive(drive);
                    tvwMain.Nodes.Insert(index, (TreeNodeBase)node);
                    return node;
                }
            }
            FormError.ShowError("Cannot find drive.", string.Format("Specified drive '{0}' cannot be found.", (object)y), (IWin32Window)this);
            return (NodeDrive)null;
        }

        private NodeBase GetNetworkNode(StringComparer sc, ref string path, ref bool handled) {
            Match match1 = new Regex("((?<=^\\\\\\\\).*?(?=\\\\))|((?<=^\\\\\\\\).*)", RegexOptions.IgnoreCase).Match(path);
            if (match1 == null || string.IsNullOrEmpty(match1.Value))
                return (NodeBase)null;
            handled = true;
            string str1 = match1.Value;
            path = path.Replace(str1, string.Empty);
            NodeNetworkServer node1 = (NodeNetworkServer)null;
            foreach (NodeBase node2 in this.tvwMain.Nodes) {
                if (node2 is NodeNetworkServer nodeNetworkServer && sc.Compare(nodeNetworkServer.ServerName, match1.Value) == 0) {
                    node1 = nodeNetworkServer;
                    break;
                }
            }
            if (node1 == null) {
                try {
                    node1 = new NodeNetworkServer(NetworkHelper.ConnectNetworkResource(this.Handle, str1, (string)null, (string)null, NETRESOURCE_CONNECT.CONNECT_INTERACTIVE));
                    tvwMain.Nodes.Add((TreeNodeBase)node1);
                }
                catch (Exception ex) {
                    FormError.ShowException(ex, (IWin32Window)this);
                    return (NodeBase)null;
                }
            }
            path = path.Trim('\\');
            Match match2 = new Regex("((?<=^\\\\*).*?(?=\\\\))|((?<=^\\\\*).*)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace).Match(path);
            if (match2 == null || string.IsNullOrEmpty(match2.Value))
                return (NodeBase)node1;
            string str2 = match2.Value;
            path = path.Replace(str2, string.Empty);
            path = path.Trim('\\');
            return (NodeBase)node1.GetShare(sc, str2) ?? (NodeBase)node1;
        }

        private NodeDirectory GetRootDirectoryNode(
          StringComparer sc,
          ref string path,
          ref bool handled) {
            Regex regex = new Regex("(^.*?(?=\\\\))|([^\\\\].*[^\\\\])", RegexOptions.IgnoreCase);
            Match match = regex.Match(path);
            if (match == null || string.IsNullOrEmpty(match.Value))
                return (NodeDirectory)null;
            handled = true;
            string x = match.Value;
            path = regex.Replace(path, string.Empty);
            path = path.Trim('\\');
            foreach (NodeBase node in this.tvwMain.Nodes) {
                if (node is NodeDirectory rootDirectoryNode && sc.Compare(x, rootDirectoryNode.Directory.Name) == 0)
                    return rootDirectoryNode;
            }
            FormError.ShowError("Cannot find root directory", string.Format("Cannot find root directory '{0}'", (object)x), (IWin32Window)this);
            return (NodeDirectory)null;
        }

        private NodeBase GetNodeFromTreePath(string path) {
            if (string.IsNullOrEmpty(path)) {
                FormError.ShowError("Invalid empty path", "The path cannot be empty.", (IWin32Window)this);
                return (NodeBase)null;
            }
            StringComparer cultureIgnoreCase = StringComparer.InvariantCultureIgnoreCase;
            bool handled = false;
            NodeBase nodeBase1 = (NodeBase)this.GetDriveNode(cultureIgnoreCase, ref path, ref handled);
            if (nodeBase1 == null && !handled)
                nodeBase1 = this.GetNetworkNode(cultureIgnoreCase, ref path, ref handled);
            if (nodeBase1 == null && !handled)
                nodeBase1 = (NodeBase)this.GetRootDirectoryNode(cultureIgnoreCase, ref path, ref handled);
            if (nodeBase1 == null) {
                if (!handled)
                    FormError.ShowError("Cannot evaluate root node.", string.Format("Root node of path '{0}' cannot be found.", (object)path), (IWin32Window)this);
                return (NodeBase)null;
            }
            NodeBase nodeFromTreePath = (NodeBase)null;
            path = path.TrimEnd('\\');
            if (string.IsNullOrEmpty(path)) {
                nodeFromTreePath = nodeBase1;
            }
            else {
                string[] strArray = path.Split(new string[1] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                TreeViewNodeCollection nodes = nodeBase1.Nodes;
                foreach (string x in strArray) {
                    foreach (TreeNode treeNode in nodes) {
                        if (treeNode is NodeBase nodeBase2 && cultureIgnoreCase.Compare(x, nodeBase2.Text) == 0) {
                            nodes = nodeBase2.Nodes;
                            nodeFromTreePath = nodeBase2;
                            break;
                        }
                    }
                    if (nodeFromTreePath == null)
                        break;
                }
            }
            return nodeFromTreePath;
        }

        private NodeBase GetRootNode(TreeNode nd) {
            while (nd.Parent != null)
                nd = nd.Parent;
            return nd as NodeBase;
        }

        private string GetTreePath(TreeNode nd, DirectoryInfo dir) => this.GetRootNode(nd) is NodeDirectory ? nd.FullPath : dir.FullName;

        private bool IsDragDropNode(TreeNode nd) {
            switch (nd) {
                case null:
                    return false;
                case NodeDirectory _:
                    return true;
                case NodeDrive _:
                    return true;
                default:
                    return false;
            }
        }

        private void ToolStripLocationsToConfig() {
            PointConverter pointConverter = new PointConverter();
            IConfigurationProperty subProperty = this.ConfigurationRoot.GetSubProperty("ToolStrips", true);
            foreach (ToolStrip toolStrip in this._toolStrips.Values)
                subProperty.GetSubProperty(toolStrip.Name, true).GetSubProperty("Location", true).Set(pointConverter.ConvertToString((object)toolStrip.Location));
        }

        private void UpdateDevices() {
            if (this.InvokeRequired) {
                Invoke((Delegate)new ThreadStart(this.UpdateDevices), new object[0]);
            }
            else {
                DriveInfo[] drives = DriveInfo.GetDrives();
                Dictionary<string, DriveInfo> dictionary = new Dictionary<string, DriveInfo>(drives.Length);
                foreach (DriveInfo driveInfo in drives)
                    dictionary.Add(driveInfo.Name, driveInfo);
                foreach (TreeNode node in this.tvwMain.Nodes) {
                    if (node is NodeDrive nodeDrive) {
                        if (dictionary.ContainsKey(nodeDrive.Drive.Name)) {
                            nodeDrive.RefreshDrive();
                            dictionary.Remove(nodeDrive.Drive.Name);
                        }
                        else
                            nodeDrive.Remove();
                    }
                }
                foreach (DriveInfo drive in dictionary.Values) {
                    int index = -1;
                    foreach (TreeNode node in this.tvwMain.Nodes) {
                        if (node is NodeDrive nodeDrive) {
                            if (index == -1)
                                index = nodeDrive.Index;
                            if (string.Compare(nodeDrive.Drive.Name, drive.Name, true) < 0)
                                index = nodeDrive.Index + 1;
                        }
                    }
                    NodeDrive node1 = new NodeDrive(drive);
                    tvwMain.Nodes.Insert(index, (TreeNodeBase)node1);
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e) {
            if (this.DesignMode)
                return;
            FsApp instance = FsApp.Instance;
            Icon = this.ApplicationInstance.GetAppIcon();
            _toolStrips = new Dictionary<string, ToolStrip>();
            CreateMenu();
            CreateCommandsToolStrips();
            if (!instance.Config.Options.Appearance.Main.ShowStatusbar)
                statMain.Visible = false;
            sptDetailView.BackColor = SystemColors.Control;
            _currentDetailView = this.detailView1;
            detailView1.SetActive(true);
            detailView2.SetActive(false);
            detailView1.ReadFromConfig(this.ConfigurationRoot.GetSubProperty("DetailView1", true), true);
            detailView2.ReadFromConfig(this.ConfigurationRoot.GetSubProperty("DetailView2", true), false);
            tslMainInfo.Text = string.Empty;
            SizeConverter sizeConverter = new SizeConverter();
            PointConverter pointConverter = new PointConverter();
            _size = (Size)sizeConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty("Size", true).ToString(sizeConverter.ConvertToString((object)this.Size)));

            if (instance.Config.Options.Appearance.Main.RememberSize)
                Size = this._size;

            _location = (Point)pointConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty("Location", true).ToString(pointConverter.ConvertToString((object)this.Location)));
            if (instance.Config.Options.Appearance.Main.RememberLocation)
                Location = this._location;

            if (instance.Config.Options.Appearance.Main.RememberWindowState)
                WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), this.ConfigurationRoot.GetSubProperty("WindowState", true).ToString(this.WindowState.ToString()));
            
            AppearanceProvider appearance = new AppearanceProvider();
            appearance.ApplyToForm(this);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            SizeConverter sizeConverter = new SizeConverter();
            PointConverter pointConverter = new PointConverter();
            ConfigurationRoot["Size"].Set(sizeConverter.ConvertToString((object)this._size));
            ConfigurationRoot["Location"].Set(pointConverter.ConvertToString((object)this._location));
            ConfigurationRoot["WindowState"].Set(((FormWindowState)(this.WindowState != FormWindowState.Minimized ? (int)this.WindowState : 0)).ToString());
            ToolStripLocationsToConfig();
            detailView1.SetToConfig(this.ConfigurationRoot.GetSubProperty("DetailView1", true), true);
            detailView2.SetToConfig(this.ConfigurationRoot.GetSubProperty("DetailView2", true), false);
        }

        private void FormMain_Move(object sender, EventArgs e) {
            if (this.WindowState != FormWindowState.Normal)
                return;
            _location = new Point(this.Location.X, this.Location.Y);
        }

        private void FormMain_Resize(object sender, EventArgs e) {
            int num = this.Width - 50 - this.tslMainInfo.Width;
            tslDriveInfo.Width = (int)((double)num / 2.4);
            tslCompleteInfo.Width = (int)((double)num / 3.8);
            tslSelectionInfo.Width = (int)((double)num / 3.8);
        }

        private void FormMain_ResizeEnd(object sender, EventArgs e) {
            if (this.WindowState != FormWindowState.Normal)
                return;
            _size = new Size(this.Size.Width, this.Size.Height);
        }

        private void FormMain_Shown(object sender, EventArgs e) {
            if (this.DesignMode)
                return;
            detailView1.Focus();
        }

        private void tvwMain_AfterSelect(object sender, TreeViewEventArgs e) {
            TreeNodeBase node = e.Node as TreeNodeBase;
            Cursor = Cursors.WaitCursor;
            FsApp instance = FsApp.Instance;
            DirectoryInfo directoryInfo = (DirectoryInfo)null;
            string treePath1 = (string)null;
            if (node is NodeDirectory nd1) {
                treePath1 = this.GetTreePath((TreeNode)nd1, nd1.Directory);
                directoryInfo = nd1.Directory;
            }
            if (node is NodeDrive nd2) {
                treePath1 = this.GetTreePath((TreeNode)nd2, nd2.Drive.RootDirectory);
                directoryInfo = nd2.Drive.RootDirectory;
            }
            if (directoryInfo != null) {
                e.Node.EnsureVisible();
                try {
                    _currentDetailView.SetParentDirectory(directoryInfo, treePath1, false);
                    instance.ShowInfoMessage(directoryInfo.Name);
                }
                catch (Exception ex) {
                    FormError.ShowException(ex, (IWin32Window)this);
                    directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                    TreeNodeBase nodeFromTreePath = (TreeNodeBase)this.GetNodeFromTreePath(directoryInfo.FullName);
                    string treePath2 = this.GetTreePath((TreeNode)nodeFromTreePath, directoryInfo);
                    nodeFromTreePath.EnsureVisible();
                    tvwMain.SelectedNode = nodeFromTreePath;
                    _currentDetailView.SetParentDirectory(directoryInfo, treePath2, true);
                }
                if (instance.Config.Options.Appearance.Main.FullPathInTitle)
                    Text = string.Format("{0} - {1}", instance.Information.Title, directoryInfo.FullName);
                else
                    Text = instance.Information.Title;
            }
            Cursor = Cursors.Default;
        }

        private void tvwMain_Enter(object sender, EventArgs e) {
        }

        private void tvwMain_DragOver(object sender, DragEventArgs e) {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.None;
            }
            else {
                TreeViewHitTestInfo treeViewHitTestInfo = this.tvwMain.HitTest(this.tvwMain.PointToClient(new Point(e.X, e.Y)));
                NodeBase node = treeViewHitTestInfo.Node as NodeBase;
                if (!this.IsDragDropNode(treeViewHitTestInfo.Node)) {
                    ClearDragDropNode();
                    e.Effect = DragDropEffects.None;
                }
                else {
                    e.Effect = (e.KeyState & 8) == 0 || (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.None ? ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.None ? ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.None ? DragDropEffects.None : DragDropEffects.Copy) : DragDropEffects.Move) : DragDropEffects.Copy;
                    if (e.Effect == DragDropEffects.None || node == this._dragOverNode)
                        return;
                    ClearDragDropNode();
                    _dragOverNode = (TreeNode)node;
                    _dragOverNode.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }

        private void tvwMain_DragDrop(object sender, DragEventArgs e) {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;
            TreeViewHitTestInfo treeViewHitTestInfo = this.tvwMain.HitTest(this.tvwMain.PointToClient(new Point(e.X, e.Y)));
            NodeBase node = treeViewHitTestInfo.Node as NodeBase;
            if (!this.IsDragDropNode(treeViewHitTestInfo.Node)) {
                ClearDragDropNode();
            }
            else {
                string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
                List<FileSystemInfo> fileSystemInfoList = new List<FileSystemInfo>(data.Length);
                foreach (string str in data) {
                    if (File.Exists(str))
                        fileSystemInfoList.Add((FileSystemInfo)new FileInfo(str));
                    else if (Directory.Exists(str))
                        fileSystemInfoList.Add((FileSystemInfo)new DirectoryInfo(str));
                }
                FormCopy formCopy = new FormCopy();
                formCopy.Sources = (IList<FileSystemInfo>)fileSystemInfoList;
                if (node is NodeDirectory)
                    formCopy.Destination = ((NodeDirectory)node).Directory;
                else if (node is NodeDrive)
                    formCopy.Destination = ((NodeDrive)node).Drive.RootDirectory;
                if (e.Effect == DragDropEffects.Move)
                    formCopy.MoveItems = true;
                int num = (int)formCopy.ShowDialog((IWin32Window)this.FindForm());
                ClearDragDropNode();
            }
        }

        private void tvwMain_DragLeave(object sender, EventArgs e) => this.ClearDragDropNode();

        private void tvwMain_Leave(object sender, EventArgs e) {
        }

        private void tvwMain_ItemDrag(object sender, ItemDragEventArgs e) {
            if (e.Button != MouseButtons.Left || !(e.Item is NodeDirectory nodeDirectory))
                return;
            string[] data = new string[1] {
                nodeDirectory.Directory.FullName
            };
            int num = (int)this.DoDragDrop((object)new DataObject(DataFormats.FileDrop, (object)data), DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void tvwMain_KeyDown(object sender, KeyEventArgs e) {
            e.SuppressKeyPress = true;
            if (e.KeyCode == Keys.Delete) {
                if (this.tvwMain.SelectedNode is NodeDirectory selectedNode)
                    FileHelper.DeleteDirectory(selectedNode.Directory.FullName, !e.Shift);
            }
            else
                e.SuppressKeyPress = false;
            e.Handled = e.SuppressKeyPress;
        }

        private void detailView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e) {
            if (!FsApp.Instance.Config.Options.DetailView.SynchronizeColumnWidths)
                return;
            if ((DetailView)sender == this.detailView1)
                detailView2.SetColumnWidth(e.Column.Name, e.Column.Width);
            else
                detailView1.SetColumnWidth(e.Column.Name, e.Column.Width);
        }

        private void detailView_RequestParentDirectory(object sender, DetailViewRequestDirectoryArgs e) {
            DetailView detailView = (DetailView)sender;
            NodeBase nd = (NodeBase)null;
            if (!string.IsNullOrEmpty(e.TreePath))
                nd = this.GetNodeFromTreePath(e.TreePath);
            if (nd == null && !string.IsNullOrEmpty(detailView.TreePath)) {
                NodeBase nodeFromTreePath = this.GetNodeFromTreePath(detailView.TreePath);
                if (nodeFromTreePath == null)
                    return;
                foreach (NodeBase node in nodeFromTreePath.Nodes) {
                    if (node is NodeDirectory nodeDirectory && e.Directory.FullName == nodeDirectory.Directory.FullName) {
                        nd = (NodeBase)nodeDirectory;
                        break;
                    }
                }
                if (nd == null) {
                    if (this.tvwMain.SelectedNode.Parent is NodeDirectory parent2 && string.Compare(parent2.Directory.FullName, e.Directory.FullName, true) == 0)
                        nd = (NodeBase)parent2;
                    else if (this.tvwMain.SelectedNode.Parent is NodeDrive parent1 && string.Compare(parent1.Drive.RootDirectory.FullName, e.Directory.FullName, true) == 0)
                        nd = (NodeBase)parent1;
                }
            }
            if (nd == null)
                nd = this.GetNodeFromTreePath(e.Directory.FullName);
            if (nd == null)
                return;
            if (detailView == this._currentDetailView) {
                nd.EnsureVisible();
                tvwMain.SelectedNode = (TreeNodeBase)nd;
            }
            else {
                DirectoryInfo directoryInfo = e.Directory;
                if (directoryInfo == null) {
                    if (nd is NodeDrive nodeDrive)
                        directoryInfo = nodeDrive.Drive.RootDirectory;
                    if (nd is NodeDirectory nodeDirectory)
                        directoryInfo = nodeDirectory.Directory;
                }
                string treePath = this.GetTreePath((TreeNode)nd, directoryInfo);
                detailView.SetParentDirectory(directoryInfo, treePath, false);
            }
        }

        private void detailView_Enter(object sender, EventArgs e) {
            DetailView detailView = (DetailView)sender;
            if (detailView == this._currentDetailView)
                return;
            _currentDetailView.SetActive(false);
            detailView.SetActive(true);
            _currentDetailView = detailView;
            if (string.IsNullOrEmpty(this._currentDetailView.TreePath))
                return;
            NodeBase nodeFromTreePath = this.GetNodeFromTreePath(this._currentDetailView.TreePath);
            if (nodeFromTreePath == null)
                return;
            nodeFromTreePath.EnsureVisible();
            tvwMain.SelectedNode = (TreeNodeBase)nodeFromTreePath;
        }

        private void detailView_SelectionChanged(object sender, EventArgs e) {
            DetailView detailView = (DetailView)sender;
            if (detailView != this._currentDetailView)
                return;
            if (this.CurrentPreview != null) {
                FileSystemInfo currentFsi = detailView.CurrentFSI;
                if (currentFsi != null)
                    CurrentPreview.SetFile(currentFsi.FullName);
            }
            Match match = new Regex("^[a-zA-Z]:\\\\").Match(detailView.ParentDirectory.FullName);
            if (match != null && !string.IsNullOrEmpty(match.Value)) {
                DriveInfo driveInfo = new DriveInfo(match.Value);
                long num = driveInfo.TotalSize - driveInfo.TotalFreeSpace;
                long totalFreeSpace = driveInfo.TotalFreeSpace;
                tslDriveInfo.Text = string.Format("{0} used / {1} free", (object)num.ToString("#,##0"), (object)totalFreeSpace.ToString("#,##0"));
            }
            uint completeCount = detailView.Infos.CompleteCount;
            ulong completeSize = detailView.Infos.CompleteSize;
            tslCompleteInfo.Text = string.Format("{0} Items {1}", (object)completeCount.ToString("#,##0"), (object)completeSize.ToString("#,##0"));
            uint selectedCount = detailView.Infos.SelectedCount;
            ulong selectedSize = detailView.Infos.SelectedSize;
            tslSelectionInfo.Text = string.Format("{0} Items {1}", (object)selectedCount.ToString("#,##0"), (object)selectedSize.ToString("#,##0"));
        }

        private void sptPanel_Enter(object sender, EventArgs e) => ((Control)sender).BackColor = System.Drawing.Color.SkyBlue;

        private void sptPanel_Leave(object sender, EventArgs e) => ((Control)sender).BackColor = SystemColors.Control;
    }
}
