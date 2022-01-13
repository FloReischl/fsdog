// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.TreeViewBase
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Drawing;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class TreeViewBase : TreeView
  {
    private bool _rightClickSelect;
    private TreeViewNodeCollection _nodes;

    public TreeViewBase()
    {
      this._rightClickSelect = false;
      this._nodes = new TreeViewNodeCollection(this, base.Nodes);
    }

    [Obsolete("Use ApplicationBase property")]
    public WindowsApplication Application => WindowsApplication.Instance;

    public new TreeNodeBase SelectedNode
    {
      get => (TreeNodeBase) base.SelectedNode;
      set => base.SelectedNode = (TreeNode) value;
    }

    public bool RightClickSelect
    {
      get => this._rightClickSelect;
      set => this._rightClickSelect = value;
    }

    public new TreeViewNodeCollection Nodes => this._nodes;

    public new TreeNodeBase GetNodeAt(int x, int y) => (TreeNodeBase) base.GetNodeAt(x, y);

    public new TreeNodeBase GetNodeAt(Point pt) => (TreeNodeBase) base.GetNodeAt(pt);

    protected internal OwnerDrawPropertyBag GetItemRenderStyles(
      TreeNodeBase node,
      int state)
    {
      return this.GetItemRenderStyles((TreeNode) node, state);
    }

    protected override void OnAfterCollapse(TreeViewEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnAfterCollapse(e);
      base.OnAfterCollapse(e);
    }

    protected override void OnAfterExpand(TreeViewEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnAfterExpand(e);
      base.OnAfterExpand(e);
    }

    protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnAfterLabelEdit(e);
      base.OnAfterLabelEdit(e);
    }

    protected override void OnAfterSelect(TreeViewEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnAfterSelect(e);
      base.OnAfterSelect(e);
    }

    protected override void OnDrawNode(DrawTreeNodeEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnDraw(new DrawNodeEventArgs(e.Graphics, node, e.Bounds, e.State, false));
      base.OnDrawNode(e);
    }

    protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnBeforeCollapse(e);
      base.OnBeforeCollapse(e);
    }

    protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnBeforeExpand(e);
      base.OnBeforeExpand(e);
    }

    protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnBeforeLabelEdit(e);
      base.OnBeforeLabelEdit(e);
    }

    protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnBeforeSelect(e);
      base.OnBeforeSelect(e);
    }

    protected override void OnDragDrop(DragEventArgs e)
    {
      if ((TreeNode) this.GetNodeAt(this.PointToClient(new Point(e.X, e.Y))) is TreeNodeBase nodeAt)
        nodeAt.OnDragDrop(e);
      base.OnDragDrop(e);
    }

    protected override void OnDragOver(DragEventArgs e)
    {
      if ((TreeNode) this.GetNodeAt(this.PointToClient(new Point(e.X, e.Y))) is TreeNodeBase nodeAt)
        nodeAt.OnDragOver(e);
      base.OnDragOver(e);
    }

    protected override void OnItemDrag(ItemDragEventArgs e)
    {
      if (e.Item is TreeNodeBase treeNodeBase)
        treeNodeBase.OnItemDrag(e);
      base.OnItemDrag(e);
    }

    protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnMouseClick(e);
      if (e.Button == MouseButtons.Right && e.Node != null && this.RightClickSelect)
        this.SelectedNode = (TreeNodeBase)e.Node;
      base.OnNodeMouseClick(e);
    }

    protected override void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
    {
      if (e.Node is TreeNodeBase node)
        node.OnMouseDoubleClick(e);
      base.OnNodeMouseDoubleClick(e);
    }
  }
}
