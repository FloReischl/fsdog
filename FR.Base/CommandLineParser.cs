// Decompiled with JetBrains decompiler
// Type: FR.Configuration.CommandLineParser
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.Globalization;

namespace FR {
    public class CommandLineParser {
        private CommandLineParser() {
        }

        public static bool GetCommandLineArgument(string[] args, string arg, bool bCaseSensitive) {
            foreach (string str in args) {
                if (str.Length == arg.Length && str.StartsWith(arg, !bCaseSensitive, CultureInfo.CurrentCulture))
                    return true;
            }
            return false;
        }

        public static string GetCommandLineValue(string[] args, string arg, bool bCaseSensitive) {
            for (int index = 0; index < args.Length; ++index) {
                string str = (string)args.GetValue(index);
                if (str.StartsWith(arg, !bCaseSensitive, CultureInfo.CurrentCulture)) {
                    if (str.Length == arg.Length && index < args.Length - 1)
                        return (string)args.GetValue(index + 1);
                    if (str.Length == arg.Length && index == args.Length - 1)
                        return "";
                    if (str.Length > arg.Length)
                        return str.Substring(arg.Length);
                }
            }
            return (string)null;
        }
    }
}
