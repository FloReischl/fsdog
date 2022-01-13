// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.ComboBoxItem
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System.Diagnostics;

namespace FR.Windows.Forms
{
  [DebuggerDisplay("{ToString()}")]
  public class ComboBoxItem
  {
    private object _value;
    private string _text;

    public object Value
    {
      [DebuggerNonUserCode] get => this._value;
      [DebuggerNonUserCode] set => this._value = value;
    }

    public string Text
    {
      [DebuggerNonUserCode] get => this._text;
      [DebuggerNonUserCode] set => this._text = value;
    }

    [DebuggerNonUserCode]
    public ComboBoxItem()
    {
      this._text = (string) null;
      this._value = (object) null;
    }

    [DebuggerNonUserCode]
    public ComboBoxItem(string text, object value)
    {
      this._text = text;
      this._value = value;
    }

    [DebuggerNonUserCode]
    public override int GetHashCode() => this._value != null ? this._value.GetHashCode() : base.GetHashCode();

    [DebuggerNonUserCode]
    public override bool Equals(object obj)
    {
      if (obj is ComboBoxItem)
        return this.Equals(((ComboBoxItem) obj)._value);
      if (obj == null && this._value == null)
        return true;
      return (obj == null || this._value != null) && (obj != null || this._value == null) && this._value.Equals(obj);
    }

    public override string ToString() => this._text != null ? this._text : base.ToString();
  }
}
