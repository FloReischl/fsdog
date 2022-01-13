// Decompiled with JetBrains decompiler
// Type: FR.Collections.IPrimaryKey
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace FR.Collections
{
  public interface IPrimaryKey : IIndex, IEnumerable<IIndexNode>, IEnumerable, ICloneable
  {
    IPrimaryKey Clone(ListSortDirection sortDirection);

    int Count { get; }

    IPrimaryKeyNode FindNode(object key);

    object FindValue(object key);

    IPrimaryKeyNode GetNodeAt(int index);

    int IndexOf(object key);

    void Remove(object key);
  }
}
