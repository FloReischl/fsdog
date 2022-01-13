// Decompiled with JetBrains decompiler
// Type: FR.CodeDom.Compiler.CSharpScriptCompilerException
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Runtime.Serialization;

namespace FR.CodeDom.Compiler
{
  [Serializable]
  public class CSharpScriptCompilerException : Exception
  {
    public const int METHOD_NOT_FOUND = 1;
    private int _errorId;

    public CSharpScriptCompilerException()
    {
    }

    public CSharpScriptCompilerException(string message)
      : base(message)
    {
    }

    public CSharpScriptCompilerException(string message, Exception inner)
      : base(message, inner)
    {
    }

    protected CSharpScriptCompilerException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public CSharpScriptCompilerException(string message, params object[] args)
      : base(string.Format(message, args))
    {
    }

    public CSharpScriptCompilerException(Exception inner, string message, params object[] args)
      : base(string.Format(message, args), inner)
    {
    }

    public int ErrorId
    {
      get => this._errorId;
      set => this._errorId = value;
    }
  }
}
