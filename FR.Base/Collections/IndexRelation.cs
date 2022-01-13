// Decompiled with JetBrains decompiler
// Type: FR.Collections.IndexRelation
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Diagnostics;

namespace FR.Collections
{
  public class IndexRelation
  {
    private IIndex _index;
    private IIndexNode _node;

    public IndexRelation(IIndex index, IIndexNode node)
    {
      this._index = index;
      this._node = node;
    }

    public IIndex Index
    {
      [DebuggerNonUserCode] get => this._index;
    }

    public IIndexNode Node
    {
      [DebuggerNonUserCode] get => this._node;
    }
  }
}
