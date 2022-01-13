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

namespace FR.Data.SqlClient.Smo
{
  [DebuggerDisplay("Type {GetType().Name} Name {Name}")]
  public abstract class SmoObject : LoggingProvider
  {
    protected DataRow _infoRow;
    private SmoObject _parentObject;
    private SmoObjectState _state;

    internal SmoObject(SmoObject parentObject, DataRow infoRow)
    {
      this._parentObject = parentObject;
      this._infoRow = infoRow;
      if (parentObject == null)
        return;
      this.Logger = parentObject.Logger;
    }

    public SmoObject ParentObject => this._parentObject;

    public SmoObjectState State => this._state;

    public virtual string Name
    {
      get => throw SmoExceptionHelper.GetInvalidOperation("Method not available for this object");
      set => throw SmoExceptionHelper.GetInvalidOperation("Method not available for this object");
    }

    public abstract int Id { get; }

    public virtual void Delete() => this.setState(SmoObjectState.Deleted);

    public override int GetHashCode() => this.Id.GetHashCode();

    public virtual string GetName() => this.GetName((SmoScriptOptions) null);

    public virtual string GetName(SmoScriptOptions options)
    {
      if (options == null)
        options = new SmoScriptOptions();
      return string.Format("[{0}]", (object) this.Name);
    }

    public override bool Equals(object obj) => obj != null && obj.GetType() == this.GetType() && ((SmoObject) obj).Id == this.Id;

    public override string ToString() => this.Name;

    public virtual void Refresh() => throw SmoExceptionHelper.GetNotImplemented("The type '{0}' is not refreshable", (object) this.GetType().Name);

    public abstract string GetCreateStatement(object scriptOptions);

    internal SqlConnection GetConnection() => this is SmoServer ? ((SmoServer) this).Connection : this.ParentObject.GetConnection();

    internal DataTable GetDataTable(
      CommandType type,
      string statement,
      params object[] args)
    {
      DataTable dataTable = new DataTable();
      if (args != null)
        statement = string.Format(statement, args);
      SqlConnection connection = this.GetConnection();
      SqlCommand selectCommand = new SqlCommand();
      selectCommand.Connection = connection;
      selectCommand.CommandType = type;
      selectCommand.CommandText = statement;
      try
      {
        new SqlDataAdapter(selectCommand).Fill(dataTable);
      }
      catch (Exception ex)
      {
        this.LogEx(ex);
        dataTable.Dispose();
      }
      return dataTable;
    }

    internal virtual void setState(SmoObjectState state)
    {
      if (this._state == SmoObjectState.Deleted && state != SmoObjectState.Deleted)
        throw SmoExceptionHelper.GetInvalidOperation("You are about to access a deleted object '{0}'", (object) this.Name);
      if (state == SmoObjectState.Changed && this._state == SmoObjectState.Original)
      {
        this._state = state;
      }
      else
      {
        if (state == SmoObjectState.Changed)
          return;
        this._state = state;
      }
    }
  }
}
