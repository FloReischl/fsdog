// Decompiled with JetBrains decompiler
// Type: FsDog.CmdScriptExecute
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR;
using FR.Commands;
using FR.Windows.Forms;
using System;
using System.Diagnostics;
using System.IO;

namespace FsDog.Commands {
    internal class CmdScriptExecute : CmdCommandBase {
        public override void Execute() {
            try {
                FsApp instance = FsApp.Instance;
                CommandInfo info = (CommandInfo)Context[(object)"CommandInfo"];
                if (!EnsureRequirements(info)) {
                    ExecutionState = CommandExecutionState.Canceled;
                }
                else {
                    ExecuteScript(instance, info);
                    ExecutionState = CommandExecutionState.Ok;
                }
            }
            catch (Exception ex) {
                ExecutionState = CommandExecutionState.Error;
                throw ex;
            }
        }

        private void ExecuteScript(FsApp instance, CommandInfo info) {
            Process p = CreateProcess(instance, info);
            p.Start();

            string errorMessage = null;
            var errorHandler = new DataReceivedEventHandler((sender, e) => errorMessage = e.Data);
            p.ErrorDataReceived += errorHandler;

            p.WaitForExit();

            p.ErrorDataReceived -= errorHandler;

            if (!string.IsNullOrEmpty(errorMessage)) {
                throw new ApplicationException($"Error while executing script {info.Command}:\r\n\r\n{errorMessage}");
            }
        }

        private Process CreateProcess(FsApp instance, CommandInfo info) {
            var arguments = this.GetArguments(info);
            ScriptingHostConfiguration scriptingHost = instance.ScriptingHosts[info.ScriptingHost];
            string str1 = !ConsoleHelper.ContainsSpecialQuoteKeys(info.Command) ? info.Command : string.Format("\"{0}\"", info.Command);
            string str2 = arguments.Length != 0 ? string.Format(scriptingHost.Arguments.Replace("[f]", "{0} {1}"), (object)str1, (object)arguments) : string.Format(scriptingHost.Arguments.Replace("[f]", "{0}"), (object)str1);
            var p = new Process() {
                StartInfo = {
                          FileName = scriptingHost.Location,
                          WorkingDirectory = GetWorkingDir(scriptingHost, info),
                          Arguments = str2
                        },
                EnableRaisingEvents = true
            };
            return p;
        }

        private void P_ErrorDataReceived(object sender, DataReceivedEventArgs e) {
            throw new NotImplementedException();
        }

        private string GetWorkingDir(ScriptingHostConfiguration scriptingHost, CommandInfo cmdInfo)
            => scriptingHost.ExecutionLocation != ScriptExecutionLocation.HostDirectory 
                ? Path.GetDirectoryName(cmdInfo.Command) 
                : Path.GetDirectoryName(scriptingHost.Location);
    }
}
