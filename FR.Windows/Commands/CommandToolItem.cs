// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.Commands.CommandToolItem
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Collections;
using FR.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace FR.Windows.Forms.Commands {
    public class CommandToolItem {
        private static Dictionary<ToolStripItem, CommandToolItem> _toolItemToCommandItem;
        private static List<CommandToolItem> _temporaryNamed;
        //private static ToolStripMenuItem _currentMenuItem;

        public CommandToolItem(string text)
          : this(text, (System.Type)null, (Image)null, (string)null, Shortcut.None) {
        }

        public CommandToolItem(string text, System.Type commandType)
          : this(text, commandType, (Image)null, (string)null, Shortcut.None) {
        }

        public CommandToolItem(string text, System.Type commandType, Image image)
          : this(text, commandType, image, (string)null, Shortcut.None) {
        }

        public CommandToolItem(string text, System.Type commandType, Image image, string toolTipText)
          : this(text, commandType, image, toolTipText, Shortcut.None) {
        }

        public CommandToolItem(string text, System.Type commandType, Image image, Keys shortcut)
          : this(text, commandType, image, (string)null, shortcut) {
        }

        public CommandToolItem(string text, System.Type commandType, Image image, Shortcut shortcut)
          : this(text, commandType, image, (string)null, shortcut) {
        }

        public CommandToolItem(
          string text,
          System.Type commandType,
          Image image,
          string toolTipText,
          Keys? shortcut) {
            this.Text = text;
            this.CommandType = commandType;
            this.Image = image;
            this.ShowAlwaysContextMenu = false;
            this.ShowAlwaysMenuStrip = true;
            this.ShowAlwaysToolStrip = false;
            this.ShowNeverContextMenu = false;
            this.ShowNeverMenuStrip = false;
            this.ShowNeverToolStrip = false;
            this.ToolTipText = toolTipText;
            this.Shortcut = Shortcut.None;
            this.ShortcutKeys = shortcut ?? Keys.None;
            this.CustomToolStripItem = (ToolStripItem)null;
            CommandToolItem._toolItemToCommandItem = new Dictionary<ToolStripItem, CommandToolItem>();
            CommandToolItem._temporaryNamed = new List<CommandToolItem>();
        }

        public CommandToolItem(
          string text,
          System.Type commandType,
          Image image,
          string toolTipText,
          Shortcut shortcut) {
            this.Text = text;
            this.CommandType = commandType;
            this.Image = image;
            this.ShowAlwaysContextMenu = false;
            this.ShowAlwaysMenuStrip = true;
            this.ShowAlwaysToolStrip = false;
            this.ShowNeverContextMenu = false;
            this.ShowNeverMenuStrip = false;
            this.ShowNeverToolStrip = false;
            this.ToolTipText = toolTipText;
            this.Shortcut = shortcut;
            this.ShortcutKeys = Keys.None;
            this.CustomToolStripItem = (ToolStripItem)null;
            CommandToolItem._toolItemToCommandItem = new Dictionary<ToolStripItem, CommandToolItem>();
            CommandToolItem._temporaryNamed = new List<CommandToolItem>();
        }

        public CommandToolItem(ToolStripItem customToolStripItem)
          : this((string)null, (System.Type)null, (Image)null, (string)null, Shortcut.None) {
            this.CustomToolStripItem = customToolStripItem;
            this.ShowAlwaysContextMenu = false;
            this.ShowAlwaysMenuStrip = false;
            this.ShowAlwaysToolStrip = false;
            this.ShowNeverContextMenu = true;
            this.ShowNeverMenuStrip = true;
            this.ShowNeverToolStrip = true;
        }

        //public static ToolStripMenuItem CurrentMenuItem => CommandToolItem._currentMenuItem;

        public virtual WindowsApplication ApplicationInstance {
            [DebuggerNonUserCode]
            get => WindowsApplication.Instance;
        }

        public DataContext CommandContext { get; set; } = new DataContext();

        public string Name { get; set; }

        public string Text { get; set; }

        public System.Type CommandType { get; set; }

        public Image Image { get; set; }

        public List<CommandToolItem> Items { get; } = new List<CommandToolItem>();

        public Shortcut Shortcut { get; set; }

        public Keys ShortcutKeys { get; set; }

        public bool ShowAlwaysMenuStrip { get; set; }

        public bool ShowAlwaysToolStrip { get; set; }

        public bool ShowAlwaysContextMenu { get; set; }

        public bool ShowNeverMenuStrip { get; set; }

        public bool ShowNeverToolStrip { get; set; }

        public bool ShowNeverContextMenu { get; set; }

        public string ToolTipText { get; set; }

        public bool IsSeparator {
            [DebuggerNonUserCode]
            get => this.Text == "-" && this.CommandType == null;
        }

        public ToolStripItem CustomToolStripItem { get; private set; }

        public static CommandToolItem CreateFileNew(System.Type commandType) {
            commandType = commandType ?? typeof(CommandFileNewBase);
            return new CommandToolItem("&New", commandType, CommonImages.GetImage(CommonImageType.PageNew), "New", Shortcut.CtrlN) {
                ShowAlwaysToolStrip = true
            };
        }

        public static CommandToolItem CreateFileOpen(System.Type commandType) {
            commandType = commandType ?? typeof(CommandFileOpenBase);
            return new CommandToolItem("&Open...", commandType, CommonImages.GetImage(CommonImageType.DirectoryOpen), "Open...", Shortcut.CtrlO) {
                ShowAlwaysToolStrip = true
            };
        }

        public static CommandToolItem CreateFileSave(System.Type commandType) {
            commandType = commandType ?? typeof(CommandFileSaveBase);
            return new CommandToolItem("&Save", commandType, CommonImages.GetImage(CommonImageType.Save), "Save", Shortcut.CtrlS) {
                ShowAlwaysToolStrip = true
            };
        }

        public static CommandToolItem CreateFilePrint(System.Type commandType) {
            commandType = commandType ?? typeof(CommandFilePrintBase);
            return new CommandToolItem("&Print...", commandType, CommonImages.GetImage(CommonImageType.Print), "Print...", Shortcut.CtrlP) {
                ShowAlwaysToolStrip = true
            };
        }

        public static CommandToolItem CreateFilePrintPreview(System.Type commandType) {
            commandType = commandType ?? typeof(CommandFilePrintPreviewBase);
            return new CommandToolItem("Print Pre&view...", commandType, CommonImages.GetImage(CommonImageType.PrintPreview), "Print Preview...") {
                ShowAlwaysToolStrip = true
            };
        }

        public static CommandToolItem CreateFileExit(System.Type commandType) {
            commandType = commandType ?? typeof(CommandFileExitBase);
            return new CommandToolItem("E&xit", commandType, CommonImages.GetImage(CommonImageType.Exit), "Exit", Shortcut.AltF4) {
                ShowAlwaysToolStrip = true
            };
        }

        public static CommandToolItem CreateEditUndoItem(System.Type commandType) {
            commandType = commandType ?? typeof(CommandEditUndoBase);
            return new CommandToolItem("&Undo", commandType, CommonImages.GetImage(CommonImageType.Undo), "Undo", Shortcut.CtrlZ) {
                ShowAlwaysToolStrip = true
            };
        }

        public static CommandToolItem CreateEditRedoItem(System.Type commandType) {
            commandType = commandType ?? typeof(CommandEditRedoBase);
            return new CommandToolItem("&Redo", commandType, CommonImages.GetImage(CommonImageType.Redo), "Redo", Shortcut.CtrlY) {
                ShowAlwaysToolStrip = true
            };
        }

        public static CommandToolItem CreateEditCutItem(System.Type commandType) {
            commandType = commandType ?? typeof(CommandEditCutBase);
            return new CommandToolItem("Cu&t", commandType, CommonImages.GetImage(CommonImageType.Cut), "Cut", Shortcut.CtrlX) {
                ShowAlwaysToolStrip = true
            };
        }

        public static CommandToolItem CreateEditCopyItem(System.Type commandType) {
            commandType = commandType ?? typeof(CommandEditCopyBase);
            return new CommandToolItem("&Copy", commandType, CommonImages.GetImage(CommonImageType.Copy), "Copy", Shortcut.CtrlC) {
                ShowAlwaysToolStrip = true
            };
        }

        public static CommandToolItem CreateEditPasteItem(System.Type commandType) {
            commandType = commandType ?? typeof(CommandEditPasteBase);
            return new CommandToolItem("&Paste", commandType, CommonImages.GetImage(CommonImageType.Paste), "Paste", Shortcut.CtrlV) {
                ShowAlwaysToolStrip = true
            };
        }

        public MenuStrip CreateMenuStrip() {
            CommandToolItem._toolItemToCommandItem.Clear();
            if (string.IsNullOrEmpty(this.Name)) {
                this.Name = "CommandMenuStrip";
                CommandToolItem._temporaryNamed.Add(this);
            }
            this.SetTemporaryNames(this.Name, (IEnumerable<CommandToolItem>)this.Items);
            MenuStrip root = new MenuStrip();
            root.Name = this.Name;
            root.Text = this.Text;
            root.Items.AddRange(this.GetToolStripMenuItem((ToolStrip)root));
            CommandToolItem._toolItemToCommandItem.Clear();
            this.ResetTemporaryNames();
            return root;
        }

        public ContextMenuStrip CreateContextMenu() {
            CommandToolItem._toolItemToCommandItem.Clear();
            if (string.IsNullOrEmpty(this.Name)) {
                this.Name = "CommandMenuStrip";
                CommandToolItem._temporaryNamed.Add(this);
            }
            this.SetTemporaryNames(this.Name, (IEnumerable<CommandToolItem>)this.Items);
            ContextMenuStrip root = new ContextMenuStrip();
            root.Name = this.Name;
            root.Text = this.Text;
            root.Items.AddRange(this.GetToolStripMenuItem((ToolStrip)root));
            CommandToolItem._toolItemToCommandItem.Clear();
            this.ResetTemporaryNames();
            return root;
        }

        public ToolStrip CreateToolStrip() {
            CommandToolItem._toolItemToCommandItem.Clear();
            if (string.IsNullOrEmpty(this.Name)) {
                this.Name = "CommandMenuStrip";
                CommandToolItem._temporaryNamed.Add(this);
            }
            this.SetTemporaryNames(this.Name, (IEnumerable<CommandToolItem>)this.Items);
            ToolStrip toolStrip = new ToolStrip();
            toolStrip.Name = this.Name;
            toolStrip.Text = this.Text;
            ToolStripItem[] toolStripMenuItem = this.GetToolStripMenuItem(toolStrip);
            this.FlatToolStrip(toolStrip, (IEnumerable)toolStripMenuItem);
            if (toolStrip.Items.Count != 0 && toolStrip.Items[toolStrip.Items.Count - 1] is ToolStripSeparator)
                toolStrip.Items.RemoveAt(toolStrip.Items.Count - 1);
            CommandToolItem._toolItemToCommandItem.Clear();
            this.ResetTemporaryNames();
            return toolStrip;
        }

        public override string ToString() => this.Text;

        private ToolStripItem[] GetToolStripMenuItem(ToolStrip root) {
            List<ToolStripItem> toolStripItemList = new List<ToolStripItem>();
            ToolStripSeparator toolStripSeparator = (ToolStripSeparator)null;
            foreach (CommandToolItem commandToolItem in this.Items) {
                bool flag1 = commandToolItem.CustomToolStripItem != null || this.ApplicationInstance.CanExecuteCommand(commandToolItem.CommandType);
                bool flag2 = flag1;
                if (!flag1) {
                    flag2 = false;
                    if (root is MenuStrip && commandToolItem.ShowAlwaysMenuStrip)
                        flag2 = true;
                    else if (root is ContextMenuStrip && commandToolItem.ShowAlwaysContextMenu)
                        flag2 = true;
                    else if (root != null && commandToolItem.ShowAlwaysToolStrip)
                        flag2 = true;
                }
                ToolStripItem[] toolStripMenuItem = commandToolItem.GetToolStripMenuItem(root);
                if (toolStripMenuItem.Length != 0)
                    flag2 = true;
                if (commandToolItem.IsSeparator)
                    flag2 = true;
                if (root is MenuStrip && commandToolItem.ShowNeverMenuStrip)
                    flag2 = false;
                else if (root is ContextMenuStrip && commandToolItem.ShowNeverContextMenu)
                    flag2 = false;
                else if (root != null && commandToolItem.ShowNeverToolStrip) {
                    switch (root) {
                        case MenuStrip _:
                        case ContextMenuStrip _:
                            break;
                        default:
                            flag2 = false;
                            break;
                    }
                }
                if (flag2) {
                    if (commandToolItem.IsSeparator) {
                        if (toolStripItemList.Count != 0 && toolStripSeparator == null) {
                            toolStripSeparator = new ToolStripSeparator();
                            toolStripSeparator.Name = commandToolItem.Name;
                        }
                    }
                    else {
                        if (toolStripSeparator != null) {
                            toolStripItemList.Add((ToolStripItem)toolStripSeparator);
                            toolStripSeparator = (ToolStripSeparator)null;
                        }
                        if (commandToolItem.CustomToolStripItem != null) {
                            ToolStripItem customToolStripItem = commandToolItem.CustomToolStripItem;
                            customToolStripItem.Owner = (ToolStrip)null;
                            toolStripItemList.Add(customToolStripItem);
                            CommandToolItem._toolItemToCommandItem.Add(customToolStripItem, commandToolItem);
                        }
                        else {
                            ToolStripMenuItem key = new ToolStripMenuItem(commandToolItem.Text, commandToolItem.Image);
                            key.Click += new EventHandler(this.ItemClicked);
                            key.Name = commandToolItem.Name;
                            key.Tag = (object)commandToolItem;
                            key.ToolTipText = commandToolItem.ToolTipText;
                            if (root is ContextMenuStrip && !flag1 && toolStripMenuItem.Length == 0)
                                key.Enabled = false;
                            if (root is MenuStrip && (commandToolItem.Shortcut != Shortcut.None || commandToolItem.ShortcutKeys != Keys.None)) {
                                Keys keys = commandToolItem.ShortcutKeys;
                                if (keys == Keys.None) {
                                    string str = commandToolItem.Shortcut.ToString();
                                    if (str.StartsWith("Alt")) {
                                        str = str.Remove(0, "Alt".Length);
                                        keys = Keys.Alt;
                                    }
                                    else if (str.StartsWith("CtrlShift")) {
                                        str = str.Remove(0, "CtrlShift".Length);
                                        keys = Keys.Shift | Keys.Control;
                                    }
                                    else if (str.StartsWith("Ctrl")) {
                                        str = str.Remove(0, "Ctrl".Length);
                                        keys = Keys.Control;
                                    }
                                    else if (str.StartsWith("Shift")) {
                                        str = str.Remove(0, "Shift".Length);
                                        keys = Keys.Shift;
                                    }
                                    if (str == "Del")
                                        str = "Delete";
                                    keys |= (Keys)Enum.Parse(typeof(Keys), str);
                                }
                                key.ShortcutKeys = keys;
                            }
                            toolStripItemList.Add((ToolStripItem)key);
                            CommandToolItem._toolItemToCommandItem.Add((ToolStripItem)key, commandToolItem);
                            if (toolStripMenuItem.Length != 0) {
                                key.DropDownItems.AddRange(toolStripMenuItem);
                                key.DropDownOpening += new EventHandler(this.ItemDropDownOpening);
                            }
                        }
                    }
                }
            }
            return toolStripItemList.ToArray();
        }

        private void FlatToolStrip(ToolStrip toolStrip, IEnumerable items) {
            bool flag = false;
            ToolStripSeparator toolStripSeparator = (ToolStripSeparator)null;
            foreach (ToolStripItem key in items) {
                CommandToolItem commandToolItem = (CommandToolItem)null;
                if (CommandToolItem._toolItemToCommandItem.ContainsKey(key))
                    commandToolItem = CommandToolItem._toolItemToCommandItem[key];
                ToolStripMenuItem toolStripMenuItem = key as ToolStripMenuItem;
                if (commandToolItem != null && commandToolItem.CustomToolStripItem != null)
                    toolStrip.Items.Add(key);
                else if (toolStripMenuItem != null) {
                    if (commandToolItem != null && (this.ApplicationInstance.CanExecuteCommand(commandToolItem.CommandType) || commandToolItem.ShowAlwaysToolStrip)) {
                        if (toolStripSeparator != null && flag) {
                            toolStrip.Items.Add((ToolStripItem)toolStripSeparator);
                            toolStripSeparator = (ToolStripSeparator)null;
                        }
                        if (toolStripMenuItem.Image != null)
                            toolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
                        else
                            toolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                        toolStrip.Items.Add((ToolStripItem)toolStripMenuItem);
                        flag = true;
                    }
                    if (toolStripMenuItem.DropDownItems.Count != 0) {
                        ArrayList items1 = new ArrayList(toolStripMenuItem.DropDownItems.Count);
                        items1.AddRange((ICollection)toolStripMenuItem.DropDownItems);
                        this.FlatToolStrip(toolStrip, (IEnumerable)items1);
                    }
                }
                else if (key is ToolStripSeparator && toolStripSeparator == null)
                    toolStripSeparator = (ToolStripSeparator)key;
            }
            if (!flag)
                return;
            toolStrip.Items.Add((ToolStripItem)new ToolStripSeparator());
        }

        private void ItemDropDownOpening(object sender, EventArgs e) {
            if (!(sender is ToolStripMenuItem toolStripMenuItem1))
                return;
            foreach (ToolStripItem dropDownItem in (ArrangedElementCollection)toolStripMenuItem1.DropDownItems) {
                if (dropDownItem is ToolStripMenuItem toolStripMenuItem2) {
                    CommandToolItem commandItem = this.TagToCommandItem(toolStripMenuItem2.Tag);
                    if (commandItem != null) {
                        if (this.ApplicationInstance.CanExecuteCommand(commandItem.CommandType))
                            toolStripMenuItem2.Enabled = true;
                        else if (toolStripMenuItem2.DropDownItems.Count != 0)
                            toolStripMenuItem2.Enabled = true;
                        else
                            toolStripMenuItem2.Enabled = false;
                    }
                }
            }
        }

        private void ItemClicked(object sender, EventArgs e) {
            if (!(sender is ToolStripMenuItem toolStripMenuItem))
                return;
            CommandToolItem commandItem = this.TagToCommandItem(toolStripMenuItem.Tag);
            if (commandItem == null || commandItem.CommandType == null)
                return;
            //CommandToolItem._currentMenuItem = toolStripMenuItem;
            this.ApplicationInstance.ExecuteCommand(commandItem.CommandType, commandItem.CommandContext);
            //CommandToolItem._currentMenuItem = (ToolStripMenuItem)null;
        }

        private CommandToolItem TagToCommandItem(object tag) => tag as CommandToolItem;

        private void SetTemporaryNames(string parentName, IEnumerable<CommandToolItem> items) {
            foreach (CommandToolItem commandToolItem in items) {
                if (string.IsNullOrEmpty(commandToolItem.Name)) {
                    commandToolItem.Name = string.Format("{0}{1}", (object)parentName, (object)commandToolItem.Text);
                    CommandToolItem._temporaryNamed.Add(commandToolItem);
                    this.SetTemporaryNames(commandToolItem.Name, (IEnumerable<CommandToolItem>)commandToolItem.Items);
                }
            }
        }

        private void ResetTemporaryNames() {
            foreach (CommandToolItem commandToolItem in CommandToolItem._temporaryNamed)
                commandToolItem.Name = (string)null;
            CommandToolItem._temporaryNamed.Clear();
        }
    }
}
