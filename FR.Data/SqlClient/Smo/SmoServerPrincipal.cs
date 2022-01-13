// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.SmoServerPrincipal
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.Data;

namespace FR.Data.SqlClient.Smo
{
  public abstract class SmoServerPrincipal : SmoObject
  {
    public SmoServer Server => (SmoServer) this.ParentObject;

    public override string Name => (string) this._infoRow["name"];

    public override int Id => (int) this._infoRow["principal_id"];

    public byte[] Sid => (byte[]) this._infoRow["sid"];

    public SmoServerPrincipalType Type
    {
      get
      {
        switch (((string) this._infoRow["type"]).ToUpper())
        {
          case "S":
            return SmoServerPrincipalType.SqlLogin;
          case "R":
            return SmoServerPrincipalType.ServerRole;
          case "C":
            return SmoServerPrincipalType.CertificateMappedLogin;
          case "G":
            return SmoServerPrincipalType.WindowsGroup;
          case "U":
            return SmoServerPrincipalType.WindowsLogin;
          case "K":
            return SmoServerPrincipalType.AsymmetricKeyMappedLogin;
          default:
            throw SmoExceptionHelper.GetNotImplemented("Missing implementation for server principal type '{0}' ('{1}')", this._infoRow["type"], this._infoRow["type_desc"]);
        }
      }
    }

    internal SmoServerPrincipal(SmoServer server, DataRow infoRow)
      : base((SmoObject) server, infoRow)
    {
    }
  }
}
