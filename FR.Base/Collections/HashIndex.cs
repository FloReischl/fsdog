// Decompiled with JetBrains decompiler
// Type: FR.Collections.HashIndex
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Collections.Generic;
using System;
using System.ComponentModel;

namespace FR.Collections
{
  [Serializable]
  public class HashIndex : RBTreeBase<object, object, HashIndex.HashIndexNode>
  {
    public HashIndex()
      : base(ListSortDirection.Ascending)
    {
      this.AllowNestedIndexes = false;
    }

    public HashIndex.HashIndexNode Add(object value) => this.Add(value, value);

    protected override int Compare(object x, object y) => x.GetHashCode() - y.GetHashCode();

    protected override HashIndex.HashIndexNode CreateNode(object key) => new HashIndex.HashIndexNode(this, key);

    public class HashIndexNode : RBTreeBase<object, object, HashIndex.HashIndexNode>.RBNodeBase
    {
      internal HashIndexNode(HashIndex tree, object key)
        : base((RBTreeBase<object, object, HashIndex.HashIndexNode>) tree, key)
      {
      }
    }
  }
}
