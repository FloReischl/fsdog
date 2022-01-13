// Decompiled with JetBrains decompiler
// Type: FR.Net.Win32Credentials
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace FR.Net
{
  public class Win32Credentials
  {
    private WindowsImpersonationContext _impersonatedUser;
    private IntPtr _tokenHandle = IntPtr.Zero;
    private IntPtr _dupeTokenHandle = IntPtr.Zero;

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool LogonUser(
      [MarshalAs(UnmanagedType.LPStr)] string lpszUsername,
      [MarshalAs(UnmanagedType.LPStr)] string lpszDomain,
      [MarshalAs(UnmanagedType.LPStr)] string lpszPassword,
      int dwLogonType,
      int dwLogonProvider,
      ref IntPtr phToken);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern bool CloseHandle(IntPtr handle);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool DuplicateToken(
      IntPtr ExistingTokenHandle,
      int SECURITY_IMPERSONATION_LEVEL,
      ref IntPtr DuplicateTokenHandle);

    ~Win32Credentials() => this.ResetCredentials();

    public bool ChangeWinCredential(string username, string domain, string password)
    {
      this._tokenHandle = new IntPtr(0);
      this._dupeTokenHandle = new IntPtr(0);
      this._tokenHandle = IntPtr.Zero;
      this._dupeTokenHandle = IntPtr.Zero;
      if (!Win32Credentials.LogonUser(username, domain, password, 2, 0, ref this._tokenHandle) || !Win32Credentials.DuplicateToken(this._tokenHandle, 2, ref this._dupeTokenHandle))
        return false;
      this._impersonatedUser = new WindowsIdentity(this._dupeTokenHandle).Impersonate();
      return true;
    }

    public void ResetCredentials()
    {
      if (this._impersonatedUser != null)
        this._impersonatedUser.Undo();
      this._impersonatedUser = (WindowsImpersonationContext) null;
      if (this._tokenHandle != IntPtr.Zero)
        Win32Credentials.CloseHandle(this._tokenHandle);
      if (!(this._dupeTokenHandle != IntPtr.Zero))
        return;
      Win32Credentials.CloseHandle(this._dupeTokenHandle);
    }
  }
}
