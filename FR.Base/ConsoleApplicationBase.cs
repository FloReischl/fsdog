// Decompiled with JetBrains decompiler
// Type: FR.ConsoleApplicationBase
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;

namespace FR
{
  public abstract class ConsoleApplicationBase : ApplicationBase
  {
    public static void Write(object obj) => Console.Write(obj);

    public static void WriteLine(string text, params object[] args) => Console.WriteLine(text, args);

    public static void ClearScreen() => Console.Clear();
  }
}
