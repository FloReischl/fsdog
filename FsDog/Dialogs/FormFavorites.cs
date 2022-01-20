// Decompiled with JetBrains decompiler
// Type: FsDog.FormFavorites
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Configuration;
using FR.Windows.Forms;
using FsDog.Commands;
using FsDog.Configuration;
using FsDog.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FsDog {
    public class FormFavorites : FormBase {
        private FsDogConfig _config;
        //private IContainer components;
        private ListView lvwFavorites;
        private ColumnHeader columnHeader1;
        private Label label1;
        private Label label2;
        private TextBox txtFavorite;
        private Button btnBrowse;
        private Button btnCancel;
        private Button btnOk;
        private LinkLabel btnDelete;
        private LinkLabel btnUpdate;
        private LinkLabel btnAdd;
        private LinkLabel btnUp;
        private LinkLabel btnDown;

        protected override void Dispose(bool disposing) {
            //if (disposing && this.components != null)
            //    this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.lvwFavorites = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtFavorite = new TextBox();
            this.btnBrowse = new Button();
            this.btnCancel = new Button();
            this.btnOk = new Button();
            this.btnDelete = new LinkLabel();
            this.btnUpdate = new LinkLabel();
            this.btnAdd = new LinkLabel();
            this.btnUp = new LinkLabel();
            this.btnDown = new LinkLabel();
            this.SuspendLayout();
            this.lvwFavorites.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.lvwFavorites.Columns.AddRange(new ColumnHeader[1]
            {
        this.columnHeader1
            });
            this.lvwFavorites.FullRowSelect = true;
            this.lvwFavorites.HideSelection = false;
            this.lvwFavorites.Location = new Point(12, 25);
            this.lvwFavorites.Name = "lvwFavorites";
            this.lvwFavorites.Size = new Size(558, 210);
            this.lvwFavorites.TabIndex = 0;
            this.lvwFavorites.UseCompatibleStateImageBehavior = false;
            this.lvwFavorites.View = View.Details;
            this.columnHeader1.Text = "Directory";
            this.columnHeader1.Width = 500;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(227, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Add remove or change your favorite directories";
            this.label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(9, 267);
            this.label2.Name = "label2";
            this.label2.Size = new Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Directory";
            this.txtFavorite.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtFavorite.Location = new Point(64, 263);
            this.txtFavorite.Name = "txtFavorite";
            this.txtFavorite.Size = new Size(467, 20);
            this.txtFavorite.TabIndex = 3;
            this.btnBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnBrowse.Location = new Point(537, 263);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new Size(33, 20);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(495, 314);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnOk.DialogResult = DialogResult.OK;
            this.btnOk.Location = new Point(414, 314);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnDelete.AutoSize = true;
            this.btnDelete.Location = new Point(493, 286);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(38, 13);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.TabStop = true;
            this.btnDelete.Text = "Delete";
            this.btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnUpdate.AutoSize = true;
            this.btnUpdate.Location = new Point(445, 286);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(42, 13);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.TabStop = true;
            this.btnUpdate.Text = "Update";
            this.btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnAdd.AutoSize = true;
            this.btnAdd.Location = new Point(413, 286);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(26, 13);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.TabStop = true;
            this.btnAdd.Text = "Add";
            this.btnUp.AutoSize = true;
            this.btnUp.Location = new Point(508, 238);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new Size(21, 13);
            this.btnUp.TabIndex = 10;
            this.btnUp.TabStop = true;
            this.btnUp.Text = "Up";
            this.btnDown.AutoSize = true;
            this.btnDown.Location = new Point(535, 238);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new Size(35, 13);
            this.btnDown.TabIndex = 11;
            this.btnDown.TabStop = true;
            this.btnDown.Text = "Down";
            this.AcceptButton = (IButtonControl)this.btnOk;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = (IButtonControl)this.btnCancel;
            this.ClientSize = new Size(582, 349);
            this.Controls.Add((Control)this.btnDown);
            this.Controls.Add((Control)this.btnUp);
            this.Controls.Add((Control)this.btnAdd);
            this.Controls.Add((Control)this.btnUpdate);
            this.Controls.Add((Control)this.btnDelete);
            this.Controls.Add((Control)this.btnOk);
            this.Controls.Add((Control)this.btnCancel);
            this.Controls.Add((Control)this.btnBrowse);
            this.Controls.Add((Control)this.txtFavorite);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.lvwFavorites);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = nameof(FormFavorites);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Edit Favorites";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public FormFavorites() {
            this.InitializeComponent();
            this.Load += new EventHandler(this.FormFavorites_Load);
            this.FormClosing += new FormClosingEventHandler(this.FormFavorites_FormClosing);
            this.lvwFavorites.SelectedIndexChanged += new EventHandler(this.lvwFavorites_SelectedIndexChanged);
            this.btnUp.Click += new EventHandler(this.btnUp_Click);
            this.btnDown.Click += new EventHandler(this.btnDown_Click);
            this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
        }

        public string StartupFavorite { get; set; }

        private bool ValidateFavorite() {
            if (this.txtFavorite.TextLength == 0) {
                int num = (int)MessageBox.Show((IWin32Window)this, "Cannot add empty directory path.", "Empty Favorites", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            try {
                DirectoryInfo directoryInfo = new DirectoryInfo(this.txtFavorite.Text);
                return directoryInfo.Exists || DialogResult.Yes == MessageBox.Show((IWin32Window)this, string.Format("Directory '{0}' cannot be found. Do you anyway want to add it to favorites?", (object)directoryInfo.FullName), "Unknown Path", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            }
            catch (Exception ex) {
                int num = (int)MessageBox.Show((IWin32Window)this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        private void FormFavorites_Load(object sender, EventArgs e) {
            this.Icon = Icon.FromHandle(new Bitmap((Image)Resources.FavoritesEdit).GetHicon());

            _config = FsApp.Instance.Config;
            //this.ConfigurationRoot = FsApp.Instance.ConfigurationSource.GetProperty(".", "Favorites", true);

            foreach (FavoriteInfo info in CmdFavorite.GetInfos())
                this.lvwFavorites.Items.Add(new ListViewItem(info.DirectoryName));

            this.txtFavorite.Text = this.StartupFavorite;
        }

        private void FormFavorites_FormClosing(object sender, FormClosingEventArgs e) {
            if (this.DialogResult != DialogResult.OK)
                return;

            _config.Favorites = lvwFavorites.Items.Cast<ListViewItem>().Select(lvi => lvi.Text).ToList();
            _config.Save();
            //foreach (IConfigurationProperty subProperty in this.ConfigurationRoot.GetSubProperties("Item"))
            //    subProperty.Delete();

            //foreach (ListViewItem listViewItem in this.lvwFavorites.Items)
            //    this.ConfigurationRoot.AddSubProperty("Item").GetSubProperty("Directory", true).Set(listViewItem.Text);

            //FsApp.Instance.ConfigurationSource.Save();
        }

        private void lvwFavorites_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.lvwFavorites.SelectedItems.Count == 0)
                return;
            this.txtFavorite.Text = this.lvwFavorites.SelectedItems[0].Text;
        }

        private void btnUp_Click(object sender, EventArgs e) {
            this.lvwFavorites.BeginUpdate();
            for (int index1 = 0; index1 < this.lvwFavorites.SelectedItems.Count; ++index1) {
                ListViewItem selectedItem = this.lvwFavorites.SelectedItems[index1];
                int index2 = selectedItem.Index;
                if (index2 != 0) {
                    this.lvwFavorites.Items.RemoveAt(index2);
                    this.lvwFavorites.Items.Insert(index2 - 1, selectedItem);
                    selectedItem.Selected = true;
                }
                else
                    break;
            }
            this.lvwFavorites.EndUpdate();
        }

        private void btnDown_Click(object sender, EventArgs e) {
            this.lvwFavorites.BeginUpdate();
            for (int index1 = this.lvwFavorites.SelectedItems.Count - 1; index1 >= 0; --index1) {
                ListViewItem selectedItem = this.lvwFavorites.SelectedItems[index1];
                int index2 = selectedItem.Index;
                if (index2 != this.lvwFavorites.Items.Count - 1) {
                    this.lvwFavorites.Items.RemoveAt(index2);
                    this.lvwFavorites.Items.Insert(index2 + 1, selectedItem);
                    selectedItem.Selected = true;
                }
                else
                    break;
            }
            this.lvwFavorites.EndUpdate();
        }

        private void btnBrowse_Click(object sender, EventArgs e) {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Select a new favorite directory.";
            if (folderBrowserDialog.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            this.txtFavorite.Text = folderBrowserDialog.SelectedPath;
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            foreach (ListViewItem selectedItem in this.lvwFavorites.SelectedItems)
                selectedItem.Remove();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if (!this.ValidateFavorite())
                return;
            this.lvwFavorites.Items.Add(new ListViewItem(this.txtFavorite.Text));
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            if (this.lvwFavorites.SelectedItems.Count == 0 || !this.ValidateFavorite())
                return;
            this.lvwFavorites.SelectedItems[0].Text = this.txtFavorite.Text;
        }
    }
}
