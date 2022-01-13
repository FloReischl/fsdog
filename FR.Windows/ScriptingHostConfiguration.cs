// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.ScriptingHostConfiguration
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Collections;

namespace FR.Windows.Forms
{
  public class ScriptingHostConfiguration
  {
    public ScriptingHostConfiguration() => this.Configuration = new DataContext();

    internal ScriptingHostConfiguration(ScriptingHostConfiguration other)
    {
      this.Configuration = other.Configuration;
      this.ExecutionLocation = other.ExecutionLocation;
      this.Location = other.Location;
      this.Arguments = other.Arguments;
      this.Name = other.Name;
    }

    public DataContext Configuration { get; private set; }

    public ScriptExecutionLocation ExecutionLocation { get; set; }

    public string Location { get; set; }

    public string Arguments { get; set; }

    public string Name { get; set; }
  }
}
