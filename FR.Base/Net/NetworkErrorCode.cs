// Decompiled with JetBrains decompiler
// Type: FR.Net.NetworkErrorCode
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

namespace FR.Net
{
  public enum NetworkErrorCode
  {
    NERR_Success = 0,
    ERROR_ACCESS_DENIED = 5,
    ERROR_NOT_ENOUGH_MEMORY = 8,
    ERROR_NOT_SUPPORTED = 50, // 0x00000032
    ERROR_BAD_NETPATH = 53, // 0x00000035
    ERROR_INVALID_PARAMETER = 87, // 0x00000057
    ERROR_INVALID_LEVEL = 124, // 0x0000007C
    ERROR_MORE_DATA = 234, // 0x000000EA
    ERROR_INVALID_FLAGS = 1004, // 0x000003EC
    ERROR_BAD_DEVICE = 1200, // 0x000004B0
    ERROR_CONNECTION_UNAVAIL = 1201, // 0x000004B1
    ERROR_INVALID_DOMAINNAME = 1212, // 0x000004BC
    ERROR_NO_NETWORK = 1222, // 0x000004C6
    ERROR_NO_SUCH_DOMAIN = 1355, // 0x0000054B
    NERR_BASE = 2100, // 0x00000834
    NERR_NetNotStarted = 2102, // 0x00000836
    NERR_ServerNotStarted = 2114, // 0x00000842
    NERR_BufTooSmall = 2123, // 0x0000084B
    NERR_WkstaNotStarted = 2138, // 0x0000085A
    NERR_ServiceNotInstalled = 2184, // 0x00000888
    ERROR_NOT_CONNECTED = 2250, // 0x000008CA
    NERR_NetNameNotFound = 2310, // 0x00000906
    ERROR_NO_BROWSER_SERVERS_FOUND = 6118, // 0x000017E6
  }
}
