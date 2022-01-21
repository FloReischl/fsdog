// Decompiled with JetBrains decompiler
// Type: FR.IO.FileHelper
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace FR.IO {
    public static class FileHelper {
        private const uint SW_NORMAL = 1;
        private const uint SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;
        private const int MAX_PATH = 260;

        [DllImport("shell32.dll", SetLastError = true)]
        private static extern bool ShellExecuteEx(ref FileHelper.SHELLEXECUTEINFO lpExecInfo);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHFileOperation(ref FileHelper.SHFILEOPSTRUCT FileOp);

        [DllImport("Shell32.dll")]
        private static extern int SHGetFileInfo(
          string pszPath,
          uint dwFileAttributes,
          ref FileHelper.SHFILEINFO psfi,
          uint cbfileInfo,
          FileHelper.SHGFI uFlags);

        public static void CopyToClipboard(FileSystemInfo[] fileInfos, DragDropEffects effects) {
            List<string> stringList = new List<string>();
            foreach (FileSystemInfo fileInfo in fileInfos)
                stringList.Add(fileInfo.FullName);
            string[] array = stringList.ToArray();
            if (array == null)
                return;
            IDataObject data1 = (IDataObject)new DataObject(DataFormats.FileDrop, (object)array);
            MemoryStream data2 = new MemoryStream(4);
            byte[] buffer = new byte[4]
            {
        (byte) effects,
        (byte) 0,
        (byte) 0,
        (byte) 0
            };
            data2.Write(buffer, 0, buffer.Length);
            data1.SetData("Preferred DropEffect", (object)data2);
            Clipboard.SetDataObject((object)data1);
        }

        public static void CopyTo(IntPtr handle, string[] files, string destination, bool silent) {
            string str = string.Join("\0", files) + "\0";
            var arg = new FileHelper.SHFILEOPSTRUCT() {
                hwnd = handle,
                wFunc = FileHelper.ShellFileOperationFunc.FO_COPY,
                pFrom = str,
                pTo = destination + "\0"
            };
            FileHelper.SHFileOperation(ref arg);
        }

        public static void CopyTo(
          IntPtr handle,
          IEnumerable<ShellItem> items,
          string destination,
          bool silent) {
            List<string> stringList = new List<string>();
            foreach (ShellItem shellItem in items)
                stringList.Add(shellItem.FullName);
            FileHelper.CopyTo(handle, stringList.ToArray(), destination, silent);
        }

        public static void Delete(IntPtr handle, string[] files, bool moveToRecycleBin, bool silent) {
            string str = string.Join("\0", files) + "\0";
            FileHelper.SHFILEOPSTRUCT FileOp = new FileHelper.SHFILEOPSTRUCT();
            FileOp.hwnd = handle;
            FileOp.wFunc = FileHelper.ShellFileOperationFunc.FO_DELETE;
            if (silent)
                FileOp.fFlags |= FileHelper.ShellFileOperationFlags.FOF_SILENT;
            if (moveToRecycleBin)
                FileOp.fFlags |= FileHelper.ShellFileOperationFlags.FOF_ALLOWUNDO;
            FileOp.pFrom = str;
            FileHelper.SHFileOperation(ref FileOp);
        }

        public static void Delete(string fileName, bool moveToRecycleBin) {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Can not find file.", fileName);
            FileHelper.SHFILEOPSTRUCT FileOp = new FileHelper.SHFILEOPSTRUCT();
            FileOp.wFunc = FileHelper.ShellFileOperationFunc.FO_DELETE;
            if (moveToRecycleBin)
                FileOp.fFlags |= FileHelper.ShellFileOperationFlags.FOF_ALLOWUNDO;
            FileOp.pFrom = fileName;
            FileHelper.SHFileOperation(ref FileOp);
        }

        public static void DeleteDirectory(string fullName, bool moveToRecycleBin) {
            if (!Directory.Exists(fullName))
                throw new FileNotFoundException("Can not find file.", fullName);
            fullName += "\0";
            FileHelper.SHFILEOPSTRUCT FileOp = new FileHelper.SHFILEOPSTRUCT();
            FileOp.wFunc = FileHelper.ShellFileOperationFunc.FO_DELETE;
            FileOp.pFrom = fullName;
            if (moveToRecycleBin)
                FileOp.fFlags |= FileHelper.ShellFileOperationFlags.FOF_ALLOWUNDO;
            FileHelper.SHFileOperation(ref FileOp);
        }

        public static string GetDisplayName(string fileName) {
            SHGFI flags = SHGFI.SHGFI_DISPLAYNAME;
            return FileHelper.GetSHFILEINFO(fileName, flags, out SHFILEINFO info) != 0 ? info.szDisplayName : (string)null;
        }

        public static DragDropEffects GetFilesFromClipboard(ref List<ShellItem> items) {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (!dataObject.GetDataPresent(DataFormats.FileDrop))
                return DragDropEffects.None;
            MemoryStream data = (MemoryStream)dataObject.GetData("Preferred DropEffect");
            if (data == null)
                return DragDropEffects.None;
            DragDropEffects filesFromClipboard = (DragDropEffects)data.ReadByte();
            foreach (string str in (string[])dataObject.GetData(DataFormats.FileDrop)) {
                if (Directory.Exists(str))
                    items.Add((ShellItem)new ShellDirectory(str));
                else if (File.Exists(str))
                    items.Add((ShellItem)new ShellFile(str));
            }
            return filesFromClipboard;
        }

        public static string GetNotExistingDirectoryName(
          DirectoryInfo dir,
          string mask,
          int indexStart) {
            string directory = dir.FullName;
            for (ShellDirectory shellDirectory = new ShellDirectory(directory); shellDirectory.Exists; shellDirectory = new ShellDirectory(directory)) {
                string path2 = string.Format(mask, (object)Path.GetFileName(dir.FullName), (object)indexStart++);
                directory = Path.Combine(dir.Parent.FullName, path2);
            }
            return directory;
        }

        public static string GetTempFile() => FileHelper.GetTempFile(Environment.GetEnvironmentVariable("TEMP"));

        public static string GetTempFile(string directoryName) {
            if (!Directory.Exists(directoryName))
                throw IOExceptionHelper.GetDirectoryNotFound(directoryName);
            string path;
            do {
                path = Path.Combine(directoryName, Guid.NewGuid().ToString() + ".tmp");
            }
            while (File.Exists(path));
            return path;
        }

        public static string GetTypeName(string fileName) {
            SHGFI flags = SHGFI.SHGFI_TYPENAME;
            return FileHelper.GetSHFILEINFO(fileName, flags, out SHFILEINFO info) != 0 ? info.szTypeName : (string)null;
        }

        public static void MoveTo(IntPtr handle, string[] files, string destination, bool silent) {
            string str = string.Join("\0", files) + "\0";
            FileHelper.SHFILEOPSTRUCT FileOp = new FileHelper.SHFILEOPSTRUCT();
            FileOp.hwnd = handle;
            FileOp.wFunc = FileHelper.ShellFileOperationFunc.FO_MOVE;
            if (silent)
                FileOp.fFlags |= FileHelper.ShellFileOperationFlags.FOF_SILENT;
            FileOp.pFrom = str;
            FileOp.pTo = destination;
            FileHelper.SHFileOperation(ref FileOp);
        }

        public static void MoveTo(
          IntPtr handle,
          IEnumerable<ShellItem> items,
          string destination,
          bool silent) {
            List<string> stringList = new List<string>();
            foreach (ShellItem shellItem in items)
                stringList.Add(shellItem.FullName);
            FileHelper.MoveTo(handle, stringList.ToArray(), destination, silent);
        }

        public static void ShellExecute(string fileName) {
            FileHelper.SHELLEXECUTEINFO lpExecInfo = new FileHelper.SHELLEXECUTEINFO();
            lpExecInfo.cbSize = Marshal.SizeOf((object)lpExecInfo);
            lpExecInfo.lpFile = fileName;
            lpExecInfo.nShow = 1U;
            if (!FileHelper.ShellExecuteEx(ref lpExecInfo))
                throw new Win32Exception();
        }

        public static void ShellExecute(
          string fileName,
          string arguments,
          string userName,
          string password) {
            SecureString secureString = (SecureString)null;
            if (password != null) {
                secureString = new SecureString();
                foreach (char c in password)
                    secureString.AppendChar(c);
            }
            Process.Start(new ProcessStartInfo() {
                FileName = fileName,
                Arguments = arguments,
                UserName = userName,
                Password = secureString,
                Verb = "runas"
            });
        }

        public static void ShellOpenWith(string fileName) {
            FileHelper.SHELLEXECUTEINFO lpExecInfo = new FileHelper.SHELLEXECUTEINFO();
            lpExecInfo.cbSize = Marshal.SizeOf((object)lpExecInfo);
            lpExecInfo.lpVerb = "openas";
            lpExecInfo.lpFile = fileName;
            lpExecInfo.nShow = 1U;
            if (!FileHelper.ShellExecuteEx(ref lpExecInfo))
                throw new Win32Exception();
        }

        public static void ShowPropertiesDialog(string fileName) {
            FileHelper.SHELLEXECUTEINFO lpExecInfo = new FileHelper.SHELLEXECUTEINFO();
            lpExecInfo.cbSize = Marshal.SizeOf((object)lpExecInfo);
            lpExecInfo.lpFile = fileName;
            lpExecInfo.nShow = 5U;
            lpExecInfo.fMask = 12U;
            lpExecInfo.lpVerb = "properties";
            FileHelper.ShellExecuteEx(ref lpExecInfo);
        }

        private static int GetSHFILEINFO(
          string path,
          FileHelper.SHGFI flags,
          out FileHelper.SHFILEINFO info) {
            info = new FileHelper.SHFILEINFO();
            uint cbfileInfo = (uint)Marshal.SizeOf((object)info);
            return FileHelper.SHGetFileInfo(path, 0U, ref info, cbfileInfo, flags);
        }

        [Serializable]
        private struct SHELLEXECUTEINFO {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            public string lpVerb;
            public string lpFile;
            public string lpParameters;
            public string lpDirectory;
            public uint nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct SHFILEOPSTRUCT {
            public IntPtr hwnd;
            public FileHelper.ShellFileOperationFunc wFunc;
            public string pFrom;
            public string pTo;
            public FileHelper.ShellFileOperationFlags fFlags;
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        public enum ShellFileOperationFunc {
            FO_MOVE = 1,
            FO_COPY = 2,
            FO_DELETE = 3,
            FO_RENAME = 4,
        }

        public enum ShellFileOperationFlags {
            FOF_MULTIDESTFILES = 1,
            FOF_CONFIRMMOUSE = 2,
            FOF_SILENT = 4,
            FOF_RENAMEONCOLLISION = 8,
            FOF_NOCONFIRMATION = 16, // 0x00000010
            FOF_WANTMAPPINGHANDLE = 32, // 0x00000020
            FOF_ALLOWUNDO = 64, // 0x00000040
            FOF_FILESONLY = 128, // 0x00000080
            FOF_SIMPLEPROGRESS = 256, // 0x00000100
            FOF_NOCONFIRMMKDIR = 512, // 0x00000200
            FOF_NOERRORUI = 1024, // 0x00000400
            FOF_NOCOPYSECURITYATTRIBS = 2048, // 0x00000800
            FOF_NORECURSION = 4096, // 0x00001000
            FOF_NO_CONNECTED_ELEMENTS = 8192, // 0x00002000
            FOF_WANTNUKEWARNING = 16384, // 0x00004000
            FOF_NORECURSEREPARSE = 32768, // 0x00008000
        }

        private struct SHFILEINFO {
            public IntPtr hIcon;
            public int iIcon;
            public int dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        private enum SHGFI {
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
