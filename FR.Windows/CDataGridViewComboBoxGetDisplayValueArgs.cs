// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.CDataGridViewComboBoxGetDisplayValueArgs
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class CDataGridViewComboBoxGetDisplayValueArgs : EventArgs
  {
    private int _rowIndex;
    private object _value;
    private TypeConverter _valueTypeConverter;
    private DataGridViewCellStyle _cellStyle;
    private object _formattedValue;
    private TypeConverter _formattedTypeComverter;
    private DataGridViewDataErrorContexts _context;
    private bool _handled;

    public CDataGridViewComboBoxGetDisplayValueArgs(
      int rowIndex,
      object value,
      DataGridViewCellStyle cellStyle,
      TypeConverter valueTypeConverter,
      TypeConverter formattedTypeConverter,
      DataGridViewDataErrorContexts context)
    {
      this._rowIndex = rowIndex;
      this._value = value;
      this._cellStyle = cellStyle;
      this._valueTypeConverter = valueTypeConverter;
      this._formattedTypeComverter = formattedTypeConverter;
      this._context = context;
    }

    public int RowIndex => this._rowIndex;

    public object Value => this._value;

    public TypeConverter ValueTypeConverter => this._valueTypeConverter;

    public DataGridViewCellStyle CellStyle => this._cellStyle;

    public object FormattedValue
    {
      get => this._formattedValue;
      set => this._formattedValue = value;
    }

    public TypeConverter FormattedTypeConverter => this._formattedTypeComverter;

    public DataGridViewDataErrorContexts Context => this._context;

    public bool Handled
    {
      get => this._handled;
      set => this._handled = value;
    }
  }
}
