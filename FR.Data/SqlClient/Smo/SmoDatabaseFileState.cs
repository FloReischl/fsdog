// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDatabaseFileState
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

namespace FR.Data.SqlClient.Smo
{
  public enum SmoDatabaseFileState
  {
    Online = 0,
    Restoring = 1,
    Recovering = 2,
    RecoveryPending = 3,
    Suspect = 4,
    Offline = 6,
    Defunct = 7,
  }
}
