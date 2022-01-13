// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.DataStoreViewObjectArgs
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Diagnostics;

namespace FR.ComponentModel
{
  public class DataStoreViewObjectArgs : EventArgs
  {
    private DataStoreViewObject _viewObject;

    internal DataStoreViewObjectArgs(DataStoreViewObject viewObject) => this._viewObject = viewObject;

    public DataStoreViewObject ViewObject
    {
      [DebuggerNonUserCode] get => this._viewObject;
    }
  }
}
