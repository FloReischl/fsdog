﻿// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDatabaseRoleScriptOptions
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

namespace FR.Data.SqlClient.Smo
{
  public class SmoDatabaseRoleScriptOptions : SmoScriptOptions
  {
    private bool _owner;

    public bool Owner
    {
      get => this._owner;
      set => this._owner = value;
    }

    public SmoDatabaseRoleScriptOptions() => this.Owner = true;
  }
}
