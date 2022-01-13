// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.IndexValueEnumerator`1
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace FR.Collections.Generic {
    public class IndexValueEnumerator<TValue> :
      IEnumerator<TValue>,
      IDisposable,
      IEnumerator,
      IEnumerable<TValue>,
      IEnumerable {
        private IIndex _index;
        private IPrimaryKey _primaryKey;
        private IEnumerator _dataEnumerator;
        private IEnumerator<IIndexNode> _indexEnumerator;
        private IEnumerator _nodeValueEnumerator;

        public IndexValueEnumerator(IIndex index, IPrimaryKey primaryKey) {
            this._index = index;
            this._indexEnumerator = this._index.GetEnumerator();
            this._primaryKey = primaryKey;
            this.Reset();
        }

        public IndexValueEnumerator(IEnumerator dataEnumerator, IPrimaryKey primaryKey) {
            this._dataEnumerator = dataEnumerator;
            this._primaryKey = primaryKey;
            this.Reset();
        }

        public TValue Current {
            get {
                if (this._primaryKey != null)
                    return (TValue)(this._dataEnumerator == null ? (IPrimaryKeyNode)this._nodeValueEnumerator.Current : (IPrimaryKeyNode)this._dataEnumerator.Current).Value;
                return this._dataEnumerator != null ? (TValue)this._dataEnumerator : (TValue)this._nodeValueEnumerator.Current;
            }
        }

        public void Dispose() {
            this._index = (IIndex)null;
            this._primaryKey = (IPrimaryKey)null;
            this._indexEnumerator = (IEnumerator<IIndexNode>)null;
            this._nodeValueEnumerator = (IEnumerator)null;
        }

        public IEnumerator<TValue> GetEnumerator() => (IEnumerator<TValue>)this;

        public bool MoveNext() {
            bool flag = false;
            if (this._dataEnumerator != null)
                flag = this._dataEnumerator.MoveNext();
            else if (this._nodeValueEnumerator != null && this._nodeValueEnumerator.MoveNext()) {
                flag = true;
            }
            else {
                while (this._indexEnumerator.MoveNext()) {
                    this._nodeValueEnumerator = this._indexEnumerator.Current.Values.GetEnumerator();
                    flag = this._nodeValueEnumerator.MoveNext();
                    if (flag)
                        break;
                }
            }
            return flag;
        }

        public void Reset() {
            if (this._dataEnumerator != null) {
                this._dataEnumerator.Reset();
            }
            else {
                this._indexEnumerator.Reset();
                this._nodeValueEnumerator = (IEnumerator)null;
            }
        }

        object IEnumerator.Current => (object)this.Current;

        bool IEnumerator.MoveNext() => this.MoveNext();

        void IEnumerator.Reset() => this.Reset();

        TValue IEnumerator<TValue>.Current => this.Current;

        void IDisposable.Dispose() => this.Dispose();

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)this.GetEnumerator();
    }
}
