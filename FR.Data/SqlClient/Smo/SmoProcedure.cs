// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoProcedure
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Diagnostics;
using System.Text;

namespace FR.Data.SqlClient.Smo
{
  public class SmoProcedure : SmoDatabaseObject
  {
    internal SmoProcedure(SmoDatabase database, DataRow infoRow)
      : base(database, infoRow)
    {
    }

    public bool IsEncrypted
    {
      [DebuggerNonUserCode] get => (bool) this._infoRow[nameof (IsEncrypted)];
    }

    public bool IsSystemProcedure
    {
      [DebuggerNonUserCode] get => (bool) this._infoRow[nameof (IsSystemProcedure)];
    }

    public override string GetCreateStatement(object scriptOptions)
    {
      SmoProcedureScriptOptions options = scriptOptions != null ? (SmoProcedureScriptOptions) scriptOptions : new SmoProcedureScriptOptions();
      if (this.IsEncrypted)
        throw SmoExceptionHelper.GetInvalidOperation("View '{0}' is encrypted and cannot be scripted", (object) this.GetName((SmoScriptOptions) null));
      StringBuilder stringBuilder = new StringBuilder();
      string name = this.GetName((SmoScriptOptions) options);
      if (options.Drop)
      {
        if (options.CheckExists)
          stringBuilder.AppendFormat("IF (OBJECT_ID('{0}') IS NOT NULL)\r\n   ", (object) name);
        stringBuilder.AppendFormat("DROP VIEW {0}", (object) name);
      }
      else
      {
        string str = this.IsSystemProcedure ? this.Database.GetSystemRoutineStatement((SmoObject) this) : this.Database.GetRoutineStatement((SmoObject) this);
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
