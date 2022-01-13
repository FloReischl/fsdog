// Decompiled with JetBrains decompiler
// Type: FR.Net.NetworkShareType
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

namespace FR.Net
{
  public enum NetworkShareType : uint
  {
    STYPE_DISKTREE = 0,
    STYPE_PRINTQ = 1,
    STYPE_DEVICE = 2,
    STYPE_IPC = 3,
    STYPE_TEMPORARY = 1073741824, // 0x40000000
    STYPE_SPECIAL = 2147483648, // 0x80000000
  }
}
