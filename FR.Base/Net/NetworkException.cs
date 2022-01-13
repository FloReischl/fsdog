// Decompiled with JetBrains decompiler
// Type: FR.Net.NetworkException
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Runtime.Serialization;

namespace FR.Net
{
  [Serializable]
  public class NetworkException : Exception
  {
    public NetworkException()
    {
    }

    public NetworkException(string message)
      : base(message)
    {
    }

    public NetworkException(string message, Exception inner)
      : base(message, inner)
    {
    }

    public NetworkException(string message, Exception inner, NetworkErrorCode errorCode)
      : base(message, inner)
    {
      this.NetworkErrorCode = errorCode;
    }

    protected NetworkException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public NetworkErrorCode NetworkErrorCode { get; private set; }
  }
}
