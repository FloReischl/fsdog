// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoViewColumn
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Text.RegularExpressions;

namespace FR.Data.SqlClient.Smo {
    public class SmoViewColumn : SmoObject {
        private string _name;
        private SmoDataType _dataType;
        private int _maxSize;
        private string _collationName;
        private bool _isNullable;
        private int _precision;

        internal SmoViewColumn(SmoView view, DataRow infoRow)
          : base((SmoObject)view, infoRow) {
            this._collationName = (string)this._infoRow["collation_name"];
            this._dataType = this.View.Database.DataTypes.FindById((int)this._infoRow["user_type_id"]);
            this._maxSize = (int)this._infoRow["max_length"];
            this._isNullable = (bool)this._infoRow["is_nullable"];
            this._precision = (int)this._infoRow["precision"];
            this.SetState(SmoObjectState.Original);
        }

        public SmoViewColumn(SmoView view)
          : base((SmoObject)view, (DataRow)null) {
            this._collationName = "";
            this._dataType = this.View.Database.DataTypes.FindByName("varchar");
            this._maxSize = 50;
            this._isNullable = true;
            this._precision = 0;
            this.SetState(SmoObjectState.New);
        }

        public SmoView View => (SmoView)this.ParentObject;

        public override string Name {
            get {
                if (this._name == null && this._infoRow != null)
                    this._name = (string)this._infoRow["name"];
                return this._name;
            }
            set {
                if (!(this._name != value))
                    return;
                this._name = value;
                this.SetState(SmoObjectState.Changed);
            }
        }

        public override int Id => (int)this._infoRow["column_id"];

        public int Ordinal => (int)this._infoRow[nameof(Ordinal)];

        public SmoDataType DataType {
            get => this._dataType;
            set {
                if (this._dataType == value)
                    return;
                this._dataType = value;
                this.SetState(SmoObjectState.Changed);
            }
        }

        public SqlDbType SqlDbType {
            get => this.DataType.SqlDbType;
            set => this.DataType = this.View.Database.DataTypes.FindBySqlDbType(value);
        }

        public int MaximumSize {
            get => this._maxSize;
            set {
                if (this._maxSize == value)
                    return;
                this._maxSize = value;
                this.SetState(SmoObjectState.Changed);
            }
        }

        public object DefaultValue => (object)null;

        public string CollationName {
            get => this._collationName;
            set {
                if (!(this._collationName != value))
                    return;
                this._collationName = value;
                this.SetState(SmoObjectState.Changed);
            }
        }

        public bool IsNullable {
            get => this._isNullable;
            set {
                if (value == this._isNullable)
                    return;
                this._isNullable = value;
                this.SetState(SmoObjectState.Changed);
            }
        }

        public int Precision {
            get => this._precision;
            set {
                if (value == this._precision)
                    return;
                this._precision = value;
                this.SetState(SmoObjectState.Changed);
            }
        }

        public override void Delete() {
            if (this.State == SmoObjectState.New)
                this.View.Columns.Remove(this);
            base.Delete();
        }

        public override string GetName(SmoScriptOptions options) {
            string name = this.Name;
            return !new Regex("^([a-zA-Z]{0,256})$").IsMatch(name) ? string.Format("[{0}]", (object)name.Replace("[", "[[").Replace("]", "]]")) : base.GetName(options);
        }

        public override string GetCreateStatement(object scriptOptions) => throw SmoExceptionHelper.GetNotImplemented("The method GetCreateStatement is not implemented.");

        internal override void SetState(SmoObjectState state) {
            base.SetState(state);
            if (!BaseHelper.InList(state, SmoObjectState.Changed, SmoObjectState.Deleted, SmoObjectState.New))
                return;
            this.View.SetState(SmoObjectState.Changed);
        }
    }
}
