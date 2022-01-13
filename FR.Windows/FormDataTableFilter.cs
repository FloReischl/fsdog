// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormDataTableFilter
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class FormDataTableFilter : FormBase
  {
    private IContainer components;
    private DataGridView dgvResult;
    private RichTextBox txtFilter;
    private RichTextBox txtError;
    private Label lblDescription;
    private Button btnOk;
    private Button btnTest;
    private Button btnCancel;
    private ToolTip tipTest;
    private SplitContainer splitContainer1;
    private DataTable _dataSource;
    private string _filter;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.dgvResult = new DataGridView();
      this.txtFilter = new RichTextBox();
      this.txtError = new RichTextBox();
      this.lblDescription = new Label();
      this.btnOk = new Button();
      this.btnTest = new Button();
      this.btnCancel = new Button();
      this.tipTest = new ToolTip(this.components);
      this.splitContainer1 = new SplitContainer();
      ((ISupportInitialize) this.dgvResult).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      this.dgvResult.AllowUserToAddRows = false;
      this.dgvResult.AllowUserToDeleteRows = false;
      this.dgvResult.AllowUserToOrderColumns = true;
      this.dgvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvResult.Dock = DockStyle.Fill;
      this.dgvResult.Location = new Point(0, 0);
      this.dgvResult.Name = "dgvResult";
      this.dgvResult.ReadOnly = true;
      this.dgvResult.Size = new Size(631, 129);
      this.dgvResult.TabIndex = 0;
      this.txtFilter.AcceptsTab = true;
      this.txtFilter.Dock = DockStyle.Fill;
      this.txtFilter.Font = new Font("Lucida Console", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.txtFilter.Location = new Point(0, 0);
      this.txtFilter.Name = "txtFilter";
      this.txtFilter.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
      this.txtFilter.Size = new Size(631, 132);
      this.txtFilter.TabIndex = 1;
      this.txtFilter.Text = "";
      this.txtError.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.txtError.Location = new Point(12, 296);
      this.txtError.Name = "txtError";
      this.txtError.ReadOnly = true;
      this.txtError.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
      this.txtError.Size = new Size(634, 44);
      this.txtError.TabIndex = 2;
      this.txtError.Text = "";
      this.lblDescription.AutoSize = true;
      this.lblDescription.Location = new Point(12, 9);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new Size(342, 13);
      this.lblDescription.TabIndex = 3;
      this.lblDescription.Text = "Enter your query filter as structured query language (SQL) where clause";
      this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOk.Location = new Point(487, 346);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new Size(75, 23);
      this.btnOk.TabIndex = 4;
      this.btnOk.Text = "&Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.btnTest.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnTest.Location = new Point(293, 346);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new Size(75, 23);
      this.btnTest.TabIndex = 5;
      this.btnTest.Text = "&Test";
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Click += new EventHandler(this.btnTest_Click);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.Location = new Point(568, 346);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.splitContainer1.Location = new Point(12, 25);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = Orientation.Horizontal;
      this.splitContainer1.Panel1.Controls.Add((Control) this.txtFilter);
      this.splitContainer1.Panel2.Controls.Add((Control) this.dgvResult);
      this.splitContainer1.Size = new Size(631, 265);
      this.splitContainer1.SplitterDistance = 132;
      this.splitContainer1.TabIndex = 7;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.ClientSize = new Size(655, 381);
      this.Controls.Add((Control) this.splitContainer1);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.btnTest);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.lblDescription);
      this.Controls.Add((Control) this.txtError);
      this.KeyPreview = true;
      this.Name = "CFormDataTableFilter";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Filter Data";
      this.KeyDown += new KeyEventHandler(this.CFormDataTableFilter_KeyDown);
      this.Load += new EventHandler(this.FormDataTableFilter_Load);
      ((ISupportInitialize) this.dgvResult).EndInit();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public DataTable DataSource
    {
      get => this._dataSource;
      set => this._dataSource = value;
    }

    public string Filter
    {
      get => this._filter;
      set => this._filter = value;
    }

    public FormDataTableFilter() => this.InitializeComponent();

    private void FormDataTableFilter_Load(object sender, EventArgs e)
    {
      if (this.DataSource == null)
      {
        Exception ex = (Exception) new NullReferenceException("DataSource property cannot be null");
        this.LogEx(ex);
        throw ex;
      }
      if (this._filter != null)
        this.txtFilter.Text = this._filter;
      this.tipTest.SetToolTip((Control) this.btnTest, "CTRL + Enter");
      this.applyFilter();
    }

    private void CFormDataTableFilter_KeyDown(object sender, KeyEventArgs e)
    {
      if (!e.Control || e.KeyCode != Keys.Return)
        return;
      this.applyFilter();
      e.Handled = true;
    }

    private void btnTest_Click(object sender, EventArgs e) => this.applyFilter();

    private void btnOk_Click(object sender, EventArgs e)
    {
      this.applyFilter();
      if (this.dgvResult.DataSource != null)
      {
        this._filter = this.txtFilter.Text;
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      else
      {
        int num = (int) MessageBox.Show((IWin32Window) this, "Error in filter, please fix.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void applyFilter()
    {
      try
      {
        this.txtError.Text = "";
        this.dgvResult.DataSource = (object) new DataView(this.DataSource, this.txtFilter.Text, (string) null, DataViewRowState.CurrentRows);
      }
      catch (Exception ex)
      {
        this.dgvResult.DataSource = (object) null;
        this.txtError.Text = ex.Message;
      }
    }
  }
}
