using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FR.Collections.Generic {
    public partial class DataStore<TItem> {
        private class PrimaryKeyEntry {
            public object Key;
            public TItem Value;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private Dictionary<IIndex, object> _indexRelations;

            public PrimaryKeyEntry(object key, TItem value) {
                this.Key = key;
                this.Value = value;
            }

            private Dictionary<IIndex, object> IndexRelations {
                [DebuggerNonUserCode]
                get {
                    if (this._indexRelations == null)
                        this._indexRelations = new Dictionary<IIndex, object>();
                    return this._indexRelations;
                }
            }

            public void AddIndexRelation(IIndex index, object key) {
                if (this.IndexRelations.ContainsKey(index))
                    this.IndexRelations.Remove(index);
                this.IndexRelations.Add(index, key);
            }

            public object GetIndexRelation(IIndex index) => this.IndexRelations[index];

            public void RemoveIndexRelation(IIndex index) => this._indexRelations.Remove(index);
        }
    }
}
