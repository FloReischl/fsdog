// Decompiled with JetBrains decompiler
// Type: FsDog.DetailTableView
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace FsDog.Detail {
    internal class DetailTableView : DataView {
        public DetailTableView(DetailTable table, string sortColumn, SortOrder sortOrder)
          : base((DataTable)table) {
            if (sortOrder == SortOrder.None || string.IsNullOrEmpty(sortColumn))
                this.Sort = "SortOrder, Name";
            else if (sortOrder == SortOrder.Ascending) {
                this.Sort = string.Format("SortOrder, {0} ASC", (object)sortColumn);
            }
            else {
                if (sortOrder != SortOrder.Descending)
                    return;
                this.Sort = string.Format("SortOrder, {0} DESC", (object)sortColumn);
            }
        }

        public DetailTableView(
          DetailTable table,
          string filter,
          string sortColumn,
          SortOrder sortOrder)
          : this(table, sortColumn, sortOrder) {
            this.RowFilter = filter;
        }

        protected override void OnListChanged(ListChangedEventArgs e) {
            if (e.ListChangedType == ListChangedType.Reset) {
                if (string.IsNullOrEmpty(this.Sort) || this.Sort.Contains("SortOrder"))
                    return;
                this.Sort = string.Format("SortOrder, {0}", (object)this.Sort);
            }
            else
                base.OnListChanged(e);
        }
    }
}
