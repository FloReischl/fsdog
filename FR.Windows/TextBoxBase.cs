// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.TextBoxBase
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FR.Windows.Forms {
    public class TextBoxBase : TextBox {
        private string _previousText;
        private int _selectionStart;
        private int _selectionLength;
        private bool _validating;
        private Color _backColor;
        private ToolTip _toolTip;
        private bool _mandatory;
        private Color _mandatoryBackColor;
        private Color _defaultBackColor;
        private string _validationErrorText;
        private TextBoxValidationErrorType _validationErrorType;
        private string _validationExpression;
        private TextBoxValidationType _validationType;
        private bool _validateOnChange;

        [Category("Behavior")]
        [Description("Gets or sets if the text box is a mandatory field")]
        [DefaultValue(false)]
        public bool Mandatory {
            get => this._mandatory;
            set {
                if (this._mandatory == value)
                    return;
                this._mandatory = value;
                if (this._mandatory) {
                    this._defaultBackColor = this.BackColor;
                    if (this.validateType())
                        this.BackColor = this._mandatoryBackColor;
                    else
                        this._backColor = this._mandatoryBackColor;
                }
                else if (this.validateType())
                    this.BackColor = this._defaultBackColor;
                else
                    this._backColor = this._defaultBackColor;
            }
        }

        [Category("Layout")]
        [Description("Gets or sets the back color for a mandatory field")]
        public Color MandatoryBackColor {
            get => this._mandatoryBackColor;
            set => this._mandatoryBackColor = value;
        }

        [Description("Gets or sets the default back color of the text box")]
        [Category("Layout")]
        public new Color DefaultBackColor {
            get => this._defaultBackColor;
            set => this._defaultBackColor = value;
        }

        [Category("Behavior")]
        [DefaultValue("")]
        [Description("Gets or sets text for a validation error")]
        public string ValidationErrorText {
            get => this._validationErrorText;
            set => this._validationErrorText = value;
        }

        [DefaultValue(TextBoxValidationErrorType.None)]
        [Category("Behavior")]
        [Description("Defines the type of action in case of an validation error")]
        public TextBoxValidationErrorType ValidationErrorType {
            get => this._validationErrorType;
            set => this._validationErrorType = value;
        }

        [Category("Behavior")]
        [DefaultValue("")]
        [Description("The custom validation regular expression")]
        public string ValidationExpression {
            get => this._validationExpression;
            set => this._validationExpression = value;
        }

        [DefaultValue(TextBoxValidationType.None)]
        [Category("Behavior")]
        [Description("The validation mask for text value")]
        public TextBoxValidationType ValidationType {
            [DebuggerNonUserCode]
            get => this._validationType;
            set {
                this._validationType = value;
                if (this.validateType())
                    return;
                if (!BaseHelper.InList(value, TextBoxValidationType.Double, TextBoxValidationType.Float, TextBoxValidationType.Int, TextBoxValidationType.Short))
                    return;
                this.Text = "0";
            }
        }

        [Category("Behavior")]
        [Description("Says if the text value shall be validated at changing, not at usual control validation")]
        [DefaultValue(false)]
        public bool ValidateOnChange {
            [DebuggerNonUserCode]
            get => this._validateOnChange;
            [DebuggerNonUserCode]
            set => this._validateOnChange = value;
        }

        public TextBoxBase() {
            this._previousText = this.Text;
            this._selectionStart = 0;
            this._selectionLength = 0;
            this._validateOnChange = false;
            this._validating = false;
            this._mandatory = false;
            this._toolTip = (ToolTip)null;
            this._mandatoryBackColor = Color.Bisque;
        }

        protected override void InitLayout() {
            base.InitLayout();
            this._previousText = this.Text;
            this.validateType();
        }

        protected override void OnEnter(EventArgs e) {
            this._previousText = this.Text;
            this._selectionStart = this.SelectionStart;
            this._selectionLength = this.SelectionLength;
            base.OnEnter(e);
        }

        protected override void OnTextChanged(EventArgs e) {
            if (this._validateOnChange && !this._validating) {
                this._validating = true;
                if (!this.validateType()) {
                    this.setValidationError();
                }
                else {
                    this._previousText = this.Text;
                    this._selectionStart = this.SelectionStart;
                    this._selectionLength = this.SelectionLength;
                    this.setValidationSuccess();
                }
                this._validating = false;
            }
            base.OnTextChanged(e);
        }

        protected override void OnValidating(CancelEventArgs e) {
            if (!this.validateType())
                e.Cancel = true;
            base.OnValidating(e);
        }

        private bool validateType() {
            if (this.Text == null || this.Text == string.Empty)
                return true;
            switch (this.ValidationType) {
                case TextBoxValidationType.Int:
                    if (!int.TryParse(this.Text, out int _))
                        return false;
                    break;
                case TextBoxValidationType.Float:
                    if (!float.TryParse(this.Text, out float _))
                        return false;
                    break;
                case TextBoxValidationType.Double:
                    if (!double.TryParse(this.Text, out double _))
                        return false;
                    break;
                case TextBoxValidationType.Short:
                    if (!short.TryParse(this.Text, out short _))
                        return false;
                    break;
                case TextBoxValidationType.Custom:
                    if (this.ValidationExpression != null && !(this.ValidationExpression == "") && !new Regex(this.ValidationExpression).IsMatch(this.Text))
                        return false;
                    break;
            }
            return true;
        }

        private void setValidationError() {
            switch (this.ValidationErrorType) {
                case TextBoxValidationErrorType.Disallow:
                    this.Text = this._previousText;
                    this.SelectionStart = this._selectionStart;
                    this.SelectionLength = this._selectionLength;
                    break;
                case TextBoxValidationErrorType.ColorRed:
                    if (!(this.BackColor != Color.Red))
                        break;
                    this._backColor = this.BackColor;
                    this.BackColor = Color.Red;
                    break;
                case TextBoxValidationErrorType.ColorApplication:
                    WindowsApplication instance = WindowsApplication.Instance;
                    if (instance == null || !(this.BackColor != instance.ValidationErrorColor))
                        break;
                    this._backColor = this.BackColor;
                    this.BackColor = instance.ValidationErrorColor;
                    break;
                case TextBoxValidationErrorType.Balloon:
                    if (this._toolTip == null)
                        this._toolTip = new ToolTip();
                    this._toolTip.IsBalloon = true;
                    this._toolTip.SetToolTip((Control)this, this.ValidationErrorText);
                    this._toolTip.ShowAlways = true;
                    break;
                case TextBoxValidationErrorType.ToolTip:
                    if (this._toolTip == null)
                        this._toolTip = new ToolTip();
                    this._toolTip.IsBalloon = false;
                    this._toolTip.SetToolTip((Control)this, this.ValidationErrorText);
                    this._toolTip.ShowAlways = true;
                    break;
            }
        }

        private void setValidationSuccess() {
            switch (this.ValidationErrorType) {
                case TextBoxValidationErrorType.ColorRed:
                    if (!(this.BackColor == Color.Red))
                        break;
                    this.BackColor = this._backColor;
                    break;
                case TextBoxValidationErrorType.ColorApplication:
                    WindowsApplication instance = WindowsApplication.Instance;
                    if (instance == null || !(this.BackColor == instance.ValidationErrorColor))
                        break;
                    this.BackColor = this._backColor;
                    break;
                case TextBoxValidationErrorType.Balloon:
                    if (this._toolTip == null)
                        break;
                    this._toolTip.ShowAlways = false;
                    this._toolTip.Hide((IWin32Window)this);
                    break;
                case TextBoxValidationErrorType.ToolTip:
                    if (this._toolTip == null)
                        break;
                    this._toolTip.ShowAlways = false;
                    this._toolTip.Hide((IWin32Window)this);
                    break;
            }
        }
    }
}
