// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.DictionaryEnumerator`2
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace FR.Collections.Generic {
    public class DictionaryEnumerator<TKey, TValue> :
      IDictionaryEnumerator,
      IEnumerator<TValue>,
      IDisposable,
      IEnumerator {
        private IDictionaryEnumerator _enumerator;
        private bool _enumerateValues;

        public DictionaryEnumerator(IDictionaryEnumerator enumerator)
          : this(enumerator, false) {
        }

        public DictionaryEnumerator(IDictionaryEnumerator enumerator, bool enumerateValues) {
            this._enumerateValues = false;
            this._enumerator = enumerator;
        }

        public bool EnumerateValues {
            get => this._enumerateValues;
            set => this._enumerateValues = value;
        }

        public DictionaryEntry Entry => this._enumerator.Entry;

        public TKey Key => (TKey)this._enumerator.Key;

        public TValue Value => (TValue)this._enumerator.Value;

        public object Current => this._enumerateValues ? this._enumerator.Value : (object)new KeyValuePair<TKey, TValue>((TKey)this._enumerator.Entry.Key, (TValue)this._enumerator.Entry.Value);

        public bool MoveNext() => this._enumerator.MoveNext();

        public void Reset() => this._enumerator.Reset();

        DictionaryEntry IDictionaryEnumerator.Entry => this._enumerator.Entry;

        object IDictionaryEnumerator.Key => (object)this.Key;

        object IDictionaryEnumerator.Value => (object)this.Value;

        object IEnumerator.Current => this.Current;

        bool IEnumerator.MoveNext() => this.MoveNext();

        void IEnumerator.Reset() => this.Reset();

        TValue IEnumerator<TValue>.Current => this.Value;

        void IDisposable.Dispose() {
        }
    }
}
