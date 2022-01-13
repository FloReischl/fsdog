// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormReportPreview
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using Microsoft.Reporting.WinForms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class FormReportPreview : FormBase
  {
    private IContainer components;
    private ReportViewer rptViewer;

    public FormReportPreview() => this.InitializeComponent();

    public LocalReport LocalReport => this.rptViewer.LocalReport;

    private void FormReportPreview_Load(object sender, EventArgs e) => this.rptViewer.RefreshReport();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.rptViewer = new ReportViewer();
      this.SuspendLayout();
      ((Control) this.rptViewer).Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      ((Control) this.rptViewer).Location = new Point(12, 12);
      ((Control) this.rptViewer).Name = "rptViewer";
      ((Control) this.rptViewer).Size = new Size(681, 443);
      ((Control) this.rptViewer).TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.ClientSize = new Size(705, 492);
      this.Controls.Add((Control) this.rptViewer);
      this.Name = "CFormReportPreview";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Load += new EventHandler(this.FormReportPreview_Load);
      this.ResumeLayout(false);
    }
  }
}
