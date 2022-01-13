// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.PrimaryKeyDictionary`2
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace FR.Collections.Generic
{
  [Serializable]
  public class PrimaryKeyDictionary<TKey, TValue> : 
    IPrimaryKey,
    IIndex,
    IEnumerable<IIndexNode>,
    IEnumerable,
    ICloneable,
    ISerializable
  {
    private Dictionary<TKey, PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode> _dict;
    private List<PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode> _list;
    private IEqualityComparer<TKey> _comparer;

    public PrimaryKeyDictionary(IEqualityComparer<TKey> comparer)
    {
      this._comparer = comparer;
      this._dict = new Dictionary<TKey, PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode>(comparer);
      this._list = new List<PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode>();
    }

    public PrimaryKeyDictionary(SerializationInfo info, StreamingContext context) => this._comparer = (IEqualityComparer<TKey>) info.GetValue(nameof (Comparer), typeof (IEqualityComparer<TKey>));

    public int Count
    {
      [DebuggerNonUserCode] get => this._dict.Count;
    }

    public IEqualityComparer<TKey> Comparer
    {
      [DebuggerNonUserCode] get => this._comparer;
    }

    public ListSortDirection SortDirection
    {
      [DebuggerNonUserCode] get => ListSortDirection.Ascending;
    }

    public PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode Add(
      TKey key,
      TValue value)
    {
      PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode primaryKeyNode = !this._dict.ContainsKey(key) ? new PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode(key, value) : throw IndexExceptionHelper.GetPrimaryKeyViolation((object) key);
      primaryKeyNode.SetIndex(this._list.Count);
      this._list.Add(primaryKeyNode);
      this._dict.Add(key, primaryKeyNode);
      return primaryKeyNode;
    }

    public void Clear()
    {
      this._dict.Clear();
      this._list.Clear();
    }

    public PrimaryKeyDictionary<TKey, TValue> Clone() => new PrimaryKeyDictionary<TKey, TValue>(this.Comparer);

    public TValue[] Find(TKey key, IndexFindOption option)
    {
      if (option == IndexFindOption.Equal)
      {
        if (!this._dict.ContainsKey(key))
          return new TValue[0];
        return new TValue[1]{ this.FindValue(key) };
      }
      throw ExceptionHelper.GetNotImplemented("{0} '{1}' is not available for {2}", (object) typeof (IndexFindOption), (object) option, (object) this.GetType());
    }

    public PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode FindNode(TKey key)
    {
      PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode node;
      this._dict.TryGetValue(key, out node);
      return node;
    }

    public TValue FindValue(TKey key)
    {
      PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode primaryKeyNode;
      return this._dict.TryGetValue(key, out primaryKeyNode) ? primaryKeyNode.Value : default (TValue);
    }

    public TValue[] GetAll(ListSortDirection direction)
    {
      TValue[] all = new TValue[this.Count];
      if (direction == ListSortDirection.Ascending)
      {
        int num = 0;
        foreach (PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode primaryKeyNode in this._list)
          all[num++] = primaryKeyNode.Value;
      }
      else
      {
        int num = this.Count - 1;
        foreach (PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode primaryKeyNode in this._list)
          all[num--] = primaryKeyNode.Value;
      }
      return all;
    }

    public PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode GetNodeAt(int index) => this._list[index];

    public IEnumerator<PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode> GetEnumerator() => (IEnumerator<PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode>) this._list.GetEnumerator();

    public TValue GetValueAt(int index) => this._list[index].Value;

    public int IndexOf(TKey key)
    {
      PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode node = this.FindNode(key);
      return node != null ? node.Index : -1;
    }

    public int Remove(TKey key)
    {
      PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode node = this.FindNode(key);
      if (node == null)
        return 0;
      this._list.RemoveAt(node.Index);
      this._dict.Remove(key);
      for (int index = node.Index; index < this._list.Count; ++index)
      {
        PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode primaryKeyNode = this._list[index];
        primaryKeyNode.SetIndex(primaryKeyNode.Index - 1);
      }
      return 1;
    }

    protected virtual void GetObjectData(SerializationInfo info, StreamingContext context) => info.AddValue("Comparer", (object) this.Comparer);

    [DebuggerNonUserCode]
    IPrimaryKey IPrimaryKey.Clone(ListSortDirection sortDirection) => (IPrimaryKey) this.Clone();

    int IPrimaryKey.Count
    {
      [DebuggerNonUserCode] get => this.Count;
    }

    [DebuggerNonUserCode]
    IPrimaryKeyNode IPrimaryKey.FindNode(object key) => (IPrimaryKeyNode) this.FindNode((TKey) key);

    [DebuggerNonUserCode]
    object IPrimaryKey.FindValue(object key) => (object) this.FindValue((TKey) key);

    [DebuggerNonUserCode]
    IPrimaryKeyNode IPrimaryKey.GetNodeAt(int index) => (IPrimaryKeyNode) this.GetNodeAt(index);

    [DebuggerNonUserCode]
    int IPrimaryKey.IndexOf(object key) => this.IndexOf((TKey) key);

    [DebuggerNonUserCode]
    void IPrimaryKey.Remove(object key) => this.Remove((TKey) key);

    ListSortDirection IIndex.SortDirection
    {
      [DebuggerNonUserCode] get => this.SortDirection;
    }

    [DebuggerNonUserCode]
    IIndexNode IIndex.Add(object key, object value) => (IIndexNode) this.Add((TKey) key, (TValue) value);

    [DebuggerNonUserCode]
    void IIndex.Clear() => this.Clear();

    [DebuggerNonUserCode]
    Array IIndex.Find(object key) => (Array) this.Find((TKey) key, IndexFindOption.Equal);

    [DebuggerNonUserCode]
    Array IIndex.Find(object key, IndexFindOption option) => (Array) this.Find((TKey) key, option);

    [DebuggerNonUserCode]
    Array IIndex.Find(IndexFindOption option, object key, params object[] keys) => (Array) this.Find((TKey) key, option);

    [DebuggerNonUserCode]
    Array IIndex.GetAll(ListSortDirection direction) => (Array) this.GetAll(direction);

    [DebuggerNonUserCode]
    object IIndex.GetValueAt(int index) => (object) this.GetValueAt(index);

    [DebuggerNonUserCode]
    int IIndex.Remove(object key, object valueToRemove, bool onlyOne) => this.Remove((TKey) key);

    [DebuggerNonUserCode]
    IEnumerator<IIndexNode> IEnumerable<IIndexNode>.GetEnumerator() => (IEnumerator<IIndexNode>) new Enumerator<IIndexNode>((IEnumerator) this.GetEnumerator());

    [DebuggerNonUserCode]
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    [DebuggerNonUserCode]
    object ICloneable.Clone() => (object) this.Clone();

    [DebuggerNonUserCode]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => this.GetObjectData(info, context);

    public class PrimaryKeyNode : IPrimaryKeyNode, IIndexNode
    {
      private int _index;
      private TKey _key;
      private TValue _value;

      public PrimaryKeyNode(TKey key, TValue value)
      {
        this._key = key;
        this._value = value;
      }

      public int Index
      {
        [DebuggerNonUserCode] get => this._index;
      }

      public TKey Key
      {
        [DebuggerNonUserCode] get => this._key;
      }

      public TValue Value
      {
        [DebuggerNonUserCode] get => this._value;
      }

      public override bool Equals(object obj)
      {
        switch (obj)
        {
          case PrimaryKeyDictionary<TKey, TValue>.PrimaryKeyNode primaryKeyNode:
            return primaryKeyNode.Key.Equals((object) this.Key);
          case TKey key:
            return key.Equals((object) this.Key);
          default:
            return false;
        }
      }

      public override int GetHashCode() => this.Key.GetHashCode();

      internal void SetIndex(int index) => this._index = index;

      object IPrimaryKeyNode.Value
      {
        [DebuggerNonUserCode] get => (object) this.Value;
      }

      object IIndexNode.Key
      {
        [DebuggerNonUserCode] get => (object) this.Key;
      }

      Array IIndexNode.Values
      {
        [DebuggerNonUserCode] get => (Array) new TValue[1]
        {
          this.Value
        };
      }
    }
  }
}
