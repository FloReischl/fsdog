// Decompiled with JetBrains decompiler
// Type: FR.Data.SqlClient.Smo.Statements
// Assembly: FR.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=4b34fad602d33c1a
// MVID: 73A11F4D-1B6A-4A84-B1D3-AD1912A96D1C
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Data.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace FR.Data.SqlClient.Smo
{
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
  [DebuggerNonUserCode]
  internal class Statements
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Statements()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Statements.resourceMan, (object) null))
          Statements.resourceMan = new ResourceManager("FR.Data.SqlClient.Smo.Statements", typeof (Statements).Assembly);
        return Statements.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => Statements.resourceCulture;
      set => Statements.resourceCulture = value;
    }

    internal static string ObjectStatistics => Statements.ResourceManager.GetString(nameof (ObjectStatistics), Statements.resourceCulture);
  }
}
