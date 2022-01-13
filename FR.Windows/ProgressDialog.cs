// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.ProgressDialog
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FR.Windows.Forms {
    public class ProgressDialog : IDisposable {
        private const int FLAG_NORMAL = 0;
        private const int FLAG_MODAL = 1;
        private const int FLAG_AUTOTIME = 2;
        private const int FLAG_NO_TIME = 4;
        private const int FLAG_NO_MINIMIZE = 8;
        private const int FLAG_NO_PROGRESS_BAR = 16;
        private const uint PDTIMER_RESET = 1;
        private const int MAX_CAPACITY = 45;
        private Form _Parent;
        private IProgressDialog _dialog;
        private IntPtr _ptrInstance = IntPtr.Zero;
        private bool _bDialogClosed = true;
        private bool _disposed;
        private ulong _value;
        private string _caption;
        private ulong _maximal;
        private string _cancelMessage;
        private ProgressDialogStyle _style = ProgressDialogStyle.NoAnimation;
        private bool _normal;
        private bool _modal;
        private bool _autoTime;
        private bool _noTime;
        private bool _noMinimize;
        private bool _noProgressBar;

        public ProgressDialog(Form parent) {
            this._Parent = parent;
            this._caption = string.Empty;
            this._cancelMessage = string.Empty;
            this._normal = true;
            this._dialog = (IProgressDialog)new ProgressDialogCom();
        }

        ~ProgressDialog() {
            if (this._disposed)
                return;
            this.Dispose();
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern int PathCompactPathEx(
          [Out] StringBuilder pszOut,
          string szPath,
          int cchMax,
          int dwFlags);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(
          [MarshalAs(UnmanagedType.LPTStr)] string lpFileName,
          IntPtr hFile,
          ProgressDialog.LoadLibraryExFlags dwFlags);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public event EventHandler UserCancelled;

        public event OnProgressBarUpdate BeforeProgressUpdate;

        public ulong Value {
            get => this._value;
            set => this._value = value;
        }

        public string Caption {
            get => this._caption;
            set {
                if (this._caption == value)
                    return;
                this._caption = value;
                if (!this._caption.EndsWith("\0"))
                    this._caption += "\0";
                this._dialog.SetTitle(this._caption);
            }
        }

        public ulong Maximal {
            get => this._maximal;
            set => this._maximal = value;
        }

        public string CancelMessage {
            get => this._cancelMessage;
            set {
                if (this._cancelMessage == value)
                    return;
                this._cancelMessage = value;
                if (!this._cancelMessage.EndsWith("\0"))
                    this._cancelMessage += (string)(object)char.MinValue;
                this._dialog.SetCancelMsg(this._cancelMessage, (object)null);
            }
        }

        public ProgressDialogStyle Style {
            get => this._style;
            set {
                if (this._style == value)
                    return;
                this._style = value;
                if (this._style != ProgressDialogStyle.NoAnimation) {
                    if (this._ptrInstance == IntPtr.Zero)
                        this._ptrInstance = ProgressDialog.LoadLibraryEx("shell32.dll\0", IntPtr.Zero, ProgressDialog.LoadLibraryExFlags.DontResolveDllReferences | ProgressDialog.LoadLibraryExFlags.LoadLibraryAsDatafile);
                    if (this._ptrInstance == IntPtr.Zero)
                        throw new Exception("Failed to map shell32.dll resource");
                    this._dialog.SetAnimation(this._ptrInstance, (ushort)this._style);
                }
                else
                    this._dialog.SetAnimation(IntPtr.Zero, (ushort)0);
            }
        }

        public bool Normal {
            get => this._normal;
            set => this._normal = value;
        }

        public bool Modal {
            get => this._modal;
            set => this._modal = value;
        }

        public bool AutoTime {
            get => this._autoTime;
            set => this._autoTime = value;
        }

        public bool NoTime {
            get => this._noTime;
            set => this._noTime = value;
        }

        public bool NoMinimize {
            get => this._noMinimize;
            set => this._noMinimize = value;
        }

        public bool NoProgressBar {
            get => this._noProgressBar;
            set => this._noProgressBar = value;
        }

        public void SetLine(ProgressDialogLineType line, string text, bool compactPath) {
            if (compactPath) {
                StringBuilder pszOut = new StringBuilder(45);
                ProgressDialog.PathCompactPathEx(pszOut, text, 45, 0);
                text = pszOut.ToString();
            }
            if (!text.EndsWith("\0"))
                text += "\0";
            this._dialog.SetLine((uint)line, text, false, IntPtr.Zero);
        }

        public void Start() {
            this._dialog.StartProgressDialog(this._Parent.Handle, (object)null, this._getFlags(), IntPtr.Zero);
            this._bDialogClosed = false;
            this._dialog.Timer(1U, (object)null);
        }

        public void SetValue(ulong value) {
            this._value = value;
            if (this._bDialogClosed)
                return;
            if (this.BeforeProgressUpdate != null)
                this.BeforeProgressUpdate((object)this, new ProgressBarUpdateArgs(this.Value, this.Maximal));
            this._dialog.SetProgress64(this.Value, this.Maximal);
            if (!this._dialog.HasUserCancelled())
                return;
            if (this.UserCancelled != null)
                this.UserCancelled((object)this, EventArgs.Empty);
            this.Stop();
        }

        public void Stop() {
            if (this._bDialogClosed)
                return;
            this._dialog.StopProgressDialog();
            this._Parent.Activate();
            this._bDialogClosed = true;
        }

        public void Dispose() {
            if (this._disposed)
                return;
            if (!this._bDialogClosed)
                this.Stop();
            this._dialog = (IProgressDialog)null;
            this._disposed = true;
        }

        private uint _getFlags() {
            uint flags = 0;
            if (this.AutoTime)
                flags |= 2U;
            if (this.Modal)
                flags |= 1U;
            if (this.NoMinimize)
                flags |= 8U;
            if (this.NoProgressBar)
                flags |= 16U;
            if (this.Normal)
                flags = flags;
            if (this.NoTime)
                flags |= 4U;
            return flags;
        }

        [Flags]
        private enum LoadLibraryExFlags : uint {
            DontResolveDllReferences = 1,
            LoadLibraryAsDatafile = 2,
            LoadWithAlteredSearchPath = 8,
            LoadIgnoreCodeAuthzLevel = 16, // 0x00000010
        }
    }
}
