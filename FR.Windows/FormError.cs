// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormError
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FR.Windows.Forms {
    public class FormError : FormBase {
        //private IContainer components;
        private TextBox txtMessage;
        private Label label1;
        private Label label2;
        private TextBox txtDescription;
        private Button btnOk;
        private LinkLabel lnkCopyToClipboard;
        private PictureBox pictureBox1;

        private FormError() => this.InitializeComponent();

        public static void ShowError(string error, string description, IWin32Window parent) {
            FormError formError = new FormError();
            formError.Error = error;
            formError.Description = description;
            if (parent == null)
                parent = (IWin32Window)Form.ActiveForm;
            if (parent == null) {
                int num1 = (int)formError.ShowDialog();
            }
            else {
                int num2 = (int)formError.ShowDialog(parent);
            }
        }

        public static void ShowException(Exception ex, IWin32Window parent) {
            if (ex == null)
                ex = new Exception("No Exception");
            FormError formError = new FormError();
            formError.Exception = ex;
            formError.Error = ex.Message;
            StringBuilder stringBuilder = new StringBuilder(ExceptionHelper.GetCompleteMessage(ex));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("Stack-trace:");
            stringBuilder.AppendLine(ex.StackTrace.ToString());
            formError.Description = stringBuilder.ToString();
            if (parent == null)
                parent = (IWin32Window)Form.ActiveForm;
            if (parent != null) {
                int num1 = (int)formError.ShowDialog(parent);
            }
            else {
                int num2 = (int)formError.ShowDialog();
            }
        }

        public Exception Exception { get; private set; }

        public string Error { get; private set; }

        public string Description { get; private set; }

        private void FormError_Load(object sender, EventArgs e) {
            this.txtMessage.Text = this.Error;
            this.txtDescription.Text = this.Description;
        }

        private void lnkCopyToClipboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Clipboard.Clear();
            Clipboard.SetText(ExceptionHelper.GetCompleteMessage(this.Exception));
        }

        private void btnOk_Click(object sender, EventArgs e) => this.Close();

        protected override void Dispose(bool disposing) {
            //if (disposing && this.components != null)
            //    this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormError));
            this.txtMessage = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtDescription = new TextBox();
            this.btnOk = new Button();
            this.lnkCopyToClipboard = new LinkLabel();
            this.pictureBox1 = new PictureBox();
            ((ISupportInitialize)this.pictureBox1).BeginInit();
            this.SuspendLayout();
            this.txtMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtMessage.Location = new Point(84, 25);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = ScrollBars.Vertical;
            this.txtMessage.Size = new Size(398, 47);
            this.txtMessage.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(84, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Error Message";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(84, 85);
            this.label2.Name = "label2";
            this.label2.Size = new Size(102, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Detailed Description";
            this.txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtDescription.Location = new Point(84, 101);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;
            this.txtDescription.Size = new Size(398, 138);
            this.txtDescription.TabIndex = 4;
            this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnOk.DialogResult = DialogResult.Cancel;
            this.btnOk.Location = new Point(407, 245);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new EventHandler(this.btnOk_Click);
            this.lnkCopyToClipboard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lnkCopyToClipboard.AutoSize = true;
            this.lnkCopyToClipboard.Location = new Point(84, 250);
            this.lnkCopyToClipboard.Name = "lnkCopyToClipboard";
            this.lnkCopyToClipboard.Size = new Size(94, 13);
            this.lnkCopyToClipboard.TabIndex = 5;
            this.lnkCopyToClipboard.TabStop = true;
            this.lnkCopyToClipboard.Text = "Copy To Clipboard";
            this.lnkCopyToClipboard.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkCopyToClipboard_LinkClicked);
            this.pictureBox1.Image = (Image)componentResourceManager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(66, 70);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.AcceptButton = (IButtonControl)this.btnOk;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.CancelButton = (IButtonControl)this.btnOk;
            this.ClientSize = new Size(494, 286);
            this.Controls.Add((Control)this.pictureBox1);
            this.Controls.Add((Control)this.lnkCopyToClipboard);
            this.Controls.Add((Control)this.btnOk);
            this.Controls.Add((Control)this.txtDescription);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.txtMessage);
            this.Name = "CFormError";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Error";
            this.Load += new EventHandler(this.FormError_Load);
            ((ISupportInitialize)this.pictureBox1).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
