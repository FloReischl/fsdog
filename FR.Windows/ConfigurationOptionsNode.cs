// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.ConfigurationOptionsNode
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class ConfigurationOptionsNode : IOptionsNode
  {
    private string _name;
    private Control _propertiesControl;
    internal List<ConfigurationOptionsNode> _nodes;
    internal List<ConfigurationOptionsProperty> _properties;

    public ConfigurationOptionsNode(string name)
    {
      this._name = name;
      this._nodes = new List<ConfigurationOptionsNode>();
      this._properties = new List<ConfigurationOptionsProperty>();
    }

    public string Name
    {
      [DebuggerNonUserCode] get => this._name;
    }

    public Control EditControl
    {
      [DebuggerNonUserCode] get => this._propertiesControl;
      [DebuggerNonUserCode] set => this._propertiesControl = value;
    }

    public ConfigurationOptionsNode[] Nodes => this._nodes.ToArray();

    public ConfigurationOptionsProperty[] Properties => this._properties.ToArray();

    string IOptionsNode.Name
    {
      [DebuggerNonUserCode] get => this.Name;
    }

    Control IOptionsNode.EditControl
    {
      [DebuggerNonUserCode] get => this.EditControl;
    }

    IOptionsNode[] IOptionsNode.Nodes
    {
      [DebuggerNonUserCode] get => (IOptionsNode[]) this.Nodes;
    }

    IOptionsProperty[] IOptionsNode.Properties
    {
      [DebuggerNonUserCode] get => (IOptionsProperty[]) this.Properties;
    }
  }
}
