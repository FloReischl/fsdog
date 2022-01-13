// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.CollapsibleGroup
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace FR.Windows.Forms
{
  public class CollapsibleGroup : Panel
  {
    private Rectangle _rectChk;
    private bool _hover;
    private Size _originalSize;
    private Size _originalMinimumSize;
    private int _prevWidth;
    private Dictionary<Control, bool> _visibilities;
    private string _caption;
    private Size _collapsedSize;
    private bool _collapsed;

    public string Caption
    {
      get => this._caption;
      set
      {
        this._caption = value;
        this.Refresh();
      }
    }

    public Size CollapsedSize
    {
      get => this._collapsedSize;
      set => this._collapsedSize = value;
    }

    [ReadOnly(true)]
    public bool Collapsed
    {
      get => this._collapsed;
      set
      {
        if (this.DesignMode)
          return;
        this._collapsed = value;
        this.collapse();
      }
    }

    public CollapsibleGroup() => this._prevWidth = -1;

    public event EventHandler AfterCollapse;

    public event EventHandler AfterExpand;

    public event CancelEventHandler BeforeCollapse;

    public event CancelEventHandler BeforeExpand;

    protected override void InitLayout()
    {
      base.InitLayout();
      this._hover = false;
      this._collapsed = false;
      this._visibilities = new Dictionary<Control, bool>();
      this._rectChk = new Rectangle(10, 0, 10, 10);
      this._collapsedSize = new Size(this.Width, 30);
      if (this._prevWidth != -1)
        return;
      this._prevWidth = this.Width;
    }

    [DebuggerNonUserCode]
    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (e.X >= this._rectChk.Left && e.X <= this._rectChk.Right && e.Y >= this._rectChk.Top && e.Y <= this._rectChk.Bottom && !this._hover)
      {
        this._hover = true;
        Graphics graphics = this.CreateGraphics();
        this.drawCheckBox(graphics);
        graphics.Dispose();
      }
      else if (this._hover)
      {
        this._hover = false;
        Graphics graphics = this.CreateGraphics();
        this.drawCheckBox(graphics);
        graphics.Dispose();
      }
      base.OnMouseMove(e);
    }

    [DebuggerNonUserCode]
    protected override void OnMouseClick(MouseEventArgs e)
    {
      if (e.X >= this._rectChk.Left && e.X <= this._rectChk.Right && e.Y >= this._rectChk.Top && e.Y <= this._rectChk.Bottom)
      {
        this._collapsed = !this._collapsed;
        this.collapse();
      }
      base.OnMouseClick(e);
    }

    [DebuggerNonUserCode]
    protected override void OnMouseLeave(EventArgs e)
    {
      if (this._hover)
      {
        this._hover = false;
        Graphics graphics = this.CreateGraphics();
        this.drawCheckBox(graphics);
        graphics.Dispose();
      }
      base.OnMouseLeave(e);
    }

    [DebuggerNonUserCode]
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this._caption == null || !(this._caption != ""))
        return;
      Graphics graphics = e.Graphics;
      Rectangle rect1 = new Rectangle(4, 4, this.Width - 8, this.Height - 8);
      graphics.DrawRectangle(new Pen(Color.Gray), rect1);
      Size size = TextRenderer.MeasureText(this._caption, this.Font);
      Rectangle rect2 = new Rectangle(this._rectChk.Left, this._rectChk.Top, this._rectChk.Right + size.Width, size.Height);
      graphics.FillRectangle((Brush) new SolidBrush(this.BackColor), rect2);
      this.drawCheckBox(graphics);
      graphics.DrawString(this._caption, this.Font, (Brush) new SolidBrush(this.ForeColor), (float) (this._rectChk.Right + 2), 0.0f);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      if (this._collapsedSize.Width == this._prevWidth)
        this._collapsedSize = new Size(this.Width, this._collapsedSize.Height);
      if (this._originalSize.Width == this._prevWidth)
        this._originalSize = new Size(this.Width, this._originalSize.Height);
      this._prevWidth = this.Width;
      base.OnSizeChanged(e);
    }

    [DebuggerNonUserCode]
    private void drawCheckBox(Graphics gfx)
    {
      Color color = !this._hover ? Color.White : Color.Gainsboro;
      gfx.FillRectangle((Brush) new SolidBrush(color), this._rectChk);
      gfx.DrawRectangle(new Pen(Color.Gray), this._rectChk);
      Pen pen = new Pen(Color.Black);
      gfx.DrawLine(pen, new Point(this._rectChk.Left + 1, this._rectChk.Height / 2), new Point(this._rectChk.Right - 1, this._rectChk.Height / 2));
      if (!this._collapsed)
        return;
      gfx.DrawLine(pen, new Point(this._rectChk.Left + this._rectChk.Width / 2, this._rectChk.Top + 1), new Point(this._rectChk.Left + this._rectChk.Width / 2, this._rectChk.Bottom - 1));
    }

    [DebuggerNonUserCode]
    private void collapse()
    {
      if (this._collapsed)
      {
        CancelEventArgs e = new CancelEventArgs(false);
        if (this.BeforeCollapse != null)
          this.BeforeCollapse((object) this, e);
        if (e.Cancel)
        {
          this._collapsed = false;
          return;
        }
      }
      else
      {
        CancelEventArgs e = new CancelEventArgs(false);
        if (this.BeforeExpand != null)
          this.BeforeExpand((object) this, e);
        if (e.Cancel)
        {
          this._collapsed = true;
          return;
        }
      }
      if (this._collapsed)
      {
        this._originalMinimumSize = this.MinimumSize;
        this.MinimumSize = new Size(0, 0);
        this._originalSize = this.Size;
        this.Size = this._collapsedSize;
        this._visibilities.Clear();
        foreach (Control control in (ArrangedElementCollection) this.Controls)
        {
          this._visibilities.Add(control, control.Visible);
          control.Visible = false;
        }
        if (this.AfterCollapse != null)
          this.AfterCollapse((object) this, EventArgs.Empty);
      }
      else
      {
        this.Size = this._originalSize;
        this.MinimumSize = this._originalMinimumSize;
        foreach (Control control in (ArrangedElementCollection) this.Controls)
        {
          if (this._visibilities.ContainsKey(control))
            control.Visible = this._visibilities[control];
        }
        this._visibilities.Clear();
        if (this.AfterExpand != null)
          this.AfterExpand((object) this, EventArgs.Empty);
      }
      this.Refresh();
    }
  }
}
