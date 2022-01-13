// Decompiled with JetBrains decompiler
// Type: FR.ComponentModel.DynamicPropertyCreator
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace FR.ComponentModel
{
  public class DynamicPropertyCreator
  {
    private static Dictionary<string, PropertyDescriptor> _properties = new Dictionary<string, PropertyDescriptor>();
    private Type _componentType;
    private bool _isReadOnly;
    private string _propertyName;
    private Type _propertyType;

    public DynamicPropertyCreator()
    {
    }

    public DynamicPropertyCreator(PropertyDescriptor template)
    {
      this._componentType = template != null ? template.ComponentType : throw new ArgumentNullException(nameof (template));
      this._isReadOnly = template.IsReadOnly;
      this._propertyName = template.Name;
      this._propertyType = template.PropertyType;
    }

    public DynamicPropertyCreator(PropertyInfo template)
    {
      this._componentType = template != null ? template.DeclaringType : throw new ArgumentNullException(nameof (template));
      this._isReadOnly = !template.CanWrite;
      this._propertyName = template.Name;
      this._propertyType = template.PropertyType;
    }

    public Type ComponentType
    {
      [DebuggerNonUserCode] get => this._componentType;
      [DebuggerNonUserCode] set => this._componentType = value;
    }

    public bool IsReadOnly
    {
      [DebuggerNonUserCode] get => this._isReadOnly;
      [DebuggerNonUserCode] set => this._isReadOnly = value;
    }

    public string PropertyName
    {
      [DebuggerNonUserCode] get => this._propertyName;
      [DebuggerNonUserCode] set => this._propertyName = value;
    }

    public Type PropertyType
    {
      [DebuggerNonUserCode] get => this._propertyType;
      [DebuggerNonUserCode] set => this._propertyType = value;
    }

    public PropertyDescriptor Create()
    {
      string str1 = string.Format("{0}.{1}", (object) this.ComponentType.Namespace, (object) this.ComponentType.Name);
      string str2 = string.Format("{0}.{1}", (object) this.PropertyType.Namespace, (object) this.PropertyType.Name);
      string key = string.Format("{0}::{1}", (object) str1, (object) this.PropertyName);
      PropertyDescriptor propertyDescriptor1;
      if (DynamicPropertyCreator._properties.TryGetValue(key, out propertyDescriptor1))
        return propertyDescriptor1;
      string str3 = string.Format("{0}_{1}_{2}", (object) this.ComponentType.Name, (object) this.PropertyName, (object) this.ComponentType.Namespace.Replace(".", "_"));
      StringWriter stringWriter = new StringWriter();
      stringWriter.WriteLine("using System;");
      stringWriter.WriteLine("using System.ComponentModel;");
      stringWriter.WriteLine("");
      stringWriter.WriteLine("namespace DynamicProperties");
      stringWriter.WriteLine("{");
      stringWriter.WriteLine(" public class {0} : {1}", (object) str3, (object) typeof (PropertyDescriptor).Name);
      stringWriter.WriteLine(" {");
      stringWriter.WriteLine("    public {0}()", (object) str3);
      stringWriter.WriteLine("       : base(\"{0}\", null)", (object) this.PropertyName);
      stringWriter.WriteLine("    { }");
      stringWriter.WriteLine("");
      stringWriter.WriteLine(" public static PropertyDescriptor Create()");
      stringWriter.WriteLine(" {");
      stringWriter.WriteLine("    return new {0}();", (object) str3);
      stringWriter.WriteLine(" }");
      stringWriter.WriteLine("");
      stringWriter.WriteLine(" public override Type ComponentType");
      stringWriter.WriteLine(" {");
      stringWriter.WriteLine("    get {{ return typeof({0}); }}", (object) str1);
      stringWriter.WriteLine(" }");
      stringWriter.WriteLine("");
      stringWriter.WriteLine(" public override bool IsReadOnly");
      stringWriter.WriteLine(" {");
      stringWriter.WriteLine("    get {{ return {0}; }}", this.IsReadOnly ? (object) "true" : (object) "false");
      stringWriter.WriteLine(" }");
      stringWriter.WriteLine("");
      stringWriter.WriteLine(" public override Type PropertyType");
      stringWriter.WriteLine(" {");
      stringWriter.WriteLine("    get {{ return typeof({0}); }}", (object) str2);
      stringWriter.WriteLine(" }");
      stringWriter.WriteLine("");
      stringWriter.WriteLine(" public override bool CanResetValue(object component)");
      stringWriter.WriteLine(" {");
      stringWriter.WriteLine("    return false;");
      stringWriter.WriteLine(" }");
      stringWriter.WriteLine("");
      stringWriter.WriteLine(" public override object GetValue(object component)");
      stringWriter.WriteLine(" {");
      stringWriter.WriteLine("    {0} obj = ({0})component;", (object) str1);
      stringWriter.WriteLine("    return obj.{0};", (object) this.PropertyName);
      stringWriter.WriteLine(" }");
      stringWriter.WriteLine("");
      stringWriter.WriteLine(" public override void ResetValue(object component)");
      stringWriter.WriteLine(" {");
      stringWriter.WriteLine("    throw new NotImplementedException(\"Reset value method is not dynamically created\");");
      stringWriter.WriteLine(" }");
      stringWriter.WriteLine("");
      stringWriter.WriteLine(" public override void SetValue(object component, object value)");
      stringWriter.WriteLine(" {");
      if (this.IsReadOnly)
      {
        stringWriter.WriteLine("    throw new Exception(\"Property {0} is read only.\");", (object) this.PropertyName);
      }
      else
      {
        stringWriter.WriteLine("    {0} obj = ({0})component;", (object) str1);
        stringWriter.WriteLine("    obj.{0} = ({1})value;", (object) this.PropertyName, (object) str2);
      }
      stringWriter.WriteLine(" }");
      stringWriter.WriteLine("");
      stringWriter.WriteLine(" public override bool ShouldSerializeValue(object component)");
      stringWriter.WriteLine(" {");
      stringWriter.WriteLine("    return false;");
      stringWriter.WriteLine(" }");
      stringWriter.WriteLine(" }");
      stringWriter.WriteLine("}");
      CodeDomProvider codeDomProvider = (CodeDomProvider) new CSharpCodeProvider();
      CompilerParameters options = new CompilerParameters();
      options.IncludeDebugInformation = false;
      options.GenerateExecutable = false;
      options.GenerateInMemory = true;
      if (options.CompilerOptions == null)
        options.CompilerOptions = "/optimize";
      else
        options.CompilerOptions += " /optimize";
      options.ReferencedAssemblies.Add("mscorlib.dll");
      if (!options.ReferencedAssemblies.Contains(this.PropertyType.Assembly.Location))
        options.ReferencedAssemblies.Add(this.PropertyType.Assembly.Location);
      foreach (AssemblyName referencedAssembly in this.PropertyType.Assembly.GetReferencedAssemblies())
      {
        Assembly assembly = Assembly.Load(referencedAssembly);
        if (!options.ReferencedAssemblies.Contains(assembly.Location))
          options.ReferencedAssemblies.Add(assembly.Location);
      }
      if (!options.ReferencedAssemblies.Contains(this.ComponentType.Assembly.Location))
        options.ReferencedAssemblies.Add(this.ComponentType.Assembly.Location);
      foreach (AssemblyName referencedAssembly in this.ComponentType.Assembly.GetReferencedAssemblies())
      {
        Assembly assembly = Assembly.Load(referencedAssembly);
        if (!options.ReferencedAssemblies.Contains(assembly.Location))
          options.ReferencedAssemblies.Add(assembly.Location);
      }
      CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(options, stringWriter.GetStringBuilder().ToString());
      if (compilerResults.Errors.Count > 0)
      {
        Exception innerException = (Exception) null;
        foreach (CompilerError error in (CollectionBase) compilerResults.Errors)
        {
          if (!error.IsWarning)
            innerException = innerException != null ? new Exception(string.Format("Copiler error:\r\n{0}: {1}", (object) error.ErrorNumber, (object) error.ErrorText), innerException) : new Exception(string.Format("Copiler error:\r\n{0}: {1}", (object) error.ErrorNumber, (object) error.ErrorText));
        }
        if (innerException != null)
          throw innerException;
      }
      PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor) compilerResults.CompiledAssembly.GetType("DynamicProperties." + str3).GetMethod(nameof (Create), BindingFlags.Static | BindingFlags.Public).Invoke((object) null, new object[0]);
      DynamicPropertyCreator._properties.Add(key, propertyDescriptor2);
      return propertyDescriptor2;
    }
  }
}
