using FR.Logging;
using FsDog.Detail;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FsDog {
    class AppearanceProvider {
        private readonly FsOptions _rootOptions;
        private readonly FsOptions.AppearanceFileViewOptions _options;
        private readonly ILogger _log;

        public AppearanceProvider() {
            //FsApp.Instance.Logger.Write
            _rootOptions = FsApp.Instance.Options;
            _options = _rootOptions.AppearanceFileView;
            //_options.ActiveBackgroundColor = Color.Black;
            //_options.ActiveForeColor = Color.White;
            _log = FsApp.Instance.CreateLogger();
        }

        public void ApplyToForm(Form form) {
            //ApplyToControlInternal(form);
            //ApplyToContainer(form);
        }

        public void ApplyToControl(Control ctrl) {
            //switch (ctrl) {
            //    case TextBox _:
            //        ApplyToTextBox((TextBox)ctrl);
            //        break;
            //    case Label _:
            //        ApplyToLabel((Label)ctrl);
            //        break;
            //    case ComboBox _:
            //        ApplyToComboBox((ComboBox)ctrl);
            //        break;
            //    case TreeView _:
            //        ApplyToTreeView((TreeView)ctrl);
            //        break;
            //    case MenuStrip _:
            //        ApplyToMenuStrip((MenuStrip)ctrl);
            //        break;
            //    case ToolStrip _:
            //        ApplyToToolStrip((ToolStrip)ctrl);
            //        break;
            //    case RichTextBox _:
            //        ApplyToRichTextBox((RichTextBox)ctrl);
            //        break;
            //    case ToolStripContainer _:
            //        ApplyToToolStripContainer((ToolStripContainer)ctrl);
            //        break;
            //    case SplitContainer _:
            //        ApplyToSplitContainer((SplitContainer)ctrl);
            //        break;
            //    case DetailView _:
            //        ApplyToDetailView((DetailView)ctrl);
            //        break;
            //    case DetailGrid _:
            //        ApplyToDetailGrid((DetailGrid)ctrl);
            //        break;
            //    case Panel _:
            //        ApplyToPanel((Panel)ctrl);
            //        break;
            //    default:
            //        _log.Info($"Unknown control type {ctrl.GetType().Name}");
            //        break;
            //}
        }

        //private void ApplyToDetailGrid(DetailGrid ctrl) {
        //    ApplyToControlInternal(ctrl);
        //    ctrl.BackgroundColor = _options.ActiveBackgroundColor;
        //    ctrl.RowsDefaultCellStyle.BackColor = _options.ActiveBackgroundColor;
        //    ctrl.RowsDefaultCellStyle.ForeColor = _options.ActiveForeColor;
        //    ctrl.RowHeadersDefaultCellStyle.BackColor = _options.ActiveBackgroundColor;
        //}

        //private void ApplyToPanel(Panel ctrl) {
        //    ApplyToControlInternal(ctrl);
        //    ApplyToContainer(ctrl);
        //}

        //private void ApplyToDetailView(DetailView ctrl) {
        //    ApplyToControlInternal(ctrl);
        //    ApplyToContainer(ctrl);
        //}

        //private void ApplyToSplitContainer(SplitContainer ctrl) {
        //    ApplyToControlInternal(ctrl);
        //    ApplyToControlInternal(ctrl.Panel1);
        //    ApplyToControlInternal(ctrl.Panel2);
        //    ApplyToContainer(ctrl.Panel1);
        //    ApplyToContainer(ctrl.Panel2);
        //}

        //private void ApplyToToolStripContainer(ToolStripContainer ctrl) {
        //    ApplyToControlInternal(ctrl);
        //    ApplyToControlInternal(ctrl.ContentPanel);
        //    ApplyToContainer(ctrl.ContentPanel);
        //}

        //private void ApplyToContainer(Control control) {
        //    foreach (var ctrl in control.Controls) {
        //        ApplyToControl((Control)ctrl);
        //    }
        //}

        //public void ApplyToTextBox(TextBox control) => ApplyToControlInternal(control);
        //public void ApplyToLabel(Label control) => ApplyToControlInternal(control);
        //public void ApplyToComboBox(ComboBox control) => ApplyToControlInternal(control);
        //public void ApplyToTreeView(TreeView control) {
        //    ApplyToControlInternal(control);
        //}

        //public void ApplyToRichTextBox(RichTextBox control) => ApplyToControlInternal(control);
        //public void ApplyToListView(ListView control) => ApplyToControlInternal(control);
        //public void ApplyToButton(Button control) => ApplyToControlInternal(control);
        //public void ApplyToMenuStrip(MenuStrip control) => ApplyToControlInternal(control);
        public void ApplyToToolStrip(ToolStrip control) {
            //ApplyToControlInternal(control);
        }
        ////public void ApplyTo(Control control) => ApplyToControlInternal(control);
        ////public void ApplyTo(Control control) => ApplyToControlInternal(control);

        //private void ApplyToControlInternal(Control control) {
        //    control.BackColor = _options.ActiveBackgroundColor;
        //    control.ForeColor = _options.ActiveForeColor;
        //}
    }
}
