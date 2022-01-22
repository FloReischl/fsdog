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
        private ConfigurationOptionsTree _configurationOptionsTree;
        private object _context;
        private string _path;

        public override void Execute() {
            _configurationOptionsTree = new ConfigurationOptionsTree("Options", "FS Dog");

            PictureBox pictureBox = CreateSplashScreen();
            _configurationOptionsTree.EditControl = (Control)pictureBox;
            BuildTree();
            FormOptions formOptions = new FormOptions();
            formOptions.Size = new Size(571, 400);
            formOptions.OptionsTree = (IOptionsTree)_configurationOptionsTree;
            if (formOptions.ShowDialog((IWin32Window)this.Application.MainForm) == DialogResult.OK) {
                var detailViewConfig = FsApp.Instance.Config.Options.DetailView;
                try {
                    string format = detailViewConfig.CustomDateTimeFormat;
                    if (!string.IsNullOrEmpty(format))
                        DateTime.Now.ToString(format);
                }
                catch {
                    detailViewConfig.CustomDateTimeFormat = "System";
                }
                FsApp.Instance.Config.Save();
                PreviewInfo.RefreshExtensions();
            }
        }

        private void BuildTree() {
            var options = FsApp.Instance.Config.Options;
            var general = options.General;
            var navigation = options.Navigation;
            var detailView = options.DetailView;
            var preview = options.Preview;
            
            _context = general;
            _path = "General";

            AddCheckBox(nameof(general.RestoreDirectories), "Restore previous directories", "Restore the directories which have been selected when FS-Dog was closed last time.");
            AddCheckBox(nameof(general.RestoreNetworkDirectories), "Restore network directories", "Restore  network directories which have been selected when FS-Dog was closed last time. This option takes no effect if \"Restore previous directories\" is not enabled.");
            AddCheckBox(nameof(general.CacheImages), "Cache icons", "Cache icons for fast navigation. If this property is enabled icons will be cached for each file type and for icons and applications.\r\nThis may cause a largeer memory usage but a much better performance.\r\n\r\nThe cache can be cleared at runtime with menu Options-\"Clear Icon Cache\".");

            _context = navigation;
            _path = "Navigation";
            AddCheckBox(nameof(navigation.ShowHiddenFiles), "Show hidden files", "Show hidden files and directories in tree and detail-view");
            AddCheckBox(nameof(navigation.ShowSystemFiles), "Show system files (experts only)", "Show system files and directories in tree and detail-view. This option should only be activated by experts.");
            AddCheckBox(nameof(navigation.ShowUserFolder), "Show user folder at root", "Show the user home folder at root of the tree.");
            AddCheckBox(nameof(navigation.ShowMyDocumentsFolder), "Show \"My Documents\" folder at root", "Show the \"My Documents\" folder at root of the tree.");
            AddCheckBox(nameof(navigation.ResolveDirectoryLinks), "Resolve directory links", "If true, links to directories will be resoved by FS-Dog and it navigates to the target location; otherwise the shortcut will be opened with Windows Explorer.");

            _context = detailView;
            _path = "Detail View";
            AddNormal(nameof(detailView.CustomDateTimeFormat), "Custom date/time format", "Specify a custom date/time (e.g. \"yyyy-MM-dd HH:mm:ss\"). Leave empty to use system default format.");
            AddCheckBox(nameof(detailView.SynchronizeColumnWidths), "Synchronize column widths", "Synchronize the column withs of the both detail views.");
            AddCheckBox(nameof(detailView.ShowModificationDateColumn), "Show modification date column", "Show column for file or directory modification date.");
            AddCheckBox(nameof(detailView.ShowCreationDateColumn), "Show creation date column", "Show column for file or directory creation date.");
            AddCheckBox(nameof(detailView.ShowFileExtensionColumn), "Show file extension column", "Show column for file extensions.");
            AddCheckBox(nameof(detailView.ShowAttributesColumn), "Show attributes column", "Show column for file or directories attributes.");
            AddCheckBox(nameof(detailView.ShowGridLines), "Show grid lines", "Show grid lines in detail view.");
            AddCheckBox(nameof(detailView.ShowDirectorySize), "Show directory sizes", "Show the sizes of directories (recursive) this information is sometimes helpfull, but may cause huge an high IO traffic on your devies.");
            AddCheckBox(nameof(detailView.DirectoriesAlwasOnTop), "Directories always on top", "If true directories will always be shown on top of the detail-view. Independant of the current sorting.");
            AddCheckBox(nameof(detailView.LoadImagesAsynchronous), "Load file icons asynchronously", "This option increases the load performance for directories in detail view.");

            _context = preview;
            _path = "Preview";
            AddNormal(nameof(preview.TextExtensions), "Text file extensions", "Specify semicolon separated the types of text files.");
            AddNormal(nameof(preview.ImageExtensions), "Image file extensions", "Specify semicolon separated the types of image files.");
            AddFont(nameof(preview.TextFontName), "Text font", "The font for the preview of text files.");
            AddCheckBox(nameof(preview.TextWrap), "Wrap text", "Specifies if the text should be wrapped or not.");

            _context = options.Menus.Applications;
            _path = "Menus/Applications";
            AddCheckBox(nameof(options.Menus.Applications.ShowShiftToolStrip), "Show \"Shift\" toolbar", "Specifies if a toolbar shall be created for application commands which are associated with the Shift-key. If no applications are available the toolbar will never be created");
            AddCheckBox(nameof(options.Menus.Applications.ShowShiftImage), "Show image for \"Shift\" applications", "Specifies if the application image shall be shown in toolbar for Shift-associated applications.");
            AddCheckBox(nameof(options.Menus.Applications.ShowShiftName), "Show name for \"Shift\" applications", "Specifies if the application name shall be shown in toolbar for Shift-associated applications.");
            AddCheckBox(nameof(options.Menus.Applications.ShowShiftShortcut), "Show shortcut for \"Shift\" applications", "Specifies if the application shortcut shall be shown in toolbar for Shift-associated applications.");
            AddCheckBox(nameof(options.Menus.Applications.ShowCtrlToolStrip), "Show \"Ctrl\" toolbar", "Specifies if a toolbar shall be created for application commands which are associated with the Ctrl-key. If no applications are available the toolbar will never be created");
            AddCheckBox(nameof(options.Menus.Applications.ShowCtrlImage), "Show image for \"Ctrl\" applications", "Specifies if the application image shall be shown in toolbar for Ctrl-associated applications.");
            AddCheckBox(nameof(options.Menus.Applications.ShowCtrlName), "Show name for \"Ctrl\" applications", "Specifies if the application name shall be shown in toolbar for Ctrl-associated applications.");
            AddCheckBox(nameof(options.Menus.Applications.ShowCtrlShortcut), "Show shortcut for \"Ctrl\" applications", "Specifies if the application shortcut shall be shown in toolbar for Ctrl-associated applications.");

            _context = options.Menus.Scripts;
            _path = "Menus/Scripts";
            AddCheckBox(nameof(options.Menus.Scripts.ShowShiftToolStrip), "Show \"Shift\" toolbar", "Specifies if a toolbar shall be created for script commands which are associated with the Shift-key. If no scripts are available the toolbar will never be created");
            AddCheckBox(nameof(options.Menus.Scripts.ShowShiftImage), "Show image for \"Shift\" scripts", "Specifies if the script image shall be shown in toolbar for Shift-associated scripts.");
            AddCheckBox(nameof(options.Menus.Scripts.ShowShiftName), "Show name for \"Shift\" scripts", "Specifies if the script name shall be shown in toolbar for Shift-associated scripts.");
            AddCheckBox(nameof(options.Menus.Scripts.ShowShiftShortcut), "Show shortcut for \"Shift\" scripts", "Specifies if the script shortcut shall be shown in toolbar for Shift-associated scripts.");
            AddCheckBox(nameof(options.Menus.Scripts.ShowCtrlToolStrip), "Show \"Ctrl\" toolbar", "Specifies if a toolbar shall be created for script commands which are associated with the Ctrl-key. If no scripts are available the toolbar will never be created");
            AddCheckBox(nameof(options.Menus.Scripts.ShowCtrlImage), "Show image for \"Ctrl\" scripts", "Specifies if the script image shall be shown in toolbar for Ctrl-associated scripts.");
            AddCheckBox(nameof(options.Menus.Scripts.ShowCtrlName), "Show name for \"Ctrl\" scripts", "Specifies if the script name shall be shown in toolbar for Ctrl-associated scripts.");
            AddCheckBox(nameof(options.Menus.Scripts.ShowCtrlShortcut), "Show shortcut for \"Ctrl\" scripts", "Specifies if the script shortcut shall be shown in toolbar for Ctrl-associated scripts.");

            _context = options.Appearance.Main;
            _path = "Appearance/Main";
            AddCheckBox(nameof(options.Appearance.Main.ShowStatusbar), "Show Application Statusbar", "Show the information status bar at bottom of FS-Dog.");
            AddCheckBox(nameof(options.Appearance.Main.FullPathInTitle), "Show full path in Title", "Show the full path of the current directory in title of FS-Dog.");
            AddCheckBox(nameof(options.Appearance.Main.RememberSize), "Remember window size", "Restore the last window size when FS-Dog becomes started next time.");
            AddCheckBox(nameof(options.Appearance.Main.RememberLocation), "Remember window location", "Restore the last window location when FS-Dog becomes started next time.");
            AddCheckBox(nameof(options.Appearance.Main.RememberWindowState), "Remember window state", "Restore the last window state (normal or maximized) when FS-Dog becomes started next time.");

            _context = options.Appearance.FileView;
            _path = "Appearance/File View";
            AddColor(nameof(options.Appearance.FileView.ActiveForeColorName), "Font color active control", "The font color of the current active control.");
            AddColor(nameof(options.Appearance.FileView.ActiveBackgroundColorName), "Background color active control", "The background color of the current active control.");
            AddColor(nameof(options.Appearance.FileView.InactiveForeColorName), "Font color inactive control", "The font color of inactive controls.");
            AddColor(nameof(options.Appearance.FileView.InactiveBackgroundColorName), "Background color inactive control", "The background color of inactive controls.");
            AddColor(nameof(options.Appearance.FileView.HiddenForeColorName), "Font color for hidden files", "The font color to show hidden files in tree and detail view");
            AddColor(nameof(options.Appearance.FileView.SystemForeColorName), "Font color for system files", "The font color to show system files in tree and detail view");
            AddColor(nameof(options.Appearance.FileView.CompressedForeColorName), "Font color for compressed files", "The font color to show compressed files in tree and detail view");
            AddFont(nameof(options.Appearance.FileView.FontName), "Font for the tree and detail-view", "The font of the items within tree and detail-view");
        }

        private ConfigurationOptionsProperty AddCheckBox(string property, string label, string description) {
            return AddSpecial(property, label, description, FormOptionsPropertySpecialType.CheckBox);
        }

        private ConfigurationOptionsProperty AddColor(string property, string label, string description) {
            return AddSpecial(property, label, description, FormOptionsPropertySpecialType.Color);
        }

        private ConfigurationOptionsProperty AddFont(string property, string label, string description) {
            return AddSpecial(property, label, description, FormOptionsPropertySpecialType.Font);
        }

        private ConfigurationOptionsProperty AddSpecial(string property, string label, string description, FormOptionsPropertySpecialType specialType) {
            var option = new ConfigurationOptionsProperty(_context, property, label, description);
            option.SpecialType = specialType;
            _configurationOptionsTree.AddOption(option, _path);
            return option;
        }

        private ConfigurationOptionsProperty AddNormal(string property, string label, string description) {
            var option = new ConfigurationOptionsProperty(_context, property, label, description);
            _configurationOptionsTree.AddOption(option, _path);
            return option;
        }

        private PictureBox CreateSplashScreen() {
            PictureBox pictureBox = new PictureBox();
            Image fsDogSplash = (Image)Resources.FsDogSplash;
            Image image = (Image)new Bitmap(fsDogSplash, new Size(fsDogSplash.Width * 2, fsDogSplash.Height * 2));
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox.Image = image;
            pictureBox.Size = image.Size;
            return pictureBox;
        }
    }
}
