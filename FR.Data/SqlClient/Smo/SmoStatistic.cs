// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoStatistic
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Diagnostics;

namespace FR.Data.SqlClient.Smo
{
  public class SmoStatistic : SmoObject
  {
    internal SmoStatistic(SmoTable table, DataRow infoRow)
      : base((SmoObject) table, infoRow)
    {
    }

    internal SmoStatistic(SmoView view, DataRow infoRow)
      : base((SmoObject) view, infoRow)
    {
    }

    public override int Id => (int) this._infoRow["stats_id"];

    public bool IsAutoCreated
    {
      [DebuggerNonUserCode] get => (bool) this._infoRow["auto_created"];
    }

    public bool IsIndexCreated => (bool) this._infoRow[nameof (IsIndexCreated)];

    public bool IsUserCreated
    {
      [DebuggerNonUserCode] get => (bool) this._infoRow["user_created"];
    }

    public override string Name
    {
      get => (string) this._infoRow["name"];
      set => base.Name = value;
    }

    public SmoTable Table
    {
      [DebuggerNonUserCode] get => this.ParentObject as SmoTable;
    }

    public SmoView View
    {
      [DebuggerNonUserCode] get => this.ParentObject as SmoView;
    }

    public override string GetCreateStatement(object scriptOptions) => throw SmoExceptionHelper.GetNotImplemented("Method GetCreateStatement is not implemented.");
  }
}
