// Decompiled with JetBrains decompiler
// Type: FsDog.CommandHelper
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Configuration;
using FR.Windows.Forms;
using FR.Windows.Forms.Commands;
using FsDog.Commands.ScriptsApps;
using FsDog.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace FsDog.Commands {
    internal static class CommandHelper {
        public const string ApplicationsMenuName = "Applications";
        public const string ScriptsMenuName = "Skripts";
        private static List<ToolStrip> _appsToolStrips;
        private static List<ToolStrip> _scriptsToolStrips;

        private static OptionsConfig OptionsConfig { get => FsApp.Instance.Config.Options; }

        public static void GetApplicationsToolStrips(out ToolStrip shift, out ToolStrip ctrl) {
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

            if (commandToolItem1.Items.Count != 0 && OptionsConfig.Menus.Applications.ShowCtrlToolStrip) {
                ToolStrip toolStrip = commandToolItem1.CreateToolStrip();
                CommandHelper.ApplyDisplaySettings(toolStrip, OptionsConfig.Menus.Applications.ShowCtrlImage, OptionsConfig.Menus.Applications.ShowCtrlName, OptionsConfig.Menus.Applications.ShowCtrlShortcut);
                toolStrip.Items.Insert(0, (ToolStripItem)new ToolStripLabel("Ctrl"));
                appearance.ApplyToToolStrip(toolStrip);
                CommandHelper._appsToolStrips.Add(toolStrip);
                ctrl = toolStrip;
            }
            else {
                ctrl = null;
            }

            if (commandToolItem2.Items.Count != 0 && OptionsConfig.Menus.Applications.ShowShiftToolStrip) {
                ToolStrip toolStrip = commandToolItem2.CreateToolStrip();
                CommandHelper.ApplyDisplaySettings(toolStrip, OptionsConfig.Menus.Applications.ShowShiftImage, OptionsConfig.Menus.Applications.ShowShiftName, OptionsConfig.Menus.Applications.ShowShiftShortcut);
                toolStrip.Items.Insert(0, (ToolStripItem)new ToolStripLabel("Shift"));
                appearance.ApplyToToolStrip(toolStrip);
                CommandHelper._appsToolStrips.Add(toolStrip);
                shift = toolStrip;
            }
            else {
                shift = null;
            }
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
            return applicationsToolItem;
        }

        public static void GetScriptsToolStrips(out ToolStrip shift, out ToolStrip ctrl) {
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
            if (commandToolItem1.Items.Count != 0 && OptionsConfig.Menus.Scripts.ShowCtrlToolStrip) {
                ToolStrip toolStrip = commandToolItem1.CreateToolStrip();
                CommandHelper.ApplyDisplaySettings(toolStrip, OptionsConfig.Menus.Scripts.ShowCtrlImage, OptionsConfig.Menus.Scripts.ShowCtrlName, OptionsConfig.Menus.Scripts.ShowCtrlShortcut);
                toolStrip.Items.Insert(0, (ToolStripItem)new ToolStripLabel("Ctrl"));
                appearance.ApplyToToolStrip(toolStrip);
                CommandHelper._scriptsToolStrips.Add(toolStrip);
                ctrl = toolStrip;
            }
            else {
                ctrl = null;
            }

            if (commandToolItem2.Items.Count != 0 && OptionsConfig.Menus.Scripts.ShowShiftToolStrip) {
                ToolStrip toolStrip = commandToolItem2.CreateToolStrip();
                CommandHelper.ApplyDisplaySettings(toolStrip, OptionsConfig.Menus.Scripts.ShowShiftImage, OptionsConfig.Menus.Scripts.ShowShiftName, OptionsConfig.Menus.Scripts.ShowShiftShortcut);
                toolStrip.Items.Insert(0, (ToolStripItem)new ToolStripLabel("Shift"));
                appearance.ApplyToToolStrip(toolStrip);
                CommandHelper._scriptsToolStrips.Add(toolStrip);
                shift = toolStrip;
            }
            else {
                shift = null;
            }
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

            return scriptsToolItem;
        }

        public static List<CommandInfo> GetInfos() {
            var config = FsApp.Instance.Config;
            return config.Commands.Select(cmd => cmd.ToCommandInfo()).ToList();
        }

        public static List<ScriptingHostConfiguration> GetScriptingHosts() {
            return FsApp.Instance.Config.Scripting.Hosts.Select(host => host.ToScriptingHost()).ToList();
        }

        public static void SetScriptingHostsToConfig(IList<ScriptingHostConfiguration> hosts) {
            var config = FsApp.Instance.Config;
            config.Scripting.Hosts.Clear();
            config.Scripting.Hosts.AddRange(hosts.Select(host => HostsConfig.FromHost(host)));
        }

        public static void SetToConfig(IList<CommandInfo> infos) {
            var config = FsApp.Instance.Config;
            config.Commands.Clear();
            config.Commands.AddRange(infos.Select(info => CommandConfig.FromInfo(info)));
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
