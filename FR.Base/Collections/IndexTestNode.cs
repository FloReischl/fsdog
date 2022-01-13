// Decompiled with JetBrains decompiler
// Type: FR.Collections.IndexTestNode
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Diagnostics;

namespace FR.Collections
{
  public class IndexTestNode
  {
    private string _color;
    private int _nodeCount;
    private IIndexNode _node;
    private IndexTestNode _ancestor;
    private IndexTestNode _less;
    private IndexTestNode _greater;

    internal IndexTestNode(IIndexNode node) => this._node = node;

    public string Color
    {
      [DebuggerNonUserCode] get => this._color;
      [DebuggerNonUserCode] set => this._color = value;
    }

    public int NodeCount
    {
      [DebuggerNonUserCode] get => this._nodeCount;
      [DebuggerNonUserCode] set => this._nodeCount = value;
    }

    public object Key
    {
      [DebuggerNonUserCode] get => this.Node.Key;
    }

    public Array Values
    {
      [DebuggerNonUserCode] get => this.Node.Values;
    }

    public IIndexNode Node
    {
      [DebuggerNonUserCode] get => this._node;
    }

    public IndexTestNode Ancestor
    {
      [DebuggerNonUserCode] get => this._ancestor;
      [DebuggerNonUserCode] set => this._ancestor = value;
    }

    public IndexTestNode Left
    {
      [DebuggerNonUserCode] get => this._less;
      [DebuggerNonUserCode] set => this._less = value;
    }

    public IndexTestNode Right
    {
      [DebuggerNonUserCode] get => this._greater;
      [DebuggerNonUserCode] set => this._greater = value;
    }
  }
}
