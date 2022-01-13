// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormLoggingDevice
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Commands;
using FR.Configuration;
using FR.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FR.Windows.Forms {
    public class FormLoggingDevice : FormBase, ILoggingDevice, IDisposable {
        //private IContainer components;
        private Button btnClose;
        private LinkLabel lnkCopyToClipboard;
        private LinkLabel lnkClear;
        private ListBox lstLogging;
        private Label lblCount;
        private List<string> _buffer;
        private FR.Logging.LogLevel _logLevel;
        private FR.Logging.LogLevel _stackTrace;
        private bool _isDisposed;

        protected override void Dispose(bool disposing) {
            //if (disposing && this.components != null)
            //    this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.btnClose = new Button();
            this.lnkCopyToClipboard = new LinkLabel();
            this.lnkClear = new LinkLabel();
            this.lstLogging = new ListBox();
            this.lblCount = new Label();
            this.SuspendLayout();
            this.btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnClose.Location = new Point(453, 354);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.lnkCopyToClipboard.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lnkCopyToClipboard.AutoSize = true;
            this.lnkCopyToClipboard.Location = new Point(12, 351);
            this.lnkCopyToClipboard.Name = "lnkCopyToClipboard";
            this.lnkCopyToClipboard.Size = new Size(94, 13);
            this.lnkCopyToClipboard.TabIndex = 7;
            this.lnkCopyToClipboard.TabStop = true;
            this.lnkCopyToClipboard.Text = "Copy To Clipboard";
            this.lnkCopyToClipboard.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkCopyToClipboard_LinkClicked);
            this.lnkClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lnkClear.AutoSize = true;
            this.lnkClear.Location = new Point(112, 351);
            this.lnkClear.Name = "lnkClear";
            this.lnkClear.Size = new Size(31, 13);
            this.lnkClear.TabIndex = 8;
            this.lnkClear.TabStop = true;
            this.lnkClear.Text = "Clear";
            this.lnkClear.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkClear_LinkClicked);
            this.lstLogging.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.lstLogging.FormattingEnabled = true;
            this.lstLogging.HorizontalScrollbar = true;
            this.lstLogging.Location = new Point(12, 12);
            this.lstLogging.Name = "lstLogging";
            this.lstLogging.ScrollAlwaysVisible = true;
            this.lstLogging.SelectionMode = SelectionMode.MultiExtended;
            this.lstLogging.Size = new Size(516, 329);
            this.lstLogging.TabIndex = 9;
            this.lblCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new Point(149, 354);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new Size(45, 13);
            this.lblCount.TabIndex = 10;
            this.lblCount.Text = "lblCount";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.ClientSize = new Size(540, 389);
            this.Controls.Add((Control)this.lblCount);
            this.Controls.Add((Control)this.lstLogging);
            this.Controls.Add((Control)this.lnkClear);
            this.Controls.Add((Control)this.lnkCopyToClipboard);
            this.Controls.Add((Control)this.btnClose);
            this.KeyPreview = true;
            this.Name = "CFormLogging";
            this.Text = "Logging Window";
            this.FormClosed += new FormClosedEventHandler(this.CFormLogging_FormClosed);
            this.KeyDown += new KeyEventHandler(this.CFormLogging_KeyDown);
            this.Load += new EventHandler(this.CFormLogging_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public FormLoggingDevice() {
            this.InitializeComponent();
            this._isDisposed = false;
            this._buffer = new List<string>();
            this.lblCount.Text = "Rows: 0";
        }

        private void CFormLogging_Load(object sender, EventArgs e) {
        }

        private void CFormLogging_FormClosed(object sender, FormClosedEventArgs e) {
            this._isDisposed = true;
            if (base.IsDisposed)
                return;
            base.Dispose();
        }

        private void CFormLogging_KeyDown(object sender, KeyEventArgs e) {
            if (!e.Control || e.KeyCode != Keys.C)
                return;
            this.copyToClipboard();
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void lnkCopyToClipboard_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => this.copyToClipboard();

        private void lnkClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.lstLogging.Items.Clear();
            this.lblCount.Text = "Rows: 0";
            this._buffer.Clear();
        }

        public new FR.Logging.LogLevel LogLevel {
            get => this._logLevel;
            set => this._logLevel = value;
        }

        public FR.Logging.LogLevel StackTraceLevel {
            get => this._stackTrace;
            set => this._stackTrace = value;
        }

        public new bool IsDisposed => this._isDisposed;

        public void Open(IConfigurationProperty deviceConfiguration) {
            this.Show();
            if (deviceConfiguration != null) {
                this.LogLevel = (FR.Logging.LogLevel)deviceConfiguration.GetSubProperty("LogLevel", true).ToUInt32(7U);
                this.StackTraceLevel = (FR.Logging.LogLevel)deviceConfiguration.GetSubProperty("StackTrace", true).ToUInt32(1U);
            }
            else {
                this.LogLevel = FR.Logging.LogLevel.Default;
                this.StackTraceLevel = FR.Logging.LogLevel.Exception;
            }
        }

        public void Log(
          FR.Logging.LogLevel logLevel,
          string message,
          DateTime dateTime,
          string className,
          string methodName,
          int skipFrames) {
            string str1 = (string)null;
            if ((logLevel & this.LogLevel) == logLevel) {
                str1 = this.getLinePrefix(logLevel, dateTime);
                this._buffer.Add(string.Format("{0}{1}", (object)str1, (object)message));
            }
            if ((logLevel & this.StackTraceLevel) != logLevel)
                return;
            if (str1 == null)
                str1 = this.getLinePrefix(logLevel, dateTime);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(str1);
            stringBuilder.AppendLine("Stack Trace");
            StackTrace stackTrace = new StackTrace(skipFrames, false);
            int num = 0;
            foreach (StackFrame frame in stackTrace.GetFrames()) {
                stringBuilder.Append(str1);
                stringBuilder.Append("Frame: ");
                stringBuilder.Append(num++);
                stringBuilder.Append(" | File: ");
                stringBuilder.Append(frame.GetFileName());
                stringBuilder.Append(" | Line: ");
                stringBuilder.Append(" :: ");
                stringBuilder.Append(frame.ToString());
            }
            string str2 = stringBuilder.ToString();
            char[] separator = new char[2] { '\r', '\n' };
            foreach (string str3 in str2.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                this._buffer.Add(str3);
        }

        public void ForceLog(
          FR.Logging.LogLevel logLevel,
          string message,
          DateTime dateTime,
          string className,
          string methodName,
          int skipFrames) {
            FR.Logging.LogLevel logLevel1 = this.LogLevel;
            this.LogLevel = logLevel;
            this.Log(logLevel, message, dateTime, className, methodName, skipFrames + 1);
            this.LogLevel = logLevel1;
        }

        public void Flush() {
            this.lstLogging.BeginUpdate();
            foreach (object obj in this._buffer)
                this.lstLogging.Items.Add(obj);
            this._buffer.Clear();
            this.lstLogging.EndUpdate();
            this.lstLogging.TopIndex = this.lstLogging.Items.Count - 1;
            this.lblCount.Text = string.Format("Rows: {0}", (object)this.lstLogging.Items.Count);
        }

        public override ICommandReceiver GetCommandReceiver(System.Type commandType) => this.ApplicationInstance != null && this.ApplicationInstance.MainForm != null && this.ApplicationInstance.MainForm is ICommandReceiver ? ((ICommandReceiver)this.ApplicationInstance.MainForm).GetCommandReceiver(commandType) : (ICommandReceiver)null;

        private string getLinePrefix(FR.Logging.LogLevel logLevel, DateTime dateTime) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(dateTime.ToString("yyyyMMdd HHmmssfff"));
            stringBuilder.Append(";");
            if ((logLevel & FR.Logging.LogLevel.Exception) != FR.Logging.LogLevel.None)
                stringBuilder.Append("EX");
            else if ((logLevel & FR.Logging.LogLevel.Error) != FR.Logging.LogLevel.None)
                stringBuilder.Append("ER");
            else if ((logLevel & FR.Logging.LogLevel.Warning) != FR.Logging.LogLevel.None)
                stringBuilder.Append("W");
            else if ((logLevel & FR.Logging.LogLevel.Info) != FR.Logging.LogLevel.None)
                stringBuilder.Append("I");
            else if ((logLevel & FR.Logging.LogLevel.Debug) != FR.Logging.LogLevel.None)
                stringBuilder.Append("DBG");
            else if ((logLevel & FR.Logging.LogLevel.Trace) != FR.Logging.LogLevel.None)
                stringBuilder.Append("TRC");
            else if ((logLevel & FR.Logging.LogLevel.User1) != FR.Logging.LogLevel.None)
                stringBuilder.Append("USR");
            else if ((logLevel & FR.Logging.LogLevel.User2) != FR.Logging.LogLevel.None)
                stringBuilder.Append("USR");
            else if ((logLevel & FR.Logging.LogLevel.User3) != FR.Logging.LogLevel.None)
                stringBuilder.Append("USR");
            else
                stringBuilder.Append("???");
            stringBuilder.Append("; ");
            return stringBuilder.ToString();
        }

        private void copyToClipboard() {
            Clipboard.Clear();
            StringBuilder stringBuilder = new StringBuilder(1000);
            foreach (object selectedItem in this.lstLogging.SelectedItems)
                stringBuilder.AppendLine(selectedItem.ToString());
            if (stringBuilder.Length == 0)
                Clipboard.SetText(" ");
            else
                Clipboard.SetText(stringBuilder.ToString());
        }

        public new void Dispose() => this.Close();

        void ILoggingDevice.Close() => this.Close();
    }
}
