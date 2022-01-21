

using FR.Configuration;
using FR.Drawing;
using FR.Windows.Forms.Commands;
using FsDog.Commands;
using FsDog.Properties;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;

namespace FsDog.Dialogs {
    public partial class FormMain {
        private void CreateMenu() {
            tscMain.TopToolStripPanel.Controls.Remove((Control)this.tsMain);
            _menu = new CommandToolItem("Main");
            _menu.Name = "Main";
            var fileParent = new CommandToolItem("&File");
            _menu.Items.Add(fileParent);
            fileParent.Items.Add(new CommandToolItem("&New File", typeof(CmdFileNewFile), CommonImages.GetImage(CommonImageType.PageNew), Keys.N | Keys.Control) {
                ShowAlwaysToolStrip = true
            });
            fileParent.Items.Add(new CommandToolItem("New &Directory", typeof(CmdFileNewDirectory), (Image)Resources.DirectoryNew, Keys.N | Keys.Shift | Keys.Control) {
                ShowAlwaysToolStrip = true
            });
            fileParent.Items.Add(new CommandToolItem("-"));
            fileParent.Items.Add(new CommandToolItem("&Rename multiple files", typeof(CmdFileRenameMulti), (Image)null, Keys.F2 | Keys.Shift | Keys.Control) {
                ShowNeverToolStrip = true
            });
            fileParent.Items.Add(new CommandToolItem("Open", typeof(CmdFileOpen)) {
                ShowNeverToolStrip = true
            });
            fileParent.Items.Add(new CommandToolItem("Open with...", typeof(CmdFileOpenWith)) {
                ShowNeverToolStrip = true
            });
            fileParent.Items.Add(new CommandToolItem("Run as...", typeof(CmdFileRunAs), (Image)null, Keys.Return | Keys.Shift | Keys.Control) {
                ShowNeverToolStrip = true
            });
            var commandToolItem2 = new CommandToolItem("Properties", typeof(CmdFileProperties), (Image)Resources.Properties, Keys.Return | Keys.Alt);
            fileParent.Items.Add(commandToolItem2);

            fileParent.Items.Add(new CommandToolItem("-"));
            var fileExit = CommandToolItem.CreateFileExit((System.Type)null);
            fileParent.Items.Add(fileExit);

            var editParent = new CommandToolItem("&Edit");
            _menu.Items.Add(editParent);
            var editCutItem = CommandToolItem.CreateEditCutItem(typeof(CommandEditCutBase));
            editCutItem.ShowAlwaysToolStrip = true;
            editParent.Items.Add(editCutItem);
            var editCopyItem = CommandToolItem.CreateEditCopyItem(typeof(CommandEditCopyBase));
            editCopyItem.ShowAlwaysToolStrip = true;
            editParent.Items.Add(editCopyItem);
            var editPasteItem = CommandToolItem.CreateEditPasteItem(typeof(CommandEditPasteBase));
            editPasteItem.ShowAlwaysToolStrip = true;
            editParent.Items.Add(editPasteItem);
            editParent.Items.Add(new CommandToolItem("-"));
            editParent.Items.Add(new CommandToolItem("Copy selected other detail view", typeof(CmdEditCopyToOtherView), (Image)null, Keys.C | Keys.Shift | Keys.Control) {
                ShowNeverToolStrip = true
            });
            editParent.Items.Add(new CommandToolItem("Move selected other detail view", typeof(CmdEditMoveToOtherView), (Image)null, Keys.X | Keys.Shift | Keys.Control) {
                ShowNeverToolStrip = true
            });
            editParent.Items.Add(new CommandToolItem("-"));
            editParent.Items.Add(new CommandToolItem("Copy selected names", typeof(CmdEditCopyFileNames), (Image)null, Keys.H | Keys.Control) {
                ShowNeverToolStrip = true,
                CommandContext = { { "CopyType", CmdEditCopyFileNames.CopyNameType.Name } }
            });
            editParent.Items.Add(new CommandToolItem("Copy selected pathes", typeof(CmdEditCopyFileNames), (Image)null, Keys.H | Keys.Shift | Keys.Control) {
                ShowNeverToolStrip = true,
                CommandContext = { { "CopyType", CmdEditCopyFileNames.CopyNameType.FullName } }
            });
            editParent.Items.Add(new CommandToolItem("Copy parent path", typeof(CmdEditCopyFileNames), (Image)null, Keys.H | Keys.Control | Keys.Alt) {
                ShowNeverToolStrip = true,
                CommandContext = { { "CopyType", CmdEditCopyFileNames.CopyNameType.ParentDirectory } }
            });
            editParent.Items.Add(new CommandToolItem("-"));
            editParent.Items.Add(new CommandToolItem("&Delete", typeof(CmdEditDelete), CommonImages.GetImage(CommonImageType.Delete)) {
                ShowAlwaysToolStrip = true
            });
            var commandToolItem5 = new CommandToolItem("-");
            editParent.Items.Add(commandToolItem5);
            editParent.Items.Add(new CommandToolItem("Select &All", typeof(CmdEditSelectAll), (Image)null, Keys.A | Keys.Control) {
                ShowNeverToolStrip = true
            });
            editParent.Items.Add(new CommandToolItem("&Invert Selection", typeof(CmdEditInvertSelection), (Image)null, Keys.A | Keys.Shift | Keys.Control) {
                ShowNeverToolStrip = true
            });

            var viewParent = new CommandToolItem("&View");
            _menu.Items.Add(viewParent);
            viewParent.Items.Add(new CommandToolItem("&Find", typeof(CmdViewFind), CommonImages.GetImage(CommonImageType.Find), Keys.F | Keys.Control) {
                ShowAlwaysToolStrip = true
            });
            viewParent.Items.Add(new CommandToolItem("-"));
            var commandToolItem8 = new CommandToolItem("Preview", typeof(CmdViewPreview), (Image)Resources.Preview, Keys.F11);
            viewParent.Items.Add(commandToolItem8);
            viewParent.Items.Add(new CommandToolItem("-"));
            viewParent.Items.Add(new CommandToolItem("Show directory sizes", typeof(CmdViewDirectorySizes), (Image)null, Keys.S | Keys.Shift | Keys.Control) {
                ShowNeverToolStrip = true
            });
            viewParent.Items.Add(new CommandToolItem("-"));
            viewParent.Items.Add(new CommandToolItem("&Refresh", typeof(CmdViewRefresh), CommonImages.GetImage(CommonImageType.Refresh), Keys.F5));

            var favoritesParent = CmdFavorite.GetFavoritesToolItem();
            _menu.Items.Add(favoritesParent);
            favoritesParent.Items.Insert(0, new CommandToolItem("&Go to Favorite", typeof(CmdViewGotoFavorite), (Image)Resources.Favorites, Keys.F4));
            favoritesParent.Items.Insert(1, new CommandToolItem("-"));

            var applicationsParent = CommandHelper.GetApplicationsToolItem();
            _menu.Items.Add(applicationsParent);

            var scriptsParent = CommandHelper.GetScriptsToolItem();
            _menu.Items.Add(scriptsParent);

            var toolsParent = new CommandToolItem("&Tools");
            _menu.Items.Add(toolsParent);
            toolsParent.Items.Add(new CommandToolItem("Clear Icon Cache", typeof(CmdToolsClearImageCache)) {
                ShowNeverToolStrip = true
            });
            toolsParent.Items.Add(new CommandToolItem("-"));
            toolsParent.Items.Add(new CommandToolItem("Edit Applications and Scripts", typeof(CmdCommandsEdit)) {
                ShowNeverToolStrip = true
            });
            toolsParent.Items.Add(new CommandToolItem("Configure Scripting Hosts", typeof(CmdScriptConfigureHosts)) {
                ShowNeverToolStrip = true
            });
            toolsParent.Items.Add(new CommandToolItem("-"));
            toolsParent.Items.Add(new CommandToolItem("Open Configuration File", typeof(CmdToolsOpenConfigFile)) {
                ShowNeverToolStrip = true
            });
            toolsParent.Items.Add(new CommandToolItem("Options", typeof(CmdToolsOptions)) {
                ShowNeverToolStrip = true
            });

            var helpParent = new CommandToolItem("&Help");
            _menu.Items.Add(helpParent);
            helpParent.Items.Add(new CommandToolItem("&About", typeof(CmdHelpAbout)) {
                ShowNeverToolStrip = true
            });
            _menuStrip = this._menu.CreateMenuStrip();
            _menuStrip.Dock = DockStyle.Top;
            Controls.Add((Control)this._menuStrip);
            PointConverter pointConverter = new PointConverter();
            _menu.Items.Remove(applicationsParent);
            _menu.Items.Remove(scriptsParent);
            _menu.Items.Remove(favoritesParent);
            ToolStrip toolStrip = this._menu.CreateToolStrip();
            AppearanceProvider appearance = new AppearanceProvider();
            appearance.ApplyToToolStrip(toolStrip);
            Point location = _formConfig.ToolStrips.ToolStrip(toolStrip.Name)?.Location ?? new Point(toolStrip.Location.X, toolStrip.Location.Y);
            tscMain.TopToolStripPanel.Join(toolStrip, location);
            _toolStrips.Add(toolStrip.Name, toolStrip);
        }


    }
}