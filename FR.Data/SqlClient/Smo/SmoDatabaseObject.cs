// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDatabaseObject
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace FR.Data.SqlClient.Smo
{
  public abstract class SmoDatabaseObject : SmoObject
  {
    internal int _id;
    private SmoSchema _schema;
    private string _name;

    public SmoDatabase Database => (SmoDatabase) this.ParentObject;

    public override int Id => this._id;

    public SmoSchema Schema
    {
      get => this._schema;
      set
      {
        this._schema = value;
        this.SetState(SmoObjectState.Changed);
      }
    }

    public override string Name
    {
      get => this._name;
      set
      {
        this._name = value;
        if (new Regex("^(\\[?[a-zA-Z0-9\\ ]{0,256}\\]?\\.\\[?[a-zA-Z0-9\\ ]{0,256}\\]?)$").IsMatch(this._name))
        {
          this._name = this._name.Replace("[", "");
          this._name = this._name.Replace("]", "");
          string[] strArray = this._name.Split(new string[1]
          {
            "."
          }, StringSplitOptions.None);
          string name = strArray[0];
          string str = strArray[1];
          SmoSchema schema = this.Database.Schemas.FindByName(name);
          if (schema == null)
          {
            schema = new SmoSchema(this.Database);
            schema.Name = name;
            this.Database.Schemas.Add(schema);
          }
          this._schema = schema;
          this._name = str;
        }
        this.SetState(SmoObjectState.Changed);
      }
    }

    public virtual string FullName => string.Format("[{0}].[{1}].[{2}]", (object) this.Database.Name, (object) this.Schema.Name, (object) this.Name);

    internal SmoDatabaseObject(SmoDatabase database, DataRow infoRow)
      : base((SmoObject) database, infoRow)
    {
      if (infoRow.Table.Columns.Contains("object_id"))
        this._id = (int) this._infoRow["object_id"];
      this._name = (string) this._infoRow["name"];
      this._schema = this.Database.Schemas.FindById((int) this._infoRow["schema_id"]);
      this.SetState(SmoObjectState.Original);
    }

    public SmoDatabaseObject(SmoDatabase database)
      : base((SmoObject) database, (DataRow) null)
    {
      this._id = 0;
      this._name = "";
      this._schema = this.Database.Schemas.DefaultSchema;
      this.SetState(SmoObjectState.New);
    }

    public override string GetName() => this.GetName((SmoScriptOptions) null);

    public override string GetName(SmoScriptOptions options)
    {
      if (options == null)
        options = new SmoScriptOptions();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.Schema.GetName(options));
      if (stringBuilder.Length != 0)
        stringBuilder.Append(".");
      stringBuilder.AppendFormat("[{0}]", (object) this.Name);
      return stringBuilder.ToString();
    }
  }
}
