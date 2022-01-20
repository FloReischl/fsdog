// Decompiled with JetBrains decompiler
// Type: FsDog.CmdToolsOpenConfigFile
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Configuration;
using FR.IO;
using FsDog.Configuration;
using System.Windows.Forms;

namespace FsDog.Commands {
    public class CmdToolsOpenConfigFile : CmdFsDogIntern {
        public override void Execute() {
            //if (!(this.Application.ConfigurationSource is ConfigurationFile configurationSource)) {
            //    int num = (int)MessageBox.Show(string.Format("Cannot open config from type {0}", (object)this.Application.ConfigurationSource.GetType()));
            //}
            //else
            //    FileHelper.ShellExecute(configurationSource.FileName);
            FileHelper.ShellExecute(FsDogConfig.FileName);
        }
    }
}
