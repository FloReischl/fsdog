// Decompiled with JetBrains decompiler
// Type: FR.Collections.Generic.RBTreeBase`3
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace FR.Collections.Generic {
    [DebuggerDisplay("NodeCount: {Count} | Values: {_root.BranchValueCount} | RootKey: {_root.Key} | Type: {GetType()}")]
    [Serializable]
    public abstract class RBTreeBase<TKey, TValue, TNode> :
      IIndex,
      IEnumerable<IIndexNode>,
      IEnumerable<RBTreeBase<TKey, TValue, TNode>.RBNodeBase>,
      IEnumerable,
      ISerializable
      where TNode : RBTreeBase<TKey, TValue, TNode>.RBNodeBase {
        private int _version;
        protected TNode _root;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _allowNestedIndexes;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ListSortDirection _sortDirection;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isUnique;

        public RBTreeBase(ListSortDirection sortDirection) {
            this._version = 0;
            this._sortDirection = sortDirection;
            this._isUnique = false;
            this.Clear();
        }

        public RBTreeBase(SerializationInfo info, StreamingContext context) => this._sortDirection = (ListSortDirection)Enum.Parse(typeof(ListSortDirection), info.GetString(nameof(SortDirection)));

        public IndexTestNode GetTestTree() => this.GetTestNodes((IndexTestNode)null, this._root);

        private IndexTestNode GetTestNodes(IndexTestNode ancestor, TNode node) {
            if ((object)node == null)
                return (IndexTestNode)null;
            IndexTestNode ancestor1 = new IndexTestNode((IIndexNode)node) {
                Color = node.Color.ToString(),
                NodeCount = node.BranchNodeCount,
                Ancestor = ancestor
            };
            ancestor1.Left = this.GetTestNodes(ancestor1, node.Left);
            ancestor1.Right = this.GetTestNodes(ancestor1, node.Right);
            return ancestor1;
        }

        protected abstract int Compare(TKey x, TKey y);

        protected abstract TNode CreateNode(TKey key);

        public bool AllowNestedIndexes {
            [DebuggerNonUserCode]
            get => this._allowNestedIndexes;
            [DebuggerNonUserCode]
            set => this._allowNestedIndexes = value;
        }

        public int Count {
            [DebuggerNonUserCode]
            get => (object)this._root != null ? this._root.BranchNodeCount : 0;
        }

        public ListSortDirection SortDirection {
            [DebuggerNonUserCode]
            get => this._sortDirection;
        }

        public bool IsUnique {
            [DebuggerNonUserCode]
            get => this._isUnique;
            [DebuggerNonUserCode]
            set => this._isUnique = value;
        }

        public virtual TNode Add(TKey key, TValue value) {
            TNode node1 = this._root;
            TNode ancestor = this._root;
            if (this.FindNode(key, out ancestor, out node1)) {
                if (this.IsUnique)
                    throw IndexExceptionHelper.GetPrimaryKeyViolation((object)key);
                this.AddNodeValue(node1, value);
                return node1;
            }
            TNode node2 = this.CreateNode(key);
            this.AddNode(ancestor, node2, value);
            return node2;
        }

        public void Clear() {
            this._root = default(TNode);
            ++this._version;
        }

        public TValue[] Find(TKey key) => this.Find(key, IndexFindOption.Equal);

        public TValue[] Find(TKey key, IndexFindOption option) => this.Find(option, key, (TKey[])null);

        public TValue[] Find(IndexFindOption option, TKey key, params TKey[] args) {
            switch (option) {
                case IndexFindOption.Between:
                    return this.FindBetween(key, args[0]);
                case IndexFindOption.Equal:
                    return this.FindEqual(key);
                case IndexFindOption.Greater:
                    return this.FindGreater(key, false);
                case IndexFindOption.GreaterOrEqual:
                    return this.FindGreater(key, true);
                case IndexFindOption.Less:
                    return this.FindLess(key, false);
                case IndexFindOption.LessOrEqual:
                    return this.FindLess(key, true);
                default:
                    return new TValue[0];
            }
        }

        public TValue[] GetAll(ListSortDirection direction) {
            if ((object)this._root == null)
                return new TValue[0];
            List<TValue> list = new List<TValue>(this._root.BranchNodeCount);
            this.GetBranchValues(this._root, list);
            if (direction == ListSortDirection.Descending)
                list.Reverse();
            return list.ToArray();
        }

        public IEnumerator<RBTreeBase<TKey, TValue, TNode>.RBNodeBase> GetEnumerator() => (IEnumerator<RBTreeBase<TKey, TValue, TNode>.RBNodeBase>)new RBTreeBase<TKey, TValue, TNode>.Enumerator(this);

        public TValue GetValueAt(int index) {
            if (index < 0 || (object)this._root == null || this._root.BranchValueCount <= index)
                throw ExceptionHelper.GetIndexOutOfRange(nameof(index), "count of values within the index");
            TValue valueAt = default(TValue);
            TNode node = this._root;
            int index1 = index;
            while ((object)node != null) {
                if ((object)node.Left != null && node.Left.BranchValueCount > index1) {
                    node = node.Left;
                }
                else {
                    if (node.Values.Count > index1) {
                        valueAt = (TValue)node.Values[index1];
                        break;
                    }
                    if ((object)node.Left != null)
                        index1 -= node.Left.BranchValueCount;
                    if (node.Values.Count > index1) {
                        valueAt = (TValue)node.Values[index1];
                        break;
                    }
                    index1 -= node.Values.Count;
                    node = node.Right;
                }
            }
            return valueAt;
        }

        public virtual int Remove(TKey key, TValue valueToRemove, bool onlyOne) {
            TNode equalNode = this.FindEqualNode(key);
            int num;
            if ((object)equalNode == null)
                num = 0;
            else if (!this.IsUnique) {
                num = equalNode.RemoveValue(valueToRemove, onlyOne);
                if (equalNode.Values.Count == 0)
                    this.RemoveNode(equalNode);
            }
            else {
                this.RemoveNode(equalNode);
                num = 1;
            }
            return num;
        }

        protected void AddNode(TNode ancestor, TNode node, TValue value) {
            ++this._version;
            this.AddNodeValue(node, value);
            TKey key = node.Key;
            if ((object)ancestor == null) {
                node.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                this._root = node;
            }
            else {
                int num = this.CompareInternal(key, ancestor.Key);
                if (num < 0)
                    ancestor.Left = node;
                else if (num > 0)
                    ancestor.Right = node;
                node.Ancestor = ancestor;
                node.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Red;
                for (TNode node1 = ancestor; (object)node1 != null; node1 = node1.Ancestor) {
                    ++node1.BranchNodeCount;
                    ++node1.BranchValueCount;
                }
                this.CleanUpInsert(node);
            }
        }

        protected void AddNodeValue(TNode node, TValue value) {
            for (TNode ancestor = node.Ancestor; (object)ancestor != null; ancestor = ancestor.Ancestor)
                ++ancestor.BranchValueCount;
            node.AddValue((object)value);
        }

        protected int CompareInternal(TKey x, TKey y) => this.SortDirection == ListSortDirection.Ascending ? this.Compare(x, y) : this.Compare(x, y) * -1;

        protected TNode[] FindBetweenNodes(TKey key1, TKey key2) {
            List<TNode> foundNodes = new List<TNode>(this.Count % 5);
            this.FindBetweenNodesRecursive(key1, key2, this._root, foundNodes);
            return foundNodes.ToArray();
        }

        protected TNode FindEqualNode(TKey key) {
            TNode node;
            this.FindNode(key, out TNode _, out node);
            return node;
        }

        protected TNode[] FindGreaterNodes(TKey key, bool equal) {
            List<TNode> nodeList1 = new List<TNode>(this.Count / 10);
            List<TNode> nodeList2 = new List<TNode>(this.Count / 10);
            TNode node1 = this._root;
            TNode node2 = default(TNode);
            while ((object)node1 != null) {
                node2 = node1;
                int num = this.CompareInternal(key, node1.Key);
                if (num < 0) {
                    nodeList2.Add(node1);
                    nodeList1.Add(node1.Right);
                    node1 = node1.Left;
                }
                else if (num > 0) {
                    node1 = node1.Right;
                }
                else {
                    if (equal)
                        nodeList2.Add(node1);
                    else
                        nodeList2.Add(default(TNode));
                    nodeList1.Add(node1.Right);
                    node1 = default(TNode);
                }
            }
            List<TNode> list;
            if ((object)node2 != null) {
                list = new List<TNode>(nodeList2.Count * 3);
                for (int index = nodeList1.Count - 1; index >= 0; --index) {
                    TNode node3 = nodeList2[index];
                    TNode start = nodeList1[index];
                    if ((object)node3 != null)
                        list.Add(node3);
                    this.GetBranchNodes(start, list);
                }
            }
            else
                list = new List<TNode>();
            return list.ToArray();
        }

        protected TNode[] FindLessNodes(TKey key, bool equal) {
            List<TNode> list = new List<TNode>();
            List<TNode> nodeList1 = new List<TNode>();
            List<TNode> nodeList2 = new List<TNode>();
            TNode node1 = this._root;
            TNode node2 = default(TNode);
            while ((object)node1 != null) {
                node2 = node1;
                int num = this.CompareInternal(key, node1.Key);
                if (num < 0)
                    node1 = node1.Left;
                else if (num > 0) {
                    nodeList2.Add(node1);
                    nodeList1.Add(node1.Left);
                    node1 = node1.Right;
                }
                else if (num == 0) {
                    if (equal)
                        nodeList2.Add(node1);
                    else
                        nodeList2.Add(default(TNode));
                    nodeList1.Add(node1.Left);
                    node1 = default(TNode);
                }
            }
            if ((object)node2 != null) {
                for (int index = 0; index < nodeList1.Count; ++index) {
                    TNode node3 = nodeList2[index];
                    this.GetBranchNodes(nodeList1[index], list);
                    if ((object)node3 != null)
                        list.Add(node3);
                }
            }
            return list.ToArray();
        }

        protected bool FindNode(TKey key, out TNode ancestor, out TNode node) {
            node = this._root;
            ancestor = this._root;
            while ((object)node != null) {
                int num = this.CompareInternal(key, node.Key);
                ancestor = node;
                if (num < 0) {
                    node = node.Left;
                }
                else {
                    if (num <= 0)
                        return true;
                    node = node.Right;
                }
            }
            return false;
        }

        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context) => info.AddValue("SortDirection", (object)this.SortDirection);

        protected void RemoveNode(TNode remove) {
            ++this._version;
            for (TNode node = remove; (object)node != null; node = node.Ancestor) {
                --node.BranchNodeCount;
                --node.BranchValueCount;
            }
            if ((object)remove.Left != null && (object)remove.Right != null) {
                TNode node1 = remove.Right;
                TNode node2 = remove.Right;
                for (; (object)node1 != null; node1 = node1.Left) {
                    --node1.BranchNodeCount;
                    --node1.BranchValueCount;
                    node2 = node1;
                }
                int branchNodeCount = remove.BranchNodeCount;
                int branchValueCount = remove.BranchValueCount;
                remove.SetContentFrom(node2);
                remove.BranchNodeCount = branchNodeCount;
                remove.BranchValueCount = branchValueCount;
                remove = node2;
            }
            TNode node3 = (object)remove.Right != null ? remove.Right : remove.Left;
            if ((object)node3 != null)
                node3.Ancestor = remove.Ancestor;
            if ((object)remove == (object)this._root) {
                this._root = node3;
            }
            else {
                if ((object)remove.Ancestor.Left == (object)remove)
                    remove.Ancestor.Left = node3;
                else
                    remove.Ancestor.Right = node3;
                if (remove.Color != RBTreeBase<TKey, TValue, TNode>.NodeColor.Black || (object)node3 == null)
                    return;
                if (node3.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Red)
                    node3.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                else
                    this.CleanUpRemove(node3);
            }
        }

        private void CleanUpInsert(TNode node) {
            TNode uncle1 = this.GetUncle(node);
            TNode ancestor = node.Ancestor;
            TNode node1 = (object)ancestor != null ? ancestor.Ancestor : default(TNode);
            if ((object)node.Ancestor == null) {
                node.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
            }
            else {
                if (ancestor.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black)
                    return;
                if ((object)uncle1 != null && uncle1.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Red) {
                    ancestor.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                    uncle1.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                    node1.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Red;
                    this.CleanUpInsert(node1);
                }
                else {
                    TNode uncle2;
                    if ((object)node == (object)ancestor.Right && (object)ancestor == (object)node1.Left) {
                        this.RotateLeft(ancestor);
                        node = node.Left;
                        uncle2 = this.GetUncle(node);
                        ancestor = node.Ancestor;
                        node1 = (object)ancestor != null ? ancestor.Ancestor : default(TNode);
                    }
                    else if ((object)node == (object)ancestor.Left && (object)ancestor == (object)node1.Right) {
                        this.RotateRight(ancestor);
                        node = node.Right;
                        uncle2 = this.GetUncle(node);
                        ancestor = node.Ancestor;
                        node1 = (object)ancestor != null ? ancestor.Ancestor : default(TNode);
                    }
                    ancestor.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                    node1.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Red;
                    if ((object)node == (object)ancestor.Left && (object)ancestor == (object)node1.Left)
                        this.RotateRight(node1);
                    else
                        this.RotateLeft(node1);
                }
            }
        }

        private void CleanUpRemove(TNode node) {
            if ((object)node.Ancestor == null)
                return;
            TNode brother = this.GetBrother(node);
            if ((object)brother == null || (object)brother.Left == null || (object)brother.Right == null)
                return;
            if (brother.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Red) {
                node.Ancestor.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Red;
                brother.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                if ((object)node.Ancestor.Left == (object)node)
                    this.RotateLeft(node.Ancestor);
                else
                    this.RotateRight(node.Ancestor);
                brother = this.GetBrother(node);
                if ((object)brother == null || (object)brother.Left == null || (object)brother.Right == null)
                    return;
            }
            if (node.Ancestor.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black && brother.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black && brother.Left.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black && brother.Right.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black) {
                brother.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Red;
                this.CleanUpRemove(node.Ancestor);
            }
            else if (node.Ancestor.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Red && brother.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black && brother.Left.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black && brother.Right.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black) {
                brother.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Red;
                node.Ancestor.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
            }
            else {
                if ((object)node.Ancestor.Left == (object)node && brother.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black && brother.Left.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Red && brother.Right.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black) {
                    brother.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Red;
                    brother.Left.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                    this.RotateRight(brother);
                    brother = this.GetBrother(node);
                }
                else if ((object)node.Ancestor.Right == (object)node && brother.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black && brother.Left.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Black && brother.Right.Color == RBTreeBase<TKey, TValue, TNode>.NodeColor.Red) {
                    brother.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Red;
                    brother.Right.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                    this.RotateLeft(brother);
                    brother = this.GetBrother(node);
                }
                if ((object)brother == null || (object)brother.Left == null || (object)brother.Right == null)
                    return;
                brother.Color = node.Ancestor.Color;
                node.Ancestor.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                if ((object)node.Ancestor.Left == (object)node) {
                    brother.Right.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                    this.RotateLeft(node.Ancestor);
                }
                else {
                    brother.Left.Color = RBTreeBase<TKey, TValue, TNode>.NodeColor.Black;
                    this.RotateRight(node.Ancestor);
                }
            }
        }

        private TValue[] FindBetween(TKey key1, TKey key2) {
            TNode[] betweenNodes = this.FindBetweenNodes(key1, key2);
            List<TValue> list = new List<TValue>(betweenNodes.Length);
            foreach (TNode node in betweenNodes)
                this.GetValues(node, list);
            return list.ToArray();
        }

        private void FindBetweenNodesRecursive(
          TKey key1,
          TKey key2,
          TNode node,
          List<TNode> foundNodes) {
            if ((object)node == null)
                return;
            int num1 = this.CompareInternal(key1, node.Key);
            int num2 = this.CompareInternal(key2, node.Key);
            if ((num1 < 0 || num1 == 0) && (0 < num2 || num2 == 0)) {
                this.FindBetweenNodesRecursive(key1, key2, node.Left, foundNodes);
                foundNodes.Add(node);
                this.FindBetweenNodesRecursive(key1, key2, node.Right, foundNodes);
            }
            else if (num1 < 0)
                this.FindBetweenNodesRecursive(key1, key2, node.Left, foundNodes);
            else if (0 < num2)
                this.FindBetweenNodesRecursive(key1, key2, node.Right, foundNodes);
            else if (num1 > 0) {
                this.FindBetweenNodesRecursive(key1, key2, node.Left, foundNodes);
            }
            else {
                if (num2 >= 0)
                    return;
                this.FindBetweenNodesRecursive(key1, key2, node.Right, foundNodes);
            }
        }

        private TValue[] FindEqual(TKey key) {
            TNode equalNode = this.FindEqualNode(key);
            return (object)equalNode != null ? equalNode.Values.GetValues<TValue>() : new TValue[0];
        }

        private TValue[] FindGreater(TKey key, bool equal) {
            TNode[] greaterNodes = this.FindGreaterNodes(key, equal);
            List<TValue> list = new List<TValue>(greaterNodes.Length);
            foreach (TNode node in greaterNodes)
                this.GetValues(node, list);
            return list.ToArray();
        }

        private TValue[] FindLess(TKey key, bool equal) {
            TNode[] lessNodes = this.FindLessNodes(key, equal);
            List<TValue> list = new List<TValue>(lessNodes.Length);
            foreach (TNode node in lessNodes)
                this.GetValues(node, list);
            return list.ToArray();
        }

        private void GetBranchNodes(TNode start, List<TNode> list) {
            if ((object)start == null)
                return;
            this.GetBranchNodes(start.Left, list);
            list.Add(start);
            this.GetBranchNodes(start.Right, list);
        }

        private void GetBranchValues(TNode start, List<TValue> list) {
            if ((object)start == null)
                return;
            this.GetBranchValues(start.Left, list);
            this.GetValues(start, list);
            this.GetBranchValues(start.Right, list);
        }

        private TNode GetBrother(TNode node) {
            if ((object)node == null || (object)node.Ancestor == null)
                return default(TNode);
            return (object)node == (object)node.Ancestor.Left ? node.Ancestor.Right : node.Ancestor.Left;
        }

        private List<TValue> GetValues(TNode node, List<TValue> list) {
            if ((object)node == null && list == null)
                return new List<TValue>();
            if ((object)node == null)
                return list;
            if (list == null) {
                list = new List<TValue>((IEnumerable<TValue>)node.Values.GetValues<TValue>());
            }
            else {
                foreach (TValue obj in node.Values)
                    list.Add(obj);
            }
            return list;
        }

        private TNode GetUncle(TNode node) {
            TNode ancestor1 = node.Ancestor;
            if ((object)ancestor1 != null) {
                TNode ancestor2 = ancestor1.Ancestor;
                if ((object)ancestor2 != null)
                    return (object)ancestor2.Left == (object)ancestor1 ? ancestor2.Right : ancestor2.Left;
            }
            return default(TNode);
        }

        private void PrintValues(List<TValue> list) {
            foreach (TValue obj in list)
                Console.WriteLine((object)obj == null ? "<NULL>" : obj.ToString());
        }

        private void RotateLeft(TNode node) {
            TNode node1 = node;
            TNode right1 = node1.Right;
            TNode left1 = node1.Left;
            TNode left2 = right1.Left;
            TNode right2 = right1.Right;
            TNode ancestor = node1.Ancestor;
            int num1 = (object)left1 == null ? 0 : left1.BranchNodeCount;
            int num2 = (object)left2 == null ? 0 : left2.BranchNodeCount;
            int num3 = (object)right2 == null ? 0 : right2.BranchNodeCount;
            int num4 = (object)left1 == null ? 0 : left1.BranchValueCount;
            int num5 = (object)left2 == null ? 0 : left2.BranchValueCount;
            int num6 = (object)right2 == null ? 0 : right2.BranchValueCount;
            if ((object)ancestor != null) {
                if ((object)node == (object)ancestor.Left)
                    ancestor.Left = right1;
                else
                    ancestor.Right = right1;
            }
            right1.Ancestor = ancestor;
            right1.Right = right2;
            right1.Left = node1;
            node1.Ancestor = right1;
            node1.Left = left1;
            node1.Right = left2;
            if ((object)left1 != null)
                left1.Ancestor = node1;
            if ((object)left2 != null)
                left2.Ancestor = node1;
            if ((object)right2 != null)
                right2.Ancestor = right1;
            node1.BranchNodeCount = num1 + num2 + 1;
            right1.BranchNodeCount = num3 + node1.BranchNodeCount + 1;
            node1.BranchValueCount = num4 + num5 + node1.Values.Count;
            right1.BranchValueCount = num6 + node1.BranchValueCount + right1.Values.Count;
            if ((object)node1 != (object)this._root)
                return;
            this._root = right1;
        }

        private void RotateRight(TNode node) {
            TNode node1 = node;
            TNode left1 = node1.Left;
            TNode left2 = left1.Left;
            TNode right1 = left1.Right;
            TNode right2 = node1.Right;
            TNode ancestor = node1.Ancestor;
            int num1 = (object)left2 == null ? 0 : left2.BranchNodeCount;
            int num2 = (object)right1 == null ? 0 : right1.BranchNodeCount;
            int num3 = (object)right2 == null ? 0 : right2.BranchNodeCount;
            int num4 = (object)left2 == null ? 0 : left2.BranchValueCount;
            int num5 = (object)right1 == null ? 0 : right1.BranchValueCount;
            int num6 = (object)right2 == null ? 0 : right2.BranchValueCount;
            if ((object)ancestor != null) {
                if ((object)node == (object)ancestor.Left)
                    ancestor.Left = left1;
                else
                    ancestor.Right = left1;
            }
            left1.Ancestor = ancestor;
            left1.Left = left2;
            left1.Right = node1;
            node1.Ancestor = left1;
            node1.Left = right1;
            node1.Right = right2;
            if ((object)left2 != null)
                left2.Ancestor = left1;
            if ((object)right1 != null)
                right1.Ancestor = node1;
            if ((object)right2 != null)
                right2.Ancestor = node1;
            node1.BranchNodeCount = num2 + num3 + 1;
            left1.BranchNodeCount = num1 + node1.BranchNodeCount + 1;
            node1.BranchValueCount = num5 + num6 + node1.Values.Count;
            left1.BranchValueCount = num4 + node1.BranchValueCount + left1.Values.Count;
            if ((object)node1 != (object)this._root)
                return;
            this._root = left1;
        }

        ListSortDirection IIndex.SortDirection {
            [DebuggerNonUserCode]
            get => this.SortDirection;
        }

        [DebuggerNonUserCode]
        IIndexNode IIndex.Add(object key, object value) => (IIndexNode)this.Add((TKey)key, (TValue)value);

        [DebuggerNonUserCode]
        void IIndex.Clear() => this.Clear();

        [DebuggerNonUserCode]
        Array IIndex.Find(object key) => (Array)this.Find((TKey)key);

        [DebuggerNonUserCode]
        Array IIndex.Find(object key, IndexFindOption option) => (Array)this.Find((TKey)key, option);

        [DebuggerNonUserCode]
        Array IIndex.Find(IndexFindOption option, object key, params object[] args) {
            TKey[] keyArray;
            if (args != null && args.Length != 0) {
                keyArray = new TKey[args.Length];
                args.CopyTo((Array)keyArray, 0);
            }
            else
                keyArray = new TKey[0];
            return (Array)this.Find(option, (TKey)key, keyArray);
        }

        [DebuggerNonUserCode]
        Array IIndex.GetAll(ListSortDirection direction) => (Array)this.GetAll(direction);

        [DebuggerNonUserCode]
        object IIndex.GetValueAt(int index) => (object)this.GetValueAt(index);

        [DebuggerNonUserCode]
        int IIndex.Remove(object key, object value, bool onlyOne) => this.Remove((TKey)key, (TValue)value, onlyOne);

        [DebuggerNonUserCode]
        IEnumerator<RBTreeBase<TKey, TValue, TNode>.RBNodeBase> IEnumerable<RBTreeBase<TKey, TValue, TNode>.RBNodeBase>.GetEnumerator() => this.GetEnumerator();

        [DebuggerNonUserCode]
        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator)this.GetEnumerator();

        IEnumerator<IIndexNode> IEnumerable<IIndexNode>.GetEnumerator() => (IEnumerator<IIndexNode>)new FR.Collections.Generic.Enumerator<IIndexNode>((IEnumerator)this.GetEnumerator());

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) => this.GetObjectData(info, context);

        public enum NodeColor {
            Red,
            Black,
        }

        [DebuggerDisplay("Key: {Key} | BranchNodes: {BranchNodeCount} | BranchValues: {BranchValueCount} | Values = {Values.Count} | Type: {GetType()}")]
        public abstract class RBNodeBase : IIndexNode {
            private const int MAX_INCREMENT = 30;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private TKey _key;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private RBTreeBase<TKey, TValue, TNode> _tree;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private RBTreeBase<TKey, TValue, TNode>.NodeValues _values;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private TNode _ancestor;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int _branchNodeCount;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int _branchValueCount;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private RBTreeBase<TKey, TValue, TNode>.NodeColor _color;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private TNode _left;
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private TNode _right;

            public RBNodeBase(RBTreeBase<TKey, TValue, TNode> tree, TKey key) {
                this._tree = tree;
                this._branchNodeCount = 1;
                this._branchValueCount = 0;
                this._key = key;
            }

            public TKey Key {
                [DebuggerNonUserCode]
                get => this._key;
            }

            public RBTreeBase<TKey, TValue, TNode> Tree {
                [DebuggerNonUserCode]
                get => this._tree;
            }

            public RBTreeBase<TKey, TValue, TNode>.NodeValues Values {
                [DebuggerNonUserCode]
                get => this._values;
            }

            internal TNode Ancestor {
                [DebuggerNonUserCode]
                get => this._ancestor;
                [DebuggerNonUserCode]
                set => this._ancestor = value;
            }

            internal int BranchNodeCount {
                [DebuggerNonUserCode]
                get => this._branchNodeCount;
                [DebuggerNonUserCode]
                set => this._branchNodeCount = value;
            }

            public int BranchValueCount {
                [DebuggerNonUserCode]
                get => this._branchValueCount;
                [DebuggerNonUserCode]
                set => this._branchValueCount = value;
            }

            internal RBTreeBase<TKey, TValue, TNode>.NodeColor Color {
                [DebuggerNonUserCode]
                get => this._color;
                [DebuggerNonUserCode]
                set => this._color = value;
            }

            internal TNode Left {
                [DebuggerNonUserCode]
                get => this._left;
                [DebuggerNonUserCode]
                set => this._left = value;
            }

            protected internal TNode Right {
                [DebuggerNonUserCode]
                get => this._right;
                [DebuggerNonUserCode]
                set => this._right = value;
            }

            public void Print() => this.PrintBranch(this, 0);

            protected internal virtual void SetContentFrom(TNode node) {
                this._branchNodeCount = node._branchNodeCount;
                this._branchValueCount = node._branchValueCount;
                this._key = node._key;
                this._values = node._values;
            }

            internal void AddValue(object value) {
                ++this._branchValueCount;
                if (this._values == null) {
                    this._values = new RBTreeBase<TKey, TValue, TNode>.NodeValues(this, value);
                }
                else {
                    if (this.Values.Mode == RBTreeBase<TKey, TValue, TNode>.NodeValuesMode.Array && this.Tree.AllowNestedIndexes && this.Values.Count >= 16)
                        this.Values.SwitchToIndex();
                    this.Values.AddValue(value);
                }
            }

            internal int RemoveValue(TValue valueToRemove, bool onlyOne) => this._values == null ? 0 : this.Values.Remove((object)valueToRemove, onlyOne);

            private void PrintBranch(RBTreeBase<TKey, TValue, TNode>.RBNodeBase node, int indent) {
                if (node == null)
                    return;
                StringBuilder stringBuilder = new StringBuilder();
                for (int index = 0; index < indent; ++index)
                    stringBuilder.Append("   ");
                if (indent == 0)
                    Console.WriteLine("{0}Key: {1} ({2}) | Count: {3}", new object[4]
                    {
            (object) stringBuilder,
            (object) node.Key,
            (object) node.Color,
            (object) node.BranchNodeCount
                    });
                if ((object)node.Left != null) {
                    Console.WriteLine("{0}   Left: {1} ({2}) | Count: {3}", new object[4]
                    {
            (object) stringBuilder,
            (object) node.Left.Key,
            (object) node.Left.Color,
            (object) node.Left.BranchNodeCount
                    });
                    this.PrintBranch((RBTreeBase<TKey, TValue, TNode>.RBNodeBase)node.Left, indent + 1);
                }
                else
                    Console.WriteLine("{0}   Left: <NULL>", (object)stringBuilder);
                if ((object)node.Right != null) {
                    Console.WriteLine("{0}   Right: {1} ({2}) | Count: {3}", new object[4]
                    {
            (object) stringBuilder,
            (object) node.Right.Key,
            (object) node.Right.Color,
            (object) node.Right.BranchNodeCount
                    });
                    this.PrintBranch((RBTreeBase<TKey, TValue, TNode>.RBNodeBase)node.Right, indent + 1);
                }
                else
                    Console.WriteLine("{0}   Right: <NULL>", (object)stringBuilder);
            }

            object IIndexNode.Key {
                [DebuggerNonUserCode]
                get => (object)this.Key;
            }

            Array IIndexNode.Values {
                [DebuggerNonUserCode]
                get => (Array)this.Tree.GetValues((TNode)this, (List<TValue>)null).ToArray();
            }
        }

        public enum NodeValuesMode {
            Array,
            Index,
        }

        public class NodeValues : ICollection, IEnumerable {
            private int _nullValues;
            private object[] _values;
            private HashIndex _hashIndex;
            private RBTreeBase<TKey, TValue, TNode>.RBNodeBase _parentNode;
            private int _count;
            private RBTreeBase<TKey, TValue, TNode>.NodeValuesMode _mode;

            public NodeValues(
              RBTreeBase<TKey, TValue, TNode>.RBNodeBase parentNode,
              object initialValue) {
                this._values = new object[1] { initialValue };
                this._count = 1;
                this._hashIndex = (HashIndex)null;
                this._nullValues = 0;
                this._mode = RBTreeBase<TKey, TValue, TNode>.NodeValuesMode.Array;
                this._parentNode = parentNode;
            }

            public object this[int index] {
                get {
                    if (index < 0 || index >= this.Count)
                        throw ExceptionHelper.GetIndexOutOfRange(nameof(index), "count of values within the current node");
                    if (index < this._nullValues)
                        return (object)null;
                    return this._hashIndex != null ? this._hashIndex.GetValueAt(index) : this._values[index];
                }
            }

            public int Count {
                [DebuggerNonUserCode]
                get => this._count + this._nullValues;
            }

            public RBTreeBase<TKey, TValue, TNode>.NodeValuesMode Mode {
                [DebuggerNonUserCode]
                get => this._mode;
            }

            public void CopyTo(Array array, int index) {
                foreach (object obj in this)
                    array.SetValue(obj, index++);
            }

            public IEnumerator GetEnumerator() => this._hashIndex != null ? (IEnumerator)new IndexValueEnumerator<object>((IIndex)this._hashIndex, (IPrimaryKey)null) : this.GetValues<object>().GetEnumerator();

            internal void AddValue(object value) {
                if (value == null)
                    ++this._nullValues;
                else if (this._hashIndex != null) {
                    this._hashIndex.Add(value, value);
                    ++this._count;
                }
                else {
                    this.Allocate();
                    this._values[this._count++] = value;
                }
            }

            internal TType[] GetValues<TType>() {
                TType[] destinationArray = new TType[this._count];
                if (this._hashIndex != null) {
                    int num = 0;
                    foreach (IIndexNode indexNode in (RBTreeBase<object, object, HashIndex.HashIndexNode>)this._hashIndex) {
                        foreach (object obj in indexNode.Values)
                            destinationArray[num++] = (TType)obj;
                    }
                }
                else
                    Array.Copy((Array)this._values, 0, (Array)destinationArray, 0, this._count);
                return destinationArray;
            }

            internal int Remove(object value, bool onlyOne) {
                if (value == null && this._nullValues != 0)
                    return this.RemoveNullValue(onlyOne);
                int num = 0;
                if (this._hashIndex != null)
                    this._count -= this._hashIndex.Remove(value, (object)null, onlyOne);
                else if (this._count != 0) {
                    bool flag = false;
                    for (int index = 0; index < this._count; ++index) {
                        object obj = this._values[index];
                        if ((!onlyOne || !flag) && (value == null || obj.Equals(value))) {
                            flag = true;
                            ++num;
                        }
                        else if (num != 0) {
                            this._values[index - num] = obj;
                            this._values[index] = (object)null;
                        }
                    }
                    this._count -= num;
                }
                return num;
            }

            internal void SwitchToIndex() {
                this._hashIndex = new HashIndex();
                for (int index = 0; index < this._count; ++index)
                    this._hashIndex.Add(this._values[index]);
                this._mode = RBTreeBase<TKey, TValue, TNode>.NodeValuesMode.Index;
            }

            private void Allocate() {
                if (this._values == null)
                    this._values = new object[1];
                if (this._count < this._values.Length)
                    return;
                object[] values = this._values;
                this._values = this._count >= 4 ? (this._count >= 256 ? (this.Count >= 4096 ? (this.Count >= 4096 ? new object[this._count + 4096] : new object[this._count + 1024]) : new object[this._count + 256]) : new object[this._count + 32]) : new object[4];
                Array.Copy((Array)values, 0, (Array)this._values, 0, this._count);
            }

            private int RemoveNullValue(bool onlyOne) {
                int num;
                if (onlyOne && this._nullValues != 0) {
                    --this._nullValues;
                    num = 1;
                }
                else {
                    num = this._nullValues;
                    this._nullValues = 0;
                }
                return num;
            }

            [DebuggerNonUserCode]
            void ICollection.CopyTo(Array array, int index) => this.CopyTo(array, index);

            int ICollection.Count {
                [DebuggerNonUserCode]
                get => this.Count;
            }

            bool ICollection.IsSynchronized {
                [DebuggerNonUserCode]
                get => false;
            }

            object ICollection.SyncRoot {
                [DebuggerNonUserCode]
                get => throw new Exception("The method or operation is not implemented.");
            }

            [DebuggerNonUserCode]
            IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        }

        public class Enumerator :
          IEnumerator<RBTreeBase<TKey, TValue, TNode>.RBNodeBase>,
          IDisposable,
          IEnumerator {
            private const int NORMAL = 0;
            private const int CURRENT = 1;
            private bool _ancestor;
            private RBTreeBase<TKey, TValue, TNode> _tree;
            private TNode _current;
            private bool _reseted;
            private int _prev;
            private int _version;

            internal Enumerator(RBTreeBase<TKey, TValue, TNode> tree) {
                this._tree = tree;
                this.Reset();
            }

            public TNode Current {
                [DebuggerNonUserCode]
                get => this._reseted ? default(TNode) : this._current;
            }

            public TKey Key {
                [DebuggerNonUserCode]
                get => this.Current.Key;
            }

            public TValue[] Values {
                [DebuggerNonUserCode]
                get => this._tree.GetValues(this.Current, (List<TValue>)null).ToArray();
            }

            public void Dispose() {
                this._current = default(TNode);
                this._tree = (RBTreeBase<TKey, TValue, TNode>)null;
            }

            public bool MoveNext() {
                if (this._version != this._tree._version)
                    throw IndexExceptionHelper.GetEnumerationCollectionChanged();
                if (this._reseted) {
                    this._reseted = false;
                    return (object)this._current != null;
                }
                if ((object)this._current.Ancestor == null) {
                    TNode node = this._current.Right;
                    this._current = default(TNode);
                    for (; (object)node != null; node = node.Left)
                        this._current = node;
                }
                else if ((object)this._current == (object)this._current.Ancestor.Left && this._prev != 1) {
                    this._current = this._current.Ancestor;
                    this._prev = 1;
                }
                else {
                    this._prev = 0;
                    TNode node = this._current;
                    if ((object)node.Right != null) {
                        for (node = node.Right; (object)node != null; node = node.Left)
                            this._current = node;
                    }
                    else {
                        for (; (object)node != null; node = node.Ancestor) {
                            if ((object)node.Ancestor == null) {
                                this._current = default(TNode);
                                break;
                            }
                            if ((object)node == (object)node.Ancestor.Left) {
                                this._prev = 1;
                                this._current = node.Ancestor;
                                break;
                            }
                        }
                    }
                }
                return (object)this._current != null;
            }

            public void Reset() {
                this._version = this._tree._version;
                for (TNode node = this._tree._root; (object)node != null; node = node.Left)
                    this._current = node;
                this._ancestor = (object)this._current == (object)this._tree._root;
                this._reseted = true;
            }

            object IEnumerator.Current {
                [DebuggerNonUserCode]
                get => (object)this.Current;
            }

            [DebuggerNonUserCode]
            bool IEnumerator.MoveNext() => this.MoveNext();

            [DebuggerNonUserCode]
            void IEnumerator.Reset() => this.Reset();

            RBTreeBase<TKey, TValue, TNode>.RBNodeBase IEnumerator<RBTreeBase<TKey, TValue, TNode>.RBNodeBase>.Current {
                [DebuggerNonUserCode]
                get => (RBTreeBase<TKey, TValue, TNode>.RBNodeBase)this.Current;
            }

            [DebuggerNonUserCode]
            void IDisposable.Dispose() => this.Dispose();
        }
    }
}
