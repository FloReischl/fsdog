// Decompiled with JetBrains decompiler
// Type: FR.EmbeddedResourceManager
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace FR
{
  public class EmbeddedResourceManager
  {
    private Assembly _assembly;
    private Dictionary<string, object> _resources;
    private object _missingItem;
    private string _defaultMask;

    public EmbeddedResourceManager(Type type)
    {
      this._assembly = type.Assembly;
      this._resources = new Dictionary<string, object>();
      this._missingItem = new object();
    }

    public string DefaultMask
    {
      [DebuggerNonUserCode] get => this._defaultMask;
      [DebuggerNonUserCode] set => this._defaultMask = value;
    }

    public string GetString(string name, bool useDefaultMask)
    {
      StreamReader streamReader = new StreamReader(this.GetStream(name, useDefaultMask));
      string end = streamReader.ReadToEnd();
      streamReader.Dispose();
      return end;
    }

    public Stream GetStream(string name, bool useDefaultMask)
    {
      if (useDefaultMask)
        name = string.Format(this.DefaultMask, (object) name);
      Stream stream;
      if (this._resources.ContainsKey(name))
      {
        object resource = this._resources[name];
        stream = resource != this._missingItem ? (Stream) resource : (Stream) null;
      }
      else
      {
        stream = this._assembly.GetManifestResourceStream(name);
        if (stream != null)
          this._resources.Add(name, (object) stream);
        else
          this._resources.Add(name, this._missingItem);
      }
      return stream;
    }
  }
}
