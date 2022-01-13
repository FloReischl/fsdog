// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.IProgressDialog
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FR.Windows.Forms
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("EBBC7C04-315E-11d2-B62F-006097DF5BD4")]
  [ComImport]
  internal interface IProgressDialog
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void StartProgressDialog(
      IntPtr hwndParent,
      [MarshalAs(UnmanagedType.IUnknown)] object punkEnableModless,
      uint dwFlags,
      IntPtr pvResevered);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void StopProgressDialog();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pwzTitle);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void SetAnimation(IntPtr hInstAnimation, ushort idAnimation);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool HasUserCancelled();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void SetProgress(uint dwCompleted, uint dwTotal);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void SetProgress64(ulong ullCompleted, ulong ullTotal);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void SetLine(uint dwLineNum, [MarshalAs(UnmanagedType.LPWStr)] string pwzString, [MarshalAs(UnmanagedType.VariantBool)] bool fCompactPath, IntPtr pvResevered);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void SetCancelMsg([MarshalAs(UnmanagedType.LPWStr)] string pwzCancelMsg, object pvResevered);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    void Timer(uint dwTimerAction, object pvResevered);
  }
}
