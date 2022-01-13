// Decompiled with JetBrains decompiler
// Type: FR.Net.SHARE_INFO_2
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Runtime.InteropServices;

namespace FR.Net
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct SHARE_INFO_2
  {
    public string shi2_netname;
    public NetworkShareType shi2_type;
    public string shi2_remark;
    public int shi2_permissions;
    public int shi2_max_uses;
    public int shi2_current_uses;
    public string shi2_path;
    public string shi2_passwd;
  }
}
