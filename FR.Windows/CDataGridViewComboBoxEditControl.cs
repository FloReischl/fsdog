// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.CDataGridViewComboBoxEditControl
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class CDataGridViewComboBoxEditControl : ComboBoxBase, IDataGridViewEditingControl
  {
    private DataGridView _editingControlDataGridView;
    private object _editingControlFormattedValue;
    private int _editingControlRowIndex;
    private bool _editingControlValueChanged;
    private bool _initializing;

    public DataGridView EditingControlDataGridView
    {
      get => this._editingControlDataGridView;
      set => this._editingControlDataGridView = value;
    }

    public object EditingControlFormattedValue
    {
      get => this._editingControlFormattedValue;
      set => this._editingControlFormattedValue = value;
    }

    public int EditingControlRowIndex
    {
      get => this._editingControlRowIndex;
      set => this._editingControlRowIndex = value;
    }

    public bool EditingControlValueChanged
    {
      get => this._editingControlValueChanged;
      set => this._editingControlValueChanged = value;
    }

    public Cursor EditingPanelCursor => Cursors.Default;

    public bool RepositionEditingControlOnValueChange => false;

    protected internal bool Initializing
    {
      get => this._initializing;
      set => this._initializing = value;
    }

    public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
    }

    public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
    {
      switch (keyData & Keys.KeyCode)
      {
        case Keys.Return:
        case Keys.End:
        case Keys.Home:
          return true;
        case Keys.Up:
          return true;
        case Keys.Down:
          return true;
        case Keys.Delete:
          this.SelectedIndex = -1;
          return true;
        default:
          return !dataGridViewWantsInputKey;
      }
    }

    public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) => this.EditingControlFormattedValue;

    public void PrepareEditingControlForEdit(bool selectAll)
    {
      this.SelectedItem = this._editingControlFormattedValue;
      this.EditingControlValueChanged = false;
      if (!selectAll)
        return;
      this.SelectAll();
    }

    protected override void InitLayout() => base.InitLayout();

    protected override void OnSelectedIndexChanged(EventArgs e)
    {
      this.EditingControlFormattedValue = !(this.SelectedItem is ComboBoxItem) ? this.SelectedItem : ((ComboBoxItem) this.SelectedItem).Value;
      if (!this.Initializing)
      {
        this.EditingControlValueChanged = true;
        this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
      }
      base.OnSelectedIndexChanged(e);
    }

    void IDataGridViewEditingControl.ApplyCellStyleToEditingControl(
      DataGridViewCellStyle dataGridViewCellStyle)
    {
      this.ApplyCellStyleToEditingControl(dataGridViewCellStyle);
    }

    DataGridView IDataGridViewEditingControl.EditingControlDataGridView
    {
      get => this.EditingControlDataGridView;
      set => this.EditingControlDataGridView = value;
    }

    object IDataGridViewEditingControl.EditingControlFormattedValue
    {
      get => this.EditingControlFormattedValue;
      set => this.EditingControlFormattedValue = value;
    }

    int IDataGridViewEditingControl.EditingControlRowIndex
    {
      get => this.EditingControlRowIndex;
      set => this.EditingControlRowIndex = value;
    }

    bool IDataGridViewEditingControl.EditingControlValueChanged
    {
      get => this.EditingControlValueChanged;
      set => this.EditingControlValueChanged = value;
    }

    bool IDataGridViewEditingControl.EditingControlWantsInputKey(
      Keys keyData,
      bool dataGridViewWantsInputKey)
    {
      return this.EditingControlWantsInputKey(keyData, dataGridViewWantsInputKey);
    }

    Cursor IDataGridViewEditingControl.EditingPanelCursor => this.EditingPanelCursor;

    object IDataGridViewEditingControl.GetEditingControlFormattedValue(
      DataGridViewDataErrorContexts context)
    {
      return this.GetEditingControlFormattedValue(context);
    }

    void IDataGridViewEditingControl.PrepareEditingControlForEdit(
      bool selectAll)
    {
      this.PrepareEditingControlForEdit(selectAll);
    }

    bool IDataGridViewEditingControl.RepositionEditingControlOnValueChange => this.RepositionEditingControlOnValueChange;
  }
}
