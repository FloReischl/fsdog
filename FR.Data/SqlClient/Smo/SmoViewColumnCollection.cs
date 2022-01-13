// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoViewColumnCollection
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

namespace FR.Data.SqlClient.Smo
{
  public class SmoViewColumnCollection : SmoCollection<SmoViewColumn>
  {
    internal SmoViewColumnCollection(SmoView view)
      : base((SmoObject) view)
    {
    }

    public override SmoViewColumn FindByName(string name, SmoScriptOptions options)
    {
      SmoViewColumn byName = base.FindByName(name, options);
      return byName != null && byName.State != SmoObjectState.Deleted ? byName : (SmoViewColumn) null;
    }
  }
}
