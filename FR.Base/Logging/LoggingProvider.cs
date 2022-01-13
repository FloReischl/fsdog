// Decompiled with JetBrains decompiler
// Type: FR.Logging.LoggingProvider
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Diagnostics;

namespace FR.Logging
{
  public class LoggingProvider : ILoggingProvider
  {
    private LoggingManager _logger;
    private LogLevel _logLevel;

    public virtual LoggingManager Logger
    {
      [DebuggerNonUserCode] get => this._logger;
      [DebuggerNonUserCode] set => this._logger = value;
    }

    public LogLevel LogLevel
    {
      [DebuggerNonUserCode] get => this._logLevel;
      [DebuggerNonUserCode] set => this._logLevel = value;
    }

    [DebuggerNonUserCode]
    public void SetLoggingProvider(ILoggingProvider loggingProvider)
    {
      if (loggingProvider != null)
      {
        this._logger = loggingProvider.Logger;
        if (this.LogLevel != LogLevel.None)
          return;
        this._logLevel = loggingProvider.LogLevel;
      }
      else
        this._logger = (LoggingManager) null;
    }

    [DebuggerNonUserCode]
    public void Log(LogLevel logLevel, string message, params object[] args)
    {
      if (this.Logger == null || (this.LogLevel & logLevel) == LogLevel.None)
        return;
      this.Logger.Write(logLevel, 1, message, args);
    }

    [DebuggerNonUserCode]
    public void LogEx(Exception ex)
    {
      if (this.Logger == null)
        return;
      this.Logger.WriteEx(ex, 1);
    }

    [DebuggerNonUserCode]
    public void LogObject(LogLevel logLevel, object obj)
    {
      if (this.Logger == null || (this.LogLevel & logLevel) == LogLevel.None)
        return;
      this.Logger.WriteObject(logLevel, 1, obj);
    }

    [DebuggerNonUserCode]
    public void ForceLog(LogLevel logLevel, string message, params object[] args)
    {
      if (this.Logger == null)
        return;
      this.Logger.ForceLog(logLevel, 1, message, args);
    }

    [DebuggerNonUserCode]
    public void ForceLog(Exception ex)
    {
      if (this.Logger == null)
        return;
      this.Logger.ForceLog(ex, 1);
    }

    [DebuggerNonUserCode]
    public void CallEntry(LogLevel logLevel)
    {
      if (this.Logger == null || (this.LogLevel & logLevel) == LogLevel.None)
        return;
      this.Logger.CallEntry(logLevel, 1);
    }

    [DebuggerNonUserCode]
    public void CallLeave(LogLevel logLevel)
    {
      if (this.Logger == null || (this.LogLevel & logLevel) == LogLevel.None)
        return;
      this.Logger.CallLeave(logLevel, 1);
    }
  }
}
