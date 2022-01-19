// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.ConfigurationOptionsTree
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FR.Windows.Forms {
    public class ConfigurationOptionsTree : ConfigurationOptionsNode, IOptionsTree, IOptionsNode {
        private readonly Dictionary<string, List<IConfigurationProperty>> _shownProperties;

        public ConfigurationOptionsTree(string caption, string rootName)
          : base(rootName) {
            this.Caption = caption;
            this._nodes = new List<ConfigurationOptionsNode>();
            this._shownProperties = new Dictionary<string, List<IConfigurationProperty>>();
        }

        public string Caption { get; }

        public void ClearOptions() => this._nodes.Clear();

        public ConfigurationOptionsProperty AddOption(
          IConfigurationProperty property,
          string shownPath,
          string text) {
            return this.AddOption(property, shownPath, text, null);
        }

        public ConfigurationOptionsProperty AddOption(
          IConfigurationProperty property,
          string shownPath,
          string text,
          string description) {
            ConfigurationOptionsProperty configurationOptionsProperty = new ConfigurationOptionsProperty(property, text, description);
            AddOption(configurationOptionsProperty, shownPath);
            return configurationOptionsProperty;
        }

        public void AddOption(ConfigurationOptionsProperty option, string shownPath) {
            ConfigurationOptionsNode nodeForPath = this.GetNodeForPath(shownPath);
            nodeForPath._properties.Add(option);
        }

        private ConfigurationOptionsNode GetNodeForPath(string path) {
            if (string.IsNullOrEmpty(path))
                return (ConfigurationOptionsNode)this;
            string[] strArray = path.Split(new string[1] { "/" }, StringSplitOptions.None);
            ConfigurationOptionsNode nodeForPath = (ConfigurationOptionsNode)null;
            List<ConfigurationOptionsNode> nodes = this._nodes;
            foreach (string name in strArray) {
                ConfigurationOptionsNode configurationOptionsNode1 = (ConfigurationOptionsNode)null;
                foreach (ConfigurationOptionsNode configurationOptionsNode2 in nodes) {
                    if (configurationOptionsNode2.Name == name) {
                        configurationOptionsNode1 = configurationOptionsNode2;
                        nodes = configurationOptionsNode1._nodes;
                        break;
                    }
                }
                if (configurationOptionsNode1 == null) {
                    configurationOptionsNode1 = new ConfigurationOptionsNode(name);
                    nodes.Add(configurationOptionsNode1);
                    nodes = configurationOptionsNode1._nodes;
                }
                nodeForPath = configurationOptionsNode1;
            }
            return nodeForPath;
        }

        string IOptionsTree.Text {
            [DebuggerNonUserCode]
            get => this.Caption;
        }
    }
}
