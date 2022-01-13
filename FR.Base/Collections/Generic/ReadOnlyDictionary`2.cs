// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.ReadOnlyDictionary`2
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace FR.Collections.Generic
{
  public class ReadOnlyDictionary<TKey, TValue> : 
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable
  {
    private IDictionary<TKey, TValue> _dict;

    public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary) => this._dict = dictionary;

    public TValue this[TKey key] => this._dict[key];

    public int Count => this._dict.Count;

    public ICollection<TKey> Keys => this._dict.Keys;

    public ICollection<TValue> Values => this._dict.Values;

    public bool Contains(KeyValuePair<TKey, TValue> item) => this.ContainsKey(item.Key);

    public bool ContainsKey(TKey key) => this._dict.ContainsKey(key);

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => this._dict.CopyTo(array, arrayIndex);

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this._dict.GetEnumerator();

    public bool TryGetValue(TKey key, out TValue value) => this._dict.TryGetValue(key, out value);

    void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => throw new Exception("The method or operation is not implemented.");

    bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => this.ContainsKey(key);

    ICollection<TKey> IDictionary<TKey, TValue>.Keys => this.Keys;

    bool IDictionary<TKey, TValue>.Remove(TKey key) => throw new Exception("The method or operation is not implemented.");

    bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) => this.TryGetValue(key, out value);

    ICollection<TValue> IDictionary<TKey, TValue>.Values => this.Values;

    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
      get => this[key];
      set => throw new Exception("The method or operation is not implemented.");
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Add(
      KeyValuePair<TKey, TValue> item)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Clear() => throw new Exception("The method or operation is not implemented.");

    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(
      KeyValuePair<TKey, TValue> item)
    {
      return this.Contains(item);
    }

    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
      KeyValuePair<TKey, TValue>[] array,
      int arrayIndex)
    {
      this.CopyTo(array, arrayIndex);
    }

    int ICollection<KeyValuePair<TKey, TValue>>.Count => this.Count;

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => true;

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(
      KeyValuePair<TKey, TValue> item)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => this.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
