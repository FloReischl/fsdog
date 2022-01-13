// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormOptionsTreeNode
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System.Diagnostics;

namespace FR.Windows.Forms
{
  public class FormOptionsTreeNode : TreeNodeBase
  {
    private IOptionsNode _optionsNode;

    public FormOptionsTreeNode(IOptionsNode optionsNode)
      : base(optionsNode.Name)
    {
      this._optionsNode = optionsNode;
      foreach (IOptionsNode node in optionsNode.Nodes)
        this.Nodes.Add((TreeNodeBase) new FormOptionsTreeNode(node));
    }

    public IOptionsNode OptionsNode
    {
      [DebuggerNonUserCode] get => this._optionsNode;
    }
  }
}
