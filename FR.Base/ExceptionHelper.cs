// Decompiled with JetBrains decompiler
// Type: FR.ExceptionHelper
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Text;

namespace FR
{
  public static class ExceptionHelper
  {
    public static string GetCompleteMessage(Exception ex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (; ex != null; ex = ex.InnerException)
      {
        if (stringBuilder.Length != 0)
          stringBuilder.Append("\r\n\r\n----- Inner Exception -----\r\n");
        stringBuilder.Append(ex.Message);
      }
      return stringBuilder.ToString();
    }

    public static ArgumentException GetArgument(
      string message,
      params object[] args)
    {
      return new ArgumentException(string.Format(message, args));
    }

    public static ArgumentNullException GetArgumentNull(string paramName) => new ArgumentNullException(paramName);

    public static IndexOutOfRangeException GetIndexOutOfRange(
      string paramName,
      string lessThanWhat)
    {
      return new IndexOutOfRangeException(string.Format("{0} has to be equal or greater than zero and less than {1}", (object) paramName, (object) lessThanWhat));
    }

    public static InvalidCastException GetInvalidCast(
      string message,
      params object[] args)
    {
      return new InvalidCastException(string.Format(message, args));
    }

    public static InvalidOperationException GetInvalidOperation(
      string message,
      params object[] args)
    {
      if (args != null && args.Length != 0)
        message = string.Format(message, args);
      return new InvalidOperationException(message);
    }

    public static NotImplementedException GetNotImplemented(
      string message,
      params object[] args)
    {
      if (args != null && args.Length != 0)
        message = string.Format(message, args);
      return new NotImplementedException(message);
    }

    public static NotSupportedException GetNotSupported(
      string message,
      params object[] args)
    {
      return new NotSupportedException(string.Format(message, args));
    }
  }
}
