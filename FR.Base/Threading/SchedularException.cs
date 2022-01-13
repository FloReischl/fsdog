// Decompiled with JetBrains decompiler
// Type: FR.Threading.SchedularException
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Runtime.Serialization;

namespace FR.Threading
{
  [Serializable]
  public class SchedularException : Exception
  {
    public const int UnknownScheduleType = 1;
    public const int UnknownDayOfWeek = 2;
    public const int UnknownMonthOfYear = 3;
    private SchedularTask _schedule;
    private int _errorId;

    public SchedularException()
    {
    }

    public SchedularException(string message, params object[] args)
      : base(string.Format(message, args))
    {
    }

    public SchedularException(Exception innerException, string message, params object[] args)
      : base(string.Format(message, args), innerException)
    {
    }

    public SchedularException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public SchedularTask Schedule
    {
      get => this._schedule;
      set => this._schedule = value;
    }

    public int ErrorId
    {
      get => this._errorId;
      set => this._errorId = value;
    }
  }
}
