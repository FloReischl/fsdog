// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoTableColumnCollection
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;

namespace FR.Data.SqlClient.Smo
{
  public class SmoTableColumnCollection : SmoCollection<SmoTableColumn>
  {
    internal SmoTableColumnCollection(SmoTable table)
      : base((SmoObject) table)
    {
    }

    public int Add(SmoTableColumn column)
    {
      this.add(column);
      this._parent.setState(SmoObjectState.Changed);
      return this.Count;
    }

    public SmoTableColumn Add(string name, SqlDbType sqlType, int maximalSize)
    {
      SmoTableColumn column = new SmoTableColumn((SmoTable) this._parent);
      column.SqlDbType = sqlType;
      column.MaximumSize = maximalSize;
      this.Add(column);
      return column;
    }

    public void Remove(SmoTableColumn column)
    {
      foreach (SmoTableColumn smoTableColumn in (SmoCollection<SmoTableColumn>) this)
      {
        if (smoTableColumn.Equals((object) column))
          smoTableColumn.setState(SmoObjectState.Deleted);
      }
    }

    public override SmoTableColumn FindByName(string name, SmoScriptOptions options)
    {
      SmoTableColumn byName = base.FindByName(name, options);
      return byName != null && byName.State != SmoObjectState.Deleted ? byName : (SmoTableColumn) null;
    }
  }
}
