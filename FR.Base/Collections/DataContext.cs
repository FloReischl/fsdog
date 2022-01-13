// Decompiled with JetBrains decompiler
// Type: FR.Collections.DataContext
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Diagnostics;

namespace FR.Collections
{
  public class DataContext : IDictionary, ICollection, IEnumerable
  {
    private Hashtable _ht;

    public DataContext() => this._ht = new Hashtable();

    public DataContext(bool synchronized)
      : this()
    {
      if (!synchronized)
        return;
      this._ht = Hashtable.Synchronized(this._ht);
    }

    public object this[object key]
    {
      [DebuggerNonUserCode] get => this._ht[key];
      [DebuggerNonUserCode] set => this._ht[key] = value;
    }

    public ICollection Values => this._ht.Values;

    public int Count => this._ht.Count;

    public bool IsSynchronized => this._ht.IsSynchronized;

    public object SyncRoot => this._ht.SyncRoot;

    public bool IsFixedSize => this._ht.IsFixedSize;

    public bool IsReadOnly => this._ht.IsReadOnly;

    public ICollection Keys => this._ht.Keys;

    public virtual void Add(object key, object value) => this._ht.Add(key, value);

    public virtual void Clear() => this._ht.Clear();

    public virtual bool ContainsKey(object key) => this._ht.ContainsKey(key);

    public virtual void CopyTo(Array array, int index) => this._ht.CopyTo(array, index);

    public virtual IDictionaryEnumerator GetEnumerator() => this._ht.GetEnumerator();

    public virtual void Remove(object key) => this._ht.Remove(key);

    public virtual bool GetAsBoolean(object key) => (bool) this._ht[key];

    public virtual byte GetAsByte(object key) => (byte) this._ht[key];

    public virtual byte[] GetAsByteArray(object key) => (byte[]) this._ht[key];

    public virtual DateTime GetAsDateTime(object key) => (DateTime) this._ht[key];

    public virtual short GetAsInt16(object key) => (short) this._ht[key];

    public virtual int GetAsInt32(object key) => (int) this._ht[key];

    public virtual long GetAsInt64(object key) => (long) this._ht[key];

    public virtual object GetAsObject(object key) => this._ht[key];

    public virtual string GetAsString(object key) => (string) this._ht[key];

    public virtual Type GetAsType(object key) => (Type) this._ht[key];

    public virtual ushort GetAsUInt16(object key) => (ushort) this._ht[key];

    public virtual uint GetAsUInt32(object key) => (uint) this._ht[key];

    public virtual ulong GetAsUInt64(object key) => (ulong) this._ht[key];

    void IDictionary.Add(object key, object value) => this.Add(key, value);

    void IDictionary.Clear() => this.Clear();

    bool IDictionary.Contains(object key) => this.ContainsKey(key);

    IDictionaryEnumerator IDictionary.GetEnumerator() => this.GetEnumerator();

    bool IDictionary.IsFixedSize => this.IsFixedSize;

    bool IDictionary.IsReadOnly => this.IsReadOnly;

    ICollection IDictionary.Keys => this.Keys;

    void IDictionary.Remove(object key) => this.Remove(key);

    ICollection IDictionary.Values => this.Values;

    object IDictionary.this[object key]
    {
      get => this[key];
      set => this[key] = value;
    }

    void ICollection.CopyTo(Array array, int index) => this.CopyTo(array, index);

    int ICollection.Count => this.Count;

    bool ICollection.IsSynchronized => this.IsSynchronized;

    object ICollection.SyncRoot => this.SyncRoot;

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
