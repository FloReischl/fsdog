// Decompiled with JetBrains decompiler
// Type: FR.Commands.CommandBase
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Collections;
using FR.Logging;
using System;
using System.Diagnostics;

namespace FR.Commands {
    public abstract class CommandBase : ICommand {
        public CommandBase() => this.ExecutionState = CommandExecutionState.None;

        public abstract void Execute();

        public virtual ApplicationBase Application {
            get => ApplicationBase.Instance;
        }

        public ICommandReceiver Receiver { get; set; }

        public DataContext Context { get; set; } = new DataContext(true);

        protected ILogger Log { get; } = LoggingProvider.CreateLogger();

        public CommandExecutionState ExecutionState { get; set; }

        public CommandInstanceState InstanceState { get; set; }
    }
}
