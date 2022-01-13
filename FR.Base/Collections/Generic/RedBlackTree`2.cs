// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.RedBlackTree`2
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace FR.Collections.Generic {
    public class RedBlackTree<TKey, TValue> :
      RBTreeBase<TKey, TValue, RedBlackTree<TKey, TValue>.RedBlackNode> {
        private IComparer<TKey> _comparer;

        public RedBlackTree(IComparer<TKey> comparer, ListSortDirection sortDirection)
          : base(sortDirection) {
            this._comparer = comparer;
        }

        public IComparer<TKey> Comparer {
            [DebuggerNonUserCode]
            get => this._comparer;
        }

        protected override int Compare(TKey x, TKey y) => this.Comparer.Compare(x, y);

        protected override RedBlackTree<TKey, TValue>.RedBlackNode CreateNode(TKey key) => new RedBlackTree<TKey, TValue>.RedBlackNode(this, key);

        public class RedBlackNode :
          RBTreeBase<TKey, TValue, RedBlackTree<TKey, TValue>.RedBlackNode>.RBNodeBase {
            internal RedBlackNode(RedBlackTree<TKey, TValue> tree, TKey key)
              : base((RBTreeBase<TKey, TValue, RedBlackTree<TKey, TValue>.RedBlackNode>)tree, key) {
            }
        }
    }
}
