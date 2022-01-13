// Decompiled with JetBrains decompiler
// Type: FR.IO.SLR_FLAGS
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;

namespace FR.IO
{
  [Flags]
  internal enum SLR_FLAGS
  {
    SLR_NO_UI = 1,
    SLR_ANY_MATCH = 2,
    SLR_UPDATE = 4,
    SLR_NOUPDATE = 8,
    SLR_NOSEARCH = 16, // 0x00000010
    SLR_NOTRACK = 32, // 0x00000020
    SLR_NOLINKINFO = 64, // 0x00000040
    SLR_INVOKE_MSI = 128, // 0x00000080
  }
}
