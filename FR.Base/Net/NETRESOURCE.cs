// Decompiled with JetBrains decompiler
// Type: FR.Net.NETRESOURCE
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Runtime.InteropServices;

namespace FR.Net
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public struct NETRESOURCE
  {
    public NETRESOURCE_SCOPE dwScope;
    public NETRESOURCE_TYPE dwType;
    public NETRESOURCE_DISPLAYTYPE dwDisplayType;
    public NETRESOURCE_USAGE dwUsage;
    public string lpLocalName;
    public string lpRemoteName;
    public string lpComment;
    public string lpProvider;
  }
}
