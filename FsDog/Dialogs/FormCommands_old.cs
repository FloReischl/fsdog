// Decompiled with JetBrains decompiler
// Type: FsDog.FormCommands
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Windows.Forms;
using FsDog.Commands;
using FsDog.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace FsDog {
    public class FormCommands_old : Form {
        private const string TypeApplication = "Application";
        private const string TypeScript = "Script";
        private IContainer components;
        private Label label1;
        private ListView lvwCommands;
        private Label label2;
        private TextBox txtKeys;
        private TextBox txtName;
        private Label label3;
        private TextBox txtFileName;
        private Label label4;
        private TextBox txtArguments;
        private Label label5;
        private Button btnFileName;
        private Button btnArguments;
        private ContextMenuStrip ctxArguments;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem4;
        private Button btnOk;
        private Button btnCancel;
        private Button btnSet;
        private ComboBox cboType;
        private Label label6;
        private Label lblScriptType;
        private ComboBox cboScriptType;
        private ToolStripMenuItem f2SecondSelectedFileNameWithPathToolStripMenuItem;

        protected override void Dispose(bool disposing) {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.components = (IContainer)new Container();
            this.label1 = new Label();
            this.lvwCommands = new ListView();
            this.label2 = new Label();
            this.txtKeys = new TextBox();
            this.txtName = new TextBox();
            this.label3 = new Label();
            this.txtFileName = new TextBox();
            this.label4 = new Label();
            this.txtArguments = new TextBox();
            this.ctxArguments = new ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new ToolStripMenuItem();
            this.toolStripMenuItem6 = new ToolStripMenuItem();
            this.toolStripMenuItem4 = new ToolStripMenuItem();
            this.label5 = new Label();
            this.btnFileName = new Button();
            this.btnArguments = new Button();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.btnSet = new Button();
            this.cboType = new ComboBox();
            this.label6 = new Label();
            this.lblScriptType = new Label();
            this.cboScriptType = new ComboBox();
            this.f2SecondSelectedFileNameWithPathToolStripMenuItem = new ToolStripMenuItem();
            this.ctxArguments.SuspendLayout();
            this.SuspendLayout();
            this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(561, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Edit remove or add new applicatiosn to be started by Shift or Ctrl in combination with F1 to F12.";
            this.lvwCommands.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.lvwCommands.FullRowSelect = true;
            this.lvwCommands.HideSelection = false;
            this.lvwCommands.Location = new Point(12, 40);
            this.lvwCommands.MultiSelect = false;
            this.lvwCommands.Name = "lvwCommands";
            this.lvwCommands.Size = new Size(561, 281);
            this.lvwCommands.TabIndex = 1;
            this.lvwCommands.UseCompatibleStateImageBehavior = false;
            this.lvwCommands.View = View.Details;
            this.lvwCommands.SelectedIndexChanged += new EventHandler(this.lvwShortCuts_SelectedIndexChanged);
            this.label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(414, 341);
            this.label2.Name = "label2";
            this.label2.Size = new Size(25, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Key";
            this.txtKeys.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.txtKeys.Location = new Point(445, 338);
            this.txtKeys.Name = "txtKeys";
            this.txtKeys.ReadOnly = true;
            this.txtKeys.Size = new Size(128, 20);
            this.txtKeys.TabIndex = 5;
            this.txtName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.txtName.Location = new Point(100, 365);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(143, 20);
            this.txtName.TabIndex = 3;
            this.label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(13, 367);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name";
            this.txtFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtFileName.Location = new Point(100, 391);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new Size(440, 20);
            this.txtFileName.TabIndex = 7;
            this.label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(13, 393);
            this.label4.Name = "label4";
            this.label4.Size = new Size(54, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "File Name";
            this.txtArguments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtArguments.ContextMenuStrip = this.ctxArguments;
            this.txtArguments.Location = new Point(100, 417);
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new Size(440, 20);
            this.txtArguments.TabIndex = 10;
            this.ctxArguments.Items.AddRange(new ToolStripItem[4]
            {
        (ToolStripItem) this.toolStripMenuItem2,
        (ToolStripItem) this.toolStripMenuItem6,
        (ToolStripItem) this.toolStripMenuItem4,
        (ToolStripItem) this.f2SecondSelectedFileNameWithPathToolStripMenuItem
            });
            this.ctxArguments.Name = "ctxArguments";
            this.ctxArguments.Size = new Size(286, 114);
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new Size(285, 22);
            this.toolStripMenuItem2.Tag = (object)"[f]";
            this.toolStripMenuItem2.Text = "[f] Selected file name with path";
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new Size(285, 22);
            this.toolStripMenuItem6.Tag = (object)"[ff]";
            this.toolStripMenuItem6.Text = "[ff] All selected file names with path";
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new Size(285, 22);
            this.toolStripMenuItem4.Tag = (object)"[d]";
            this.toolStripMenuItem4.Text = "[d] Current directory name with path";
            this.label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(13, 419);
            this.label5.Name = "label5";
            this.label5.Size = new Size(57, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Arguments";
            this.btnFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnFileName.Location = new Point(546, 391);
            this.btnFileName.Name = "btnFileName";
            this.btnFileName.Size = new Size(27, 20);
            this.btnFileName.TabIndex = 8;
            this.btnFileName.Text = "...";
            this.btnFileName.UseVisualStyleBackColor = true;
            this.btnFileName.Click += new EventHandler(this.btnFileName_Click);
            this.btnArguments.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnArguments.Image = (Image)Resources.Help;
            this.btnArguments.Location = new Point(546, 416);
            this.btnArguments.Name = "btnArguments";
            this.btnArguments.Size = new Size(27, 20);
            this.btnArguments.TabIndex = 11;
            this.btnArguments.UseVisualStyleBackColor = true;
            this.btnArguments.Click += new EventHandler(this.btnArguments_Click);
            this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnOk.DialogResult = DialogResult.OK;
            this.btnOk.Location = new Point(417, 486);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 23);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(498, 486);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnSet.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnSet.Location = new Point(498, 442);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new Size(75, 23);
            this.btnSet.TabIndex = 12;
            this.btnSet.Text = "&Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new EventHandler(this.btnSet_Click);
            this.cboType.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new Point(100, 338);
            this.cboType.Name = "cboType";
            this.cboType.Size = new Size(143, 21);
            this.cboType.TabIndex = 15;
            this.label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(13, 342);
            this.label6.Name = "label6";
            this.label6.Size = new Size(81, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Command Type";
            this.lblScriptType.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lblScriptType.AutoSize = true;
            this.lblScriptType.Location = new Point(13, 448);
            this.lblScriptType.Name = "lblScriptType";
            this.lblScriptType.Size = new Size(61, 13);
            this.lblScriptType.TabIndex = 18;
            this.lblScriptType.Text = "Script Type";
            this.cboScriptType.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.cboScriptType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboScriptType.FormattingEnabled = true;
            this.cboScriptType.Location = new Point(100, 444);
            this.cboScriptType.Name = "cboScriptType";
            this.cboScriptType.Size = new Size(143, 21);
            this.cboScriptType.TabIndex = 17;
            this.f2SecondSelectedFileNameWithPathToolStripMenuItem.Name = "f2SecondSelectedFileNameWithPathToolStripMenuItem";
            this.f2SecondSelectedFileNameWithPathToolStripMenuItem.Size = new Size(285, 22);
            this.f2SecondSelectedFileNameWithPathToolStripMenuItem.Tag = (object)"[f2]";
            this.f2SecondSelectedFileNameWithPathToolStripMenuItem.Text = "[f2] Second selected file name with path";
            this.AcceptButton = (IButtonControl)this.btnOk;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = (IButtonControl)this.btnCancel;
            this.ClientSize = new Size(585, 521);
            this.Controls.Add((Control)this.lblScriptType);
            this.Controls.Add((Control)this.cboScriptType);
            this.Controls.Add((Control)this.label6);
            this.Controls.Add((Control)this.btnSet);
            this.Controls.Add((Control)this.btnArguments);
            this.Controls.Add((Control)this.cboType);
            this.Controls.Add((Control)this.btnCancel);
            this.Controls.Add((Control)this.btnFileName);
            this.Controls.Add((Control)this.txtArguments);
            this.Controls.Add((Control)this.btnOk);
            this.Controls.Add((Control)this.label5);
            this.Controls.Add((Control)this.txtFileName);
            this.Controls.Add((Control)this.label4);
            this.Controls.Add((Control)this.txtName);
            this.Controls.Add((Control)this.label3);
            this.Controls.Add((Control)this.lvwCommands);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.txtKeys);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new Size(444, 379);
            this.Name = "FormCommands_old";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Edit Applications";
            this.Load += new EventHandler(this.FormApplications_Load);
            this.FormClosing += new FormClosingEventHandler(this.FormApplications_FormClosing);
            this.ctxArguments.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public FormCommands_old() {
            this.InitializeComponent();
        }

        private ListViewItem CreateListViewItem() => new ListViewItem() {
            Text = "",
            Name = "",
            SubItems = {
                "",
                "",
                "",
                ""
              }
        };

        private void UpdateCommand(CommandInfo cmd) {
            cmd.Name = this.txtName.Text;
            cmd.Command = this.txtFileName.Text;
            cmd.CommandType = CommandType.Application;
            cmd.Arguments = this.txtArguments.Text;
            if (object.ReferenceEquals(this.cboType.SelectedItem, (object)"Application")) {
                cmd.CommandType = CommandType.Application;
                cmd.ScriptingHost = (string)null;
            }
            else {
                cmd.CommandType = CommandType.Script;
                ComboBoxItem selectedItem = (ComboBoxItem)this.cboScriptType.SelectedItem;
                cmd.ScriptingHost = ((ScriptingHostConfiguration)selectedItem.Value).Name;
            }
            KeysConverter keysConverter = new KeysConverter();
            cmd.Key = (Keys)keysConverter.ConvertFromString(this.txtKeys.Text);
        }

        private void UpdateView() {
            bool flag;
            if (this.cboType.SelectedIndex == -1) {
                this.txtName.Text = string.Empty;
                this.txtFileName.Text = string.Empty;
                this.txtArguments.Text = string.Empty;
                this.cboScriptType.SelectedIndex = -1;
                flag = false;
            }
            else
                flag = true;
            this.txtKeys.Enabled = flag;
            this.txtName.Enabled = flag;
            this.txtFileName.Enabled = flag;
            this.btnFileName.Enabled = flag;
            this.txtArguments.Enabled = flag;
            this.btnArguments.Enabled = flag;
            this.cboScriptType.Enabled = flag;
            this.btnSet.Enabled = flag;
            if (object.ReferenceEquals(this.cboType.SelectedItem, (object)"Script"))
                this.cboScriptType.Enabled = true;
            else
                this.cboScriptType.Enabled = false;
        }

        private void CommandToItem(CommandInfo cmd, ListViewItem item) {
            item.SubItems[1].Text = cmd.Name;
            item.SubItems[2].Text = cmd.CommandType != CommandType.Application ? "Script" : "Application";
            item.SubItems[3].Text = cmd.Command;
            item.SubItems[4].Text = cmd.Arguments;
            item.Tag = (object)cmd;
        }

        private void FormApplications_Load(object sender, EventArgs e) {
            if (DesignMode) {
                return;
            }

            //this.cboType.SelectedIndexChanged += new EventHandler(this.cboType_SelectedIndexChanged);
            //this.lvwCommands.KeyDown += new KeyEventHandler(this.lvwCommands_KeyDown);
            //this.cboType.Items.Add((object)"Application");
            //this.cboType.Items.Add((object)"Script");
            //this.UpdateView();
            //this.lvwCommands.BeginUpdate();
            //this.lvwCommands.Columns.Add("Keys", "Keys", 60);
            //this.lvwCommands.Columns.Add("Name", "Name", 100);
            //this.lvwCommands.Columns.Add("Type", "Type", 80);
            //this.lvwCommands.Columns.Add("FileName", "File Name", 250);
            //this.lvwCommands.Columns.Add("Arguments", "Arguments", 100);
            //for (Keys keys = Keys.F1; keys <= Keys.F12; ++keys) {
            //    ListViewItem listViewItem = this.CreateListViewItem();
            //    listViewItem.Text = string.Format("Ctrl+{0}", (object)keys);
            //    listViewItem.Name = (keys | Keys.Control).ToString();
            //    this.lvwCommands.Items.Add(listViewItem);
            //}
            //for (Keys keys = Keys.F1; keys <= Keys.F12; ++keys) {
            //    ListViewItem listViewItem = this.CreateListViewItem();
            //    listViewItem.Text = string.Format("Shift+{0}", (object)keys);
            //    listViewItem.Name = (keys | Keys.Shift).ToString();
            //    this.lvwCommands.Items.Add(listViewItem);
            //}
            //this.lvwCommands.Items[0].Selected = true;
            //this.lvwCommands.EndUpdate();
            //foreach (ScriptingHostConfiguration hostConfiguration in FsApp.Instance.ScriptingHosts.Values)
            //    this.cboScriptType.Items.Add((object)new ComboBoxItem(hostConfiguration.Name, (object)hostConfiguration));
            //foreach (CommandInfo info in CommandHelper.GetInfos()) {
            //    CommandInfo cmd = new CommandInfo(info);
            //    ListViewItem listViewItem = this.lvwCommands.Items[info.Key.ToString()];
            //    if (listViewItem != null)
            //        this.CommandToItem(cmd, listViewItem);
            //}
            //foreach (ToolStripItem toolStripItem in (ArrangedElementCollection)this.ctxArguments.Items)
            //    toolStripItem.Click += new EventHandler(this.ctxArguments_Items_Click);
        }

        private void FormApplications_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult != DialogResult.OK)
                return;
            KeysConverter keysConverter = new KeysConverter();
            List<CommandInfo> infos = new List<CommandInfo>();
            foreach (ListViewItem listViewItem in this.lvwCommands.Items) {
                CommandInfo tag = (CommandInfo)listViewItem.Tag;
                if (tag != null)
                    infos.Add(tag);
            }
            CommandHelper.SetToConfig((IList<CommandInfo>)infos);
            WindowsApplication.Instance.ConfigurationSource.Save();
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e) => this.UpdateView();

        private void lvwShortCuts_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.lvwCommands.SelectedItems.Count == 0) {
                this.cboType.SelectedIndex = -1;
                this.UpdateView();
            }
            else {
                ListViewItem selectedItem = this.lvwCommands.SelectedItems[0];
                this.txtKeys.Text = selectedItem.Text;
                CommandInfo tag = (CommandInfo)selectedItem.Tag;
                if (tag == null)
                    return;
                if (tag.CommandType == CommandType.Application) {
                    this.cboType.SelectedItem = (object)"Application";
                    this.cboScriptType.SelectedItem = (object)null;
                }
                else {
                    this.cboType.SelectedItem = (object)"Script";
                    ScriptingHostConfiguration hostConfiguration = (ScriptingHostConfiguration)null;
                    if (!string.IsNullOrEmpty(tag.ScriptingHost))
                        FsApp.Instance.ScriptingHosts.TryGetValue(tag.ScriptingHost, out hostConfiguration);
                    this.cboScriptType.SelectedItem = (object)hostConfiguration;
                }
                this.txtName.Text = tag.Name;
                this.txtFileName.Text = tag.Command;
                this.txtArguments.Text = tag.Arguments;
            }
        }

        private void lvwCommands_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Delete)
                return;
            foreach (ListViewItem selectedItem in this.lvwCommands.SelectedItems) {
                selectedItem.SubItems[1].Text = string.Empty;
                selectedItem.SubItems[2].Text = string.Empty;
                selectedItem.SubItems[3].Text = string.Empty;
                selectedItem.SubItems[4].Text = string.Empty;
                selectedItem.Tag = (object)null;
            }
            this.UpdateView();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void ctxArguments_Items_Click(object sender, EventArgs e) => this.txtArguments.SelectedText = ((ToolStripItem)sender).Tag.ToString();

        private void btnFileName_Click(object sender, EventArgs e) {
            if (this.lvwCommands.SelectedItems.Count == 0)
                return;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (object.ReferenceEquals(this.cboType.SelectedItem, (object)"Application"))
                openFileDialog.Filter = "Applications (*.exe)|*.exe|All Files (*.*)|*.*";
            else
                openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.CheckFileExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select a new command file to be executed";
            if (openFileDialog.ShowDialog((IWin32Window)WindowsApplication.Instance.MainForm) != DialogResult.OK)
                return;
            this.txtFileName.Text = openFileDialog.FileName;
        }

        private void btnArguments_Click(object sender, EventArgs e) => this.ctxArguments.Show((Control)this.btnArguments, new Point(this.btnArguments.Width, 0));

        private void btnSet_Click(object sender, EventArgs e) {
            if (this.lvwCommands.SelectedItems.Count == 0)
                return;
            if (string.IsNullOrEmpty(this.txtName.Text) && !string.IsNullOrEmpty(this.txtFileName.Text)) {
                int num1 = (int)MessageBox.Show((IWin32Window)this, "No name was specified.", "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (string.IsNullOrEmpty(this.txtFileName.Text) && !string.IsNullOrEmpty(this.txtName.Text)) {
                int num2 = (int)MessageBox.Show((IWin32Window)this, "No command was specified.", "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (object.ReferenceEquals(this.cboType.SelectedItem, (object)"Script") && this.cboScriptType.SelectedIndex == -1) {
                int num3 = (int)MessageBox.Show((IWin32Window)this, "No scripting host was specified.", "Not saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else {
                ListViewItem selectedItem = this.lvwCommands.SelectedItems[0];
                CommandInfo cmd = (CommandInfo)selectedItem.Tag ?? new CommandInfo();
                this.UpdateCommand(cmd);
                this.CommandToItem(cmd, selectedItem);
            }
        }
    }
}
