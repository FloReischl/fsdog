// Decompiled with JetBrains decompiler
// Type: FR.Int32Comparer
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
  public sealed class Int32Comparer : 
    IComparer<int>,
    IComparer,
    IEqualityComparer,
    IEqualityComparer<int>,
    ISerializable
  {
    public Int32Comparer()
    {
    }

    private Int32Comparer(SerializationInfo info, StreamingContext context)
    {
    }

    public static int Compare(int x, int y) => x - y;

    public static int GetHashCode(int dt) => dt;

    public static bool Equals(int x, int y) => x == y;

    int IComparer<int>.Compare(int x, int y) => Int32Comparer.Compare(x, y);

    int IComparer.Compare(object x, object y) => Int32Comparer.Compare((int) x, (int) y);

    bool IEqualityComparer.Equals(object x, object y) => Int32Comparer.Equals((int) x, (int) y);

    int IEqualityComparer.GetHashCode(object obj) => Int32Comparer.GetHashCode((int) obj);

    bool IEqualityComparer<int>.Equals(int x, int y) => Int32Comparer.Equals(x, y);

    int IEqualityComparer<int>.GetHashCode(int obj) => Int32Comparer.GetHashCode(obj);

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
