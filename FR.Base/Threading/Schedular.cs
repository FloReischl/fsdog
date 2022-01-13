// Decompiled with JetBrains decompiler
// Type: FR.Threading.Schedular
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections.Generic;
using System.Timers;

namespace FR.Threading
{
  public class Schedular : IDisposable
  {
    private Timer _timer;
    private double _checkInterval;
    private List<SchedularTask> _schedules;

    public double CheckInterval
    {
      get => this._checkInterval;
      set => this._checkInterval = value;
    }

    public List<SchedularTask> Schedules => this._schedules;

    public event Schedular.ScheduleOccurresEventHandler ScheduleOccurres;

    public Schedular()
      : this(100.0)
    {
    }

    public Schedular(double checkInterval)
    {
      this.CheckInterval = 100.0;
      this._schedules = new List<SchedularTask>();
    }

    public void Start()
    {
      this.Stop();
      this._timer = new Timer();
      this._timer.Interval = this.CheckInterval;
      this._timer.Elapsed += new ElapsedEventHandler(this.timerTick);
      this._timer.Start();
    }

    public void Stop()
    {
      if (this._timer == null)
        return;
      this._timer.Stop();
      this._timer.Elapsed -= new ElapsedEventHandler(this.timerTick);
      this._timer.Dispose();
      this._timer = (Timer) null;
    }

    public void Dispose() => this.Stop();

    protected virtual void OnScheduleOccurres(SchedularTask schedule)
    {
      if (this.ScheduleOccurres == null)
        return;
      this.ScheduleOccurres((object) this, new ScheduleOccurresEventArgs(schedule));
    }

    private void timerTick(object sender, ElapsedEventArgs e)
    {
      DateTime now = DateTime.Now;
      foreach (SchedularTask schedule in this.Schedules)
      {
        if (schedule.Enabled && !(schedule.StartDate > now))
        {
          if (schedule.EndDate < now)
          {
            schedule.Enabled = false;
          }
          else
          {
            switch (schedule.ScheduleType)
            {
              case SchedularTaskType.Interval:
                this.checkInterval(schedule);
                continue;
              case SchedularTaskType.Daily:
                this.checkDaily(schedule);
                continue;
              case SchedularTaskType.Weekly:
                this.checkWeekly(schedule);
                continue;
              case SchedularTaskType.Monthly:
                this.checkMonthly(schedule);
                continue;
              default:
                throw new SchedularException("Unknown Schedule type '{0}'", new object[1]
                {
                  (object) schedule.ScheduleType
                })
                {
                  ErrorId = 1
                };
            }
          }
        }
      }
    }

    private void occureSchedule(SchedularTask schedule)
    {
      DateTime now1 = DateTime.Now;
      this.OnScheduleOccurres(schedule);
      if (schedule.LastOccurred.Year == 1900)
        schedule.setLastOccurred(DateTime.Now);
      if (schedule.ScheduleType == SchedularTaskType.Interval && schedule.IntervalType == SchedularTaskIntervalType.EndToStart)
      {
        schedule.setLastOccurred(DateTime.Now);
      }
      else
      {
        DateTime now2 = DateTime.Now;
        schedule.setLastOccurred(new DateTime(now2.Year, now2.Month, now2.Day, schedule.LastOccurred.Hour, schedule.LastOccurred.Minute, schedule.LastOccurred.Second, schedule.LastOccurred.Millisecond));
      }
    }

    private void checkInterval(SchedularTask schedule)
    {
      if (!(DateTime.Now - schedule.LastOccurred >= schedule.IntervalSpan))
        return;
      this.occureSchedule(schedule);
    }

    private void checkDaily(SchedularTask schedule)
    {
      DateTime now = DateTime.Now;
      if (!(schedule.LastOccurred.Date != now.Date) || !(now.TimeOfDay >= schedule.Time.TimeOfDay))
        return;
      this.occureSchedule(schedule);
    }

    private void checkWeekly(SchedularTask schedule)
    {
      DateTime now = DateTime.Now;
      bool flag = false;
      switch (now.DayOfWeek)
      {
        case DayOfWeek.Sunday:
          if (schedule.Sunday)
          {
            flag = true;
            break;
          }
          break;
        case DayOfWeek.Monday:
          if (schedule.Monday)
          {
            flag = true;
            break;
          }
          break;
        case DayOfWeek.Tuesday:
          if (schedule.Tuesday)
          {
            flag = true;
            break;
          }
          break;
        case DayOfWeek.Wednesday:
          if (schedule.Wednesday)
          {
            flag = true;
            break;
          }
          break;
        case DayOfWeek.Thursday:
          if (schedule.Friday)
          {
            flag = true;
            break;
          }
          break;
        case DayOfWeek.Friday:
          if (schedule.Friday)
          {
            flag = true;
            break;
          }
          break;
        case DayOfWeek.Saturday:
          if (schedule.Saturday)
          {
            flag = true;
            break;
          }
          break;
        default:
          throw new SchedularException("Unknown day of week '{0}'", new object[1]
          {
            (object) now.DayOfWeek
          })
          {
            ErrorId = 2
          };
      }
      if (!flag || !(schedule.LastOccurred.Date != now.Date) || !(now.TimeOfDay >= schedule.Time.TimeOfDay))
        return;
      this.occureSchedule(schedule);
    }

    private void checkMonthly(SchedularTask schedule)
    {
      DateTime now = DateTime.Now;
      bool flag = false;
      switch (now.Month)
      {
        case 1:
          if (schedule.January)
          {
            flag = true;
            break;
          }
          break;
        case 2:
          if (schedule.February)
          {
            flag = true;
            break;
          }
          break;
        case 3:
          if (schedule.March)
          {
            flag = true;
            break;
          }
          break;
        case 4:
          if (schedule.April)
          {
            flag = true;
            break;
          }
          break;
        case 5:
          if (schedule.May)
          {
            flag = true;
            break;
          }
          break;
        case 6:
          if (schedule.June)
          {
            flag = true;
            break;
          }
          break;
        case 7:
          if (schedule.July)
          {
            flag = true;
            break;
          }
          break;
        case 8:
          if (schedule.August)
          {
            flag = true;
            break;
          }
          break;
        case 9:
          if (schedule.September)
          {
            flag = true;
            break;
          }
          break;
        case 10:
          if (schedule.October)
          {
            flag = true;
            break;
          }
          break;
        case 11:
          if (schedule.November)
          {
            flag = true;
            break;
          }
          break;
        case 12:
          if (schedule.December)
          {
            flag = true;
            break;
          }
          break;
        default:
          throw new SchedularException("Unknown month of year '{0}'", new object[1]
          {
            (object) now.Month
          })
          {
            ErrorId = 3
          };
      }
      if (!flag || !schedule.DaysOfMonth.Contains((short) now.Day) || !(schedule.LastOccurred.Date != now.Date) || !(now.TimeOfDay >= schedule.Time.TimeOfDay))
        return;
      this.occureSchedule(schedule);
    }

    public delegate void ScheduleOccurresEventHandler(object sender, ScheduleOccurresEventArgs e);
  }
}
