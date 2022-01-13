// Decompiled with JetBrains decompiler
// Type: FsDog.PreviewContainer
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FsDog.Detail {
    public class PreviewContainer : UserControl {
        private Dictionary<PreviewType, IPreviewControl> _loadedControls;
        private IPreviewControl _currentPreview;
        //private IContainer components;
        private Panel panel1;
        private Label label1;
        private ComboBox cboType;
        private Panel pnlContent;

        public PreviewContainer() => this.InitializeComponent();

        public void SetFile(string fileName) {
            if (this._currentPreview != null && this._currentPreview.PreviewType == PreviewInfo.GetTypeForFile(fileName)) {
                this._currentPreview.SetFile(fileName);
            }
            else {
                PreviewType previewType = (PreviewType)((ComboBoxItem)this.cboType.SelectedItem).Value;
                IPreviewControl control1;
                if (!this._loadedControls.TryGetValue(previewType, out control1)) {
                    control1 = PreviewInfo.GetControl(previewType, fileName);
                    this._currentPreview = control1;
                }
                this.pnlContent.Controls.Clear();
                if (control1 != null) {
                    Control control2 = (Control)control1;
                    control2.Dock = DockStyle.Fill;
                    this.pnlContent.Controls.Add(control2);
                    control1.SetFile(fileName);
                }
                else
                    new Label().Text = "Unknown file type";
            }
        }

        protected override void InitLayout() {
            base.InitLayout();
            this._loadedControls = new Dictionary<PreviewType, IPreviewControl>();
            this.cboType.Items.Add((object)new ComboBoxItem("Auto Detect", (object)PreviewType.AutoDetect));
            this.cboType.Items.Add((object)new ComboBoxItem("Text", (object)PreviewType.Text));
            this.cboType.Items.Add((object)new ComboBoxItem("Image", (object)PreviewType.Image));
            this.cboType.SelectedIndex = 0;
        }

        protected override void Dispose(bool disposing) {
            //if (disposing && this.components != null)
            //  this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.panel1 = new Panel();
            this.label1 = new Label();
            this.cboType = new ComboBox();
            this.pnlContent = new Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            this.panel1.Controls.Add((Control)this.label1);
            this.panel1.Controls.Add((Control)this.cboType);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(424, 27);
            this.panel1.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type";
            this.cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new Point(40, 3);
            this.cboType.Name = "cboType";
            this.cboType.Size = new Size(143, 21);
            this.cboType.TabIndex = 0;
            this.pnlContent.Dock = DockStyle.Fill;
            this.pnlContent.Location = new Point(0, 27);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new Size(424, 276);
            this.pnlContent.TabIndex = 1;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add((Control)this.pnlContent);
            this.Controls.Add((Control)this.panel1);
            this.Name = nameof(PreviewContainer);
            this.Size = new Size(424, 303);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
