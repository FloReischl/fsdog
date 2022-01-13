// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDataTypeCollection
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;

namespace FR.Data.SqlClient.Smo
{
  public class SmoDataTypeCollection : SmoCollection<SmoDataType>
  {
    internal SmoDataTypeCollection(SmoDatabase parent)
      : base((SmoObject) parent)
    {
    }

    public SmoDataType FindBySqlDbType(SqlDbType sqlType)
    {
      foreach (SmoDataType bySqlDbType in (SmoCollection<SmoDataType>) this)
      {
        if (bySqlDbType.SqlDbType == sqlType)
          return bySqlDbType;
      }
      return (SmoDataType) null;
    }
  }
}
