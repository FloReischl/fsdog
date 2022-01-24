// Decompiled with JetBrains decompiler
// Type: FsDog.CmdEditFind
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FsDog.Search;

namespace FsDog.Commands.View {
    public class CmdViewFind : CmdFsDogIntern {
        public override void Execute() {
            var main = base.FormMain;
            var search = new FormSearch();
            search.Directory = main.CurrentDirectory;
            search.ShowDialog(main);
        }
    }
}
