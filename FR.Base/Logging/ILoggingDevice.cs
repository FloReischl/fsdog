// Decompiled with JetBrains decompiler
// Type: FR.Logging.ILoggingDevice
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Configuration;
using System;

namespace FR.Logging
{
  public interface ILoggingDevice : IDisposable
  {
    bool IsDisposed { get; }

    LogLevel LogLevel { get; set; }

    LogLevel StackTraceLevel { get; set; }

    void Open(IConfigurationProperty deviceConfiguration);

    void Close();

    void Log(
      LogLevel logLevel,
      string message,
      DateTime dateTime,
      string className,
      string methodName,
      int skipFrames);

    void ForceLog(
      LogLevel logLevel,
      string message,
      DateTime dateTime,
      string className,
      string methodName,
      int skipFrames);

    void Flush();
  }
}
