// Decompiled with JetBrains decompiler
// Type: FR.IO.IShellLinkA
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FR.IO {
    [Guid("000214EE-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    internal interface IShellLinkA {
        void GetPath(
          [MarshalAs(UnmanagedType.LPStr), Out] StringBuilder pszFile,
          int cchMaxPath,
          out WIN32_FIND_DATAA pfd,
          SLGP_FLAGS fFlags);

        void GetIDList(out IntPtr ppidl);

        void SetIDList(IntPtr pidl);

        void GetDescription([MarshalAs(UnmanagedType.LPStr), Out] StringBuilder pszName, int cchMaxName);

        void SetDescription([MarshalAs(UnmanagedType.LPStr)] string pszName);

        void GetWorkingDirectory([MarshalAs(UnmanagedType.LPStr), Out] StringBuilder pszDir, int cchMaxPath);

        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPStr)] string pszDir);

        void GetArguments([MarshalAs(UnmanagedType.LPStr), Out] StringBuilder pszArgs, int cchMaxPath);

        void SetArguments([MarshalAs(UnmanagedType.LPStr)] string pszArgs);

        void GetHotkey(out short pwHotkey);

        void SetHotkey(short wHotkey);

        void GetShowCmd(out int piShowCmd);

        void SetShowCmd(int iShowCmd);

        void GetIconLocation([MarshalAs(UnmanagedType.LPStr), Out] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

        void SetIconLocation([MarshalAs(UnmanagedType.LPStr)] string pszIconPath, int iIcon);

        void SetRelativePath([MarshalAs(UnmanagedType.LPStr)] string pszPathRel, int dwReserved);

        void Resolve(IntPtr hwnd, SLR_FLAGS fFlags);

        void SetPath([MarshalAs(UnmanagedType.LPStr)] string pszFile);
    }
}
