// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoSchema
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Text;

namespace FR.Data.SqlClient.Smo
{
  public class SmoSchema : SmoObject
  {
    private string _name;
    private SmoDatabasePrincipal _owner;

    public SmoDatabase Database => (SmoDatabase) this.ParentObject;

    public override string Name
    {
      get => this._name;
      set
      {
        this._name = value;
        this.SetState(SmoObjectState.Changed);
      }
    }

    public override int Id => (int) this._infoRow["schema_id"];

    public SmoDatabasePrincipal Owner
    {
      get => this._owner;
      set
      {
        this._owner = value;
        this.SetState(SmoObjectState.Changed);
      }
    }

    internal SmoSchema(SmoDatabase database, DataRow infoRow)
      : base((SmoObject) database, infoRow)
    {
      this._name = (string) this._infoRow["name"];
      this._owner = this.Database.Principals.FindById((int) this._infoRow["principal_id"]);
    }

    public SmoSchema(SmoDatabase database)
      : base((SmoObject) database, (DataRow) null)
    {
      this._owner = this.Database.Principals.FindByName((string) this.GetDataTable(CommandType.Text, "SELECT CURRENT_USER Name").Rows[0][nameof (Name)]);
      this.SetState(SmoObjectState.New);
    }

    public override string GetCreateStatement(object scriptOptions)
    {
      SmoSchemaScriptOptions options = scriptOptions != null ? (SmoSchemaScriptOptions) scriptOptions : new SmoSchemaScriptOptions();
      options.Database = false;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("CREATE SCHEMA {0}", (object) this.GetName((SmoScriptOptions) options));
      if (options.Owner)
        stringBuilder.AppendFormat(" AUTHORIZATION {0}", (object) this.Owner.GetName((SmoScriptOptions) options));
      return stringBuilder.ToString();
    }

    public override string GetName(SmoScriptOptions options)
    {
      if (options == null)
        options = new SmoScriptOptions();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.Database.GetName(options));
      if (stringBuilder.Length != 0)
        stringBuilder.Append(".");
      stringBuilder.AppendFormat("[{0}]", (object) this.Name);
      return stringBuilder.ToString();
    }
  }
}
