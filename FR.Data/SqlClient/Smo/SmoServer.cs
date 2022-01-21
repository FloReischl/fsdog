// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoServer
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using FR.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FR.Data.SqlClient.Smo {
    public class SmoServer : SmoObject {
        private SmoCollection<SmoDatabase> _databases;
        private SmoCollection<SmoServerPrincipal> _principals;
        private SmoCollection<SmoLogin> _logins;
        private SmoCollection<SmoServerRole> _roles;

        public SmoServer(SqlConnection connection, LoggingManager logger)
          : base((SmoObject)null, (DataRow)null) {
            this.Connection = connection;
            if (this.Connection.State != ConnectionState.Open)
                this.Connection.Open();
            this._infoRow = this.GetDataTable(CommandType.Text, "SELECT * FROM sys.servers WHERE server_id = 0").Rows[0];
        }

        public override string Name => (string)this._infoRow["name"];

        public override int Id => (int)this._infoRow["server_id"];

        public SqlConnection Connection { get; }

        public SmoServerMajorVersion MajorVersion {
            get {
                switch (int.Parse(this.Connection.ServerVersion.Substring(0, 2))) {
                    case 6:
                        return SmoServerMajorVersion.SqlServer6;
                    case 7:
                        return SmoServerMajorVersion.SqlServer7;
                    case 8:
                        return SmoServerMajorVersion.SqlServer2000;
                    case 9:
                        return SmoServerMajorVersion.SqlServer2005;
                    default:
                        return SmoServerMajorVersion.Unknown;
                }
            }
        }

        public SmoCollection<SmoDatabase> Databases {
            get {
                if (this._databases == null)
                    this.GetDatabases();
                return this._databases;
            }
        }

        public SmoCollection<SmoServerPrincipal> Principals {
            get {
                if (this._principals == null)
                    this.GetPrincipals();
                return this._principals;
            }
        }

        public SmoCollection<SmoLogin> Logins {
            get {
                if (this._logins == null)
                    this.GetPrincipals();
                return this._logins;
            }
        }

        public SmoCollection<SmoServerRole> Roles {
            get {
                if (this._roles == null)
                    this.GetPrincipals();
                return this._roles;
            }
        }

        public override string GetCreateStatement(object scriptOptions) => throw new Exception("The method or operation is not implemented.");

        public SmoLogin FindLoginBySid(byte[] sid) {
            foreach (SmoLogin login in this.Logins) {
                byte[] sid1 = login.Sid;
                if (sid.Length == sid1.Length) {
                    bool flag = true;
                    for (int index = 0; index < sid.Length; ++index) {
                        if ((int)sid[index] != (int)sid1[index]) {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        return login;
                }
            }
            return (SmoLogin)null;
        }

        private void GetDatabases() {
            this._databases = new SmoCollection<SmoDatabase>((SmoObject)this);
            DataTable dataTable = this.GetDataTable(CommandType.Text, "SELECT * FROM sys.databases");
            foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                this._databases.Add(new SmoDatabase(this, row));
            dataTable.Dispose();
        }

        private void GetPrincipals() {
            this._logins = new SmoCollection<SmoLogin>((SmoObject)this);
            this._roles = new SmoCollection<SmoServerRole>((SmoObject)this);
            this._principals = new SmoCollection<SmoServerPrincipal>((SmoObject)this);
            foreach (DataRow row in (InternalDataCollectionBase)this.GetDataTable(CommandType.Text, "SELECT * FROM sys.server_principals").Rows) {
                SmoServerPrincipal smoServerPrincipal;
                switch (((string)row["type"]).ToUpper()) {
                    case "S":
                        smoServerPrincipal = (SmoServerPrincipal)new SmoLogin(this, row);
                        break;
                    case "R":
                        smoServerPrincipal = (SmoServerPrincipal)new SmoServerRole(this, row);
                        break;
                    case "C":
                        smoServerPrincipal = (SmoServerPrincipal)new SmoLogin(this, row);
                        break;
                    case "G":
                        smoServerPrincipal = (SmoServerPrincipal)new SmoLogin(this, row);
                        break;
                    case "U":
                        smoServerPrincipal = (SmoServerPrincipal)new SmoLogin(this, row);
                        break;
                    case "K":
                        smoServerPrincipal = (SmoServerPrincipal)new SmoLogin(this, row);
                        break;
                    default:
                        throw new InvalidExpressionException(string.Format("Invalid server principal type '{0}' with description '{1}'", row["type"], row["type_desc"]));
                }
                this._principals.Add(smoServerPrincipal);
                if (smoServerPrincipal is SmoLogin)
                    this._logins.Add((SmoLogin)smoServerPrincipal);
                else if (smoServerPrincipal is SmoServerRole)
                    this._roles.Add((SmoServerRole)smoServerPrincipal);
            }
        }
    }
}
