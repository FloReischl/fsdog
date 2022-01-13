// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.TreeNodeTextComparer
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class TreeNodeTextComparer : IComparer, IComparer<TreeNode>
  {
    public int Compare(TreeNode x, TreeNode y)
    {
      if (x == null)
        return -1;
      return y == null ? 1 : string.Compare(x.Text, y.Text);
    }

    int IComparer.Compare(object x, object y) => this.Compare((TreeNode) x, (TreeNode) y);

    int IComparer<TreeNode>.Compare(TreeNode x, TreeNode y) => this.Compare(x, y);
  }
}
