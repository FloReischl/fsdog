// Decompiled with JetBrains decompiler
// Type: FR.Drawing.ImageHelper
// Assembly: FR.Drawing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=a1c377fe888b0e9a
// MVID: 5443DC0B-C77E-46BB-B960-A3DBDF862D86
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Drawing.dll

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FR.Drawing
{
  public static class ImageHelper
  {
    private const int MAX_PATH = 260;

    [DllImport("Shell32.dll")]
    private static extern int SHGetFileInfo(
      string pszPath,
      uint dwFileAttributes,
      ref ImageHelper.SHFILEINFO psfi,
      uint cbfileInfo,
      ImageHelper.SHGFI uFlags);

    public static Icon ExtractAssociatedIcon(string path, bool small)
    {
      ImageHelper.SHGFI flags = !small ? ImageHelper.SHGFI.SHGFI_ICON : ImageHelper.SHGFI.SHGFI_ICON | ImageHelper.SHGFI.SHGFI_SMALLICON;
      ImageHelper.SHFILEINFO info;
      return ImageHelper.GetSHFILEINFO(path, flags, out info) != 0 ? Icon.FromHandle(info.hIcon) : (Icon) null;
    }

    public static Image ExtractAssociatedImage(string path, bool small)
    {
      Icon associatedIcon = ImageHelper.ExtractAssociatedIcon(path, small);
      if (associatedIcon == null)
        return (Image) null;
      Image associatedImage = (Image) Bitmap.FromHicon(associatedIcon.Handle);
      associatedIcon.Dispose();
      return associatedImage;
    }

    private static int GetSHFILEINFO(
      string path,
      ImageHelper.SHGFI flags,
      out ImageHelper.SHFILEINFO info)
    {
      info = new ImageHelper.SHFILEINFO();
      uint cbfileInfo = (uint) Marshal.SizeOf((object) info);
      return ImageHelper.SHGetFileInfo(path, 0U, ref info, cbfileInfo, flags);
    }

    private struct SHFILEINFO
    {
      public IntPtr hIcon;
      public int iIcon;
      public int dwAttributes;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
      public string szDisplayName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
      public string szTypeName;
    }

    private enum SHGFI
    {
      SHGFI_LARGEICON = 0,
      SHGFI_SMALLICON = 1,
      SHGFI_OPENICON = 2,
      SHGFI_SHELLICONSIZE = 4,
      SHGFI_USEFILEATTRIBUTES = 16, // 0x00000010
      SHGFI_ADDOVERLAYS = 32, // 0x00000020
      SHGFI_OVERLAYINDEX = 64, // 0x00000040
      SHGFI_ICON = 256, // 0x00000100
      SHGFI_DISPLAYNAME = 512, // 0x00000200
      SHGFI_TYPENAME = 1024, // 0x00000400
      SHGFI_ATTRIBUTES = 2048, // 0x00000800
      SHGFI_ICONLOCATION = 4096, // 0x00001000
      SHGFI_EXETYPE = 8192, // 0x00002000
      SHGFI_SYSICONINDEX = 16384, // 0x00004000
      SHGFI_LINKOVERLAY = 32768, // 0x00008000
      SHGFI_SELECTED = 65536, // 0x00010000
      SHGFI_ATTR_SPECIFIED = 131072, // 0x00020000
    }
  }
}
