// Decompiled with JetBrains decompiler
// Type: FR.Commands.ICommandHandler
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Collections;
using System;

namespace FR.Commands {
    public interface ICommandHandler {
        bool CanExecuteCommand(Type commandType);

        void ExecuteCommand(Type commandType, DataContext context);

        void ExecuteCommand(ICommand command, DataContext context);
    }
}
