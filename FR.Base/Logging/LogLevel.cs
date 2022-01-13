// Decompiled with JetBrains decompiler
// Type: FR.Logging.LogLevel
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

namespace FR.Logging {
    public enum LogLevel {
        None = 0,
        Exception = 1,
        Error = 2,
        Warning = 4,
        Default = 7,
        Info = 16, // 0x00000010
        Debug = 32, // 0x00000020
        Trace = 64, // 0x00000040
        User1 = 256, // 0x00000100
        User2 = 512, // 0x00000200
        User3 = 1024, // 0x00000400
        Full = 65535, // 0x0000FFFF
    }
}
