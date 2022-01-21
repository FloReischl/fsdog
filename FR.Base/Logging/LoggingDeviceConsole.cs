// Decompiled with JetBrains decompiler
// Type: FR.Logging.LoggingDeviceConsole
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FR.Logging {
    public class LoggingDeviceConsole : ILoggingDevice, IDisposable {
        public LogLevel LogLevel { get; set; }

        public LogLevel StackTraceLevel { get; set; }

        public bool IsDisposed { get; private set; }

        public void Close() => this.IsDisposed = true;

        public async void Log(
          LogLevel logLevel,
          string message,
          DateTime dateTime,
          string className,
          string methodName,
          int skipFrames) {
            TextWriter stream = (logLevel & (LogLevel.Error | LogLevel.Exception)) != 0 ? Console.Error : Console.Out;
            string str = null;
            if ((logLevel & this.LogLevel) == logLevel) {
                str = GetLinePrefix(logLevel, dateTime);
                await stream.WriteLineAsync($"{str}{message}");
            }
            if ((logLevel & this.StackTraceLevel) != logLevel)
                return;
            if (str == null)
                str = LoggingDeviceConsole.GetLinePrefix(logLevel, dateTime);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(str);
            stringBuilder.AppendLine("Stack Trace");
            StackTrace stackTrace = new StackTrace(skipFrames, false);
            int num = 0;
            foreach (StackFrame frame in stackTrace.GetFrames()) {
                stringBuilder.Append(str);
                stringBuilder.Append("Frame: ");
                stringBuilder.Append(num++);
                stringBuilder.Append(" | File: ");
                stringBuilder.Append(frame.GetFileName());
                stringBuilder.Append(" | Line: ");
                stringBuilder.Append(" :: ");
                stringBuilder.Append(frame.ToString());
            }
            await stream.WriteLineAsync(stringBuilder.ToString());
        }

        public void ForceLog(
          LogLevel logLevel,
          string message,
          DateTime dateTime,
          string className,
          string methodName,
          int skipFrames) {
            LogLevel logLevel1 = this.LogLevel;
            this.LogLevel = logLevel;
            this.Log(logLevel, message, dateTime, className, methodName, skipFrames + 1);
            this.LogLevel = logLevel1;
        }

        public void Flush() => Console.Out.Flush();

        private static string GetLinePrefix(LogLevel logLevel, DateTime dateTime) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(dateTime.ToString("yyyyMMdd HHmmssfff"));
            stringBuilder.Append(";");
            if ((logLevel & LogLevel.Exception) != LogLevel.None)
                stringBuilder.Append("EX");
            else if ((logLevel & LogLevel.Error) != LogLevel.None)
                stringBuilder.Append("ER");
            else if ((logLevel & LogLevel.Warning) != LogLevel.None)
                stringBuilder.Append("W");
            else if ((logLevel & LogLevel.Info) != LogLevel.None)
                stringBuilder.Append("I");
            else if ((logLevel & LogLevel.Debug) != LogLevel.None)
                stringBuilder.Append("DBG");
            else if ((logLevel & LogLevel.Trace) != LogLevel.None)
                stringBuilder.Append("TRC");
            else if ((logLevel & LogLevel.User1) != LogLevel.None)
                stringBuilder.Append("USR");
            else if ((logLevel & LogLevel.User2) != LogLevel.None)
                stringBuilder.Append("USR");
            else if ((logLevel & LogLevel.User3) != LogLevel.None)
                stringBuilder.Append("USR");
            else
                stringBuilder.Append("???");
            stringBuilder.Append("; ");
            return stringBuilder.ToString();
        }

        public void Dispose() => this.Close();
    }
}
