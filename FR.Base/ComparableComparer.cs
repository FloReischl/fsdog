// Decompiled with JetBrains decompiler
// Type: FR.ComparableComparer
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FR
{
  [Serializable]
  public sealed class ComparableComparer : 
    IComparer,
    IEqualityComparer,
    IComparer<IComparable>,
    IEqualityComparer<IComparable>,
    ISerializable
  {
    public ComparableComparer()
    {
    }

    private ComparableComparer(SerializationInfo info, StreamingContext context)
    {
    }

    public static int Compare(IComparable x, IComparable y)
    {
      if (x == null && y == null)
        return 0;
      return x != null ? x.CompareTo((object) y) : y.CompareTo((object) x);
    }

    public static bool Equals(IComparable x, IComparable y)
    {
      if (x == null && y == null)
        return true;
      return x != null ? x.Equals((object) y) : y.Equals((object) x);
    }

    public static int GetHashCode(IComparable obj) => obj.GetHashCode();

    int IComparer.Compare(object x, object y) => ComparableComparer.Compare((IComparable) x, (IComparable) y);

    bool IEqualityComparer.Equals(object x, object y) => ComparableComparer.Equals((IComparable) x, (IComparable) y);

    int IEqualityComparer.GetHashCode(object obj) => ComparableComparer.GetHashCode((IComparable) obj);

    int IComparer<IComparable>.Compare(IComparable x, IComparable y) => ComparableComparer.Compare(x, y);

    bool IEqualityComparer<IComparable>.Equals(
      IComparable x,
      IComparable y)
    {
      return ComparableComparer.Equals(x, y);
    }

    int IEqualityComparer<IComparable>.GetHashCode(IComparable obj) => ComparableComparer.GetHashCode(obj);

    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
