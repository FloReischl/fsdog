// Decompiled with JetBrains decompiler
// Type: FsDog.FormCopy
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Collections;
using FR.Collections.Generic;
using FR.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FsDog
{
  public class FormCopy : Form
  {
    private const string INFO_OK = "{0} can be copied/moved.";
    private const string INFO_ERRORS = "{0} of {1} items contain copy/move collisions.";
    private const string INFO_LOADING = "Loading items to be copied.";
    private IContainer components;
    private DataGridView dgvFiles;
    private ContextMenuStrip ctxErrorHandling;
    private ToolStripMenuItem ctxErrorHandle;
    private ToolStripMenuItem ctxErrorSkip;
    private Button btnOk;
    private Button btnCancel;
    private Label lblInfo;
    private ProgressBar pbWorking;
    private DataStore<CopyItem> _items;
    private bool _cancel;
    private SynchronizationContext _snycCtx;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool _moveItems;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DirectoryInfo _destination;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private IList<FileSystemInfo> _sources;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private Dictionary<string, string> _newNames;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.dgvFiles = new DataGridView();
      this.ctxErrorHandling = new ContextMenuStrip(this.components);
      this.ctxErrorSkip = new ToolStripMenuItem();
      this.ctxErrorHandle = new ToolStripMenuItem();
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.lblInfo = new Label();
      this.pbWorking = new ProgressBar();
      ((ISupportInitialize) this.dgvFiles).BeginInit();
      this.ctxErrorHandling.SuspendLayout();
      this.SuspendLayout();
      this.dgvFiles.AllowUserToAddRows = false;
      this.dgvFiles.AllowUserToDeleteRows = false;
      this.dgvFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvFiles.Location = new Point(12, 12);
      this.dgvFiles.Name = "dgvFiles";
      this.dgvFiles.ReadOnly = true;
      this.dgvFiles.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvFiles.Size = new Size(965, 502);
      this.dgvFiles.TabIndex = 2;
      this.ctxErrorHandling.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.ctxErrorSkip,
        (ToolStripItem) this.ctxErrorHandle
      });
      this.ctxErrorHandling.Name = "contextMenuStrip1";
      this.ctxErrorHandling.Size = new Size(113, 48);
      this.ctxErrorSkip.Name = "ctxErrorSkip";
      this.ctxErrorSkip.Size = new Size(112, 22);
      this.ctxErrorSkip.Text = "Skip";
      this.ctxErrorHandle.Name = "ctxErrorHandle";
      this.ctxErrorHandle.Size = new Size(112, 22);
      this.ctxErrorHandle.Text = "Handle";
      this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOk.DialogResult = DialogResult.OK;
      this.btnOk.Location = new Point(821, 520);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new Size(75, 23);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "&Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Location = new Point(902, 520);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.lblInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.lblInfo.AutoSize = true;
      this.lblInfo.Location = new Point(12, 514);
      this.lblInfo.Name = "lblInfo";
      this.lblInfo.Size = new Size(249, 13);
      this.lblInfo.TabIndex = 3;
      this.lblInfo.Text = "One or more copy/mode collisions have to be fixed.";
      this.pbWorking.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.pbWorking.Location = new Point(420, 517);
      this.pbWorking.Name = "pbWorking";
      this.pbWorking.Size = new Size(395, 14);
      this.pbWorking.TabIndex = 4;
      this.AcceptButton = (IButtonControl) this.btnOk;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.ClientSize = new Size(989, 555);
      this.Controls.Add((Control) this.lblInfo);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.pbWorking);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.dgvFiles);
      this.Name = nameof (FormCopy);
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = nameof (FormCopy);
      ((ISupportInitialize) this.dgvFiles).EndInit();
      this.ctxErrorHandling.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormCopy()
    {
      this.InitializeComponent();
      this.Load += new EventHandler(this.FormCopy_Load);
      this.dgvFiles.CellMouseDown += new DataGridViewCellMouseEventHandler(this.dgvFiles_CellMouseDown);
      this.dgvFiles.KeyDown += new KeyEventHandler(this.dgvFiles_KeyDown);
      this.dgvFiles.CellEnter += new DataGridViewCellEventHandler(this.dgvFiles_CellEnter);
      this.ctxErrorSkip.Click += new EventHandler(this.ctxErrorSkip_Click);
      this.ctxErrorHandle.Click += new EventHandler(this.ctxErrorHandle_Click);
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    }

    public bool MoveItems
    {
      [DebuggerNonUserCode] get => this._moveItems;
      [DebuggerNonUserCode] set => this._moveItems = value;
    }

    public DirectoryInfo Destination
    {
      [DebuggerNonUserCode] get => this._destination;
      [DebuggerNonUserCode] set => this._destination = value;
    }

    public IList<FileSystemInfo> Sources
    {
      [DebuggerNonUserCode] get => this._sources;
      [DebuggerNonUserCode] set => this._sources = value;
    }

    public Dictionary<string, string> NameMapping
    {
      [DebuggerNonUserCode] get
      {
        if (this._newNames == null)
          this._newNames = new Dictionary<string, string>();
        return this._newNames;
      }
      [DebuggerNonUserCode] set => this._newNames = value;
    }

    private void CreateCopyItemsBegin()
    {
      this._items = new DataStore<CopyItem>(new DynamicPropertyCreator(PropertyDescriptorHelper.GetProperty(typeof (CopyItem), "SourceFullName")).Create(), (IPrimaryKey) new PrimaryKeyDictionary<string, object>((IEqualityComparer<string>) StringComparer.CurrentCulture));
      this._items.BeginInit();
      this.CreateCopyItems(this.Destination, (IEnumerable<FileSystemInfo>) this.Sources);
      this._snycCtx.Send(new SendOrPostCallback(this.CreateCopyItemsEnd), (object) null);
    }

    private void CreateCopyItems(DirectoryInfo parentDestination, IEnumerable<FileSystemInfo> items)
    {
      if (this._cancel)
        return;
      foreach (FileSystemInfo fileSystemInfo in items)
      {
        if (fileSystemInfo is FileInfo sourceFile)
        {
          FileInfo destinationFile;
          if (this.NameMapping.ContainsKey(sourceFile.FullName))
          {
            string path2 = this.NameMapping[sourceFile.FullName];
            destinationFile = new FileInfo(Path.Combine(parentDestination.FullName, path2));
          }
          else
            destinationFile = new FileInfo(Path.Combine(parentDestination.FullName, sourceFile.Name));
          this._items.Add(new CopyItem(sourceFile, destinationFile));
        }
        if (fileSystemInfo is DirectoryInfo sourceDirectory)
        {
          DirectoryInfo directoryInfo;
          if (this.NameMapping.ContainsKey(sourceDirectory.FullName))
          {
            string path2 = this.NameMapping[sourceDirectory.FullName];
            directoryInfo = new DirectoryInfo(Path.Combine(parentDestination.FullName, path2));
          }
          else
            directoryInfo = new DirectoryInfo(Path.Combine(parentDestination.FullName, sourceDirectory.Name));
          this._items.Add(new CopyItem(sourceDirectory, directoryInfo));
          this.CreateCopyItems(directoryInfo, (IEnumerable<FileSystemInfo>) sourceDirectory.GetFileSystemInfos());
        }
      }
    }

    private void CreateCopyItemsEnd(object dummy)
    {
      if (this._cancel)
        return;
      this._items.EndInit();
      this.dgvFiles.DataSource = (object) this._items;
      this.dgvFiles.Columns["DetailDescription"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
      this.dgvFiles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
      this.dgvFiles.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
      this.Cursor = Cursors.Default;
      this.pbWorking.Visible = false;
      this.UpdateOk();
    }

    private void DoActionsBegin()
    {
      List<CopyItem> copyItemList = new List<CopyItem>();
      foreach (CopyItem copyItem in this._items)
      {
        if (copyItem.SourceDirectory != null)
          copyItemList.Add(copyItem);
        else
          copyItem.DoAction(this.MoveItems);
      }
      foreach (CopyItem copyItem in copyItemList)
        copyItem.DoAction(this.MoveItems);
      this._snycCtx.Send(new SendOrPostCallback(this.DoActionsEnd), (object) null);
    }

    private void DoActionsEnd(object dummy) => this.Close();

    private void UpdateOk()
    {
      int count = this._items.FindAll("Error", IndexFindOption.Equal, (object) true).Count;
      if (count == 0)
      {
        this.btnOk.Enabled = true;
        this.btnOk.Focus();
        this.lblInfo.Text = string.Format("{0} can be copied/moved.", (object) this._items.Count);
        this.AcceptButton = (IButtonControl) this.btnOk;
      }
      else
      {
        this.btnOk.Enabled = false;
        this.lblInfo.Text = string.Format("{0} of {1} items contain copy/move collisions.", (object) count, (object) this._items.Count);
      }
    }

    private void FormCopy_Load(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      this.btnCancel.Cursor = Cursors.Default;
      this.lblInfo.Text = "Loading items to be copied.";
      this.btnOk.Enabled = false;
      this.pbWorking.MarqueeAnimationSpeed = 20;
      this.pbWorking.Visible = true;
      this.pbWorking.Style = ProgressBarStyle.Marquee;
      if (this.MoveItems)
        this.Text = string.Format("Move items to {0}", (object) this.Destination.FullName);
      else
        this.Text = string.Format("Copy items to {0}", (object) this.Destination.FullName);
      this._snycCtx = SynchronizationContext.Current;
      new Thread(new ThreadStart(this.CreateCopyItemsBegin)).Start();
    }

    private void dgvFiles_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.Button != MouseButtons.Right)
        return;
      DataGridViewCell dgvFile = this.dgvFiles[e.ColumnIndex, e.RowIndex];
      if (dgvFile.Selected)
        return;
      foreach (DataGridViewBand selectedRow in (BaseCollection) this.dgvFiles.SelectedRows)
        selectedRow.Selected = false;
      this.dgvFiles.Rows[dgvFile.RowIndex].Selected = true;
    }

    private void dgvFiles_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
        return;
      DataGridViewRow row = this.dgvFiles.Rows[e.RowIndex];
      if (!((CopyItem) row.DataBoundItem).Error)
        return;
      row.ContextMenuStrip = this.ctxErrorHandling;
    }

    private void ctxErrorSkip_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      foreach (DataGridViewRow selectedRow in (BaseCollection) this.dgvFiles.SelectedRows)
      {
        CopyItem dataBoundItem = (CopyItem) selectedRow.DataBoundItem;
        if (dataBoundItem.Error)
        {
          dataBoundItem.ErrorHandling = CopyErrorHandling.Skip;
          this.dgvFiles.UpdateCellValue(this.dgvFiles.Columns["ErrorHandlingText"].Index, selectedRow.Index);
          this.dgvFiles.UpdateCellValue(this.dgvFiles.Columns["Info"].Index, selectedRow.Index);
        }
      }
      this.UpdateOk();
      this.Cursor = Cursors.Default;
    }

    private void ctxErrorHandle_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      foreach (DataGridViewRow selectedRow in (BaseCollection) this.dgvFiles.SelectedRows)
      {
        CopyItem dataBoundItem = (CopyItem) selectedRow.DataBoundItem;
        if (dataBoundItem.Error)
        {
          dataBoundItem.ErrorHandling = CopyErrorHandling.Handle;
          this.dgvFiles.UpdateCellValue(this.dgvFiles.Columns["ErrorHandlingText"].Index, selectedRow.Index);
        }
      }
      this.UpdateOk();
      this.Cursor = Cursors.Default;
    }

    private void dgvFiles_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Apps)
        return;
      Rectangle displayRectangle = this.dgvFiles.GetCellDisplayRectangle(this.dgvFiles.CurrentCell.ColumnIndex, this.dgvFiles.CurrentCell.RowIndex, true);
      if (displayRectangle.Y <= 0 || displayRectangle.Y >= this.dgvFiles.Height)
        return;
      this.ctxErrorHandling.Show(this.dgvFiles.PointToScreen(new Point(displayRectangle.Location.X, displayRectangle.Y + displayRectangle.Height)));
    }

    private void btnCancel_Click(object sender, EventArgs e) => this._cancel = true;

    private void btnOk_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.None;
      this.Cursor = Cursors.WaitCursor;
      this.btnCancel.Cursor = Cursors.Default;
      this.lblInfo.Text = "Loading items to be copied.";
      this.btnOk.Enabled = false;
      this.pbWorking.Style = ProgressBarStyle.Marquee;
      this.pbWorking.MarqueeAnimationSpeed = 20;
      this.pbWorking.Visible = true;
      new Thread(new ThreadStart(this.DoActionsBegin)).Start();
    }
  }
}
