// Decompiled with JetBrains decompiler
// Type: FR.Threading.AsynchronousWorker
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Diagnostics;
using System.Threading;

namespace FR.Threading
{
  public class AsynchronousWorker
  {
    private SynchronizationContext _entryContext;
    private AsynchronousWorkerSettings _settings;
    private bool _isFinished;
    private object _returnValue;

    private AsynchronousWorker(AsynchronousWorkerSettings settings) => this._settings = settings;

    public static AsynchronousWorker Create(AsynchronousWorkerSettings settings)
    {
      if (settings == null)
        throw new ArgumentException("Settings cannot be null", nameof (settings));
      if ((object) settings.Delegate == null && settings.MethodInfo == null)
        throw new ArgumentException("The settings has no valid delegate or MethodInformation", nameof (settings));
      return new AsynchronousWorker(settings);
    }

    public event EventHandler FinishedWork;

    public AsynchronousWorkerSettings Settings
    {
      [DebuggerNonUserCode] get => this._settings;
    }

    public bool IsFinished
    {
      [DebuggerNonUserCode] get => this._isFinished;
    }

    public object ReturnValue
    {
      [DebuggerNonUserCode] get => this._returnValue;
    }

    public void Start()
    {
      this._entryContext = SynchronizationContext.Current;
      new Thread(new ThreadStart(this.DoWork)).Start();
    }

    public void WaitForFinished()
    {
      while (!this.IsFinished)
        Thread.Sleep(50);
    }

    private void DoFinishedWork(object dummy)
    {
      lock (this)
      {
        this._isFinished = true;
        if (this.FinishedWork == null)
          return;
        this.FinishedWork((object) this, EventArgs.Empty);
      }
    }

    private void DoWork()
    {
      try
      {
        if ((object) this.Settings.Delegate != null)
          this._returnValue = this.Settings.Delegate.DynamicInvoke(this.Settings.Parameters);
        else
          this._returnValue = this.Settings.MethodInfo.Invoke(this.Settings.TargedObject, this.Settings.Parameters);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (this._entryContext != null)
          this._entryContext.Post(new SendOrPostCallback(this.DoFinishedWork), (object) null);
        else
          this.DoFinishedWork((object) null);
      }
    }
  }
}
