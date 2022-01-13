// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.PrimaryKey`2
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
  public class PrimaryKey<TKey, TValue> : 
    RBTreeBase<TKey, TValue, PrimaryKey<TKey, TValue>.PrimaryKeyNode>,
    IPrimaryKey,
    IIndex,
    IEnumerable<IIndexNode>,
    IEnumerable,
    ICloneable
  {
    private IComparer<TKey> _comparer;

    public PrimaryKey(IComparer<TKey> comparer, ListSortDirection sortDirection)
      : base(sortDirection)
    {
      this._comparer = comparer;
      this.IsUnique = true;
      this.Clear();
    }

    public PrimaryKey(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._comparer = (IComparer<TKey>) info.GetValue(nameof (Comparer), typeof (IComparer<TKey>));
    }

    public IComparer<TKey> Comparer
    {
      [DebuggerNonUserCode] get => this._comparer;
    }

    public override PrimaryKey<TKey, TValue>.PrimaryKeyNode Add(TKey key, TValue value) => base.Add(key, value);

    public PrimaryKey<TKey, TValue> Clone(ListSortDirection sortDirection) => new PrimaryKey<TKey, TValue>(this.Comparer, sortDirection);

    public PrimaryKey<TKey, TValue>.PrimaryKeyNode FindNode(TKey key)
    {
      PrimaryKey<TKey, TValue>.PrimaryKeyNode node;
      return this.FindNode(key, out PrimaryKey<TKey, TValue>.PrimaryKeyNode _, out node) ? node : (PrimaryKey<TKey, TValue>.PrimaryKeyNode) null;
    }

    public TValue FindValue(TKey key)
    {
      TValue[] objArray = this.Find(key, IndexFindOption.Equal);
      return objArray.Length != 0 ? objArray[0] : default (TValue);
    }

    public PrimaryKey<TKey, TValue>.PrimaryKeyNode GetNodeAt(int index)
    {
      if (index < 0 || index >= this.Count)
        throw ExceptionHelper.GetIndexOutOfRange(nameof (index), "count of node within the tree");
      PrimaryKey<TKey, TValue>.PrimaryKeyNode nodeAt = this._root;
      int num = index + 1;
      while (nodeAt != null && (nodeAt.Left != null || num != 1))
      {
        if (nodeAt.Left != null && nodeAt.Left.BranchNodeCount == num)
        {
          for (PrimaryKey<TKey, TValue>.PrimaryKeyNode primaryKeyNode = nodeAt.Left; primaryKeyNode != null; primaryKeyNode = primaryKeyNode.Right)
            nodeAt = primaryKeyNode;
          break;
        }
        if (nodeAt.Left != null && nodeAt.Left.BranchNodeCount > num)
        {
          nodeAt = nodeAt.Left;
        }
        else
        {
          if (nodeAt.Left != null)
            num -= nodeAt.Left.BranchNodeCount;
          if (num != 1)
          {
            --num;
            nodeAt = nodeAt.Right;
          }
          else
            break;
        }
      }
      return nodeAt;
    }

    public int IndexOf(TKey key)
    {
      int num1 = 0;
      int num2 = -1;
      PrimaryKey<TKey, TValue>.PrimaryKeyNode primaryKeyNode = this._root;
      while (primaryKeyNode != null)
      {
        int num3 = this.Comparer.Compare(key, primaryKeyNode.Key);
        if (this.SortDirection == ListSortDirection.Descending)
          num3 *= -1;
        if (num3 < 0)
          primaryKeyNode = primaryKeyNode.Left;
        else if (num3 > 0)
        {
          if (primaryKeyNode.Left != null)
            num1 += primaryKeyNode.Left.BranchNodeCount + 1;
          else
            ++num1;
          primaryKeyNode = primaryKeyNode.Right;
        }
        else
        {
          if (primaryKeyNode.Left != null)
            num1 += primaryKeyNode.Left.BranchNodeCount;
          num2 = num1;
          break;
        }
      }
      return num2;
    }

    public int Remove(TKey key) => this.Remove(key, default (TValue), true);

    protected override int Compare(TKey x, TKey y) => this.Comparer.Compare(x, y);

    protected override PrimaryKey<TKey, TValue>.PrimaryKeyNode CreateNode(TKey key) => new PrimaryKey<TKey, TValue>.PrimaryKeyNode(this, key);

    protected override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("Comparer", (object) this.Comparer);
    }

    int IPrimaryKey.Count
    {
      [DebuggerNonUserCode] get => this.Count;
    }

    [DebuggerNonUserCode]
    IPrimaryKey IPrimaryKey.Clone(ListSortDirection sortDirection) => (IPrimaryKey) this.Clone(sortDirection);

    [DebuggerNonUserCode]
    IPrimaryKeyNode IPrimaryKey.FindNode(object key) => (IPrimaryKeyNode) this.FindNode((TKey) key);

    [DebuggerNonUserCode]
    object IPrimaryKey.FindValue(object key) => (object) this.FindValue((TKey) key);

    [DebuggerNonUserCode]
    int IPrimaryKey.IndexOf(object key) => this.IndexOf((TKey) key);

    [DebuggerNonUserCode]
    IPrimaryKeyNode IPrimaryKey.GetNodeAt(int index) => (IPrimaryKeyNode) this.GetNodeAt(index);

    [DebuggerNonUserCode]
    void IPrimaryKey.Remove(object key) => this.Remove((TKey) key);

    [DebuggerNonUserCode]
    object ICloneable.Clone() => (object) this.Clone(this.SortDirection);

    public sealed class PrimaryKeyNode : 
      RBTreeBase<TKey, TValue, PrimaryKey<TKey, TValue>.PrimaryKeyNode>.RBNodeBase,
      IPrimaryKeyNode,
      IIndexNode
    {
      internal PrimaryKeyNode(PrimaryKey<TKey, TValue> tree, TKey key)
        : base((RBTreeBase<TKey, TValue, PrimaryKey<TKey, TValue>.PrimaryKeyNode>) tree, key)
      {
      }

      public TValue Value
      {
        [DebuggerNonUserCode] get => (TValue) this.Values[0];
      }

      public override bool Equals(object obj)
      {
        switch (obj)
        {
          case PrimaryKey<TKey, TValue>.PrimaryKeyNode primaryKeyNode:
            return primaryKeyNode.Key.Equals((object) this.Key);
          case TKey key:
            return key.Equals((object) this.Key);
          default:
            return false;
        }
      }

      public override int GetHashCode() => this.Key.GetHashCode();

      protected internal override void SetContentFrom(PrimaryKey<TKey, TValue>.PrimaryKeyNode node) => base.SetContentFrom(node);

      object IPrimaryKeyNode.Value
      {
        [DebuggerNonUserCode] get => (object) this.Value;
      }
    }
  }
}
