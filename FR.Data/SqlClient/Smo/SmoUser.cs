// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoUser
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.Data;
using System.Text;

namespace FR.Data.SqlClient.Smo
{
  public class SmoUser : SmoDatabasePrincipal
  {
    public string DefaultSchemaName
    {
      get
      {
        object obj = this._infoRow["default_schema_name"];
        return obj == DBNull.Value ? (string) null : (string) obj;
      }
    }

    public SmoLogin Login
    {
      get
      {
        object sid = this._infoRow["sid"];
        return sid == DBNull.Value ? (SmoLogin) null : this.Database.Server.FindLoginBySid((byte[]) sid);
      }
    }

    internal SmoUser(SmoDatabase database, DataRow infoRow)
      : base(database, infoRow)
    {
    }

    public override string GetCreateStatement(object scriptOptions)
    {
      SmoUserScriptOptions options = scriptOptions != null ? (SmoUserScriptOptions) scriptOptions : new SmoUserScriptOptions();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("CREATE USER {0} ", (object) this.GetName((SmoScriptOptions) options));
      if (this.Login != null)
      {
        stringBuilder.Append("FOR ");
        switch (this.Type)
        {
          case SmoDatabasePrincipalType.SqlUser:
            stringBuilder.Append("LOGIN ");
            break;
          case SmoDatabasePrincipalType.WindowsUser:
            stringBuilder.Append("LOGIN ");
            break;
          case SmoDatabasePrincipalType.WindowsGroup:
            stringBuilder.Append("LOGIN ");
            break;
          case SmoDatabasePrincipalType.CertificateMappedUser:
            stringBuilder.Append("CERTIFICATE ");
            break;
          case SmoDatabasePrincipalType.AsymmetricKeyMappedUser:
            stringBuilder.Append("ASYMMETRIC KEY ");
            break;
        }
        stringBuilder.AppendFormat("{0} ", (object) this.Login.GetName((SmoScriptOptions) options));
      }
      else
        stringBuilder.Append("WITHOUT LOGIN ");
      if (options.DefaultSchema && this.DefaultSchemaName != null)
        stringBuilder.AppendFormat("WITH DEFAULT_SCHEMA = {0}", (object) this.DefaultSchemaName);
      return stringBuilder.ToString();
    }
  }
}
