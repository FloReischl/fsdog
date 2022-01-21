// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoForeignKeyConstraint
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Text;

namespace FR.Data.SqlClient.Smo
{
  public class SmoForeignKeyConstraint : SmoConstraint
  {
    private SmoCollection<SmoTableColumn> _columns;
    private SmoCollection<SmoTableColumn> _referencedColumns;

    public SmoCollection<SmoTableColumn> Columns
    {
      get
      {
        if (this._columns == null)
          this.getColumns();
        return this._columns;
      }
    }

    public SmoTable ReferencedTable => this.Table.Database.Tables.FindById((int) this._infoRow["referenced_object_id"]);

    public SmoCollection<SmoTableColumn> ReferencedColumns
    {
      get
      {
        if (this._referencedColumns == null)
          this.getColumns();
        return this._referencedColumns;
      }
    }

    public SmoForeginKeyAction OnUpdate => (SmoForeginKeyAction) (byte) this._infoRow["update_referential_action"];

    public SmoForeginKeyAction OnDelete => (SmoForeginKeyAction) (byte) this._infoRow["delete_referential_action"];

    public bool NotForReplication => (bool) this._infoRow["is_not_for_replication"];

    internal SmoForeignKeyConstraint(SmoTable table, DataRow infoRow)
      : base(table, infoRow)
    {
    }

    public override string GetCreateStatement(object scriptOptions)
    {
      SmoConstraintScriptOptions options = scriptOptions != null ? (SmoConstraintScriptOptions) scriptOptions : new SmoConstraintScriptOptions();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("ALTER TABLE {0} \r\n", (object) this.Table.GetName((SmoScriptOptions) options));
      stringBuilder.AppendFormat("   ADD CONSTRAINT {0} ", (object) this.GetName((SmoScriptOptions) options));
      stringBuilder.Append("FOREIGN KEY \r\n   (\r\n");
      foreach (SmoTableColumn column in this.Columns)
        stringBuilder.AppendFormat("      {0},\r\n", (object) column.GetName((SmoScriptOptions) options));
      stringBuilder.Remove(stringBuilder.Length - 3, 1);
      stringBuilder.Append("   )\r\n");
      stringBuilder.AppendFormat("   REFERENCES {0}\r\n", (object) this.ReferencedTable.GetName((SmoScriptOptions) options));
      stringBuilder.Append("   (\r\n");
      foreach (SmoTableColumn referencedColumn in this.ReferencedColumns)
        stringBuilder.AppendFormat("   {0},\r\n", (object) referencedColumn.GetName((SmoScriptOptions) options));
      stringBuilder.Remove(stringBuilder.Length - 3, 1);
      stringBuilder.Append("   )");
      if (options.OnDelete)
      {
        stringBuilder.Append("\r\n   ON DELETE ");
        switch (this.OnDelete)
        {
          case SmoForeginKeyAction.NoAction:
            stringBuilder.Append("NO ACTION");
            break;
          case SmoForeginKeyAction.Cascade:
            stringBuilder.Append("CASCADE");
            break;
          case SmoForeginKeyAction.SetNull:
            stringBuilder.Append("SET NULL");
            break;
          case SmoForeginKeyAction.SetDefault:
            stringBuilder.Append("SET DEFAULT");
            break;
        }
      }
      if (options.OnUpdate)
      {
        stringBuilder.Append("\r\n   ON UPDATE ");
        switch (this.OnDelete)
        {
          case SmoForeginKeyAction.NoAction:
            stringBuilder.Append("NO ACTION");
            break;
          case SmoForeginKeyAction.Cascade:
            stringBuilder.Append("CASCADE");
            break;
          case SmoForeginKeyAction.SetNull:
            stringBuilder.Append("SET NULL");
            break;
          case SmoForeginKeyAction.SetDefault:
            stringBuilder.Append("SET DEFAULT");
            break;
        }
      }
      if (options.NotForReplication && this.NotForReplication)
        stringBuilder.Append("\r\n   NOT FOR REPLICATION");
      return stringBuilder.ToString();
    }

    private void getColumns()
    {
      this._columns = new SmoCollection<SmoTableColumn>((SmoObject) this.Table);
      this._referencedColumns = new SmoCollection<SmoTableColumn>((SmoObject) this.ReferencedTable);
      foreach (DataRow row in (InternalDataCollectionBase) this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.foreign_key_columns WHERE constraint_object_id = {1}", (object) this.Table.Database.GetName((SmoScriptOptions) null), (object) this.Id).Rows)
      {
        this._columns.Add(this.Table.Columns.FindById((int) row["parent_column_id"]));
        this._referencedColumns.Add(this.ReferencedTable.Columns.FindById((int) row["referenced_column_id"]));
      }
      this._columns.SetReadOnly(true);
      this._referencedColumns.SetReadOnly(true);
    }
  }
}
