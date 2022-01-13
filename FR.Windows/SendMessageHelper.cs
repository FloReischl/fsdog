// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.SendMessageHelper
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public static class SendMessageHelper
  {
    public const int WM_KEYDOWN = 256;
    public const int WM_KEYUP = 257;
    public const int WM_CHAR = 258;
    public const int WM_CUT = 768;
    public const int WM_COPY = 769;
    public const int WM_DEVICECHANGE = 537;
    public const int WM_PASTE = 770;
    public const int WM_CLEAR = 771;
    public const int WM_UNDO = 772;
    public const int WM_USER = 1024;

    [DllImport("user32")]
    private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

    [DllImport("user32")]
    private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

    public static void SendCopy(Control control)
    {
      if (control == null)
        control = FormBase.GetActiveChildControl();
      if (control == null)
        return;
      SendMessageHelper.SendMessage(control.Handle, 769, 0, IntPtr.Zero);
    }

    public static void SendCut(Control control)
    {
      if (control == null)
        control = FormBase.GetActiveChildControl();
      if (control == null)
        return;
      SendMessageHelper.SendMessage(control.Handle, 768, 0, IntPtr.Zero);
    }

    public static void SendMessage(Control control, int msg, IntPtr lparam, IntPtr wparam)
    {
      if (control == null)
        control = FormBase.GetActiveChildControl();
      SendMessageHelper.SendMessage(control.Handle, msg, lparam, wparam);
    }

    public static void SendKeyDown(Control control, Keys key)
    {
      if (control == null)
        control = FormBase.GetActiveChildControl();
      if (control == null)
        return;
      SendMessageHelper.SendMessage(control.Handle, 256, (int) key, new IntPtr(1));
    }

    public static void SendKeyPress(Control control, char key)
    {
      if (control == null)
        control = FormBase.GetActiveChildControl();
      if (control == null)
        return;
      SendMessageHelper.SendMessage(control.Handle, 258, (int) key, new IntPtr(1));
    }

    public static void SendPaste(Control control)
    {
      if (control == null)
        control = FormBase.GetActiveChildControl();
      if (control == null)
        return;
      SendMessageHelper.SendMessage(control.Handle, 770, 0, IntPtr.Zero);
    }

    public static void SendUndo(Control control)
    {
      if (control == null)
        control = FormBase.GetActiveChildControl();
      if (control == null)
        return;
      SendMessageHelper.SendMessage(control.Handle, 772, 0, IntPtr.Zero);
    }
  }
}
