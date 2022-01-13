// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.HashQueue`2
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Collections;

namespace FR.Collections.Generic {
    public class HashQueue<TKey, TValue> : HashQueue {
        public HashQueue()
          : this(100, false) {
        }

        public HashQueue(int capacity)
          : this(capacity, false) {
        }

        public HashQueue(bool synchronized)
          : this(100, synchronized) {
        }

        public HashQueue(int capacity, bool synchronized)
          : base(capacity, synchronized) {
        }

        public TValue this[TKey key] => (TValue)this[(object)key];

        public override void Enqueue(object key, object value) => this.Enqueue((TKey)key, (TValue)value);

        public void Enqueue(TKey key, TValue value) => base.Enqueue((object)key, (object)value);

        public new TValue Dequeue() => (TValue)base.Dequeue();

        public TValue Find(TKey key) => this[key];

        public new TValue Peek() => (TValue)base.Peek();

        public override bool ContainsKey(object key) => this.ContainsKey((TKey)key);

        public virtual bool ContainsKey(TKey key) => base.ContainsKey((object)key);

        public override IDictionaryEnumerator GetEnumerator() => (IDictionaryEnumerator)new DictionaryEnumerator<TKey, TValue>(base.GetEnumerator());

        public override object Dequeue(object key) => (object)this.Dequeue((TKey)key);

        public TValue Dequeue(TKey key) => (TValue)base.Dequeue((object)key);
    }
}
