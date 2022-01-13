// Decompiled with JetBrains decompiler
// Type: FR.Configuration.ConfigurationFilePropertyDoesNotExistException
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;

namespace FR.Configuration
{
  public class ConfigurationFilePropertyDoesNotExistException : Exception
  {
    public ConfigurationFilePropertyDoesNotExistException(string path, string name)
      : base(string.Format("Property {0} not found at {1}", (object) name, (object) path))
    {
    }
  }
}
