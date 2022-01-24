// Decompiled with JetBrains decompiler
// Type: FR.ConsoleHelper
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace FR {
    public static class ConsoleHelper {
        public static ReadOnlyCollection<ConsoleKey> AlphaKeys {
            get {
                List<ConsoleKey> list = new List<ConsoleKey>();
                list.AddRange((IEnumerable<ConsoleKey>)new ConsoleKey[26]
                {
          ConsoleKey.A,
          ConsoleKey.B,
          ConsoleKey.C,
          ConsoleKey.D,
          ConsoleKey.E,
          ConsoleKey.F,
          ConsoleKey.G,
          ConsoleKey.H,
          ConsoleKey.I,
          ConsoleKey.J,
          ConsoleKey.K,
          ConsoleKey.L,
          ConsoleKey.M,
          ConsoleKey.N,
          ConsoleKey.O,
          ConsoleKey.P,
          ConsoleKey.Q,
          ConsoleKey.R,
          ConsoleKey.S,
          ConsoleKey.T,
          ConsoleKey.U,
          ConsoleKey.V,
          ConsoleKey.W,
          ConsoleKey.X,
          ConsoleKey.Y,
          ConsoleKey.Z
                });
                return new ReadOnlyCollection<ConsoleKey>((IList<ConsoleKey>)list);
            }
        }

        public static ReadOnlyCollection<ConsoleKey> NumericKeys {
            get {
                List<ConsoleKey> list = new List<ConsoleKey>();
                list.AddRange((IEnumerable<ConsoleKey>)new ConsoleKey[10]
                {
          ConsoleKey.NumPad0,
          ConsoleKey.NumPad1,
          ConsoleKey.NumPad2,
          ConsoleKey.NumPad3,
          ConsoleKey.NumPad4,
          ConsoleKey.NumPad5,
          ConsoleKey.NumPad6,
          ConsoleKey.NumPad7,
          ConsoleKey.NumPad8,
          ConsoleKey.NumPad9
                });
                return new ReadOnlyCollection<ConsoleKey>((IList<ConsoleKey>)list);
            }
        }

        public static ReadOnlyCollection<ConsoleKey> AlphaNumericKeys {
            get {
                List<ConsoleKey> list = new List<ConsoleKey>();
                list.AddRange((IEnumerable<ConsoleKey>)ConsoleHelper.AlphaKeys);
                list.AddRange((IEnumerable<ConsoleKey>)ConsoleHelper.NumericKeys);
                return new ReadOnlyCollection<ConsoleKey>((IList<ConsoleKey>)list);
            }
        }

        public static IntPtr Handle {
            get {
                Process currentProcess = Process.GetCurrentProcess();
                return currentProcess != null ? currentProcess.MainWindowHandle : new IntPtr(0);
            }
        }

        public static bool ContainsSpecialQuoteKeys(string text) {
            foreach (int find in text.ToCharArray()) {
                if (BaseHelper.InList((char)find, ' ', '&', '(', ')', '[', ']', '{', '}', '^', '=', ';', '!', '\'', '+', ',', '`', '~'))
                    return true;
            }
            return false;
        }

        public static ConsoleKeyInfo ReadKeySpecial(
          bool intercept,
          IEnumerable<ConsoleKey> specialKeys) {
            ConsoleKeyInfo consoleKeyInfo1 = new ConsoleKeyInfo(char.MinValue, ConsoleKey.A, false, false, false);
            while (consoleKeyInfo1.KeyChar == char.MinValue && consoleKeyInfo1.Key == ConsoleKey.A) {
                ConsoleKeyInfo consoleKeyInfo2 = Console.ReadKey(true);
                if (specialKeys == null) {
                    consoleKeyInfo1 = consoleKeyInfo2;
                }
                else {
                    foreach (ConsoleKey specialKey in specialKeys) {
                        if (specialKey == consoleKeyInfo2.Key)
                            consoleKeyInfo1 = consoleKeyInfo2;
                    }
                }
                if (consoleKeyInfo1.KeyChar != char.MinValue && consoleKeyInfo1.Key == ConsoleKey.A && !intercept)
                    Console.Write(consoleKeyInfo1.KeyChar);
            }
            return consoleKeyInfo1;
        }

        public static string ReadLineSpecial(IEnumerable<ConsoleKey> specialKeys) {
            string str = "";
            List<ConsoleKey> specialKeys1 = new List<ConsoleKey>();
            specialKeys1.AddRange(specialKeys);
            if (!specialKeys1.Contains(ConsoleKey.Backspace))
                specialKeys1.Add(ConsoleKey.Backspace);
            if (!specialKeys1.Contains(ConsoleKey.Enter))
                specialKeys1.Add(ConsoleKey.Enter);
            while (true) {
                ConsoleKeyInfo consoleKeyInfo;
                do {
                    consoleKeyInfo = ConsoleHelper.ReadKeySpecial(true, (IEnumerable<ConsoleKey>)specialKeys1);
                    if (consoleKeyInfo.Key == ConsoleKey.Backspace && str.Length != 0) {
                        --Console.CursorLeft;
                        Console.Write(' ');
                        --Console.CursorLeft;
                        str = str.Substring(0, str.Length - 1);
                    }
                    else if (consoleKeyInfo.Key == ConsoleKey.Enter)
                        goto label_9;
                }
                while (consoleKeyInfo.KeyChar == char.MinValue || consoleKeyInfo.KeyChar == '\b');
                Console.Write(consoleKeyInfo.KeyChar);
                str += consoleKeyInfo.KeyChar.ToString();
            }
        label_9:
            return str;
        }
    }
}
