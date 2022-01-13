// Decompiled with JetBrains decompiler
// Type: FsDog.CmdCommandBase
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FsDog.Commands {
    internal abstract class CmdCommandBase : CmdFsDogIntern {
        protected bool EnsureRequirements(CommandInfo info) {
            StringBuilder stringBuilder = new StringBuilder(info.Arguments);
            return (this.SelectedItems.Length != 0 || !info.Arguments.Contains("[f]") && !info.Arguments.Contains("[ff]")) && (this.ParentDirectory != null || !info.Arguments.Contains("[d]")) && (this.GetSecondFileSystemInfo() != null || !info.Arguments.Contains("[f2]"));
        }

        protected string GetArguments(CommandInfo info) {
            StringBuilder stringBuilder = new StringBuilder(info.Arguments, Math.Max(info.Arguments.Length, 256));
            if (info.Arguments.Contains("[f]")) {
                if (ConsoleHelper.ContainsSpecialQuoteKeys(this.SelectedItems[0].FullName))
                    stringBuilder.Replace("[f]", string.Format("\"{0}\"", (object)this.SelectedItems[0].FullName));
                else
                    stringBuilder.Replace("[f]", this.SelectedItems[0].FullName);
            }
            if (info.Arguments.Contains("[f2]")) {
                FileSystemInfo secondFileSystemInfo = this.GetSecondFileSystemInfo();
                if (ConsoleHelper.ContainsSpecialQuoteKeys(secondFileSystemInfo.FullName))
                    stringBuilder.Replace("[f2]", string.Format("\"{0}\"", (object)secondFileSystemInfo.FullName));
                else
                    stringBuilder.Replace("[f2]", secondFileSystemInfo.FullName);
            }
            if (info.Arguments.Contains("[ff]")) {
                bool flag = true;
                foreach (FileSystemInfo selectedItem in this.SelectedItems) {
                    if (flag) {
                        if (ConsoleHelper.ContainsSpecialQuoteKeys(selectedItem.FullName))
                            stringBuilder.Replace("[ff]", string.Format("\"{0}\"[ff]", (object)selectedItem.FullName));
                        else
                            stringBuilder.Replace("[ff]", string.Format("{0}[ff]", (object)selectedItem.FullName));
                        flag = false;
                    }
                    else if (ConsoleHelper.ContainsSpecialQuoteKeys(selectedItem.FullName))
                        stringBuilder.Replace("[ff]", string.Format(" \"{0}\"[ff]", (object)selectedItem.FullName));
                    else
                        stringBuilder.Replace("[ff]", string.Format(" {0}[ff]", (object)selectedItem.FullName));
                }
                stringBuilder.Replace("[ff]", "");
            }
            if (info.Arguments.Contains("[d]")) {
                if (ConsoleHelper.ContainsSpecialQuoteKeys(this.ParentDirectory.FullName))
                    stringBuilder.Replace("[d]", string.Format("\"{0}\"", (object)this.ParentDirectory.FullName));
                else
                    stringBuilder.Replace("[d]", this.ParentDirectory.FullName);
            }
            return stringBuilder.ToString();
        }

        protected FileSystemInfo GetSecondFileSystemInfo() {
            List<FileSystemInfo> selectedSystemInfos1 = this.CurrentDetailView.GetSelectedSystemInfos();
            if (selectedSystemInfos1.Count > 1)
                return selectedSystemInfos1[1];
            List<FileSystemInfo> selectedSystemInfos2 = this.OtherDetailView.GetSelectedSystemInfos();
            return selectedSystemInfos2.Count != 0 ? selectedSystemInfos2[0] : (FileSystemInfo)null;
        }
    }
}
