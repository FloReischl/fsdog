// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.RichTextEditor
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class RichTextEditor : UserControl
  {
    private IContainer components;
    private ToolStrip toolStrip1;
    private ToolStripComboBox tsiFontName;
    private ToolStripComboBox tsiFontSize;
    private ToolStripButton tsiFontBold;
    private ToolStripButton tsiFontItalic;
    private ToolStripButton tsiFontUnderline;
    private RichTextBox txtText;
    private ToolStripButton tsiFontColor;
    private Font _font;
    private Color _foreColor;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RichTextEditor));
      this.toolStrip1 = new ToolStrip();
      this.tsiFontName = new ToolStripComboBox();
      this.tsiFontSize = new ToolStripComboBox();
      this.tsiFontBold = new ToolStripButton();
      this.tsiFontItalic = new ToolStripButton();
      this.tsiFontUnderline = new ToolStripButton();
      this.tsiFontColor = new ToolStripButton();
      this.txtText = new RichTextBox();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      this.toolStrip1.Items.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.tsiFontName,
        (ToolStripItem) this.tsiFontSize,
        (ToolStripItem) this.tsiFontBold,
        (ToolStripItem) this.tsiFontItalic,
        (ToolStripItem) this.tsiFontUnderline,
        (ToolStripItem) this.tsiFontColor
      });
      this.toolStrip1.Location = new Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new Size(310, 25);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      this.tsiFontName.DropDownStyle = ComboBoxStyle.DropDownList;
      this.tsiFontName.Name = "tsiFontName";
      this.tsiFontName.Size = new Size(121, 25);
      this.tsiFontName.SelectedIndexChanged += new EventHandler(this.tsiFontName_SelectedIndexChanged);
      this.tsiFontSize.DropDownStyle = ComboBoxStyle.DropDownList;
      this.tsiFontSize.Name = "tsiFontSize";
      this.tsiFontSize.Size = new Size(75, 25);
      this.tsiFontSize.SelectedIndexChanged += new EventHandler(this.tsiFontSize_SelectedIndexChanged);
      this.tsiFontBold.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsiFontBold.Image = (Image) componentResourceManager.GetObject("tsiFontBold.Image");
      this.tsiFontBold.ImageTransparentColor = Color.Magenta;
      this.tsiFontBold.Name = "tsiFontBold";
      this.tsiFontBold.Size = new Size(23, 22);
      this.tsiFontBold.Text = "toolStripButton1";
      this.tsiFontBold.Click += new EventHandler(this.tsiFontBold_Click);
      this.tsiFontItalic.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsiFontItalic.Image = (Image) componentResourceManager.GetObject("tsiFontItalic.Image");
      this.tsiFontItalic.ImageTransparentColor = Color.Magenta;
      this.tsiFontItalic.Name = "tsiFontItalic";
      this.tsiFontItalic.Size = new Size(23, 22);
      this.tsiFontItalic.Text = "toolStripButton1";
      this.tsiFontItalic.Click += new EventHandler(this.tsiFontItalic_Click);
      this.tsiFontUnderline.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsiFontUnderline.Image = (Image) componentResourceManager.GetObject("tsiFontUnderline.Image");
      this.tsiFontUnderline.ImageTransparentColor = Color.Magenta;
      this.tsiFontUnderline.Name = "tsiFontUnderline";
      this.tsiFontUnderline.Size = new Size(23, 22);
      this.tsiFontUnderline.Text = "toolStripButton1";
      this.tsiFontUnderline.Click += new EventHandler(this.tsiFontUnderline_Click);
      this.tsiFontColor.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsiFontColor.Image = (Image) componentResourceManager.GetObject("tsiFontColor.Image");
      this.tsiFontColor.ImageTransparentColor = Color.Magenta;
      this.tsiFontColor.Name = "tsiFontColor";
      this.tsiFontColor.Size = new Size(23, 22);
      this.tsiFontColor.Text = "toolStripButton1";
      this.tsiFontColor.Click += new EventHandler(this.tsiFontColor_Click);
      this.txtText.AcceptsTab = true;
      this.txtText.Dock = DockStyle.Fill;
      this.txtText.HideSelection = false;
      this.txtText.Location = new Point(0, 25);
      this.txtText.Name = "txtText";
      this.txtText.Size = new Size(310, 163);
      this.txtText.TabIndex = 2;
      this.txtText.Text = "";
      this.txtText.SelectionChanged += new EventHandler(this.txtText_SelectionChanged);
      this.txtText.KeyDown += new KeyEventHandler(this.txtText_KeyDown);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.txtText);
      this.Controls.Add((Control) this.toolStrip1);
      this.Name = "CRichTextEditor";
      this.Size = new Size(310, 188);
      this.Load += new EventHandler(this.UserControl1_Load);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public RichTextEditor() => this.InitializeComponent();

    public string Rtf
    {
      get => this.txtText.Rtf;
      set => this.txtText.Rtf = value;
    }

    public override string Text
    {
      [DebuggerNonUserCode] get => this.txtText.Text;
      [DebuggerNonUserCode] set => this.txtText.Text = value;
    }

    public override Font Font
    {
      get => this._font != null ? this._font : base.Font;
      set
      {
        this._font = value;
        this.txtText.Font = value;
      }
    }

    public override Color ForeColor
    {
      get => !(this._foreColor == Color.Empty) ? this._foreColor : base.ForeColor;
      set
      {
        this._foreColor = value;
        this.txtText.ForeColor = value;
      }
    }

    public bool AcceptsTabs
    {
      [DebuggerNonUserCode] get => this.txtText.AcceptsTab;
      [DebuggerNonUserCode] set => this.txtText.AcceptsTab = value;
    }

    public void LoadFile(string fileName) => this.txtText.LoadFile(fileName);

    public void LoadFile(Stream stream, RichTextBoxStreamType fileType) => this.txtText.LoadFile(stream, fileType);

    public void LoadFile(string fileName, RichTextBoxStreamType fileType) => this.txtText.LoadFile(fileName, fileType);

    public void SaveFile(string fileName) => this.txtText.SaveFile(fileName);

    public void SafeFile(Stream stream, RichTextBoxStreamType fileType)
    {
      this.txtText.SaveFile(stream, fileType);
      stream.Flush();
    }

    public void SafeFile(string fileName, RichTextBoxStreamType fileType) => this.txtText.SaveFile(fileName, fileType);

    private void InitializeToolStrip()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(this.GetType());
      FontFamily[] families = FontFamily.Families;
      this.tsiFontName.BeginUpdate();
      this.tsiFontName.Items.Clear();
      FontFamily fontFamily1 = (FontFamily) null;
      foreach (FontFamily fontFamily2 in families)
      {
        if (fontFamily2.Name == "Arial")
          fontFamily1 = fontFamily2;
        this.tsiFontName.Items.Add((object) fontFamily2.Name);
      }
      if (fontFamily1 != null)
        this.tsiFontName.SelectedItem = (object) "Arial";
      else if (this.tsiFontName.Items.Count != 0)
        this.tsiFontName.SelectedIndex = 0;
      this.tsiFontName.EndUpdate();
      int[] numArray = new int[15]
      {
        8,
        9,
        10,
        11,
        12,
        14,
        16,
        18,
        20,
        22,
        24,
        28,
        36,
        48,
        72
      };
      this.tsiFontSize.BeginUpdate();
      this.tsiFontSize.Items.Clear();
      foreach (int num in numArray)
        this.tsiFontSize.Items.Add((object) num);
      this.tsiFontSize.SelectedItem = (object) 10;
      this.tsiFontSize.EndUpdate();
      this.tsiFontBold.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsiFontBold.Text = "B";
      this.tsiFontBold.ToolTipText = "Bold";
      this.tsiFontItalic.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsiFontItalic.Text = "I";
      this.tsiFontItalic.ToolTipText = "Italic";
      this.tsiFontUnderline.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsiFontUnderline.Text = "U";
      this.tsiFontUnderline.ToolTipText = "Underline";
      this.tsiFontColor.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsiFontColor.Text = "C";
      this.tsiFontColor.ToolTipText = "Color";
    }

    private void DoFontName(string name)
    {
      Font selectionFont = this.GetSelectionFont();
      this.txtText.SelectionFont = new Font(name, selectionFont.Size, selectionFont.Style);
    }

    private void DoFontSize(int size) => this.txtText.SelectionFont = new Font(this.GetSelectionFont().FontFamily, (float) size);

    private void DoBold()
    {
      Font selectionFont = this.GetSelectionFont();
      FontStyle newStyle = FontStyle.Regular;
      if ((selectionFont.Style & FontStyle.Bold) != FontStyle.Bold)
        newStyle |= FontStyle.Bold;
      if ((selectionFont.Style & FontStyle.Italic) == FontStyle.Italic)
        newStyle |= FontStyle.Italic;
      if ((selectionFont.Style & FontStyle.Underline) == FontStyle.Underline)
        newStyle |= FontStyle.Underline;
      this.txtText.SelectionFont = new Font(selectionFont, newStyle);
    }

    private void DoItalic()
    {
      Font selectionFont = this.GetSelectionFont();
      FontStyle newStyle = FontStyle.Regular;
      if ((selectionFont.Style & FontStyle.Bold) == FontStyle.Bold)
        newStyle |= FontStyle.Bold;
      if ((selectionFont.Style & FontStyle.Italic) != FontStyle.Italic)
        newStyle |= FontStyle.Italic;
      if ((selectionFont.Style & FontStyle.Underline) == FontStyle.Underline)
        newStyle |= FontStyle.Underline;
      this.txtText.SelectionFont = new Font(selectionFont, newStyle);
    }

    private void DoUnderline()
    {
      Font selectionFont = this.GetSelectionFont();
      FontStyle newStyle = FontStyle.Regular;
      if ((selectionFont.Style & FontStyle.Bold) == FontStyle.Bold)
        newStyle |= FontStyle.Bold;
      if ((selectionFont.Style & FontStyle.Italic) == FontStyle.Italic)
        newStyle |= FontStyle.Italic;
      if ((selectionFont.Style & FontStyle.Underline) != FontStyle.Underline)
        newStyle |= FontStyle.Underline;
      this.txtText.SelectionFont = new Font(selectionFont, newStyle);
    }

    private void DoColor(Color color) => this.txtText.SelectionColor = color;

    private Font GetSelectionFont()
    {
      Font selectionFont = this.txtText.SelectionFont;
      if (selectionFont == null)
      {
        int selectionStart = this.txtText.SelectionStart;
        int selectionLength = this.txtText.SelectionLength;
        this.txtText.SelectionLength = 0;
        selectionFont = this.txtText.SelectionFont;
        this.txtText.SelectionLength = selectionLength;
      }
      return selectionFont;
    }

    private void UserControl1_Load(object sender, EventArgs e) => this.InitializeToolStrip();

    private void tsiFontName_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.DoFontName((string) this.tsiFontName.SelectedItem);
      this.txtText.Focus();
    }

    private void tsiFontSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.DoFontSize((int) this.tsiFontSize.SelectedItem);
      this.txtText.Focus();
    }

    private void tsiFontBold_Click(object sender, EventArgs e)
    {
      this.DoBold();
      this.txtText.Focus();
    }

    private void tsiFontItalic_Click(object sender, EventArgs e)
    {
      this.DoItalic();
      this.txtText.Focus();
    }

    private void tsiFontUnderline_Click(object sender, EventArgs e)
    {
      this.DoUnderline();
      this.txtText.Focus();
    }

    private void tsiFontColor_Click(object sender, EventArgs e)
    {
      ColorDialog colorDialog = new ColorDialog();
      colorDialog.Color = this.txtText.SelectionColor;
      colorDialog.FullOpen = true;
      if (colorDialog.ShowDialog((IWin32Window) this.FindForm()) != DialogResult.OK)
        return;
      this.DoColor(colorDialog.Color);
    }

    private void txtText_KeyDown(object sender, KeyEventArgs e)
    {
      if (!e.Control)
        return;
      switch (e.KeyCode)
      {
        case Keys.B:
          this.DoBold();
          break;
        case Keys.I:
          this.DoItalic();
          break;
        case Keys.U:
          this.DoUnderline();
          break;
      }
    }

    private void txtText_SelectionChanged(object sender, EventArgs e)
    {
      Font selectionFont = this.GetSelectionFont();
      this.tsiFontName.SelectedItem = (object) selectionFont.FontFamily.Name;
      this.tsiFontSize.SelectedItem = (object) (int) selectionFont.Size;
    }
  }
}
