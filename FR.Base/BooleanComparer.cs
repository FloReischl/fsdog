// Decompiled with JetBrains decompiler
// Type: FR.BooleanComparer
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace FR
{
  [Serializable]
  public sealed class BooleanComparer : 
    IComparer<bool>,
    IComparer,
    IEqualityComparer,
    IEqualityComparer<bool>,
    ISerializable
  {
    public BooleanComparer()
    {
    }

    private BooleanComparer(SerializationInfo info, StreamingContext context)
    {
    }

    public static int Compare(bool x, bool y)
    {
      if (!x && y)
        return -1;
      return x && !y ? 1 : 0;
    }

    public static int GetHashCode(bool dt) => dt.GetHashCode();

    public static bool Equals(bool x, bool y) => x == y;

    int IComparer<bool>.Compare(bool x, bool y) => BooleanComparer.Compare(x, y);

    int IComparer.Compare(object x, object y) => BooleanComparer.Compare((bool) x, (bool) y);

    bool IEqualityComparer.Equals(object x, object y) => BooleanComparer.Equals((bool) x, (bool) y);

    int IEqualityComparer.GetHashCode(object obj) => BooleanComparer.GetHashCode((bool) obj);

    bool IEqualityComparer<bool>.Equals(bool x, bool y) => BooleanComparer.Equals(x, y);

    int IEqualityComparer<bool>.GetHashCode(bool obj) => BooleanComparer.GetHashCode(obj);

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
