// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.ComboBoxBase
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class ComboBoxBase : ComboBox
  {
    private bool _keyWasHandled;
    private bool _allowOnlyListEntries;

    public bool AllowOnlyListEntries
    {
      get => this._allowOnlyListEntries;
      set => this._allowOnlyListEntries = value;
    }

    public ComboBoxBase() => this._allowOnlyListEntries = false;

    protected override void OnKeyDown(KeyEventArgs e) => base.OnKeyDown(e);

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (this._allowOnlyListEntries)
      {
        this._keyWasHandled = false;
        if (char.IsControl(e.KeyChar))
          return;
        string s = this.Text.Substring(0, this.SelectionStart) + (object) e.KeyChar;
        int index = this.FindString(s);
        e.Handled = true;
        this._keyWasHandled = true;
        if (index <= -1)
          return;
        string str = this.Items[index].ToString();
        this.SelectedIndex = index;
        this.Text = str;
        this.SelectionStart = s.Length;
        this.SelectionLength = str.Length;
      }
      else
        base.OnKeyPress(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      if (this._allowOnlyListEntries && this._keyWasHandled)
        e.Handled = true;
      else
        base.OnKeyUp(e);
    }

    protected override void OnLeave(EventArgs e)
    {
      if (this._allowOnlyListEntries)
      {
        this.SelectedIndex = this.FindStringExact(this.Text);
        if (this.SelectedIndex == -1)
          this.Text = "";
        else
          this.Text = this.SelectedItem.ToString();
      }
      base.OnLeave(e);
    }
  }
}
