// Decompiled with JetBrains decompiler
// Type: FR.Logging.LoggingManager
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FR.Logging
{
  public class LoggingManager
  {
    private int _bufferedLines;
    private int _indention;
    private List<ILoggingDevice> _devices;
    private int _flushLines;
    private bool _isClosed;

    public LoggingManager(IConfigurationProperty loggingConfiguration)
    {
      this.FlushLines = loggingConfiguration.GetSubProperty(nameof (FlushLines), true).ToInt32(5);
      this._devices = new List<ILoggingDevice>();
      foreach (IConfigurationProperty subProperty in loggingConfiguration.GetSubProperties("Device"))
      {
        string typeName = subProperty.GetSubProperty("ClassName").ToString();
        Type type = Type.GetType(typeName);
        if (type == null)
          throw ExceptionHelper.GetArgument("Cannot load logging device type '{0}'", (object) typeName);
        ILoggingDevice loggingDevice = (ILoggingDevice) ((type.GetInterface(typeof (ILoggingDevice).FullName) != null ? type.GetConstructor(new Type[0]) : throw ExceptionHelper.GetNotImplemented("Logging device type '{0}' does not implement interface '{1}'", (object) type, (object) typeof (ILoggingDevice))) ?? throw ExceptionHelper.GetNotImplemented("Logging device type '{0}' does not implement an empty constructor", (object) type)).Invoke(new object[0]);
        loggingDevice.Open(subProperty);
        this.Devices.Add(loggingDevice);
      }
    }

    public List<ILoggingDevice> Devices => this._devices;

    public int FlushLines
    {
      get => this._flushLines;
      set => this._flushLines = value;
    }

    public bool IsClosed => this._isClosed;

    [DebuggerNonUserCode]
    public void Flush()
    {
      foreach (ILoggingDevice device in this.Devices)
      {
        if (!device.IsDisposed)
          device.Flush();
      }
    }

    public void Close()
    {
      foreach (ILoggingDevice device in this.Devices)
      {
        if (!device.IsDisposed)
          device.Dispose();
      }
      this.Devices.Clear();
      this._isClosed = true;
    }

    [DebuggerNonUserCode]
    public void Write(
      LogLevel logLevel,
      int stackDifference,
      string message,
      params object[] args)
    {
      this.WriteDevices(logLevel, stackDifference + 1, false, message, args);
    }

    [DebuggerNonUserCode]
    public void WriteEx(Exception ex, int stackDifference)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("Exception occurred");
      stringBuilder.Append(ExceptionHelper.GetCompleteMessage(ex));
      this.WriteDevices(LogLevel.Exception, stackDifference + 1, false, stringBuilder.ToString());
      this.Flush();
    }

    [DebuggerNonUserCode]
    public void WriteObject(LogLevel logLevel, int stackDifference, object obj)
    {
      if (!this.CheckWrite(logLevel))
        return;
      if (obj == null)
      {
        this.Write(logLevel, stackDifference + 1, "LogObject is null");
      }
      else
      {
        Type type = obj.GetType();
        this.Write(logLevel, stackDifference + 1, "------------------------------------");
        this.Write(logLevel, stackDifference + 1, "LogObject Type '{0}.{1}'", (object) type.Namespace, (object) type.Name);
        foreach (PropertyInfo property in obj.GetType().GetProperties())
        {
          try
          {
            object obj1 = property.GetValue(obj, (object[]) null);
            this.Write(logLevel, stackDifference + 1, "   {0}: {1}", (object) property.Name, obj1);
          }
          catch (TargetInvocationException ex)
          {
            Exception exception = (Exception) ex;
            if (exception.InnerException != null)
              exception = exception.InnerException;
            this.Write(logLevel, stackDifference + 1, "   {0}: <exception: {1}>", (object) property.Name, (object) exception.Message);
          }
          catch (Exception ex)
          {
            this.Write(logLevel, stackDifference + 1, "   {0}: <exception: {1}>", (object) property.Name, (object) ex.Message);
          }
        }
        this.Write(logLevel, stackDifference + 1, "------------------------------------");
      }
    }

    [DebuggerNonUserCode]
    public void ForceLog(
      LogLevel logLevel,
      int stackDifference,
      string message,
      params object[] args)
    {
      this.WriteDevices(logLevel, stackDifference + 1, true, message, args);
    }

    [DebuggerNonUserCode]
    public void ForceLog(Exception ex, int stackDifference)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("Exception occured");
      stringBuilder.Append(ExceptionHelper.GetCompleteMessage(ex));
      this.WriteDevices(LogLevel.Exception, stackDifference + 1, true, stringBuilder.ToString());
    }

    [DebuggerNonUserCode]
    public void CallEntry(LogLevel logLevel, int stackDifference)
    {
      ++stackDifference;
      StackFrame stackFrame = new StackFrame(stackDifference);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(" ->");
      stringBuilder.Append(stackFrame.GetMethod().DeclaringType.ToString());
      stringBuilder.Append("::");
      stringBuilder.Append(stackFrame.GetMethod().ToString());
      this.WriteDevices(logLevel, stackDifference, false, stringBuilder.ToString());
      this._indention += 3;
    }

    [DebuggerNonUserCode]
    public void CallLeave(LogLevel logLevel, int stackDifference)
    {
      if (this._indention >= 3)
        this._indention -= 3;
      else
        this._indention = 0;
      ++stackDifference;
      StackFrame stackFrame = new StackFrame(stackDifference);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(" <-");
      stringBuilder.Append(stackFrame.GetMethod().DeclaringType.ToString());
      stringBuilder.Append("::");
      stringBuilder.Append(stackFrame.GetMethod().ToString());
      this.WriteDevices(logLevel, stackDifference + 1, false, stringBuilder.ToString());
    }

    [DebuggerNonUserCode]
    private bool CheckWrite(LogLevel logLevel)
    {
      bool flag = false;
      if (this._devices != null)
      {
        foreach (ILoggingDevice device in this._devices)
        {
          if ((device.LogLevel & logLevel) != LogLevel.None)
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    private void WriteDevices(
      LogLevel logLevel,
      int stackDifference,
      bool force,
      string message,
      params object[] args)
    {
      if (!this.CheckWrite(logLevel))
        return;
      if (args != null)
      {
        for (int index = 0; index < args.Length; ++index)
        {
          if (args[index] == null)
            args[index] = (object) "<null>";
        }
      }
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(' ', this._indention);
      stringBuilder.AppendFormat(message, args);
      ++this._bufferedLines;
      StackFrame stackFrame = new StackFrame(stackDifference);
      ILoggingDevice loggingDevice = (ILoggingDevice) null;
      DateTime now = DateTime.Now;
      foreach (ILoggingDevice device in this.Devices)
      {
        if (device.IsDisposed)
        {
          loggingDevice = device;
        }
        else
        {
          string className = stackFrame.GetMethod().DeclaringType.ToString();
          string methodName = stackFrame.GetMethod().ToString();
          if (force)
            device.ForceLog(logLevel, stringBuilder.ToString(), now, className, methodName, stackDifference + 1);
          else if ((logLevel & device.LogLevel) != LogLevel.None)
            device.Log(logLevel, stringBuilder.ToString(), now, className, methodName, stackDifference + 1);
          if (this._bufferedLines >= this.FlushLines)
            device.Flush();
        }
      }
      if (this._bufferedLines >= this.FlushLines)
        this._bufferedLines = 0;
      if (loggingDevice == null)
        return;
      this.Devices.Remove(loggingDevice);
    }
  }
}
