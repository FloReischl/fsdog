// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoUserScriptOptions
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

namespace FR.Data.SqlClient.Smo
{
  public class SmoUserScriptOptions : SmoScriptOptions
  {
    private bool _defaultSchema;

    public bool DefaultSchema
    {
      get => this._defaultSchema;
      set => this._defaultSchema = value;
    }

    public SmoUserScriptOptions() => this.DefaultSchema = true;
  }
}
