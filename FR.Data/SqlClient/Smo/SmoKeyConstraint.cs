// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoKeyConstraint
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Text;

namespace FR.Data.SqlClient.Smo
{
  public class SmoKeyConstraint : SmoConstraint
  {
    private SmoIndex _index;

    public SmoIndex Index
    {
      get
      {
        if (this._index == null && this._infoRow != null)
          this._index = this.Table.Indexes.FindById((int) this._infoRow["unique_index_id"]);
        return this._index;
      }
      set
      {
        if (object.Equals((object) this._index, (object) value))
          return;
        this._index = value;
        this.SetState(SmoObjectState.Changed);
      }
    }

    internal SmoKeyConstraint(SmoTable table, DataRow infoRow)
      : base(table, infoRow)
    {
    }

    public SmoKeyConstraint(SmoTable table)
      : base(table, (DataRow) null)
    {
      this.SetState(SmoObjectState.New);
    }

    public override string GetCreateStatement(object scriptOptions)
    {
      SmoConstraintScriptOptions options = scriptOptions != null ? (SmoConstraintScriptOptions) scriptOptions : new SmoConstraintScriptOptions();
      if (options.TableScript)
      {
        options.CheckExists = false;
        options.Database = false;
        options.Schema = false;
      }
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = (StringBuilder) null;
      if (options.CheckExists)
      {
        if (options.Script2000)
        {
          stringBuilder2 = new StringBuilder();
          stringBuilder2.AppendFormat("IF NOT EXISTS (SELECT * FROM Sysobjects WHERE parent_obj = OBJECT_ID('{0}') AND name = '{1}')\r\n", (object) this.Table.GetName((SmoScriptOptions) options), (object) this.Name);
          stringBuilder2.AppendFormat("BEGIN\r\n   ");
        }
        else
        {
          stringBuilder2 = new StringBuilder();
          stringBuilder2.AppendFormat("IF NOT EXISTS (SELECT * FROM sys.key_constraints WHERE parent_object_id = OBJECT_ID('{0}') AND name = '{1}')\r\n", (object) this.Table.GetName((SmoScriptOptions) options), (object) this.Name);
          stringBuilder2.AppendFormat("BEGIN\r\n   ");
        }
      }
      if (!options.TableScript)
      {
        stringBuilder1.AppendFormat("ALTER TABLE {0} \r\n", (object) this.Table.GetName((SmoScriptOptions) options));
        stringBuilder1.AppendFormat("   ADD CONSTRAINT {0} ", (object) this.GetName((SmoScriptOptions) options));
      }
      else
        stringBuilder1.AppendFormat("CONSTRAINT {0}", (object) this.GetName((SmoScriptOptions) options));
      if (this.Type == SmoConstraintType.PrimaryKey)
        stringBuilder1.Append("PRIMARY KEY ");
      else if (this.Type == SmoConstraintType.Unique)
        stringBuilder1.Append("UNIQUE ");
      if (this.Index.Type == SmoIndexType.Clustered)
        stringBuilder1.Append("CLUSTERED ");
      else if (this.Index.Type == SmoIndexType.NonClustered)
        stringBuilder1.Append("NONCLUSTERED ");
      stringBuilder1.Append("\r\n   (");
      foreach (SmoIndexColumn column in (SmoCollection<SmoIndexColumn>) this.Index.Columns)
        stringBuilder1.AppendFormat("\r\n      {0}, ", (object) column.TableColumn.GetName((SmoScriptOptions) options));
      stringBuilder1.Remove(stringBuilder1.Length - 2, 2);
      stringBuilder1.Append("\r\n   )");
      SmoIndexScriptOptions scriptOptions1 = new SmoIndexScriptOptions();
      scriptOptions1.OptionsOnly = true;
      scriptOptions1.NoOptions = options.NoIndexOptions;
      if (options.Script2000)
        scriptOptions1.NoOptions = true;
      stringBuilder1.Append(this.Index.GetCreateStatement((object) scriptOptions1));
      if (stringBuilder2 != null)
      {
        stringBuilder1.Replace("\r\n", "\r\n   ");
        stringBuilder1.Insert(0, (object) stringBuilder2);
        stringBuilder1.Append("\r\nEND");
      }
      return stringBuilder1.ToString();
    }

    public string GetDropStatement(object scriptOptions)
    {
      SmoConstraintScriptOptions options = scriptOptions != null ? (SmoConstraintScriptOptions) scriptOptions : new SmoConstraintScriptOptions();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("ALTER TABLE {0} \r\n", (object) this.Table.GetName((SmoScriptOptions) options));
      stringBuilder.AppendFormat("   DROP CONSTRAINT {0} ", (object) this.GetName((SmoScriptOptions) options));
      return stringBuilder.ToString();
    }
  }
}
