// Decompiled with JetBrains decompiler
// Type: FsDog.ShellContextMenu
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FsDog {
    public class ShellContextMenu : NativeWindow {
        private const int MAX_PATH = 260;
        private const uint CMD_FIRST = 1;
        private const uint CMD_LAST = 30000;
        private const int S_OK = 0;
        private const int S_FALSE = 1;
        private ShellContextMenu.IContextMenu _oContextMenu;
        private ShellContextMenu.IContextMenu2 _oContextMenu2;
        private ShellContextMenu.IContextMenu3 _oContextMenu3;
        private ShellContextMenu.IShellFolder _oDesktopFolder;
        private ShellContextMenu.IShellFolder _oParentFolder;
        private IntPtr[] _arrPIDLs;
        private string _strParentFolder;
        private static int cbMenuItemInfo = Marshal.SizeOf(typeof(ShellContextMenu.MENUITEMINFO));
        private static int cbInvokeCommand = Marshal.SizeOf(typeof(ShellContextMenu.CMINVOKECOMMANDINFOEX));
        private static Guid IID_IShellFolder = new Guid("{000214E6-0000-0000-C000-000000000046}");
        private static Guid IID_IContextMenu = new Guid("{000214e4-0000-0000-c000-000000000046}");
        private static Guid IID_IContextMenu2 = new Guid("{000214f4-0000-0000-c000-000000000046}");
        private static Guid IID_IContextMenu3 = new Guid("{bcfce0a0-ec17-11d0-8d10-00a0c90f2719}");

        public ShellContextMenu() => this.CreateHandle(new CreateParams());

        ~ShellContextMenu() => this.ReleaseAll();

        private bool GetContextMenuInterfaces(
          ShellContextMenu.IShellFolder oParentFolder,
          IntPtr[] arrPIDLs,
          out IntPtr ctxMenuPtr) {
            if (oParentFolder.GetUIObjectOf(IntPtr.Zero, (uint)arrPIDLs.Length, arrPIDLs, ref ShellContextMenu.IID_IContextMenu, IntPtr.Zero, out ctxMenuPtr) == 0) {
                this._oContextMenu = (ShellContextMenu.IContextMenu)Marshal.GetTypedObjectForIUnknown(ctxMenuPtr, typeof(ShellContextMenu.IContextMenu));
                return true;
            }
            ctxMenuPtr = IntPtr.Zero;
            this._oContextMenu = (ShellContextMenu.IContextMenu)null;
            return false;
        }

        protected override void WndProc(ref Message m) {
            if (this._oContextMenu != null && m.Msg == 287 && ((int)ShellHelper.HiWord(m.WParam) & 2048) == 0 && ((int)ShellHelper.HiWord(m.WParam) & 16) == 0) {
                string empty = string.Empty;
                int num = (int)ShellHelper.LoWord(m.WParam);
            }
            if (this._oContextMenu2 != null && (m.Msg == 279 || m.Msg == 44 || m.Msg == 43) && this._oContextMenu2.HandleMenuMsg((uint)m.Msg, m.WParam, m.LParam) == 0 || this._oContextMenu3 != null && m.Msg == 288 && this._oContextMenu3.HandleMenuMsg2((uint)m.Msg, m.WParam, m.LParam, IntPtr.Zero) == 0)
                return;
            base.WndProc(ref m);
        }

        private void InvokeCommand(
          ShellContextMenu.IContextMenu oContextMenu,
          uint nCmd,
          string strFolder,
          Point pointInvoke) {
            var arg = new ShellContextMenu.CMINVOKECOMMANDINFOEX() {
                cbSize = ShellContextMenu.cbInvokeCommand,
                lpVerb = (IntPtr)(long)(nCmd - 1U),
                lpDirectory = strFolder,
                lpVerbW = (IntPtr)(long)(nCmd - 1U),
                lpDirectoryW = strFolder,
                fMask = (ShellContextMenu.CMIC)(536887296 | ((Control.ModifierKeys & Keys.Control) != Keys.None ? 1073741824 : 0) | ((Control.ModifierKeys & Keys.Shift) != Keys.None ? 268435456 : 0)),
                ptInvoke = new ShellContextMenu.POINT(pointInvoke.X, pointInvoke.Y),
                nShow = ShellContextMenu.SW.SHOWNORMAL
            };
            oContextMenu.InvokeCommand(ref arg);
        }

        private void ReleaseAll() {
            if (this._oContextMenu != null) {
                Marshal.ReleaseComObject((object)this._oContextMenu);
                this._oContextMenu = (ShellContextMenu.IContextMenu)null;
            }
            if (this._oContextMenu2 != null) {
                Marshal.ReleaseComObject((object)this._oContextMenu2);
                this._oContextMenu2 = (ShellContextMenu.IContextMenu2)null;
            }
            if (this._oContextMenu3 != null) {
                Marshal.ReleaseComObject((object)this._oContextMenu3);
                this._oContextMenu3 = (ShellContextMenu.IContextMenu3)null;
            }
            if (this._oDesktopFolder != null) {
                Marshal.ReleaseComObject((object)this._oDesktopFolder);
                this._oDesktopFolder = (ShellContextMenu.IShellFolder)null;
            }
            if (this._oParentFolder != null) {
                Marshal.ReleaseComObject((object)this._oParentFolder);
                this._oParentFolder = (ShellContextMenu.IShellFolder)null;
            }
            if (this._arrPIDLs == null)
                return;
            this.FreePIDLs(this._arrPIDLs);
            this._arrPIDLs = (IntPtr[])null;
        }

        private ShellContextMenu.IShellFolder GetDesktopFolder() {
            IntPtr ppshf = IntPtr.Zero;
            if (this._oDesktopFolder == null) {
                if (ShellContextMenu.SHGetDesktopFolder(out ppshf) != 0)
                    throw new ShellContextMenuException("Failed to get the desktop shell folder");
                this._oDesktopFolder = (ShellContextMenu.IShellFolder)Marshal.GetTypedObjectForIUnknown(ppshf, typeof(ShellContextMenu.IShellFolder));
            }
            return this._oDesktopFolder;
        }

        private ShellContextMenu.IShellFolder GetParentFolder(string folderName) {
            if (this._oParentFolder == null) {
                ShellContextMenu.IShellFolder desktopFolder = this.GetDesktopFolder();
                if (desktopFolder == null)
                    return (ShellContextMenu.IShellFolder)null;
                IntPtr ppidl = IntPtr.Zero;
                uint pchEaten = 0;
                ShellContextMenu.SFGAO pdwAttributes = (ShellContextMenu.SFGAO)0;
                if (desktopFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, folderName, ref pchEaten, out ppidl, ref pdwAttributes) != 0)
                    return (ShellContextMenu.IShellFolder)null;
                IntPtr num1 = Marshal.AllocCoTaskMem(524);
                Marshal.WriteInt32(num1, 0, 0);
                this._oDesktopFolder.GetDisplayNameOf(ppidl, ShellContextMenu.SHGNO.FORPARSING, num1);
                StringBuilder pszBuf = new StringBuilder(260);
                ShellContextMenu.StrRetToBuf(num1, ppidl, pszBuf, 260);
                Marshal.FreeCoTaskMem(num1);
                IntPtr zero = IntPtr.Zero;
                this._strParentFolder = pszBuf.ToString();
                IntPtr ppv = IntPtr.Zero;
                int num2 = desktopFolder.BindToObject(ppidl, IntPtr.Zero, ref ShellContextMenu.IID_IShellFolder, out ppv);
                Marshal.FreeCoTaskMem(ppidl);
                if (num2 != 0)
                    return (ShellContextMenu.IShellFolder)null;
                this._oParentFolder = (ShellContextMenu.IShellFolder)Marshal.GetTypedObjectForIUnknown(ppv, typeof(ShellContextMenu.IShellFolder));
            }
            return this._oParentFolder;
        }

        protected IntPtr[] GetPIDLs(FileInfo[] arrFI) {
            if (arrFI == null || arrFI.Length == 0)
                return (IntPtr[])null;
            ShellContextMenu.IShellFolder parentFolder = this.GetParentFolder(arrFI[0].DirectoryName);
            if (parentFolder == null)
                return (IntPtr[])null;
            IntPtr[] arrPIDLs = new IntPtr[arrFI.Length];
            int index = 0;
            foreach (FileInfo fileInfo in arrFI) {
                uint pchEaten = 0;
                ShellContextMenu.SFGAO pdwAttributes = (ShellContextMenu.SFGAO)0;
                IntPtr ppidl = IntPtr.Zero;
                if (parentFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, fileInfo.Name, ref pchEaten, out ppidl, ref pdwAttributes) != 0) {
                    this.FreePIDLs(arrPIDLs);
                    return (IntPtr[])null;
                }
                arrPIDLs[index] = ppidl;
                ++index;
            }
            return arrPIDLs;
        }

        protected IntPtr[] GetPIDLs(DirectoryInfo[] arrFI) {
            if (arrFI == null || arrFI.Length == 0)
                return (IntPtr[])null;
            ShellContextMenu.IShellFolder parentFolder = this.GetParentFolder(arrFI[0].Parent.FullName);
            if (parentFolder == null)
                return (IntPtr[])null;
            IntPtr[] arrPIDLs = new IntPtr[arrFI.Length];
            int index = 0;
            foreach (DirectoryInfo directoryInfo in arrFI) {
                uint pchEaten = 0;
                ShellContextMenu.SFGAO pdwAttributes = (ShellContextMenu.SFGAO)0;
                IntPtr ppidl = IntPtr.Zero;
                if (parentFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, directoryInfo.Name, ref pchEaten, out ppidl, ref pdwAttributes) != 0) {
                    this.FreePIDLs(arrPIDLs);
                    return (IntPtr[])null;
                }
                arrPIDLs[index] = ppidl;
                ++index;
            }
            return arrPIDLs;
        }

        protected IntPtr[] GetPIDLs(FileSystemInfo[] arrFI) {
            if (arrFI == null || arrFI.Length == 0)
                return (IntPtr[])null;
            ShellContextMenu.IShellFolder shellFolder = (ShellContextMenu.IShellFolder)null;
            FileSystemInfo fileSystemInfo1 = arrFI[0];
            if (fileSystemInfo1 is FileInfo)
                shellFolder = this.GetParentFolder(((FileInfo)fileSystemInfo1).DirectoryName);
            else if (fileSystemInfo1 is DirectoryInfo)
                shellFolder = this.GetParentFolder(((DirectoryInfo)fileSystemInfo1).Parent.FullName);
            if (shellFolder == null)
                return (IntPtr[])null;
            IntPtr[] arrPIDLs = new IntPtr[arrFI.Length];
            int index = 0;
            foreach (FileSystemInfo fileSystemInfo2 in arrFI) {
                uint pchEaten = 0;
                ShellContextMenu.SFGAO pdwAttributes = (ShellContextMenu.SFGAO)0;
                IntPtr ppidl = IntPtr.Zero;
                if (shellFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, fileSystemInfo2.Name, ref pchEaten, out ppidl, ref pdwAttributes) != 0) {
                    this.FreePIDLs(arrPIDLs);
                    return (IntPtr[])null;
                }
                arrPIDLs[index] = ppidl;
                ++index;
            }
            return arrPIDLs;
        }

        protected void FreePIDLs(IntPtr[] arrPIDLs) {
            if (arrPIDLs == null)
                return;
            for (int index = 0; index < arrPIDLs.Length; ++index) {
                if (arrPIDLs[index] != IntPtr.Zero) {
                    Marshal.FreeCoTaskMem(arrPIDLs[index]);
                    arrPIDLs[index] = IntPtr.Zero;
                }
            }
        }

        private void InvokeContextMenuDefault(FileInfo[] arrFI) {
            this.ReleaseAll();
            IntPtr num = IntPtr.Zero;
            IntPtr ctxMenuPtr = IntPtr.Zero;
            try {
                this._arrPIDLs = this.GetPIDLs(arrFI);
                if (this._arrPIDLs == null)
                    this.ReleaseAll();
                else if (!this.GetContextMenuInterfaces(this._oParentFolder, this._arrPIDLs, out ctxMenuPtr)) {
                    this.ReleaseAll();
                }
                else {
                    num = ShellContextMenu.CreatePopupMenu();
                    this._oContextMenu.QueryContextMenu(num, 0U, 1U, 30000U, (ShellContextMenu.CMF)(1 | ((Control.ModifierKeys & Keys.Shift) != Keys.None ? 256 : 0)));
                    uint menuDefaultItem = (uint)ShellContextMenu.GetMenuDefaultItem(num, false, 0U);
                    if (menuDefaultItem >= 1U)
                        this.InvokeCommand(this._oContextMenu, menuDefaultItem, arrFI[0].DirectoryName, Control.MousePosition);
                    ShellContextMenu.DestroyMenu(num);
                    num = IntPtr.Zero;
                }
            }
            catch {
                throw;
            }
            finally {
                if (num != IntPtr.Zero)
                    ShellContextMenu.DestroyMenu(num);
                this.ReleaseAll();
            }
        }

        public void ShowContextMenu(FileInfo[] files, Point pointScreen) {
            this.ReleaseAll();
            this._arrPIDLs = this.GetPIDLs(files);
            this.ShowContextMenu(pointScreen);
        }

        public void ShowContextMenu(DirectoryInfo[] dirs, Point pointScreen) {
            this.ReleaseAll();
            this._arrPIDLs = this.GetPIDLs(dirs);
            this.ShowContextMenu(pointScreen);
        }

        public void ShowContextMenu(FileSystemInfo[] fsis, Point pointScreen) {
            this.ReleaseAll();
            this._arrPIDLs = this.GetPIDLs(fsis);
            this.ShowContextMenu(pointScreen);
        }

        public void ShowContextMenu(Point pointScreen) {
            IntPtr num = IntPtr.Zero;
            IntPtr ctxMenuPtr = IntPtr.Zero;
            IntPtr ppv1 = IntPtr.Zero;
            IntPtr ppv2 = IntPtr.Zero;
            try {
                if (this._arrPIDLs == null)
                    this.ReleaseAll();
                else if (!this.GetContextMenuInterfaces(this._oParentFolder, this._arrPIDLs, out ctxMenuPtr)) {
                    this.ReleaseAll();
                }
                else {
                    num = ShellContextMenu.CreatePopupMenu();
                    this._oContextMenu.QueryContextMenu(num, 0U, 1U, 30000U, (ShellContextMenu.CMF)(4 | ((Control.ModifierKeys & Keys.Shift) != Keys.None ? 256 : 0)));
                    Marshal.QueryInterface(ctxMenuPtr, ref ShellContextMenu.IID_IContextMenu2, out ppv1);
                    Marshal.QueryInterface(ctxMenuPtr, ref ShellContextMenu.IID_IContextMenu3, out ppv2);
                    this._oContextMenu2 = (ShellContextMenu.IContextMenu2)Marshal.GetTypedObjectForIUnknown(ppv1, typeof(ShellContextMenu.IContextMenu2));
                    this._oContextMenu3 = (ShellContextMenu.IContextMenu3)Marshal.GetTypedObjectForIUnknown(ppv2, typeof(ShellContextMenu.IContextMenu3));
                    uint nCmd = ShellContextMenu.TrackPopupMenuEx(num, ShellContextMenu.TPM.RETURNCMD, pointScreen.X, pointScreen.Y, this.Handle, IntPtr.Zero);
                    ShellContextMenu.DestroyMenu(num);
                    num = IntPtr.Zero;
                    if (nCmd == 0U)
                        return;
                    this.InvokeCommand(this._oContextMenu, nCmd, this._strParentFolder, pointScreen);
                }
            }
            catch {
                throw;
            }
            finally {
                if (num != IntPtr.Zero)
                    ShellContextMenu.DestroyMenu(num);
                if (ctxMenuPtr != IntPtr.Zero)
                    Marshal.Release(ctxMenuPtr);
                if (ppv1 != IntPtr.Zero)
                    Marshal.Release(ppv1);
                if (ppv2 != IntPtr.Zero)
                    Marshal.Release(ppv2);
                this.ReleaseAll();
            }
        }

        private void WindowsHookInvoked(object sender, HookEventArgs e) {
            ShellContextMenu.CWPSTRUCT structure = (ShellContextMenu.CWPSTRUCT)Marshal.PtrToStructure(e.lParam, typeof(ShellContextMenu.CWPSTRUCT));
            if (this._oContextMenu2 != null && (structure.message == 279 || structure.message == 44 || structure.message == 43) && this._oContextMenu2.HandleMenuMsg((uint)structure.message, structure.wparam, structure.lparam) == 0 || this._oContextMenu3 == null || structure.message != 288)
                return;
            this._oContextMenu3.HandleMenuMsg2((uint)structure.message, structure.wparam, structure.lparam, IntPtr.Zero);
        }

        [DllImport("shell32.dll")]
        private static extern int SHGetDesktopFolder(out IntPtr ppshf);

        [DllImport("shlwapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int StrRetToBuf(
          IntPtr pstr,
          IntPtr pidl,
          StringBuilder pszBuf,
          int cchBuf);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern uint TrackPopupMenuEx(
          IntPtr hmenu,
          ShellContextMenu.TPM flags,
          int x,
          int y,
          IntPtr hwnd,
          IntPtr lptpm);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreatePopupMenu();

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DestroyMenu(IntPtr hMenu);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetMenuDefaultItem(IntPtr hMenu, bool fByPos, uint gmdiFlags);

#pragma warning disable 0649
        private struct CWPSTRUCT {
            public IntPtr lparam;
            public IntPtr wparam;
            public int message;
            public IntPtr hwnd;
        }
#pragma warning restore 0649

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CMINVOKECOMMANDINFOEX {
            public int cbSize;
            public ShellContextMenu.CMIC fMask;
            public IntPtr hwnd;
            public IntPtr lpVerb;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpParameters;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpDirectory;
            public ShellContextMenu.SW nShow;
            public int dwHotKey;
            public IntPtr hIcon;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpTitle;
            public IntPtr lpVerbW;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpParametersW;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpDirectoryW;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpTitleW;
            public ShellContextMenu.POINT ptInvoke;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct MENUITEMINFO {
            public int cbSize;
            public ShellContextMenu.MIIM fMask;
            public ShellContextMenu.MFT fType;
            public ShellContextMenu.MFS fState;
            public uint wID;
            public IntPtr hSubMenu;
            public IntPtr hbmpChecked;
            public IntPtr hbmpUnchecked;
            public IntPtr dwItemData;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string dwTypeData;
            public int cch;
            public IntPtr hbmpItem;

            public MENUITEMINFO(string text) {
                this.cbSize = ShellContextMenu.cbMenuItemInfo;
                this.dwTypeData = text;
                this.cch = text.Length;
                this.fMask = (ShellContextMenu.MIIM)0;
                this.fType = ShellContextMenu.MFT.BYCOMMAND;
                this.fState = ShellContextMenu.MFS.ENABLED;
                this.wID = 0U;
                this.hSubMenu = IntPtr.Zero;
                this.hbmpChecked = IntPtr.Zero;
                this.hbmpUnchecked = IntPtr.Zero;
                this.dwItemData = IntPtr.Zero;
                this.hbmpItem = IntPtr.Zero;
            }
        }

#pragma warning disable 0649
        private struct STGMEDIUM {
            public ShellContextMenu.TYMED tymed;
            public IntPtr hBitmap;
            public IntPtr hMetaFilePict;
            public IntPtr hEnhMetaFile;
            public IntPtr hGlobal;
            public IntPtr lpszFileName;
            public IntPtr pstm;
            public IntPtr pstg;
            public IntPtr pUnkForRelease;
        }
#pragma warning restore 0649

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct POINT {
            public int x;
            public int y;

            public POINT(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }

        [Flags]
        private enum SHGNO {
            NORMAL = 0,
            INFOLDER = 1,
            FOREDITING = 4096, // 0x00001000
            FORADDRESSBAR = 16384, // 0x00004000
            FORPARSING = 32768, // 0x00008000
        }

#pragma warning disable 0649
        [Flags]
        private enum SFGAO : uint {
            BROWSABLE = 134217728, // 0x08000000
            CANCOPY = 1,
            CANDELETE = 32, // 0x00000020
            CANLINK = 4,
            CANMONIKER = 4194304, // 0x00400000
            CANMOVE = 2,
            CANRENAME = 16, // 0x00000010
            CAPABILITYMASK = 375, // 0x00000177
            COMPRESSED = 67108864, // 0x04000000
            CONTENTSMASK = 2147483648, // 0x80000000
            DISPLAYATTRMASK = 1032192, // 0x000FC000
            DROPTARGET = 256, // 0x00000100
            ENCRYPTED = 8192, // 0x00002000
            FILESYSANCESTOR = 268435456, // 0x10000000
            FILESYSTEM = 1073741824, // 0x40000000
            FOLDER = 536870912, // 0x20000000
            GHOSTED = 32768, // 0x00008000
            HASPROPSHEET = 64, // 0x00000040
            HASSTORAGE = CANMONIKER, // 0x00400000
            HASSUBFOLDER = CONTENTSMASK, // 0x80000000
            HIDDEN = 524288, // 0x00080000
            ISSLOW = 16384, // 0x00004000
            LINK = 65536, // 0x00010000
            NEWCONTENT = 2097152, // 0x00200000
            NONENUMERATED = 1048576, // 0x00100000
            READONLY = 262144, // 0x00040000
            REMOVABLE = 33554432, // 0x02000000
            SHARE = 131072, // 0x00020000
            STORAGE = 8,
            STORAGEANCESTOR = 8388608, // 0x00800000
            STORAGECAPMASK = STORAGEANCESTOR | STORAGE | READONLY | LINK | HASSTORAGE | FOLDER | FILESYSTEM | FILESYSANCESTOR, // 0x70C50008
            STREAM = HASSTORAGE, // 0x00400000
            VALIDATE = 16777216, // 0x01000000
        }

        [Flags]
        private enum SHCONTF {
            FOLDERS = 32, // 0x00000020
            NONFOLDERS = 64, // 0x00000040
            INCLUDEHIDDEN = 128, // 0x00000080
            INIT_ON_FIRST_NEXT = 256, // 0x00000100
            NETPRINTERSRCH = 512, // 0x00000200
            SHAREABLE = 1024, // 0x00000400
            STORAGE = 2048, // 0x00000800
        }

        [Flags]
        private enum CMF : uint {
            NORMAL = 0,
            DEFAULTONLY = 1,
            VERBSONLY = 2,
            EXPLORE = 4,
            NOVERBS = 8,
            CANRENAME = 16, // 0x00000010
            NODEFAULT = 32, // 0x00000020
            INCLUDESTATIC = 64, // 0x00000040
            EXTENDEDVERBS = 256, // 0x00000100
            RESERVED = 4294901760, // 0xFFFF0000
        }

        [Flags]
        private enum GCS : uint {
            VERBA = 0,
            HELPTEXTA = 1,
            VALIDATEA = 2,
            VERBW = 4,
            HELPTEXTW = VERBW | HELPTEXTA, // 0x00000005
            VALIDATEW = VERBW | VALIDATEA, // 0x00000006
        }

        [Flags]
        private enum TPM : uint {
            LEFTBUTTON = 0,
            RIGHTBUTTON = 2,
            LEFTALIGN = 0,
            CENTERALIGN = 4,
            RIGHTALIGN = 8,
            TOPALIGN = 0,
            VCENTERALIGN = 16, // 0x00000010
            BOTTOMALIGN = 32, // 0x00000020
            HORIZONTAL = 0,
            VERTICAL = 64, // 0x00000040
            NONOTIFY = 128, // 0x00000080
            RETURNCMD = 256, // 0x00000100
            RECURSE = 1,
            HORPOSANIMATION = 1024, // 0x00000400
            HORNEGANIMATION = 2048, // 0x00000800
            VERPOSANIMATION = 4096, // 0x00001000
            VERNEGANIMATION = 8192, // 0x00002000
            NOANIMATION = 16384, // 0x00004000
            LAYOUTRTL = 32768, // 0x00008000
        }

        private enum CMD_CUSTOM {
            ExpandCollapse = 30001, // 0x00007531
        }

        [Flags]
        private enum CMIC : uint {
            HOTKEY = 32, // 0x00000020
            ICON = 16, // 0x00000010
            FLAG_NO_UI = 1024, // 0x00000400
            UNICODE = 16384, // 0x00004000
            NO_CONSOLE = 32768, // 0x00008000
            ASYNCOK = 1048576, // 0x00100000
            NOZONECHECKS = 8388608, // 0x00800000
            SHIFT_DOWN = 268435456, // 0x10000000
            CONTROL_DOWN = 1073741824, // 0x40000000
            FLAG_LOG_USAGE = 67108864, // 0x04000000
            PTINVOKE = 536870912, // 0x20000000
        }

        [Flags]
        private enum SW {
            HIDE = 0,
            SHOWNORMAL = 1,
            NORMAL = SHOWNORMAL, // 0x00000001
            SHOWMINIMIZED = 2,
            SHOWMAXIMIZED = SHOWMINIMIZED | NORMAL, // 0x00000003
            MAXIMIZE = SHOWMAXIMIZED, // 0x00000003
            SHOWNOACTIVATE = 4,
            SHOW = SHOWNOACTIVATE | NORMAL, // 0x00000005
            MINIMIZE = SHOWNOACTIVATE | SHOWMINIMIZED, // 0x00000006
            SHOWMINNOACTIVE = MINIMIZE | NORMAL, // 0x00000007
            SHOWNA = 8,
            RESTORE = SHOWNA | NORMAL, // 0x00000009
            SHOWDEFAULT = SHOWNA | SHOWMINIMIZED, // 0x0000000A
        }

        [Flags]
        private enum WM : uint {
            ACTIVATE = 6,
            ACTIVATEAPP = 28, // 0x0000001C
            AFXFIRST = 864, // 0x00000360
            AFXLAST = 895, // 0x0000037F
            APP = 32768, // 0x00008000
            ASKCBFORMATNAME = 780, // 0x0000030C
            CANCELJOURNAL = 75, // 0x0000004B
            CANCELMODE = 31, // 0x0000001F
            CAPTURECHANGED = 533, // 0x00000215
            CHANGECBCHAIN = 781, // 0x0000030D
            CHAR = 258, // 0x00000102
            CHARTOITEM = 47, // 0x0000002F
            CHILDACTIVATE = 34, // 0x00000022
            CLEAR = 771, // 0x00000303
            CLOSE = 16, // 0x00000010
            COMMAND = 273, // 0x00000111
            COMPACTING = 65, // 0x00000041
            COMPAREITEM = 57, // 0x00000039
            CONTEXTMENU = 123, // 0x0000007B
            COPY = 769, // 0x00000301
            COPYDATA = 74, // 0x0000004A
            CREATE = 1,
            CTLCOLORBTN = 309, // 0x00000135
            CTLCOLORDLG = 310, // 0x00000136
            CTLCOLOREDIT = 307, // 0x00000133
            CTLCOLORLISTBOX = 308, // 0x00000134
            CTLCOLORMSGBOX = 306, // 0x00000132
            CTLCOLORSCROLLBAR = 311, // 0x00000137
            CTLCOLORSTATIC = 312, // 0x00000138
            CUT = 768, // 0x00000300
            DEADCHAR = CREATE | CHAR, // 0x00000103
            DELETEITEM = 45, // 0x0000002D
            DESTROY = 2,
            DESTROYCLIPBOARD = 775, // 0x00000307
            DEVICECHANGE = 537, // 0x00000219
            DEVMODECHANGE = 27, // 0x0000001B
            DISPLAYCHANGE = 126, // 0x0000007E
            DRAWCLIPBOARD = 776, // 0x00000308
            DRAWITEM = 43, // 0x0000002B
            DROPFILES = 563, // 0x00000233
            ENABLE = 10, // 0x0000000A
            ENDSESSION = 22, // 0x00000016
            ENTERIDLE = 289, // 0x00000121
            ENTERMENULOOP = 529, // 0x00000211
            ENTERSIZEMOVE = 561, // 0x00000231
            ERASEBKGND = 20, // 0x00000014
            EXITMENULOOP = 530, // 0x00000212
            EXITSIZEMOVE = 562, // 0x00000232
            FONTCHANGE = 29, // 0x0000001D
            GETDLGCODE = 135, // 0x00000087
            GETFONT = 49, // 0x00000031
            GETHOTKEY = GETFONT | DESTROY, // 0x00000033
            GETICON = 127, // 0x0000007F
            GETMINMAXINFO = 36, // 0x00000024
            GETOBJECT = 61, // 0x0000003D
            GETSYSMENU = 787, // 0x00000313
            GETTEXT = 13, // 0x0000000D
            GETTEXTLENGTH = 14, // 0x0000000E
            HANDHELDFIRST = 856, // 0x00000358
            HANDHELDLAST = 863, // 0x0000035F
            HELP = 83, // 0x00000053
            HOTKEY = 786, // 0x00000312
            HSCROLL = 276, // 0x00000114
            HSCROLLCLIPBOARD = GETTEXTLENGTH | CUT, // 0x0000030E
            ICONERASEBKGND = GETMINMAXINFO | DESTROY | CREATE, // 0x00000027
            IME_CHAR = 646, // 0x00000286
            IME_COMPOSITION = 271, // 0x0000010F
            IME_COMPOSITIONFULL = 644, // 0x00000284
            IME_CONTROL = 643, // 0x00000283
            IME_ENDCOMPOSITION = 270, // 0x0000010E
            IME_KEYDOWN = 656, // 0x00000290
            IME_KEYLAST = IME_ENDCOMPOSITION | CREATE, // 0x0000010F
            IME_KEYUP = IME_KEYDOWN | CREATE, // 0x00000291
            IME_NOTIFY = 642, // 0x00000282
            IME_REQUEST = 648, // 0x00000288
            IME_SELECT = IME_COMPOSITIONFULL | CREATE, // 0x00000285
            IME_SETCONTEXT = 641, // 0x00000281
            IME_STARTCOMPOSITION = 269, // 0x0000010D
            INITDIALOG = 272, // 0x00000110
            INITMENU = 278, // 0x00000116
            INITMENUPOPUP = INITMENU | CREATE, // 0x00000117
            INPUTLANGCHANGE = 81, // 0x00000051
            INPUTLANGCHANGEREQUEST = 80, // 0x00000050
            KEYDOWN = 256, // 0x00000100
            KEYFIRST = KEYDOWN, // 0x00000100
            KEYLAST = 264, // 0x00000108
            KEYUP = KEYFIRST | CREATE, // 0x00000101
            KILLFOCUS = 8,
            LBUTTONDBLCLK = 515, // 0x00000203
            LBUTTONDOWN = 513, // 0x00000201
            LBUTTONUP = 514, // 0x00000202
            LVM_GETEDITCONTROL = 4120, // 0x00001018
            LVM_SETIMAGELIST = 4099, // 0x00001003
            MBUTTONDBLCLK = LBUTTONDOWN | KILLFOCUS, // 0x00000209
            MBUTTONDOWN = 519, // 0x00000207
            MBUTTONUP = 520, // 0x00000208
            MDIACTIVATE = 546, // 0x00000222
            MDICASCADE = 551, // 0x00000227
            MDICREATE = 544, // 0x00000220
            MDIDESTROY = MDICREATE | CREATE, // 0x00000221
            MDIGETACTIVE = MDIDESTROY | KILLFOCUS, // 0x00000229
            MDIICONARRANGE = MDICREATE | KILLFOCUS, // 0x00000228
            MDIMAXIMIZE = 549, // 0x00000225
            MDINEXT = 548, // 0x00000224
            MDIREFRESHMENU = MDINEXT | CLOSE, // 0x00000234
            MDIRESTORE = MDIDESTROY | DESTROY, // 0x00000223
            MDISETMENU = MDICREATE | CLOSE, // 0x00000230
            MDITILE = MDINEXT | DESTROY, // 0x00000226
            MEASUREITEM = KILLFOCUS | GETMINMAXINFO, // 0x0000002C
            MENUCHAR = 288, // 0x00000120
            MENUCOMMAND = 294, // 0x00000126
            MENUDRAG = MENUCHAR | DESTROY | CREATE, // 0x00000123
            MENUGETOBJECT = 292, // 0x00000124
            MENURBUTTONUP = MENUCHAR | DESTROY, // 0x00000122
            MENUSELECT = KILLFOCUS | KEYUP | ERASEBKGND | DESTROY, // 0x0000011F
            MOUSEACTIVATE = 33, // 0x00000021
            MOUSEFIRST = 512, // 0x00000200
            MOUSEHOVER = 673, // 0x000002A1
            MOUSELAST = MOUSEFIRST | KILLFOCUS | DESTROY, // 0x0000020A
            MOUSELEAVE = MOUSEHOVER | DESTROY, // 0x000002A3
            MOUSEMOVE = MOUSEFIRST, // 0x00000200
            MOUSEWHEEL = MOUSEMOVE | KILLFOCUS | DESTROY, // 0x0000020A
            MOVE = DESTROY | CREATE, // 0x00000003
            MOVING = MOUSEMOVE | ERASEBKGND | DESTROY, // 0x00000216
            NCACTIVATE = 134, // 0x00000086
            NCCALCSIZE = 131, // 0x00000083
            NCCREATE = 129, // 0x00000081
            NCDESTROY = 130, // 0x00000082
            NCHITTEST = 132, // 0x00000084
            NCLBUTTONDBLCLK = NCDESTROY | MOUSEACTIVATE, // 0x000000A3
            NCLBUTTONDOWN = 161, // 0x000000A1
            NCLBUTTONUP = 162, // 0x000000A2
            NCMBUTTONDBLCLK = NCLBUTTONDOWN | KILLFOCUS, // 0x000000A9
            NCMBUTTONDOWN = 167, // 0x000000A7
            NCMBUTTONUP = 168, // 0x000000A8
            NCMOUSEHOVER = 672, // 0x000002A0
            NCMOUSELEAVE = NCMOUSEHOVER | DESTROY, // 0x000002A2
            NCMOUSEMOVE = 160, // 0x000000A0
            NCPAINT = NCHITTEST | CREATE, // 0x00000085
            NCRBUTTONDBLCLK = 166, // 0x000000A6
            NCRBUTTONDOWN = 164, // 0x000000A4
            NCRBUTTONUP = NCRBUTTONDOWN | CREATE, // 0x000000A5
            NEXTDLGCTL = 40, // 0x00000028
            NEXTMENU = MOVE | MOUSEMOVE | CLOSE, // 0x00000213
            NOTIFY = 78, // 0x0000004E
            NOTIFYFORMAT = 85, // 0x00000055
            NULL = 0,
            PAINT = 15, // 0x0000000F
            PAINTCLIPBOARD = MOUSEMOVE | KILLFOCUS | KEYUP, // 0x00000309
            PAINTICON = GETMINMAXINFO | DESTROY, // 0x00000026
            PALETTECHANGED = MOUSEMOVE | KEYUP | CLOSE, // 0x00000311
            PALETTEISCHANGING = MOUSEMOVE | KEYFIRST | CLOSE, // 0x00000310
            PARENTNOTIFY = MOUSEMOVE | CLOSE, // 0x00000210
            PASTE = MOUSEMOVE | KEYFIRST | DESTROY, // 0x00000302
            PENWINFIRST = 896, // 0x00000380
            PENWINLAST = PENWINFIRST | PAINT, // 0x0000038F
            POWER = 72, // 0x00000048
            PRINT = PASTE | ERASEBKGND | CREATE, // 0x00000317
            PRINTCLIENT = PARENTNOTIFY | KILLFOCUS | KEYFIRST, // 0x00000318
            QUERYDRAGICON = PAINTICON | CREATE | CLOSE, // 0x00000037
            QUERYENDSESSION = CREATE | CLOSE, // 0x00000011
            QUERYNEWPALETTE = 783, // 0x0000030F
            QUERYOPEN = QUERYENDSESSION | DESTROY, // 0x00000013
            QUEUESYNC = 35, // 0x00000023
            QUIT = DESTROY | CLOSE, // 0x00000012
            RBUTTONDBLCLK = 518, // 0x00000206
            RBUTTONDOWN = 516, // 0x00000204
            RBUTTONUP = RBUTTONDOWN | CREATE, // 0x00000205
            RENDERALLFORMATS = RBUTTONDOWN | KEYFIRST | DESTROY, // 0x00000306
            RENDERFORMAT = RBUTTONUP | KEYFIRST, // 0x00000305
            SETCURSOR = 32, // 0x00000020
            SETFOCUS = 7,
            SETFONT = SETCURSOR | CLOSE, // 0x00000030
            SETHOTKEY = SETFONT | DESTROY, // 0x00000032
            SETICON = 128, // 0x00000080
            SETMARGINS = 211, // 0x000000D3
            SETREDRAW = MOVE | KILLFOCUS, // 0x0000000B
            SETTEXT = 12, // 0x0000000C
            SETTINGCHANGE = QUIT | KILLFOCUS, // 0x0000001A
            SHOWWINDOW = KILLFOCUS | CLOSE, // 0x00000018
            SIZE = 5,
            SIZECLIPBOARD = SETREDRAW | MOUSEMOVE | KEYFIRST, // 0x0000030B
            SIZING = RBUTTONDOWN | CLOSE, // 0x00000214
            SPOOLERSTATUS = SETCURSOR | KILLFOCUS | DESTROY, // 0x0000002A
            STYLECHANGED = 125, // 0x0000007D
            STYLECHANGING = 124, // 0x0000007C
            SYNCPAINT = SETICON | KILLFOCUS, // 0x00000088
            SYSCHAR = 262, // 0x00000106
            SYSCOLORCHANGE = SIZE | CLOSE, // 0x00000015
            SYSCOMMAND = QUIT | KEYFIRST, // 0x00000112
            SYSDEADCHAR = SYSCHAR | CREATE, // 0x00000107
            SYSKEYDOWN = 260, // 0x00000104
            SYSKEYUP = SYSKEYDOWN | CREATE, // 0x00000105
            TCARD = 82, // 0x00000052
            TIMECHANGE = 30, // 0x0000001E
            TIMER = SYSCOMMAND | CREATE, // 0x00000113
            TVM_GETEDITCONTROL = 4367, // 0x0000110F
            TVM_SETIMAGELIST = 4361, // 0x00001109
            UNDO = SYSKEYDOWN | MOUSEMOVE, // 0x00000304
            UNINITMENUPOPUP = SYSKEYUP | SETCURSOR, // 0x00000125
            USER = 1024, // 0x00000400
            USERCHANGED = 84, // 0x00000054
            VKEYTOITEM = 46, // 0x0000002E
            VSCROLL = SYSKEYUP | CLOSE, // 0x00000115
            VSCROLLCLIPBOARD = PASTE | KILLFOCUS, // 0x0000030A
            WINDOWPOSCHANGED = 71, // 0x00000047
            WINDOWPOSCHANGING = 70, // 0x00000046
            WININICHANGE = SHOWWINDOW | DESTROY, // 0x0000001A
            SH_NOTIFY = USER | CREATE, // 0x00000401
        }

        [Flags]
        private enum MFT : uint {
            GRAYED = 3,
            DISABLED = GRAYED, // 0x00000003
            CHECKED = 8,
            SEPARATOR = 2048, // 0x00000800
            RADIOCHECK = 512, // 0x00000200
            BITMAP = 4,
            OWNERDRAW = 256, // 0x00000100
            MENUBARBREAK = 32, // 0x00000020
            MENUBREAK = 64, // 0x00000040
            RIGHTORDER = 8192, // 0x00002000
            BYCOMMAND = 0,
            BYPOSITION = 1024, // 0x00000400
            POPUP = 16, // 0x00000010
        }

        [Flags]
        private enum MFS : uint {
            GRAYED = 3,
            DISABLED = GRAYED, // 0x00000003
            CHECKED = 8,
            HILITE = 128, // 0x00000080
            ENABLED = 0,
            UNCHECKED = 0,
            UNHILITE = 0,
            DEFAULT = 4096, // 0x00001000
        }

        [Flags]
        private enum MIIM : uint {
            BITMAP = 128, // 0x00000080
            CHECKMARKS = 8,
            DATA = 32, // 0x00000020
            FTYPE = 256, // 0x00000100
            ID = 2,
            STATE = 1,
            STRING = 64, // 0x00000040
            SUBMENU = 4,
            TYPE = 16, // 0x00000010
        }

        [Flags]
        private enum TYMED {
            ENHMF = 64, // 0x00000040
            FILE = 2,
            GDI = 16, // 0x00000010
            HGLOBAL = 1,
            ISTORAGE = 8,
            ISTREAM = 4,
            MFPICT = 32, // 0x00000020
            NULL = 0,
        }
#pragma warning restore 0649

        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214E6-0000-0000-C000-000000000046")]
        [ComImport]
        private interface IShellFolder {
            [MethodImpl(MethodImplOptions.PreserveSig)]
            int ParseDisplayName(
              IntPtr hwnd,
              IntPtr pbc,
              [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName,
              ref uint pchEaten,
              out IntPtr ppidl,
              ref ShellContextMenu.SFGAO pdwAttributes);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int EnumObjects(IntPtr hwnd, ShellContextMenu.SHCONTF grfFlags, out IntPtr enumIDList);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int BindToObject(IntPtr pidl, IntPtr pbc, ref Guid riid, out IntPtr ppv);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int BindToStorage(IntPtr pidl, IntPtr pbc, ref Guid riid, out IntPtr ppv);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int CompareIDs(IntPtr lParam, IntPtr pidl1, IntPtr pidl2);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int CreateViewObject(IntPtr hwndOwner, Guid riid, out IntPtr ppv);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetAttributesOf(uint cidl, [MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, ref ShellContextMenu.SFGAO rgfInOut);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetUIObjectOf(
              IntPtr hwndOwner,
              uint cidl,
              [MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl,
              ref Guid riid,
              IntPtr rgfReserved,
              out IntPtr ppv);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetDisplayNameOf(IntPtr pidl, ShellContextMenu.SHGNO uFlags, IntPtr lpName);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int SetNameOf(
              IntPtr hwnd,
              IntPtr pidl,
              [MarshalAs(UnmanagedType.LPWStr)] string pszName,
              ShellContextMenu.SHGNO uFlags,
              out IntPtr ppidlOut);
        }

        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214e4-0000-0000-c000-000000000046")]
        [ComImport]
        private interface IContextMenu {
            [MethodImpl(MethodImplOptions.PreserveSig)]
            int QueryContextMenu(
              IntPtr hmenu,
              uint iMenu,
              uint idCmdFirst,
              uint idCmdLast,
              ShellContextMenu.CMF uFlags);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int InvokeCommand(ref ShellContextMenu.CMINVOKECOMMANDINFOEX info);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetCommandString(
              uint idcmd,
              ShellContextMenu.GCS uflags,
              uint reserved,
              [MarshalAs(UnmanagedType.LPArray)] byte[] commandstring,
              int cch);
        }

        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214f4-0000-0000-c000-000000000046")]
        [ComImport]
        private interface IContextMenu2 {
            [MethodImpl(MethodImplOptions.PreserveSig)]
            int QueryContextMenu(
              IntPtr hmenu,
              uint iMenu,
              uint idCmdFirst,
              uint idCmdLast,
              ShellContextMenu.CMF uFlags);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int InvokeCommand(ref ShellContextMenu.CMINVOKECOMMANDINFOEX info);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetCommandString(
              uint idcmd,
              ShellContextMenu.GCS uflags,
              uint reserved,
              [MarshalAs(UnmanagedType.LPWStr)] StringBuilder commandstring,
              int cch);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int HandleMenuMsg(uint uMsg, IntPtr wParam, IntPtr lParam);
        }

        [Guid("bcfce0a0-ec17-11d0-8d10-00a0c90f2719")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComImport]
        private interface IContextMenu3 {
            [MethodImpl(MethodImplOptions.PreserveSig)]
            int QueryContextMenu(
              IntPtr hmenu,
              uint iMenu,
              uint idCmdFirst,
              uint idCmdLast,
              ShellContextMenu.CMF uFlags);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int InvokeCommand(ref ShellContextMenu.CMINVOKECOMMANDINFOEX info);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int GetCommandString(
              uint idcmd,
              ShellContextMenu.GCS uflags,
              uint reserved,
              [MarshalAs(UnmanagedType.LPWStr)] StringBuilder commandstring,
              int cch);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int HandleMenuMsg(uint uMsg, IntPtr wParam, IntPtr lParam);

            [MethodImpl(MethodImplOptions.PreserveSig)]
            int HandleMenuMsg2(uint uMsg, IntPtr wParam, IntPtr lParam, IntPtr plResult);
        }
    }
}
