using FR.Windows.Forms;
using FsDog.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Linq;

namespace FsDog.Dialogs {
    public partial class FormCommands : Form {
        private static readonly HashSet<Keys> ValidKeys;
        private const string TypeApplication = "Application";
        private const string TypeScript = "Script";

        static FormCommands() {
            ValidKeys = new HashSet<Keys>();
            
            for (Keys key = Keys.F1; key <= Keys.F12; ++key) {
                ValidKeys.Add(key);
            }

            for (Keys key = Keys.A; key <= Keys.Z; ++key) {
                ValidKeys.Add(key);
            }
        }

        public FormCommands() {
            InitializeComponent();
        }

        private ListViewItem CreateListViewItem() => new ListViewItem() {
            Text = "",
            Name = "",
            SubItems = { "", "", "", "", "" }
        };

        private bool VerifyInput(bool add, bool update, bool delete) {
            DialogResult result = DialogResult.None;

            void show(string msg) => result = MessageBox.Show(this, msg, "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if ((add || update) && cboType.SelectedIndex == -1) {
                show("No command type specified.");
            }
            if ((add || update) && string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtFileName.Text)) {
                show("No name was specified.");
            }
            else if ((add || update) && string.IsNullOrEmpty(txtFileName.Text) && !string.IsNullOrEmpty(txtName.Text)) {
                show("No command was specified.");
            }
            //else if ((add || update) && string.IsNullOrEmpty(txtKeys.Text)) {
            //    show("No shortcut specified.");
            //}
            else if ((add || update) && cboType.SelectedText.Equals(CommandType.Script.ToString()) && cboScriptType.SelectedIndex == -1) {
                show("No scripting host was specified.");
            }
            else if ((update || delete) && lvwCommands.SelectedItems.Count == 0) {
                show("No item for update selected.");
            }

            return result == DialogResult.None;
        }

        private void CommandToItem(CommandInfo cmd, ListViewItem item) {
            item.SubItems[0].Text = cmd.GetShortcutText();
            item.SubItems[1].Text = cmd.Name;
            item.SubItems[2].Text = cmd.CommandType.ToString();
            item.SubItems[3].Text = cmd.Command;
            item.SubItems[4].Text = cmd.Arguments;
            item.SubItems[5].Text = cmd.ScriptingHost;
            item.Tag = (object)cmd;
        }

        private CommandInfo ViewToCommand() {
            var cmd = new CommandInfo();
            cmd.Name = txtName.Text;
            cmd.Command = txtFileName.Text;
            cmd.Arguments = txtArguments.Text;
            cmd.CommandType = (CommandType)cboType.SelectedItem;

            if (cmd.CommandType == CommandType.Script) {
                ComboBoxItem selectedItem = (ComboBoxItem)cboScriptType.SelectedItem;
                cmd.ScriptingHost = ((ScriptingHostConfiguration)selectedItem.Value).Name;
            }

            string keys = txtKeys.Text;
            if (!string.IsNullOrEmpty(keys)) {
                KeysConverter keysConverter = new KeysConverter();
                cmd.Key = (Keys)keysConverter.ConvertFromString(keys);
            }

            return cmd;
        }

        private void CommandToView(CommandInfo cmd) {
            cboType.SelectedItem = cmd.CommandType;
            txtName.Text = cmd.Name;
            txtFileName.Text = cmd.Command;
            txtArguments.Text = cmd.Arguments;
            txtKeys.Text = cmd.GetShortcutText();

            if (cmd.CommandType == CommandType.Application) {
                cboScriptType.SelectedItem = (object)null;
            }
            else {
                ScriptingHostConfiguration hostConfiguration = (ScriptingHostConfiguration)null;
                if (!string.IsNullOrEmpty(cmd.ScriptingHost))
                    FsApp.Instance.ScriptingHosts.TryGetValue(cmd.ScriptingHost, out hostConfiguration);
                cboScriptType.SelectedItem = (object)hostConfiguration;
            }
        }

        private void FormApplications_Load(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }

            //cboType.SelectedIndexChanged += new EventHandler(cboType_SelectedIndexChanged);
            //lvwCommands.KeyDown += new KeyEventHandler(lvwCommands_KeyDown);
            cboType.Items.Add(CommandType.Application);
            cboType.Items.Add(CommandType.Script);
            foreach (ScriptingHostConfiguration hostConfiguration in FsApp.Instance.ScriptingHosts.Values)
                cboScriptType.Items.Add((object)new ComboBoxItem(hostConfiguration.Name, (object)hostConfiguration));

            lvwCommands.BeginUpdate();
            lvwCommands.Columns.Add("Keys", "Keys", 60);
            lvwCommands.Columns.Add("Name", "Name", 100);
            lvwCommands.Columns.Add("Type", "Type", 80);
            lvwCommands.Columns.Add("FileName", "File Name", 250);
            lvwCommands.Columns.Add("Arguments", "Arguments", 100);
            lvwCommands.Columns.Add("Engine", "Engine", 100);

            foreach (CommandInfo info in CommandHelper.GetInfos()) {
                ListViewItem item = CreateListViewItem();
                CommandInfo cmd = new CommandInfo(info);
                CommandToItem(cmd, item);
                lvwCommands.Items.Add(item);
            }

            lvwCommands.EndUpdate();

            foreach (ToolStripItem toolStripItem in (ArrangedElementCollection)ctxArguments.Items)
                toolStripItem.Click += new EventHandler(ctxArguments_Items_Click);
        }

        private void FormApplications_FormClosing(object sender, FormClosingEventArgs e) {
            if (DialogResult != DialogResult.OK)
                return;
            KeysConverter keysConverter = new KeysConverter();
            List<CommandInfo> infos = new List<CommandInfo>();
            foreach (ListViewItem listViewItem in lvwCommands.Items) {
                CommandInfo tag = (CommandInfo)listViewItem.Tag;
                if (tag != null)
                    infos.Add(tag);
            }
            CommandHelper.SetToConfig((IList<CommandInfo>)infos);
            WindowsApplication.Instance.ConfigurationSource.Save();
        }

        private void lvwCommands_SelectedIndexChanged(object sender, EventArgs e) {
            CommandInfo cmd;

            if (lvwCommands.SelectedItems.Count != 0) {
                var lvi = lvwCommands.SelectedItems[0];
                cmd = (CommandInfo)lvi.Tag;
            }
            else {
                cmd = new CommandInfo();
            }

            CommandToView(cmd);
        }

        private void ctxArguments_Items_Click(object sender, EventArgs e) => txtArguments.SelectedText = ((ToolStripItem)sender).Tag.ToString();

        private void btnFileName_Click(object sender, EventArgs e) {
            if (lvwCommands.SelectedItems.Count == 0)
                return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (object.ReferenceEquals(cboType.SelectedItem, (object)"Application"))
                openFileDialog.Filter = "Applications (*.exe)|*.exe|All Files (*.*)|*.*";
            else
                openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.CheckFileExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select a new command file to be executed";
            if (openFileDialog.ShowDialog((IWin32Window)WindowsApplication.Instance.MainForm) != DialogResult.OK)
                return;
            txtFileName.Text = openFileDialog.FileName;
        }

        private void btnArguments_Click(object sender, EventArgs e) => ctxArguments.Show((Control)btnArguments, new Point(btnArguments.Width, 0));

        private void txtKeys_KeyDown(object sender, KeyEventArgs e) {
            if (!ValidKeys.Contains(e.KeyCode)) {
                txtKeys.Text = string.Empty;
                return;
            }

            var s = e.KeyCode.ToString();

            if (e.Alt)
                s += $" + {Keys.Alt}";

            if (e.Control)
                s += $" + {Keys.Control}";

            if (e.Shift)
                s += $" + {Keys.Shift}";

            txtKeys.Text = s;
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if (VerifyInput(true, false, false)) {
                var cmd = ViewToCommand();
                var item = CreateListViewItem();
                CommandToItem(cmd, item);
                lvwCommands.Items.Add(item);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            if (VerifyInput(false, true, false)) {
                var cmd = ViewToCommand();
                var item = lvwCommands.SelectedItems[0];
                CommandToItem(cmd, item);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e) {
            if (VerifyInput(false, false, true)) {
                var item = lvwCommands.SelectedItems[0];
                lvwCommands.Items.Remove(item);
            }
        }
    }
}
