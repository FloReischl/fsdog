// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoDatabasePrincipal
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;

namespace FR.Data.SqlClient.Smo
{
  public abstract class SmoDatabasePrincipal : SmoObject
  {
    public SmoDatabase Database => (SmoDatabase) this.ParentObject;

    public override string Name => (string) this._infoRow["name"];

    public override int Id => (int) this._infoRow["principal_id"];

    public SmoDatabasePrincipalType Type
    {
      get
      {
        switch (((string) this._infoRow["type"]).ToUpper())
        {
          case "S":
            return SmoDatabasePrincipalType.SqlUser;
          case "R":
            return SmoDatabasePrincipalType.DatabaseRole;
          case "C":
            return SmoDatabasePrincipalType.CertificateMappedUser;
          case "G":
            return SmoDatabasePrincipalType.WindowsGroup;
          case "U":
            return SmoDatabasePrincipalType.WindowsUser;
          case "A":
            return SmoDatabasePrincipalType.ApplicationRole;
          case "K":
            return SmoDatabasePrincipalType.AsymmetricKeyMappedUser;
          default:
            throw SmoExceptionHelper.GetNotImplemented("Missing implementation for database pricipal type '{0}'", this._infoRow["type"]);
        }
      }
    }

    internal SmoDatabasePrincipal(SmoDatabase database, DataRow infoRow)
      : base((SmoObject) database, infoRow)
    {
    }
  }
}
