// Decompiled with JetBrains decompiler
// Type: FR.Collections.RedBlackTree
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Collections.Generic;
using System.ComponentModel;

namespace FR.Collections
{
  public class RedBlackTree : RBTreeBase<int, object, RedBlackTree.RedBlackNode>
  {
    public RedBlackTree(ListSortDirection sortDirection)
      : base(sortDirection)
    {
    }

    protected override int Compare(int x, int y) => x - y;

    protected override RedBlackTree.RedBlackNode CreateNode(int key) => new RedBlackTree.RedBlackNode(this, key);

    public class RedBlackNode : RBTreeBase<int, object, RedBlackTree.RedBlackNode>.RBNodeBase
    {
      internal RedBlackNode(RedBlackTree tree, int key)
        : base((RBTreeBase<int, object, RedBlackTree.RedBlackNode>) tree, key)
      {
      }
    }
  }
}
