// Decompiled with JetBrains decompiler
// Type: FR.MathHelper
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;

namespace FR
{
  public static class MathHelper
  {
    public static double MercantileRound(double value, int digits)
    {
      double num1 = Math.Pow(10.0, (double) digits);
      value *= num1;
      double num2 = Math.Abs(value);
      if (value - num2 > 0.5)
        ++num2;
      return num2 / num1;
    }
  }
}
