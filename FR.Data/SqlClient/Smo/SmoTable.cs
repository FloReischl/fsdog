// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoTable
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using FR.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace FR.Data.SqlClient.Smo {
    public class SmoTable : SmoDatabaseObject {
        private SmoTableColumnCollection _columns;
        private SmoConstraintCollection _constraints;
        private SmoIndexCollection _indexes;
        private SmoStatisticCollection _statistics;
        private SmoTriggerCollection _triggers;

        internal SmoTable(SmoDatabase database, DataRow infoRow)
          : base(database, infoRow) {
        }

        public SmoTable(SmoDatabase database, string name)
          : base(database) {
            this.Name = name;
        }

        private ILogger Log { get; } = LoggingProvider.CreateLogger();

        public SmoTableColumnCollection Columns {
            [DebuggerNonUserCode]
            get {
                if (this._columns == null)
                    this.GetColumns();
                return this._columns;
            }
        }

        public SmoConstraintCollection Constraints {
            [DebuggerNonUserCode]
            get {
                if (this._constraints == null)
                    this.GetConstraints();
                return this._constraints;
            }
        }

        public SmoIndexCollection Indexes {
            [DebuggerNonUserCode]
            get {
                if (this._indexes == null)
                    this.GetIndexes();
                return this._indexes;
            }
        }

        public SmoKeyConstraint PrimaryKey {
            [DebuggerNonUserCode]
            get {
                foreach (SmoConstraint constraint in (SmoCollection<SmoConstraint>)this.Constraints) {
                    if (constraint.Type == SmoConstraintType.PrimaryKey)
                        return (SmoKeyConstraint)constraint;
                }
                return (SmoKeyConstraint)null;
            }
        }

        public SmoStatisticCollection Statistics {
            [DebuggerNonUserCode]
            get {
                if (this._statistics == null)
                    this.GetStatistics();
                return this._statistics;
            }
        }

        public SmoTriggerCollection Triggers {
            [DebuggerNonUserCode]
            get {
                if (this._triggers == null)
                    this.GetTriggers();
                return this._triggers;
            }
        }

        public override void Delete() {
            if (this.State == SmoObjectState.New)
                this.Database.Tables.Remove(this);
            base.Delete();
        }

        public override void Refresh() {
            if (this._indexes != null) {
                this._indexes.Clear();
                this._indexes = (SmoIndexCollection)null;
            }
            if (this._constraints != null) {
                this._constraints.Clear();
                this._constraints = (SmoConstraintCollection)null;
            }
            if (this._columns != null) {
                this._columns.Clear();
                this._columns = (SmoTableColumnCollection)null;
            }
            if (this._infoRow == null)
                return;
            DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.tables WHERE object_id = {1}", (object)this.Database.Name, (object)this.Id);
            if (dataTable.Rows.Count == 1) {
                this._infoRow = dataTable.Rows[0];
                dataTable.Dispose();
            }
            else {
                dataTable.Dispose();
                throw (Exception)SmoExceptionHelper.GetInvalidOperation("Table '{0}' does not exist in database", (object)this.Name);
            }
        }

        public string GetChangeStatement(object scriptOptions) {
            SmoTableScriptOptions tableScriptOptions = scriptOptions != null ? (SmoTableScriptOptions)scriptOptions : new SmoTableScriptOptions();
            StringBuilder stringBuilder = new StringBuilder();
            string empty = string.Empty;
            if (tableScriptOptions.Database)
                empty += string.Format("[{0}].", (object)this.Database.Name);
            if (tableScriptOptions.Schema)
                empty += string.Format("[{0}].", (object)this.Schema.Name);
            else if (empty != string.Empty)
                empty += ".";
            string str = empty + string.Format("[{0}]", (object)this.Name);
            foreach (SmoTableColumn column in (SmoCollection<SmoTableColumn>)this.Columns) {
                if (column.State != SmoObjectState.Original)
                    stringBuilder.AppendFormat("{0}\r\n", (object)column.GetCreateStatement((object)new SmoTableColumnScriptOptions() {
                        Defaults = tableScriptOptions.Defaults,
                        Collation = tableScriptOptions.Collation
                    }));
            }
            return stringBuilder.ToString();
        }

        public override string GetCreateStatement(object scriptOptions) {
            SmoTableScriptOptions scriptOptions1 = scriptOptions != null ? (SmoTableScriptOptions)scriptOptions : new SmoTableScriptOptions();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            string empty = string.Empty;
            if (scriptOptions1.Database)
                empty += string.Format("[{0}].", (object)this.Database.Name);
            if (scriptOptions1.Schema)
                empty += string.Format("[{0}].", (object)this.Schema.Name);
            else if (empty != string.Empty)
                empty += ".";
            string str = empty + string.Format("[{0}]", (object)this.Name);
            if (scriptOptions1.Drop) {
                if (scriptOptions1.CheckExists)
                    stringBuilder1.AppendFormat("IF (OBJECT_ID('{0}') IS NOT NULL)\r\n   ", (object)str);
                stringBuilder1.AppendFormat("DROP TABLE {0}\r\n\r\n", (object)str);
            }
            else if (scriptOptions1.CheckExists) {
                stringBuilder2.AppendFormat("IF (OBJECT_ID('{0}') IS NULL)\r\n", (object)str);
                stringBuilder2.Append("BEGIN\r\n   ");
            }
            stringBuilder1.AppendFormat("CREATE TABLE {0} (\r\n", (object)str);
            bool flag = true;
            foreach (SmoTableColumn column in (SmoCollection<SmoTableColumn>)this.Columns) {
                if (!flag)
                    stringBuilder1.Append(",\r\n");
                flag = false;
                stringBuilder1.AppendFormat("   {0} ", (object)column.GetCreateStatement((object)scriptOptions1));
            }
            if (scriptOptions1.PrimaryKey && this.PrimaryKey != null) {
                stringBuilder1.Append(",\r\n   ");
                stringBuilder1.Append(this.PrimaryKey.GetCreateStatement((object)new SmoConstraintScriptOptions() {
                    TableScript = true,
                    Script2000 = scriptOptions1.Script2000
                }));
                stringBuilder1.Append("\r\n   )");
            }
            else
                stringBuilder1.Append("\r\n   )");
            if (stringBuilder2.Length != 0) {
                stringBuilder1.Replace("\r\n", "\r\n   ");
                stringBuilder1.Insert(0, (object)stringBuilder2);
                stringBuilder1.Append("\r\nEND");
            }
            return stringBuilder1.ToString();
        }

        public DataTable GetDataTable() {
            DataTable dataTable = new DataTable(this.Name);
            foreach (SmoTableColumn column in (SmoCollection<SmoTableColumn>)this.Columns)
                dataTable.Columns.Add(new DataColumn(column.Name, column.DataType.Type));
            return dataTable;
        }

        public SqlCommand GetUpdateCommand(SmoTableScriptOptions options) {
            if (this.PrimaryKey == null) {
                Exception invalidOperation = (Exception)SmoExceptionHelper.GetInvalidOperation("Cannot create UPDATE command for table {0} because it has no primary key", (object)this.Name);
                this.Log.Ex(invalidOperation);
                throw invalidOperation;
            }
            if (options == null)
                options = new SmoTableScriptOptions();
            SqlCommand updateCommand = new SqlCommand {
                CommandType = CommandType.Text
            };
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("UPDATE {0} SET \r\n", (object)this.GetName((SmoScriptOptions)options));
            foreach (SmoTableColumn column1 in (SmoCollection<SmoTableColumn>)this.Columns) {
                bool flag = false;
                foreach (SmoObject column2 in (SmoCollection<SmoIndexColumn>)this.PrimaryKey.Index.Columns) {
                    if (column2.Name == column1.Name) {
                        flag = true;
                        break;
                    }
                }
                if (!flag || options.UpdateKeyColumn) {
                    string parameterName = string.Format("@{0}", (object)column1.Name.Replace(" ", ""));
                    updateCommand.Parameters.Add(parameterName, column1.DataType.SqlDbType, column1.MaximumSize, column1.Name);
                    stringBuilder.AppendFormat("      {0} = {1},\r\n", (object)column1.GetName((SmoScriptOptions)options), (object)parameterName);
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 3, 1);
            stringBuilder.Append("   WHERE\r\n");
            foreach (SmoIndexColumn column in (SmoCollection<SmoIndexColumn>)this.PrimaryKey.Index.Columns) {
                SmoTableColumn tableColumn = column.TableColumn;
                string parameterName = string.Format("@Old{0}", (object)tableColumn.Name.Replace(" ", ""));
                updateCommand.Parameters.Add(parameterName, tableColumn.DataType.SqlDbType, tableColumn.MaximumSize, tableColumn.Name);
                stringBuilder.AppendFormat("      {0} = {1},\r\n", (object)tableColumn.GetName((SmoScriptOptions)options), (object)parameterName);
            }
            stringBuilder.Remove(stringBuilder.Length - 3, 3);
            updateCommand.CommandText = stringBuilder.ToString();
            return updateCommand;
        }

        public SqlCommand GetInsertCommand(SmoTableScriptOptions options) {
            if (options == null)
                options = new SmoTableScriptOptions();
            SqlCommand insertCommand = new SqlCommand {
                CommandType = CommandType.Text
            };
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            stringBuilder1.AppendFormat("INSERT INTO {0} ", (object)this.GetName((SmoScriptOptions)options));
            foreach (SmoTableColumn column in (SmoCollection<SmoTableColumn>)this.Columns) {
                stringBuilder2.AppendFormat("{0}, ", (object)column.GetName((SmoScriptOptions)options));
                string parameterName = string.Format("@{0}", (object)column.Name.Replace(" ", ""));
                stringBuilder3.AppendFormat("{0}, ", (object)parameterName);
                insertCommand.Parameters.Add(parameterName, column.DataType.SqlDbType, column.MaximumSize, column.Name);
            }
            stringBuilder2.Remove(stringBuilder2.Length - 2, 2);
            stringBuilder3.Remove(stringBuilder3.Length - 2, 2);
            if (options.ColumnsForInsert)
                stringBuilder1.AppendFormat("({0})\r\n   ", (object)stringBuilder2);
            stringBuilder1.AppendFormat("VALUES ({0})", (object)stringBuilder3);
            insertCommand.CommandText = stringBuilder1.ToString();
            return insertCommand;
        }

        public SqlCommand GetDeleteCommand(SmoTableScriptOptions options) {
            if (this.PrimaryKey == null) {
                Exception invalidOperation = (Exception)SmoExceptionHelper.GetInvalidOperation("Cannot create DELETE command for table {0} because it has no primary key", (object)this.Name);
                this.Log.Ex(invalidOperation);
                throw invalidOperation;
            }
            if (options == null)
                options = new SmoTableScriptOptions();
            SqlCommand deleteCommand = new SqlCommand {
                CommandType = CommandType.Text
            };
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("DELETE FROM {0} \r\n", (object)this.GetName((SmoScriptOptions)options));
            stringBuilder.Append("   WHERE ");
            bool flag = true;
            foreach (SmoIndexColumn column in (SmoCollection<SmoIndexColumn>)this.PrimaryKey.Index.Columns) {
                SmoTableColumn tableColumn = column.TableColumn;
                if (!flag)
                    stringBuilder.Append("\r\n      AND ");
                flag = false;
                string parameterName = string.Format("@{0}", (object)tableColumn.Name.Replace(" ", ""));
                deleteCommand.Parameters.Add(parameterName, tableColumn.DataType.SqlDbType, tableColumn.MaximumSize, tableColumn.Name);
                stringBuilder.AppendFormat("{0} = {1}", (object)tableColumn.GetName((SmoScriptOptions)options), (object)parameterName);
            }
            deleteCommand.CommandText = stringBuilder.ToString();
            return deleteCommand;
        }

        private void GetColumns() {
            this._columns = new SmoTableColumnCollection(this);
            DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT *, COLUMNPROPERTY(object_id, Name, 'Ordinal') Ordinal FROM sys.columns WHERE object_id = '{1}' ORDER BY column_id", (object)this.Database.Name, (object)this.Id);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this._columns.Add(new SmoTableColumn(this, row));
            dataTable.Dispose();
        }

        private void GetConstraints() {
            this._constraints = new SmoConstraintCollection(this);
            DataTable dataTable1 = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.key_constraints WHERE parent_object_id = {1}", (object)this.Database.Name, (object)this.Id);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable1.Rows)
                this._constraints.Add((SmoConstraint)new SmoKeyConstraint(this, row));
            dataTable1.Dispose();
            DataTable dataTable2 = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.foreign_keys WHERE parent_object_id = {1}", (object)this.Database.Name, (object)this.Id);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable2.Rows)
                this._constraints.Add((SmoConstraint)new SmoForeignKeyConstraint(this, row));
            dataTable2.Dispose();
            DataTable dataTable3 = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.check_constraints WHERE parent_object_id = {1}", (object)this.Database.Name, (object)this.Id);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable3.Rows)
                this._constraints.Add((SmoConstraint)new SmoCheckConstraint(this, row));
            dataTable3.Dispose();
        }

        private void GetIndexes() {
            this._indexes = new SmoIndexCollection(this);
            DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.indexes WHERE object_id = {1} AND Name IS NOT NULL", (object)this.Database.Name, (object)this.Id);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this._indexes.Add(new SmoIndex(this, row));
            dataTable.Dispose();
        }

        private void GetStatistics() {
            this._statistics = new SmoStatisticCollection(this);
            DataTable dataTable = this.GetDataTable(CommandType.Text, Statements.ObjectStatistics, (object)this.Database.Name, (object)this.Id);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this._statistics.Add(new SmoStatistic(this, row));
            dataTable.Dispose();
        }

        private void GetTriggers() {
            this._triggers = new SmoTriggerCollection(this);
            DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT t.*,    CONVERT(BIT, CASE WHEN ISNULL(smt.definition, ssmt.definition) IS NULL THEN 1 ELSE 0 END) IsEncrypted,    CONVERT(BIT, CASE WHEN t.is_ms_shipped = 1 OR ep.major_id IS NOT NULL THEN 1 ELSE 0 END) IsSystemTrigger FROM sys.triggers AS t    LEFT JOIN sys.sql_modules smt ON t.object_id = smt.object_id    LEFT JOIN sys.system_sql_modules ssmt ON t.object_id = ssmt.object_id    LEFT JOIN sys.extended_properties ep ON t.object_id = ep.major_id       AND ep.minor_id = 0       AND ep.class = 1       AND ep.name = N'microsoft_database_tools_support' WHERE t.type = 'TR'    AND parent_id = {1}", (object)this.Database.Name, (object)this.Id);
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this._triggers.Add(new SmoTrigger(this, row));
            dataTable.Dispose();
        }
    }
}
