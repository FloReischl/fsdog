// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDataType
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.Data;

namespace FR.Data.SqlClient.Smo
{
  public class SmoDataType : SmoDatabaseObject
  {
    public override int Id => (int) this._infoRow["user_type_id"];

    public int SystemTypeId => (int) this._infoRow["system_type_id"];

    public int MaxSize => (int) this._infoRow["max_length"];

    public int Precision => (int) this._infoRow["precision"];

    public int Scale => (int) this._infoRow["scale"];

    public string CollationName => (string) this._infoRow["collation_name"];

    public bool IsNullable => (bool) this._infoRow["is_nullable"];

    public bool IsUserDefined => (bool) this._infoRow["is_user_defined"];

    public bool IsAssemblyType => (bool) this._infoRow["is_assembly_type"];

    public SmoConstraint DefaultConstraint => (SmoConstraint) null;

    public override string FullName => this.Name;

    public SqlDbType SqlDbType
    {
      get
      {
        switch (this.Name.ToLower())
        {
          case "bigint":
            return SqlDbType.BigInt;
          case "binary":
            return SqlDbType.Binary;
          case "bit":
            return SqlDbType.Bit;
          case "char":
            return SqlDbType.Char;
          case "datetime":
            return SqlDbType.DateTime;
          case "decimal":
            return SqlDbType.Decimal;
          case "float":
            return SqlDbType.Float;
          case "image":
            return SqlDbType.Image;
          case "int":
            return SqlDbType.Int;
          case "money":
            return SqlDbType.Money;
          case "nchar":
            return SqlDbType.NChar;
          case "ntext":
            return SqlDbType.NText;
          case "numeric":
            return SqlDbType.Decimal;
          case "nvarchar":
            return SqlDbType.NVarChar;
          case "real":
            return SqlDbType.Real;
          case "smalldatetime":
            return SqlDbType.SmallDateTime;
          case "smallint":
            return SqlDbType.SmallInt;
          case "smallmoney":
            return SqlDbType.SmallMoney;
          case "sql_variant":
            return SqlDbType.Variant;
          case "text":
            return SqlDbType.Text;
          case "timestamp":
            return SqlDbType.Timestamp;
          case "tinyint":
            return SqlDbType.TinyInt;
          case "uniqueidentifier":
            return SqlDbType.UniqueIdentifier;
          case "varbinary":
            return SqlDbType.VarBinary;
          case "varchar":
            return SqlDbType.VarChar;
          case "xml":
            return SqlDbType.Xml;
          default:
            return SqlDbType.Udt;
        }
      }
    }

    public Type Type => SqlTypeHelper.GetType(this.SqlDbType);

    public bool IsNumeric
    {
      get
      {
        switch (this.SqlDbType)
        {
          case SqlDbType.BigInt:
          case SqlDbType.Decimal:
          case SqlDbType.Float:
          case SqlDbType.Int:
          case SqlDbType.Money:
          case SqlDbType.Real:
          case SqlDbType.SmallInt:
          case SqlDbType.SmallMoney:
          case SqlDbType.TinyInt:
            return true;
          default:
            return false;
        }
      }
    }

    public bool IsString
    {
      get
      {
        switch (this.SqlDbType)
        {
          case SqlDbType.Char:
          case SqlDbType.NChar:
          case SqlDbType.NText:
          case SqlDbType.NVarChar:
          case SqlDbType.Text:
          case SqlDbType.VarChar:
          case SqlDbType.Xml:
            return true;
          default:
            return false;
        }
      }
    }

    public bool IsDateTime
    {
      get
      {
        switch (this.SqlDbType)
        {
          case SqlDbType.DateTime:
          case SqlDbType.SmallDateTime:
            return true;
          default:
            return false;
        }
      }
    }

    public bool IsBinary
    {
      get
      {
        switch (this.SqlDbType)
        {
          case SqlDbType.Binary:
          case SqlDbType.Image:
          case SqlDbType.Timestamp:
          case SqlDbType.VarBinary:
          case SqlDbType.Variant:
            return true;
          default:
            return false;
        }
      }
    }

    public bool IsFixedSize
    {
      get
      {
        switch (this.SqlDbType)
        {
          case SqlDbType.NText:
          case SqlDbType.NVarChar:
          case SqlDbType.Text:
          case SqlDbType.VarChar:
          case SqlDbType.Xml:
            return false;
          default:
            return true;
        }
      }
    }

    public bool IsSizable
    {
      get
      {
        switch (this.SqlDbType)
        {
          case SqlDbType.Char:
          case SqlDbType.NChar:
          case SqlDbType.NVarChar:
          case SqlDbType.VarBinary:
          case SqlDbType.VarChar:
            return true;
          default:
            return false;
        }
      }
    }

    public SmoDataType(SmoDatabase database, DataRow infoRow)
      : base(database, infoRow)
    {
      this._id = (int) infoRow["user_type_id"];
    }

    public override string GetCreateStatement(object scriptOptions) => throw SmoExceptionHelper.GetNotImplemented("The method GetCreateStatement is not implemented.");
  }
}
