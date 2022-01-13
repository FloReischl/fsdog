// Decompiled with JetBrains decompiler
// Type: FR.CodeDom.Compiler.CSharpScriptCompiler
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FR.CodeDom.Compiler
{
  public class CSharpScriptCompiler
  {
    internal string EntryPoint = "Main";
    private StringBuilder _sourceCode;
    private bool _generateExecutable;
    private FileInfo _outputFile;
    private bool _optimize;
    private List<string> _references;
    private bool _appendHostInformation;
    private FileInfo _file;

    public StringBuilder SourceCode
    {
      get => this._sourceCode;
      set => this._sourceCode = value;
    }

    public bool GenerateExecutable
    {
      get => this._generateExecutable;
      set => this._generateExecutable = value;
    }

    public FileInfo OutputFile
    {
      get => this._outputFile;
      set => this._outputFile = value;
    }

    public bool Optimize
    {
      get => this._optimize;
      set => this._optimize = value;
    }

    public List<string> References
    {
      get => this._references;
      set => this._references = value;
    }

    public bool AppendHostInformation
    {
      get => this._appendHostInformation;
      set => this._appendHostInformation = value;
    }

    public FileInfo SourceFile
    {
      get => this._file;
      set => this._file = value;
    }

    public CSharpScriptCompiler(StringBuilder sbCode)
      : this()
    {
      this.SourceCode = sbCode;
    }

    public CSharpScriptCompiler()
    {
      this.EntryPoint = "Main";
      this.Optimize = false;
      this.GenerateExecutable = true;
      this.References = new List<string>();
    }

    public Assembly Compile(StringBuilder sbCode)
    {
      if (sbCode != null)
        this.SourceCode = sbCode;
      if (this.SourceCode.Length == 0)
        throw new CSharpScriptCompilerException("No source code to compile found");
      CodeDomProvider codeDomProvider = (CodeDomProvider) new CSharpCodeProvider();
      CompilerParameters options = new CompilerParameters();
      options.IncludeDebugInformation = false;
      options.GenerateExecutable = this.GenerateExecutable;
      if (this.OutputFile == null)
      {
        options.GenerateInMemory = true;
      }
      else
      {
        options.GenerateInMemory = false;
        options.OutputAssembly = this.OutputFile.FullName;
      }
      if (this.Optimize)
      {
        if (options.CompilerOptions == null)
          options.CompilerOptions = "/optimize";
        else
          options.CompilerOptions += " /optimize";
      }
      options.ReferencedAssemblies.Add("mscorlib.dll");
      foreach (string reference in this.References)
        options.ReferencedAssemblies.Add(reference);
      if (this.AppendHostInformation)
      {
        Assembly assembly = this.GetType().Assembly;
        if (!options.ReferencedAssemblies.Contains(assembly.Location))
          options.ReferencedAssemblies.Add(assembly.Location);
        this.appendHostInformation();
      }
      CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(options, this.SourceCode.ToString());
      if (compilerResults.Errors.Count > 0)
      {
        Exception inner = (Exception) null;
        foreach (CompilerError error in (CollectionBase) compilerResults.Errors)
        {
          if (!error.IsWarning)
          {
            if (inner == null)
              inner = (Exception) new CSharpScriptCompilerException("Copiler error:\r\n{0}: {1}", new object[2]
              {
                (object) error.ErrorNumber,
                (object) error.ErrorText
              });
            else
              inner = (Exception) new CSharpScriptCompilerException(inner, "Copiler error:\r\n{0}: {1}", new object[2]
              {
                (object) error.ErrorNumber,
                (object) error.ErrorText
              });
          }
        }
        if (inner != null)
          throw inner;
      }
      return compilerResults.CompiledAssembly;
    }

    public void Run(Assembly assembly, string[] args)
    {
      if (args == null)
        args = new string[0];
      foreach (Type type in assembly.GetModules(false)[0].GetTypes())
      {
        MethodInfo method = type.GetMethod(this.EntryPoint, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (method != null)
        {
          if (method.GetParameters().Length == 1)
          {
            if (!method.GetParameters()[0].ParameterType.IsArray)
              return;
            method.Invoke((object) null, new object[1]
            {
              (object) args
            });
            return;
          }
          method.Invoke((object) null, (object[]) null);
          return;
        }
      }
      throw new CSharpScriptCompilerException("Can not find static entry point '{0}'", new object[1]
      {
        (object) this.EntryPoint
      })
      {
        ErrorId = 1
      };
    }

    public static object Run(Assembly assembly, string methodName, Type[] types, object[] values)
    {
      foreach (Type type in assembly.GetModules()[0].GetTypes())
      {
        MethodInfo method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, types, (ParameterModifier[]) null);
        if (method != null)
          return method.Invoke((object) null, values);
      }
      throw new CSharpScriptCompilerException("Can not find static method '{0}'", new object[1]
      {
        (object) methodName
      })
      {
        ErrorId = 1
      };
    }

    private void appendHostInformation()
    {
      string str = "CSScriptHost";
      this.SourceCode.Append("\r\n");
      this.SourceCode.Append("namespace CSScript\r\n");
      this.SourceCode.Append("{\r\n");
      this.SourceCode.AppendFormat("public sealed class {0} : {1}\r\n", (object) str, (object) typeof (CSharpScriptHost).FullName);
      this.SourceCode.Append("{\r\n");
      this.SourceCode.AppendFormat("public {0} ()\r\n", (object) str);
      this.SourceCode.Append("{ }\r\n");
      this.SourceCode.Append("protected override string getScriptFileName()\r\n");
      this.SourceCode.Append("{\r\n");
      this.SourceCode.AppendFormat("return @\"{0}\";\r\n", (object) this.SourceFile.FullName);
      this.SourceCode.Append("}\r\n");
      this.SourceCode.Append("}\r\n");
      this.SourceCode.Append("}\r\n");
    }
  }
}
