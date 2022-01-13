// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoConstraint
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;

namespace FR.Data.SqlClient.Smo
{
  public abstract class SmoConstraint : SmoObject
  {
    private string _name;
    private SmoConstraintType _type;

    public SmoTable Table => (SmoTable) this.ParentObject;

    public override string Name
    {
      get => this._name;
      set
      {
        if (object.Equals((object) value, (object) this._name))
          return;
        this.setState(SmoObjectState.Changed);
        this._name = value;
      }
    }

    public override int Id => (int) this._infoRow["object_id"];

    public SmoConstraintType Type
    {
      get => this._type;
      set
      {
        if (this._type == value)
          return;
        this.setState(SmoObjectState.Changed);
        this._type = value;
      }
    }

    internal SmoConstraint(SmoTable table, DataRow infoRow)
      : base((SmoObject) table, infoRow)
    {
      if (this._infoRow == null)
        return;
      this._name = (string) infoRow["name"];
      switch (((string) this._infoRow["type"]).ToUpper().Trim())
      {
        case "PK":
          this._type = SmoConstraintType.PrimaryKey;
          break;
        case "F":
          this._type = SmoConstraintType.ForeignKey;
          break;
        case "UQ":
          this._type = SmoConstraintType.Unique;
          break;
        case "C":
          this._type = SmoConstraintType.Check;
          break;
        default:
          throw SmoExceptionHelper.GetNotImplemented("Unknown handling for constraint type '{0}'", this._infoRow["type"]);
      }
    }
  }
}
