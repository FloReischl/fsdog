// Decompiled with JetBrains decompiler
// Type: FR.Logging.ILoggingProvider
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;

namespace FR.Logging {
    public interface ILoggingProvider {
        LoggingManager Logger { get; set; }

        LogLevel LogLevel { get; set; }

        void SetLoggingProvider(ILoggingProvider loggingProvider);

        void Log(LogLevel logLevel, string message, params object[] args);

        void LogEx(Exception ex);

        void LogObject(LogLevel logLevel, object obj);

        void ForceLog(LogLevel logLevel, string message, params object[] args);

        void ForceLog(Exception ex);

        void CallEntry(LogLevel logLevel);

        void CallLeave(LogLevel logLevel);
    }
}
