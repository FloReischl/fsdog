// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoCheckConstraint
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Text;

namespace FR.Data.SqlClient.Smo
{
  public class SmoCheckConstraint : SmoConstraint
  {
    public bool IsNotForReplication => (bool) this._infoRow["is_not_for_replication"];

    public string Definition => (string) this._infoRow["definition"];

    internal SmoCheckConstraint(SmoTable table, DataRow infoRow)
      : base(table, infoRow)
    {
    }

    public override string GetCreateStatement(object scriptOptions)
    {
      SmoConstraintScriptOptions options = scriptOptions != null ? (SmoConstraintScriptOptions) scriptOptions : new SmoConstraintScriptOptions();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("ALTER TABLE {0} \r\n", (object) this.Table.GetName((SmoScriptOptions) options));
      stringBuilder.AppendFormat("   ADD CONSTRAINT {0} \r\n", (object) this.GetName((SmoScriptOptions) options));
      stringBuilder.Append("   CHECK ");
      if (this.IsNotForReplication)
        stringBuilder.Append("NOT FOR REPLICATION ");
      stringBuilder.Append(this.Definition);
      return stringBuilder.ToString();
    }
  }
}
