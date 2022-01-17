// Decompiled with JetBrains decompiler
// Type: FsDog.FsOptions
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Configuration;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace FsDog {
    public class FsOptions {
        public FsOptions() {
            this.General = new FsOptions.GenerealOptions();
            this.Navigation = new FsOptions.NavigationOptions();
            this.DetailView = new FsOptions.DetailViewOptions();
            this.Preview = new FsOptions.PreviewOptions();
            this.AppearanceMain = new FsOptions.AppearanceMainOptions();
            this.AppearanceFileView = new FsOptions.AppearanceFileViewOptions();
            this.Commands = new FsOptions.CommandsOptions();
            this.MenusApplications = new FsOptions.MenusCommandsOptions("Applications");
            this.MenusScripts = new FsOptions.MenusCommandsOptions("Scripts");
        }

        public FsOptions.GenerealOptions General { get; private set; }

        public FsOptions.NavigationOptions Navigation { get; private set; }

        public FsOptions.DetailViewOptions DetailView { get; set; }

        public FsOptions.PreviewOptions Preview { get; private set; }

        public FsOptions.AppearanceMainOptions AppearanceMain { get; private set; }

        public FsOptions.AppearanceFileViewOptions AppearanceFileView { get; private set; }

        public FsOptions.CommandsOptions Commands { get; private set; }

        public FsOptions.MenusCommandsOptions MenusApplications { get; private set; }

        public FsOptions.MenusCommandsOptions MenusScripts { get; private set; }

        public abstract class OptionsBase {
            public OptionsBase() => this.ConfigurationSource = FsApp.Instance.ConfigurationSource;

            public IConfigurationSource ConfigurationSource { get; private set; }

            public IConfigurationProperty ConfigurationRoot { get; set; }
        }

        public class GenerealOptions : FsOptions.OptionsBase {
            public GenerealOptions() {
                this.ConfigurationRoot = this.ConfigurationSource.GetProperty("Options", "General", true);
                this.RestoreDirectories = this.ConfigurationRoot.GetSubProperty(nameof(RestoreDirectories), true).ToBoolean(true);
                this.RestoreNetworkDirectories = this.ConfigurationRoot.GetSubProperty(nameof(RestoreNetworkDirectories), true).ToBoolean(false);
                this.CacheImages = this.ConfigurationRoot.GetSubProperty(nameof(CacheImages), true).ToBoolean(true);
            }

            public bool RestoreDirectories { get; private set; }

            public bool RestoreNetworkDirectories { get; private set; }

            public bool CacheImages { get; private set; }
        }

        public class NavigationOptions : FsOptions.OptionsBase {
            public NavigationOptions() {
                this.ConfigurationRoot = this.ConfigurationSource.GetProperty("Options", "Navigation", true);
                this.ShowHiddenFiles = this.ConfigurationRoot.GetSubProperty(nameof(ShowHiddenFiles), true).ToBoolean(false);
                this.ShowSystemFiles = this.ConfigurationRoot.GetSubProperty(nameof(ShowSystemFiles), true).ToBoolean(false);
                this.ShowUserFolder = this.ConfigurationRoot.GetSubProperty(nameof(ShowUserFolder), true).ToBoolean(true);
                this.ShowMyDocumentsFolder = this.ConfigurationRoot.GetSubProperty(nameof(ShowMyDocumentsFolder), true).ToBoolean(false);
                this.ResolveDirectoryLinks = this.ConfigurationRoot.GetSubProperty(nameof(ResolveDirectoryLinks), true).ToBoolean(true);
            }

            public bool ShowHiddenFiles { get; private set; }

            public bool ShowSystemFiles { get; private set; }

            public bool ShowUserFolder { get; private set; }

            public bool ShowMyDocumentsFolder { get; private set; }

            public bool ResolveDirectoryLinks { get; private set; }
        }

        public class DetailViewOptions : FsOptions.OptionsBase {
            public DetailViewOptions() {
                this.ConfigurationRoot = this.ConfigurationSource.GetProperty("Options", "DetailView", true);
                this.CustomDateTimeFormat = this.ConfigurationRoot.GetSubProperty(nameof(CustomDateTimeFormat), true).ToString(string.Empty);
                if (string.IsNullOrEmpty(this.CustomDateTimeFormat))
                    this.CustomDateTimeFormat = string.Format("{0} {1}", (object)CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, (object)CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern);
                this.SynchronizeColumnWidths = this.ConfigurationRoot.GetSubProperty(nameof(SynchronizeColumnWidths), true).ToBoolean(true);
                this.ShowModificationDateColumn = this.ConfigurationRoot.GetSubProperty(nameof(ShowModificationDateColumn), true).ToBoolean(true);
                this.ShowCreationDateColumn = this.ConfigurationRoot.GetSubProperty(nameof(ShowCreationDateColumn), true).ToBoolean(false);
                this.ShowFileExtensionColumn = this.ConfigurationRoot.GetSubProperty(nameof(ShowFileExtensionColumn), true).ToBoolean(false);
                this.ShowAttributesColumn = this.ConfigurationRoot.GetSubProperty(nameof(ShowAttributesColumn), true).ToBoolean(false);
                this.ShowGridLines = this.ConfigurationRoot.GetSubProperty(nameof(ShowGridLines), true).ToBoolean(true);
                this.ShowDirectorySize = this.ConfigurationRoot.GetSubProperty(nameof(ShowDirectorySize), true).ToBoolean(false);
                this.DirectoriesAlwasOnTop = this.ConfigurationRoot.GetSubProperty(nameof(DirectoriesAlwasOnTop), true).ToBoolean(true);
                this.LoadImagesAsynchronous = this.ConfigurationRoot.GetSubProperty(nameof(LoadImagesAsynchronous), true).ToBoolean(false);
            }

            public string CustomDateTimeFormat { get; private set; }

            public bool SynchronizeColumnWidths { get; private set; }

            public bool ShowModificationDateColumn { get; private set; }

            public bool ShowCreationDateColumn { get; private set; }

            public bool ShowFileExtensionColumn { get; private set; }

            public bool ShowAttributesColumn { get; private set; }

            public bool ShowGridLines { get; private set; }

            public bool ShowDirectorySize { get; private set; }

            public bool DirectoriesAlwasOnTop { get; private set; }

            public bool LoadImagesAsynchronous { get; private set; }
        }

        public class PreviewOptions : FsOptions.OptionsBase {
            public PreviewOptions() {
                this.ConfigurationRoot = this.ConfigurationSource.GetProperty("Options", "Preview", true);
                FontConverter fontConverter = new FontConverter();
                this.TextExtensions = this.ConfigurationRoot.GetSubProperty(nameof(TextExtensions), true).ToString(".txt;.log;.xml;.bat;.css;.aspx;.cpp;.cs;.csscript;.csv;.htm;.html;.js;.reg;.sql;.vb;.vbs;.xsd;.xslt;");
                this.ImageExtensions = this.ConfigurationRoot.GetSubProperty(nameof(ImageExtensions), true).ToString(".jpg;.png;.bmp");
                //this.TextFont = (Font) fontConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty(nameof (TextFont), true).ToString(fontConverter.ConvertToString("  Microsoft Sans Serif, 8.25[pt]")));
                this.TextFont = Control.DefaultFont;
                this.TextWrap = this.ConfigurationRoot.GetSubProperty(nameof(TextWrap), true).ToBoolean(false);
            }

            public Font TextFont { get; private set; }

            public bool TextWrap { get; private set; }

            public string TextExtensions { get; private set; }

            public string ImageExtensions { get; private set; }
        }

        public class MenusCommandsOptions : FsOptions.OptionsBase {
            public MenusCommandsOptions(string commandType) {
                this.ConfigurationRoot = this.ConfigurationSource.GetProperty("Options/Menus", commandType, true);
                this.ShowShiftToolStrip = this.ConfigurationRoot.GetSubProperty(nameof(ShowShiftToolStrip), true).ToBoolean(true);
                this.ShowShiftImage = this.ConfigurationRoot.GetSubProperty(nameof(ShowShiftImage), true).ToBoolean(true);
                this.ShowShiftName = this.ConfigurationRoot.GetSubProperty(nameof(ShowShiftName), true).ToBoolean(true);
                this.ShowShiftShortcut = this.ConfigurationRoot.GetSubProperty(nameof(ShowShiftShortcut), true).ToBoolean(true);
                this.ShowCtrlToolStrip = this.ConfigurationRoot.GetSubProperty(nameof(ShowCtrlToolStrip), true).ToBoolean(true);
                this.ShowCtrlImage = this.ConfigurationRoot.GetSubProperty(nameof(ShowCtrlImage), true).ToBoolean(true);
                this.ShowCtrlName = this.ConfigurationRoot.GetSubProperty(nameof(ShowCtrlName), true).ToBoolean(true);
                this.ShowCtrlShortcut = this.ConfigurationRoot.GetSubProperty(nameof(ShowCtrlShortcut), true).ToBoolean(true);
            }

            public bool ShowShiftToolStrip { get; private set; }

            public bool ShowShiftImage { get; private set; }

            public bool ShowShiftName { get; private set; }

            public bool ShowShiftShortcut { get; private set; }

            public bool ShowCtrlToolStrip { get; private set; }

            public bool ShowCtrlImage { get; private set; }

            public bool ShowCtrlName { get; private set; }

            public bool ShowCtrlShortcut { get; private set; }
        }

        public class AppearanceMainOptions : FsOptions.OptionsBase {
            public AppearanceMainOptions() {
                this.ConfigurationRoot = this.ConfigurationSource.GetProperty("Options/Appearance", "Main", true);
                this.ShowStatusBar = this.ConfigurationRoot.GetSubProperty("ShowStatusbar", true).ToBoolean(true);
                this.FullPathInTitle = this.ConfigurationRoot.GetSubProperty(nameof(FullPathInTitle), true).ToBoolean(false);
                this.RememberSize = this.ConfigurationRoot.GetSubProperty(nameof(RememberSize), true).ToBoolean(false);
                this.RememberLocation = this.ConfigurationRoot.GetSubProperty(nameof(RememberLocation), true).ToBoolean(false);
                this.RememberWindowState = this.ConfigurationRoot.GetSubProperty(nameof(RememberWindowState), true).ToBoolean(false);
            }

            public bool ShowStatusBar { get; private set; }

            public bool FullPathInTitle { get; private set; }

            public bool RememberSize { get; private set; }

            public bool RememberLocation { get; private set; }

            public bool RememberWindowState { get; private set; }
        }

        public class AppearanceFileViewOptions : FsOptions.OptionsBase {
            public AppearanceFileViewOptions() {
                this.ConfigurationRoot = this.ConfigurationSource.GetProperty("Options/Appearance", "FileView", true);
                ColorConverter colorConverter = new ColorConverter();
                FontConverter fontConverter = new FontConverter();
                this.ActiveForeColor = (Color)colorConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty(nameof(ActiveForeColor), true).ToString(colorConverter.ConvertToString((object)SystemColors.ControlText)));
                this.ActiveBackgroundColor = (Color)colorConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty(nameof(ActiveBackgroundColor), true).ToString(colorConverter.ConvertToString((object)SystemColors.Window)));
                this.InactiveForeColor = (Color)colorConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty(nameof(InactiveForeColor), true).ToString(colorConverter.ConvertToString((object)SystemColors.ControlText)));
                this.InactiveBackgroundColor = (Color)colorConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty(nameof(InactiveBackgroundColor), true).ToString(colorConverter.ConvertToString((object)SystemColors.InactiveBorder)));
                this.HiddenForeColor = (Color)colorConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty(nameof(HiddenForeColor), true).ToString(colorConverter.ConvertToString((object)Color.DarkGray)));
                this.SystemForeColor = (Color)colorConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty(nameof(SystemForeColor), true).ToString(colorConverter.ConvertToString((object)Color.DarkGray)));
                this.CompressedForeColor = (Color)colorConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty(nameof(CompressedForeColor), true).ToString(colorConverter.ConvertToString((object)Color.Blue)));
                this.Font = Control.DefaultFont; //(Font) fontConverter.ConvertFromString(this.ConfigurationRoot.GetSubProperty(nameof (Font), true).ToString(fontConverter.ConvertToString((object) Control.DefaultFont)));
            }

            public Color ActiveForeColor { get; set; }

            public Color ActiveBackgroundColor { get; set; }

            public Color InactiveForeColor { get; private set; }

            public Color InactiveBackgroundColor { get; private set; }

            public Color HiddenForeColor { get; private set; }

            public Color SystemForeColor { get; private set; }

            public Color CompressedForeColor { get; private set; }

            public Font Font { get; private set; }
        }

        public class CommandsOptions : FsOptions.OptionsBase {
            public CommandsOptions() {
                this.ConfigurationRoot = this.ConfigurationSource.GetProperty("Options", "CommandsApps", true);
                this.ShowIcon = this.ConfigurationRoot.GetSubProperty(nameof(ShowIcon), true).ToBoolean(true);
                this.ShowName = this.ConfigurationRoot.GetSubProperty(nameof(ShowName), true).ToBoolean(true);
                this.ShowShortcut = this.ConfigurationRoot.GetSubProperty(nameof(ShowShortcut), true).ToBoolean(true);
            }

            public bool ShowIcon { get; private set; }

            public bool ShowName { get; private set; }

            public bool ShowShortcut { get; private set; }
        }
    }
}
