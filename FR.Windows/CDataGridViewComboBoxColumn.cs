// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.CDataGridViewComboBoxColumn
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class CDataGridViewComboBoxColumn : DataGridViewColumn
  {
    private object _dataSource;
    private string _displayMember;
    private string _valueMember;
    private ComboBoxStyle _dropDownStyle;
    private bool _allowOnlyListEntries;
    private AutoCompleteStringCollection _autoCompleteCustomSource;
    private AutoCompleteMode _autoCompleteMode;
    private AutoCompleteSource _autoCompleteSource;

    public CDataGridViewComboBoxColumn()
      : base((DataGridViewCell) new CDataGridViewComboBoxCell())
    {
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.AutoCompleteMode = AutoCompleteMode.None;
      this.AutoCompleteSource = AutoCompleteSource.None;
    }

    public event DataGridViewComboBoxInitEditControlHandler InitializeEditingControl;

    public event DataGridViewComboBoxGetDisplayValueHandler GetDisplayValue;

    public object DataSource
    {
      get => this._dataSource;
      set => this._dataSource = value;
    }

    public string DisplayMember
    {
      get => this._displayMember;
      set => this._displayMember = value;
    }

    public string ValueMember
    {
      get => this._valueMember;
      set => this._valueMember = value;
    }

    public ComboBoxStyle DropDownStyle
    {
      get => this._dropDownStyle;
      set => this._dropDownStyle = value;
    }

    public bool AllowOnlyListEntries
    {
      get => this._allowOnlyListEntries;
      set => this._allowOnlyListEntries = value;
    }

    public AutoCompleteStringCollection AutoCompleteCustomSource
    {
      get => this._autoCompleteCustomSource;
      set => this._autoCompleteCustomSource = value;
    }

    public AutoCompleteMode AutoCompleteMode
    {
      get => this._autoCompleteMode;
      set => this._autoCompleteMode = value;
    }

    public AutoCompleteSource AutoCompleteSource
    {
      get => this._autoCompleteSource;
      set => this._autoCompleteSource = value;
    }

    protected void OnGetDisplayValue(CDataGridViewComboBoxGetDisplayValueArgs e)
    {
      if (this.GetDisplayValue == null)
        return;
      this.GetDisplayValue((object) this, e);
    }

    protected internal void OnInitializeEditingControl(
      DataGridViewColumnInitializeEditingControlArgs e)
    {
      if (this.InitializeEditingControl == null)
        return;
      this.InitializeEditingControl((object) this, e);
    }

    internal object getFormattedValue(
      object value,
      int rowIndex,
      ref DataGridViewCellStyle cellStyle,
      TypeConverter valueTypeConverter,
      TypeConverter formattedValueTypeConverter,
      DataGridViewDataErrorContexts context)
    {
      CDataGridViewComboBoxGetDisplayValueArgs e = new CDataGridViewComboBoxGetDisplayValueArgs(rowIndex, value, cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
      this.OnGetDisplayValue(e);
      if (e.Handled)
        return e.FormattedValue;
      object formattedValue = (object) null;
      if (value == null)
        return formattedValue;
      IEnumerable objA = (IEnumerable) null;
      if (this.DataSource is IListSource)
        objA = (IEnumerable) ((IListSource) this.DataSource).GetList();
      else if (this.DataSource is IList)
        objA = (IEnumerable) this.DataSource;
      else if (this.DataSource is IEnumerable)
        objA = (IEnumerable) this.DataSource;
      if (!object.ReferenceEquals((object) objA, (object) null))
      {
        foreach (object obj in objA)
        {
          ICustomTypeDescriptor customTypeDescriptor = !(obj is ICustomTypeDescriptor) ? TypeDescriptor.GetProvider(obj).GetTypeDescriptor(obj) : (ICustomTypeDescriptor) obj;
          PropertyDescriptorCollection properties = customTypeDescriptor.GetProperties();
          PropertyDescriptor pd1 = properties.Find(this.ValueMember, false);
          if (pd1 != null)
          {
            customTypeDescriptor.GetPropertyOwner(pd1);
            object objB = pd1.GetValue(obj);
            if (object.Equals(value, objB))
            {
              PropertyDescriptor pd2 = properties.Find(this.DisplayMember, false);
              if (pd2 != null)
              {
                object propertyOwner = customTypeDescriptor.GetPropertyOwner(pd2);
                formattedValue = pd2.GetValue(propertyOwner);
                break;
              }
              break;
            }
          }
        }
      }
      return formattedValue;
    }
  }
}
