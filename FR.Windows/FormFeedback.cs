// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormFeedback
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FR.Windows.Forms {
    public class FormFeedback : FormBase {
        //private IContainer components;
        private Button btnClose;
        private RichTextBox txtFeedback;
        private Label lblDescription;

        protected override void Dispose(bool disposing) {
            //if (disposing && this.components != null)
            //    this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.btnClose = new Button();
            this.txtFeedback = new RichTextBox();
            this.lblDescription = new Label();
            this.SuspendLayout();
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Enabled = false;
            this.btnClose.Location = new Point(446, 262);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.txtFeedback.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtFeedback.BackColor = SystemColors.Window;
            this.txtFeedback.Location = new Point(12, 25);
            this.txtFeedback.Name = "txtFeedback";
            this.txtFeedback.ReadOnly = true;
            this.txtFeedback.Size = new Size(509, 231);
            this.txtFeedback.TabIndex = 1;
            this.txtFeedback.Text = "";
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new Point(12, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(110, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Feedback Information";
            this.AcceptButton = (IButtonControl)this.btnClose;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.CancelButton = (IButtonControl)this.btnClose;
            this.ClientSize = new Size(533, 297);
            this.Controls.Add((Control)this.lblDescription);
            this.Controls.Add((Control)this.txtFeedback);
            this.Controls.Add((Control)this.btnClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormFeedback";
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Feedback";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public FormFeedback() => this.InitializeComponent();

        public string Description {
            get => this.lblDescription.Text;
            set => this.lblDescription.Text = value;
        }

        public void Finish() => this.btnClose.Enabled = true;

        public void Add(string msg, params object[] args) {
            if (args != null) {
                for (int index = 0; index < args.Length; ++index) {
                    if (args[index] == null)
                        args[index] = (object)"<NULL>";
                }
                msg = string.Format(msg, args);
            }
            this.txtFeedback.AppendText(msg);
        }

        public void AddRange(IEnumerable<string> messages) {
            this.txtFeedback.SuspendLayout();
            foreach (string message in messages)
                this.Add(message);
            this.txtFeedback.ResumeLayout();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
