// Decompiled with JetBrains decompiler
// Type: FR.Configuration.CommandLineArg
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;

namespace FR.Configuration
{
  public class CommandLineArg : EventArgs
  {
    private string _argument;
    private string _value;
    private string _originalValue;

    public CommandLineArg(string originalValue)
    {
      this._originalValue = originalValue;
      this.Parse();
    }

    public string Argument => this._argument;

    public string Value => this._value;

    public string Original => this._originalValue;

    private void Parse()
    {
      string[] strArray = this.Original.Split('=');
      this._argument = "";
      this._value = "";
      if (strArray.Length == 1)
        this._argument = this._originalValue;
      else if (strArray.Length == 2)
      {
        this._argument = strArray[0];
        this._value = strArray[1];
      }
      else if (strArray.Length > 2)
      {
        this._argument = strArray[0];
        this._value = "";
        for (int index = 1; index < strArray.Length; ++index)
        {
          if (this._value.Length != 0)
            this._value += "=";
          this._value += strArray[index];
        }
      }
      if (!this._argument.Substring(0, 1).Equals("-") && !this._argument.Substring(0, 1).Equals("/"))
        return;
      this._argument = this._argument.Substring(1, this._argument.Length - 1);
    }
  }
}
