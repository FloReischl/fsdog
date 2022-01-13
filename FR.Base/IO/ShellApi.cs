// Decompiled with JetBrains decompiler
// Type: FR.IO.ShellApi
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Runtime.InteropServices;

namespace FR.IO
{
  internal static class ShellApi
  {
    internal const int MAX_PATH = 260;
    internal static readonly IntPtr InvalidHandle = new IntPtr(-1);

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr ExtractIcon(
      IntPtr hInst,
      string lpszExeFileName,
      int nIconIndex);

    [DllImport("user32.dll")]
    internal static extern bool DestroyIcon(IntPtr hIcon);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr FindFirstFile(
      string fileName,
      [In, Out] WIN32_FIND_DATA lpFindFileData);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    internal static extern bool FindNextFile(IntPtr hFindFile, [In, Out] WIN32_FIND_DATA lpFindFileData);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    internal static extern bool FindClose(IntPtr hFindFile);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    internal static extern bool MoveFile(string lpExistingFileName, string lpNewFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    internal static extern bool CopyFile(
      string lpExistingFileName,
      string lpNewFileName,
      bool bFailIfExists);

    internal static ulong IntToLong(int lowOrder, int highOrder)
    {
      byte[] bytes = BitConverter.GetBytes(highOrder);
      Array.Resize<byte>(ref bytes, 8);
      return BitConverter.ToUInt64(bytes, 0) << 32 | (ulong) (uint) lowOrder;
    }
  }
}
