// Decompiled with JetBrains decompiler
// Type: FsDog.HookEventArgs
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System;

namespace FsDog
{
  public class HookEventArgs : EventArgs
  {
    public int HookCode;
    public IntPtr wParam;
    public IntPtr lParam;
  }
}
