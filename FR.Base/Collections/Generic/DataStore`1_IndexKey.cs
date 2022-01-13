using System.ComponentModel;
using System.Diagnostics;

namespace FR.Collections.Generic {
    public partial class DataStore<TItem> {
        private class IndexKey {
            private PropertyDescriptor _property;
            private ListSortDirection _sortDirection;

            public IndexKey(PropertyDescriptor property, ListSortDirection sortDirection) {
                this._property = property;
                this._sortDirection = sortDirection;
            }

            public PropertyDescriptor Property {
                [DebuggerNonUserCode]
                get => this._property;
            }

            public ListSortDirection SortDirection {
                [DebuggerNonUserCode]
                get => this._sortDirection;
            }

            public override bool Equals(object obj) => obj is DataStore<TItem>.IndexKey indexKey && indexKey.Property == this.Property && indexKey.SortDirection == this.SortDirection;

            public override int GetHashCode() => this.Property.GetHashCode();
        }
    }
}
