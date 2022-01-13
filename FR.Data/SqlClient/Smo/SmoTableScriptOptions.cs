// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoTableScriptOptions
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Diagnostics;

namespace FR.Data.SqlClient.Smo
{
  public class SmoTableScriptOptions : SmoScriptOptions
  {
    private bool _collation;
    private bool _defaults;
    private bool _primaryKey;
    private bool _script2000;
    private bool _updateKeyColumn;
    private bool _columnsForInsert;

    public SmoTableScriptOptions()
    {
      this.Collation = false;
      this.Defaults = true;
      this.Script2000 = false;
      this.UpdateKeyColumn = false;
      this.ColumnsForInsert = true;
    }

    public bool Collation
    {
      get => this._collation;
      set => this._collation = value;
    }

    public bool Defaults
    {
      get => this._defaults;
      set => this._defaults = value;
    }

    public bool PrimaryKey
    {
      [DebuggerNonUserCode] get => this._primaryKey;
      [DebuggerNonUserCode] set => this._primaryKey = value;
    }

    public bool Script2000
    {
      get => this._script2000;
      set => this._script2000 = value;
    }

    public bool UpdateKeyColumn
    {
      get => this._updateKeyColumn;
      set => this._updateKeyColumn = value;
    }

    public bool ColumnsForInsert
    {
      get => this._columnsForInsert;
      set => this._columnsForInsert = value;
    }
  }
}
