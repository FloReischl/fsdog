// Decompiled with JetBrains decompiler
// Type: FR.Configuration.CommandLineArg
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;

namespace FR {
    public class CommandLineArg : EventArgs {
        public CommandLineArg(string originalValue) {
            this.Original = originalValue;
            this.Parse();
        }

        public string Argument { get; private set; }

        public string Value { get; private set; }

        public string Original { get; }

        private void Parse() {
            string[] strArray = this.Original.Split('=');
            this.Argument = "";
            this.Value = "";
            if (strArray.Length == 1)
                this.Argument = this.Original;
            else if (strArray.Length == 2) {
                this.Argument = strArray[0];
                this.Value = strArray[1];
            }
            else if (strArray.Length > 2) {
                this.Argument = strArray[0];
                this.Value = "";
                for (int index = 1; index < strArray.Length; ++index) {
                    if (this.Value.Length != 0)
                        this.Value += "=";
                    this.Value += strArray[index];
                }
            }
            if (!this.Argument.Substring(0, 1).Equals("-") && !this.Argument.Substring(0, 1).Equals("/"))
                return;
            this.Argument = this.Argument.Substring(1, this.Argument.Length - 1);
        }
    }
}
