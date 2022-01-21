// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoIndexColumn
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Diagnostics;

namespace FR.Data.SqlClient.Smo
{
  public class SmoIndexColumn : SmoObject
  {
    private SmoTableColumn _tableColumn;
    private SmoViewColumn _viewColumn;
    private int _id;
    private bool _isDecending;
    private bool _isIncluded;

    internal SmoIndexColumn(SmoIndex index, DataRow infoRow)
      : base((SmoObject) index, infoRow)
    {
      this._tableColumn = this.Index.Table.Columns.FindById((int) this._infoRow["column_id"]);
      this._id = (int) this._infoRow["index_column_id"];
      this._isDecending = (bool) this._infoRow["is_descending_key"];
      this._isIncluded = (bool) this._infoRow["is_included_column"];
    }

    public SmoIndexColumn(SmoIndex index, ref SmoTableColumn tableColumn)
      : base((SmoObject) index, (DataRow) null)
    {
      this._tableColumn = tableColumn;
      this.SetState(SmoObjectState.New);
    }

    public SmoIndexColumn(SmoIndex index, ref SmoViewColumn viewColumn)
      : base((SmoObject) index, (DataRow) null)
    {
      this._viewColumn = viewColumn;
      this.SetState(SmoObjectState.New);
    }

    public SmoTableColumn TableColumn
    {
      get => this._tableColumn;
      set
      {
        if (object.Equals((object) value, (object) this._tableColumn))
          return;
        this.SetState(SmoObjectState.Changed);
        this._tableColumn = value;
      }
    }

    public SmoViewColumn ViewColumn
    {
      [DebuggerNonUserCode] get => this._viewColumn;
      [DebuggerNonUserCode] set
      {
        if (object.Equals((object) value, (object) this._viewColumn))
          return;
        this.SetState(SmoObjectState.Changed);
        this._viewColumn = value;
      }
    }

    public SmoIndex Index => (SmoIndex) this.ParentObject;

    public override int Id => this._id;

    public override string Name => this.TableColumn.Name;

    public bool IsDecending
    {
      get => this._isDecending;
      set
      {
        if (value == this._isDecending)
          return;
        this.SetState(SmoObjectState.Changed);
        this._isDecending = value;
      }
    }

    public bool IsIncluded
    {
      get => this._isIncluded;
      set
      {
        if (value == this._isIncluded)
          return;
        this.SetState(SmoObjectState.Changed);
        this._isIncluded = value;
      }
    }

    public override string GetCreateStatement(object scriptOptions)
    {
      if (this.IsIncluded)
        return this.Name;
      return this.IsDecending ? string.Format("[{0}] DESC", (object) this.Name) : string.Format("[{0}] ASC", (object) this.Name);
    }
  }
}
