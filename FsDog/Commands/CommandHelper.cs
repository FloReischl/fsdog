// Decompiled with JetBrains decompiler
// Type: FsDog.CommandHelper
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Configuration;
using FR.Windows.Forms;
using FR.Windows.Forms.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace FsDog.Commands {
    internal static class CommandHelper {
        public const string ApplicationsMenuName = "Applications";
        public const string ScriptsMenuName = "Skripts";
        private static List<ToolStrip> _appsToolStrips;
        private static List<ToolStrip> _scriptsToolStrips;

        public static IList<ToolStrip> GetApplicationsToolStrips() {
            if (CommandHelper._appsToolStrips != null) {
                foreach (ToolStrip appsToolStrip in CommandHelper._appsToolStrips) {
                    if (appsToolStrip.Parent != null)
                        appsToolStrip.Parent.Controls.Remove((Control)appsToolStrip);
                    while (appsToolStrip.Items.Count != 0) {
                        ToolStripItem toolStripItem = appsToolStrip.Items[0];
                        appsToolStrip.Items.Remove(toolStripItem);
                        if (!toolStripItem.IsDisposed)
                            toolStripItem.Dispose();
                    }
                    appsToolStrip.Dispose();
                }
                CommandHelper._appsToolStrips.Clear();
            }
            FsApp instance = FsApp.Instance;
            CommandToolItem applicationsToolItem = CommandHelper.GetApplicationsToolItem();
            CommandToolItem commandToolItem1 = new CommandToolItem("ApplicationsCtrl");
            commandToolItem1.Name = "ApplicationsCtrl";
            CommandToolItem commandToolItem2 = new CommandToolItem("ApplicationsShift");
            commandToolItem2.Name = "ApplicationsShift";
            foreach (CommandToolItem commandToolItem3 in applicationsToolItem.Items) {
                CommandInfo commandInfo = (CommandInfo)commandToolItem3.CommandContext[(object)"CommandInfo"];
                if (commandInfo != null) {
                    if ((commandInfo.Key & Keys.Control) == Keys.Control) {
                        commandToolItem1.Items.Add(commandToolItem3);
                        commandToolItem1.Items.Add(new CommandToolItem("-"));
                    }
                    if ((commandInfo.Key & Keys.Shift) == Keys.Shift) {
                        commandToolItem2.Items.Add(commandToolItem3);
                        commandToolItem2.Items.Add(new CommandToolItem("-"));
                    }
                }
            }

            AppearanceProvider appearance = new AppearanceProvider();

            if (CommandHelper._appsToolStrips == null)
                CommandHelper._appsToolStrips = new List<ToolStrip>();

            if (commandToolItem1.Items.Count != 0 && instance.Options.MenusApplications.ShowCtrlToolStrip) {
                ToolStrip toolStrip = commandToolItem1.CreateToolStrip();
                CommandHelper.ApplyDisplaySettings(toolStrip, instance.Options.MenusApplications.ShowCtrlImage, instance.Options.MenusApplications.ShowCtrlName, instance.Options.MenusApplications.ShowCtrlShortcut);
                toolStrip.Items.Insert(0, (ToolStripItem)new ToolStripLabel("Ctrl"));
                appearance.ApplyToToolStrip(toolStrip);
                CommandHelper._appsToolStrips.Add(toolStrip);
            }

            if (commandToolItem2.Items.Count != 0 && instance.Options.MenusApplications.ShowShiftToolStrip) {
                ToolStrip toolStrip = commandToolItem2.CreateToolStrip();
                CommandHelper.ApplyDisplaySettings(toolStrip, instance.Options.MenusApplications.ShowShiftImage, instance.Options.MenusApplications.ShowShiftName, instance.Options.MenusApplications.ShowShiftShortcut);
                toolStrip.Items.Insert(0, (ToolStripItem)new ToolStripLabel("Shift"));
                appearance.ApplyToToolStrip(toolStrip);
                CommandHelper._appsToolStrips.Add(toolStrip);
            }

            return (IList<ToolStrip>)CommandHelper._appsToolStrips;
        }

        public static CommandToolItem GetApplicationsToolItem() {
            FsApp instance = FsApp.Instance;
            CommandToolItem applicationsToolItem = new CommandToolItem("&Applications");
            applicationsToolItem.Name = "Applications";
            foreach (CommandInfo info in CommandHelper.GetInfos()) {
                if (info.CommandType == CommandType.Application) {
                    Image image = info.GetImage();
                    string toolTipText = string.Format("{0} ({1})", (object)info.Name, (object)info.GetShortcutText());
                    applicationsToolItem.Items.Add(new CommandToolItem(info.Name, typeof(CmdApplicationExecute), image, toolTipText, info.Key) {
                        CommandContext = {
                            { "CommandInfo", info }
                        }
                    });
                }
            }
            //CommandToolItem commandToolItem = new CommandToolItem("-");
            //applicationsToolItem.Items.Add(commandToolItem);
            //applicationsToolItem.Items.Add(new CommandToolItem("&Edit Applications and Scripts", typeof(CmdCommandsEdit)) {
            //    ShowNeverToolStrip = true
            //});
            return applicationsToolItem;
        }

        public static IList<ToolStrip> GetScriptsToolStrips() {
            if (CommandHelper._scriptsToolStrips != null) {
                foreach (ToolStrip scriptsToolStrip in CommandHelper._scriptsToolStrips) {
                    if (scriptsToolStrip.Parent != null)
                        scriptsToolStrip.Parent.Controls.Remove((Control)scriptsToolStrip);
                    while (scriptsToolStrip.Items.Count != 0) {
                        ToolStripItem toolStripItem = scriptsToolStrip.Items[0];
                        scriptsToolStrip.Items.Remove(toolStripItem);
                        if (!toolStripItem.IsDisposed)
                            toolStripItem.Dispose();
                    }
                    scriptsToolStrip.Dispose();
                }
                CommandHelper._scriptsToolStrips.Clear();
            }
            FsApp instance = FsApp.Instance;
            CommandToolItem scriptsToolItem = CommandHelper.GetScriptsToolItem();
            CommandToolItem commandToolItem1 = new CommandToolItem("ScriptsCtrl");
            commandToolItem1.Name = "ScriptsCtrl";
            CommandToolItem commandToolItem2 = new CommandToolItem("ScriptsShift");
            commandToolItem2.Name = "ScriptsShift";
            foreach (CommandToolItem commandToolItem3 in scriptsToolItem.Items) {
                CommandInfo commandInfo = (CommandInfo)commandToolItem3.CommandContext[(object)"CommandInfo"];
                if (commandInfo != null) {
                    if ((commandInfo.Key & Keys.Control) == Keys.Control) {
                        commandToolItem1.Items.Add(commandToolItem3);
                        commandToolItem1.Items.Add(new CommandToolItem("-"));
                    }
                    if ((commandInfo.Key & Keys.Shift) == Keys.Shift) {
                        commandToolItem2.Items.Add(commandToolItem3);
                        commandToolItem2.Items.Add(new CommandToolItem("-"));
                    }
                }
            }

            AppearanceProvider appearance = new AppearanceProvider();

            if (CommandHelper._scriptsToolStrips == null)
                CommandHelper._scriptsToolStrips = new List<ToolStrip>();
            if (commandToolItem1.Items.Count != 0 && instance.Options.MenusScripts.ShowCtrlToolStrip) {
                ToolStrip toolStrip = commandToolItem1.CreateToolStrip();
                CommandHelper.ApplyDisplaySettings(toolStrip, instance.Options.MenusScripts.ShowCtrlImage, instance.Options.MenusScripts.ShowCtrlName, instance.Options.MenusScripts.ShowCtrlShortcut);
                toolStrip.Items.Insert(0, (ToolStripItem)new ToolStripLabel("Ctrl"));
                appearance.ApplyToToolStrip(toolStrip);
                CommandHelper._scriptsToolStrips.Add(toolStrip);
            }

            if (commandToolItem2.Items.Count != 0 && instance.Options.MenusScripts.ShowShiftToolStrip) {
                ToolStrip toolStrip = commandToolItem2.CreateToolStrip();
                CommandHelper.ApplyDisplaySettings(toolStrip, instance.Options.MenusScripts.ShowShiftImage, instance.Options.MenusScripts.ShowShiftName, instance.Options.MenusScripts.ShowShiftShortcut);
                toolStrip.Items.Insert(0, (ToolStripItem)new ToolStripLabel("Shift"));
                appearance.ApplyToToolStrip(toolStrip);
                CommandHelper._scriptsToolStrips.Add(toolStrip);
            }

            return (IList<ToolStrip>)CommandHelper._scriptsToolStrips;
        }

        public static CommandToolItem GetScriptsToolItem() {
            FsApp instance = FsApp.Instance;
            CommandToolItem scriptsToolItem = new CommandToolItem("&Scripts");
            scriptsToolItem.Name = "Skripts";
            foreach (CommandInfo info in CommandHelper.GetInfos()) {
                if (info.CommandType == CommandType.Script) {
                    Image image = info.GetImage();
                    string toolTipText = string.Format("{0} ({1})", (object)info.Name, (object)info.GetShortcutText());
                    scriptsToolItem.Items.Add(new CommandToolItem(info.Name, typeof(CmdScriptExecute), image, toolTipText, info.Key) {
                        CommandContext = { { "CommandInfo", info } }
                    });
                }
            }
            //CommandToolItem commandToolItem = new CommandToolItem("-");
            //scriptsToolItem.Items.Add(commandToolItem);
            //scriptsToolItem.Items.Add(new CommandToolItem("&Edit Applications and Scripts", typeof(CmdCommandsEdit)) {
            //    ShowNeverToolStrip = true
            //});
            //scriptsToolItem.Items.Add(new CommandToolItem("Configure Scripting Hosts", typeof(CmdScriptConfigureHosts)) {
            //    ShowNeverToolStrip = true
            //});
            return scriptsToolItem;
        }

        public static List<CommandInfo> GetInfos() {
            List<CommandInfo> infos = new List<CommandInfo>();
            foreach (IConfigurationProperty subProperty in FsApp.Instance.ConfigurationSource.GetProperty(".", "Commands", true).GetSubProperties("Command")) {
                Keys keys = (Keys)new KeysConverter().ConvertFromString(subProperty["Key"].ToString());
                infos.Add(new CommandInfo() {
                    Key = keys != Keys.None ? keys : (Keys?)null,
                    Name = subProperty["Name"].ToString(),
                    Command = subProperty["Command"].ToString(),
                    Arguments = subProperty["Arguments"].ToString(""),
                    CommandType = (CommandType)Enum.Parse(typeof(CommandType), subProperty.GetSubProperty("CommandType", true).ToString(CommandType.Application.ToString())),
                    ScriptingHost = subProperty.GetSubProperty("ScriptingHost", true).ToString("")
                });
            }
            return infos;
        }

        public static List<ScriptingHostConfiguration> GetScriptingHosts() {
            List<ScriptingHostConfiguration> scriptingHosts = new List<ScriptingHostConfiguration>();
            foreach (IConfigurationProperty subProperty in FsApp.Instance.ConfigurationSource.GetProperty("Scripting", "Hosts", true).GetSubProperties("Item"))
                scriptingHosts.Add(new ScriptingHostConfiguration() {
                    Name = subProperty["Name"].ToString(),
                    Location = subProperty["Location"].ToString(),
                    Arguments = subProperty["Arguments"].ToString(""),
                    ExecutionLocation = (ScriptExecutionLocation)Enum.Parse(typeof(ScriptExecutionLocation), subProperty["ExecuteAt"].ToString())
                });
            return scriptingHosts;
        }

        public static void SetScriptingHostsToConfig(IList<ScriptingHostConfiguration> hosts) {
            IConfigurationProperty property = FsApp.Instance.ConfigurationSource.GetProperty("Scripting", "Hosts", true);
            while (property.ExistsSubProperty("Item"))
                property["Item"].Delete();
            foreach (ScriptingHostConfiguration host in (IEnumerable<ScriptingHostConfiguration>)hosts) {
                IConfigurationProperty configurationProperty = property.AddSubProperty("Item");
                configurationProperty.GetSubProperty("Name", true).Set(host.Name);
                configurationProperty.GetSubProperty("Location", true).Set(host.Location);
                configurationProperty.GetSubProperty("Arguments", true).Set(host.Arguments);
                configurationProperty.GetSubProperty("ExecuteAt", true).Set(host.ExecutionLocation.ToString());
            }
        }

        public static void SetToConfig(IList<CommandInfo> infos) {
            IConfigurationProperty property = FsApp.Instance.ConfigurationSource.GetProperty(".", "Commands");
            foreach (IConfigurationProperty subProperty in property.GetSubProperties("Command"))
                subProperty.Delete();
            foreach (CommandInfo info in (IEnumerable<CommandInfo>)infos) {
                IConfigurationProperty configurationProperty = property.AddSubProperty("Command");
                configurationProperty.GetSubProperty("Key", true).Set((info.Key ?? Keys.None).ToString());
                configurationProperty.GetSubProperty("CommandType", true).Set(info.CommandType.ToString());
                configurationProperty.GetSubProperty("Name", true).Set(info.Name);
                configurationProperty.GetSubProperty("Command", true).Set(info.Command);
                configurationProperty.GetSubProperty("Arguments", true).Set(info.Arguments);
                if (info.CommandType == CommandType.Script)
                    configurationProperty.GetSubProperty("ScriptingHost", true).Set(info.ScriptingHost);
            }
        }

        private static void ApplyDisplaySettings(
          ToolStrip ts,
          bool showImage,
          bool showText,
          bool showShortcut) {
            foreach (ToolStripItem toolStripItem in (ArrangedElementCollection)ts.Items) {
                if (toolStripItem is ToolStripMenuItem toolStripMenuItem && toolStripMenuItem.Tag is CommandToolItem tag && tag.CommandContext.ContainsKey((object)"CommandInfo")) {
                    CommandInfo commandInfo = (CommandInfo)tag.CommandContext[(object)"CommandInfo"];
                    if (showImage) {
                        toolStripMenuItem.ImageAlign = ContentAlignment.MiddleRight;
                        toolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
                    }
                    else
                        toolStripMenuItem.Image = (Image)null;
                    if (showText) {
                        toolStripMenuItem.TextAlign = ContentAlignment.MiddleLeft;
                        toolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    }
                    else
                        toolStripMenuItem.Text = string.Empty;
                    if (showShortcut) {
                        if (string.IsNullOrEmpty(toolStripMenuItem.Text))
                            toolStripMenuItem.Text = commandInfo.GetShortcutText();
                        else
                            toolStripMenuItem.Text = string.Format("{0} ({1})", (object)toolStripMenuItem.Text, (object)commandInfo.GetShortcutText());
                    }
                    if (showImage && (showText || showShortcut)) {
                        toolStripMenuItem.TextImageRelation = TextImageRelation.TextBeforeImage;
                        toolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                    }
                }
            }
        }
    }
}
