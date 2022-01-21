// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormFolderBrowser
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FR.Windows.Forms {
    public class FormFolderBrowser : FormBase {
        private const string DUMMY = "{68586E3B-23B5-419a-9A4D-DAE9469BA306}";
        private bool _startup;
        private DirectoryInfo[] _directories;
        private string _initialDirectory;
        private TreeView tvwFolders;
        private Label lblDescription;
        private Button btnOk;
        private Button btnCancel;
        private ComboBox cboDrive;

        public DirectoryInfo[] Directories => this._directories;

        public string InitialDirectory {
            get => this._initialDirectory;
            set => this._initialDirectory = value;
        }

        public FormFolderBrowser() => this.InitializeComponent();

        private void CFormFolderBrowser_Load(object sender, EventArgs e) {
            this._startup = true;
            this.tvwFolders.CheckBoxes = true;
            foreach (object logicalDrive in Directory.GetLogicalDrives())
                this.cboDrive.Items.Add(logicalDrive);
            this.cboDrive.SelectedItem = (object)Directory.GetDirectoryRoot(this.InitialDirectory == null || !Directory.Exists(this.InitialDirectory) ? Environment.GetFolderPath(Environment.SpecialFolder.System) : this.InitialDirectory);
        }

        private void btnOk_Click(object sender, EventArgs e) {
            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            foreach (TreeNode node in this.tvwFolders.Nodes)
                this.getCheckedDirectories(node, dirs);
            this._directories = dirs.ToArray();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tvwFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e) {
            TreeNode node = e.Node;
            if (node.Nodes.Count != 1 || !(node.Nodes[0].Text == "{68586E3B-23B5-419a-9A4D-DAE9469BA306}"))
                return;
            this.getChildren(node, true);
        }

        private void tvwFolders_AfterCheck(object sender, TreeViewEventArgs e) {
            TreeNode node1 = e.Node;
            Cursor current = Cursor.Current;
            try {
                this.Cursor = Cursors.WaitCursor;
                if (node1.Checked && node1.Nodes.Count == 1 && node1.Nodes[0].Text == "{68586E3B-23B5-419a-9A4D-DAE9469BA306}")
                    this.getChildren(node1, false);
                foreach (TreeNode node2 in node1.Nodes)
                    node2.Checked = node1.Checked;
                if (!node1.Checked)
                    return;
                node1.Expand();
            }
            catch (Exception ex) {
                FormError.ShowException(ex, (IWin32Window)this);
                node1.Checked = false;
            }
            finally {
                this.Cursor = current;
            }
        }

        private void cboDrive_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                DirectoryInfo directoryInfo = new DirectoryInfo(this.cboDrive.SelectedItem.ToString());
                directoryInfo.GetDirectories();
                this.tvwFolders.BeginUpdate();
                this.tvwFolders.Nodes.Clear();
                TreeNode treeNode1 = new TreeNode(this.cboDrive.SelectedItem.ToString());
                treeNode1.Tag = (object)directoryInfo;
                this.getChildren(treeNode1, true);
                this.tvwFolders.Nodes.Add(treeNode1);
                if (this._startup && this.InitialDirectory != null) {
                    TreeNode treeNode2 = treeNode1;
                    string[] strArray = this.InitialDirectory.Split('\\');
                    for (int index = 1; index < strArray.Length; ++index) {
                        TreeNode[] treeNodeArray = treeNode2.Nodes.Find(strArray[index], false);
                        if (treeNodeArray.Length == 1) {
                            treeNode2 = treeNodeArray[0];
                            treeNode2.Expand();
                        }
                        else
                            break;
                    }
                    this.tvwFolders.SelectedNode = treeNode2;
                    treeNode2.EnsureVisible();
                }
                this._startup = false;
                treeNode1.Expand();
            }
            catch (Exception ex) {
                FormError.ShowException(ex, (IWin32Window)this);
            }
            finally {
                this.tvwFolders.EndUpdate();
            }
        }

        private void getChildren(TreeNode parent, bool handleException) {
            try {
                parent.Nodes.Clear();
                foreach (DirectoryInfo directory in ((DirectoryInfo)parent.Tag).GetDirectories())
                    parent.Nodes.Add(new TreeNode() {
                        Text = directory.Name,
                        Name = directory.Name,
                        Tag = (object)directory,
                        Nodes = {
              "{68586E3B-23B5-419a-9A4D-DAE9469BA306}"
            }
                    });
            }
            catch (Exception ex) {
                if (!handleException)
                    throw ex;
                FormError.ShowException(ex, (IWin32Window)this);
            }
        }

        private void getCheckedDirectories(TreeNode parent, List<DirectoryInfo> dirs) {
            if (parent.Checked)
                dirs.Add((DirectoryInfo)parent.Tag);
            foreach (TreeNode node in parent.Nodes)
                this.getCheckedDirectories(node, dirs);
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.tvwFolders = new TreeView();
            this.lblDescription = new Label();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.cboDrive = new ComboBox();
            this.SuspendLayout();
            this.tvwFolders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.tvwFolders.Location = new Point(12, 30);
            this.tvwFolders.Name = "tvwFolders";
            this.tvwFolders.Size = new Size(293, 237);
            this.tvwFolders.TabIndex = 0;
            this.tvwFolders.AfterCheck += new TreeViewEventHandler(this.tvwFolders_AfterCheck);
            this.tvwFolders.BeforeExpand += new TreeViewCancelEventHandler(this.tvwFolders_BeforeExpand);
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new Point(12, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(147, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Select one or more directories";
            this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnOk.Location = new Point(149, 273);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(230, 273);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.cboDrive.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDrive.FormattingEnabled = true;
            this.cboDrive.Location = new Point(211, 3);
            this.cboDrive.Name = "cboDrive";
            this.cboDrive.Size = new Size(94, 21);
            this.cboDrive.TabIndex = 6;
            this.cboDrive.SelectedIndexChanged += new EventHandler(this.cboDrive_SelectedIndexChanged);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.ClientSize = new Size(317, 308);
            this.Controls.Add((Control)this.cboDrive);
            this.Controls.Add((Control)this.btnOk);
            this.Controls.Add((Control)this.btnCancel);
            this.Controls.Add((Control)this.lblDescription);
            this.Controls.Add((Control)this.tvwFolders);
            this.MinimizeBox = false;
            this.Name = "CFormFolderBrowser";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Select Directory";
            this.Load += new EventHandler(this.CFormFolderBrowser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
