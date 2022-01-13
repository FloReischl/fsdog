using FsDog.Detail;
using FsDog.Tree;
using System.Drawing;
using System.Windows.Forms;

namespace FsDog.Dialogs {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private StatusStrip statMain;
        private ToolStripContainer tscMain;
        private SplitContainer sptMain;
        private TreeMain tvwMain;
        private ToolStrip tsMain;
        private DetailView detailView1;
        private SplitContainer sptDetailView;
        private DetailView detailView2;
        private ToolStripContainer tscDetailView;
        private ToolStripStatusLabel tslDriveInfo;
        private ToolStripStatusLabel tslCompleteInfo;
        private ToolStripStatusLabel tslSelectionInfo;
        private ToolStripStatusLabel tslMainInfo;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.statMain = new StatusStrip();
            this.tslMainInfo = new ToolStripStatusLabel();
            this.tslDriveInfo = new ToolStripStatusLabel();
            this.tslCompleteInfo = new ToolStripStatusLabel();
            this.tslSelectionInfo = new ToolStripStatusLabel();
            this.tscMain = new ToolStripContainer();
            this.sptMain = new SplitContainer();
            this.tvwMain = new TreeMain();
            this.tscDetailView = new ToolStripContainer();
            this.sptDetailView = new SplitContainer();
            this.detailView1 = new DetailView();
            this.detailView2 = new DetailView();
            this.tsMain = new ToolStrip();
            this.statMain.SuspendLayout();
            this.tscMain.ContentPanel.SuspendLayout();
            this.tscMain.TopToolStripPanel.SuspendLayout();
            this.tscMain.SuspendLayout();
            this.sptMain.Panel1.SuspendLayout();
            this.sptMain.Panel2.SuspendLayout();
            this.sptMain.SuspendLayout();
            this.tscDetailView.ContentPanel.SuspendLayout();
            this.tscDetailView.SuspendLayout();
            this.sptDetailView.Panel1.SuspendLayout();
            this.sptDetailView.Panel2.SuspendLayout();
            this.sptDetailView.SuspendLayout();
            this.SuspendLayout();
            this.statMain.Items.AddRange(new ToolStripItem[4]
            {
        (ToolStripItem) this.tslMainInfo,
        (ToolStripItem) this.tslDriveInfo,
        (ToolStripItem) this.tslCompleteInfo,
        (ToolStripItem) this.tslSelectionInfo
            });
            this.statMain.Location = new Point(0, 557);
            this.statMain.Name = "statMain";
            this.statMain.ShowItemToolTips = true;
            this.statMain.Size = new Size(909, 22);
            this.statMain.TabIndex = 0;
            this.statMain.Text = "statusStrip1";
            this.tslMainInfo.AutoSize = false;
            this.tslMainInfo.Name = "tslMainInfo";
            this.tslMainInfo.Size = new Size(225, 17);
            this.tslMainInfo.Text = "tslMainInfo";
            this.tslMainInfo.TextAlign = ContentAlignment.MiddleLeft;
            this.tslDriveInfo.AutoSize = false;
            this.tslDriveInfo.Name = "tslDriveInfo";
            this.tslDriveInfo.Size = new Size(150, 17);
            this.tslDriveInfo.Text = "tslDriveInfo";
            this.tslDriveInfo.TextAlign = ContentAlignment.MiddleLeft;
            this.tslCompleteInfo.AutoSize = false;
            this.tslCompleteInfo.Name = "tslCompleteInfo";
            this.tslCompleteInfo.Size = new Size(150, 17);
            this.tslCompleteInfo.Text = "tslCompleteInfo";
            this.tslCompleteInfo.TextAlign = ContentAlignment.MiddleLeft;
            this.tslSelectionInfo.AutoSize = false;
            this.tslSelectionInfo.Name = "tslSelectionInfo";
            this.tslSelectionInfo.Size = new Size(150, 17);
            this.tslSelectionInfo.Text = "tslSelectionInfo";
            this.tslSelectionInfo.TextAlign = ContentAlignment.MiddleLeft;
            this.tscMain.ContentPanel.Controls.Add((Control)this.sptMain);
            this.tscMain.ContentPanel.Size = new Size(909, 532);
            this.tscMain.Dock = DockStyle.Fill;
            this.tscMain.Location = new Point(0, 0);
            this.tscMain.Name = "tscMain";
            this.tscMain.Size = new Size(909, 557);
            this.tscMain.TabIndex = 2;
            this.tscMain.Text = "toolStripContainer1";
            this.tscMain.TopToolStripPanel.Controls.Add((Control)this.tsMain);
            this.sptMain.Dock = DockStyle.Fill;
            this.sptMain.Location = new Point(0, 0);
            this.sptMain.Name = "sptMain";
            this.sptMain.Panel1.Controls.Add((Control)this.tvwMain);
            this.sptMain.Panel2.Controls.Add((Control)this.tscDetailView);
            this.sptMain.Size = new Size(909, 532);
            this.sptMain.SplitterDistance = 274;
            this.sptMain.TabIndex = 0;
            this.sptMain.TabStop = false;
            this.tvwMain.AllowDrop = true;
            this.tvwMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.tvwMain.HideSelection = false;
            this.tvwMain.Location = new Point(3, 3);
            this.tvwMain.Name = "tvwMain";
            this.tvwMain.RightClickSelect = false;
            this.tvwMain.SelectedNode = null;
            this.tvwMain.Size = new Size(268, 526);
            this.tvwMain.TabIndex = 0;
            this.tscDetailView.ContentPanel.Controls.Add((Control)this.sptDetailView);
            this.tscDetailView.ContentPanel.Size = new Size(631, 532);
            this.tscDetailView.Dock = DockStyle.Fill;
            this.tscDetailView.Location = new Point(0, 0);
            this.tscDetailView.Name = "tscDetailView";
            this.tscDetailView.Size = new Size(631, 532);
            this.tscDetailView.TabIndex = 0;
            this.tscDetailView.Text = "toolStripContainer1";
            this.tscDetailView.TopToolStripPanelVisible = false;
            this.sptDetailView.BackColor = SystemColors.ControlDark;
            this.sptDetailView.Dock = DockStyle.Fill;
            this.sptDetailView.Location = new Point(0, 0);
            this.sptDetailView.Name = "sptDetailView";
            this.sptDetailView.Orientation = Orientation.Horizontal;
            this.sptDetailView.Panel1.Controls.Add((Control)this.detailView1);
            this.sptDetailView.Panel2.Controls.Add((Control)this.detailView2);
            this.sptDetailView.Size = new Size(631, 532);
            this.sptDetailView.SplitterDistance = 297;
            this.sptDetailView.TabIndex = 1;
            this.sptDetailView.TabStop = false;
            this.detailView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.detailView1.BackColor = SystemColors.Control;
            this.detailView1.Location = new Point(3, 3);
            this.detailView1.Name = "detailView1";
            this.detailView1.Size = new Size(625, 291);
            this.detailView1.TabIndex = 0;
            this.detailView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.detailView2.BackColor = SystemColors.Control;
            this.detailView2.Location = new Point(3, 3);
            this.detailView2.Name = "detailView2";
            this.detailView2.Size = new Size(625, 225);
            this.detailView2.TabIndex = 1;
            this.tsMain.Dock = DockStyle.None;
            this.tsMain.Location = new Point(3, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new Size(111, 25);
            this.tsMain.TabIndex = 3;
            this.tsMain.Text = "toolStrip1";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(909, 579);
            this.Controls.Add((Control)this.tscMain);
            this.Controls.Add((Control)this.statMain);
            this.Name = "FormMain";
            this.Text = "FS Dog";
            this.statMain.ResumeLayout(false);
            this.statMain.PerformLayout();
            this.tscMain.ContentPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.PerformLayout();
            this.tscMain.ResumeLayout(false);
            this.tscMain.PerformLayout();
            this.sptMain.Panel1.ResumeLayout(false);
            this.sptMain.Panel2.ResumeLayout(false);
            this.sptMain.ResumeLayout(false);
            this.tscDetailView.ContentPanel.ResumeLayout(false);
            this.tscDetailView.ResumeLayout(false);
            this.tscDetailView.PerformLayout();
            this.sptDetailView.Panel1.ResumeLayout(false);
            this.sptDetailView.Panel2.ResumeLayout(false);
            this.sptDetailView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}