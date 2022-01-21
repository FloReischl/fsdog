// Decompiled with JetBrains decompiler
// Type: FsDog.LocalWindowsHook
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace FsDog {
    public class LocalWindowsHook {
        protected IntPtr m_hhook = IntPtr.Zero;
        protected LocalWindowsHook.HookProc m_filterFunc;
        protected HookType m_hookType;

        public event LocalWindowsHook.HookEventHandler HookInvoked;

        protected void OnHookInvoked(HookEventArgs e) {
            if (this.HookInvoked == null)
                return;
            this.HookInvoked((object)this, e);
        }

        public LocalWindowsHook(HookType hook) {
            this.m_hookType = hook;
            this.m_filterFunc = new LocalWindowsHook.HookProc(this.CoreHookProc);
        }

        public LocalWindowsHook(HookType hook, LocalWindowsHook.HookProc func) {
            this.m_hookType = hook;
            this.m_filterFunc = func;
        }

        protected int CoreHookProc(int code, IntPtr wParam, IntPtr lParam) {
            if (code < 0)
                return LocalWindowsHook.CallNextHookEx(this.m_hhook, code, wParam, lParam);
            this.OnHookInvoked(new HookEventArgs() {
                HookCode = code,
                wParam = wParam,
                lParam = lParam
            });
            return LocalWindowsHook.CallNextHookEx(this.m_hhook, code, wParam, lParam);
        }

        public void Install() => this.m_hhook = LocalWindowsHook.SetWindowsHookEx(this.m_hookType, this.m_filterFunc, IntPtr.Zero, Thread.CurrentThread.ManagedThreadId);

        public void Uninstall() => LocalWindowsHook.UnhookWindowsHookEx(this.m_hhook);

        [DllImport("user32.dll")]
        protected static extern IntPtr SetWindowsHookEx(
          HookType code,
          LocalWindowsHook.HookProc func,
          IntPtr hInstance,
          int threadID);

        [DllImport("user32.dll")]
        protected static extern int UnhookWindowsHookEx(IntPtr hhook);

        [DllImport("user32.dll")]
        protected static extern int CallNextHookEx(
          IntPtr hhook,
          int code,
          IntPtr wParam,
          IntPtr lParam);

        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        public delegate void HookEventHandler(object sender, HookEventArgs e);
    }
}
