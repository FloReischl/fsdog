// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoFunction
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;
using System.Diagnostics;

namespace FR.Data.SqlClient.Smo
{
  public class SmoFunction : SmoDatabaseObject
  {
    private SmoFunctionType _functionType;

    internal SmoFunction(SmoDatabase database, DataRow infoRow)
      : base(database, infoRow)
    {
    }

    public SmoFunctionType FunctionType
    {
      [DebuggerNonUserCode] get
      {
        if (this._functionType == SmoFunctionType.Unknown)
          this.getFunctionType();
        return this._functionType;
      }
    }

    public override string GetCreateStatement(object scriptOptions) => this.Database.GetRoutineStatement((SmoObject) this);

    private void getFunctionType()
    {
      DataTable dataTable = this.GetDataTable(CommandType.Text, "USE {0} SELECT OBJECTPROPERTYEX({1}, 'IsScalarFunction') IsScalarFunction, OBJECTPROPERTYEX({2}, 'IsTableFunction') IsTableFunction USE {3}", (object) this.Database.Name, (object) this.Id, (object) this.Id, (object) this.Database.Server.Connection.Database);
      DataRow row = dataTable.Rows[0];
      this._functionType = (int) row["IsScalarFunction"] != 1 ? ((int) row["IsTableFunction"] != 1 ? SmoFunctionType.Unknown : SmoFunctionType.Table) : SmoFunctionType.Scalar;
      dataTable.Dispose();
    }
  }
}
