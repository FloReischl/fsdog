// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoIndex
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using FR.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace FR.Data.SqlClient.Smo {
    public class SmoIndex : SmoObject {
        private string _name;
        private readonly int _id;
        private SmoIndexType _type;
        private SmoIndexColumnCollection _columns;
        private bool _isUnique;
        private bool _isPrimaryKey;
        private bool _isUniqueConstraint;
        private bool _isIgnoreDubKey;
        private bool _isPadded;
        private byte _fillFactor;
        private bool _isDisapled;
        private bool _isAllowRowLocks;
        private bool _isAllowPageLocks;

        private ILogger Log { get; } = LoggingProvider.CreateLogger();

        public SmoTable Table => this.ParentObject as SmoTable;

        public SmoView View {
            [DebuggerNonUserCode]
            get => this.ParentObject as SmoView;
        }

        public override string Name {
            get => this._name;
            set {
                if (object.Equals((object)this._name, (object)value))
                    return;
                this.SetState(SmoObjectState.Changed);
                this._name = value;
            }
        }

        public override int Id => this._id;

        public SmoIndexType Type {
            get => this._type;
            set {
                if (value == this._type)
                    return;
                this.SetState(SmoObjectState.Changed);
                this._type = value;
            }
        }

        public SmoIndexColumnCollection Columns {
            get {
                if (this._columns == null)
                    this.GetIndexColumns();
                return this._columns;
            }
        }

        public bool IsUnique {
            get => this._isUnique;
            set {
                if (value == this._isUnique)
                    return;
                this.SetState(SmoObjectState.Changed);
                this._isUnique = value;
            }
        }

        public bool IsPrimaryKey {
            get => this._isPrimaryKey;
            set {
                if (value == this._isPrimaryKey)
                    return;
                this.SetState(SmoObjectState.Changed);
                this._isPrimaryKey = value;
            }
        }

        public bool IsUniqueConstraint {
            get => this._isUniqueConstraint;
            set {
                if (value == this._isUniqueConstraint)
                    return;
                this.SetState(SmoObjectState.Changed);
                this._isUniqueConstraint = value;
            }
        }

        public bool IsIgnoreDupKey {
            get => this._isIgnoreDubKey;
            set {
                if (value == this._isIgnoreDubKey)
                    return;
                this.SetState(SmoObjectState.Changed);
                this._isIgnoreDubKey = value;
            }
        }

        public bool IsPadded {
            get => this._isPadded;
            set {
                if (value == this._isPadded)
                    return;
                this.SetState(SmoObjectState.Changed);
                this._isPadded = value;
            }
        }

        public byte FillFactor {
            get => this._fillFactor;
            set {
                if ((int)value == (int)this._fillFactor)
                    return;
                this.SetState(SmoObjectState.Changed);
                this._fillFactor = value;
            }
        }

        public bool IsDisabled {
            get => this._isDisapled;
            set {
                if (value == this._isDisapled)
                    return;
                this.SetState(SmoObjectState.Changed);
                this._isDisapled = value;
            }
        }

        public bool IsAllowRowLocks {
            get => this._isAllowRowLocks;
            set {
                if (value == this._isAllowRowLocks)
                    return;
                this.SetState(SmoObjectState.Changed);
                this._isAllowRowLocks = value;
            }
        }

        public bool IsAllowPageLocks {
            get => this._isAllowPageLocks;
            set {
                if (value == this._isAllowPageLocks)
                    return;
                this.SetState(SmoObjectState.Changed);
                this._isAllowPageLocks = value;
            }
        }

        public bool CanDelete => !this.IsPrimaryKey && !this.IsUniqueConstraint;

        public bool CanCreate => this.CanDelete;

        internal SmoIndex(SmoTable table, DataRow infoRow)
          : base((SmoObject)table, infoRow) {
            this._name = (string)this._infoRow["name"];
            this._id = (int)this._infoRow["index_id"];
            this._isUnique = (bool)this._infoRow["is_unique"];
            this._isPrimaryKey = (bool)this._infoRow["is_primary_key"];
            this._isUniqueConstraint = (bool)this._infoRow["is_unique_constraint"];
            this._isIgnoreDubKey = (bool)this._infoRow["ignore_dup_key"];
            this._isPadded = (bool)this._infoRow["is_padded"];
            this._fillFactor = (byte)this._infoRow["fill_factor"];
            if (this._fillFactor == (byte)0)
                this._fillFactor = (byte)80;
            this._isDisapled = (bool)this._infoRow["is_disabled"];
            this._isAllowRowLocks = (bool)this._infoRow["allow_row_locks"];
            this._isAllowPageLocks = (bool)this._infoRow["allow_page_locks"];
            this._type = (SmoIndexType)(byte)this._infoRow["type"];
            this.SetState(SmoObjectState.Original);
        }

        internal SmoIndex(SmoView view, DataRow infoRow)
          : base((SmoObject)view, infoRow) {
            this._name = (string)this._infoRow["name"];
            this._id = (int)this._infoRow["index_id"];
            this._isUnique = (bool)this._infoRow["is_unique"];
            this._isPrimaryKey = (bool)this._infoRow["is_primary_key"];
            this._isUniqueConstraint = (bool)this._infoRow["is_unique_constraint"];
            this._isIgnoreDubKey = (bool)this._infoRow["ignore_dup_key"];
            this._isPadded = (bool)this._infoRow["is_padded"];
            this._fillFactor = (byte)this._infoRow["fill_factor"];
            if (this._fillFactor == (byte)0)
                this._fillFactor = (byte)80;
            this._isDisapled = (bool)this._infoRow["is_disabled"];
            this._isAllowRowLocks = (bool)this._infoRow["allow_row_locks"];
            this._isAllowPageLocks = (bool)this._infoRow["allow_page_locks"];
            this._type = (SmoIndexType)(byte)this._infoRow["type"];
            this.SetState(SmoObjectState.Original);
        }

        public SmoIndex(SmoTable table, string name)
          : base((SmoObject)table, (DataRow)null) {
            this._name = name;
            this._id = 0;
            this._isUnique = false;
            this._isPrimaryKey = false;
            this._isUniqueConstraint = false;
            this._fillFactor = (byte)80;
            this._type = SmoIndexType.NonClustered;
            this.SetState(SmoObjectState.New);
        }

        public override string GetCreateStatement(object scriptOptions) {
            SmoIndexScriptOptions options = scriptOptions != null ? (SmoIndexScriptOptions)scriptOptions : new SmoIndexScriptOptions();
            if (!options.OptionsOnly && !this.CanCreate) {
                Exception invalidOperation = (Exception)SmoExceptionHelper.GetInvalidOperation("Index cannot be created because it is a primary key or unique constraint");
                this.Log.Ex(invalidOperation);
                throw invalidOperation;
            }
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            string name = this.ParentObject.GetName((SmoScriptOptions)options);
            if (!options.OptionsOnly) {
                if (options.Drop) {
                    if (options.CheckExists)
                        stringBuilder1.AppendFormat("IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('{0}') AND name = '{1}')\r\n   ", (object)name, (object)this.Name);
                    stringBuilder1.AppendFormat("DROP INDEX {0} ON {1}\r\n\r\n", (object)this.Name, (object)name);
                }
                else if (options.CheckExists) {
                    stringBuilder2.AppendFormat("IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('{0}') AND name = '{1}')\r\n", (object)name, (object)this.Name);
                    stringBuilder2.Append("BEGIN\r\n   ");
                }
                stringBuilder1.Append("CREATE ");
                if (this.IsUnique)
                    stringBuilder1.Append("UNIQUE ");
                if (this.Type == SmoIndexType.Clustered)
                    stringBuilder1.Append("CLUSTERED ");
                else
                    stringBuilder1.Append("NONCLUSTERED ");
                stringBuilder1.AppendFormat("INDEX [{0}]\r\n", (object)this.Name);
                stringBuilder1.AppendFormat("   ON {0}\r\n", (object)name);
                stringBuilder1.Append("   (");
                List<SmoIndexColumn> smoIndexColumnList = new List<SmoIndexColumn>();
                bool flag1 = true;
                foreach (SmoIndexColumn column in (SmoCollection<SmoIndexColumn>)this.Columns) {
                    if (column.IsIncluded) {
                        smoIndexColumnList.Add(column);
                    }
                    else {
                        if (!flag1)
                            stringBuilder1.Append(",");
                        flag1 = false;
                        stringBuilder1.AppendFormat("\r\n      {0} ", (object)column.GetCreateStatement((object)null));
                    }
                }
                stringBuilder1.Append("\r\n   )");
                if (smoIndexColumnList.Count != 0) {
                    stringBuilder1.Append("\r\n   INCLUDE\r\n   (");
                    bool flag2 = true;
                    foreach (SmoIndexColumn smoIndexColumn in smoIndexColumnList) {
                        if (!flag2)
                            stringBuilder1.Append(",");
                        stringBuilder1.AppendFormat("\r\n      {0} ", (object)smoIndexColumn.GetCreateStatement((object)null));
                    }
                    stringBuilder1.Append("\r\n   )");
                }
            }
            if (!options.NoOptions) {
                if (options.PadIndex && !options.Script2000) {
                    if (this.IsPadded)
                        stringBuilder3.Append("      PAD_INDEX = ON,\r\n");
                    else
                        stringBuilder3.Append("      PAD_INDEX = OFF,\r\n");
                }
                if (options.FillFactor)
                    stringBuilder3.AppendFormat("      FILLFACTOR = {0},\r\n", (object)this.FillFactor);
                if (options.SortInTempDb && !options.Script2000)
                    stringBuilder3.Append("      SORT_IN_TEMPDB = ON,\r\n");
                if (options.IgnoreDupKey && !options.Script2000) {
                    if (this.IsIgnoreDupKey)
                        stringBuilder3.Append("      IGNORE_DUP_KEY = ON,\r\n");
                    else
                        stringBuilder3.Append("      IGNORE_DUP_KEY = OFF,\r\n");
                }
                if (options.StatisticsNoRecompute && !options.Script2000)
                    stringBuilder3.Append("      STATISTICS_NORECOMPUTE = ON,\r\n");
                if (options.DropExisting && !options.Script2000)
                    stringBuilder3.Append("      DROP_EXISTING = ON,\r\n");
                if (options.Online && !options.Script2000)
                    stringBuilder3.Append("      ONLINE = ON,\r\n");
                if (options.AllowRowLocks && !options.Script2000) {
                    if (this.IsAllowRowLocks)
                        stringBuilder3.Append("      ALLOW_ROW_LOCKS = ON,\r\n");
                    else
                        stringBuilder3.Append("      ALLOW_ROW_LOCKS = OFF,\r\n");
                }
                if (options.AllowPageLocks && !options.Script2000) {
                    if (this.IsAllowPageLocks)
                        stringBuilder3.Append("      ALLOW_PAGE_LOCKS = ON,\r\n");
                    else
                        stringBuilder3.Append("      ALLOW_PAGE_LOCKS = OFF,\r\n");
                }
                if (options.MaxDop != 0 && !options.Script2000)
                    stringBuilder3.AppendFormat("      MAXDOP = {0},\r\n", (object)options.MaxDop);
            }
            if (stringBuilder3.Length != 0) {
                stringBuilder3.Remove(stringBuilder3.Length - 3, 1);
                stringBuilder1.Append("\r\n   WITH\r\n");
                stringBuilder1.Append("   (\r\n");
                stringBuilder1.Append((object)stringBuilder3);
                stringBuilder1.Append("   )");
            }
            if (stringBuilder2.Length != 0) {
                stringBuilder1.Replace("\r\n", "\r\n   ");
                stringBuilder1.Insert(0, (object)stringBuilder2);
                stringBuilder1.Append("\r\nEND");
            }
            return stringBuilder1.ToString();
        }

        public override string ToString() => this.Name;

        private void GetIndexColumns() {
            this._columns = new SmoIndexColumnCollection(this);
            SmoDatabaseObject parentObject = (SmoDatabaseObject)this.ParentObject;
            DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.index_columns    WHERE object_id = OBJECT_ID('{1}')       AND index_id = {2}    ORDER BY index_column_id", (object)parentObject.Database.Name, (object)parentObject.FullName, (object)this.Id);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this._columns.Add(new SmoIndexColumn(this, row));
            dataTable.Dispose();
        }
    }
}
