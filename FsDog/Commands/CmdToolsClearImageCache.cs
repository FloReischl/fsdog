﻿// Decompiled with JetBrains decompiler
// Type: FsDog.CmdToolsClearImageCache
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;

namespace FsDog.Commands {
    public class CmdToolsClearImageCache : CmdFsDogIntern {
        public override void Execute() {
            this.Application.ClearImageCache();
            this.ExecutionState = CommandExecutionState.Ok;
        }
    }
}
