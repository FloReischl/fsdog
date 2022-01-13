// Decompiled with JetBrains decompiler
// Type: FsDog.FormRename
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Windows.Forms;
using FsDog.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FsDog {
    public class FormRename : Form {
        private const string CASE_NO_CHANGE = "No changes";
        private const string CASE_UPPER = "All to upper";
        private const string CASE_LOWER = "All to lower";
        private const string CASE_START_UPPER = "Start characters to upper";
        private bool _skipRebuild;
        private Dictionary<string, FileSystemInfo> _otherItems;
        private FormRename.RenameTable _items;
        private Regex _regWildcards;
        private Regex _regCase;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private RichTextBox txtFileName;
        private Label label1;
        private RichTextBox txtExtension;
        private Label label2;
        private RichTextBox txtWith;
        private Label label4;
        private RichTextBox txtReplace;
        private Label label3;
        private Button btnFileName;
        private ComboBox cboCase;
        private Label label5;
        private Button btnOk;
        private Button btnCancel;
        private Button btnExtension;
        private DataGridView dgvFiles;
        private ContextMenuStrip ctxFile;
        private ToolStripMenuItem ctxFileFileName;
        private ToolStripMenuItem ctxFileFileNameXY;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem ctxFileExtension;
        private ToolStripMenuItem ctxFileExtensionXY;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem ctxFileCounterOne;
        private ToolStripMenuItem ctxFileCounterThree;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem ctxFileModifiedYY;
        private ToolStripMenuItem ctxFileModifiedYYYY;
        private ToolStripMenuItem ctxFileModifiedMM;
        private ToolStripMenuItem ctxFileModifiedDD;
        private ToolStripMenuItem ctxFileModifiedHH;
        private ToolStripMenuItem ctxFileModifiedNN;
        private ToolStripMenuItem ctxFileModifiedSS;
        private Label label6;
        private NumericUpDown nudCounterStart;

        public FormRename() {
            this.InitializeComponent();
            this.Load += new EventHandler(this.FormRename_Load);
            this.FormClosing += new FormClosingEventHandler(this.FormRename_FormClosing);
            this.txtFileName.TextChanged += new EventHandler(this.RebuildNames_Event);
            this.txtExtension.TextChanged += new EventHandler(this.RebuildNames_Event);
            this.txtReplace.TextChanged += new EventHandler(this.RebuildNames_Event);
            this.txtWith.TextChanged += new EventHandler(this.RebuildNames_Event);
            this.cboCase.SelectedIndexChanged += new EventHandler(this.RebuildNames_Event);
            this.nudCounterStart.ValueChanged += new EventHandler(this.RebuildNames_Event);
            this.ctxFileFileName.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileFileNameXY.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileExtension.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileExtensionXY.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileCounterOne.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileCounterThree.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileModifiedYY.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileModifiedYYYY.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileModifiedMM.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileModifiedDD.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileModifiedHH.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileModifiedNN.Click += new EventHandler(this.ctxFile_Click);
            this.ctxFileModifiedSS.Click += new EventHandler(this.ctxFile_Click);
        }

        public DirectoryInfo ParentDirectory { get; set; }

        public IList<FileSystemInfo> FileItems { get; set; }

        private void RebuildNames() {
            StringBuilder sb = new StringBuilder();
            if (this._regWildcards == null)
                this._regWildcards = new Regex("(?<FPart>\\[f\\d+\\-\\d+\\])|(?<EPart>\\[e\\d+\\-\\d+\\])|(?<CPart>\\[c+\\])", RegexOptions.Compiled);
            if (this._regCase == null)
                this._regCase = new Regex("\\w*", RegexOptions.Compiled);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            int counter = (int)this.nudCounterStart.Value - 1;
            foreach (FormRename.RenameItem row in (InternalDataCollectionBase)this._items.Rows) {
                ++counter;
                string text1 = this.txtFileName.Text;
                sb.Remove(0, sb.Length);
                sb.Append(text1);
                string input = this.HandleWildcards(sb, row, counter);
                if (!string.IsNullOrEmpty(this.txtReplace.Text))
                    input = input.Replace(this.txtReplace.Text, this.txtWith.Text == null ? "" : this.txtWith.Text);
                if (this.cboCase.SelectedIndex != -1 && !(this.cboCase.SelectedItem.ToString() == "No changes")) {
                    if (this.cboCase.SelectedItem.ToString() == "All to upper")
                        input = input.ToUpper();
                    else if (this.cboCase.SelectedItem.ToString() == "All to lower")
                        input = input.ToLower();
                    else if (this.cboCase.SelectedItem.ToString() == "Start characters to upper") {
                        MatchCollection matchCollection = this._regCase.Matches(input);
                        for (int i = 0; i < matchCollection.Count; ++i) {
                            Match match = matchCollection[i];
                            if (!string.IsNullOrEmpty(match.Value)) {
                                char ch = input[match.Index];
                                input = string.Format("{0}{1}{2}", (object)input.Substring(0, match.Index), (object)ch.ToString().ToUpper(), (object)input.Substring(match.Index + 1));
                            }
                        }
                    }
                }
                string text2 = this.txtExtension.Text;
                sb.Remove(0, sb.Length);
                sb.Append(text2);
                string str = this.HandleWildcards(sb, row, counter);
                string key = string.Format("{0}{1}", (object)input, (object)str);
                if (this._otherItems.ContainsKey(key))
                    row.Error = "Item already exists.";
                else if (dictionary.ContainsKey(key)) {
                    row.Error = "Duplicate name.";
                }
                else {
                    dictionary.Add(key, key);
                    row.Error = string.Empty;
                }
                row.NewName = key;
            }
        }

        private string HandleWildcards(StringBuilder sb, FormRename.RenameItem item, int counter) {
            string str = sb.ToString();
            if (str.Contains("[f]"))
                sb.Replace("[f]", item.Name);
            if (str.Contains("[e]"))
                sb.Replace("[e]", item.Extension);
            if (str.Contains("[yy]"))
                sb.Replace("[yy]", item.FSI.LastWriteTime.Year.ToString().Substring(2));
            if (str.Contains("[yyyy]"))
                sb.Replace("[yyyy]", item.FSI.LastWriteTime.Year.ToString());
            if (str.Contains("[MM]"))
                sb.Replace("[MM]", item.FSI.LastWriteTime.Month.ToString("00"));
            if (str.Contains("[dd]"))
                sb.Replace("[dd]", item.FSI.LastWriteTime.Day.ToString("00"));
            if (str.Contains("[hh]"))
                sb.Replace("[hh]", item.FSI.LastWriteTime.Hour.ToString("00"));
            if (str.Contains("[mm]"))
                sb.Replace("[mm]", item.FSI.LastWriteTime.Minute.ToString("00"));
            if (str.Contains("[ss]"))
                sb.Replace("[ss]", item.FSI.LastWriteTime.Second.ToString("00"));
            string input = sb.ToString();
            while (true) {
                MatchCollection matchCollection = this._regWildcards.Matches(input);
                if (matchCollection.Count != 0) {
                    Match match = matchCollection[0];
                    Group group1 = match.Groups["FPart"];
                    if (group1 != null && !string.IsNullOrEmpty(group1.Value)) {
                        string[] strArray = group1.Value.Trim('[', 'f', ']').Split('-');
                        int num1 = int.Parse(strArray[0]);
                        int num2 = int.Parse(strArray[1]);
                        input = num2 <= num1 || num2 >= item.Name.Length ? (num2 <= num1 || num1 >= item.Name.Length ? input.Replace(group1.Value, "") : input.Replace(group1.Value, item.Name.Substring(num1 - 1))) : input.Replace(group1.Value, item.Name.Substring(num1 - 1, num2 - num1));
                    }
                    else {
                        Group group2 = match.Groups["EPart"];
                        if (group2 != null && !string.IsNullOrEmpty(group2.Value)) {
                            string[] strArray = group2.Value.Trim('[', 'e', ']').Split('-');
                            int num3 = int.Parse(strArray[0]);
                            int num4 = int.Parse(strArray[1]);
                            input = num4 <= num3 || num4 >= item.Extension.Length ? (num4 <= num3 || num3 >= item.Extension.Length ? input.Replace(group2.Value, "") : input.Replace(group2.Value, item.Name.Substring(num3 - 1))) : input.Replace(group2.Value, item.Extension.Substring(num3 - 1, num4 - num3));
                        }
                        else {
                            Group group3 = match.Groups["CPart"];
                            if (group3 != null && !string.IsNullOrEmpty(group3.Value)) {
                                string format = new string('0', group3.Value.Trim('[', ']').Length);
                                input = input.Replace(group3.Value, counter.ToString(format));
                            }
                            else
                                break;
                        }
                    }
                }
                else
                    break;
            }
            return input;
        }

        private void FormRename_Load(object sender, EventArgs e) {
            this._skipRebuild = true;
            this.txtFileName.SelectAll();
            this.txtExtension.SelectAll();
            this.cboCase.Items.Add((object)"No changes");
            this.cboCase.Items.Add((object)"Start characters to upper");
            this.cboCase.Items.Add((object)"All to upper");
            this.cboCase.Items.Add((object)"All to lower");
            this.cboCase.SelectedIndex = 0;
            this._items = new FormRename.RenameTable();
            FsApp instance = FsApp.Instance;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (FileSystemInfo fileItem in (IEnumerable<FileSystemInfo>)this.FileItems) {
                FormRename.RenameItem row = this._items.NewItem();
                dictionary.Add(fileItem.Name, fileItem.Name);
                row.Image = instance.GetFsiImage(fileItem);
                row.OldName = fileItem.Name;
                row.NewName = fileItem.Name;
                row.FSI = fileItem;
                row.Error = string.Empty;
                if (fileItem is DirectoryInfo directoryInfo) {
                    row.Extension = string.Empty;
                    row.Name = directoryInfo.Name;
                }
                if (fileItem is FileInfo fileInfo) {
                    row.Extension = fileInfo.Extension;
                    row.Name = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                }
                this._items.Rows.Add((DataRow)row);
            }
            this._otherItems = new Dictionary<string, FileSystemInfo>();
            foreach (FileSystemInfo fileSystemInfo in this.ParentDirectory.GetFileSystemInfos()) {
                if (!dictionary.ContainsKey(fileSystemInfo.Name))
                    this._otherItems.Add(fileSystemInfo.Name, fileSystemInfo);
            }
            this.dgvFiles.AutoGenerateColumns = false;
            this.dgvFiles.DataSource = (object)this._items.DefaultView;
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.Name = "Image";
            gridViewImageColumn.DataPropertyName = "Image";
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Width = 26;
            gridViewImageColumn.Resizable = DataGridViewTriState.False;
            this.dgvFiles.Columns.Add((DataGridViewColumn)gridViewImageColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.Name = "OldName";
            viewTextBoxColumn1.DataPropertyName = "OldName";
            viewTextBoxColumn1.HeaderText = "Old Name";
            this.dgvFiles.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.Name = "NewName";
            viewTextBoxColumn2.DataPropertyName = "NewName";
            viewTextBoxColumn2.HeaderText = "New Name";
            this.dgvFiles.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.Name = "Error";
            viewTextBoxColumn3.DataPropertyName = "Error";
            viewTextBoxColumn3.HeaderText = "Error";
            viewTextBoxColumn3.Width = 150;
            this.dgvFiles.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            this.dgvFiles.AutoResizeColumn(this.dgvFiles.Columns["OldName"].Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
            this.dgvFiles.AutoResizeColumn(this.dgvFiles.Columns["NewName"].Index, DataGridViewAutoSizeColumnMode.DisplayedCells);
            this._skipRebuild = false;
            this.RebuildNames();
        }

        private void FormRename_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult != DialogResult.OK)
                return;
            foreach (FormRename.RenameItem row in (InternalDataCollectionBase)this._items.Rows) {
                try {
                    if (row.FSI is FileInfo fsi1) {
                        FileInfo fileInfo = new FileInfo(Path.Combine(fsi1.DirectoryName, row.NewName));
                        if (fsi1.Exists && !fileInfo.Exists)
                            fsi1.MoveTo(fileInfo.FullName);
                    }
                    if (row.FSI is DirectoryInfo fsi2) {
                        DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(fsi2.Parent.FullName, row.NewName));
                        if (fsi2.Exists) {
                            if (!directoryInfo.Exists)
                                fsi2.MoveTo(directoryInfo.FullName);
                        }
                    }
                }
                catch (Exception ex) {
                    FormError.ShowException(ex, (IWin32Window)this);
                }
            }
        }

        private void RebuildNames_Event(object sender, EventArgs e) {
            if (this._skipRebuild)
                return;
            this.RebuildNames();
        }

        private void ctxFile_Click(object sender, EventArgs e) {
            if (!(sender is ToolStripMenuItem toolStripMenuItem) || toolStripMenuItem.Tag == null)
                return;
            this.txtFileName.SelectedText = toolStripMenuItem.Tag.ToString();
        }

        protected override void Dispose(bool disposing) {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.components = (IContainer)new Container();
            this.groupBox1 = new GroupBox();
            this.btnFileName = new Button();
            this.txtFileName = new RichTextBox();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.btnExtension = new Button();
            this.txtExtension = new RichTextBox();
            this.label2 = new Label();
            this.groupBox3 = new GroupBox();
            this.cboCase = new ComboBox();
            this.label5 = new Label();
            this.groupBox4 = new GroupBox();
            this.txtWith = new RichTextBox();
            this.label4 = new Label();
            this.txtReplace = new RichTextBox();
            this.label3 = new Label();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.dgvFiles = new DataGridView();
            this.ctxFile = new ContextMenuStrip(this.components);
            this.ctxFileFileName = new ToolStripMenuItem();
            this.ctxFileFileNameXY = new ToolStripMenuItem();
            this.toolStripMenuItem1 = new ToolStripSeparator();
            this.ctxFileExtension = new ToolStripMenuItem();
            this.ctxFileExtensionXY = new ToolStripMenuItem();
            this.toolStripMenuItem2 = new ToolStripSeparator();
            this.ctxFileCounterOne = new ToolStripMenuItem();
            this.ctxFileCounterThree = new ToolStripMenuItem();
            this.toolStripMenuItem3 = new ToolStripSeparator();
            this.ctxFileModifiedYY = new ToolStripMenuItem();
            this.ctxFileModifiedYYYY = new ToolStripMenuItem();
            this.ctxFileModifiedMM = new ToolStripMenuItem();
            this.ctxFileModifiedDD = new ToolStripMenuItem();
            this.ctxFileModifiedHH = new ToolStripMenuItem();
            this.ctxFileModifiedNN = new ToolStripMenuItem();
            this.ctxFileModifiedSS = new ToolStripMenuItem();
            this.nudCounterStart = new NumericUpDown();
            this.label6 = new Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((ISupportInitialize)this.dgvFiles).BeginInit();
            this.ctxFile.SuspendLayout();
            this.nudCounterStart.BeginInit();
            this.SuspendLayout();
            this.groupBox1.Controls.Add((Control)this.btnFileName);
            this.groupBox1.Controls.Add((Control)this.txtFileName);
            this.groupBox1.Controls.Add((Control)this.label1);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(252, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File name";
            this.btnFileName.Image = (Image)Resources.Help;
            this.btnFileName.Location = new Point(217, 19);
            this.btnFileName.Name = "btnFileName";
            this.btnFileName.Size = new Size(24, 21);
            this.btnFileName.TabIndex = 2;
            this.btnFileName.UseVisualStyleBackColor = true;
            this.txtFileName.Location = new Point(64, 19);
            this.txtFileName.Multiline = false;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new Size(147, 21);
            this.txtFileName.TabIndex = 1;
            this.txtFileName.Text = "[f]";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&File name";
            this.groupBox2.Controls.Add((Control)this.btnExtension);
            this.groupBox2.Controls.Add((Control)this.txtExtension);
            this.groupBox2.Controls.Add((Control)this.label2);
            this.groupBox2.Location = new Point(270, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(222, 51);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Extension";
            this.btnExtension.Image = (Image)Resources.Help;
            this.btnExtension.Location = new Point(184, 19);
            this.btnExtension.Name = "btnExtension";
            this.btnExtension.Size = new Size(24, 21);
            this.btnExtension.TabIndex = 2;
            this.btnExtension.UseVisualStyleBackColor = true;
            this.txtExtension.Location = new Point(64, 19);
            this.txtExtension.Multiline = false;
            this.txtExtension.Name = "txtExtension";
            this.txtExtension.Size = new Size(114, 21);
            this.txtExtension.TabIndex = 1;
            this.txtExtension.Text = "[e]";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "&Extension";
            this.groupBox3.Controls.Add((Control)this.label6);
            this.groupBox3.Controls.Add((Control)this.nudCounterStart);
            this.groupBox3.Controls.Add((Control)this.cboCase);
            this.groupBox3.Controls.Add((Control)this.label5);
            this.groupBox3.Location = new Point(270, 69);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(303, 77);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Others";
            this.cboCase.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCase.FormattingEnabled = true;
            this.cboCase.Location = new Point(6, 38);
            this.cboCase.Name = "cboCase";
            this.cboCase.Size = new Size(172, 21);
            this.cboCase.TabIndex = 1;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new Size(76, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Case &Handling";
            this.groupBox4.Controls.Add((Control)this.txtWith);
            this.groupBox4.Controls.Add((Control)this.label4);
            this.groupBox4.Controls.Add((Control)this.txtReplace);
            this.groupBox4.Controls.Add((Control)this.label3);
            this.groupBox4.Location = new Point(12, 69);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(252, 77);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Replace";
            this.txtWith.Location = new Point(125, 38);
            this.txtWith.Multiline = false;
            this.txtWith.Name = "txtWith";
            this.txtWith.Size = new Size(110, 21);
            this.txtWith.TabIndex = 3;
            this.txtWith.Text = "";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(122, 22);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "&With";
            this.txtReplace.Location = new Point(9, 38);
            this.txtReplace.Multiline = false;
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new Size(110, 21);
            this.txtReplace.TabIndex = 1;
            this.txtReplace.Text = "";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new Size(47, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "&Replace";
            this.btnOk.DialogResult = DialogResult.OK;
            this.btnOk.Location = new Point(498, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(498, 41);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.AllowUserToResizeRows = false;
            this.dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Location = new Point(12, 152);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.ReadOnly = true;
            this.dgvFiles.Size = new Size(561, 309);
            this.dgvFiles.TabIndex = 6;
            this.ctxFile.Items.AddRange(new ToolStripItem[16]
            {
        (ToolStripItem) this.ctxFileFileName,
        (ToolStripItem) this.ctxFileFileNameXY,
        (ToolStripItem) this.toolStripMenuItem1,
        (ToolStripItem) this.ctxFileExtension,
        (ToolStripItem) this.ctxFileExtensionXY,
        (ToolStripItem) this.toolStripMenuItem2,
        (ToolStripItem) this.ctxFileCounterOne,
        (ToolStripItem) this.ctxFileCounterThree,
        (ToolStripItem) this.toolStripMenuItem3,
        (ToolStripItem) this.ctxFileModifiedYY,
        (ToolStripItem) this.ctxFileModifiedYYYY,
        (ToolStripItem) this.ctxFileModifiedMM,
        (ToolStripItem) this.ctxFileModifiedDD,
        (ToolStripItem) this.ctxFileModifiedHH,
        (ToolStripItem) this.ctxFileModifiedNN,
        (ToolStripItem) this.ctxFileModifiedSS
            });
            this.ctxFile.Name = "ctxFile";
            this.ctxFile.Size = new Size(219, 308);
            this.ctxFileFileName.Name = "ctxFileFileName";
            this.ctxFileFileName.Size = new Size(218, 22);
            this.ctxFileFileName.Tag = (object)"[f]";
            this.ctxFileFileName.Text = "File name [f]";
            this.ctxFileFileNameXY.Name = "ctxFileFileNameXY";
            this.ctxFileFileNameXY.Size = new Size(218, 22);
            this.ctxFileFileNameXY.Tag = (object)"[f1-4]";
            this.ctxFileFileNameXY.Text = "File name from x to y [f1-4]";
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(215, 6);
            this.ctxFileExtension.Name = "ctxFileExtension";
            this.ctxFileExtension.Size = new Size(218, 22);
            this.ctxFileExtension.Tag = (object)"[e]";
            this.ctxFileExtension.Text = "Extension [e]";
            this.ctxFileExtensionXY.Name = "ctxFileExtensionXY";
            this.ctxFileExtensionXY.Size = new Size(218, 22);
            this.ctxFileExtensionXY.Tag = (object)"[e1-2]";
            this.ctxFileExtensionXY.Text = "Extension from x to y [e1-2]";
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new Size(215, 6);
            this.ctxFileCounterOne.Name = "ctxFileCounterOne";
            this.ctxFileCounterOne.Size = new Size(218, 22);
            this.ctxFileCounterOne.Tag = (object)"[c]";
            this.ctxFileCounterOne.Text = "Counter one digig [c]";
            this.ctxFileCounterThree.Name = "ctxFileCounterThree";
            this.ctxFileCounterThree.Size = new Size(218, 22);
            this.ctxFileCounterThree.Tag = (object)"[ccc]";
            this.ctxFileCounterThree.Text = "Counter three digits [ccc]";
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new Size(215, 6);
            this.ctxFileModifiedYY.Name = "ctxFileModifiedYY";
            this.ctxFileModifiedYY.Size = new Size(218, 22);
            this.ctxFileModifiedYY.Tag = (object)"[yy]";
            this.ctxFileModifiedYY.Text = "Modified year [yy]";
            this.ctxFileModifiedYYYY.Name = "ctxFileModifiedYYYY";
            this.ctxFileModifiedYYYY.Size = new Size(218, 22);
            this.ctxFileModifiedYYYY.Tag = (object)"[yyyy]";
            this.ctxFileModifiedYYYY.Text = "Modified year [yyyy]";
            this.ctxFileModifiedMM.Name = "ctxFileModifiedMM";
            this.ctxFileModifiedMM.Size = new Size(218, 22);
            this.ctxFileModifiedMM.Tag = (object)"[MM]";
            this.ctxFileModifiedMM.Text = "Modified month [MM]";
            this.ctxFileModifiedDD.Name = "ctxFileModifiedDD";
            this.ctxFileModifiedDD.Size = new Size(218, 22);
            this.ctxFileModifiedDD.Tag = (object)"[dd]";
            this.ctxFileModifiedDD.Text = "Modified day [dd]";
            this.ctxFileModifiedHH.Name = "ctxFileModifiedHH";
            this.ctxFileModifiedHH.Size = new Size(218, 22);
            this.ctxFileModifiedHH.Tag = (object)"[hh]";
            this.ctxFileModifiedHH.Text = "Modified hour [hh]";
            this.ctxFileModifiedNN.Name = "ctxFileModifiedNN";
            this.ctxFileModifiedNN.Size = new Size(218, 22);
            this.ctxFileModifiedNN.Tag = (object)"[mm]";
            this.ctxFileModifiedNN.Text = "Modified minute [mm]";
            this.ctxFileModifiedSS.Name = "ctxFileModifiedSS";
            this.ctxFileModifiedSS.Size = new Size(218, 22);
            this.ctxFileModifiedSS.Tag = (object)"[ss]";
            this.ctxFileModifiedSS.Text = "Modified second [ss]";
            this.nudCounterStart.Location = new Point(184, 39);
            this.nudCounterStart.Maximum = new Decimal(new int[4]
            {
        1000000,
        0,
        0,
        0
            });
            this.nudCounterStart.Minimum = new Decimal(new int[4]
            {
        1000000,
        0,
        0,
        int.MinValue
            });
            this.nudCounterStart.Name = "nudCounterStart";
            this.nudCounterStart.Size = new Size(113, 20);
            this.nudCounterStart.TabIndex = 3;
            this.nudCounterStart.TextAlign = HorizontalAlignment.Right;
            this.nudCounterStart.Value = new Decimal(new int[4]
            {
        1,
        0,
        0,
        0
            });
            this.label6.AutoSize = true;
            this.label6.Location = new Point(181, 22);
            this.label6.Name = "label6";
            this.label6.Size = new Size(67, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "&Counter start";
            this.AcceptButton = (IButtonControl)this.btnOk;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = (IButtonControl)this.btnCancel;
            this.ClientSize = new Size(586, 473);
            this.Controls.Add((Control)this.dgvFiles);
            this.Controls.Add((Control)this.btnCancel);
            this.Controls.Add((Control)this.btnOk);
            this.Controls.Add((Control)this.groupBox4);
            this.Controls.Add((Control)this.groupBox3);
            this.Controls.Add((Control)this.groupBox2);
            this.Controls.Add((Control)this.groupBox1);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = nameof(FormRename);
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Rename files";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((ISupportInitialize)this.dgvFiles).EndInit();
            this.ctxFile.ResumeLayout(false);
            this.nudCounterStart.EndInit();
            this.ResumeLayout(false);
        }

        public class RenameTable : DataTable {
            public RenameTable() {
                this.Columns.Add(new DataColumn("Image", typeof(Image)) {
                    AllowDBNull = false
                });
                this.Columns.Add(new DataColumn("OldName", typeof(string)) {
                    AllowDBNull = false
                });
                this.Columns.Add(new DataColumn("NewName", typeof(string)) {
                    AllowDBNull = false
                });
                this.Columns.Add(new DataColumn("Name", typeof(string)) {
                    AllowDBNull = false
                });
                this.Columns.Add(new DataColumn("Extension", typeof(string)) {
                    AllowDBNull = false
                });
                this.Columns.Add(new DataColumn("FSI", typeof(FileSystemInfo)) {
                    AllowDBNull = false
                });
                this.Columns.Add(new DataColumn("Error", typeof(string)) {
                    AllowDBNull = false
                });
            }

            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow)new FormRename.RenameItem(builder);

            public FormRename.RenameItem NewItem() => (FormRename.RenameItem)this.NewRow();
        }

        public class RenameItem : DataRow {
            public RenameItem(DataRowBuilder builder)
              : base(builder) {
            }

            [DisplayName("")]
            public Image Image {
                get => (Image)this[nameof(Image)];
                set => this[nameof(Image)] = (object)value;
            }

            [DisplayName("Old Name")]
            public string OldName {
                get => (string)this[nameof(OldName)];
                set => this[nameof(OldName)] = (object)value;
            }

            [DisplayName("New Name")]
            public string NewName {
                get => (string)this[nameof(NewName)];
                set => this[nameof(NewName)] = (object)value;
            }

            [Browsable(false)]
            public string Name {
                get => (string)this[nameof(Name)];
                set => this[nameof(Name)] = (object)value;
            }

            [Browsable(false)]
            public string Extension {
                get => (string)this[nameof(Extension)];
                set => this[nameof(Extension)] = (object)value;
            }

            [Browsable(false)]
            public FileSystemInfo FSI {
                get => (FileSystemInfo)this[nameof(FSI)];
                set => this[nameof(FSI)] = (object)value;
            }

            public string Error {
                get => (string)this[nameof(Error)];
                set => this[nameof(Error)] = (object)value;
            }
        }
    }
}
