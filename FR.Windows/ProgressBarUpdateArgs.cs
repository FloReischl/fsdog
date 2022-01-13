// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.ProgressBarUpdateArgs
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;

namespace FR.Windows.Forms
{
  public class ProgressBarUpdateArgs : EventArgs
  {
    private ulong _ui8Value;
    private ulong _ui8Maximal;

    public ProgressBarUpdateArgs(ulong value, ulong maximum)
    {
      this.Value = value;
      this.Maximum = maximum;
    }

    public ulong Value
    {
      get => this._ui8Value;
      set => this._ui8Value = value;
    }

    public ulong Maximum
    {
      get => this._ui8Maximal;
      set => this._ui8Maximal = value;
    }
  }
}
