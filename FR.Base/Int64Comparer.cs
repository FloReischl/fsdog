// Decompiled with JetBrains decompiler
// Type: FR.Int64Comparer
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
  public sealed class Int64Comparer : 
    IComparer<long>,
    IComparer,
    IEqualityComparer,
    IEqualityComparer<long>,
    ISerializable
  {
    public Int64Comparer()
    {
    }

    private Int64Comparer(SerializationInfo info, StreamingContext context)
    {
    }

    public static int Compare(long x, long y) => (int) (x - y);

    public static int GetHashCode(long dt) => (int) (dt % (long) int.MaxValue);

    public static bool Equals(long x, long y) => x == y;

    int IComparer<long>.Compare(long x, long y) => Int64Comparer.Compare(x, y);

    int IComparer.Compare(object x, object y) => Int64Comparer.Compare((long) x, (long) y);

    bool IEqualityComparer.Equals(object x, object y) => Int64Comparer.Equals((long) x, (long) y);

    int IEqualityComparer.GetHashCode(object obj) => Int64Comparer.GetHashCode((long) obj);

    bool IEqualityComparer<long>.Equals(long x, long y) => Int64Comparer.Equals(x, y);

    int IEqualityComparer<long>.GetHashCode(long obj) => Int64Comparer.GetHashCode(obj);

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
