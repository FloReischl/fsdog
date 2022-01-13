// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDatabase
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System;
using System.Data;
using System.Diagnostics;

namespace FR.Data.SqlClient.Smo
{
  [DebuggerDisplay("{Name}")]
  public class SmoDatabase : SmoObject
  {
    private SmoDataTypeCollection _dataTypes;
    private SmoCollection<SmoDatabaseFile> _files;
    private SmoDatabaseObjectCollection<SmoFunction> _functions;
    private SmoCollection<SmoDatabasePrincipal> _principals;
    private SmoDatabaseObjectCollection<SmoProcedure> _procedures;
    private SmoCollection<SmoDatabaseRole> _roles;
    private SmoSchemaCollection _schemas;
    private SmoDatabaseObjectCollection<SmoTable> _tables;
    private SmoCollection<SmoUser> _users;
    private SmoDatabaseObjectCollection<SmoView> _views;

    internal SmoDatabase(SmoServer server, DataRow infoRow)
      : base((SmoObject) server, infoRow)
    {
      this._tables = (SmoDatabaseObjectCollection<SmoTable>) null;
      this._procedures = (SmoDatabaseObjectCollection<SmoProcedure>) null;
      this._functions = (SmoDatabaseObjectCollection<SmoFunction>) null;
    }

    public SmoServer Server => (SmoServer) this.ParentObject;

    public override string Name => (string) this._infoRow["name"];

    public override int Id => (int) this._infoRow["database_id"];

    public SmoDataTypeCollection DataTypes
    {
      get
      {
        if (this._dataTypes == null)
          this.getDataTypes();
        return this._dataTypes;
      }
    }

    public SmoCollection<SmoDatabaseFile> Files
    {
      [DebuggerNonUserCode] get
      {
        if (this._files == null)
          this.getFiles();
        return this._files;
      }
    }

    public SmoDatabaseObjectCollection<SmoFunction> Functions
    {
      get
      {
        if (this._functions == null)
          this.getFunctions();
        return this._functions;
      }
    }

    public SmoCollection<SmoDatabasePrincipal> Principals
    {
      get
      {
        if (this._principals == null)
          this.getPrincipals();
        return this._principals;
      }
    }

    public SmoDatabaseObjectCollection<SmoProcedure> Procedures
    {
      get
      {
        if (this._procedures == null)
          this.getProcedures();
        return this._procedures;
      }
    }

    public SmoCollection<SmoDatabaseRole> Roles
    {
      get
      {
        if (this._roles == null)
          this.getPrincipals();
        return this._roles;
      }
    }

    public SmoSchemaCollection Schemas
    {
      get
      {
        if (this._schemas == null)
          this.getSchemas();
        return this._schemas;
      }
    }

    public SmoDatabaseObjectCollection<SmoTable> Tables
    {
      get
      {
        if (this._tables == null)
          this.getTables();
        return this._tables;
      }
    }

    public SmoCollection<SmoUser> Users
    {
      get
      {
        if (this._users == null)
          this.getPrincipals();
        return this._users;
      }
    }

    public SmoDatabaseObjectCollection<SmoView> Views
    {
      [DebuggerNonUserCode] get
      {
        if (this._views == null)
          this.getViews();
        return this._views;
      }
    }

    public override string GetCreateStatement(object scriptOptions) => string.Format("CREATE DATABASE {0}", (object) this.Name);

    public override string ToString() => this.Name;

    public override string GetName(SmoScriptOptions options)
    {
      if (options == null)
        options = new SmoScriptOptions();
      return options.Database ? string.Format("[{0}]", (object) this.Name) : "";
    }

    public override void Refresh()
    {
      if (this._dataTypes != null)
      {
        this._dataTypes.clear();
        this._dataTypes = (SmoDataTypeCollection) null;
      }
      if (this._functions != null)
      {
        this._functions.clear();
        this._functions = (SmoDatabaseObjectCollection<SmoFunction>) null;
      }
      if (this._principals != null)
      {
        this._principals.clear();
        this._principals = (SmoCollection<SmoDatabasePrincipal>) null;
      }
      if (this._procedures != null)
      {
        this._procedures.clear();
        this._procedures = (SmoDatabaseObjectCollection<SmoProcedure>) null;
      }
      if (this._roles != null)
      {
        this._roles.clear();
        this._roles = (SmoCollection<SmoDatabaseRole>) null;
      }
      if (this._schemas != null)
      {
        this._schemas.clear();
        this._schemas = (SmoSchemaCollection) null;
      }
      if (this._tables != null)
      {
        this._tables.clear();
        this._tables = (SmoDatabaseObjectCollection<SmoTable>) null;
      }
      if (this._users != null)
      {
        this._users.clear();
        this._users = (SmoCollection<SmoUser>) null;
      }
      if (this._infoRow == null)
        return;
      DataTable dataTable = this.GetDataTable(CommandType.Text, "SELECT * FROM sys.databases WHERE database_id = {0}", (object) this.Id);
      if (dataTable.Rows.Count == 1)
      {
        this._infoRow = dataTable.Rows[0];
        dataTable.Dispose();
      }
      else
      {
        dataTable.Dispose();
        throw SmoExceptionHelper.GetArgument("Cannot find database '{0}'", (object) this.Name);
      }
    }

    internal string getSystemRoutineStatement(SmoObject databaseObject)
    {
      string columnName;
      string statement;
      if (this.Server.MajorVersion == SmoServerMajorVersion.SqlServer2005)
      {
        columnName = "definition";
        statement = string.Format("USE {0} SELECT * FROM sys.system_sql_modules WHERE object_Id = {1}", (object) this.Name, (object) databaseObject.Id);
      }
      else
      {
        columnName = "text";
        statement = string.Format("USE {0} SELECT * FROM dbo.syscomments WHERE id = {1}", (object) this.Name, (object) databaseObject.Id);
      }
      DataTable dataTable = (DataTable) null;
      try
      {
        dataTable = this.Server.GetDataTable(CommandType.Text, statement);
        statement = string.Empty;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          statement += (string) row[columnName];
      }
      catch (Exception ex)
      {
        this.LogEx(ex);
        throw ex;
      }
      finally
      {
        dataTable?.Dispose();
      }
      return statement;
    }

    internal string getRoutineStatement(SmoObject databaseObject)
    {
      string columnName;
      string statement;
      if (this.Server.MajorVersion == SmoServerMajorVersion.SqlServer2005)
      {
        columnName = "definition";
        statement = string.Format("SELECT * FROM {0}.sys.sql_modules WHERE Object_Id = {1}", (object) this.Name, (object) databaseObject.Id);
      }
      else
      {
        columnName = "text";
        statement = string.Format("SELECT * FROM {0}.dbo.syscomments WHERE ID = {1}", (object) this.Name, (object) databaseObject.Id);
      }
      DataTable dataTable = (DataTable) null;
      try
      {
        dataTable = this.Server.GetDataTable(CommandType.Text, statement);
        statement = string.Empty;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          statement += (string) row[columnName];
      }
      catch (Exception ex)
      {
        this.LogEx(ex);
        throw ex;
      }
      finally
      {
        dataTable?.Dispose();
      }
      return statement;
    }

    private void getDataTypes()
    {
      this._dataTypes = new SmoDataTypeCollection(this);
      DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.types ORDER BY name", (object) this.Name);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._dataTypes.add(new SmoDataType(this, row));
      dataTable.Dispose();
    }

    private void getFiles()
    {
      this._files = new SmoCollection<SmoDatabaseFile>((SmoObject) this);
      DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.database_files", (object) this.Name);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._files.add(new SmoDatabaseFile(this, row));
      dataTable.Dispose();
    }

    private void getFunctions()
    {
      this._functions = new SmoDatabaseObjectCollection<SmoFunction>(this);
      DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.objects WHERE type = 'FN'", (object) this.Name);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._functions.add(new SmoFunction(this, row));
      dataTable.Dispose();
    }

    private void getPrincipals()
    {
      this._principals = new SmoCollection<SmoDatabasePrincipal>((SmoObject) this);
      this._users = new SmoCollection<SmoUser>((SmoObject) this);
      this._roles = new SmoCollection<SmoDatabaseRole>((SmoObject) this);
      DataTable dataTable = this.Server.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.database_principals", (object) this.Name);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        SmoDatabasePrincipal databasePrincipal;
        switch (((string) row["type"]).ToUpper())
        {
          case "S":
            databasePrincipal = (SmoDatabasePrincipal) new SmoUser(this, row);
            break;
          case "R":
            databasePrincipal = (SmoDatabasePrincipal) new SmoDatabaseRole(this, row);
            break;
          case "C":
            databasePrincipal = (SmoDatabasePrincipal) new SmoUser(this, row);
            break;
          case "G":
            databasePrincipal = (SmoDatabasePrincipal) new SmoUser(this, row);
            break;
          case "U":
            databasePrincipal = (SmoDatabasePrincipal) new SmoUser(this, row);
            break;
          case "A":
            databasePrincipal = (SmoDatabasePrincipal) new SmoUser(this, row);
            break;
          case "K":
            databasePrincipal = (SmoDatabasePrincipal) new SmoUser(this, row);
            break;
          default:
            databasePrincipal = (SmoDatabasePrincipal) null;
            break;
        }
        if (databasePrincipal != null)
        {
          this._principals.add(databasePrincipal);
          if (databasePrincipal is SmoUser)
            this._users.add((SmoUser) databasePrincipal);
          else if (databasePrincipal is SmoDatabaseRole)
            this._roles.add((SmoDatabaseRole) databasePrincipal);
        }
      }
      dataTable.Dispose();
    }

    private void getProcedures()
    {
      DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT sp.*,     CONVERT(BIT, CASE WHEN ISNULL(smsp.definition, ssmsp.definition) IS NULL THEN 1 ELSE 0 END) IsEncrypted,     CONVERT(BIT, CASE WHEN SP.is_ms_shipped = 1 OR EP.major_id IS NOT NULL THEN 1 ELSE 0 END) IsSystemProcedure FROM sys.all_objects AS sp     LEFT JOIN sys.sql_modules SMSP ON SP.object_id = SMSP.object_id     LEFT JOIN sys.system_sql_modules SSMSP ON SP.object_id = SSMSP.object_id     LEFT JOIN sys.extended_properties EP ON SP.object_id = EP.major_id        AND EP.minor_id = 0        AND EP.class = 1        AND EP.name = N'microsoft_database_tools_support' WHERE SP.type IN (N'P', N'RF', N'PC')", (object) this.Name);
      this._procedures = new SmoDatabaseObjectCollection<SmoProcedure>(this);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._procedures.add(new SmoProcedure(this, row));
      dataTable.Dispose();
    }

    private void getSchemas()
    {
      this._schemas = new SmoSchemaCollection(this);
      DataTable dataTable = this.Server.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.schemas", (object) this.Name);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._schemas.add(new SmoSchema(this, row));
      dataTable.Dispose();
    }

    private void getTables()
    {
      this._tables = new SmoDatabaseObjectCollection<SmoTable>(this);
      DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT * FROM sys.tables", (object) this.Name);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._tables.add(new SmoTable(this, row));
      dataTable.Dispose();
    }

    private void getViews()
    {
      DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT v.*,     CONVERT(BIT, CASE WHEN ISNULL(smv.definition, ssmv.definition) IS NULL THEN 1 ELSE 0 END) IsEncrypted,     CONVERT(BIT, CASE WHEN v.is_ms_shipped = 1 OR ep.major_id IS NOT NULL THEN 1 ELSE 0 END) IsSystemView FROM sys.all_views AS V     LEFT JOIN sys.sql_modules SMV ON V.object_id = SMV.object_id     LEFT JOIN sys.system_sql_modules SSMV ON V.object_id = SSMV.object_id     LEFT JOIN sys.extended_properties EP ON V.object_id = EP.major_id        AND EP.minor_id = 0        AND EP.class = 1        AND EP.name = N'microsoft_database_tools_support' WHERE V.type = 'V'", (object) this.Name);
      this._views = new SmoDatabaseObjectCollection<SmoView>(this);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this._views.add(new SmoView(this, row));
      dataTable.Dispose();
    }
  }
}
