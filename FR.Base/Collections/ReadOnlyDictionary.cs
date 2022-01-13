// Decompiled with JetBrains decompiler
// Type: FR.Collections.ReadOnlyDictionary
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Diagnostics;

namespace FR.Collections
{
  public class ReadOnlyDictionary : IDictionary, ICollection, IEnumerable
  {
    private IDictionary _dict;

    public ReadOnlyDictionary(IDictionary dict) => this._dict = dict;

    public virtual object this[object key] => this.Dictionary[key];

    public int Count
    {
      [DebuggerNonUserCode] get => this.Dictionary.Count;
    }

    public bool IsFixedSize
    {
      [DebuggerNonUserCode] get => true;
    }

    public bool IsReadOnly
    {
      [DebuggerNonUserCode] get => true;
    }

    public bool IsSynchronized
    {
      [DebuggerNonUserCode] get => this.Dictionary.IsSynchronized;
    }

    public ICollection Keys
    {
      [DebuggerNonUserCode] get => this.Dictionary.Keys;
    }

    public object SyncRoot
    {
      [DebuggerNonUserCode] get => this.Dictionary.SyncRoot;
    }

    public ICollection Values
    {
      [DebuggerNonUserCode] get => this.Dictionary.Values;
    }

    protected IDictionary Dictionary => this._dict;

    public bool Contains(object key) => this.Dictionary.Contains(key);

    public bool ContainsKey(object key) => this.Dictionary.Contains(key);

    public bool ContainsValue(object value)
    {
      foreach (object obj in (IEnumerable) this.Dictionary.Values)
      {
        if (value.Equals(obj))
          return true;
      }
      return false;
    }

    public void CopyTo(Array array, int index) => this.Dictionary.CopyTo(array, index);

    public virtual IDictionaryEnumerator GetEnumerator() => this.Dictionary.GetEnumerator();

    public override string ToString() => this.Dictionary.ToString();

    void ICollection.CopyTo(Array array, int index) => this.CopyTo(array, index);

    int ICollection.Count => this.Count;

    bool ICollection.IsSynchronized => this.IsSynchronized;

    object ICollection.SyncRoot => this.SyncRoot;

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    void IDictionary.Add(object key, object value) => throw new NotSupportedException("Adding objects is not supported for a read-only dictionary");

    void IDictionary.Clear() => throw new NotSupportedException("Removing objects is not supported for a read-only dictionary");

    bool IDictionary.Contains(object key) => this.Contains(key);

    IDictionaryEnumerator IDictionary.GetEnumerator() => this.GetEnumerator();

    bool IDictionary.IsFixedSize => this.IsFixedSize;

    bool IDictionary.IsReadOnly => this.IsReadOnly;

    ICollection IDictionary.Keys => this.Keys;

    void IDictionary.Remove(object key) => throw new NotSupportedException("Removing objects is not supported for a read-only dictionary");

    ICollection IDictionary.Values => this.Values;

    object IDictionary.this[object key]
    {
      get => this[key];
      set => throw new NotSupportedException("Changing objects is not supported for a read-only dictionary");
    }
  }
}
