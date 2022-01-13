// Decompiled with JetBrains decompiler
// Type: FsDog.CmdToolsOptions
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Commands;
using FR.Configuration;
using FR.Windows.Forms;
using FsDog.Detail;
using FsDog.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FsDog.Commands {
    public class CmdToolsOptions : CmdFsDogIntern {
        public override void Execute() {
            IConfigurationSource configurationSource = this.Application.ConfigurationSource;
            ConfigurationOptionsTree configurationOptionsTree = new ConfigurationOptionsTree(configurationSource, "Options", "FS Dog");
            PictureBox pictureBox = new PictureBox();
            Image fsDogSplash = (Image)Resources.FsDogSplash;
            Image image = (Image)new Bitmap(fsDogSplash, new Size(fsDogSplash.Width * 2, fsDogSplash.Height * 2));
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox.Image = image;
            pictureBox.Size = image.Size;
            configurationOptionsTree.EditControl = (Control)pictureBox;
            IConfigurationProperty property1 = configurationSource.GetProperty("Options/General", "RestoreDirectories");
            configurationOptionsTree.AddOption(property1, "General", "Restore previous directories", "Restore the directories which have been selected when FS-Dog was closed last time.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property2 = configurationSource.GetProperty("Options/General", "RestoreNetworkDirectories");
            configurationOptionsTree.AddOption(property2, "General", "Restore network directories", "Restore  network directories which have been selected when FS-Dog was closed last time. This option takes no effect if \"Restore previous directories\" is not enabled.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property3 = configurationSource.GetProperty("Options/General", "CacheImages");
            configurationOptionsTree.AddOption(property3, "General", "Cache icons", "Cache icons for fast navigation. If this property is enabled icons will be cached for each file type and for icons and applications.\r\nThis may cause a largeer memory usage but a much better performance.\r\n\r\nThe cache can be cleared at runtime with menu Options-\"Clear Icon Cache\".").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property4 = configurationSource.GetProperty("Options/Navigation", "ShowHiddenFiles");
            configurationOptionsTree.AddOption(property4, "Navigation", "Show hidden files", "Show hidden files and directories in tree and detail-view").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property5 = configurationSource.GetProperty("Options/Navigation", "ShowSystemFiles");
            configurationOptionsTree.AddOption(property5, "Navigation", "Show system files (experts only)", "Show system files and directories in tree and detail-view. This option should only be activated by experts.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property6 = configurationSource.GetProperty("Options/Navigation", "ShowUserFolder");
            configurationOptionsTree.AddOption(property6, "Navigation", "Show user folder at root", "Show the user home folder at root of the tree.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property7 = configurationSource.GetProperty("Options/Navigation", "ShowMyDocumentsFolder");
            configurationOptionsTree.AddOption(property7, "Navigation", "Show \"My Documents\" folder at root", "Show the \"My Documents\" folder at root of the tree.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property8 = configurationSource.GetProperty("Options/Navigation", "ResolveDirectoryLinks");
            configurationOptionsTree.AddOption(property8, "Navigation", "Resolve directory shortcuts", "If true shortcuts to other directories will be resoved by FS-Dog and it navigates to the target location; otherwise the shortcut will be opened with Windows Explorer.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property9 = configurationSource.GetProperty("Options/DetailView", "CustomDateTimeFormat");
            configurationOptionsTree.AddOption(property9, "Detail View", "Custom date/time format", "Specify a custom date/time (e.g. \"yyyy-MM-dd HH:mm:ss\"). Leave empty to use system default format.");
            IConfigurationProperty property10 = configurationSource.GetProperty("Options/DetailView", "SynchronizeColumnWidths");
            configurationOptionsTree.AddOption(property10, "Detail View", "Synchronize column widths", "Synchronize the column withs of the both detail-views").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property11 = configurationSource.GetProperty("Options/DetailView", "ShowModificationDateColumn");
            configurationOptionsTree.AddOption(property11, "Detail View", "Show modification date column", "Show column for file or directory modification date.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property12 = configurationSource.GetProperty("Options/DetailView", "ShowCreationDateColumn");
            configurationOptionsTree.AddOption(property12, "Detail View", "Show creation date column", "Show column for file or directory creation date.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property13 = configurationSource.GetProperty("Options/DetailView", "ShowFileExtensionColumn");
            configurationOptionsTree.AddOption(property13, "Detail View", "Show file extension column", "Show column for file extensions.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property14 = configurationSource.GetProperty("Options/DetailView", "ShowAttributesColumn");
            configurationOptionsTree.AddOption(property14, "Detail View", "Show attributes column", "Show column for file or directories attributes.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property15 = configurationSource.GetProperty("Options/DetailView", "ShowGridLines");
            configurationOptionsTree.AddOption(property15, "Detail View", "Show grid lines", "Show grid lines in detail-view").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property16 = configurationSource.GetProperty("Options/DetailView", "ShowDirectorySize");
            configurationOptionsTree.AddOption(property16, "Detail View", "Show directory sizes", "Show the sizes of directories (recursive) this information is sometimes helpfull, but may cause huge an high IO traffic on your devies.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property17 = configurationSource.GetProperty("Options/DetailView", "DirectoriesAlwasOnTop");
            configurationOptionsTree.AddOption(property17, "Detail View", "Directories always on top", "If true directories will always be shown on top of the detail-view. Independant of the current sorting.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property18 = configurationSource.GetProperty("Options/DetailView", "LoadImagesAsynchronous");
            configurationOptionsTree.AddOption(property18, "Detail View", "Load file icons asynchronously", "This option increases the load performance for directories in detail-view").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property19 = configurationSource.GetProperty("Options/Preview", "TextExtensions");
            configurationOptionsTree.AddOption(property19, "Preview", "Text file extensions", "Specify semicolon separated the types of text files.").EditControlWidth = int.MaxValue;
            IConfigurationProperty property20 = configurationSource.GetProperty("Options/Preview", "ImageExtensions");
            configurationOptionsTree.AddOption(property20, "Preview", "Image file extensions", "Specify semicolon separated the types of image files.").EditControlWidth = int.MaxValue;
            IConfigurationProperty property21 = configurationSource.GetProperty("Options/Preview", "TextFont");
            configurationOptionsTree.AddOption(property21, "Preview", "Text font", "The font for the preview of text files.").SpecialType = FormOptionsPropertySpecialType.Font;
            IConfigurationProperty property22 = configurationSource.GetProperty("Options/Preview", "TextWrap");
            configurationOptionsTree.AddOption(property22, "Preview", "Wrap text", "Specifies if the text should be wrapped or not.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property23 = configurationSource.GetProperty("Options/Menus/Applications", "ShowShiftToolStrip");
            configurationOptionsTree.AddOption(property23, "Menus/Applications", "Show \"Shift\" toolbar", "Specifies if a toolbar shall be created for application commands which are associated with the Shift-key. If no applications are available the toolbar will never be created").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property24 = configurationSource.GetProperty("Options/Menus/Applications", "ShowShiftImage");
            configurationOptionsTree.AddOption(property24, "Menus/Applications", "Show image for \"Shift\" applications", "Specifies if the application image shall be shown in toolbar for Shift-associated applications.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property25 = configurationSource.GetProperty("Options/Menus/Applications", "ShowShiftName");
            configurationOptionsTree.AddOption(property25, "Menus/Applications", "Show name for \"Shift\" applications", "Specifies if the application name shall be shown in toolbar for Shift-associated applications.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property26 = configurationSource.GetProperty("Options/Menus/Applications", "ShowShiftShortcut");
            configurationOptionsTree.AddOption(property26, "Menus/Applications", "Show shortcut for \"Shift\" applications", "Specifies if the application shortcut shall be shown in toolbar for Shift-associated applications.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property27 = configurationSource.GetProperty("Options/Menus/Applications", "ShowCtrlToolStrip");
            configurationOptionsTree.AddOption(property27, "Menus/Applications", "Show \"Ctrl\" toolbar", "Specifies if a toolbar shall be created for application commands which are associated with the Ctrl-key. If no applications are available the toolbar will never be created").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property28 = configurationSource.GetProperty("Options/Menus/Applications", "ShowCtrlImage");
            configurationOptionsTree.AddOption(property28, "Menus/Applications", "Show image for \"Ctrl\" applications", "Specifies if the application image shall be shown in toolbar for Ctrl-associated applications.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property29 = configurationSource.GetProperty("Options/Menus/Applications", "ShowCtrlName");
            configurationOptionsTree.AddOption(property29, "Menus/Applications", "Show name for \"Ctrl\" applications", "Specifies if the application name shall be shown in toolbar for Ctrl-associated applications.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property30 = configurationSource.GetProperty("Options/Menus/Applications", "ShowCtrlShortcut");
            configurationOptionsTree.AddOption(property30, "Menus/Applications", "Show shortcut for \"Ctrl\" applications", "Specifies if the application shortcut shall be shown in toolbar for Ctrl-associated applications.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property31 = configurationSource.GetProperty("Options/Menus/Scripts", "ShowShiftToolStrip");
            configurationOptionsTree.AddOption(property31, "Menus/Scripts", "Show \"Shift\" toolbar", "Specifies if a toolbar shall be created for script commands which are associated with the Shift-key. If no scripts are available the toolbar will never be created").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property32 = configurationSource.GetProperty("Options/Menus/Scripts", "ShowShiftImage");
            configurationOptionsTree.AddOption(property32, "Menus/Scripts", "Show image for \"Shift\" scripts", "Specifies if the script image shall be shown in toolbar for Shift-associated scripts.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property33 = configurationSource.GetProperty("Options/Menus/Scripts", "ShowShiftName");
            configurationOptionsTree.AddOption(property33, "Menus/Scripts", "Show name for \"Shift\" scripts", "Specifies if the script name shall be shown in toolbar for Shift-associated scripts.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property34 = configurationSource.GetProperty("Options/Menus/Scripts", "ShowShiftShortcut");
            configurationOptionsTree.AddOption(property34, "Menus/Scripts", "Show shortcut for \"Shift\" scripts", "Specifies if the script shortcut shall be shown in toolbar for Shift-associated scripts.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property35 = configurationSource.GetProperty("Options/Menus/Scripts", "ShowCtrlToolStrip");
            configurationOptionsTree.AddOption(property35, "Menus/Scripts", "Show \"Ctrl\" toolbar", "Specifies if a toolbar shall be created for script commands which are associated with the Ctrl-key. If no scripts are available the toolbar will never be created").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property36 = configurationSource.GetProperty("Options/Menus/Scripts", "ShowCtrlImage");
            configurationOptionsTree.AddOption(property36, "Menus/Scripts", "Show image for \"Ctrl\" scripts", "Specifies if the script image shall be shown in toolbar for Ctrl-associated scripts.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property37 = configurationSource.GetProperty("Options/Menus/Scripts", "ShowCtrlName");
            configurationOptionsTree.AddOption(property37, "Menus/Scripts", "Show name for \"Ctrl\" scripts", "Specifies if the script name shall be shown in toolbar for Ctrl-associated scripts.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property38 = configurationSource.GetProperty("Options/Menus/Scripts", "ShowCtrlShortcut");
            configurationOptionsTree.AddOption(property38, "Menus/Scripts", "Show shortcut for \"Ctrl\" scripts", "Specifies if the script shortcut shall be shown in toolbar for Ctrl-associated scripts.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property39 = configurationSource.GetProperty("Options/Appearance/Main", "ShowStatusbar");
            configurationOptionsTree.AddOption(property39, "Appearance/Main", "Show Application Statusbar", "Show the information status bar at bottom of FS-Dog.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property40 = configurationSource.GetProperty("Options/Appearance/Main", "FullPathInTitle");
            configurationOptionsTree.AddOption(property40, "Appearance/Main", "Show full path in Title", "Show the full path of the current directory in title of FS-Dog.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property41 = configurationSource.GetProperty("Options/Appearance/Main", "RememberSize");
            configurationOptionsTree.AddOption(property41, "Appearance/Main", "Remember window size", "Restore the last window size when FS-Dog becomes started next time.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property42 = configurationSource.GetProperty("Options/Appearance/Main", "RememberLocation");
            configurationOptionsTree.AddOption(property42, "Appearance/Main", "Remember window location", "Restore the last window location when FS-Dog becomes started next time.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property43 = configurationSource.GetProperty("Options/Appearance/Main", "RememberWindowState");
            configurationOptionsTree.AddOption(property43, "Appearance/Main", "Remember window state", "Restore the last window state (normal or maximized) when FS-Dog becomes started next time.").SpecialType = FormOptionsPropertySpecialType.CheckBox;
            IConfigurationProperty property44 = configurationSource.GetProperty("Options/Appearance/FileView", "ActiveForeColor");
            configurationOptionsTree.AddOption(property44, "Appearance/File View", "Font color active control", "The font color of the current active control.").SpecialType = FormOptionsPropertySpecialType.Color;
            IConfigurationProperty property45 = configurationSource.GetProperty("Options/Appearance/FileView", "ActiveBackgroundColor");
            configurationOptionsTree.AddOption(property45, "Appearance/File View", "Background color active control", "The background color of the current active control.").SpecialType = FormOptionsPropertySpecialType.Color;
            IConfigurationProperty property46 = configurationSource.GetProperty("Options/Appearance/FileView", "InactiveForeColor");
            configurationOptionsTree.AddOption(property46, "Appearance/File View", "Font color inactive control", "The font color of inactive controls.").SpecialType = FormOptionsPropertySpecialType.Color;
            IConfigurationProperty property47 = configurationSource.GetProperty("Options/Appearance/FileView", "InactiveBackgroundColor");
            configurationOptionsTree.AddOption(property47, "Appearance/File View", "Background color inactive control", "The background color of inactive controls.").SpecialType = FormOptionsPropertySpecialType.Color;
            IConfigurationProperty property48 = configurationSource.GetProperty("Options/Appearance/FileView", "HiddenForeColor");
            configurationOptionsTree.AddOption(property48, "Appearance/File View", "Font color for hidden files", "The font color to show hidden files in tree and detail-view").SpecialType = FormOptionsPropertySpecialType.Color;
            IConfigurationProperty property49 = configurationSource.GetProperty("Options/Appearance/FileView", "SystemForeColor");
            configurationOptionsTree.AddOption(property49, "Appearance/File View", "Font color for system files", "The font color to show system files in tree and detail-view").SpecialType = FormOptionsPropertySpecialType.Color;
            IConfigurationProperty property50 = configurationSource.GetProperty("Options/Appearance/FileView", "CompressedForeColor");
            configurationOptionsTree.AddOption(property50, "Appearance/File View", "Font color for compressed files", "The font color to show compressed files in tree and detail-view").SpecialType = FormOptionsPropertySpecialType.Color;
            IConfigurationProperty property51 = configurationSource.GetProperty("Options/Appearance/FileView", "Font");
            configurationOptionsTree.AddOption(property51, "Appearance/File View", "Font for the tree and detail-view", "The font of the items within tree and detail-view").SpecialType = FormOptionsPropertySpecialType.Font;
            FormOptions formOptions = new FormOptions();
            formOptions.Size = new Size(571, 400);
            formOptions.OptionsTree = (IOptionsTree)configurationOptionsTree;
            if (formOptions.ShowDialog((IWin32Window)this.Application.MainForm) == DialogResult.OK) {
                try {
                    string format = configurationSource.GetProperty("Options/DetailView", "CustomDateTimeFormat", true).ToString(string.Empty);
                    if (!string.IsNullOrEmpty(format))
                        DateTime.Now.ToString(format);
                }
                catch {
                    configurationSource.GetProperty("Options/DetailView", "CustomDateTimeFormat", true).Set("System");
                }
                this.Application.Options = new FsOptions();
                this.Application.ConfigurationSource.Save();
                PreviewInfo.RefreshExtensions();
                this.ExecutionState = CommandExecutionState.Ok;
            }
            else
                this.ExecutionState = CommandExecutionState.Canceled;
        }
    }
}
