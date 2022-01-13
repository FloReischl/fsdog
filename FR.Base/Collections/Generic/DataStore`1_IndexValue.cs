using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FR.Collections.Generic {
    public partial class DataStore<TItem> {
        private class IndexValue {
            private IIndex _index;
            private bool _isPublic;

            public IndexValue(IIndex index, bool isPublic) {
                this._index = index;
                this._isPublic = isPublic;
            }

            public IIndex Index {
                [DebuggerNonUserCode]
                get => this._index;
            }

            public bool IsPublic {
                [DebuggerNonUserCode]
                get => this._isPublic;
                [DebuggerNonUserCode]
                set => this._isPublic = value;
            }
        }
    }
}
