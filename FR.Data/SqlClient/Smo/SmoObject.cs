// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoObject
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using FR.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace FR.Data.SqlClient.Smo {
    [DebuggerDisplay("Type {GetType().Name} Name {Name}")]
    public abstract class SmoObject {
        protected DataRow _infoRow;

        internal SmoObject(SmoObject parentObject, DataRow infoRow) {
            this.ParentObject = parentObject;
            this._infoRow = infoRow;
            if (parentObject == null)
                return;
        }

        private ILogger Log { get; } = LoggingProvider.CreateLogger();

        public SmoObject ParentObject { get; }

        public SmoObjectState State { get; private set; }

        public virtual string Name {
            get => throw SmoExceptionHelper.GetInvalidOperation("Method not available for this object");
            set => throw SmoExceptionHelper.GetInvalidOperation("Method not available for this object");
        }

        public abstract int Id { get; }

        public virtual void Delete() => this.SetState(SmoObjectState.Deleted);

        public override int GetHashCode() => this.Id.GetHashCode();

        public virtual string GetName() => this.GetName((SmoScriptOptions)null);

        public virtual string GetName(SmoScriptOptions options) {
            if (options == null)
                options = new SmoScriptOptions();
            return string.Format("[{0}]", (object)this.Name);
        }

        public override bool Equals(object obj) => obj != null && obj.GetType() == this.GetType() && ((SmoObject)obj).Id == this.Id;

        public override string ToString() => this.Name;

        public virtual void Refresh() => throw SmoExceptionHelper.GetNotImplemented("The type '{0}' is not refreshable", (object)this.GetType().Name);

        public abstract string GetCreateStatement(object scriptOptions);

        internal SqlConnection GetConnection() => this is SmoServer ? ((SmoServer)this).Connection : this.ParentObject.GetConnection();

        internal DataTable GetDataTable(
          CommandType type,
          string statement,
          params object[] args) {
            DataTable dataTable = new DataTable();
            if (args != null)
                statement = string.Format(statement, args);
            SqlConnection connection = this.GetConnection();
            SqlCommand selectCommand = new SqlCommand {
                Connection = connection,
                CommandType = type,
                CommandText = statement
            };
            try {
                new SqlDataAdapter(selectCommand).Fill(dataTable);
            }
            catch (Exception ex) {
                this.Log.Ex(ex);
                dataTable.Dispose();
            }
            return dataTable;
        }

        internal virtual void SetState(SmoObjectState state) {
            if (this.State == SmoObjectState.Deleted && state != SmoObjectState.Deleted)
                throw SmoExceptionHelper.GetInvalidOperation("You are about to access a deleted object '{0}'", (object)this.Name);
            if (state == SmoObjectState.Changed && this.State == SmoObjectState.Original) {
                this.State = state;
            }
            else {
                if (state == SmoObjectState.Changed)
                    return;
                this.State = state;
            }
        }
    }
}
