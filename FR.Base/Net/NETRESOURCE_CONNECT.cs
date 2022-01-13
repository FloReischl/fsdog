// Decompiled with JetBrains decompiler
// Type: FR.Net.NETRESOURCE_CONNECT
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

namespace FR.Net
{
  public enum NETRESOURCE_CONNECT : uint
  {
    CONNECT_UPDATE_PROFILE = 1,
    CONNECT_INTERACTIVE = 8,
    CONNECT_PROMPT = 16, // 0x00000010
    CONNECT_REDIRECT = 128, // 0x00000080
    CONNECT_COMMANDLINE = 2048, // 0x00000800
    CONNECT_CMD_SAVECRED = 4096, // 0x00001000
  }
}
