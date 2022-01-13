// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.SqlTypeHelper
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.Data;

namespace FR.Data.SqlClient
{
  public static class SqlTypeHelper
  {
    public static Type GetType(SqlDbType dbType)
    {
      switch (dbType)
      {
        case SqlDbType.BigInt:
          return typeof (long);
        case SqlDbType.Binary:
          return typeof (byte[]);
        case SqlDbType.Bit:
          return typeof (bool);
        case SqlDbType.Char:
          return typeof (string);
        case SqlDbType.DateTime:
          return typeof (DateTime);
        case SqlDbType.Decimal:
          return typeof (Decimal);
        case SqlDbType.Float:
          return typeof (double);
        case SqlDbType.Image:
          return typeof (byte[]);
        case SqlDbType.Int:
          return typeof (int);
        case SqlDbType.Money:
          return typeof (Decimal);
        case SqlDbType.NChar:
          return typeof (string);
        case SqlDbType.NText:
          return typeof (string);
        case SqlDbType.NVarChar:
          return typeof (string);
        case SqlDbType.Real:
          return typeof (float);
        case SqlDbType.UniqueIdentifier:
          return typeof (Guid);
        case SqlDbType.SmallDateTime:
          return typeof (DateTime);
        case SqlDbType.SmallInt:
          return typeof (short);
        case SqlDbType.SmallMoney:
          return typeof (Decimal);
        case SqlDbType.Text:
          return typeof (string);
        case SqlDbType.Timestamp:
          return typeof (byte[]);
        case SqlDbType.TinyInt:
          return typeof (byte);
        case SqlDbType.VarBinary:
          return typeof (byte[]);
        case SqlDbType.VarChar:
          return typeof (string);
        case SqlDbType.Variant:
          return typeof (object);
        case SqlDbType.Xml:
          return typeof (string);
        case SqlDbType.Udt:
          return typeof (object);
        case SqlDbType.Structured:
          return typeof (object);
        case SqlDbType.Date:
          return typeof (DateTime);
        case SqlDbType.Time:
          return typeof (DateTime);
        case SqlDbType.DateTime2:
          return typeof (DateTime);
        case SqlDbType.DateTimeOffset:
          return typeof (DateTime);
        default:
          return (Type) null;
      }
    }
  }
}
