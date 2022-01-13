// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoScriptOptions
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

namespace FR.Data.SqlClient.Smo
{
  public class SmoScriptOptions
  {
    private bool _ifExisting;
    private bool _drop;
    private bool _dataFile;
    private bool _database;
    private bool _schema;

    public bool CheckExists
    {
      get => this._ifExisting;
      set => this._ifExisting = value;
    }

    public bool Drop
    {
      get => this._drop;
      set => this._drop = value;
    }

    public bool DataFile
    {
      get => this._dataFile;
      set => this._dataFile = value;
    }

    public bool Database
    {
      get => this._database;
      set => this._database = value;
    }

    public bool Schema
    {
      get => this._schema;
      set => this._schema = value;
    }

    public SmoScriptOptions()
    {
      this.CheckExists = true;
      this.Drop = false;
      this.DataFile = false;
      this.Database = true;
      this.Schema = true;
    }
  }
}
