// Decompiled with JetBrains decompiler
// Type: FR.Security.Permissions.DeviceConnector
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Net;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace FR.Security.Permissions {
    public static class DeviceConnector {
        private const int RESOURCETYPE_ANY = 0;
        private const int RESOURCETYPE_DISK = 1;
        private const int RESOURCETYPE_PRINT = 2;
        private const int CONNECT_UPDATE_PROFILE = 1;
        private const int CONNECT_UPDATE_RECENT = 2;
        private const int CONNECT_TEMPORARY = 4;
        private const int CONNECT_INTERACTIVE = 8;
        private const int CONNECT_PROMPT = 16;
        private const int CONNECT_NEED_DRIVE = 32;
        private const int CONNECT_REFCOUNT = 64;
        private const int CONNECT_REDIRECT = 128;
        private const int CONNECT_LOCALDRIVE = 256;
        private const int CONNECT_CURRENT_MEDIA = 512;
        private const int CONNECT_DEFERRED = 1024;
        private const int CONNECT_COMMANDLINE = 2048;
        private const int CONNECT_CMD_SAVECRED = 4096;

        [DllImport("mpr.dll")]
        private static extern NetworkErrorCode WNetAddConnection2A(
          [MarshalAs(UnmanagedType.LPArray)] DeviceConnector.NETRESOURCEA[] lpNetResource,
          [MarshalAs(UnmanagedType.LPStr)] string lpPassword,
          [MarshalAs(UnmanagedType.LPStr)] string lpUserName,
          int dwFlags);

        [DllImport("mpr.dll")]
        private static extern NetworkErrorCode WNetAddConnection3(
          IntPtr hwndOwner,
          [MarshalAs(UnmanagedType.Struct)] ref DeviceConnector.NETRESOURCEA lpNetResource,
          [MarshalAs(UnmanagedType.LPStr)] string lpPassword,
          [MarshalAs(UnmanagedType.LPStr)] string lpUserName,
          int dwFlags);

        [DllImport("mpr.dll")]
        private static extern NetworkErrorCode WNetCancelConnection(
          [MarshalAs(UnmanagedType.LPStr)] string lpName,
          bool fForce);

        public static void Connect(IntPtr hwndOwner, string uncPath, string driveLetter) {
            var arg = new DeviceConnector.NETRESOURCEA() {
                dwType = 1,
                lpLocalName = driveLetter,
                lpRemoteName = uncPath,
                lpProvider = (string)null
            };
            NetworkErrorCode error = DeviceConnector.WNetAddConnection3(hwndOwner, ref arg, (string)null, (string)null, 8);
            if (error != NetworkErrorCode.NERR_Success)
                throw new Win32Exception((int)error);
        }

        public static bool Connect(
          string uncPath,
          string driveLetter,
          string userName,
          string password) {
            DeviceConnector.NETRESOURCEA[] lpNetResource = new DeviceConnector.NETRESOURCEA[1];
            lpNetResource[0].lpRemoteName = uncPath;
            lpNetResource[0].lpLocalName = driveLetter;
            lpNetResource[0].dwType = 1;
            lpNetResource[0].lpProvider = (string)null;
            NetworkErrorCode error = DeviceConnector.WNetAddConnection2A(lpNetResource, password, userName, 1);
            if (error != NetworkErrorCode.NERR_Success)
                throw new Win32Exception((int)error);
            return true;
        }

        public static bool Disconnect(string sDrive) {
            NetworkErrorCode error = DeviceConnector.WNetCancelConnection(sDrive, true);
            if (error != NetworkErrorCode.NERR_Success)
                throw new Win32Exception((int)error);
            return true;
        }

        private struct NETRESOURCEA {
            public int dwScope;
            public int dwType;
            public int dwDisplayType;
            public int dwUsage;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpLocalName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpRemoteName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpComment;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpProvider;
        }
    }
}
