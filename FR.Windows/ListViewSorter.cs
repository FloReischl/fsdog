// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.ListViewSorter
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System.Collections;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class ListViewSorter : IComparer
  {
    private ColumnHeader _column;
    private SortOrder _sortOrder;

    public ColumnHeader Column
    {
      get => this._column;
      set => this._column = value;
    }

    public SortOrder SortOrder
    {
      get => this._sortOrder;
      set => this._sortOrder = value;
    }

    public int Compare(object x, object y)
    {
      if (!(x is ListViewItem) || !(y is ListViewItem))
        return 0;
      ListViewItem listViewItem1;
      ListViewItem listViewItem2;
      if (this.SortOrder != SortOrder.Descending)
      {
        listViewItem1 = (ListViewItem) x;
        listViewItem2 = (ListViewItem) y;
      }
      else
      {
        listViewItem1 = (ListViewItem) y;
        listViewItem2 = (ListViewItem) x;
      }
      return this._column == null ? 0 : listViewItem1.SubItems[this._column.Index].Text.CompareTo(listViewItem2.SubItems[this._column.Index].Text);
    }
  }
}
