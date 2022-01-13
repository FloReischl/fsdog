// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoConstraintScriptOptions
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Diagnostics;

namespace FR.Data.SqlClient.Smo
{
  public class SmoConstraintScriptOptions : SmoScriptOptions
  {
    private bool _noIndexOptions;
    private bool _onDelete;
    private bool _onUpdate;
    private bool _notForReplication;
    private bool _script2000;
    private bool _tableScript;

    public SmoConstraintScriptOptions()
    {
      this.NoIndexOptions = false;
      this.OnDelete = true;
      this.OnUpdate = true;
      this.NotForReplication = true;
      this.CheckExists = false;
      this.Script2000 = false;
    }

    public bool NoIndexOptions
    {
      get => this._noIndexOptions;
      set => this._noIndexOptions = value;
    }

    public bool OnDelete
    {
      get => this._onDelete;
      set => this._onDelete = value;
    }

    public bool OnUpdate
    {
      get => this._onUpdate;
      set => this._onUpdate = value;
    }

    public bool NotForReplication
    {
      get => this._notForReplication;
      set => this._notForReplication = value;
    }

    public bool Script2000
    {
      get => this._script2000;
      set => this._script2000 = value;
    }

    internal bool TableScript
    {
      [DebuggerNonUserCode] get => this._tableScript;
      [DebuggerNonUserCode] set => this._tableScript = value;
    }
  }
}
