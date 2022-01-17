// Decompiled with JetBrains decompiler
// Type: FsDog.CommandInfo
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FsDog.Commands {
    public class CommandInfo {
        public CommandInfo() {
        }

        public CommandInfo(CommandInfo other) {
            this.Key = other.Key;
            this.Name = other.Name;
            this.Command = other.Command;
            this.Arguments = other.Arguments;
            this.CommandType = other.CommandType;
            this.ScriptingHost = other.ScriptingHost;
        }

        public Keys? Key { get; set; }

        public string Name { get; set; }

        public string Command { get; set; }

        public string Arguments { get; set; }

        public CommandType CommandType { get; set; }

        public string ScriptingHost { get; set; }
        
        public string GetShortcutText() => this.Key?.ToString().Replace("|", "+").Replace(",", " +");

        public Image GetImage() => FsApp.Instance.GetFsiImage((FileSystemInfo)new FileInfo(this.Command));
    }
}
