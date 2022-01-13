// Decompiled with JetBrains decompiler
// Type: FR.Commands.CommandBase
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Collections;
using FR.Logging;
using System.Diagnostics;

namespace FR.Commands
{
  public abstract class CommandBase : LoggingProvider, ICommand
  {
    private ICommandReceiver _receiver;
    private DataContext _context;
    private CommandExecutionState _executionState;
    private CommandInstanceState _instanceState;

    public CommandBase() => this._executionState = CommandExecutionState.None;

    public abstract void Execute();

    public virtual ApplicationBase ApplicationInstance
    {
      [DebuggerNonUserCode] get => ApplicationBase.Instance;
    }

    public ICommandReceiver Receiver
    {
      [DebuggerNonUserCode] get => this._receiver;
      [DebuggerNonUserCode] set => this._receiver = value;
    }

    public DataContext Context
    {
      [DebuggerNonUserCode] get
      {
        if (this._context == null)
          this._context = new DataContext(true);
        return this._context;
      }
    }

    public override LoggingManager Logger
    {
      [DebuggerNonUserCode] get => base.Logger == null && ApplicationBase.Instance != null ? ApplicationBase.Instance.Logger : base.Logger;
      [DebuggerNonUserCode] set => base.Logger = value;
    }

    public CommandExecutionState ExecutionState
    {
      [DebuggerNonUserCode] get => this._executionState;
      [DebuggerNonUserCode] set => this._executionState = value;
    }

    public CommandInstanceState IntanceState
    {
      [DebuggerNonUserCode] get => this._instanceState;
      [DebuggerNonUserCode] set => this._instanceState = value;
    }

    public virtual void SetContext(DataContext context) => this._context = context;

    DataContext ICommand.Context
    {
      [DebuggerNonUserCode] get => this.Context;
    }

    ICommandReceiver ICommand.Receiver
    {
      [DebuggerNonUserCode] get => this.Receiver;
      [DebuggerNonUserCode] set => this.Receiver = value;
    }

    CommandExecutionState ICommand.ExecutionState
    {
      [DebuggerNonUserCode] get => this.ExecutionState;
    }

    CommandInstanceState ICommand.InstanceState
    {
      [DebuggerNonUserCode] get => this.IntanceState;
      [DebuggerNonUserCode] set => this.IntanceState = value;
    }

    [DebuggerNonUserCode]
    void ICommand.Execute() => this.Execute();

    void ICommand.SetContext(DataContext context) => this.SetContext(context);
  }
}
