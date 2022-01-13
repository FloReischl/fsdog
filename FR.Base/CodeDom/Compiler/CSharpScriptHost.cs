// Decompiled with JetBrains decompiler
// Type: FR.CodeDom.Compiler.CSharpScriptHost
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.IO;

namespace FR.CodeDom.Compiler
{
  public abstract class CSharpScriptHost
  {
    internal const string HOST_NAMESPACE = "CSSriptHostNS";
    internal const string HOST_CLASS = "C62448B4B1C7B49c5BC0FE7C8F68A5020";

    public string GetScriptFileName() => this.getScriptFileName();

    public string GetScriptDirectoryName() => new FileInfo(this.getScriptFileName()).DirectoryName;

    protected abstract string getScriptFileName();
  }
}
