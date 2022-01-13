// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoIndexScriptOptions
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

namespace FR.Data.SqlClient.Smo
{
  public class SmoIndexScriptOptions : SmoScriptOptions
  {
    private bool _fillFactor;
    private bool _padIndex;
    private bool _ignoreDupKey;
    private bool _allowRowLocks;
    private bool _allowPageLocks;
    private bool _online;
    private bool _dropExisting;
    private bool _sortInTempDb;
    private bool _statisticsNoRecompute;
    private int _maxDop;
    private bool _includeColumns;
    private bool _optionsOnly;
    private bool _noOptions;
    private bool _script2000;

    public bool FillFactor
    {
      get => this._fillFactor;
      set => this._fillFactor = value;
    }

    public bool PadIndex
    {
      get => this._padIndex;
      set => this._padIndex = value;
    }

    public bool IgnoreDupKey
    {
      get => this._ignoreDupKey;
      set => this._ignoreDupKey = value;
    }

    public bool AllowRowLocks
    {
      get => this._allowRowLocks;
      set => this._allowRowLocks = value;
    }

    public bool AllowPageLocks
    {
      get => this._allowPageLocks;
      set => this._allowPageLocks = value;
    }

    public bool Online
    {
      get => this._online;
      set => this._online = value;
    }

    public bool DropExisting
    {
      get => this._dropExisting;
      set => this._dropExisting = value;
    }

    public bool SortInTempDb
    {
      get => this._sortInTempDb;
      set => this._sortInTempDb = value;
    }

    public bool StatisticsNoRecompute
    {
      get => this._statisticsNoRecompute;
      set => this._statisticsNoRecompute = value;
    }

    public int MaxDop
    {
      get => this._maxDop;
      set => this._maxDop = value;
    }

    public bool IncludeColumns
    {
      get => this._includeColumns;
      set => this._includeColumns = value;
    }

    public bool OptionsOnly
    {
      get => this._optionsOnly;
      set => this._optionsOnly = value;
    }

    public bool NoOptions
    {
      get => this._noOptions;
      set => this._noOptions = value;
    }

    public bool Script2000
    {
      get => this._script2000;
      set => this._script2000 = value;
    }

    public SmoIndexScriptOptions()
    {
      this.FillFactor = true;
      this.PadIndex = true;
      this.IgnoreDupKey = true;
      this.AllowRowLocks = true;
      this.AllowPageLocks = true;
      this.Online = false;
      this.DropExisting = false;
      this.SortInTempDb = false;
      this.StatisticsNoRecompute = false;
      this.MaxDop = 0;
      this.IncludeColumns = true;
      this.OptionsOnly = false;
      this.NoOptions = false;
      this.Script2000 = false;
    }
  }
}
