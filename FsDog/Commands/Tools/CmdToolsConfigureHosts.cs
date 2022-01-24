// Decompiled with JetBrains decompiler
// Type: FsDog.CmdScriptConfigureHosts
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Configuration;
using FR.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FsDog.Commands.Tools {
    public class CmdToolsConfigureHosts : CmdFsDogIntern {
        public override void Execute() {
            FormScriptingHostConfiguration hostConfiguration = new FormScriptingHostConfiguration();
            hostConfiguration.Hosts.AddRange((IEnumerable<ScriptingHostConfiguration>)this.Application.ScriptingHosts.Values);
            if (hostConfiguration.ShowDialog((IWin32Window)this.Application.MainForm) != DialogResult.OK)
                return;
            CommandHelper.SetScriptingHostsToConfig((IList<ScriptingHostConfiguration>)hostConfiguration.Hosts);
            this.Application.Config.Save();
            this.Application.ReloadScriptingHosts();
        }
    }
}
