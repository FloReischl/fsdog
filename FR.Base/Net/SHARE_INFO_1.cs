// Decompiled with JetBrains decompiler
// Type: FR.Net.SHARE_INFO_1
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Runtime.InteropServices;

namespace FR.Net
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct SHARE_INFO_1
  {
    [MarshalAs(UnmanagedType.LPWStr)]
    public string shi0_netname;
    public NetworkShareType shi1_type;
    public string shi1_remark;
  }
}
