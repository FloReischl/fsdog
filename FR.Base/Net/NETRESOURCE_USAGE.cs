// Decompiled with JetBrains decompiler
// Type: FR.Net.NETRESOURCE_USAGE
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

namespace FR.Net
{
  public enum NETRESOURCE_USAGE : uint
  {
    RESOURCEUSAGE_CONNECTABLE = 1,
    RESOURCEUSAGE_CONTAINER = 2,
    RESOURCEUSAGE_NOLOCALDEVICE = 4,
    RESOURCEUSAGE_SIBLING = 8,
    RESOURCEUSAGE_ATTACHED = 16, // 0x00000010
    RESOURCEUSAGE_ALL = 19, // 0x00000013
  }
}
