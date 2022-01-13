// Decompiled with JetBrains decompiler
// Type: FR.BaseHelper
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Diagnostics;

namespace FR
{
  public static class BaseHelper
  {
    [DebuggerNonUserCode]
    public static bool InList(object find, params object[] args)
    {
      foreach (object obj in args)
      {
        if (find == null && obj == null || find != null && obj != null && find.Equals(obj))
          return true;
      }
      return false;
    }
  }
}
