// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDatabaseRole
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace FR.Data.SqlClient.Smo
{
  [DebuggerDisplay("{Name}")]
  public class SmoDatabaseRole : SmoDatabasePrincipal
  {
    public SmoUser ParentUser
    {
      get
      {
        object id = this._infoRow["owning_principal_id"];
        return id == DBNull.Value ? (SmoUser) null : this.Database.Users.FindById((int) id);
      }
    }

    internal SmoDatabaseRole(SmoDatabase database, DataRow infoRow)
      : base(database, infoRow)
    {
    }

    public override string GetCreateStatement(object scriptOptions)
    {
      SmoDatabaseRoleScriptOptions options = scriptOptions != null ? (SmoDatabaseRoleScriptOptions) scriptOptions : new SmoDatabaseRoleScriptOptions();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("CREATE ROLE {0}", (object) this.GetName((SmoScriptOptions) options));
      if (this.ParentUser != null && options.Owner)
        stringBuilder.AppendFormat(" AUTHORIZATION {0}", (object) this.ParentUser.GetName((SmoScriptOptions) options));
      return stringBuilder.ToString();
    }
  }
}
