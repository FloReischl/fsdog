// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.DataGridViewColumnInitializeEditingControlArgs
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class DataGridViewColumnInitializeEditingControlArgs : EventArgs
  {
    private int _rowIndex;
    private int _columnIndex;
    private bool _listFilled;
    private object _initialFormattedValue;
    private DataGridViewCellStyle _cellStyle;
    private ComboBox _editingControl;

    public DataGridViewColumnInitializeEditingControlArgs(
      int rowIndex,
      int columnIndex,
      object initialFormattedValue,
      DataGridViewCellStyle cellStyle,
      ComboBox editingControl)
    {
      this._rowIndex = rowIndex;
      this._columnIndex = columnIndex;
      this.ListFilled = false;
      this._initialFormattedValue = initialFormattedValue;
      this._cellStyle = cellStyle;
      this._editingControl = editingControl;
    }

    public int RowIndex => this._rowIndex;

    public int ColumnIndex => this._columnIndex;

    public bool ListFilled
    {
      get => this._listFilled;
      set => this._listFilled = value;
    }

    public object InitialFormattedValue => this._initialFormattedValue;

    public DataGridViewCellStyle CellStyle => this._cellStyle;

    public ComboBox EditingControl => this._editingControl;
  }
}
