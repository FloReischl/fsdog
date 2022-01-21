// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoView
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Diagnostics;
using System.Text;

namespace FR.Data.SqlClient.Smo
{
  public class SmoView : SmoDatabaseObject
  {
    private SmoViewColumnCollection _columns;
    private SmoIndexCollection _indexes;
    private SmoStatisticCollection _statistics;

    internal SmoView(SmoDatabase database, DataRow infoRow)
      : base(database, infoRow)
    {
    }

    public SmoViewColumnCollection Columns
    {
      [DebuggerNonUserCode] get
      {
        if (this._columns == null)
          this.GetColumns();
        return this._columns;
      }
    }

    public SmoIndexCollection Indexes
    {
      [DebuggerNonUserCode] get
      {
        if (this._indexes == null)
          this.GetIndexes();
        return this._indexes;
      }
    }

    public bool IsEncrypted
    {
      [DebuggerNonUserCode] get => (bool) this._infoRow[nameof (IsEncrypted)];
    }

    public bool IsSystemView
    {
      [DebuggerNonUserCode] get => (bool) this._infoRow[nameof (IsSystemView)];
    }

    public SmoStatisticCollection Statistics
    {
      [DebuggerNonUserCode] get
      {
        if (this._statistics == null)
          this.GetStatistics();
        return this._statistics;
      }
    }

    public override string GetCreateStatement(object scriptOptions)
    {
      SmoViewScriptOptions options = scriptOptions != null ? (SmoViewScriptOptions) scriptOptions : new SmoViewScriptOptions();
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
        string str = this.IsSystemView ? this.Database.GetSystemRoutineStatement((SmoObject) this) : this.Database.GetRoutineStatement((SmoObject) this);
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

    private void GetColumns()
    {
      this._columns = new SmoViewColumnCollection(this);
      DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT *, COLUMNPROPERTY(object_id, Name, 'Ordinal') Ordinal FROM sys.columns WHERE object_id = '{1}' ORDER BY column_id", (object) this.Database.Name, (object) this.Id);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._columns.Add(new SmoViewColumn(this, row));
      dataTable.Dispose();
    }

    private void GetIndexes()
    {
      this._indexes = new SmoIndexCollection(this);
      DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.indexes WHERE object_id = {1} AND Name IS NOT NULL", (object) this.Database.Name, (object) this.Id);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._indexes.Add(new SmoIndex(this, row));
      dataTable.Dispose();
    }

    private void GetStatistics()
    {
      this._statistics = new SmoStatisticCollection(this);
      DataTable dataTable = this.GetDataTable(CommandType.Text, Statements.ObjectStatistics, (object) this.Database.Name, (object) this.Id);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._statistics.Add(new SmoStatistic(this, row));
      dataTable.Dispose();
    }
  }
}
