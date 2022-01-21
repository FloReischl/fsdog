using FR.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FsDog.Commands {
    class CmdAppSaveConfig : CommandBase {
        public override void Execute() {
            FsApp.Instance.Config.Save();
        }
    }
}
