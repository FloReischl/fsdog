// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoTrigger
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Diagnostics;
using System.Text;

namespace FR.Data.SqlClient.Smo
{
  public class SmoTrigger : SmoObject
  {
    internal SmoTrigger(SmoTable table, DataRow infoRow)
      : base((SmoObject) table, infoRow)
    {
    }

    public override int Id => (int) this._infoRow["object_id"];

    public bool IsEncrypted
    {
      [DebuggerNonUserCode] get => (bool) this._infoRow[nameof (IsEncrypted)];
    }

    public bool IsInsteadOf
    {
      [DebuggerNonUserCode] get => (int) this._infoRow["is_instead_of_trigger"] == 1;
    }

    public bool IsSystemTrigger
    {
      [DebuggerNonUserCode] get => (bool) this._infoRow[nameof (IsSystemTrigger)];
    }

    public override string Name
    {
      [DebuggerNonUserCode] get => (string) this._infoRow["name"];
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

    public override string GetCreateStatement(object scriptOptions)
    {
      SmoTriggerScriptOptions options = scriptOptions != null ? (SmoTriggerScriptOptions) scriptOptions : new SmoTriggerScriptOptions();
      if (this.IsEncrypted)
        throw SmoExceptionHelper.GetInvalidOperation("Trigger '{0}' is encrypted and cannot be scripted", (object) this.GetName((SmoScriptOptions) null));
      StringBuilder stringBuilder = new StringBuilder();
      string name = this.GetName((SmoScriptOptions) options);
      if (options.Drop)
      {
        if (options.CheckExists)
          stringBuilder.AppendFormat("IF (OBJECT_ID('{0}') IS NOT NULL)\r\n   ", (object) name);
        stringBuilder.AppendFormat("DROP TRIGGER {0}.{1}", (object) this.ParentObject.GetName((SmoScriptOptions) options), (object) name);
      }
      else
      {
        string str = this.IsSystemTrigger ? ((SmoDatabaseObject) this.ParentObject).Database.getSystemRoutineStatement((SmoObject) this) : ((SmoDatabaseObject) this.ParentObject).Database.getRoutineStatement((SmoObject) this);
        if (options.CheckExists)
        {
          stringBuilder.AppendFormat("IF (OBJECT_ID('{0}') IS NOT NULL)\r\n", (object) name);
          stringBuilder.AppendFormat("BEGIN\r\n");
          stringBuilder.AppendFormat("   EXECUTE sp_executesql N'{0}'", (object) str.Replace("'", "''"));
          stringBuilder.AppendFormat("\r\nEND");
        }
        else
          stringBuilder.Append(str);
      }
      return stringBuilder.ToString();
    }
  }
}
