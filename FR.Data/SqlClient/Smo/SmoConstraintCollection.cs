// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoConstraintCollection
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

namespace FR.Data.SqlClient.Smo
{
  public class SmoConstraintCollection : SmoCollection<SmoConstraint>
  {
    internal SmoConstraintCollection(SmoTable table)
      : base((SmoObject) table)
    {
    }

    public int Add(SmoConstraint constraint)
    {
      this.add(constraint);
      return this.Count;
    }

    public void Remove(SmoConstraint constraint)
    {
      foreach (SmoConstraint smoConstraint in (SmoCollection<SmoConstraint>) this)
      {
        if (smoConstraint.Equals((object) constraint))
          smoConstraint.setState(SmoObjectState.Deleted);
      }
    }

    public override SmoConstraint FindByName(string name, SmoScriptOptions options)
    {
      SmoConstraint byName = base.FindByName(name, options);
      return byName != null && byName.State != SmoObjectState.Deleted ? byName : (SmoConstraint) null;
    }
  }
}
