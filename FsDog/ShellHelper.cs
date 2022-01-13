// Decompiled with JetBrains decompiler
// Type: FsDog.ShellHelper
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System;

namespace FsDog
{
  internal static class ShellHelper
  {
    public static uint HiWord(IntPtr ptr)
    {
      if (IntPtr.Size == 4)
      {
        uint num = (uint) (int) ptr;
        return ((int) num & int.MinValue) == int.MinValue ? num >> 16 : num >> 16 & (uint) ushort.MaxValue;
      }
      ulong num1 = (ulong) (long) ptr;
      if (((long) num1 & 4294967296L) == 4294967296L)
        num1 >>= 32;
      return ((long) num1 & 2147483648L) == 2147483648L ? (uint) (num1 >> 16) : (uint) num1 >> 16 & (uint) ushort.MaxValue;
    }

    public static uint LoWord(IntPtr ptr) => IntPtr.Size == 4 ? (uint) ((ulong) ptr.ToInt64() & (ulong) ushort.MaxValue) : (uint) ((ulong) ptr.ToInt64() & (ulong) ushort.MaxValue);
  }
}
