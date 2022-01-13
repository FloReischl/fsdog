// Decompiled with JetBrains decompiler
// Type: FR.Net.NetworkHelper
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace FR.Net
{
  public static class NetworkHelper
  {
    private const uint MAX_PREFERRED_LENGTH = 4294967295;

    [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern NetworkErrorCode NetShareEnum(
      string ServerName,
      int level,
      ref IntPtr lpBuffer,
      uint prefmaxlen,
      ref int entriesread,
      ref int totalentries,
      ref int resume_handle);

    [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern NetworkErrorCode NetShareGetInfo(
      string servername,
      string netname,
      int level,
      ref IntPtr lpBuffer);

    [DllImport("Netapi32.dll", CharSet = CharSet.Unicode)]
    private static extern NetworkErrorCode NetApiBufferFree(IntPtr lpBuffer);

    [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
    private static extern NetworkErrorCode WNetAddConnection3(
      IntPtr hwndOwner,
      ref NETRESOURCE lpNetResource,
      string lpPassword,
      string lpUserName,
      NETRESOURCE_CONNECT dwFlags);

    [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
    private static extern NetworkErrorCode WNetOpenEnum(
      NETRESOURCE_SCOPE dwScope,
      NETRESOURCE_TYPE dwType,
      NETRESOURCE_USAGE dwUsage,
      ref NETRESOURCE ptrNetRes,
      ref IntPtr hNet);

    [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
    private static extern NetworkErrorCode WNetCloseEnum(IntPtr hNet);

    [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
    private static extern NetworkErrorCode WNetEnumResource(
      IntPtr hNet,
      ref int count,
      IntPtr lpBuffer,
      ref int lpdwBufferSize);

    public static NETRESOURCE GetNetworkRoot() => new NETRESOURCE();

    public static NETRESOURCE ConnectNetworkResource(
      IntPtr owner,
      string remoteName,
      string userName,
      string password,
      NETRESOURCE_CONNECT flags)
    {
      NETRESOURCE lpNetResource = new NETRESOURCE();
      lpNetResource.lpRemoteName = remoteName;
      int num = (int) NetworkHelper.WNetAddConnection3(owner, ref lpNetResource, password, userName, flags);
      return lpNetResource;
    }

    public static NETRESOURCE[] GetNetworkChildren(NETRESOURCE netParent)
    {
      List<NETRESOURCE> netresourceList = new List<NETRESOURCE>();
      IntPtr zero = IntPtr.Zero;
      int num1 = (int) NetworkHelper.WNetOpenEnum(NETRESOURCE_SCOPE.RESOURCE_GLOBALNET, NETRESOURCE_TYPE.RESOURCETYPE_DISK, NETRESOURCE_USAGE.RESOURCEUSAGE_ALL, ref netParent, ref zero);
      int count = -1;
      int lpdwBufferSize = 1023 * Marshal.SizeOf(typeof (NETRESOURCE));
      IntPtr num2 = Marshal.AllocHGlobal(lpdwBufferSize);
      int num3 = (int) NetworkHelper.WNetEnumResource(zero, ref count, num2, ref lpdwBufferSize);
      for (int index = 0; index < count; ++index)
      {
        NETRESOURCE structure = (NETRESOURCE) Marshal.PtrToStructure(new IntPtr(num2.ToInt32() + index * Marshal.SizeOf(typeof (NETRESOURCE))), typeof (NETRESOURCE));
        netresourceList.Add(structure);
      }
      Marshal.FreeHGlobal(num2);
      int num4 = (int) NetworkHelper.WNetCloseEnum(zero);
      return netresourceList.ToArray();
    }

    public static string[] GetShareNames(string server)
    {
      int entriesread = 0;
      int totalentries = 0;
      int resume_handle = 0;
      int num1 = Marshal.SizeOf(typeof (SHARE_INFO_0));
      IntPtr zero = IntPtr.Zero;
      NetworkErrorCode errorCode = NetworkHelper.NetShareEnum(server, 0, ref zero, uint.MaxValue, ref entriesread, ref totalentries, ref resume_handle);
      List<string> stringList = new List<string>();
      if (errorCode != NetworkErrorCode.NERR_Success)
        throw new NetworkException("Error while trying to get the shares for the specified server.", (Exception) new Win32Exception(), errorCode);
      IntPtr ptr = zero;
      for (int index = 0; index < entriesread; ++index)
      {
        SHARE_INFO_0 structure = (SHARE_INFO_0) Marshal.PtrToStructure(ptr, typeof (SHARE_INFO_0));
        stringList.Add(structure.shi0_netname);
        ptr = new IntPtr(ptr.ToInt32() + num1);
      }
      int num2 = (int) NetworkHelper.NetApiBufferFree(zero);
      return stringList.ToArray();
    }

    public static void GetShareInformation(string server, string share, out SHARE_INFO_0 shi)
    {
      IntPtr zero = IntPtr.Zero;
      NetworkErrorCode info = NetworkHelper.NetShareGetInfo(server, share, 0, ref zero);
      if (info == NetworkErrorCode.NERR_Success)
      {
        shi = (SHARE_INFO_0) Marshal.PtrToStructure(zero, typeof (SHARE_INFO_0));
        int num = (int) NetworkHelper.NetApiBufferFree(zero);
      }
      else
      {
        shi = new SHARE_INFO_0();
        throw new NetworkException("Error while retriving share information.", (Exception) new Win32Exception(), info);
      }
    }

    public static void GetShareInformation(string server, string share, out SHARE_INFO_1 shi)
    {
      IntPtr zero = IntPtr.Zero;
      NetworkErrorCode info = NetworkHelper.NetShareGetInfo(server, share, 1, ref zero);
      if (info == NetworkErrorCode.NERR_Success)
      {
        shi = (SHARE_INFO_1) Marshal.PtrToStructure(zero, typeof (SHARE_INFO_1));
        int num = (int) NetworkHelper.NetApiBufferFree(zero);
      }
      else
      {
        shi = new SHARE_INFO_1();
        throw new NetworkException("Error while retriving share information.", (Exception) new Win32Exception(), info);
      }
    }

    public static void GetShareInformation(string server, string share, out SHARE_INFO_2 shi)
    {
      IntPtr zero = IntPtr.Zero;
      NetworkErrorCode info = NetworkHelper.NetShareGetInfo(server, share, 2, ref zero);
      if (info == NetworkErrorCode.NERR_Success)
      {
        shi = (SHARE_INFO_2) Marshal.PtrToStructure(zero, typeof (SHARE_INFO_2));
        int num = (int) NetworkHelper.NetApiBufferFree(zero);
      }
      else
      {
        shi = new SHARE_INFO_2();
        throw new NetworkException("Error while retriving share information.", (Exception) new Win32Exception(), info);
      }
    }
  }
}
