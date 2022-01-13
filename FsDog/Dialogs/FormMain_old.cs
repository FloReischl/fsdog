//// Decompiled with JetBrains decompiler
//// Type: FsDog.FormMain
//// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
//// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

//using FR;
//using FR.Commands;
//using FR.Configuration;
//using FR.Drawing;
//using FR.IO;
//using FR.Net;
//using FR.Windows.Forms;
//using FR.Windows.Forms.Commands;
//using FsDog.Commands;
//using FsDog.Properties;
//using FsDogBase;
//using Microsoft.Win32;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.IO;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Windows.Forms;

//namespace FsDog {
//    public class FormMain_old : FormBase {
//        private const int DROP_ALT = 32;
//        private const int DROP_CTRL = 8;
//        private const int DROP_SHIFT = 4;
//        private CommandToolItem _menu;
//        private MenuStrip _menuStrip;
//        private DetailView _currentDetailView;
//        private TreeNode _dragOverNode;
//        private Dictionary<string, ToolStrip> _toolStrips;
//        private Point _location;
//        private Size _size;
//        private IContainer components;
//        private StatusStrip statMain;
//        private ToolStripContainer tscMain;
//        private SplitContainer sptMain;
//        private TreeMain tvwMain;
//        private ToolStrip tsMain;
//        private DetailView detailView1;
//        private SplitContainer sptDetailView;
//        private DetailView detailView2;
//        private ToolStripContainer tscDetailView;
//        private ToolStripStatusLabel tslDriveInfo;
//        private ToolStripStatusLabel tslCompleteInfo;
//        private ToolStripStatusLabel tslSelectionInfo;
//        private ToolStripStatusLabel tslMainInfo;
//        private static readonly HashSet<Type> MyCommands = new HashSet<Type> {
//                typeof(CmdFileNewFile), typeof(CmdFileNewFile), typeof(CmdFileNewDirectory), typeof(CmdFileRenameMulti), typeof(CmdFileOpen), typeof(CmdFileOpenWith), typeof(CmdFileRunAs), typeof(CmdFileProperties), typeof(CmdFileDosShell), typeof(CmdFilePowerShell), typeof(CommandFileExitBase), typeof(CommandEditCutBase), typeof(CommandEditCopyBase), typeof(CommandEditPasteBase), typeof(CmdEditCopyToOtherView), typeof(CmdEditMoveToOtherView), typeof(CmdEditCopyFileNames), typeof(CmdEditDelete), typeof(CmdEditSelectAll), typeof(CmdEditInvertSelection), typeof(CmdViewPreview), typeof(CmdViewGotoFavorite), typeof(CmdViewDirectorySizes), typeof(CmdViewRefresh), typeof(CmdFavorite), typeof(CmdFavoritesEdit), typeof(CmdCommandsEdit), typeof(CmdApplicationExecute), typeof(CmdScriptExecute), typeof(CmdScriptConfigureHosts), typeof(CmdToolsClearImageCache), typeof(CmdToolsOpenConfigFile), typeof(CmdToolsOptions), typeof(CmdHelpAbout)
//            };


//        public FormMain_old() {
//            InitializeComponent();
//            FormClosing += new FormClosingEventHandler(this.FormMain_FormClosing);
//            Load += new EventHandler(this.FormMain_Load);
//            Move += new EventHandler(this.FormMain_Move);
//            Resize += new EventHandler(this.FormMain_Resize);
//            ResizeEnd += new EventHandler(this.FormMain_ResizeEnd);
//            Shown += new EventHandler(this.FormMain_Shown);
//            detailView1.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.detailView_ColumnWidthChanged);
//            detailView1.Enter += new EventHandler(this.detailView_Enter);
//            detailView1.SelectionChanged += new EventHandler(this.detailView_SelectionChanged);
//            detailView1.RequestParentDirectory += new DetailViewRequestParentDirectoryHandler(this.detailView_RequestParentDirectory);
//            detailView2.SelectionChanged += new EventHandler(this.detailView_SelectionChanged);
//            detailView2.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.detailView_ColumnWidthChanged);
//            detailView2.Enter += new EventHandler(this.detailView_Enter);
//            detailView2.RequestParentDirectory += new DetailViewRequestParentDirectoryHandler(this.detailView_RequestParentDirectory);
//            sptDetailView.Panel1.Leave += new EventHandler(this.sptPanel_Leave);
//            sptDetailView.Panel1.Enter += new EventHandler(this.sptPanel_Enter);
//            sptDetailView.Panel2.Leave += new EventHandler(this.sptPanel_Leave);
//            sptDetailView.Panel2.Enter += new EventHandler(this.sptPanel_Enter);
//            sptMain.Panel1.Leave += new EventHandler(this.sptPanel_Leave);
//            sptMain.Panel1.Enter += new EventHandler(this.sptPanel_Enter);
//            sptMain.Panel2.Leave += new EventHandler(this.sptPanel_Leave);
//            sptMain.Panel2.Enter += new EventHandler(this.sptPanel_Enter);
//            tvwMain.AfterSelect += new TreeViewEventHandler(this.tvwMain_AfterSelect);
//            tvwMain.Enter += new EventHandler(this.tvwMain_Enter);
//            tvwMain.DragDrop += new DragEventHandler(this.tvwMain_DragDrop);
//            tvwMain.DragLeave += new EventHandler(this.tvwMain_DragLeave);
//            tvwMain.DragOver += new DragEventHandler(this.tvwMain_DragOver);
//            tvwMain.Leave += new EventHandler(this.tvwMain_Leave);
//            tvwMain.ItemDrag += new ItemDragEventHandler(this.tvwMain_ItemDrag);
//            tvwMain.KeyDown += new KeyEventHandler(this.tvwMain_KeyDown);
//        }

//        public PreviewContainer CurrentPreview { get; private set; }

//        public void GotoNextControl(Control ctrl, bool forward) {
//            if (forward) {
//                if (ctrl == this.tvwMain)
//                    detailView1.Focus();
//                else if (ctrl == this.detailView1) {
//                    detailView2.Focus();
//                }
//                else {
//                    if (ctrl != this.detailView2)
//                        return;
//                    tvwMain.Focus();
//                }
//            }
//            else if (ctrl == this.tvwMain)
//                detailView2.Focus();
//            else if (ctrl == this.detailView1) {
//                tvwMain.Focus();
//            }
//            else {
//                if (ctrl != this.detailView2)
//                    return;
//                detailView1.Focus();
//            }
//        }

//        public override ICommandReceiver GetCommandReceiver(System.Type commandType) {
//            return MyCommands.Contains(commandType) ? (ICommandReceiver)this : base.GetCommandReceiver(commandType);
//        }

//        public override void InitializeCommand(ICommand command) {
//            base.InitializeCommand(command);
//            Control activeControl = this.ActiveControl;
//            while (true) {
//                for (; !(activeControl is ToolStripContainer); activeControl = ((ContainerControl)activeControl).ActiveControl) {
//                    if (!(activeControl is SplitContainer)) {
//                        if (command is CmdFsDog cmdFsDog) {
//                            cmdFsDog.SelectedItems = this._currentDetailView.GetSelectedSystemInfos().ToArray();
//                            cmdFsDog.ParentDirectory = this._currentDetailView.ParentDirectory;
//                        }
//                        if (!(command is CmdFsDogIntern cmdFsDogIntern))
//                            return;
//                        cmdFsDogIntern.ActiveFileControl = activeControl;
//                        cmdFsDogIntern.DetailView1 = this.detailView1;
//                        cmdFsDogIntern.DetailView2 = this.detailView2;
//                        cmdFsDogIntern.CurrentDetailView = this._currentDetailView;
//                        cmdFsDogIntern.Tree = this.tvwMain;
//                        cmdFsDogIntern.FormMain = this;
//                        return;
//                    }
//                }
//                activeControl = ((ContainerControl)activeControl).ActiveControl;
//            }
//        }

//        public override void FinishCommand(ICommand command) {
//            base.FinishCommand(command);
//            if (command.ExecutionState != CommandExecutionState.Ok)
//                return;
//            switch (command) {
//                case CmdCommandsEdit _:
//                    CreateCommandsToolStrips();
//                    MenuStrip menuStrip1 = new CommandToolItem(".") {
//                        Items = {
//              CommandHelper.GetApplicationsToolItem()
//            }
//                    }.CreateMenuStrip();
//                    ToolStripItem toolStripItem1 = menuStrip1.Items["Applications"];
//                    menuStrip1.Items.Remove(toolStripItem1);
//                    int index1 = this._menuStrip.Items.IndexOf(this._menuStrip.Items["Applications"]);
//                    _menuStrip.Items.RemoveAt(index1);
//                    _menuStrip.Items.Insert(index1, toolStripItem1);
//                    menuStrip1.Dispose();
//                    MenuStrip menuStrip2 = new CommandToolItem(".") {
//                        Items = {
//              CommandHelper.GetScriptsToolItem()
//            }
//                    }.CreateMenuStrip();
//                    ToolStripItem toolStripItem2 = menuStrip2.Items["Skripts"];
//                    menuStrip2.Items.Remove(toolStripItem2);
//                    int index2 = this._menuStrip.Items.IndexOf(this._menuStrip.Items["Skripts"]);
//                    _menuStrip.Items.RemoveAt(index2);
//                    _menuStrip.Items.Insert(index2, toolStripItem2);
//                    menuStrip2.Dispose();
//                    break;
//                case CmdFavoritesEdit _:
//                    MenuStrip menuStrip3 = new CommandToolItem(".") {
//                        Items = {
//              CmdFavorite.GetFavoritesToolItem()
//            }
//                    }.CreateMenuStrip();
//                    ToolStripItem toolStripItem3 = menuStrip3.Items["F&avorites"];
//                    menuStrip3.Items.Remove(toolStripItem3);
//                    int index3 = this._menuStrip.Items.IndexOf(this._menuStrip.Items["F&avorites"]);
//                    _menuStrip.Items.RemoveAt(index3);
//                    _menuStrip.Items.Insert(index3, toolStripItem3);
//                    menuStrip3.Dispose();
//                    break;
//            }
//        }

//        public void SetPreview(PreviewContainer pc) {
//            if (pc == null) {
//                if (this.CurrentPreview != null) {
//                    if (this.CurrentPreview.Parent != null)
//                        CurrentPreview.Parent.Controls.Remove((Control)this.CurrentPreview);
//                    CurrentPreview.Dispose();
//                }
//            }
//            else {
//                pc.Dock = DockStyle.Fill;
//                if (this._currentDetailView == this.detailView1)
//                    sptDetailView.Panel2.Controls.Add((Control)pc);
//                else
//                    sptDetailView.Panel1.Controls.Add((Control)pc);
//                if (this._currentDetailView.CurrentFSI != null)
//                    pc.SetFile(this._currentDetailView.CurrentFSI.FullName);
//                pc.BringToFront();
//            }
//            CurrentPreview = pc;
//        }

//        public void ShowInfoMessage(string format, params object[] args) {
//            format = string.Format(format, args);
//            tslMainInfo.Text = format;
//            tslMainInfo.ToolTipText = format;
//        }

//        protected override void WndProc(ref Message m) {
//            if (m.Msg == 537)
//                UpdateDevices();
//            base.WndProc(ref m);
//        }

//        private void ClearDragDropNode() {
//            if (this._dragOverNode == null)
//                return;
//            _dragOverNode.BackColor = this.tvwMain.BackColor;
//            _dragOverNode = (TreeNode)null;
//        }

//        private void CreateCommandsToolStrips() {
//            PointConverter pointConverter = new PointConverter();
//            IConfigurationProperty subProperty = this.ConfigurationRoot.GetSubProperty("ToolStrips", true);
//            ToolStripLocationsToConfig();
//            foreach (ToolStrip applicationsToolStrip in (IEnumerable<ToolStrip>)CommandHelper.GetApplicationsToolStrips()) {
//                Point location = new Point(applicationsToolStrip.Location.X, applicationsToolStrip.Location.Y);
//                if (subProperty.ExistsSubProperty(applicationsToolStrip.Name))
//                    location = (Point)pointConverter.ConvertFromString(subProperty[applicationsToolStrip.Name]["Location"].ToString(pointConverter.ConvertToString((object)location)));
//                tscMain.TopToolStripPanel.Join(applicationsToolStrip, location);
//                if (this._toolStrips.ContainsKey(applicationsToolStrip.Name))
//                    _toolStrips.Remove(applicationsToolStrip.Name);
//                _toolStrips.Add(applicationsToolStrip.Name, applicationsToolStrip);
//            }
//            foreach (ToolStrip scriptsToolStrip in (IEnumerable<ToolStrip>)CommandHelper.GetScriptsToolStrips()) {
//                Point location = new Point(scriptsToolStrip.Location.X, scriptsToolStrip.Location.Y);
//                if (subProperty.ExistsSubProperty(scriptsToolStrip.Name))
//                    location = (Point)pointConverter.ConvertFromString(subProperty[scriptsToolStrip.Name]["Location"].ToString(pointConverter.ConvertToString((object)location)));
//                tscMain.TopToolStripPanel.Join(scriptsToolStrip, location);
//                if (this._toolStrips.ContainsKey(scriptsToolStrip.Name))
//                    _toolStrips.Remove(scriptsToolStrip.Name);
//                _toolStrips.Add(scriptsToolStrip.Name, scriptsToolStrip);
//            }
//        }

//        private void CreateMenu() {
//            tscMain.TopToolStripPanel.Controls.Remove((Control)this.tsMain);
//            _menu = new CommandToolItem("Main");
//            _menu.Name = "Main";
//            var fileParent = new CommandToolItem("&File");
//            _menu.Items.Add(fileParent);
//            fileParent.Items.Add(new CommandToolItem("&New File", typeof(CmdFileNewFile), CommonImages.GetImage(CommonImageType.PageNew), Keys.N | Keys.Control) {
//                ShowAlwaysToolStrip = true
//            });
//            fileParent.Items.Add(new CommandToolItem("New &Directory", typeof(CmdFileNewDirectory), (Image)Resources.DirectoryNew, Keys.N | Keys.Shift | Keys.Control) {
//                ShowAlwaysToolStrip = true
//            });
//            fileParent.Items.Add(new CommandToolItem("-"));
//            fileParent.Items.Add(new CommandToolItem("&Rename multiple files", typeof(CmdFileRenameMulti), (Image)null, Keys.F2 | Keys.Shift | Keys.Control) {
//                ShowNeverToolStrip = true
//            });
//            fileParent.Items.Add(new CommandToolItem("Open", typeof(CmdFileOpen)) {
//                ShowNeverToolStrip = true
//            });
//            fileParent.Items.Add(new CommandToolItem("Open with...", typeof(CmdFileOpenWith)) {
//                ShowNeverToolStrip = true
//            });
//            fileParent.Items.Add(new CommandToolItem("Run as...", typeof(CmdFileRunAs), (Image)null, Keys.Return | Keys.Shift | Keys.Control) {
//                ShowNeverToolStrip = true
//            });
//            var commandToolItem2 = new CommandToolItem("Properties", typeof(CmdFileProperties), (Image)Resources.Properties, Keys.Return | Keys.Alt);
//            fileParent.Items.Add(commandToolItem2);
//            fileParent.Items.Add(new CommandToolItem("-"));
//            var commandToolItem3 = new CommandToolItem("Open DOS Shell", typeof(CmdFileDosShell), (Image)Resources.DosShell, Keys.D | Keys.Control);
//            fileParent.Items.Add(commandToolItem3);
//            string str = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\PowerShell\\1\\ShellIds\\Microsoft.PowerShell", "Path", (object)null);
//            if (!string.IsNullOrEmpty(str))
//                fileParent.Items.Add(new CommandToolItem("Open PowerShell", typeof(CmdFilePowerShell), (Image)Resources.PowerShell, Keys.D | Keys.Shift | Keys.Control) {
//                    CommandContext = { { "PowerShell", str } }
//            });
//            fileParent.Items.Add(new CommandToolItem("-"));
//            var fileExit = CommandToolItem.CreateFileExit((System.Type)null);
//            fileParent.Items.Add(fileExit);

//            var editParent = new CommandToolItem("&Edit");
//            _menu.Items.Add(editParent);
//            var editCutItem = CommandToolItem.CreateEditCutItem(typeof(CommandEditCutBase));
//            editCutItem.ShowAlwaysToolStrip = true;
//            editParent.Items.Add(editCutItem);
//            var editCopyItem = CommandToolItem.CreateEditCopyItem(typeof(CommandEditCopyBase));
//            editCopyItem.ShowAlwaysToolStrip = true;
//            editParent.Items.Add(editCopyItem);
//            var editPasteItem = CommandToolItem.CreateEditPasteItem(typeof(CommandEditPasteBase));
//            editPasteItem.ShowAlwaysToolStrip = true;
//            editParent.Items.Add(editPasteItem);
//            editParent.Items.Add(new CommandToolItem("-"));
//            editParent.Items.Add(new CommandToolItem("Copy selected other detail view", typeof(CmdEditCopyToOtherView), (Image)null, Keys.C | Keys.Shift | Keys.Control) {
//                ShowNeverToolStrip = true
//            });
//            editParent.Items.Add(new CommandToolItem("Move selected other detail view", typeof(CmdEditMoveToOtherView), (Image)null, Keys.X | Keys.Shift | Keys.Control) {
//                ShowNeverToolStrip = true
//            });
//            editParent.Items.Add(new CommandToolItem("-"));
//            editParent.Items.Add(new CommandToolItem("Copy selected names", typeof(CmdEditCopyFileNames), (Image)null, Keys.H | Keys.Control) {
//                ShowNeverToolStrip = true,
//                CommandContext = { { "CopyType", CmdEditCopyFileNames.CopyNameType.Name } }
//            });
//            editParent.Items.Add(new CommandToolItem("Copy selected pathes", typeof(CmdEditCopyFileNames), (Image)null, Keys.H | Keys.Shift | Keys.Control) {
//                ShowNeverToolStrip = true,
//                CommandContext = { { "CopyType", CmdEditCopyFileNames.CopyNameType.FullName } }
//            });
//            editParent.Items.Add(new CommandToolItem("Copy parent path", typeof(CmdEditCopyFileNames), (Image)null, Keys.H | Keys.Control | Keys.Alt) {
//                ShowNeverToolStrip = true,
//                CommandContext = { { "CopyType", CmdEditCopyFileNames.CopyNameType.ParentDirectory } }
//            });
//            editParent.Items.Add(new CommandToolItem("-"));
//            editParent.Items.Add(new CommandToolItem("&Delete", typeof(CmdEditDelete), CommonImages.GetImage(CommonImageType.Delete)) {
//                ShowAlwaysToolStrip = true
//            });
//            var commandToolItem5 = new CommandToolItem("-");
//            editParent.Items.Add(commandToolItem5);
//            editParent.Items.Add(new CommandToolItem("Select &All", typeof(CmdEditSelectAll), (Image)null, Keys.A | Keys.Control) {
//                ShowNeverToolStrip = true
//            });
//            editParent.Items.Add(new CommandToolItem("&Invert Selection", typeof(CmdEditInvertSelection), (Image)null, Keys.A | Keys.Shift | Keys.Control) {
//                ShowNeverToolStrip = true
//            });
//            editParent.Items.Add(new CommandToolItem("&Find", typeof(CmdEditFind), CommonImages.GetImage(CommonImageType.Find), Keys.F3 | Keys.Control) {
//                ShowAlwaysToolStrip = true
//            });

//            var viewParent = new CommandToolItem("&View");
//            _menu.Items.Add(viewParent);
//            var commandToolItem8 = new CommandToolItem("Preview", typeof(CmdViewPreview), (Image)Resources.Preview, Keys.F11);
//            viewParent.Items.Add(commandToolItem8);
//            viewParent.Items.Add(new CommandToolItem("-"));
//            viewParent.Items.Add(new CommandToolItem("Show directory sizes", typeof(CmdViewDirectorySizes), (Image)null, Keys.S | Keys.Shift | Keys.Control) {
//                ShowNeverToolStrip = true
//            });
//            viewParent.Items.Add(new CommandToolItem("-"));
//            viewParent.Items.Add(new CommandToolItem("&Refresh", typeof(CmdViewRefresh), CommonImages.GetImage(CommonImageType.Refresh), Keys.F5));

//            var favoritesParent = CmdFavorite.GetFavoritesToolItem();
//            _menu.Items.Add(favoritesParent);
//            favoritesParent.Items.Insert(0, new CommandToolItem("&Go to Favorite", typeof(CmdViewGotoFavorite), (Image)Resources.Favorites, Keys.F4));
//            favoritesParent.Items. Insert(1, new CommandToolItem("-"));
//            var applicationsParent = CommandHelper.GetApplicationsToolItem();
//            _menu.Items.Add(applicationsParent);
//            var scriptsParent = CommandHelper.GetScriptsToolItem();
//            _menu.Items.Add(scriptsParent);

//            var toolsParent = new CommandToolItem("&Tools");
//            _menu.Items.Add(toolsParent);
//            toolsParent.Items.Add(new CommandToolItem("Clear Icon Cache", typeof(CmdToolsClearImageCache)) {
//                ShowNeverToolStrip = true
//            });
//            toolsParent.Items.Add(new CommandToolItem("-"));
//            toolsParent.Items.Add(new CommandToolItem("Edit Applications and Scripts", typeof(CmdCommandsEdit)) {
//                ShowNeverToolStrip = true
//            });
//            toolsParent.Items.Add(new CommandToolItem("Configure Scripting Hosts", typeof(CmdScriptConfigureHosts)) {
//                ShowNeverToolStrip = true
//            });
//            toolsParent.Items.Add(new CommandToolItem("-"));
//            toolsParent.Items.Add(new CommandToolItem("Open Configuration File", typeof(CmdToolsOpenConfigFile)) {
//                ShowNeverToolStrip = true
//            });
//            toolsParent.Items.Add(new CommandToolItem("Options", typeof(CmdToolsOptions)) {
//                ShowNeverToolStrip = true
//            });

//            var helpParent = new CommandToolItem("&Help");
//            _menu.Items.Add(helpParent);
//            helpParent.Items.Add(new CommandToolItem("&About", typeof(CmdHelpAbout)) {
//                ShowNeverToolStrip = true
//            });
//            _menuStrip = this._menu.CreateMenuStrip();
//            _menuStrip.Dock = DockStyle.Top;
//            Controls.Add((Control)this._menuStrip);
//            PointConverter pointConverter = new PointConverter();
//            IConfigurationProperty subProperty = this.ConfigurationRoot.GetSubProperty("ToolStrips", true);
//            _menu.Items.Remove(applicationsParent);
//            _menu.Items.Remove(scriptsParent);
//            _menu.Items.Remove(favoritesParent);
//            ToolStrip toolStrip = this._menu.CreateToolStrip();
//            Point location = new Point(toolStrip.Location.X, toolStrip.Location.Y);
//            if (subProperty.ExistsSubProperty(toolStrip.Name))
//                location = (Point)pointConverter.ConvertFrom((object)subProperty[toolStrip.Name]["Location"].ToString(pointConverter.ConvertToString((object)location)));
//            tscMain.TopToolStripPanel.Join(toolStrip, location);
//            _toolStrips.Add(toolStrip.Name, toolStrip);
//        }

//        private NodeDrive GetDriveNode(StringComparer sc, ref string path, ref bool handled) {
//            Regex regex = new Regex("^[a-z]:\\\\", RegexOptions.IgnoreCase);
//            Match match = regex.Match(path);
//            if (match == null || string.IsNullOrEmpty(match.Value))
//                return (NodeDrive)null;
//            handled = true;
//            path = regex.Replace(path, "");
//            string y = match.Value;
//            int index = 0;
//            foreach (NodeBase node in this.tvwMain.Nodes) {
//                if (node is NodeDrive driveNode) {
//                    if (sc.Compare(driveNode.Drive.Name, y) == 0)
//                        return driveNode;
//                    if (sc.Compare(driveNode.Drive.Name, y) < 0)
//                        index = driveNode.Index + 1;
//                }
//            }
//            foreach (DriveInfo drive in DriveInfo.GetDrives()) {
//                if (sc.Compare(drive.Name, y) == 0) {
//                    NodeDrive node = new NodeDrive(drive);
//                    tvwMain.Nodes.Insert(index, (TreeNodeBase)node);
//                    return node;
//                }
//            }
//            FormError.ShowError("Cannot find drive.", string.Format("Specified drive '{0}' cannot be found.", (object)y), (IWin32Window)this);
//            return (NodeDrive)null;
//        }

//        private NodeBase GetNetworkNode(StringComparer sc, ref string path, ref bool handled) {
//            Match match1 = new Regex("((?<=^\\\\\\\\).*?(?=\\\\))|((?<=^\\\\\\\\).*)", RegexOptions.IgnoreCase).Match(path);
//            if (match1 == null || string.IsNullOrEmpty(match1.Value))
//                return (NodeBase)null;
//            handled = true;
//            string str1 = match1.Value;
//            path = path.Replace(str1, string.Empty);
//            NodeNetworkServer node1 = (NodeNetworkServer)null;
//            foreach (NodeBase node2 in this.tvwMain.Nodes) {
//                if (node2 is NodeNetworkServer nodeNetworkServer && sc.Compare(nodeNetworkServer.ServerName, match1.Value) == 0) {
//                    node1 = nodeNetworkServer;
//                    break;
//                }
//            }
//            if (node1 == null) {
//                try {
//                    node1 = new NodeNetworkServer(NetworkHelper.ConnectNetworkResource(this.Handle, str1, (string)null, (string)null, NETRESOURCE_CONNECT.CONNECT_INTERACTIVE));
//                    tvwMain.Nodes.Add((TreeNodeBase)node1);
//                }
//                catch (Exception ex) {
//                    FormError.ShowException(ex, (IWin32Window)this);
//                    return (NodeBase)null;
//                }
//            }
//            path = path.Trim('\\');
//            Match match2 = new Regex("((?<=^\\\\*).*?(?=\\\\))|((?<=^\\\\*).*)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace).Match(path);
//            if (match2 == null || string.IsNullOrEmpty(match2.Value))
//                return (NodeBase)node1;
//            string str2 = match2.Value;
//            path = path.Replace(str2, string.Empty);
//            path = path.Trim('\\');
//            return (NodeBase)node1.GetShare(sc, str2) ?? (NodeBase)node1;
//        }

//        private NodeDirectory GetRootDirectoryNode(
//          StringComparer sc,
//          ref string path,
//          ref bool handled) {
//            Regex regex = new Regex("(^.*?(?=\\\\))|([^\\\\].*[^\\\\])", RegexOptions.IgnoreCase);
//            Match match = regex.Match(path);
//            if (match == null || string.IsNullOrEmpty(match.Value))
//                return (NodeDirectory)null;
//            handled = true;
//            string x = match.Value;
//            path = regex.Replace(path, string.Empty);
//            path = path.Trim('\\');
//            foreach (NodeBase node in this.tvwMain.Nodes) {
//                if (node is NodeDirectory rootDirectoryNode && sc.Compare(x, rootDirectoryNode.Directory.Name) == 0)
//                    return rootDirectoryNode;
//            }
//            FormError.ShowError("Cannot find root directory", string.Format("Cannot find root directory '{0}'", (object)x), (IWin32Window)this);
//            return (NodeDirectory)null;
//        }

//        private NodeBase GetNodeFromTreePath(string path) {
//            if (string.IsNullOrEmpty(path)) {
//                FormError.ShowError("Invalid empty path", "The path cannot be empty.", (IWin32Window)this);
//                return (NodeBase)null;
//            }
//            StringComparer cultureIgnoreCase = StringComparer.InvariantCultureIgnoreCase;
//            bool handled = false;
//            NodeBase nodeBase1 = (NodeBase)this.GetDriveNode(cultureIgnoreCase, ref path, ref handled);
//            if (nodeBase1 == null && !handled)
//                nodeBase1 = this.GetNetworkNode(cultureIgnoreCase, ref path, ref handled);
//            if (nodeBase1 == null && !handled)
//                nodeBase1 = (NodeBase)this.GetRootDirectoryNode(cultureIgnoreCase, ref path, ref handled);
//            if (nodeBase1 == null) {
//                if (!handled)
//                    FormError.ShowError("Cannot evaluate root node.", string.Format("Root node of path '{0}' cannot be found.", (object)path), (IWin32Window)this);
//                return (NodeBase)null;
//            }
//            NodeBase nodeFromTreePath = (NodeBase)null;
//            path = path.TrimEnd('\\');
//            if (string.IsNullOrEmpty(path)) {
//                nodeFromTreePath = nodeBase1;
//            }
//            else {
//                string[] strArray = path.Split(new string[1] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
//                TreeViewNodeCollection nodes = nodeBase1.Nodes;
//                foreach (string x in strArray) {
//                    foreach (TreeNode treeNode in nodes) {
//                        if (treeNode is NodeBase nodeBase2 && cultureIgnoreCase.Compare(x, nodeBase2.Text) == 0) {
//                            nodes = nodeBase2.Nodes;
//                            nodeFromTreePath = nodeBase2;
//                            break;
//                        }
//                    }
//                    if (nodeFromTreePath == null)
//                        break;
//                }
//            }
//            return nodeFromTreePath;
//        }

//        private NodeBase GetRootNode(TreeNode nd) {
//            while (nd.Parent != null)
//                nd = nd.Parent;
//            return nd as NodeBase;
//        }

//        private string GetTreePath(TreeNode nd, DirectoryInfo dir) => this.GetRootNode(nd) is NodeDirectory ? nd.FullPath : dir.FullName;

//        private bool IsDragDropNode(TreeNode nd) {
//            switch (nd) {
//                case null:
//                    return false;
//                case NodeDirectory _:
//                    return true;
//                case NodeDrive _:
//                    return true;
//                default:
//                    return false;
//            }
//        }

//        private void ToolStripLocationsToConfig() {
//            PointConverter pointConverter = new PointConverter();
//            IConfigurationProperty subProperty = this.ConfigurationRoot.GetSubProperty("ToolStrips", true);
//            foreach (ToolStrip toolStrip in this._toolStrips.Values)
//                subProperty.GetSubProperty(toolStrip.Name, true).GetSubProperty("Location", true).Set(pointConverter.ConvertToString((object)toolStrip.Location));
//        }

//        private void UpdateDevices() {
//            if (this.InvokeRequired) {
//                Invoke((Delegate)new ThreadStart(this.UpdateDevices), new object[0]);
//            }
//            else {
//                DriveInfo[] drives = DriveInfo.GetDrives();
//                Dictionary<string, DriveInfo> dictionary = new Dictionary<string, DriveInfo>(drives.Length);
//                foreach (DriveInfo driveInfo in drives)
//                    dictionary.Add(driveInfo.Name, driveInfo);
//                foreach (TreeNode node in this.tvwMain.Nodes) {
//                    if (node is NodeDrive nodeDrive) {
//                        if (dictionary.ContainsKey(nodeDrive.Drive.Name)) {
//                            nodeDrive.RefreshDrive();
//                            dictionary.Remove(nodeDrive.Drive.Name);
//                        }
//                        else
//                            nodeDrive.Remove();
//                    }
//                }
//                foreach (DriveInfo drive in dictionary.Values) {
//                    int index = -1;
//                    foreach (TreeNode node in this.tvwMain.Nodes) {
//                        if (node is NodeDrive nodeDrive) {
//                            if (index == -1)
//                                index = nodeDrive.Index;
//                            if (string.Compare(nodeDrive.Drive.Name, drive.Name, true) < 0)
//                                index = nodeDrive.Index + 1;
//                        }
//                    }
//                    NodeDrive node1 = new NodeDrive(drive);
//                    tvwMain.Nodes.Insert(index, (TreeNodeBase)node1);
//                }
//            }
//        }

//        private void FormMain_Load(object sender, EventArgs e) {
//            if (this.DesignMode)
//                return;
//            FsApp instance = FsApp.Instance;
//            Icon = this.ApplicationInstance.GetAppIcon();
//            _toolStrips = new Dictionary<string, ToolStrip>();
//            CreateMenu();
//            CreateCommandsToolStrips();
//            if (!instance.Options.AppearanceMain.ShowStatusBar)
//                statMain.Visible = false;
//            sptDetailView.BackColor = SystemColors.Control;
//            _currentDetailView = this.detailView1;
//            detailView1.SetActive(true);
//            detailView2.SetActive(false);
//            detailView1.ReadFromConfig(this.ConfigurationRoot.GetSubProperty("DetailView1", true), true);
//            detailView2.ReadFromConfig(this.ConfigurationRoot.GetSubProperty("DetailView2", true), false);
//            tslMainInfo.Text = string.Empty;
//            SizeConverter sizeConverter = new SizeConverter();
//            PointConverter pointConverter = new PointConverter();
//            _size = (Size)sizeConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty("Size", true).ToString(sizeConverter.ConvertToString((object)this.Size)));
//            if (instance.Options.AppearanceMain.RememberSize)
//                Size = this._size;
//            _location = (Point)pointConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty("Location", true).ToString(pointConverter.ConvertToString((object)this.Location)));
//            if (instance.Options.AppearanceMain.RememberLocation)
//                Location = this._location;
//            if (!instance.Options.AppearanceMain.RememberWindowState)
//                return;
//            WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), this.ConfigurationRoot.GetSubProperty("WindowState", true).ToString(this.WindowState.ToString()));
//        }

//        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
//            SizeConverter sizeConverter = new SizeConverter();
//            PointConverter pointConverter = new PointConverter();
//            ConfigurationRoot["Size"].Set(sizeConverter.ConvertToString((object)this._size));
//            ConfigurationRoot["Location"].Set(pointConverter.ConvertToString((object)this._location));
//            ConfigurationRoot["WindowState"].Set(((FormWindowState)(this.WindowState != FormWindowState.Minimized ? (int)this.WindowState : 0)).ToString());
//            ToolStripLocationsToConfig();
//            detailView1.SetToConfig(this.ConfigurationRoot.GetSubProperty("DetailView1", true), true);
//            detailView2.SetToConfig(this.ConfigurationRoot.GetSubProperty("DetailView2", true), false);
//        }

//        private void FormMain_Move(object sender, EventArgs e) {
//            if (this.WindowState != FormWindowState.Normal)
//                return;
//            _location = new Point(this.Location.X, this.Location.Y);
//        }

//        private void FormMain_Resize(object sender, EventArgs e) {
//            int num = this.Width - 50 - this.tslMainInfo.Width;
//            tslDriveInfo.Width = (int)((double)num / 2.4);
//            tslCompleteInfo.Width = (int)((double)num / 3.8);
//            tslSelectionInfo.Width = (int)((double)num / 3.8);
//        }

//        private void FormMain_ResizeEnd(object sender, EventArgs e) {
//            if (this.WindowState != FormWindowState.Normal)
//                return;
//            _size = new Size(this.Size.Width, this.Size.Height);
//        }

//        private void FormMain_Shown(object sender, EventArgs e) {
//            if (this.DesignMode)
//                return;
//            detailView1.Focus();
//        }

//        private void tvwMain_AfterSelect(object sender, TreeViewEventArgs e) {
//            TreeNodeBase node = e.Node as TreeNodeBase;
//            Cursor = Cursors.WaitCursor;
//            FsApp instance = FsApp.Instance;
//            DirectoryInfo directoryInfo = (DirectoryInfo)null;
//            string treePath1 = (string)null;
//            if (node is NodeDirectory nd1) {
//                treePath1 = this.GetTreePath((TreeNode)nd1, nd1.Directory);
//                directoryInfo = nd1.Directory;
//            }
//            if (node is NodeDrive nd2) {
//                treePath1 = this.GetTreePath((TreeNode)nd2, nd2.Drive.RootDirectory);
//                directoryInfo = nd2.Drive.RootDirectory;
//            }
//            if (directoryInfo != null) {
//                e.Node.EnsureVisible();
//                try {
//                    _currentDetailView.SetParentDirectory(directoryInfo, treePath1, false);
//                    instance.ShowInfoMessage(directoryInfo.Name);
//                }
//                catch (Exception ex) {
//                    FormError.ShowException(ex, (IWin32Window)this);
//                    directoryInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
//                    TreeNodeBase nodeFromTreePath = (TreeNodeBase)this.GetNodeFromTreePath(directoryInfo.FullName);
//                    string treePath2 = this.GetTreePath((TreeNode)nodeFromTreePath, directoryInfo);
//                    nodeFromTreePath.EnsureVisible();
//                    tvwMain.SelectedNode = nodeFromTreePath;
//                    _currentDetailView.SetParentDirectory(directoryInfo, treePath2, true);
//                }
//                if (instance.Options.AppearanceMain.FullPathInTitle)
//                    Text = string.Format("{0} - {1}", (object)instance.Information.Title, (object)directoryInfo.FullName);
//                else
//                    Text = instance.Information.Title;
//            }
//            Cursor = Cursors.Default;
//        }

//        private void tvwMain_Enter(object sender, EventArgs e) {
//        }

//        private void tvwMain_DragOver(object sender, DragEventArgs e) {
//            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) {
//                e.Effect = DragDropEffects.None;
//            }
//            else {
//                TreeViewHitTestInfo treeViewHitTestInfo = this.tvwMain.HitTest(this.tvwMain.PointToClient(new Point(e.X, e.Y)));
//                NodeBase node = treeViewHitTestInfo.Node as NodeBase;
//                if (!this.IsDragDropNode(treeViewHitTestInfo.Node)) {
//                    ClearDragDropNode();
//                    e.Effect = DragDropEffects.None;
//                }
//                else {
//                    e.Effect = (e.KeyState & 8) == 0 || (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.None ? ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.None ? ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.None ? DragDropEffects.None : DragDropEffects.Copy) : DragDropEffects.Move) : DragDropEffects.Copy;
//                    if (e.Effect == DragDropEffects.None || node == this._dragOverNode)
//                        return;
//                    ClearDragDropNode();
//                    _dragOverNode = (TreeNode)node;
//                    _dragOverNode.BackColor = System.Drawing.Color.LightGray;
//                }
//            }
//        }

//        private void tvwMain_DragDrop(object sender, DragEventArgs e) {
//            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
//                return;
//            TreeViewHitTestInfo treeViewHitTestInfo = this.tvwMain.HitTest(this.tvwMain.PointToClient(new Point(e.X, e.Y)));
//            NodeBase node = treeViewHitTestInfo.Node as NodeBase;
//            if (!this.IsDragDropNode(treeViewHitTestInfo.Node)) {
//                ClearDragDropNode();
//            }
//            else {
//                string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
//                List<FileSystemInfo> fileSystemInfoList = new List<FileSystemInfo>(data.Length);
//                foreach (string str in data) {
//                    if (File.Exists(str))
//                        fileSystemInfoList.Add((FileSystemInfo)new FileInfo(str));
//                    else if (Directory.Exists(str))
//                        fileSystemInfoList.Add((FileSystemInfo)new DirectoryInfo(str));
//                }
//                FormCopy formCopy = new FormCopy();
//                formCopy.Sources = (IList<FileSystemInfo>)fileSystemInfoList;
//                if (node is NodeDirectory)
//                    formCopy.Destination = ((NodeDirectory)node).Directory;
//                else if (node is NodeDrive)
//                    formCopy.Destination = ((NodeDrive)node).Drive.RootDirectory;
//                if (e.Effect == DragDropEffects.Move)
//                    formCopy.MoveItems = true;
//                int num = (int)formCopy.ShowDialog((IWin32Window)this.FindForm());
//                ClearDragDropNode();
//            }
//        }

//        private void tvwMain_DragLeave(object sender, EventArgs e) => this.ClearDragDropNode();

//        private void tvwMain_Leave(object sender, EventArgs e) {
//        }

//        private void tvwMain_ItemDrag(object sender, ItemDragEventArgs e) {
//            if (e.Button != MouseButtons.Left || !(e.Item is NodeDirectory nodeDirectory))
//                return;
//            string[] data = new string[1]
//            {
//        nodeDirectory.Directory.FullName
//            };
//            int num = (int)this.DoDragDrop((object)new DataObject(DataFormats.FileDrop, (object)data), DragDropEffects.Copy | DragDropEffects.Move);
//        }

//        private void tvwMain_KeyDown(object sender, KeyEventArgs e) {
//            e.SuppressKeyPress = true;
//            if (e.KeyCode == Keys.Delete) {
//                if (this.tvwMain.SelectedNode is NodeDirectory selectedNode)
//                    FileHelper.DeleteDirectory(selectedNode.Directory.FullName, !e.Shift);
//            }
//            else
//                e.SuppressKeyPress = false;
//            e.Handled = e.SuppressKeyPress;
//        }

//        private void detailView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e) {
//            if (!FsApp.Instance.Options.DetailView.SynchronizeColumnWidths)
//                return;
//            if ((DetailView)sender == this.detailView1)
//                detailView2.SetColumnWidth(e.Column.Name, e.Column.Width);
//            else
//                detailView1.SetColumnWidth(e.Column.Name, e.Column.Width);
//        }

//        private void detailView_RequestParentDirectory(object sender, DetailViewRequestDirectoryArgs e) {
//            DetailView detailView = (DetailView)sender;
//            NodeBase nd = (NodeBase)null;
//            if (!string.IsNullOrEmpty(e.TreePath))
//                nd = this.GetNodeFromTreePath(e.TreePath);
//            if (nd == null && !string.IsNullOrEmpty(detailView.TreePath)) {
//                NodeBase nodeFromTreePath = this.GetNodeFromTreePath(detailView.TreePath);
//                if (nodeFromTreePath == null)
//                    return;
//                foreach (NodeBase node in nodeFromTreePath.Nodes) {
//                    if (node is NodeDirectory nodeDirectory && e.Directory.FullName == nodeDirectory.Directory.FullName) {
//                        nd = (NodeBase)nodeDirectory;
//                        break;
//                    }
//                }
//                if (nd == null) {
//                    if (this.tvwMain.SelectedNode.Parent is NodeDirectory parent2 && string.Compare(parent2.Directory.FullName, e.Directory.FullName, true) == 0)
//                        nd = (NodeBase)parent2;
//                    else if (this.tvwMain.SelectedNode.Parent is NodeDrive parent1 && string.Compare(parent1.Drive.RootDirectory.FullName, e.Directory.FullName, true) == 0)
//                        nd = (NodeBase)parent1;
//                }
//            }
//            if (nd == null)
//                nd = this.GetNodeFromTreePath(e.Directory.FullName);
//            if (nd == null)
//                return;
//            if (detailView == this._currentDetailView) {
//                nd.EnsureVisible();
//                tvwMain.SelectedNode = (TreeNodeBase)nd;
//            }
//            else {
//                DirectoryInfo directoryInfo = e.Directory;
//                if (directoryInfo == null) {
//                    if (nd is NodeDrive nodeDrive)
//                        directoryInfo = nodeDrive.Drive.RootDirectory;
//                    if (nd is NodeDirectory nodeDirectory)
//                        directoryInfo = nodeDirectory.Directory;
//                }
//                string treePath = this.GetTreePath((TreeNode)nd, directoryInfo);
//                detailView.SetParentDirectory(directoryInfo, treePath, false);
//            }
//        }

//        private void detailView_Enter(object sender, EventArgs e) {
//            DetailView detailView = (DetailView)sender;
//            if (detailView == this._currentDetailView)
//                return;
//            _currentDetailView.SetActive(false);
//            detailView.SetActive(true);
//            _currentDetailView = detailView;
//            if (string.IsNullOrEmpty(this._currentDetailView.TreePath))
//                return;
//            NodeBase nodeFromTreePath = this.GetNodeFromTreePath(this._currentDetailView.TreePath);
//            if (nodeFromTreePath == null)
//                return;
//            nodeFromTreePath.EnsureVisible();
//            tvwMain.SelectedNode = (TreeNodeBase)nodeFromTreePath;
//        }

//        private void detailView_SelectionChanged(object sender, EventArgs e) {
//            DetailView detailView = (DetailView)sender;
//            if (detailView != this._currentDetailView)
//                return;
//            if (this.CurrentPreview != null) {
//                FileSystemInfo currentFsi = detailView.CurrentFSI;
//                if (currentFsi != null)
//                    CurrentPreview.SetFile(currentFsi.FullName);
//            }
//            Match match = new Regex("^[a-zA-Z]:\\\\").Match(detailView.ParentDirectory.FullName);
//            if (match != null && !string.IsNullOrEmpty(match.Value)) {
//                DriveInfo driveInfo = new DriveInfo(match.Value);
//                long num = driveInfo.TotalSize - driveInfo.TotalFreeSpace;
//                long totalFreeSpace = driveInfo.TotalFreeSpace;
//                tslDriveInfo.Text = string.Format("{0} used / {1} free", (object)num.ToString("#,##0"), (object)totalFreeSpace.ToString("#,##0"));
//            }
//            uint completeCount = detailView.Infos.CompleteCount;
//            ulong completeSize = detailView.Infos.CompleteSize;
//            tslCompleteInfo.Text = string.Format("{0} Items {1}", (object)completeCount.ToString("#,##0"), (object)completeSize.ToString("#,##0"));
//            uint selectedCount = detailView.Infos.SelectedCount;
//            ulong selectedSize = detailView.Infos.SelectedSize;
//            tslSelectionInfo.Text = string.Format("{0} Items {1}", (object)selectedCount.ToString("#,##0"), (object)selectedSize.ToString("#,##0"));
//        }

//        private void sptPanel_Enter(object sender, EventArgs e) => ((Control)sender).BackColor = System.Drawing.Color.SkyBlue;

//        private void sptPanel_Leave(object sender, EventArgs e) => ((Control)sender).BackColor = SystemColors.Control;

//        protected override void Dispose(bool disposing) {
//            if (disposing && this.components != null)
//                components.Dispose();
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent() {
//            this.statMain = new StatusStrip();
//            this.tslMainInfo = new ToolStripStatusLabel();
//            this.tslDriveInfo = new ToolStripStatusLabel();
//            this.tslCompleteInfo = new ToolStripStatusLabel();
//            this.tslSelectionInfo = new ToolStripStatusLabel();
//            this.tscMain = new ToolStripContainer();
//            this.sptMain = new SplitContainer();
//            this.tvwMain = new TreeMain();
//            this.tscDetailView = new ToolStripContainer();
//            this.sptDetailView = new SplitContainer();
//            this.detailView1 = new DetailView();
//            this.detailView2 = new DetailView();
//            this.tsMain = new ToolStrip();
//            this.statMain.SuspendLayout();
//            this.tscMain.ContentPanel.SuspendLayout();
//            this.tscMain.TopToolStripPanel.SuspendLayout();
//            this.tscMain.SuspendLayout();
//            this.sptMain.Panel1.SuspendLayout();
//            this.sptMain.Panel2.SuspendLayout();
//            this.sptMain.SuspendLayout();
//            this.tscDetailView.ContentPanel.SuspendLayout();
//            this.tscDetailView.SuspendLayout();
//            this.sptDetailView.Panel1.SuspendLayout();
//            this.sptDetailView.Panel2.SuspendLayout();
//            this.sptDetailView.SuspendLayout();
//            this.SuspendLayout();
//            this.statMain.Items.AddRange(new ToolStripItem[4]
//            {
//        (ToolStripItem) this.tslMainInfo,
//        (ToolStripItem) this.tslDriveInfo,
//        (ToolStripItem) this.tslCompleteInfo,
//        (ToolStripItem) this.tslSelectionInfo
//            });
//            this.statMain.Location = new Point(0, 557);
//            this.statMain.Name = "statMain";
//            this.statMain.ShowItemToolTips = true;
//            this.statMain.Size = new Size(909, 22);
//            this.statMain.TabIndex = 0;
//            this.statMain.Text = "statusStrip1";
//            this.tslMainInfo.AutoSize = false;
//            this.tslMainInfo.Name = "tslMainInfo";
//            this.tslMainInfo.Size = new Size(225, 17);
//            this.tslMainInfo.Text = "tslMainInfo";
//            this.tslMainInfo.TextAlign = ContentAlignment.MiddleLeft;
//            this.tslDriveInfo.AutoSize = false;
//            this.tslDriveInfo.Name = "tslDriveInfo";
//            this.tslDriveInfo.Size = new Size(150, 17);
//            this.tslDriveInfo.Text = "tslDriveInfo";
//            this.tslDriveInfo.TextAlign = ContentAlignment.MiddleLeft;
//            this.tslCompleteInfo.AutoSize = false;
//            this.tslCompleteInfo.Name = "tslCompleteInfo";
//            this.tslCompleteInfo.Size = new Size(150, 17);
//            this.tslCompleteInfo.Text = "tslCompleteInfo";
//            this.tslCompleteInfo.TextAlign = ContentAlignment.MiddleLeft;
//            this.tslSelectionInfo.AutoSize = false;
//            this.tslSelectionInfo.Name = "tslSelectionInfo";
//            this.tslSelectionInfo.Size = new Size(150, 17);
//            this.tslSelectionInfo.Text = "tslSelectionInfo";
//            this.tslSelectionInfo.TextAlign = ContentAlignment.MiddleLeft;
//            this.tscMain.ContentPanel.Controls.Add((Control)this.sptMain);
//            this.tscMain.ContentPanel.Size = new Size(909, 532);
//            this.tscMain.Dock = DockStyle.Fill;
//            this.tscMain.Location = new Point(0, 0);
//            this.tscMain.Name = "tscMain";
//            this.tscMain.Size = new Size(909, 557);
//            this.tscMain.TabIndex = 2;
//            this.tscMain.Text = "toolStripContainer1";
//            this.tscMain.TopToolStripPanel.Controls.Add((Control)this.tsMain);
//            this.sptMain.Dock = DockStyle.Fill;
//            this.sptMain.Location = new Point(0, 0);
//            this.sptMain.Name = "sptMain";
//            this.sptMain.Panel1.Controls.Add((Control)this.tvwMain);
//            this.sptMain.Panel2.Controls.Add((Control)this.tscDetailView);
//            this.sptMain.Size = new Size(909, 532);
//            this.sptMain.SplitterDistance = 274;
//            this.sptMain.TabIndex = 0;
//            this.sptMain.TabStop = false;
//            this.tvwMain.AllowDrop = true;
//            this.tvwMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
//            this.tvwMain.HideSelection = false;
//            this.tvwMain.Location = new Point(3, 3);
//            this.tvwMain.Name = "tvwMain";
//            this.tvwMain.RightClickSelect = false;
//            this.tvwMain.SelectedNode = (TreeNodeBase)null;
//            this.tvwMain.Size = new Size(268, 526);
//            this.tvwMain.TabIndex = 0;
//            this.tscDetailView.ContentPanel.Controls.Add((Control)this.sptDetailView);
//            this.tscDetailView.ContentPanel.Size = new Size(631, 532);
//            this.tscDetailView.Dock = DockStyle.Fill;
//            this.tscDetailView.Location = new Point(0, 0);
//            this.tscDetailView.Name = "tscDetailView";
//            this.tscDetailView.Size = new Size(631, 532);
//            this.tscDetailView.TabIndex = 0;
//            this.tscDetailView.Text = "toolStripContainer1";
//            this.tscDetailView.TopToolStripPanelVisible = false;
//            this.sptDetailView.BackColor = SystemColors.ControlDark;
//            this.sptDetailView.Dock = DockStyle.Fill;
//            this.sptDetailView.Location = new Point(0, 0);
//            this.sptDetailView.Name = "sptDetailView";
//            this.sptDetailView.Orientation = Orientation.Horizontal;
//            this.sptDetailView.Panel1.Controls.Add((Control)this.detailView1);
//            this.sptDetailView.Panel2.Controls.Add((Control)this.detailView2);
//            this.sptDetailView.Size = new Size(631, 532);
//            this.sptDetailView.SplitterDistance = 297;
//            this.sptDetailView.TabIndex = 1;
//            this.sptDetailView.TabStop = false;
//            this.detailView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
//            this.detailView1.BackColor = SystemColors.Control;
//            this.detailView1.Location = new Point(3, 3);
//            this.detailView1.Name = "detailView1";
//            this.detailView1.Size = new Size(625, 291);
//            this.detailView1.TabIndex = 0;
//            this.detailView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
//            this.detailView2.BackColor = SystemColors.Control;
//            this.detailView2.Location = new Point(3, 3);
//            this.detailView2.Name = "detailView2";
//            this.detailView2.Size = new Size(625, 225);
//            this.detailView2.TabIndex = 1;
//            this.tsMain.Dock = DockStyle.None;
//            this.tsMain.Location = new Point(3, 0);
//            this.tsMain.Name = "tsMain";
//            this.tsMain.Size = new Size(111, 25);
//            this.tsMain.TabIndex = 3;
//            this.tsMain.Text = "toolStrip1";
//            this.AutoScaleDimensions = new SizeF(6f, 13f);
//            this.AutoScaleMode = AutoScaleMode.Font;
//            this.ClientSize = new Size(909, 579);
//            this.Controls.Add((Control)this.tscMain);
//            this.Controls.Add((Control)this.statMain);
//            this.Name = "FormMain";
//            this.Text = "FS Dog";
//            this.statMain.ResumeLayout(false);
//            this.statMain.PerformLayout();
//            this.tscMain.ContentPanel.ResumeLayout(false);
//            this.tscMain.TopToolStripPanel.ResumeLayout(false);
//            this.tscMain.TopToolStripPanel.PerformLayout();
//            this.tscMain.ResumeLayout(false);
//            this.tscMain.PerformLayout();
//            this.sptMain.Panel1.ResumeLayout(false);
//            this.sptMain.Panel2.ResumeLayout(false);
//            this.sptMain.ResumeLayout(false);
//            this.tscDetailView.ContentPanel.ResumeLayout(false);
//            this.tscDetailView.ResumeLayout(false);
//            this.tscDetailView.PerformLayout();
//            this.sptDetailView.Panel1.ResumeLayout(false);
//            this.sptDetailView.Panel2.ResumeLayout(false);
//            this.sptDetailView.ResumeLayout(false);
//            this.ResumeLayout(false);
//            this.PerformLayout();
//        }
//    }
//}
