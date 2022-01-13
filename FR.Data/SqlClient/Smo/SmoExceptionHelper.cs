// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoExceptionHelper
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;

namespace FR.Data.SqlClient.Smo
{
  internal static class SmoExceptionHelper
  {
    public static ArgumentException GetArgument(
      string message,
      params object[] args)
    {
      return new ArgumentException(string.Format(message, args));
    }

    public static ArgumentNullException GetArgumentNull(string paramName) => new ArgumentNullException(paramName);

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
  }
}
