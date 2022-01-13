// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoTableColumnScriptOptions
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

namespace FR.Data.SqlClient.Smo
{
  public class SmoTableColumnScriptOptions : SmoScriptOptions
  {
    private bool _collation;
    private bool _defaults;
    private bool _script2000;
    private bool _tableScriptPart;

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

    public bool Script2000
    {
      get => this._script2000;
      set => this._script2000 = value;
    }

    public bool TableScriptPart
    {
      get => this._tableScriptPart;
      set => this._tableScriptPart = value;
    }

    public SmoTableColumnScriptOptions()
    {
      this.Collation = false;
      this.Defaults = true;
      this.TableScriptPart = false;
      this.Script2000 = false;
      this.Schema = true;
      this.Database = false;
      this.CheckExists = false;
    }

    public SmoTableColumnScriptOptions(SmoTableScriptOptions options)
    {
      this.Defaults = options.Defaults;
      this.Collation = options.Collation;
      this.TableScriptPart = true;
      this.Script2000 = options.Script2000;
      this.Schema = options.Schema;
      this.Database = options.Database;
      this.CheckExists = options.CheckExists;
    }
  }
}
