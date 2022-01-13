// Decompiled with JetBrains decompiler
// Type: FR.Logging.LoggingDeviceFlatFile
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace FR.Logging
{
  public class LoggingDeviceFlatFile : ILoggingDevice, IDisposable
  {
    private StreamWriter _file;
    private string _fileName;
    private LogLevel _logLevel;
    private LogLevel _stackTrace;
    private bool _isDisposed;

    public StreamWriter File => this._file;

    public string FileName => this._fileName;

    public static string GetLinePrefix(LogLevel logLevel, DateTime dateTime) => string.Format("{0};{1}; ", (object) dateTime.ToString("yyyyMMdd HHmmssfff"), (object) LoggingDeviceFlatFile.GetLogLevelShortText(logLevel));

    public static string GetLogLevelShortText(LogLevel logLevel)
    {
      if ((logLevel & LogLevel.Exception) != LogLevel.None)
        return "EX";
      if ((logLevel & LogLevel.Error) != LogLevel.None)
        return "ER";
      if ((logLevel & LogLevel.Warning) != LogLevel.None)
        return "W";
      if ((logLevel & LogLevel.Info) != LogLevel.None)
        return "I";
      if ((logLevel & LogLevel.Debug) != LogLevel.None)
        return "DBG";
      if ((logLevel & LogLevel.Trace) != LogLevel.None)
        return "TRC";
      return (logLevel & LogLevel.User1) != LogLevel.None || (logLevel & LogLevel.User2) != LogLevel.None || (logLevel & LogLevel.User3) != LogLevel.None ? "USR" : "???";
    }

    public LogLevel LogLevel
    {
      [DebuggerNonUserCode] get => this._logLevel;
      [DebuggerNonUserCode] set => this._logLevel = value;
    }

    public LogLevel StackTraceLevel
    {
      [DebuggerNonUserCode] get => this._stackTrace;
      [DebuggerNonUserCode] set => this._stackTrace = value;
    }

    public bool IsDisposed
    {
      [DebuggerNonUserCode] get => this._isDisposed;
    }

    public void Open(IConfigurationProperty deviceConfiguration)
    {
      FileInfo fileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);
      string str1 = deviceConfiguration.GetSubProperty("Directory", true).ToString(fileInfo.DirectoryName);
      str1.TrimEnd('\\');
      string defaultValue = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
      string str2 = deviceConfiguration.GetSubProperty("FileName", true).ToString(defaultValue);
      this.LogLevel = (LogLevel) deviceConfiguration.GetSubProperty("LogLevel", true).ToUInt32(7U);
      this.StackTraceLevel = (LogLevel) deviceConfiguration.GetSubProperty("StackTrace", true).ToUInt32(1U);
      int num = 0;
      do
      {
        ++num;
        this._fileName = string.Format("{0}\\{1}_{2}.log", (object) str1, (object) str2, (object) num.ToString("000"));
      }
      while (System.IO.File.Exists(this.FileName));
      this._file = new StreamWriter(this.FileName);
      this._file.AutoFlush = false;
    }

    [DebuggerNonUserCode]
    public void Close()
    {
      this.File.Close();
      this._isDisposed = true;
    }

    public void Log(
      LogLevel logLevel,
      string message,
      DateTime dateTime,
      string className,
      string methodName,
      int skipFrames)
    {
      string str = (string) null;
      if ((logLevel & this.LogLevel) == logLevel)
      {
        str = LoggingDeviceFlatFile.GetLinePrefix(logLevel, dateTime);
        message = string.Format("{0}{1}\r\n", (object) str, (object) message);
        this.File.Write(message);
      }
      if ((logLevel & this.StackTraceLevel) != logLevel)
        return;
      if (str == null)
        str = LoggingDeviceFlatFile.GetLinePrefix(logLevel, dateTime);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(str);
      stringBuilder.AppendLine("Stack Trace");
      StackTrace stackTrace = new StackTrace(skipFrames, false);
      int num = 0;
      foreach (StackFrame frame in stackTrace.GetFrames())
      {
        stringBuilder.Append(str);
        stringBuilder.Append("Frame: ");
        stringBuilder.Append(num++);
        stringBuilder.Append(" | File: ");
        stringBuilder.Append(frame.GetFileName());
        stringBuilder.Append(" | Line: ");
        stringBuilder.Append(" :: ");
        stringBuilder.Append(frame.ToString());
      }
      this.File.Write((object) stringBuilder);
    }

    public void ForceLog(
      LogLevel logLevel,
      string message,
      DateTime dateTime,
      string className,
      string methodName,
      int skipFrames)
    {
      LogLevel logLevel1 = this.LogLevel;
      this.LogLevel = logLevel;
      this.Log(logLevel, message, dateTime, className, methodName, skipFrames + 1);
      this.LogLevel = logLevel1;
    }

    public void Flush() => this.File.Flush();

    public void Dispose() => this.Close();
  }
}
