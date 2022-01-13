// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDatabaseFile
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Diagnostics;

namespace FR.Data.SqlClient.Smo
{
  public class SmoDatabaseFile : SmoObject
  {
    private double _spaceAllocated;
    private double _spaceUsed;

    internal SmoDatabaseFile(SmoDatabase database, DataRow infoRow)
      : base((SmoObject) database, infoRow)
    {
    }

    public string FileLocation
    {
      [DebuggerNonUserCode] get => (string) this._infoRow["physical_name"];
    }

    public SmoDatabase Database
    {
      [DebuggerNonUserCode] get => (SmoDatabase) this.ParentObject;
    }

    public override int Id => (int) this._infoRow["file_id"];

    public override string Name => (string) this._infoRow["name"];

    public double SpaceAllocated
    {
      [DebuggerNonUserCode] get
      {
        if (this._spaceAllocated == 0.0)
          this.GetSpaceInformation();
        return this._spaceAllocated;
      }
    }

    public double SpaceFree
    {
      [DebuggerNonUserCode] get => this.SpaceAllocated - this.SpaceUsed;
    }

    public double SpaceUsed
    {
      [DebuggerNonUserCode] get
      {
        if (this._spaceUsed == 0.0)
          this.GetSpaceInformation();
        return this._spaceUsed;
      }
    }

    public SmoDatabaseFileType FileType
    {
      [DebuggerNonUserCode] get => (SmoDatabaseFileType) (byte) this._infoRow["type"];
    }

    public SmoDatabaseFileState FileState
    {
      [DebuggerNonUserCode] get => (SmoDatabaseFileState) this._infoRow["state"];
    }

    public override string GetCreateStatement(object scriptOptions) => throw SmoExceptionHelper.GetNotImplemented("The method 'GetCreateStatement' is not implemented.");

    private void GetSpaceInformation()
    {
      string database = this.Database.Server.Connection.Database;
      if (this.FileType == SmoDatabaseFileType.Data)
      {
        DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} DBCC SHOWFILESTATS USE {1}", (object) this.Database.Name, (object) database);
        DataRow row = dataTable.Rows[0];
        this._spaceAllocated = (double) (long) row["TotalExtents"] * 64.0 / 1024.0;
        this._spaceUsed = (double) (long) row["UsedExtents"] * 64.0 / 1024.0;
        dataTable.Dispose();
      }
      else if (this.FileType == SmoDatabaseFileType.Log)
      {
        DataTable dataTable = this.GetDataTable(CommandType.Text, "DBCC SQLPERF ( LOGSPACE )");
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if ((string) row["Database Name"] == this.Database.Name)
          {
            this._spaceAllocated = (double) (float) row["Log Size (MB)"];
            this._spaceUsed = (double) (float) row["Log Space Used (%)"] * this._spaceAllocated / 100.0;
            break;
          }
        }
        dataTable.Dispose();
      }
      else
      {
        this._spaceAllocated = -1.0;
        this._spaceUsed = -1.0;
      }
    }
  }
}
