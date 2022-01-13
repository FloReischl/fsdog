// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.Enumerator`1
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FR.Collections.Generic {
    public class Enumerator<T> : IEnumerator<T>, IDisposable, IEnumerator, IEnumerable<T>, IEnumerable {
        private IEnumerator _enumerator;
        private int _position;

        public Enumerator(IEnumerator enumerator) {
            this._enumerator = enumerator;
            this._position = -1;
        }

        public int Position => this._position;

        public virtual T Current => (T)this._enumerator.Current;

        public virtual void Dispose() {
            this._enumerator.Reset();
            this._enumerator = (IEnumerator)null;
        }

        public virtual IEnumerator<T> GetEnumerator() => (IEnumerator<T>)new Enumerator<T>(this._enumerator);

        public virtual bool MoveNext() {
            bool flag = this._enumerator.MoveNext();
            if (flag)
                ++this._position;
            else
                this._position = -1;
            return flag;
        }

        public virtual void Reset() {
            this._enumerator.Reset();
            this._position = -1;
        }

        T IEnumerator<T>.Current {
            [DebuggerNonUserCode]
            get => this.Current;
        }

        [DebuggerNonUserCode]
        void IDisposable.Dispose() => this.Dispose();

        object IEnumerator.Current {
            [DebuggerNonUserCode]
            get => (object)this.Current;
        }

        [DebuggerNonUserCode]
        bool IEnumerator.MoveNext() => this.MoveNext();

        [DebuggerNonUserCode]
        void IEnumerator.Reset() => this.Reset();

        [DebuggerNonUserCode]
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.GetEnumerator();

        [DebuggerNonUserCode]
        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)this.GetEnumerator();
    }
}
