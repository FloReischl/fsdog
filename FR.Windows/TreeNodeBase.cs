// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.TreeNodeBase
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace FR.Windows.Forms
{
  public class TreeNodeBase : TreeNode
  {
    private string DUMMY = "{EF056123-0C24-4745-8502-6A2CD32BC740}";
    private TreeViewNodeCollection _nodes;
    private Image _image;
    private Image _selectedImage;
    private Image _stateImage;

    public TreeNodeBase()
    {
    }

    public TreeNodeBase(string text)
      : base(text)
    {
    }

    public TreeNodeBase(string text, int imageIndex)
      : base(text)
    {
      this.ImageIndex = imageIndex;
    }

    public TreeNodeBase(string text, int imageIndex, int selectedImageIndex)
      : base(text, imageIndex, selectedImageIndex)
    {
    }

    public TreeNodeBase(string text, string imageKey)
      : base(text)
    {
      this.ImageKey = imageKey;
    }

    public TreeNodeBase(string text, string imageKey, string selectedImageKey)
      : base(text)
    {
      this.ImageKey = imageKey;
      this.SelectedImageKey = selectedImageKey;
    }

    public event TreeViewEventHandler AfterCollapse;

    public event TreeViewEventHandler AfterExpand;

    public event NodeLabelEditEventHandler AfterLabelEdit;

    public event TreeViewEventHandler AfterSelect;

    public event TreeViewCancelEventHandler BeforeCollapse;

    public event TreeViewCancelEventHandler BeforeExpand;

    public event NodeLabelEditEventHandler BeforeLabelEdit;

    public event TreeViewCancelEventHandler BeforeSelect;

    public event DragEventHandler DragDrop;

    public event DragEventHandler DragOver;

    public event DrawTreeNodeEventHandler Draw;

    public event ItemDragEventHandler ItemDrag;

    public event TreeViewCancelEventHandler LoadChildren;

    public event TreeNodeMouseClickEventHandler MouseClick;

    public event TreeNodeMouseClickEventHandler MouseDoubleClick;

    public WindowsApplication ApplicationBase => WindowsApplication.Instance;

    public new TreeViewNodeCollection Nodes
    {
      get
      {
        if (this._nodes == null)
          this._nodes = new TreeViewNodeCollection(this, base.Nodes);
        return this._nodes;
      }
    }

    public TreeNodeBase ParentNode
    {
      [DebuggerNonUserCode] get => (TreeNodeBase) this.Parent;
    }

    public new TreeViewBase TreeView => (TreeViewBase) base.TreeView;

    public Image Image
    {
      get => this._image;
      set => this._image = value;
    }

    public Image SelectedImage
    {
      get => this._selectedImage;
      set => this._selectedImage = value;
    }

    public Image StateImage
    {
      get => this._stateImage;
      set => this._stateImage = value;
    }

    public bool IsDummy() => this.Text == this.DUMMY;

    public void CreateDummy()
    {
      if (this.IsExpanded)
        this.Collapse();
      this.Nodes.Clear();
      this.Nodes.Add(this.DUMMY);
    }

    public bool HasDummy() => this.Nodes._nodes.Count == 1 && string.Compare(this.Nodes._nodes[0].Text, this.DUMMY) == 0;

    public void RemoveDummy()
    {
      if (!this.HasDummy())
        return;
      this.Nodes.Clear();
    }

    public void LoadIfHasDummy()
    {
      if (!this.HasDummy())
        return;
      this.RemoveDummy();
      this.OnLoadChildren(new TreeViewCancelEventArgs((TreeNode) this, false, TreeViewAction.Unknown));
    }

    protected OwnerDrawPropertyBag GetItemRenderStyles(int state) => this.TreeView.GetItemRenderStyles(this, state);

    protected internal virtual void OnAfterCollapse(TreeViewEventArgs e)
    {
      if (this.AfterCollapse == null)
        return;
      this.AfterCollapse((object) this, e);
    }

    protected internal virtual void OnAfterExpand(TreeViewEventArgs e)
    {
      if (this.AfterExpand == null)
        return;
      this.AfterExpand((object) this, e);
    }

    protected internal virtual void OnAfterLabelEdit(NodeLabelEditEventArgs e)
    {
      if (this.AfterLabelEdit == null)
        return;
      this.AfterLabelEdit((object) this, e);
    }

    protected internal virtual void OnAfterSelect(TreeViewEventArgs e)
    {
      if (this.AfterSelect == null)
        return;
      this.AfterSelect((object) this, e);
    }

    protected internal virtual void OnBeforeCollapse(TreeViewCancelEventArgs e)
    {
      if (this.BeforeCollapse == null)
        return;
      this.BeforeCollapse((object) this, e);
    }

    protected internal virtual void OnBeforeExpand(TreeViewCancelEventArgs e)
    {
      if (this.HasDummy())
      {
        this.RemoveDummy();
        this.OnLoadChildren(e);
      }
      if (e.Cancel || this.BeforeExpand == null)
        return;
      this.BeforeExpand((object) this, e);
    }

    protected internal virtual void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
    {
      if (this.BeforeLabelEdit == null)
        return;
      this.BeforeLabelEdit((object) this, e);
    }

    protected internal virtual void OnBeforeSelect(TreeViewCancelEventArgs e)
    {
      if (this.BeforeSelect == null)
        return;
      this.BeforeSelect((object) this, e);
    }

    protected internal virtual void OnDragDrop(DragEventArgs e)
    {
      if (this.DragDrop == null)
        return;
      this.DragDrop((object) this, e);
    }

    protected internal virtual void OnDragOver(DragEventArgs e)
    {
      if (this.DragOver == null)
        return;
      this.DragOver((object) this, e);
    }

    protected internal virtual void OnItemDrag(ItemDragEventArgs e)
    {
      if (this.ItemDrag == null)
        return;
      this.ItemDrag((object) this, e);
    }

    protected internal virtual void OnDraw(DrawNodeEventArgs e)
    {
      if (this.Draw != null)
        this.Draw((object) this, e);
      if (e.Handled || this.TreeView.DrawMode != TreeViewDrawMode.OwnerDrawAll || e.Bounds.Width == 0)
        return;
      Font font = this.TreeView.Font;
      int height = SystemInformation.SmallIconSize.Height;
      int width = SystemInformation.SmallIconSize.Width;
      bool flag = false;
      Image image = (Image) null;
      if (this.TreeView.ImageList != null)
      {
        flag = true;
        if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
        {
          image = this.SelectedImage;
          if (image == null && !string.IsNullOrEmpty(this.SelectedImageKey) && this.TreeView.ImageList.Images.ContainsKey(this.SelectedImageKey))
            image = this.TreeView.ImageList.Images[this.SelectedImageKey];
          else if (image == null && this.SelectedImageIndex != -1 && this.SelectedImageIndex < this.TreeView.ImageList.Images.Count)
            image = this.TreeView.ImageList.Images[this.SelectedImageIndex];
        }
        else
        {
          image = this.Image;
          if (image == null && !string.IsNullOrEmpty(this.ImageKey) && this.TreeView.ImageList.Images.ContainsKey(this.ImageKey))
            image = this.TreeView.ImageList.Images[this.ImageKey];
          else if (image == null && this.ImageIndex != -1 && this.ImageIndex < this.TreeView.ImageList.Images.Count)
            image = this.TreeView.ImageList.Images[this.ImageIndex];
        }
        if (image == null && this.TreeView.ImageList.Images.Count != 0)
          image = this.TreeView.ImageList.Images[0];
        if (image == null)
          image = (Image) new Bitmap(this.TreeView.ImageList.ImageSize.Width, this.TreeView.ImageList.ImageSize.Height);
        if (image.Size != this.TreeView.ImageList.ImageSize)
          image = (Image) new Bitmap(image, this.TreeView.ImageList.ImageSize);
        height = image.Height;
        width = image.Width;
      }
      Graphics graphics = e.Graphics;
      Rectangle rectangle = Rectangle.Empty;
      rectangle = this.TreeView.ImageList != null ? new Rectangle(new Point(this.Bounds.X - width, this.Bounds.Y), new Size(this.Bounds.Width, this.Bounds.Height)) : this.Bounds;
      Rectangle bounds = new Rectangle(new Point(rectangle.X - width, rectangle.Y), new Size(width, height));
      Rectangle rect1 = new Rectangle(new Point(bounds.Right, rectangle.Y), new Size(width, height));
      SizeF sizeF = graphics.MeasureString(this.Text, font);
      PointF point = new PointF((float) (rect1.Right + 2), (float) rectangle.Y + (float) (((double) rectangle.Height - (double) sizeF.Height) / 2.0));
      Rectangle rect2 = new Rectangle(new Point(rect1.Right, rectangle.Y), new Size((int) sizeF.Width + 2, rectangle.Height));
      VisualStyleElement element = (VisualStyleElement) null;
      if (this.Nodes.Count != 0)
        element = !this.IsExpanded ? VisualStyleElement.TreeView.Glyph.Closed : VisualStyleElement.TreeView.Glyph.Opened;
      Color color1;
      Color color2;
      if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
      {
        color1 = SystemColors.Highlight;
        color2 = SystemColors.HighlightText;
      }
      else
      {
        color1 = this.BackColor.IsEmpty ? this.TreeView.BackColor : this.BackColor;
        color2 = this.ForeColor.IsEmpty ? this.TreeView.ForeColor : this.ForeColor;
      }
      if (element != null)
        new VisualStyleRenderer(element).DrawBackground((IDeviceContext) graphics, bounds);
      if (flag)
        graphics.DrawImage(image, rect1);
      graphics.FillRectangle((Brush) new SolidBrush(color1), rect2);
      graphics.DrawString(this.Text, font, (Brush) new SolidBrush(color2), point);
    }

    protected internal virtual void OnLoadChildren(TreeViewCancelEventArgs e)
    {
      if (this.LoadChildren == null)
        return;
      this.LoadChildren((object) this, e);
    }

    protected internal virtual void OnMouseClick(TreeNodeMouseClickEventArgs e)
    {
      if (this.MouseClick == null)
        return;
      this.MouseClick((object) this, e);
    }

    protected internal virtual void OnMouseDoubleClick(TreeNodeMouseClickEventArgs e)
    {
      if (this.MouseDoubleClick == null)
        return;
      this.MouseDoubleClick((object) this, e);
    }

    internal void AfterAddedToTree()
    {
      if (this.TreeView.ImageList != null)
      {
        if (this.Image != null)
        {
          if (string.IsNullOrEmpty(this.ImageKey))
            this.ImageKey = Guid.NewGuid().ToString();
          if (!this.TreeView.ImageList.Images.ContainsKey(this.ImageKey))
            this.TreeView.ImageList.Images.Add(this.ImageKey, this.Image);
        }
        if (this.SelectedImage != null)
        {
          if (string.IsNullOrEmpty(this.SelectedImageKey))
            this.SelectedImageKey = Guid.NewGuid().ToString();
          if (!this.TreeView.ImageList.Images.ContainsKey(this.SelectedImageKey))
            this.TreeView.ImageList.Images.Add(this.SelectedImageKey, this.SelectedImage);
        }
        if (this.StateImage != null)
        {
          if (string.IsNullOrEmpty(this.StateImageKey))
            this.StateImageKey = Guid.NewGuid().ToString();
          if (!this.TreeView.ImageList.Images.ContainsKey(this.StateImageKey))
            this.TreeView.ImageList.Images.Add(this.StateImageKey, this.StateImage);
        }
      }
      if (this.HasDummy() || this.Nodes.Count == 0)
        return;
      foreach (TreeNodeBase node in this.Nodes)
        node.AfterAddedToTree();
    }

    internal void BeforeRemovedFromTree()
    {
    }
  }
}
