// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.ConfigurationOptionsProperty
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Configuration;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class ConfigurationOptionsProperty : IOptionsProperty, INotifyPropertyChanged
  {
    private object[] _allowedValues;
    private IConfigurationProperty _configurationProperty;
    private string _description;
    private Control _editControl;
    private int _editControlWidth;
    private System.Type _propertyType;
    private FormOptionsPropertySpecialType _specialType;
    private string _text;

    public ConfigurationOptionsProperty(
      IConfigurationProperty configurationProperty,
      string text,
      string description)
    {
      this._configurationProperty = configurationProperty;
      this._description = description;
      this._propertyType = typeof (string);
      this._text = text;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public object[] AllowedValues
    {
      [DebuggerNonUserCode] get => this._allowedValues;
      [DebuggerNonUserCode] set => this._allowedValues = value;
    }

    public IConfigurationProperty ConfigurationProperty => this._configurationProperty;

    public string Description
    {
      [DebuggerNonUserCode] get => this._description;
      [DebuggerNonUserCode] set => this._description = value;
    }

    public Control EditControl
    {
      [DebuggerNonUserCode] get => this._editControl;
      [DebuggerNonUserCode] set => this._editControl = value;
    }

    public int EditControlWidth
    {
      get => this._editControlWidth;
      set => this._editControlWidth = value;
    }

    public System.Type PropertyType
    {
      get => this._propertyType;
      set => this._propertyType = value;
    }

    public FormOptionsPropertySpecialType SpecialType
    {
      [DebuggerNonUserCode] get => this._specialType;
      [DebuggerNonUserCode] set => this._specialType = value;
    }

    public string Text
    {
      get => this._text;
      set => this._text = value;
    }

    public object Value
    {
      get => (object) this._configurationProperty.ToString();
      set
      {
        this._configurationProperty.Set(value.ToString());
        this.OnPropertyChanged();
      }
    }

    protected void OnPropertyChanged()
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(this.Text));
    }

    object[] IOptionsProperty.AllowedValues
    {
      [DebuggerNonUserCode] get => this.AllowedValues;
      [DebuggerNonUserCode] set => this.AllowedValues = value;
    }

    string IOptionsProperty.Description
    {
      [DebuggerNonUserCode] get => this.Description;
    }

    Control IOptionsProperty.EditControl
    {
      [DebuggerNonUserCode] get => this.EditControl;
    }

    System.Type IOptionsProperty.PropertyType
    {
      [DebuggerNonUserCode] get => this.PropertyType;
      [DebuggerNonUserCode] set => this.PropertyType = value;
    }

    FormOptionsPropertySpecialType IOptionsProperty.SpecialType
    {
      [DebuggerNonUserCode] get => this.SpecialType;
    }

    string IOptionsProperty.Text
    {
      [DebuggerNonUserCode] get => this.Text;
    }

    object IOptionsProperty.Value
    {
      [DebuggerNonUserCode] get => this.Value;
      [DebuggerNonUserCode] set => this.Value = value;
    }

    int IOptionsProperty.EditControlWidth => this.EditControlWidth;

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
    {
      add => this.PropertyChanged += value;
      remove => this.PropertyChanged -= value;
    }
  }
}
