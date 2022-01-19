// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormOptions
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FR.Windows.Forms {
    public class FormOptions : FormBase {
        public const int ControlMaxWidth = 2147483647;
        private readonly List<IOptionsEditControl> _optionsEditControls;
        private IOptionsTree _optionsTree;
        private double _treeProportion;
        private IContainer components;
        private SplitContainer splitContainer1;
        private TreeViewBase tvwMain;
        private Button btnOk;
        private Button btnCancel;
        private Panel pnlMain;
        private ToolTip tipOptions;

        public FormOptions() {
            this.InitializeComponent();
            this._treeProportion = 0.3;
            this._optionsEditControls = new List<IOptionsEditControl>();
        }

        public IOptionsTree OptionsTree {
            [DebuggerNonUserCode]
            get => this._optionsTree;
            [DebuggerNonUserCode]
            set => this._optionsTree = value;
        }

        public double TreeProportion {
            [DebuggerNonUserCode]
            get => this._treeProportion;
            [DebuggerNonUserCode]
            set => this._treeProportion = value;
        }

        private Control GetControlWithButton(
          IOptionsProperty property,
          Control editControl,
          EventHandler buttonClick) {
            Control controlWithButton = new Control();
            Button button = new Button();
            DataContext dataContext = new DataContext {
                { "EditControl", editControl },
                { "Property", property }
            };
            editControl.Location = new Point(0, 0);
            editControl.Tag = (object)dataContext;
            button.Location = new Point(editControl.Width + editControl.Margin.Right, 0);
            button.Height = editControl.Height;
            button.Width = (int)((double)button.Height * 1.3);
            button.Text = "...";
            button.Tag = (object)dataContext;
            button.Click += buttonClick;
            controlWithButton.Tag = (object)dataContext;
            controlWithButton.Controls.Add(editControl);
            controlWithButton.Controls.Add((Control)button);
            controlWithButton.Height = editControl.Height;
            controlWithButton.Width = button.Right;
            editControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            return controlWithButton;
        }

        private void CFormOptions_Load(object sender, EventArgs e) {
            this.Text = this.OptionsTree.Text;
            this.AcceptButton = (IButtonControl)null;
            this.CancelButton = (IButtonControl)null;
            this.splitContainer1.SplitterDistance = (int)((double)this.splitContainer1.Width * this.TreeProportion);
            FormOptionsTreeNode node = new FormOptionsTreeNode((IOptionsNode)this.OptionsTree);
            this.tvwMain.Nodes.Add((TreeNodeBase)node);
            node.Expand();
        }

        private void CFormOptions_FormClosed(object sender, FormClosedEventArgs e) {
            foreach (IOptionsEditControl optionsEditControl in this._optionsEditControls)
                optionsEditControl.HandleDialogResult(this.DialogResult);
        }

        private void tvwMain_AfterSelect(object sender, TreeViewEventArgs e) {
            this.tipOptions.RemoveAll();
            if (!(e.Node is FormOptionsTreeNode node))
                return;
            IOptionsNode optionsNode = node.OptionsNode;
            float val1_1 = 0.0f;
            List<OptionItemSet> optionItemSetList = new List<OptionItemSet>();
            this.pnlMain.Controls.Clear();
            if (optionsNode.EditControl != null) {
                if (optionsNode.EditControl is IOptionsEditControl editControl) {
                    if (!this._optionsEditControls.Contains(editControl))
                        this._optionsEditControls.Add(editControl);
                    editControl.SetOptionsNode(optionsNode);
                }
                this.pnlMain.Controls.Add(optionsNode.EditControl);
            }
            else {
                Graphics graphics = this.CreateGraphics();
                foreach (IOptionsProperty property in optionsNode.Properties) {
                    OptionItemSet optionItemSet = new OptionItemSet {
                        Property = property
                    };
                    optionItemSetList.Add(optionItemSet);
                    SizeF sizeF1 = graphics.MeasureString(property.Text, this.Font);
                    val1_1 = Math.Max(val1_1, sizeF1.Width);
                    Label label = new Label {
                        Text = property.Text,
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    optionItemSet.Label = label;
                    if (property.EditControl != null) {
                        Control editControl = property.EditControl;
                        if (editControl is IOptionsEditControl optionsEditControl) {
                            if (!this._optionsEditControls.Contains(optionsEditControl))
                                this._optionsEditControls.Add(optionsEditControl);
                            optionsEditControl.SetOptionsProperty(property);
                        }
                        optionItemSet.EditControl = editControl;
                    }
                    else if (property.SpecialType == FormOptionsPropertySpecialType.Color) {
                        PictureBox editControl = new PictureBox();
                        TextBox textBox = new TextBox();
                        editControl.Height = textBox.Height;
                        if (property.Value != null) {
                            Color color = (Color)new ColorConverter().ConvertFrom(property.Value);
                            Bitmap bitmap = new Bitmap(editControl.Width, editControl.Height);
                            graphics = Graphics.FromImage((Image)bitmap);
                            graphics.FillRectangle((Brush)new SolidBrush(color), new Rectangle(0, 0, editControl.Width, editControl.Height));
                            editControl.Image = (Image)bitmap;
                        }
                        Control controlWithButton = this.GetControlWithButton(property, (Control)editControl, new EventHandler(this.BtnColor_Click));
                        optionItemSet.EditControl = controlWithButton;
                    }
                    else if (property.SpecialType == FormOptionsPropertySpecialType.Directory) {
                        TextBox editControl = new TextBox {
                            Text = property.Value == null ? "" : property.Value.ToString(),
                            ReadOnly = true
                        };
                        Control controlWithButton = this.GetControlWithButton(property, (Control)editControl, new EventHandler(this.BtnDirectory_Click));
                        controlWithButton.Width = int.MaxValue;
                        optionItemSet.EditControl = controlWithButton;
                    }
                    else if (property.SpecialType == FormOptionsPropertySpecialType.File) {
                        TextBox editControl = new TextBox {
                            Text = property.Value == null ? "" : property.Value.ToString(),
                            ReadOnly = true
                        };
                        Control controlWithButton = this.GetControlWithButton(property, (Control)editControl, new EventHandler(this.BtnFile_Click));
                        controlWithButton.Width = int.MaxValue;
                        optionItemSet.EditControl = controlWithButton;
                    }
                    else if (property.SpecialType == FormOptionsPropertySpecialType.Font) {
                        FontConverter fontConverter = new FontConverter();
                        object obj1 = property.Value;
                        System.Type type;
                        object obj2;
                        if (obj1 == null) {
                            type = typeof(string);
                            obj2 = (object)new Font(this.Font, this.Font.Style);
                        }
                        else if (obj1 is string && obj1.ToString() == "") {
                            type = obj1.GetType();
                            obj2 = (object)new Font(this.Font, this.Font.Style);
                        }
                        else {
                            type = obj1.GetType();
                            obj2 = fontConverter.ConvertFrom(obj1);
                        }
                        Font font = obj2 as Font;
                        TextBox editControl = new TextBox {
                            Text = fontConverter.ConvertToString((object)font),
                            ReadOnly = true
                        };
                        Control controlWithButton = this.GetControlWithButton(property, (Control)editControl, new EventHandler(this.BtnFont_Click));
                        DataContext tag = (DataContext)controlWithButton.Tag;
                        tag.Add((object)"Type", (object)type);
                        tag.Add((object)"Font", (object)font);
                        controlWithButton.Width = int.MaxValue;
                        optionItemSet.EditControl = controlWithButton;
                    }
                    else if (property.SpecialType == FormOptionsPropertySpecialType.CheckBox) {
                        CheckBox checkBox = new CheckBox {
                            Width = 200,
                            Checked = property.Value is bool ? (bool)property.Value : bool.Parse(property.Value.ToString())
                        };
                        optionItemSet.EditControl = checkBox;
                        checkBox.Click += new EventHandler(this.CheckBox_Click);
                    }
                    else if (property.SpecialType != FormOptionsPropertySpecialType.SqlConnectionString) {
                        if (property.AllowedValues != null) {
                            float val1_2 = 0.0f;
                            ComboBox comboBox = new ComboBox();
                            foreach (object allowedValue in property.AllowedValues) {
                                SizeF sizeF2 = graphics.MeasureString(allowedValue.ToString(), this.Font);
                                val1_2 = Math.Max(val1_2, sizeF2.Width);
                                comboBox.Items.Add(allowedValue);
                            }
                            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                            comboBox.Width = Math.Max(comboBox.Width, (int)val1_2 + 20);
                            if (property.Value != null)
                                comboBox.SelectedItem = property.Value;
                            comboBox.Validating += new CancelEventHandler(this.ComboBox_Validating);
                            optionItemSet.EditControl = (Control)comboBox;
                        }
                        else if (BaseHelper.InList((object)property.PropertyType, (object)typeof(byte), (object)typeof(sbyte), (object)typeof(short), (object)typeof(ushort), (object)typeof(int), (object)typeof(uint), (object)typeof(long), (object)typeof(ulong), (object)typeof(float), (object)typeof(double), (object)typeof(Decimal))) {
                            NumericUpDown numericUpDown = new NumericUpDown {
                                Value = (Decimal)Convert.ChangeType(property.Value, typeof(Decimal))
                            };
                            optionItemSet.EditControl = (Control)numericUpDown;
                            numericUpDown.Validating += new CancelEventHandler(this.NumericUpDown_Validating);
                        }
                        else if (property.PropertyType == typeof(DateTime)) {
                            DateTimePicker dateTimePicker = new DateTimePicker {
                                Value = (DateTime)property.Value
                            };
                            optionItemSet.EditControl = dateTimePicker;
                            dateTimePicker.Validating += new CancelEventHandler(this.DateTimePicker_Validating);
                        }
                        else {
                            TextBox textBox = new TextBox {
                                Text = property.Value == null ? "" : property.Value.ToString()
                            };
                            optionItemSet.EditControl = (Control)textBox;
                            textBox.Validating += new CancelEventHandler(this.TextBox_Validating);
                        }
                    }
                }
                graphics.Dispose();
                int num1 = 20;
                int num2 = 10;
                foreach (FormOptions.OptionItemSet optionItemSet in optionItemSetList) {
                    Label label = optionItemSet.Label;
                    Control editControl = optionItemSet.EditControl;
                    IOptionsProperty property = optionItemSet.Property;
                    if (editControl.Tag == null || editControl.Tag is DataContext) {
                        if (!(editControl.Tag is DataContext dataContext))
                            dataContext = new DataContext();
                        editControl.Tag = (object)dataContext;
                        if (!dataContext.ContainsKey((object)"EditControl"))
                            dataContext.Add((object)"EditControl", (object)editControl);
                        if (!dataContext.ContainsKey((object)"Property"))
                            dataContext.Add((object)"Property", (object)property);
                    }
                    editControl.Top = num1;
                    num1 = editControl.Bottom + editControl.Margin.Bottom;
                    editControl.Left = (int)((double)num2 + (double)val1_1 + (double)editControl.Margin.Left);
                    if (property.EditControlWidth != 0)
                        editControl.Width = property.EditControlWidth;
                    if (editControl.Width == int.MaxValue) {
                        editControl.Width = this.pnlMain.Width - editControl.Left - this.pnlMain.Padding.Right - 10;
                        editControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    }
                    label.Top = editControl.Top;
                    label.Left = num2;
                    this.pnlMain.Controls.Add((Control)label);
                    this.pnlMain.Controls.Add(editControl);
                    if (!string.IsNullOrEmpty(property.Description))
                        this.tipOptions.SetToolTip((Control)label, property.Description);
                }
            }
        }

        private void BtnColor_Click(object sender, EventArgs e) {
            DataContext tag = (DataContext)((Control)sender).Tag;
            PictureBox pictureBox = (PictureBox)tag[(object)"EditControl"];
            IOptionsProperty optionsProperty = (IOptionsProperty)tag[(object)"Property"];
            ColorConverter colorConverter = new ColorConverter();
            Color color1 = optionsProperty.Value == null ? Color.Empty : (Color)colorConverter.ConvertFrom(optionsProperty.Value);
            ColorDialog colorDialog = new ColorDialog();
            if (color1 != Color.Empty)
                colorDialog.Color = color1;
            if (colorDialog.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            Color color2 = colorDialog.Color;
            optionsProperty.Value = colorConverter.ConvertTo((object)color2, typeof(string));
            Image image = pictureBox.Image;
            Graphics.FromImage(image).FillRectangle((Brush)new SolidBrush(color2), new Rectangle(0, 0, image.Width, image.Height));
        }

        private void BtnDirectory_Click(object sender, EventArgs e) {
            DataContext tag = (DataContext)((Control)sender).Tag;
            TextBox textBox = (TextBox)tag[(object)"EditControl"];
            IOptionsProperty optionsProperty = (IOptionsProperty)tag[(object)"Property"];
            string path = optionsProperty.Value == null ? (string)null : optionsProperty.Value.ToString();
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (path != null && Directory.Exists(path))
                folderBrowserDialog.SelectedPath = path;
            if (folderBrowserDialog.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            optionsProperty.Value = (object)folderBrowserDialog.SelectedPath;
            textBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void BtnFile_Click(object sender, EventArgs e) {
            DataContext tag = (DataContext)((Control)sender).Tag;
            TextBox textBox = (TextBox)tag[(object)"EditControl"];
            IOptionsProperty optionsProperty = (IOptionsProperty)tag[(object)"Property"];
            string str = optionsProperty.Value == null ? (string)null : optionsProperty.Value.ToString();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (str != null && File.Exists(str)) {
                FileInfo fileInfo = new FileInfo(str);
                openFileDialog.InitialDirectory = fileInfo.DirectoryName;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FileName = str;
            }
            if (openFileDialog.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            optionsProperty.Value = (object)openFileDialog.FileName;
            textBox.Text = openFileDialog.FileName;
        }

        private void BtnFont_Click(object sender, EventArgs e) {
            DataContext tag = (DataContext)((Control)sender).Tag;
            TextBox textBox = (TextBox)tag[(object)"EditControl"];
            IOptionsProperty optionsProperty = (IOptionsProperty)tag[(object)"Property"];
            FontConverter fontConverter = new FontConverter();
            System.Type asType = tag.GetAsType((object)"Type");
            Font font = (Font)tag[(object)"Font"];
            FontDialog fontDialog = new FontDialog {
                Font = font
            };
            if (fontDialog.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            optionsProperty.Value = fontConverter.ConvertTo((object)fontDialog.Font, asType);
            textBox.Text = fontConverter.ConvertToString((object)fontDialog.Font);
        }

        private void ComboBox_Validating(object sender, CancelEventArgs e) {
            ComboBox comboBox = (ComboBox)sender;
            IOptionsProperty optionsProperty = (IOptionsProperty)((DataContext)comboBox.Tag)[(object)"Property"];
            object selectedItem = comboBox.SelectedItem;
            if (selectedItem is ComboBoxItem)
                optionsProperty.Value = ((ComboBoxItem)selectedItem).Value;
            else
                optionsProperty.Value = selectedItem;
        }

        private void DateTimePicker_Validating(object sender, CancelEventArgs e) {
            DateTimePicker dateTimePicker = (DateTimePicker)sender;
            ((IOptionsProperty)((DataContext)dateTimePicker.Tag)[(object)"Property"]).Value = (object)dateTimePicker.Value;
        }

        private void NumericUpDown_Validating(object sender, CancelEventArgs e) {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ((IOptionsProperty)((DataContext)numericUpDown.Tag)[(object)"Property"]).Value = (object)numericUpDown.Value;
        }

        private void TextBox_Validating(object sender, CancelEventArgs e) {
            TextBox textBox = (TextBox)sender;
            ((IOptionsProperty)((DataContext)textBox.Tag)[(object)"Property"]).Value = (object)textBox.Text;
        }

        private void CheckBox_Click(object sender, EventArgs e) {
            CheckBox checkBox = (CheckBox)sender;
            ((IOptionsProperty)((DataContext)checkBox.Tag)[(object)"Property"]).Value = (object)checkBox.Checked;
        }

        protected override void Dispose(bool disposing) {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.components = (IContainer)new Container();
            this.splitContainer1 = new SplitContainer();
            this.tvwMain = new TreeViewBase();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.pnlMain = new Panel();
            this.tipOptions = new ToolTip(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            this.splitContainer1.BackColor = Color.Transparent;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add((Control)this.tvwMain);
            this.splitContainer1.Panel2.BackColor = Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add((Control)this.btnOk);
            this.splitContainer1.Panel2.Controls.Add((Control)this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add((Control)this.pnlMain);
            this.splitContainer1.Size = new Size(470, 325);
            this.splitContainer1.SplitterDistance = 156;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            this.tvwMain.Dock = DockStyle.Fill;
            this.tvwMain.HideSelection = false;
            this.tvwMain.Location = new Point(0, 0);
            this.tvwMain.Name = "tvwMain";
            this.tvwMain.RightClickSelect = false;
            this.tvwMain.SelectedNode = (TreeNodeBase)null;
            this.tvwMain.Size = new Size(156, 325);
            this.tvwMain.TabIndex = 0;
            this.tvwMain.AfterSelect += new TreeViewEventHandler(this.tvwMain_AfterSelect);
            this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnOk.DialogResult = DialogResult.OK;
            this.btnOk.Location = new Point(142, 290);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(223, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.pnlMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.pnlMain.BackColor = Color.Transparent;
            this.pnlMain.Location = new Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new Size(298, 284);
            this.pnlMain.TabIndex = 0;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.ClientSize = new Size(470, 325);
            this.Controls.Add((Control)this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = nameof(FormOptions);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new EventHandler(this.CFormOptions_Load);
            this.FormClosed += new FormClosedEventHandler(this.CFormOptions_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private class OptionItemSet {
            private Label _label;
            private Control _editControl;
            private IOptionsProperty _property;

            public Label Label {
                [DebuggerNonUserCode]
                get => this._label;
                [DebuggerNonUserCode]
                set => this._label = value;
            }

            public Control EditControl {
                [DebuggerNonUserCode]
                get => this._editControl;
                [DebuggerNonUserCode]
                set => this._editControl = value;
            }

            public IOptionsProperty Property {
                [DebuggerNonUserCode]
                get => this._property;
                [DebuggerNonUserCode]
                set => this._property = value;
            }
        }
    }
}
