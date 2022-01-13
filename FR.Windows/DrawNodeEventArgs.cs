// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.DrawNodeEventArgs
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System.Drawing;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class DrawNodeEventArgs : DrawTreeNodeEventArgs
  {
    private bool _handled;

    public DrawNodeEventArgs(
      Graphics graphics,
      TreeNodeBase node,
      Rectangle bounds,
      TreeNodeStates state,
      bool handled)
      : base(graphics, (TreeNode) node, bounds, state)
    {
      this._handled = handled;
    }

    public bool Handled
    {
      get => this._handled;
      set => this._handled = value;
    }
  }
}
