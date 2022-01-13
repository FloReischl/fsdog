// Decompiled with JetBrains decompiler
// Type: FR.IO.IPersistFile
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FR.IO {
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000010B-0000-0000-C000-000000000046")]
    [ComImport]
    internal interface IPersistFile {
        void GetClassID(out Guid pClassID);

        [MethodImpl(MethodImplOptions.PreserveSig)]
        int IsDirty();

        void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

        void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

        void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

        void GetCurFile(out IntPtr ppszFileName);
    }
}
