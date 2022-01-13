// Decompiled with JetBrains decompiler
// Type: FsDog.PreviewImage
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FsDog {
    public class PreviewImage : UserControl, IPreviewControl {
        private IContainer components;
        private PictureBox picContent;
        private string _fileName;

        protected override void Dispose(bool disposing) {
            if (disposing && this.components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            picContent = new PictureBox();
            ((ISupportInitialize)this.picContent).BeginInit();
            SuspendLayout();
            picContent.Location = new Point(0, 0);
            picContent.Name = "picContent";
            picContent.Size = new Size(100, 50);
            picContent.TabIndex = 0;
            picContent.TabStop = false;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add((Control)this.picContent);
            Name = nameof(PreviewImage);
            Size = new Size(267, 178);
            ((ISupportInitialize)this.picContent).EndInit();
            ResumeLayout(false);
        }

        public PreviewImage() {
            InitializeComponent();
            picContent.Click += new EventHandler(this.picContent_Click);
        }

        public string FileName => this._fileName;

        public PreviewType PreviewType => PreviewType.Image;

        public void SetFile(string fileName) {
            _fileName = fileName;
            Image image;
            try {
                image = Image.FromFile(fileName);
                picContent.Image = image;
            }
            catch (Exception ex) {
                image = (Image)new Bitmap(100, 100);
                Graphics graphics = Graphics.FromImage(image);
                graphics.DrawString(ex.Message, this.Font, Brushes.Red, new PointF(0.0f, 0.0f));
                graphics.Dispose();
            }
            picContent.SizeMode = PictureBoxSizeMode.Zoom;
            //this.picContent.Size = image.Size;
            picContent.Size = Parent.Size;
            picContent.Image = image;
        }

        private void picContent_Click(object sender, EventArgs e) => this.picContent.Focus();
    }
}
