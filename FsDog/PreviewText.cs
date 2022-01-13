// Decompiled with JetBrains decompiler
// Type: FsDog.PreviewText
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FsDog
{
  public class PreviewText : UserControl, IPreviewControl
  {
    private IContainer components;
    private RichTextBox txtContent;
    private string _fileName;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.txtContent = new RichTextBox();
      this.SuspendLayout();
      this.txtContent.Dock = DockStyle.Fill;
      this.txtContent.Location = new Point(0, 0);
      this.txtContent.Name = "txtContent";
      this.txtContent.ShowSelectionMargin = true;
      this.txtContent.Size = new Size(453, 259);
      this.txtContent.TabIndex = 0;
      this.txtContent.Text = "";
      this.txtContent.WordWrap = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.txtContent);
      this.Name = nameof (PreviewText);
      this.Size = new Size(453, 259);
      this.ResumeLayout(false);
    }

    public PreviewText() => this.InitializeComponent();

    public string FileName => this._fileName;

    public PreviewType PreviewType => PreviewType.Text;

    public void SetFile(string fileName)
    {
      this._fileName = fileName;
      try
      {
        this.txtContent.Text = File.ReadAllText(fileName);
      }
      catch (Exception ex)
      {
        this.txtContent.Text = ex.Message;
      }
    }

    protected override void InitLayout()
    {
      base.InitLayout();
      FsApp instance = FsApp.Instance;
      this.txtContent.Font = instance.Options.Preview.TextFont;
      this.txtContent.WordWrap = instance.Options.Preview.TextWrap;
    }
  }
}
