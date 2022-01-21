// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.FormBase
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Commands;
using FR.Configuration;
using FR.Logging;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FR.Windows.Forms {
    public class FormBase : Form, /*ILoggingProvider, *//*IConfigurable,*/ ICommandReceiver {
        private bool _bMoveForm;
        private Point _ptMoveMouse;
        private Point _ptMoveForm;
        private Color _gradientColorStart;
        private Color _gradientColorEnd;

        public FormBase() {
            this.InitializeComponent();
            this._bMoveForm = false;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        [Browsable(false)]
        public WindowsApplication ApplicationInstance {
            [DebuggerNonUserCode]
            get => WindowsApplication.Instance;
        }

        [Browsable(false)]
        public LoggingManager Logger {
            [DebuggerNonUserCode]
            get => this.ApplicationInstance != null ? this.ApplicationInstance.Logger : (LoggingManager)null;
            set {
                if (this.ApplicationInstance == null)
                    return;
                this.ApplicationInstance.Logger = value;
            }
        }

        public FR.Logging.LogLevel LogLevel {
            [DebuggerNonUserCode]
            get => this.ApplicationInstance != null ? this.ApplicationInstance.LogLevel : FR.Logging.LogLevel.Default;
            [DebuggerNonUserCode]
            set {
                if (this.ApplicationInstance == null)
                    return;
                this.ApplicationInstance.LogLevel = value;
            }
        }

        [Description("The left side gradient color")]
        [Category("Appearance")]
        [DefaultValue("White")]
        public Color GradientColorStart {
            [DebuggerNonUserCode]
            get => this._gradientColorStart;
            [DebuggerNonUserCode]
            set {
                this._gradientColorStart = value;
                this.Invalidate();
            }
        }

        [DefaultValue("White")]
        [Description("The right side gradient color")]
        [Category("Appearance")]
        public Color GradientColorEnd {
            [DebuggerNonUserCode]
            get => this._gradientColorEnd;
            [DebuggerNonUserCode]
            set {
                this._gradientColorEnd = value;
                this.Invalidate();
            }
        }

        [DefaultValue(false)]
        [Category("Window Style")]
        [Description("Defines if it is possible to move the form by the client area")]
        public bool MoveByClientArea { [DebuggerNonUserCode]
            get; [DebuggerNonUserCode]
            set; }

        public static Control GetActiveChildControl() {
            Form activeForm = Form.ActiveForm;
            if (activeForm != null) {
                Control activeControl = activeForm.ActiveControl;
                if (activeControl != null) {
                    while (activeControl is ContainerControl && ((ContainerControl)activeControl).ActiveControl != null)
                        activeControl = ((ContainerControl)activeControl).ActiveControl;
                    return activeControl;
                }
            }
            return (Control)null;
        }

        [DebuggerNonUserCode]
        public virtual ICommandReceiver GetCommandReceiver(System.Type commandType) => (ICommandReceiver)null;

        [DebuggerNonUserCode]
        public virtual void InitializeCommand(ICommand command) {
        }

        [DebuggerNonUserCode]
        public virtual void FinishCommand(ICommand command) {
        }

        [DebuggerNonUserCode]
        protected override void OnLoad(EventArgs e) {
            if (this.ApplicationInstance != null)
                this.ApplicationInstance.OnRunning();
            base.OnLoad(e);
        }

        [DebuggerNonUserCode]
        protected override void OnPaintBackground(PaintEventArgs pevent) {
            if (this.Height == 0 || this.Width == 0)
                return;
            Graphics graphics = pevent.Graphics;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            Brush brush = !(this.GradientColorStart == this.GradientColorEnd) ? (Brush)new LinearGradientBrush(rect, this.GradientColorStart, this.GradientColorEnd, 30f) : (Brush)new SolidBrush(this.BackColor);
            graphics.FillRectangle(brush, rect);
            brush.Dispose();
        }

        [DebuggerNonUserCode]
        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            this._bMoveForm = false;
        }

        [DebuggerNonUserCode]
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (!this.MoveByClientArea)
                return;
            this._bMoveForm = true;
            this._ptMoveMouse = new Point(Cursor.Position.X, Cursor.Position.Y);
            this._ptMoveForm = new Point(this.Location.X, this.Location.Y);
        }

        [DebuggerNonUserCode]
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (!this._bMoveForm)
                return;
            this.Location = new Point(this._ptMoveForm.X + Cursor.Position.X - this._ptMoveMouse.X, this._ptMoveForm.Y + Cursor.Position.Y - this._ptMoveMouse.Y);
        }

        [DebuggerNonUserCode]
        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            this._bMoveForm = false;
        }

        [DebuggerNonUserCode]
        ICommandReceiver ICommandReceiver.GetCommandReceiver(System.Type commandType) {
            return this.GetCommandReceiver(commandType);
        }

        [DebuggerNonUserCode]
        void ICommandReceiver.InitializeCommand(ICommand command) => this.InitializeCommand(command);

        [DebuggerNonUserCode]
        void ICommandReceiver.FinishCommand(ICommand command) => this.FinishCommand(command);

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(292, 268);
            this.Name = "CForm";
            this.Text = "Form";
            this.ResumeLayout(false);
        }
    }
}
