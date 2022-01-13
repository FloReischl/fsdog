// Decompiled with JetBrains decompiler
// Type: FR.IO.ShellFileAttributes
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

namespace FR.IO {
    public enum ShellFileAttributes {
        ReadOnly = 1,
        Hidden = 2,
        System = 4,
        Directory = 16, // 0x00000010
        Archive = 32, // 0x00000020
        Device = 64, // 0x00000040
        Normal = 128, // 0x00000080
        Temporary = 256, // 0x00000100
        SparseFile = 512, // 0x00000200
        ReparsePoint = 1024, // 0x00000400
        Compressed = 2048, // 0x00000800
        Offline = 4096, // 0x00001000
        NotContentIndexed = 8192, // 0x00002000
        Encrypted = 16384, // 0x00004000
        Virtual = 65536, // 0x00010000
    }
}
