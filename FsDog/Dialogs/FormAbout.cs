// Decompiled with JetBrains decompiler
// Type: FsDog.FormAbout
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FsDog.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FsDog {
    public class FormAbout : Form {
        //private IContainer components;
        private PictureBox picDog;
        private Label lblProductName;
        private Label lblVersion;
        private Label lblCopyright;
        private Label lblTitle;
        private Label lblDescription;

        protected override void Dispose(bool disposing) {
            //if (disposing && this.components != null)
            //    this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.picDog = new PictureBox();
            this.lblProductName = new Label();
            this.lblVersion = new Label();
            this.lblCopyright = new Label();
            this.lblTitle = new Label();
            this.lblDescription = new Label();
            ((ISupportInitialize)this.picDog).BeginInit();
            this.SuspendLayout();
            this.picDog.Location = new Point(12, 12);
            this.picDog.Name = "picDog";
            this.picDog.Size = new Size(274, 151);
            this.picDog.SizeMode = PictureBoxSizeMode.CenterImage;
            this.picDog.TabIndex = 0;
            this.picDog.TabStop = false;
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new Point(12, 190);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new Size(82, 13);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "lblProductName";
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new Point(12, 203);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new Size(52, 13);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "lblVersion";
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new Point(12, 249);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new Size(61, 13);
            this.lblCopyright.TabIndex = 4;
            this.lblCopyright.Text = "lblCopyright";
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(12, 177);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(37, 13);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "lblTitle";
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new Point(12, 216);
            this.lblDescription.MaximumSize = new Size(274, 30);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(70, 13);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "lblDescription";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(298, 281);
            this.Controls.Add((Control)this.lblDescription);
            this.Controls.Add((Control)this.lblTitle);
            this.Controls.Add((Control)this.lblCopyright);
            this.Controls.Add((Control)this.lblVersion);
            this.Controls.Add((Control)this.lblProductName);
            this.Controls.Add((Control)this.picDog);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = nameof(FormAbout);
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = nameof(FormAbout);
            ((ISupportInitialize)this.picDog).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public FormAbout() {
            this.InitializeComponent();
            this.Load += new EventHandler(this.FormAbout_Load);
            this.KeyDown += new KeyEventHandler(this.FormAbout_KeyDown);
        }

        private void FormAbout_Load(object sender, EventArgs e) {
            Bitmap fsDogSplash = Resources.FsDogSplash;
            this.picDog.Image = (Image)fsDogSplash;
            this.BackColor = fsDogSplash.GetPixel(0, 0);
            FsApp instance = FsApp.Instance;
            this.Text = instance.Information.Title;
            this.lblTitle.Text = string.Format("Title: {0}", (object)instance.Information.Title);
            this.lblProductName.Text = string.Format("Product Name: {0}", (object)instance.Information.ProductName);
            this.lblVersion.Text = string.Format("Version: {0}", (object)instance.Information.Version);
            this.lblDescription.Text = instance.Information.Description;
            this.lblCopyright.Text = instance.Information.LegalCopyright;
        }

        private void FormAbout_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Escape)
                return;
            this.Close();
            e.SuppressKeyPress = true;
        }
    }
}
