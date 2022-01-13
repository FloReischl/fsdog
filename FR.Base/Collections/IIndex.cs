// Decompiled with JetBrains decompiler
// Type: FR.Collections.IIndex
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace FR.Collections
{
  public interface IIndex : IEnumerable<IIndexNode>, IEnumerable
  {
    ListSortDirection SortDirection { get; }

    IIndexNode Add(object key, object value);

    void Clear();

    Array Find(object key);

    Array Find(object key, IndexFindOption option);

    Array Find(IndexFindOption option, object key, params object[] keys);

    Array GetAll(ListSortDirection direction);

    object GetValueAt(int index);

    int Remove(object key, object valueToRemove, bool onlyOne);
  }
}
