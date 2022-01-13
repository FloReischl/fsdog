// Decompiled with JetBrains decompiler
// Type: FsDog.DetailViewRequestDirectoryArgs
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System;
using System.IO;

namespace FsDog {
    public class DetailViewRequestDirectoryArgs : EventArgs {
        public DetailViewRequestDirectoryArgs(DirectoryInfo dir) => this.Directory = dir;

        public DetailViewRequestDirectoryArgs(string treePath) => this.TreePath = treePath;

        public DirectoryInfo Directory { get; private set; }

        public string TreePath { get; private set; }
    }
}
