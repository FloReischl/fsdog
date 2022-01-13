// Decompiled with JetBrains decompiler
// Type: FR.DateTimeComparer
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
  public sealed class DateTimeComparer : 
    IComparer<DateTime>,
    IComparer,
    IEqualityComparer,
    IEqualityComparer<DateTime>,
    ISerializable
  {
    public DateTimeComparer()
    {
    }

    private DateTimeComparer(SerializationInfo info, StreamingContext context)
    {
    }

    public static int Compare(DateTime x, DateTime y)
    {
      if (x < y)
        return -1;
      return x > y ? 1 : 0;
    }

    public static int GetHashCode(DateTime dt) => dt.GetHashCode();

    public static bool Equals(DateTime x, DateTime y) => DateTimeComparer.Compare(x, y) == 0;

    int IComparer<DateTime>.Compare(DateTime x, DateTime y) => DateTimeComparer.Compare(x, y);

    int IComparer.Compare(object x, object y) => DateTimeComparer.Compare((DateTime) x, (DateTime) y);

    bool IEqualityComparer.Equals(object x, object y) => DateTimeComparer.Equals((DateTime) x, (DateTime) y);

    int IEqualityComparer.GetHashCode(object obj) => DateTimeComparer.GetHashCode((DateTime) obj);

    bool IEqualityComparer<DateTime>.Equals(DateTime x, DateTime y) => DateTimeComparer.Equals(x, y);

    int IEqualityComparer<DateTime>.GetHashCode(DateTime obj) => DateTimeComparer.GetHashCode(obj);

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }
  }
}
