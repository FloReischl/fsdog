// Decompiled with JetBrains decompiler
// Type: FR.StringHelper
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Text;

namespace FR
{
  public static class StringHelper
  {
    public static string Repeat(string value, int count)
    {
      StringBuilder stringBuilder = new StringBuilder(value.Length * count);
      for (int index = 0; index < count; ++index)
        stringBuilder.Append(value);
      return stringBuilder.ToString();
    }
  }
}
