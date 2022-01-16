// Decompiled with JetBrains decompiler
// Type: FsDog.DetailView
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Collections;
using FR.Configuration;
using FR.IO;
using FR.Windows.Forms;
using FsDog.Commands;
using FsDog.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FsDog.Detail {
    public class DetailView : UserControl {
        private const int HISTORY_NONE = 0;
        private const int HISTORY_BACK = 1;
        private const int HISTORY_FORWARD = 2;
        private const int DROP_ALT = 32;
        private const int DROP_CTRL = 8;
        private const int DROP_SHIFT = 4;
        private List<ShellItem> _shellItems;
        private DetailTable _files;
        private FileSystemWatcher _fsw;
        private Dictionary<string, int> _columnWidths;
        private DirectoryInfo _previousParent;
        private string _editItemFullName;
        private List<string> _historyBack;
        private List<string> _historyForward;
        private int _historyDirection;
        private SynchronizationContext _syncCtx;
        private DataGridViewRow _dragOverRow;
        private bool _isInvoking;
        private IContainer components;
        private Panel panel1;
        private DetailGrid dgvFiles;
        private RichTextBox txtSearch;
        private ComboBox cboPath;
        private ContextMenuStrip ctxCboPath;
        private ToolStripMenuItem ctxCboPathAddToFavorites;

        public DetailView() {
            this.InitializeComponent();
            this.txtSearch.KeyDown += new KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.PreviewKeyDown += new PreviewKeyDownEventHandler(this.txtSearch_PreviewKeyDown);
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);
            this.cboPath.KeyDown += new KeyEventHandler(this.cboPath_KeyDown);
            this.ctxCboPathAddToFavorites.Click += new EventHandler(this.ctxCboPathAddToFavorites_Click);
            this.dgvFiles.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvFiles_CellDoubleClick);
            this.dgvFiles.CellEndEdit += new DataGridViewCellEventHandler(this.dgvFiles_CellEndEdit);
            this.dgvFiles.CellValidating += new DataGridViewCellValidatingEventHandler(this.dgvFiles_CellValidating);
            this.dgvFiles.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dgvFiles_ColumnWidthChanged);
            this.dgvFiles.DragDrop += new DragEventHandler(this.dgvFiles_DragDrop);
            this.dgvFiles.DragEnter += new DragEventHandler(this.dgvFiles_DragEnter);
            this.dgvFiles.DragLeave += new EventHandler(this.dgvFiles_DragLeave);
            this.dgvFiles.DragOver += new DragEventHandler(this.dgvFiles_DragOver);
            this.dgvFiles.HandleMessage += new MessageHandler(this.dgvFiles_HandleMessage);
            this.dgvFiles.MouseDown += new MouseEventHandler(this.dgvFiles_MouseDown);
            this.dgvFiles.KeyDown += new KeyEventHandler(this.dgvFiles_KeyDown);
            this.dgvFiles.KeyPress += new KeyPressEventHandler(this.dgvFiles_KeyPress);
            this.dgvFiles.SelectionChanged += new EventHandler(this.dgvFiles_SelectionChanged);
        }

        public event DetailViewRequestParentDirectoryHandler RequestParentDirectory;

        public event DataGridViewColumnEventHandler ColumnWidthChanged;

        public event EventHandler SelectionChanged;

        public FileSystemInfo CurrentFSI => this.dgvFiles.CurrentRow == null || this.dgvFiles.CurrentRow.Index < 0 ? (FileSystemInfo)null : this.GetDetailItem(this.dgvFiles.CurrentRow).FileSystemInfo;

        [Browsable(false)]
        public string TreePath { get; private set; }

        [Browsable(false)]
        public DetailView.DetailViewInfos Infos { get; private set; }

        [Browsable(false)]
        public DirectoryInfo ParentDirectory { get; private set; }

        public void BeginEditItem(string fullName) {
            bool flag = false;
            foreach (DataGridViewRow row in (IEnumerable)this.dgvFiles.Rows) {
                if (this.GetDetailItem(row).FileSystemInfo.FullName == fullName) {
                    flag = true;
                    this.dgvFiles.CurrentCell = row.Cells["Name"];
                    this.dgvFiles.BeginEdit(true);
                    break;
                }
            }
            if (!flag)
                this._editItemFullName = fullName;
            else
                this._editItemFullName = (string)null;
        }

        public void InvertSelection() {
            foreach (DataGridViewRow row in (IEnumerable)this.dgvFiles.Rows)
                row.Selected = !row.Selected;
        }

        public void GetDirectorySizes() {
            foreach (ShellItem shellItem in this._shellItems) {
                if (shellItem is ShellDirectory shellDirectory)
                    shellDirectory.BeginGetSize();
            }
        }

        public List<FileSystemInfo> GetSelectedSystemInfos() {
            List<FileSystemInfo> selectedSystemInfos = new List<FileSystemInfo>();
            foreach (DataGridViewRow selectedRow in (BaseCollection)this.dgvFiles.SelectedRows) {
                FileSystemInfo fileSystemInfo = this.GetDetailItem(selectedRow).FileSystemInfo;
                selectedSystemInfos.Add(fileSystemInfo);
            }
            return selectedSystemInfos;
        }

        public void ReadFromConfig(IConfigurationProperty root, bool isFirst) {
            FsApp instance = FsApp.Instance;
            foreach (IConfigurationProperty subProperty in root.GetSubProperty("Columns", true).GetSubProperties("Column"))
                this._columnWidths.Add(subProperty["Name"].ToString(), subProperty["Width"].ToInt32());
            if (this._columnWidths.Count == 0) {
                this._columnWidths.Add("Image", 24);
                this._columnWidths.Add("Name", 300);
                this._columnWidths.Add("Size", 150);
                this._columnWidths.Add("TypeName", 150);
                this._columnWidths.Add("DateModified", 150);
            }
            try {
                string treePath = root.GetSubProperty("Path", true).ToString("");
                if (!string.IsNullOrEmpty(treePath) && treePath.StartsWith("\\\\") && instance.Options.General.RestoreNetworkDirectories)
                    this.OnRequestParentDirectory(treePath);
                else if (!string.IsNullOrEmpty(treePath) && instance.Options.General.RestoreDirectories)
                    this.OnRequestParentDirectory(treePath);
                else
                    this.OnRequestParentDirectory(instance.DefaultDirectoryName);
            }
            catch (Exception ex) {
                FormError.ShowException(ex, (IWin32Window)this.FindForm());
                this.OnRequestParentDirectory(instance.DefaultDirectoryName);
            }
        }

        public override void Refresh() {
            base.Refresh();
            this.SetParentDirectory(this.ParentDirectory, this.TreePath, true);
        }

        public void SelectAll() {
            if (this.ActiveControl == this.txtSearch)
                this.txtSearch.SelectAll();
            else if (this.ActiveControl == this.cboPath) {
                this.cboPath.SelectAll();
            }
            else {
                if (this.ActiveControl != this.dgvFiles)
                    return;
                this.dgvFiles.SelectAll();
            }
        }

        public void SetActive(bool active) {
            FsApp instance = FsApp.Instance;
            if (active) {
                this.txtSearch.BackColor = instance.Options.AppearanceFileView.ActiveBackgroundColor;
                this.txtSearch.ForeColor = instance.Options.AppearanceFileView.ActiveForeColor;
                this.cboPath.BackColor = instance.Options.AppearanceFileView.ActiveBackgroundColor;
                this.cboPath.ForeColor = instance.Options.AppearanceFileView.ActiveForeColor;
            }
            else {
                this.txtSearch.BackColor = instance.Options.AppearanceFileView.InactiveBackgroundColor;
                this.txtSearch.ForeColor = instance.Options.AppearanceFileView.InactiveForeColor;
                this.cboPath.BackColor = instance.Options.AppearanceFileView.InactiveBackgroundColor;
                this.cboPath.ForeColor = instance.Options.AppearanceFileView.InactiveForeColor;
            }
        }

        public void SetColumnWidth(string name, int width) {
            if (this._columnWidths.ContainsKey(name))
                this._columnWidths.Remove(name);
            this._columnWidths.Add(name, width);
            this.dgvFiles.Columns[name].Width = width;
        }

        public void SetParentDirectory(DirectoryInfo parent, string treePath, bool force) {
            if (this.ParentDirectory != null && parent != null && this.ParentDirectory.FullName == parent.FullName && !force) {
                this.TreePath = treePath;
            }
            else {
                FsApp instance = FsApp.Instance;
                string treePath1 = this.TreePath;
                this._previousParent = this.ParentDirectory;
                this.ParentDirectory = parent;
                this.TreePath = treePath;
                Dictionary<string, string> dictionary = (Dictionary<string, string>)null;
                this.dgvFiles.SuspendLayout();
                this._files.BeginLoadData();
                SortOrder sortOrder = this.dgvFiles.SortOrder;
                foreach (ShellItem shellItem in this._shellItems) {
                    lock (shellItem) {
                        if (shellItem is ShellDirectory shellDirectory)
                            shellDirectory.GetSizeEnd -= new ShellDirectorySizeEndHandler(this.ShellDirectory_GetSizeEnd);
                    }
                }
                this._shellItems.Clear();
                string sortColumn = this.dgvFiles.SortedColumn == null ? (string)null : this.dgvFiles.SortedColumn.DataPropertyName;
                this.Infos.CompleteCount = 0U;
                this.Infos.CompleteSize = 0UL;
                this.dgvFiles.DataSource = (object)null;
                this._files.Rows.Clear();
                this.txtSearch.Clear();
                if (this._fsw != null) {
                    this._fsw.Changed -= new FileSystemEventHandler(this.FileSystemWatcher_Changed);
                    this._fsw.Created -= new FileSystemEventHandler(this.FileSystemWatcher_Changed);
                    this._fsw.Deleted -= new FileSystemEventHandler(this.FileSystemWatcher_Changed);
                    this._fsw.Renamed -= new RenamedEventHandler(this.FileSystemWatcher_Renamed);
                    while (this._isInvoking)
                        Application.DoEvents();
                    this._fsw.Dispose();
                    this._fsw = (FileSystemWatcher)null;
                }
                if (this.ParentDirectory != null) {
                    this.cboPath.Text = this.TreePath;
                    if (this._previousParent != null && this._previousParent.FullName == this.ParentDirectory.FullName) {
                        dictionary = new Dictionary<string, string>();
                        foreach (DataGridViewRow selectedRow in (BaseCollection)this.dgvFiles.SelectedRows) {
                            DetailItem detailItem = this.GetDetailItem(selectedRow);
                            dictionary.Add(detailItem.FileSystemInfo.Name, detailItem.FileSystemInfo.Name);
                        }
                    }
                    foreach (DirectoryInfo subDirectory in instance.GetSubDirectories(this.ParentDirectory)) {
                        DetailItem detailItem = this._files.Add(subDirectory);
                        ShellDirectory shellDirectory = new ShellDirectory(subDirectory.FullName);
                        shellDirectory.GetSizeEnd += new ShellDirectorySizeEndHandler(this.ShellDirectory_GetSizeEnd);
                        this._shellItems.Add((ShellItem)shellDirectory);
                        if (detailItem != null)
                            ++this.Infos.CompleteCount;
                    }
                    foreach (FileInfo file in instance.GetFiles(this.ParentDirectory)) {
                        DetailItem detailItem = this._files.Add(file);
                        this._shellItems.Add((ShellItem)new ShellFile(file.FullName));
                        if (detailItem != null && detailItem.Size is ulong) {
                            this.Infos.CompleteSize += (ulong)detailItem.Size;
                            ++this.Infos.CompleteCount;
                        }
                    }
                    this._fsw = new FileSystemWatcher();
                    this._fsw.IncludeSubdirectories = false;
                    this._fsw.Path = this.ParentDirectory.FullName;
                    this._fsw.Changed += new FileSystemEventHandler(this.FileSystemWatcher_Changed);
                    this._fsw.Created += new FileSystemEventHandler(this.FileSystemWatcher_Changed);
                    this._fsw.Deleted += new FileSystemEventHandler(this.FileSystemWatcher_Changed);
                    this._fsw.Renamed += new RenamedEventHandler(this.FileSystemWatcher_Renamed);
                    this._fsw.EnableRaisingEvents = true;
                }
                this._files.EndLoadData();
                //this.dgvFiles.AutoGenerateColumns = false;
                this.dgvFiles.DataSource = (object)new DetailTableView(this._files, sortColumn, sortOrder);
                //this.dgvFiles.Columns.Clear();

                //foreach (DataColumn column in (InternalDataCollectionBase)this._files.Columns) {
                //    DataGridViewColumn gridColumn = GridColumnFactory.CreateGridColumn(column);
                //    if (gridColumn != null) {
                //        if (this._columnWidths.TryGetValue(column.ColumnName, out int num))
                //            gridColumn.Width = num;
                //        this.dgvFiles.Columns.Add(gridColumn);
                //    }
                //}
                if (dictionary != null) {
                    this.dgvFiles.ClearSelection();
                    foreach (DataGridViewRow row in (IEnumerable)this.dgvFiles.Rows) {
                        DetailItem detailItem = this.GetDetailItem(row);
                        if (dictionary.ContainsKey(detailItem.FileSystemInfo.Name))
                            row.Selected = true;
                    }
                }
                this.dgvFiles.ResumeLayout();
                if (this._previousParent != null) {
                    foreach (DataGridViewRow row in (IEnumerable)this.dgvFiles.Rows) {
                        DetailItem detailItem = this.GetDetailItem(row);
                        if (detailItem != null && detailItem.DirectoryInfo != null && string.Equals(detailItem.DirectoryInfo.FullName, this._previousParent.FullName)) {
                            this.dgvFiles.Rows[0].Selected = false;
                            row.Selected = true;
                            this.dgvFiles.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
                if (instance.Options.DetailView.ShowDirectorySize)
                    this.GetDirectorySizes();
                if (this._historyDirection == 0) {
                    if (!string.IsNullOrEmpty(treePath1))
                        this._historyBack.Add(treePath1);
                    this._historyForward.Clear();
                }
                else if (this._historyDirection == 1) {
                    if (this._historyBack.Count != 0)
                        this._historyBack.RemoveAt(this._historyBack.Count - 1);
                    if (!string.IsNullOrEmpty(treePath1))
                        this._historyForward.Add(treePath1);
                }
                else if (this._historyDirection == 2) {
                    if (!string.IsNullOrEmpty(treePath1))
                        this._historyBack.Add(treePath1);
                    if (this._historyForward.Count != 0)
                        this._historyForward.RemoveAt(this._historyForward.Count - 1);
                }
                this._previousParent = (DirectoryInfo)null;
            }
        }

        public void SetToConfig(IConfigurationProperty root, bool isFirst) {
            root.GetSubProperty("Path", true).Set(this.TreePath);
            IConfigurationProperty subProperty = root.GetSubProperty("Columns");
            while (subProperty.ExistsSubProperty("Column"))
                subProperty.GetSubProperty("Column").Delete();
            foreach (KeyValuePair<string, int> columnWidth in this._columnWidths) {
                IConfigurationProperty configurationProperty = subProperty.AddSubProperty("Column");
                configurationProperty.GetSubProperty("Name", true).Set(columnWidth.Key);
                configurationProperty.GetSubProperty("Width", true).Set(columnWidth.Value);
            }
        }

        protected override void InitLayout() {
            base.InitLayout();
            if (this.DesignMode)
                return;
            FsApp instance = FsApp.Instance;
            this._syncCtx = SynchronizationContext.Current;
            this.Infos = new DetailView.DetailViewInfos();
            this._historyBack = new List<string>();
            this._historyForward = new List<string>();
            this._shellItems = new List<ShellItem>();
            this._files = new DetailTable();
            this._columnWidths = new Dictionary<string, int>();
            this.BackColor = instance.Options.AppearanceFileView.ActiveBackgroundColor;
            this.dgvFiles.BackgroundColor = instance.Options.AppearanceFileView.ActiveBackgroundColor;

            this.dgvFiles.AutoGenerateColumns = false;

            this.dgvFiles.Columns.Clear();
            void addGridColumn(string columnName, bool show) {
                if (show) dgvFiles.Columns.Add(GridColumnFactory.CreateGridColumn(columnName));
            }

            var app = FsApp.Instance;
            addGridColumn("Image", true);
            addGridColumn("Name", true);
            addGridColumn("Size", true);
            addGridColumn("TypeName", true);
            addGridColumn("DateModified", app.Options.DetailView.ShowModificationDateColumn);
        }

        protected void OnColumnWidthChanged(DataGridViewColumnEventArgs e) {
            if (this.ColumnWidthChanged == null)
                return;
            this.ColumnWidthChanged((object)this, e);
        }

        protected internal void OnRequestParentDirectory(DirectoryInfo dir) {
            if (this.RequestParentDirectory == null)
                return;
            this.RequestParentDirectory((object)this, new DetailViewRequestDirectoryArgs(dir));
        }

        protected internal void OnRequestParentDirectory(string treePath) {
            if (this.RequestParentDirectory == null)
                return;
            this.RequestParentDirectory((object)this, new DetailViewRequestDirectoryArgs(treePath));
        }

        protected void OnSelectionChanged() {
            if (this.SelectionChanged == null)
                return;
            this.SelectionChanged((object)this, EventArgs.Empty);
        }

        private void ClearDragOverRow() {
            if (this._dragOverRow == null)
                return;
            this._dragOverRow.DefaultCellStyle.BackColor = this.dgvFiles.DefaultCellStyle.BackColor;
            this._dragOverRow = (DataGridViewRow)null;
        }

        private void CopyFromClipboard() {
            List<ShellItem> items = new List<ShellItem>();
            DragDropEffects filesFromClipboard = FileHelper.GetFilesFromClipboard(ref items);
            if (items.Count == 0 || filesFromClipboard == DragDropEffects.None)
                return;
            List<string> stringList = new List<string>(items.Count);
            foreach (ShellItem shellItem in items)
                stringList.Add(shellItem.FullName);
            if ((filesFromClipboard & DragDropEffects.Move) == DragDropEffects.Move)
                FileHelper.MoveTo(this.FindForm().Handle, stringList.ToArray(), this.ParentDirectory.FullName, false);
            else
                FileHelper.CopyTo(this.FindForm().Handle, stringList.ToArray(), this.ParentDirectory.FullName, false);
        }

        private void CopyToClipboard(bool cut) {
            List<FileSystemInfo> selectedSystemInfos = this.GetSelectedSystemInfos();
            if (cut)
                FileHelper.CopyToClipboard(selectedSystemInfos.ToArray(), DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);
            else
                FileHelper.CopyToClipboard(selectedSystemInfos.ToArray(), DragDropEffects.Copy | DragDropEffects.Link);
        }

        public void DeleteSelected(bool bin) {
            List<FileSystemInfo> selectedSystemInfos = this.GetSelectedSystemInfos();
            List<string> stringList = new List<string>(selectedSystemInfos.Count);
            foreach (FileSystemInfo fileSystemInfo in selectedSystemInfos)
                stringList.Add(fileSystemInfo.FullName);
            FileHelper.Delete(this.FindForm().Handle, stringList.ToArray(), bin, false);
        }

        private void EnsureVisibleRowIndex(int rowIndex) {
            if (rowIndex <= this.dgvFiles.FirstDisplayedCell.RowIndex)
                return;
            for (DataGridViewCell firstDisplayedCell = this.dgvFiles.FirstDisplayedCell; rowIndex > firstDisplayedCell.RowIndex + this.dgvFiles.DisplayedRowCount(false); firstDisplayedCell = this.dgvFiles.FirstDisplayedCell)
                this.dgvFiles.FirstDisplayedCell = this.dgvFiles.Rows[firstDisplayedCell.RowIndex + 1].Cells[0];
        }

        private DetailItem GetDetailItem(DataGridViewRow gridRow) => (gridRow.DataBoundItem as DataRowView).Row as DetailItem;

        private bool HandleKeyDown(KeyEventArgs e) {
            bool flag = true;
            if (e.KeyCode == Keys.End) {
                if (this.dgvFiles.Rows.Count != 0) {
                    this.dgvFiles.CurrentCell = this.dgvFiles.Rows[this.dgvFiles.Rows.Count - 1].Cells[0];
                    this.dgvFiles.FirstDisplayedCell = this.dgvFiles.Rows[this.dgvFiles.Rows.Count - 1].Cells[0];
                }
            }
            else if (e.KeyCode == Keys.Return) {
                FsApp.Instance.ExecuteCommand(typeof(CmdFileOpen), (DataContext)null);
                this.dgvFiles.Focus();
            }
            else if (e.KeyCode == Keys.Escape) {
                this.txtSearch.Clear();
                this.dgvFiles.Focus();
            }
            else if (e.KeyCode == Keys.G && e.Control) {
                this.txtSearch.Focus();
                this.txtSearch.SelectionStart = 0;
                this.txtSearch.SelectionLength = this.txtSearch.TextLength;
            }
            else if (e.KeyCode == Keys.Home && !e.Control && !e.Alt && !e.Shift) {
                if (this.dgvFiles.Rows.Count != 0) {
                    this.dgvFiles.CurrentCell = this.dgvFiles.Rows[0].Cells[0];
                    this.dgvFiles.FirstDisplayedCell = this.dgvFiles.Rows[0].Cells[0];
                }
            }
            else if (e.KeyCode == Keys.Left && e.Alt)
                this.HistoryGoBack(true);
            else if (e.KeyCode == Keys.Right && e.Alt)
                this.HistoryGoBack(false);
            else if (e.KeyCode == Keys.Tab)
                ((FormMain)this.FindForm())?.GotoNextControl((Control)this, !e.Shift);
            else
                flag = false;
            return flag;
        }

        private void HistoryGoBack(bool back) {
            if (back) {
                if (this._historyBack.Count == 0)
                    return;
                this._historyDirection = 1;
                this.OnRequestParentDirectory(this._historyBack[this._historyBack.Count - 1]);
            }
            else {
                if (this._historyForward.Count == 0)
                    return;
                this._historyDirection = 2;
                this.OnRequestParentDirectory(this._historyForward[this._historyForward.Count - 1]);
            }
        }

        private void ShowContextMenu(IList rows, Point pt) {
            FileSystemInfo[] fsis;
            if (rows == null) {
                fsis = new FileSystemInfo[1]
                {
          (FileSystemInfo) this.ParentDirectory
                };
            }
            else {
                fsis = new FileSystemInfo[rows.Count];
                for (int index = 0; index < rows.Count; ++index) {
                    DetailItem detailItem = this.GetDetailItem((DataGridViewRow)rows[index]);
                    fsis[index] = detailItem.FileSystemInfo;
                }
            }
            new ShellContextMenu().ShowContextMenu(fsis, pt);
            this.dgvFiles.Focus();
        }

        private void ShellDirectory_GetSizeEnd(object sender, ShellDirectorySizeEndArgs e) {
            if (this.InvokeRequired) {
                this.Invoke((Delegate)new ShellDirectorySizeEndHandler(this.ShellDirectory_GetSizeEnd), sender, (object)e);
            }
            else {
                ShellDirectory shellDirectory = (ShellDirectory)sender;
                DetailItem itemByName = this._files.FindItemByName(shellDirectory.Name);
                if (itemByName != null && itemByName.DirectoryInfo != null && itemByName.DirectoryInfo.FullName == shellDirectory.FullName) {
                    itemByName.Size = (object)e.Size;
                    this.Infos.CompleteSize += e.Size;
                }
                this.OnSelectionChanged();
            }
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e) {
            FsApp instance = FsApp.Instance;
            if (this.InvokeRequired) {
                this._isInvoking = true;
                this.Invoke((Delegate)new FileSystemEventHandler(this.FileSystemWatcher_Changed), sender, (object)e);
                this._isInvoking = false;
            }
            else {
                if (e.ChangeType == WatcherChangeTypes.All) {
                    if (this.ParentDirectory.Exists)
                        this.SetParentDirectory(this.ParentDirectory, this.TreePath, true);
                }
                else if (e.ChangeType == WatcherChangeTypes.Changed) {
                    DetailItem itemByName = this._files.FindItemByName(e.Name);
                    if (itemByName != null) {
                        if (itemByName.Size != null && itemByName.Size is ulong)
                            this.Infos.CompleteSize -= (ulong)itemByName.Size;
                        bool flag = false;
                        if (File.Exists(e.FullPath))
                            flag = this._files.Update(itemByName, new FileInfo(e.FullPath));
                        else if (Directory.Exists(e.FullPath))
                            flag = this._files.Update(itemByName, new DirectoryInfo(e.FullPath));
                        if (flag && itemByName.Size is ulong)
                            this.Infos.CompleteSize += (ulong)itemByName.Size;
                    }
                }
                else if (e.ChangeType == WatcherChangeTypes.Created) {
                    if (File.Exists(e.FullPath)) {
                        FileInfo fileInfo = new FileInfo(e.FullPath);
                        if (instance.IsValidToShow((FileSystemInfo)fileInfo))
                            this.Infos.CompleteSize += (ulong)this._files.Add(fileInfo).Size;
                    }
                    else if (Directory.Exists(e.FullPath)) {
                        DirectoryInfo fsi = new DirectoryInfo(e.FullPath);
                        if (instance.IsValidToShow((FileSystemInfo)fsi))
                            this._files.Add(new DirectoryInfo(e.FullPath));
                    }
                    ++this.Infos.CompleteCount;
                    if (!string.IsNullOrEmpty(this._editItemFullName) && string.Compare(e.FullPath, this._editItemFullName) == 0)
                        this.BeginEditItem(this._editItemFullName);
                }
                else if (e.ChangeType == WatcherChangeTypes.Deleted) {
                    DetailItem itemByName = this._files.FindItemByName(e.Name);
                    if (itemByName != null) {
                        if (itemByName.FileInfo != null && itemByName.Size is ulong)
                            this.Infos.CompleteSize -= (ulong)itemByName.Size;
                        --this.Infos.CompleteCount;
                        itemByName?.Delete();
                    }
                }
                else {
                    int changeType = (int)e.ChangeType;
                }
                this.OnSelectionChanged();
            }
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e) {
            if (this.InvokeRequired) {
                this._isInvoking = true;
                this.Invoke((Delegate)new RenamedEventHandler(this.FileSystemWatcher_Renamed), sender, (object)e);
                this._isInvoking = false;
            }
            else {
                DetailItem itemByName = this._files.FindItemByName(e.OldName);
                if (itemByName == null)
                    return;
                if (File.Exists(e.FullPath)) {
                    this._files.Update(itemByName, new FileInfo(e.FullPath));
                }
                else {
                    if (!Directory.Exists(e.FullPath))
                        return;
                    this._files.Update(itemByName, new DirectoryInfo(e.FullPath));
                }
            }
        }

        private void dgvFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            FsApp.Instance.ExecuteCommand(typeof(CmdFileOpen), (DataContext)null);
        }

        private void dgvFiles_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex != this.dgvFiles.Columns["Name"].Index)
                return;
            DataGridViewRow row = this.dgvFiles.Rows[e.RowIndex];
            DetailItem detailItem = this.GetDetailItem(row);
            DataGridViewCell cell = row.Cells[e.ColumnIndex];
            string path2 = cell.Value as string;
            string name = detailItem.FileSystemInfo.Name;
            if (!(path2 != name))
                return;
            try {
                if (detailItem.FileInfo != null) {
                    string str = Path.Combine(detailItem.FileInfo.DirectoryName, path2);
                    if (!(detailItem.FileInfo.FullName != str))
                        return;
                    detailItem.FileInfo.MoveTo(str);
                    this._files.Update(detailItem, new FileInfo(str));
                }
                else {
                    if (detailItem.DirectoryInfo == null)
                        return;
                    string str = Path.Combine(detailItem.DirectoryInfo.Parent.FullName, path2);
                    if (!(detailItem.DirectoryInfo.FullName != str))
                        return;
                    detailItem.DirectoryInfo.MoveTo(str);
                    this._files.Update(detailItem, new DirectoryInfo(str));
                }
            }
            catch (Exception ex) {
                cell.Value = (object)detailItem.Name;
                FormError.ShowException(ex, (IWin32Window)null);
            }
        }

        private void dgvFiles_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            if (e.RowIndex < 0 || e.ColumnIndex != this.dgvFiles.Columns["Name"].Index)
                return;
            string formattedValue = e.FormattedValue as string;
            if (string.IsNullOrEmpty(formattedValue)) {
                int num = (int)MessageBox.Show("File name cannot be empty", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                e.Cancel = true;
            }
            else {
                DetailItem detailItem = this.GetDetailItem(this.dgvFiles.Rows[e.RowIndex]);
                DetailItem itemByName = this._files.FindItemByName(formattedValue);
                if (itemByName == null || itemByName == detailItem)
                    return;
                int num = (int)MessageBox.Show("Name already exists", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                e.Cancel = true;
            }
        }

        private void dgvFiles_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e) {
            if (this._columnWidths.TryGetValue(e.Column.Name, out int num)) {
                if (num == e.Column.Width)
                    return;
                this._columnWidths.Remove(e.Column.Name);
                this._columnWidths.Add(e.Column.Name, e.Column.Width);
                this.OnColumnWidthChanged(e);
            }
            else
                this._columnWidths.Add(e.Column.Name, e.Column.Width);
        }

        private void dgvFiles_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                return;
            e.Effect = DragDropEffects.None;
        }

        private void dgvFiles_DragDrop(object sender, DragEventArgs e) {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;
            string[] data1 = (string[])e.Data.GetData(DataFormats.FileDrop);
            List<FileSystemInfo> fileSystemInfoList = new List<FileSystemInfo>(data1.Length);
            FormCopy formCopy = new FormCopy();
            string[] strArray = (string[])null;
            if (e.Data.GetDataPresent("FileNameMapW")) {
                MemoryStream data2 = (MemoryStream)e.Data.GetData("FileNameMapW");
                data2.Position = 0L;
                byte[] numArray = new byte[data2.Length];
                data2.Read(numArray, 0, numArray.Length);
                strArray = Encoding.Unicode.GetString(numArray).Split(new char[1], StringSplitOptions.RemoveEmptyEntries);
            }
            for (int index = 0; index < data1.Length; ++index) {
                string str = data1[index];
                FileSystemInfo fileSystemInfo = (FileSystemInfo)null;
                if (File.Exists(str))
                    fileSystemInfo = (FileSystemInfo)new FileInfo(str);
                else if (Directory.Exists(str))
                    fileSystemInfo = (FileSystemInfo)new DirectoryInfo(str);
                if (fileSystemInfo != null) {
                    fileSystemInfoList.Add(fileSystemInfo);
                    if (strArray != null && strArray.Length > index && !formCopy.NameMapping.ContainsKey(fileSystemInfo.FullName))
                        formCopy.NameMapping.Add(fileSystemInfo.FullName, strArray[index]);
                }
            }
            formCopy.Sources = (IList<FileSystemInfo>)fileSystemInfoList;
            if (this._dragOverRow != null) {
                DetailItem detailItem = this.GetDetailItem(this._dragOverRow);
                formCopy.Destination = detailItem.DirectoryInfo ?? this.ParentDirectory;
            }
            else
                formCopy.Destination = this.ParentDirectory;
            if (e.Effect == DragDropEffects.Move)
                formCopy.MoveItems = true;
            int num = (int)formCopy.ShowDialog((IWin32Window)this.FindForm());
            this.ClearDragOverRow();
        }

        private void dgvFiles_DragLeave(object sender, EventArgs e) => this.ClearDragOverRow();

        private void dgvFiles_DragOver(object sender, DragEventArgs e) {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.None;
            }
            else {
                e.Effect = (e.KeyState & 8) == 0 || (e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.None ? ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.None ? ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.None ? DragDropEffects.None : DragDropEffects.Copy) : DragDropEffects.Move) : DragDropEffects.Copy;
                if (e.Effect == DragDropEffects.None)
                    return;
                Point client = this.dgvFiles.PointToClient(new Point(e.X, e.Y));
                DataGridView.HitTestInfo hitTestInfo = this.dgvFiles.HitTest(client.X, client.Y);
                this.ClearDragOverRow();
                if (hitTestInfo.RowIndex < 0)
                    return;
                DataGridViewRow row = this.dgvFiles.Rows[hitTestInfo.RowIndex];
                if (this.GetDetailItem(row).DirectoryInfo == null)
                    return;
                this._dragOverRow = row;
                this._dragOverRow.DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private void dgvFiles_HandleMessage(object sender, ref Message m) {
            if (m.Msg == 769)
                this.CopyToClipboard(false);
            else if (m.Msg == 768) {
                this.CopyToClipboard(true);
            }
            else {
                if (m.Msg != 770)
                    return;
                this.CopyFromClipboard();
            }
        }

        private void dgvFiles_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                DataGridView.HitTestInfo hitTestInfo = this.dgvFiles.HitTest(e.X, e.Y);
                if (hitTestInfo.ColumnIndex != -1 || hitTestInfo.RowIndex < 0)
                    return;
                List<FileSystemInfo> selectedSystemInfos = this.GetSelectedSystemInfos();
                List<string> stringList = new List<string>(selectedSystemInfos.Count);
                foreach (FileSystemInfo fileSystemInfo in selectedSystemInfos)
                    stringList.Add(fileSystemInfo.FullName);
                int num = (int)this.DoDragDrop((object)new DataObject(DataFormats.FileDrop, (object)stringList.ToArray()), DragDropEffects.Copy | DragDropEffects.Move);
            }
            else if (e.Button == MouseButtons.Right) {
                DataGridView.HitTestInfo hitTestInfo = this.dgvFiles.HitTest(e.X, e.Y);
                Point screen = this.dgvFiles.PointToScreen(new Point(e.X, e.Y));
                if (hitTestInfo.RowIndex < 0 || hitTestInfo.ColumnIndex < 0) {
                    this.ShowContextMenu((IList)null, screen);
                }
                else {
                    if (!this.dgvFiles.Rows[hitTestInfo.RowIndex].Selected) {
                        this.dgvFiles.ClearSelection();
                        this.dgvFiles.Rows[hitTestInfo.RowIndex].Selected = true;
                        this.dgvFiles.CurrentCell = this.dgvFiles.Rows[hitTestInfo.RowIndex].Cells[0];
                    }
                    this.ShowContextMenu((IList)new ArrayList()
                    {
            (object) this.dgvFiles.Rows[hitTestInfo.RowIndex]
          }, screen);
                }
            }
            else if (e.Button == MouseButtons.XButton1) {
                this.HistoryGoBack(true);
            }
            else {
                if (e.Button != MouseButtons.XButton2)
                    return;
                this.HistoryGoBack(false);
            }
        }

        private void dgvFiles_KeyDown(object sender, KeyEventArgs e) {
            if (this.dgvFiles.EditingControl == null) {
                e.SuppressKeyPress = true;
                if (!this.HandleKeyDown(e)) {
                    if (e.KeyCode == Keys.A && e.Control)
                        this.dgvFiles.SelectAll();
                    else if (e.KeyCode == Keys.Apps) {
                        Rectangle displayRectangle = this.dgvFiles.GetCellDisplayRectangle(this.dgvFiles.CurrentCell.ColumnIndex, this.dgvFiles.CurrentCell.RowIndex, true);
                        Point screen = this.dgvFiles.PointToScreen(new Point(displayRectangle.Location.X, displayRectangle.Location.Y + displayRectangle.Height));
                        ArrayList rows = new ArrayList(this.dgvFiles.SelectedRows.Count);
                        rows.AddRange((ICollection)this.dgvFiles.SelectedRows);
                        this.ShowContextMenu((IList)rows, screen);
                    }
                    else if (e.KeyCode == Keys.Back) {
                        if (this.txtSearch.TextLength != 0)
                            SendMessageHelper.SendKeyDown((Control)this.txtSearch, e.KeyCode);
                        else if (this.ParentDirectory.Parent != null)
                            this.OnRequestParentDirectory(this.ParentDirectory.Parent);
                    }
                    else if (e.KeyCode == Keys.C && e.Control && !e.Shift)
                        this.CopyToClipboard(false);
                    else if (e.KeyCode == Keys.Delete)
                        this.DeleteSelected(!e.Shift);
                    else if (e.KeyCode == Keys.F2) {
                        if (this.dgvFiles.SelectedRows.Count > 1)
                            FsApp.Instance.ExecuteCommand(typeof(CmdFileRenameMulti), (DataContext)null);
                        else if (this.dgvFiles.CurrentRow != null) {
                            this.dgvFiles.CurrentCell = this.dgvFiles.CurrentRow.Cells["Name"];
                            this.dgvFiles.BeginEdit(false);
                        }
                    }
                    else if (e.KeyCode == Keys.Tab && !e.Shift)
                        this.FindForm().SelectNextControl((Control)this, true, true, false, false);
                    else if (e.KeyCode == Keys.Tab && e.Shift)
                        this.FindForm().SelectNextControl((Control)this, false, true, false, false);
                    else if (e.KeyCode == Keys.V && e.Control)
                        this.CopyFromClipboard();
                    else if (e.KeyCode == Keys.X && e.Control && !e.Shift)
                        this.CopyToClipboard(true);
                    else
                        e.SuppressKeyPress = false;
                }
            }
            e.Handled = e.SuppressKeyPress;
        }

        private void dgvFiles_KeyPress(object sender, KeyPressEventArgs e) {
            if (this.dgvFiles.EditingControl != null)
                return;
            SendMessageHelper.SendKeyPress((Control)this.txtSearch, e.KeyChar);
            e.Handled = true;
        }

        private void dgvFiles_SelectionChanged(object sender, EventArgs e) {
            this.Infos.SelectedCount = (uint)this.dgvFiles.SelectedRows.Count;
            this.Infos.SelectedSize = 0UL;
            foreach (DataGridViewRow selectedRow in (BaseCollection)this.dgvFiles.SelectedRows) {
                DetailItem row = (DetailItem)((DataRowView)selectedRow.DataBoundItem).Row;
                if (!(row.Size is DBNull))
                    this.Infos.SelectedSize += (ulong)row.Size;
            }
            this.OnSelectionChanged();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e) {
            e.SuppressKeyPress = true;
            if (this.HandleKeyDown(e))
                return;
            if (e.KeyCode == Keys.Down) {
                if (this.dgvFiles.Rows.Count == 0)
                    return;
                if (this.dgvFiles.SelectedRows.Count != 0) {
                    DataGridViewRow selectedRow = this.dgvFiles.SelectedRows[this.dgvFiles.SelectedRows.Count - 1];
                    if (selectedRow.Index != this.dgvFiles.Rows.Count - 1) {
                        selectedRow.Selected = false;
                        DataGridViewRow row = this.dgvFiles.Rows[selectedRow.Index + 1];
                        row.Selected = true;
                        this.dgvFiles.CurrentCell = row.Cells[0];
                        this.EnsureVisibleRowIndex(row.Index);
                    }
                    this.dgvFiles.Focus();
                }
                else {
                    this.dgvFiles.Rows[0].Selected = true;
                    this.dgvFiles.FirstDisplayedScrollingRowIndex = 0;
                    this.dgvFiles.Focus();
                }
            }
            else if (e.KeyCode == Keys.Next)
                SendMessageHelper.SendKeyDown((Control)this.dgvFiles, e.KeyCode);
            else if (e.KeyCode == Keys.Prior)
                SendMessageHelper.SendKeyDown((Control)this.dgvFiles, e.KeyCode);
            else
                e.SuppressKeyPress = false;
        }

        private void txtSearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            if (e.KeyCode != Keys.Tab)
                return;
            this.HandleKeyDown(new KeyEventArgs(e.KeyCode));
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) {
            if (this.txtSearch.TextLength == 0)
                this.dgvFiles.DataSource = (object)this._files.DefaultView;
            string sortColumn = (string)null;
            if (this.dgvFiles.SortedColumn != null)
                sortColumn = this.dgvFiles.SortedColumn.Name;
            SortOrder sortOrder = this.dgvFiles.SortOrder;
            try {
                this.dgvFiles.DataSource = (object)new DetailTableView(this._files, string.Format("Name LIKE '{0}%'", (object)this.txtSearch.Text.Replace('%', '*')), sortColumn, sortOrder);
                FsApp.Instance.ShowInfoMessage("");
            }
            catch (Exception ex) {
                FsApp.Instance.ShowInfoMessage("Error: {0}", (object)ex.Message);
                this.dgvFiles.DataSource = (object)new DetailTableView(this._files, "1 = 2", sortColumn, sortOrder);
            }
        }

        private void cboPath_KeyDown(object sender, KeyEventArgs e) {
            e.Handled = true;
            e.SuppressKeyPress = true;
            if (e.KeyCode == Keys.Return) {
                try {
                    DirectoryInfo dir = new DirectoryInfo(this.cboPath.Text);
                    if (dir.FullName.ToLower() != this.ParentDirectory.FullName.ToLower())
                        this.OnRequestParentDirectory(dir);
                    else
                        this.cboPath.Text = this.ParentDirectory.FullName;
                }
                catch {
                    this.cboPath.Text = this.ParentDirectory.FullName;
                }
                finally {
                    this.dgvFiles.Focus();
                }
            }
            else if (e.KeyCode == Keys.Escape) {
                if (this.cboPath.DroppedDown) {
                    this.cboPath.DroppedDown = false;
                }
                else {
                    this.cboPath.Text = this.ParentDirectory.FullName;
                    this.dgvFiles.Focus();
                    this.OnRequestParentDirectory(this.ParentDirectory);
                }
            }
            else {
                e.Handled = false;
                e.SuppressKeyPress = false;
            }
        }

        private void ctxCboPathAddToFavorites_Click(object sender, EventArgs e) => FsApp.Instance.ExecuteCommand(typeof(CmdFavoritesEdit), new DataContext()
        {
      {
        (object) "Favorite",
        (object) this.cboPath.Text
      }
    });

        protected override void Dispose(bool disposing) {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.components = (IContainer)new Container();
            this.panel1 = new Panel();
            this.cboPath = new ComboBox();
            this.txtSearch = new RichTextBox();
            this.dgvFiles = new DetailGrid();
            this.ctxCboPath = new ContextMenuStrip(this.components);
            this.ctxCboPathAddToFavorites = new ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((ISupportInitialize)this.dgvFiles).BeginInit();
            this.ctxCboPath.SuspendLayout();
            this.SuspendLayout();
            this.panel1.Controls.Add((Control)this.cboPath);
            this.panel1.Controls.Add((Control)this.txtSearch);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(541, 23);
            this.panel1.TabIndex = 0;
            this.cboPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.cboPath.ContextMenuStrip = this.ctxCboPath;
            this.cboPath.FormattingEnabled = true;
            this.cboPath.Location = new Point(160, 0);
            this.cboPath.Name = "cboPath";
            this.cboPath.Size = new Size(381, 21);
            this.cboPath.TabIndex = 1;
            this.cboPath.TabStop = false;
            this.txtSearch.Location = new Point(0, 0);
            this.txtSearch.Multiline = false;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(154, 22);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TabStop = false;
            this.txtSearch.Text = "";
            this.dgvFiles.AllowDrop = true;
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.AllowUserToOrderColumns = true;
            this.dgvFiles.AllowUserToResizeRows = false;
            this.dgvFiles.BackgroundColor = SystemColors.Control;
            this.dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Dock = DockStyle.Fill;
            this.dgvFiles.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dgvFiles.GridColor = Color.LightGray;
            this.dgvFiles.Location = new Point(0, 23);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.RowHeadersWidth = 20;
            this.dgvFiles.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvFiles.Size = new Size(541, 286);
            this.dgvFiles.TabIndex = 1;
            this.ctxCboPath.Items.AddRange(new ToolStripItem[1]
            {
        (ToolStripItem) this.ctxCboPathAddToFavorites
            });
            this.ctxCboPath.Name = "ctxCboPath";
            this.ctxCboPath.Size = new Size(161, 26);
            this.ctxCboPathAddToFavorites.Name = "ctxCboPathAddToFavorites";
            this.ctxCboPathAddToFavorites.Size = new Size(160, 22);
            this.ctxCboPathAddToFavorites.Text = "Add to Favorites";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add((Control)this.dgvFiles);
            this.Controls.Add((Control)this.panel1);
            this.Name = "DetailView";
            this.Size = new Size(541, 309);
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize)this.dgvFiles).EndInit();
            this.ctxCboPath.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        public class DetailViewInfos {
            public ulong CompleteSize;
            public ulong SelectedSize;
            public uint CompleteCount;
            public uint SelectedCount;
        }
    }
}
