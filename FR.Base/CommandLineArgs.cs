// Decompiled with JetBrains decompiler
// Type: FR.Configuration.CommandLineArgs
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Collections.Generic;
using System.Collections.Specialized;

namespace FR {
    public class CommandLineArgs : List<CommandLineArg> {
        private string[] _originalArguments;

        public string[] OriginalArguments {
            get => this._originalArguments;
            set => this.Parse(value);
        }

        public NameValueCollection pnvArguments { get; }

        public CommandLineArgs() => this.pnvArguments = new NameValueCollection();

        public CommandLineArgs(string[] args)
          : this() {
            this.Parse(args);
        }

        protected virtual void Parse() {
            this.Clear();
            this.pnvArguments.Clear();
            if (this._originalArguments == null)
                return;
            foreach (string originalArgument in this._originalArguments) {
                CommandLineArg commandLineArg = new CommandLineArg(originalArgument);
                this.Add(commandLineArg);
                this.pnvArguments.Add(commandLineArg.Argument, commandLineArg.Value);
            }
        }

        public virtual void Parse(string[] args) {
            this._originalArguments = args;
            this.Parse();
        }

        public CommandLineArg FindByArgument(string sArgument, bool bCaseSensitive) {
            foreach (CommandLineArg byArgument in (List<CommandLineArg>)this) {
                if (bCaseSensitive) {
                    if (byArgument.Argument == sArgument)
                        return byArgument;
                }
                else if (byArgument.Argument.ToLower() == sArgument.ToLower())
                    return byArgument;
            }
            return (CommandLineArg)null;
        }
    }
}
