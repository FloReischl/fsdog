// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormListSelect
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class FormListSelect : FormBase
  {
    private IContainer components;
    private Label lblDescription;
    private ListView lvwList;
    private Button btnCancel;
    private Button btnOk;
    private bool _customWidth;
    private List<FormListSelectItem> _selectedItems;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblDescription = new Label();
      this.lvwList = new ListView();
      this.btnCancel = new Button();
      this.btnOk = new Button();
      this.SuspendLayout();
      this.lblDescription.AutoSize = true;
      this.lblDescription.Location = new Point(12, 9);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new Size(70, 13);
      this.lblDescription.TabIndex = 0;
      this.lblDescription.Text = "lblDescription";
      this.lvwList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.lvwList.Location = new Point(15, 44);
      this.lvwList.Name = "lvwList";
      this.lvwList.Size = new Size(506, 178);
      this.lvwList.TabIndex = 0;
      this.lvwList.UseCompatibleStateImageBehavior = false;
      this.lvwList.View = View.Details;
      this.lvwList.MouseDoubleClick += new MouseEventHandler(this.lvwList_MouseDoubleClick);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Location = new Point(446, 228);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOk.Location = new Point(365, 228);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new Size(75, 23);
      this.btnOk.TabIndex = 3;
      this.btnOk.Text = "&Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.AcceptButton = (IButtonControl) this.btnOk;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.ClientSize = new Size(533, 263);
      this.Controls.Add((Control) this.btnOk);
      this.Controls.Add((Control) this.btnCancel);
      this.Controls.Add((Control) this.lvwList);
      this.Controls.Add((Control) this.lblDescription);
      this.MinimizeBox = false;
      this.Name = nameof (FormListSelect);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Select an item";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public FormListSelect()
    {
      this.InitializeComponent();
      this._customWidth = false;
      this._selectedItems = new List<FormListSelectItem>();
      this.MultiSelect = false;
      this.lvwList.HideSelection = false;
      this.lvwList.FullRowSelect = true;
    }

    public string Description
    {
      get => this.lblDescription.Text;
      set => this.lblDescription.Text = value;
    }

    public bool MultiSelect
    {
      get => this.lvwList.MultiSelect;
      set => this.lvwList.MultiSelect = value;
    }

    public FormListSelectItem SelectedItem
    {
      get => this.SelectedItems.Count == 0 ? (FormListSelectItem) null : this.SelectedItems[0];
      set
      {
        this.SelectedItems.Clear();
        this.SelectedItems.Add(value);
      }
    }

    public List<FormListSelectItem> SelectedItems => this._selectedItems;

    public ImageList SmallImageList
    {
      get => this.lvwList.SmallImageList;
      set => this.lvwList.SmallImageList = value;
    }

    public void SetColumns(string column, params string[] subColumns)
    {
      this.lvwList.Columns.Clear();
      this.AddColumn(column);
      foreach (string subColumn in subColumns)
        this.AddColumn(subColumn);
    }

    public void SetColumnWidths(params int[] width)
    {
      for (int index = 0; index < width.Length; ++index)
        this.lvwList.Columns[index].Width = width[index];
      this._customWidth = true;
    }

    public void AddColumn(string columnName) => this.lvwList.Columns.Add(columnName);

    public FormListSelectItem AddItem(object value, params string[] items)
    {
      FormListSelectItem formListSelectItem = new FormListSelectItem(value, items);
      this.AddItem(formListSelectItem);
      return formListSelectItem;
    }

    public void AddItem(FormListSelectItem item) => this.lvwList.Items.Add((ListViewItem) item);

    private void GetSelectedItems()
    {
      this.SelectedItems.Clear();
      foreach (FormListSelectItem selectedItem in this.lvwList.SelectedItems)
        this.SelectedItems.Add(selectedItem);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!(this.MaximumSize != Size.Empty) || this.lvwList.Items.Count == 0)
        return;
      this.Size = new Size(this.MaximumSize.Width, this.lvwList.GetItemRect(this.lvwList.Items.Count - 1).Bottom + (this.lvwList.Height - this.lvwList.ClientRectangle.Height) + (this.lvwList.Top + (this.Height - this.lvwList.Bottom)));
    }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      foreach (ListViewItem selectedItem in this.SelectedItems)
      {
        int index = this.lvwList.Items.IndexOf(selectedItem);
        if (index != -1)
          this.lvwList.Items[index].Selected = true;
      }
      if (!this._customWidth)
      {
        foreach (ColumnHeader column in this.lvwList.Columns)
          column.Width = this.lvwList.ClientSize.Width / this.lvwList.Columns.Count;
      }
      this.lvwList.Focus();
    }

    private void lvwList_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.lvwList.SelectedItems.Count == 0)
        return;
      this.GetSelectedItems();
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      if (this.lvwList.SelectedItems.Count == 0)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, "Nothing selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        this.GetSelectedItems();
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }

    private void btnCancel_Click(object sender, EventArgs e) => this.Close();
  }
}
