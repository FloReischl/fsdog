// Decompiled with JetBrains decompiler
// Type: FR.Threading.SchedularTask
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections.Generic;

namespace FR.Threading
{
  public class SchedularTask
  {
    private string _name;
    private bool _enabled;
    private Dictionary<string, object> _data;
    private object _tag;
    private SchedularTaskType _scheduleType;
    private TimeSpan _intervalSpan;
    private SchedularTaskIntervalType _intervalType;
    private DateTime _startDate;
    private DateTime _endDate;
    private DateTime _lastOccurred;
    private DateTime _time;
    private List<short> _daysOfMonth;
    private bool _monday;
    private bool _tuesday;
    private bool _wednesday;
    private bool _thursday;
    private bool _friday;
    private bool _saturday;
    private bool _sunday;
    private bool _january;
    private bool _febrary;
    private bool _march;
    private bool _april;
    private bool _may;
    private bool _june;
    private bool _july;
    private bool _august;
    private bool _september;
    private bool _october;
    private bool _november;
    private bool _december;

    public string Name
    {
      get => this._name;
      set => this._name = value;
    }

    public bool Enabled
    {
      get => this._enabled;
      set => this._enabled = value;
    }

    public Dictionary<string, object> Data => this._data;

    public object Tag
    {
      get => this._tag;
      set => this._tag = value;
    }

    public SchedularTaskType ScheduleType
    {
      get => this._scheduleType;
      set => this._scheduleType = value;
    }

    public TimeSpan IntervalSpan
    {
      get => this._intervalSpan;
      set => this._intervalSpan = value;
    }

    public SchedularTaskIntervalType IntervalType
    {
      get => this._intervalType;
      set => this._intervalType = value;
    }

    public DateTime StartDate
    {
      get => this._startDate;
      set => this._startDate = value;
    }

    public DateTime EndDate
    {
      get => this._endDate;
      set => this._endDate = value;
    }

    public DateTime LastOccurred => this._lastOccurred;

    public DateTime Time
    {
      get => this._time;
      set => this._time = value;
    }

    public List<short> DaysOfMonth => this._daysOfMonth;

    public bool Monday
    {
      get => this._monday;
      set => this._monday = value;
    }

    public bool Tuesday
    {
      get => this._tuesday;
      set => this._tuesday = value;
    }

    public bool Wednesday
    {
      get => this._wednesday;
      set => this._wednesday = value;
    }

    public bool Thursday
    {
      get => this._thursday;
      set => this._thursday = value;
    }

    public bool Friday
    {
      get => this._friday;
      set => this._friday = value;
    }

    public bool Saturday
    {
      get => this._saturday;
      set => this._saturday = value;
    }

    public bool Sunday
    {
      get => this._sunday;
      set => this._sunday = value;
    }

    public bool January
    {
      get => this._january;
      set => this._january = value;
    }

    public bool February
    {
      get => this._febrary;
      set => this._febrary = value;
    }

    public bool March
    {
      get => this._march;
      set => this._march = value;
    }

    public bool April
    {
      get => this._april;
      set => this._april = value;
    }

    public bool May
    {
      get => this._may;
      set => this._may = value;
    }

    public bool June
    {
      get => this._june;
      set => this._june = value;
    }

    public bool July
    {
      get => this._july;
      set => this._july = value;
    }

    public bool August
    {
      get => this._august;
      set => this._august = value;
    }

    public bool September
    {
      get => this._september;
      set => this._september = value;
    }

    public bool October
    {
      get => this._october;
      set => this._october = value;
    }

    public bool November
    {
      get => this._november;
      set => this._november = value;
    }

    public bool December
    {
      get => this._december;
      set => this._december = value;
    }

    public SchedularTask(string name, SchedularTaskType scheduleType)
    {
      this.Name = name;
      this.Enabled = true;
      this.ScheduleType = scheduleType;
      this.IntervalSpan = new TimeSpan(1, 0, 0);
      this.IntervalType = SchedularTaskIntervalType.EndToStart;
      this.StartDate = new DateTime(1900, 1, 1);
      this.EndDate = new DateTime(2100, 12, 31);
      this._lastOccurred = new DateTime(1900, 1, 1);
      this._time = new DateTime(1900, 1, 1, 0, 0, 0);
      this._daysOfMonth = new List<short>();
      this._data = new Dictionary<string, object>();
    }

    internal void setLastOccurred(DateTime dateTime) => this._lastOccurred = dateTime;
  }
}
