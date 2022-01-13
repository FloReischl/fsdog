// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.TreeViewNodeCollection
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Collections;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class TreeViewNodeCollection : IList, ICollection, IEnumerable
  {
    private TreeViewBase _tree;
    private TreeNodeBase _parent;
    internal TreeNodeCollection _nodes;

    internal TreeViewNodeCollection(TreeNodeBase parent, TreeNodeCollection nodes)
    {
      this._parent = parent;
      this._nodes = nodes;
    }

    internal TreeViewNodeCollection(TreeViewBase tree, TreeNodeCollection nodes)
    {
      this._tree = tree;
      this._nodes = nodes;
    }

    public static implicit operator TreeNodeCollection(
      TreeViewNodeCollection nodes)
    {
      return nodes._nodes;
    }

    public TreeNodeBase this[int index]
    {
      get
      {
        this.HandleDummy();
        return (TreeNodeBase) this._nodes[index];
      }
      set
      {
        this.HandleDummy();
        this._nodes[index] = (TreeNode) value;
      }
    }

    public TreeNodeBase this[string key]
    {
      get
      {
        this.HandleDummy();
        return (TreeNodeBase) this._nodes[key];
      }
    }

    public int Count => this._nodes.Count;

    public bool IsReadOnly => this._nodes.IsReadOnly;

    public TreeNodeBase Add(string text)
    {
      TreeNodeBase node = new TreeNodeBase(text);
      this.Add(node);
      return node;
    }

    public TreeNodeBase Add(string key, string text)
    {
      TreeNodeBase node = new TreeNodeBase(key, text);
      this.Add(node);
      return node;
    }

    public int Add(TreeNodeBase node)
    {
      this.HandleDummy();
      int num = this._nodes.Add((TreeNode) node);
      if (this._tree != null || this._parent != null && this._parent.TreeView != null)
        node.AfterAddedToTree();
      return num;
    }

    public int Add(string key, TreeNodeBase node)
    {
      node.Name = key;
      return this.Add(node);
    }

    public TreeNodeBase Add(string key, string text, int imageIndex)
    {
      TreeNodeBase node = new TreeNodeBase(text, imageIndex);
      node.Name = key;
      this.Add(node);
      return node;
    }

    public TreeNodeBase Add(
      string key,
      string text,
      int imageIndex,
      int selectedImageIndex)
    {
      TreeNodeBase node = new TreeNodeBase(text, imageIndex, selectedImageIndex);
      node.Name = key;
      this.Add(node);
      return node;
    }

    public TreeNodeBase Add(string key, string text, string imageKey)
    {
      TreeNodeBase node = new TreeNodeBase(text, imageKey);
      node.Name = key;
      this.Add(node);
      return node;
    }

    public TreeNodeBase Add(
      string key,
      string text,
      string imageKey,
      string selectedImageKey)
    {
      TreeNodeBase node = new TreeNodeBase(text, imageKey, selectedImageKey);
      node.Name = key;
      this.Add(node);
      return node;
    }

    public void AddRange(TreeNodeBase[] nodes)
    {
      this.HandleDummy();
      this._nodes.AddRange((TreeNode[]) nodes);
      if (this._tree == null && (this._parent == null || this._parent.TreeView == null))
        return;
      foreach (TreeNodeBase node in nodes)
        node.AfterAddedToTree();
    }

    public void Clear()
    {
      foreach (TreeNodeBase node in this._nodes)
        node.BeforeRemovedFromTree();
      this._nodes.Clear();
    }

    public bool Contains(TreeNodeBase node)
    {
      this.HandleDummy();
      return this._nodes.Contains((TreeNode) node);
    }

    public bool ContainsKey(string key)
    {
      this.HandleDummy();
      return this._nodes.ContainsKey(key);
    }

    public void CopyTo(Array array, int index)
    {
      this.HandleDummy();
      this._nodes.CopyTo(array, index);
    }

    public IEnumerator GetEnumerator()
    {
      if (this._nodes.Count == 1)
        this.HandleDummy();
      return this._nodes.GetEnumerator();
    }

    public override int GetHashCode()
    {
      this.HandleDummy();
      return this._nodes.GetHashCode();
    }

    public int IndexOf(TreeNodeBase node)
    {
      this.HandleDummy();
      return this._nodes.IndexOf((TreeNode) node);
    }

    public int IndexOfKey(string key)
    {
      this.HandleDummy();
      return this._nodes.IndexOfKey(key);
    }

    public void Insert(int index, TreeNodeBase node)
    {
      this.HandleDummy();
      this._nodes.Insert(index, (TreeNode) node);
      if (this._tree == null && (this._parent == null || this._parent.TreeView == null))
        return;
      node.AfterAddedToTree();
    }

    public TreeNodeBase Insert(int index, string text)
    {
      TreeNodeBase node = new TreeNodeBase(text);
      this.Insert(index, node);
      return node;
    }

    public TreeNodeBase Insert(int index, string key, string text)
    {
      TreeNodeBase node = new TreeNodeBase(text);
      node.Name = key;
      this.Insert(index, node);
      return node;
    }

    public TreeNodeBase Insert(int index, string key, string text, int imageIndex)
    {
      TreeNodeBase node = new TreeNodeBase(text, imageIndex);
      node.Name = key;
      this.Insert(index, node);
      return node;
    }

    public TreeNodeBase Insert(
      int index,
      string key,
      string text,
      int imageIndex,
      int selectedImageIndex)
    {
      TreeNodeBase node = new TreeNodeBase(text, imageIndex, selectedImageIndex);
      node.Name = key;
      this.Insert(index, node);
      return node;
    }

    public TreeNodeBase Insert(int index, string key, string text, string imageKey)
    {
      TreeNodeBase node = new TreeNodeBase(text, imageKey);
      node.Name = key;
      this.Insert(index, node);
      return node;
    }

    public TreeNodeBase Insert(
      int index,
      string key,
      string text,
      string imageKey,
      string selectedImageKey)
    {
      TreeNodeBase node = new TreeNodeBase(text, imageKey, selectedImageKey);
      node.Name = key;
      this.Insert(index, node);
      return node;
    }

    public void Remove(TreeNodeBase node)
    {
      this.HandleDummy();
      if (node.TreeView != null)
        node.BeforeRemovedFromTree();
      this._nodes.Remove((TreeNode) node);
    }

    public void RemoveAt(int index) => this.Remove(this[index]);

    public void RemoveByKey(string key) => this.Remove(this[key]);

    public void Sort(IComparer nodeComparer)
    {
      ArrayList arrayList = new ArrayList(this._nodes.Count);
      arrayList.AddRange((ICollection) this._nodes);
      arrayList.Sort(nodeComparer);
      if (this._tree != null)
        this._tree.BeginUpdate();
      else if (this._parent != null && this._parent.TreeView != null)
        this._parent.TreeView.BeginUpdate();
      this._nodes.Clear();
      foreach (TreeNode node in arrayList)
        this._nodes.Add(node);
      if (this._tree != null)
      {
        this._tree.EndUpdate();
      }
      else
      {
        if (this._parent == null || this._parent.TreeView == null)
          return;
        this._parent.TreeView.EndUpdate();
      }
    }

    public override string ToString() => this._nodes.ToString();

    private void HandleDummy()
    {
      if (this._parent == null || !this._parent.HasDummy())
        return;
      this._parent.RemoveDummy();
      this._parent.OnLoadChildren(new TreeViewCancelEventArgs((TreeNode) this._parent, false, TreeViewAction.Unknown));
    }

    int IList.Add(object node) => this.Add((TreeNodeBase) node);

    void IList.Clear() => this.Clear();

    bool IList.Contains(object node) => this.Contains((TreeNodeBase) node);

    int IList.IndexOf(object node) => this.IndexOf((TreeNodeBase) node);

    void IList.Insert(int index, object node) => this.Insert(index, (TreeNodeBase) node);

    bool IList.IsFixedSize => ((IList) this._nodes).IsFixedSize;

    bool IList.IsReadOnly => this.IsReadOnly;

    void IList.Remove(object node) => this.Remove((TreeNodeBase) node);

    void IList.RemoveAt(int index) => this.RemoveAt(index);

    object IList.this[int index]
    {
      get => (object) this[index];
      set => this[index] = (TreeNodeBase) value;
    }

    void ICollection.CopyTo(Array array, int index) => this.CopyTo(array, index);

    int ICollection.Count => this.Count;

    bool ICollection.IsSynchronized => ((ICollection) this._nodes).IsSynchronized;

    object ICollection.SyncRoot => ((ICollection) this._nodes).SyncRoot;

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
  }
}
