// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoSchemaCollection
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data.SqlClient;

namespace FR.Data.SqlClient.Smo
{
  public class SmoSchemaCollection : SmoCollection<SmoSchema>
  {
    private SmoSchema _defaultSchema;

    public SmoSchema DefaultSchema
    {
      get
      {
        if (object.ReferenceEquals((object) this._defaultSchema, (object) null))
          this._defaultSchema = this.FindByName((string) new SqlCommand("SELECT SCHEMA_NAME()", ((SmoDatabase) this._parent).Server.Connection).ExecuteScalar());
        return this._defaultSchema;
      }
    }

    internal SmoSchemaCollection(SmoDatabase database)
      : base((SmoObject) database)
    {
    }

    public int Add(SmoSchema schema)
    {
      this._parent.setState(SmoObjectState.Changed);
      this.add(schema);
      return this.Count;
    }
  }
}
