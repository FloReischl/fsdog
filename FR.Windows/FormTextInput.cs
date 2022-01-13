// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormTextInput
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class FormTextInput : FormBase
  {
    private IContainer components;
    private TextBox TextBoxInput;
    private Label LabelDescription;
    private Button ButtonCancel;
    private Button ButtonOk;
    private string _inputText;
    private string _defaultText;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.TextBoxInput = new TextBox();
      this.LabelDescription = new Label();
      this.ButtonCancel = new Button();
      this.ButtonOk = new Button();
      this.SuspendLayout();
      this.TextBoxInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.TextBoxInput.Location = new Point(16, 41);
      this.TextBoxInput.Name = "TextBoxInput";
      this.TextBoxInput.Size = new Size(213, 20);
      this.TextBoxInput.TabIndex = 0;
      this.LabelDescription.AutoSize = true;
      this.LabelDescription.Location = new Point(13, 13);
      this.LabelDescription.Name = "LabelDescription";
      this.LabelDescription.Size = new Size(86, 13);
      this.LabelDescription.TabIndex = 1;
      this.LabelDescription.Text = "LabelDescription";
      this.ButtonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.ButtonCancel.DialogResult = DialogResult.Cancel;
      this.ButtonCancel.Location = new Point(165, 67);
      this.ButtonCancel.Name = "ButtonCancel";
      this.ButtonCancel.Size = new Size(64, 23);
      this.ButtonCancel.TabIndex = 2;
      this.ButtonCancel.Text = "&Cancel";
      this.ButtonCancel.UseVisualStyleBackColor = true;
      this.ButtonCancel.Click += new EventHandler(this.ButtonCancel_Click);
      this.ButtonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.ButtonOk.Location = new Point(95, 67);
      this.ButtonOk.Name = "ButtonOk";
      this.ButtonOk.Size = new Size(64, 23);
      this.ButtonOk.TabIndex = 3;
      this.ButtonOk.Text = "&Ok";
      this.ButtonOk.UseVisualStyleBackColor = true;
      this.ButtonOk.Click += new EventHandler(this.ButtonOk_Click);
      this.AcceptButton = (IButtonControl) this.ButtonOk;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.CancelButton = (IButtonControl) this.ButtonCancel;
      this.ClientSize = new Size(241, 102);
      this.Controls.Add((Control) this.ButtonOk);
      this.Controls.Add((Control) this.ButtonCancel);
      this.Controls.Add((Control) this.LabelDescription);
      this.Controls.Add((Control) this.TextBoxInput);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = "CFormTextInput";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Load += new EventHandler(this.CFormTextInput_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public string InputText
    {
      get => this._inputText;
      set => this._inputText = value;
    }

    public string DefaultText
    {
      get => this._defaultText;
      set
      {
        this._defaultText = value;
        this.InputText = this.DefaultText;
      }
    }

    public string Description
    {
      get => this.LabelDescription.Text;
      set => this.LabelDescription.Text = value;
    }

    public FormTextInput()
    {
      this.InitializeComponent();
      this.DefaultText = "";
    }

    private void CFormTextInput_Load(object sender, EventArgs e) => this.TextBoxInput.Text = this.DefaultText;

    private void ButtonOk_Click(object sender, EventArgs e)
    {
      this.InputText = this.TextBoxInput.Text;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }
  }
}
