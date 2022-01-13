// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoTableColumn
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace FR.Data.SqlClient.Smo
{
  public class SmoTableColumn : SmoObject
  {
    private string _name;
    private SmoDataType _dataType;
    private int _maxSize;
    private string _collationName;
    private bool _isNullable;
    private int _precision;

    public SmoTable Table => (SmoTable) this.ParentObject;

    public override string Name
    {
      get
      {
        if (this._name == null && this._infoRow != null)
          this._name = (string) this._infoRow["name"];
        return this._name;
      }
      set
      {
        if (!(this._name != value))
          return;
        this._name = value;
        this.setState(SmoObjectState.Changed);
      }
    }

    public override int Id => (int) this._infoRow["column_id"];

    public int Ordinal => (int) this._infoRow[nameof (Ordinal)];

    public SmoDataType DataType
    {
      get => this._dataType;
      set
      {
        if (this._dataType == value)
          return;
        this._dataType = value;
        this.setState(SmoObjectState.Changed);
      }
    }

    public SqlDbType SqlDbType
    {
      get => this.DataType.SqlDbType;
      set => this.DataType = this.Table.Database.DataTypes.FindBySqlDbType(value);
    }

    public int MaximumSize
    {
      get => this._maxSize;
      set
      {
        if (this._maxSize == value)
          return;
        this._maxSize = value;
        this.setState(SmoObjectState.Changed);
      }
    }

    public object DefaultValue => (object) null;

    public string CollationName
    {
      get => this._collationName;
      set
      {
        if (!(this._collationName != value))
          return;
        this._collationName = value;
        this.setState(SmoObjectState.Changed);
      }
    }

    public bool IsNullable
    {
      get => this._isNullable;
      set
      {
        if (value == this._isNullable)
          return;
        this._isNullable = value;
        this.setState(SmoObjectState.Changed);
      }
    }

    public int Precision
    {
      get => this._precision;
      set
      {
        if (value == this._precision)
          return;
        this._precision = value;
        this.setState(SmoObjectState.Changed);
      }
    }

    internal SmoTableColumn(SmoTable table, DataRow infoRow)
      : base((SmoObject) table, infoRow)
    {
      this._collationName = this._infoRow["collation_name"] is DBNull ? "" : (string) this._infoRow["collation_name"];
      this._dataType = this.Table.Database.DataTypes.FindById((int) this._infoRow["user_type_id"]);
      this._maxSize = (int) (short) this._infoRow["max_length"];
      this._isNullable = (bool) this._infoRow["is_nullable"];
      this._precision = (int) (byte) this._infoRow["precision"];
      this.setState(SmoObjectState.Original);
    }

    public SmoTableColumn(SmoTable table)
      : base((SmoObject) table, (DataRow) null)
    {
      this._collationName = "";
      this._dataType = this.Table.Database.DataTypes.FindByName("varchar");
      this._maxSize = 50;
      this._isNullable = true;
      this._precision = 0;
      this.setState(SmoObjectState.New);
    }

    public override void Delete()
    {
      if (this.State == SmoObjectState.New)
        this.Table.Columns.remove(this);
      base.Delete();
    }

    public override string GetName(SmoScriptOptions options)
    {
      string name = this.Name;
      return !new Regex("^([a-zA-Z]{0,256})$").IsMatch(name) ? string.Format("[{0}]", (object) name.Replace("[", "[[").Replace("]", "]]")) : base.GetName(options);
    }

    public override string GetCreateStatement(object scriptOptions)
    {
      SmoTableColumnScriptOptions options1 = scriptOptions != null ? (!(scriptOptions is SmoTableScriptOptions) ? (SmoTableColumnScriptOptions) scriptOptions : new SmoTableColumnScriptOptions((SmoTableScriptOptions) scriptOptions)) : new SmoTableColumnScriptOptions();
      StringBuilder stringBuilder = new StringBuilder();
      SmoTableScriptOptions options2 = new SmoTableScriptOptions();
      options2.Database = options1.Database;
      options2.Schema = options1.Schema;
      string name = this.Table.GetName((SmoScriptOptions) options2);
      if (!options1.TableScriptPart)
      {
        if (this.State == SmoObjectState.Changed && this._infoRow != null && (string) this._infoRow["name"] != this.Name)
          stringBuilder.AppendFormat("EXECUTE sp_rename '{0}.{1}', '{2}', 'COLUMN'\r\n", (object) name, (object) (string) this._infoRow["name"], (object) this.Name);
        string str = !options1.Script2000 ? string.Format("SELECT * FROM Sys.Columns WHERE object_id = OBJECT_ID('{0}') AND Name = '{1}'", (object) name, (object) this.Name) : string.Format("SELECT * FROM SysColumns WHERE ID = OBJECT_ID('{0}') AND Name = '{1}'", (object) name, (object) this.Name);
        if (BaseHelper.InList((object) this.State, (object) SmoObjectState.New, (object) SmoObjectState.Original))
        {
          if (options1.CheckExists)
            stringBuilder.AppendFormat("IF NOT EXISTS ({0})\r\n   ", (object) str);
          stringBuilder.AppendFormat("ALTER TABLE {0} ADD ", (object) name);
        }
        else if (this.State == SmoObjectState.Changed)
        {
          if (options1.CheckExists)
            stringBuilder.AppendFormat("IF EXISTS ({0})\r\n   ", (object) str);
          stringBuilder.AppendFormat("ALTER TABLE {0} ALTER COLUMN ", (object) name);
        }
        else if (this.State == SmoObjectState.Deleted)
        {
          if (options1.CheckExists)
            stringBuilder.AppendFormat("IF EXISTS ({0})\r\n   ", (object) str);
          stringBuilder.AppendFormat("ALTER TABLE {0} DROP COLUMN ", (object) name);
        }
      }
      stringBuilder.AppendFormat("{0} ", (object) this.GetName((SmoScriptOptions) options1));
      if (this.State == SmoObjectState.Deleted)
        return stringBuilder.ToString();
      stringBuilder.AppendFormat("{0} ", (object) this.DataType.Name.ToUpper());
      if (BaseHelper.InList((object) this.DataType.SqlDbType, (object) SqlDbType.VarBinary, (object) SqlDbType.VarChar) && this.MaximumSize == int.MaxValue)
        stringBuilder.Append("(MAX) ");
      else if (BaseHelper.InList((object) this.DataType.SqlDbType, (object) SqlDbType.NVarChar) && this.MaximumSize == 1073741823)
        stringBuilder.Append("(MAX) ");
      else if (BaseHelper.InList((object) this.DataType.SqlDbType, (object) SqlDbType.Binary, (object) SqlDbType.Char, (object) SqlDbType.NChar, (object) SqlDbType.NVarChar, (object) SqlDbType.VarBinary, (object) SqlDbType.VarChar))
        stringBuilder.AppendFormat("({0}) ", (object) this.MaximumSize);
      if (options1.Collation && this.CollationName != null && this.CollationName != "")
        stringBuilder.AppendFormat("COLLATE {0} ", (object) this.CollationName);
      if (this.IsNullable)
        stringBuilder.Append("NULL ");
      else
        stringBuilder.Append("NOT NULL ");
      if (options1.Defaults && this.DefaultValue != null)
        stringBuilder.AppendFormat("DEFAULT {0} ", this.DefaultValue);
      return stringBuilder.ToString();
    }

    internal override void setState(SmoObjectState state)
    {
      base.setState(state);
      if (!BaseHelper.InList((object) state, (object) SmoObjectState.Changed, (object) SmoObjectState.Deleted, (object) SmoObjectState.New))
        return;
      this.Table.setState(SmoObjectState.Changed);
    }
  }
}
