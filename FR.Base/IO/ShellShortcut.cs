// Decompiled with JetBrains decompiler
// Type: FR.IO.ShellShortcut
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FR.IO {
    public class ShellShortcut : IDisposable {
        private const int INFOTIPSIZE = 1024;
        private const int MAX_PATH = 260;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_SHOWMINNOACTIVE = 7;
        private IShellLinkA _link;
        private readonly string _path;

        public ShellShortcut(string linkPath) {
            this._path = linkPath;
            this._link = (IShellLinkA)new FR.IO.ShellLink();
            if (!File.Exists(linkPath))
                return;
            ((IPersistFile)this._link).Load(linkPath, 0);
        }

        public string Arguments {
            get {
                StringBuilder pszArgs = new StringBuilder(1024);
                this._link.GetArguments(pszArgs, pszArgs.Capacity);
                return pszArgs.ToString();
            }
            set => this._link.SetArguments(value);
        }

        public string Description {
            get {
                StringBuilder pszName = new StringBuilder(1024);
                this._link.GetDescription(pszName, pszName.Capacity);
                return pszName.ToString();
            }
            set => this._link.SetDescription(value);
        }

        public string WorkingDirectory {
            get {
                StringBuilder pszDir = new StringBuilder(260);
                this._link.GetWorkingDirectory(pszDir, pszDir.Capacity);
                return pszDir.ToString();
            }
            set => this._link.SetWorkingDirectory(value);
        }

        public string Path {
            get {
                WIN32_FIND_DATAA pfd = new WIN32_FIND_DATAA();
                StringBuilder pszFile = new StringBuilder(260);
                this._link.GetPath(pszFile, pszFile.Capacity, out pfd, SLGP_FLAGS.SLGP_UNCPRIORITY);
                return pszFile.ToString();
            }
            set => this._link.SetPath(value);
        }

        public string IconPath {
            get {
                StringBuilder pszIconPath = new StringBuilder(260);
                this._link.GetIconLocation(pszIconPath, pszIconPath.Capacity, out int _);
                return pszIconPath.ToString();
            }
            set => this._link.SetIconLocation(value, this.IconIndex);
        }

        public int IconIndex {
            get {
                StringBuilder pszIconPath = new StringBuilder(260);
                this._link.GetIconLocation(pszIconPath, pszIconPath.Capacity, out int piIcon);
                return piIcon;
            }
            set => this._link.SetIconLocation(this.IconPath, value);
        }

        public Icon Icon {
            get {
                StringBuilder pszIconPath = new StringBuilder(260);
                this._link.GetIconLocation(pszIconPath, pszIconPath.Capacity, out int piIcon);
                IntPtr icon1 = ShellApi.ExtractIcon(Marshal.GetHINSTANCE(this.GetType().Module), pszIconPath.ToString(), piIcon);
                if (icon1 == IntPtr.Zero)
                    return (Icon)null;
                Icon icon2 = Icon.FromHandle(icon1);
                Icon icon3 = (Icon)icon2.Clone();
                icon2.Dispose();
                ShellApi.DestroyIcon(icon1);
                return icon3;
            }
        }

        public ProcessWindowStyle WindowStyle {
            get {
                this._link.GetShowCmd(out int piShowCmd);
                switch (piShowCmd) {
                    case 2:
                    case 7:
                        return ProcessWindowStyle.Minimized;
                    case 3:
                        return ProcessWindowStyle.Maximized;
                    default:
                        return ProcessWindowStyle.Normal;
                }
            }
            set {
                int iShowCmd;
                switch (value) {
                    case ProcessWindowStyle.Normal:
                        iShowCmd = 1;
                        break;
                    case ProcessWindowStyle.Minimized:
                        iShowCmd = 7;
                        break;
                    case ProcessWindowStyle.Maximized:
                        iShowCmd = 3;
                        break;
                    default:
                        throw new ArgumentException("Unsupported ProcessWindowStyle value.");
                }
                this._link.SetShowCmd(iShowCmd);
            }
        }

        public Keys Hotkey {
            get {
                this._link.GetHotkey(out short pwHotkey);
                return (Keys)(((int)pwHotkey & 65280) << 8 | (int)pwHotkey & (int)byte.MaxValue);
            }
            set {
                if ((value & Keys.Modifiers) == Keys.None)
                    throw new ArgumentException("Hotkey must include a modifier key.");
                this._link.SetHotkey((short)((Keys)((int)(value & Keys.Modifiers) >> 8) | value & Keys.KeyCode));
            }
        }

        public void Dispose() {
            if (this._link == null)
                return;
            Marshal.ReleaseComObject((object)this._link);
            this._link = (IShellLinkA)null;
        }

        public void Save() => ((IPersistFile)this._link).Save(this._path, true);

        public object ShellLink => (object)this._link;
    }
}
