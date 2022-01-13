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

namespace FR.Windows.Forms
{
  public class FormBase : Form, ILoggingProvider, IConfigurable, ICommandReceiver
  {
    private bool _bMoveForm;
    private Point _ptMoveMouse;
    private Point _ptMoveForm;
    private IConfigurationProperty _configurationRoot;
    private Color _gradientColorStart;
    private Color _gradientColorEnd;
    private bool _moveByClientArea;
    private IContainer components;

    public FormBase()
    {
      this.InitializeComponent();
      this._bMoveForm = false;
      this.SetStyle(ControlStyles.ResizeRedraw, true);
    }

    [Browsable(false)]
    public WindowsApplication ApplicationInstance
    {
      [DebuggerNonUserCode] get => WindowsApplication.Instance;
    }

    [Browsable(false)]
    public LoggingManager Logger
    {
      [DebuggerNonUserCode] get => this.ApplicationInstance != null ? this.ApplicationInstance.Logger : (LoggingManager) null;
      set
      {
        if (this.ApplicationInstance == null)
          return;
        this.ApplicationInstance.Logger = value;
      }
    }

    public FR.Logging.LogLevel LogLevel
    {
      [DebuggerNonUserCode] get => this.ApplicationInstance != null ? this.ApplicationInstance.LogLevel : FR.Logging.LogLevel.Default;
      [DebuggerNonUserCode] set
      {
        if (this.ApplicationInstance == null)
          return;
        this.ApplicationInstance.LogLevel = value;
      }
    }

    [Browsable(false)]
    public IConfigurationSource ConfigurationSource
    {
      [DebuggerNonUserCode, Browsable(false)] get => this.ApplicationInstance != null ? ((IConfigurable) this.ApplicationInstance).ConfigurationSource : (IConfigurationSource) null;
    }

    [Browsable(false)]
    public IConfigurationProperty ConfigurationRoot
    {
      [Browsable(false)] get
      {
        if (this._configurationRoot == null)
        {
          IConfigurable applicationInstance = (IConfigurable) this.ApplicationInstance;
          if (applicationInstance != null && applicationInstance.ConfigurationRoot != null)
            this._configurationRoot = applicationInstance.ConfigurationRoot.GetSubProperty(this.Name, true);
        }
        return this._configurationRoot;
      }
      [Browsable(false)] set => this._configurationRoot = value;
    }

    [Description("The left side gradient color")]
    [Category("Appearance")]
    [DefaultValue("White")]
    public Color GradientColorStart
    {
      [DebuggerNonUserCode] get => this._gradientColorStart;
      [DebuggerNonUserCode] set
      {
        this._gradientColorStart = value;
        this.Invalidate();
      }
    }

    [DefaultValue("White")]
    [Description("The right side gradient color")]
    [Category("Appearance")]
    public Color GradientColorEnd
    {
      [DebuggerNonUserCode] get => this._gradientColorEnd;
      [DebuggerNonUserCode] set
      {
        this._gradientColorEnd = value;
        this.Invalidate();
      }
    }

    [DefaultValue(false)]
    [Category("Window Style")]
    [Description("Defines if it is possible to move the form by the client area")]
    public bool MoveByClientArea
    {
      [DebuggerNonUserCode] get => this._moveByClientArea;
      [DebuggerNonUserCode] set => this._moveByClientArea = value;
    }

    public static Control GetActiveChildControl()
    {
      Form activeForm = Form.ActiveForm;
      if (activeForm != null)
      {
        Control activeControl = activeForm.ActiveControl;
        if (activeControl != null)
        {
          while (activeControl is ContainerControl && ((ContainerControl) activeControl).ActiveControl != null)
            activeControl = ((ContainerControl) activeControl).ActiveControl;
          return activeControl;
        }
      }
      return (Control) null;
    }

    public static bool IsDescendantOf(Control parent, Control descendant)
    {
      for (; descendant != null; descendant = descendant.Parent)
      {
        if (descendant.Parent != null && parent == descendant.Parent)
          return true;
      }
      return false;
    }

    protected void SetLoggingProvider(ILoggingProvider loggingProvider)
    {
      if (loggingProvider == null)
        return;
      this.Logger = loggingProvider.Logger;
    }

    [DebuggerNonUserCode]
    protected void Log(FR.Logging.LogLevel logLevel, string message, params object[] args)
    {
      if (this.Logger == null)
        return;
      this.Logger.Write(logLevel, 1, message, args);
    }

    [DebuggerNonUserCode]
    protected void LogEx(Exception ex)
    {
      if (this.Logger == null)
        return;
      this.Logger.WriteEx(ex, 1);
    }

    [DebuggerNonUserCode]
    protected void LogObject(FR.Logging.LogLevel logLevel, object obj)
    {
      if (this.Logger == null)
        return;
      this.Logger.WriteObject(logLevel, 1, obj);
    }

    [DebuggerNonUserCode]
    protected void ForceLog(FR.Logging.LogLevel logLevel, string message, params object[] args)
    {
      if (this.Logger == null)
        return;
      this.Logger.ForceLog(logLevel, 1, message, args);
    }

    [DebuggerNonUserCode]
    protected void ForceLog(Exception ex)
    {
      if (this.Logger == null)
        return;
      this.Logger.ForceLog(ex, 1);
    }

    [DebuggerNonUserCode]
    protected void CallEntry(FR.Logging.LogLevel logLevel)
    {
      if (this.Logger == null)
        return;
      this.Logger.CallEntry(logLevel, 1);
    }

    [DebuggerNonUserCode]
    protected void CallLeave(FR.Logging.LogLevel logLevel)
    {
      if (this.Logger == null)
        return;
      this.Logger.CallLeave(logLevel, 1);
    }

    [DebuggerNonUserCode]
    public virtual ICommandReceiver GetCommandReceiver(System.Type commandType) => (ICommandReceiver) null;

    [DebuggerNonUserCode]
    public virtual void InitializeCommand(ICommand command)
    {
    }

    [DebuggerNonUserCode]
    public virtual void FinishCommand(ICommand command)
    {
    }

    [DebuggerNonUserCode]
    protected override void OnLoad(EventArgs e)
    {
      if (this.ApplicationInstance != null)
        this.ApplicationInstance.OnRunning();
      base.OnLoad(e);
    }

    [DebuggerNonUserCode]
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      if (this.Height == 0 || this.Width == 0)
        return;
      Graphics graphics = pevent.Graphics;
      Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
      Brush brush = !(this.GradientColorStart == this.GradientColorEnd) ? (Brush) new LinearGradientBrush(rect, this.GradientColorStart, this.GradientColorEnd, 30f) : (Brush) new SolidBrush(this.BackColor);
      graphics.FillRectangle(brush, rect);
      brush.Dispose();
    }

    [DebuggerNonUserCode]
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this._bMoveForm = false;
    }

    [DebuggerNonUserCode]
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.MoveByClientArea)
        return;
      this._bMoveForm = true;
      this._ptMoveMouse = new Point(Cursor.Position.X, Cursor.Position.Y);
      this._ptMoveForm = new Point(this.Location.X, this.Location.Y);
    }

    [DebuggerNonUserCode]
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this._bMoveForm)
        return;
      this.Location = new Point(this._ptMoveForm.X + Cursor.Position.X - this._ptMoveMouse.X, this._ptMoveForm.Y + Cursor.Position.Y - this._ptMoveMouse.Y);
    }

    [DebuggerNonUserCode]
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this._bMoveForm = false;
    }

    LoggingManager ILoggingProvider.Logger
    {
      get => this.Logger;
      set => this.Logger = value;
    }

    [Browsable(false)]
    FR.Logging.LogLevel ILoggingProvider.LogLevel
    {
      get => this.LogLevel;
      set => this.LogLevel = value;
    }

    void ILoggingProvider.SetLoggingProvider(ILoggingProvider loggingProvider) => this.SetLoggingProvider(loggingProvider);

    void ILoggingProvider.Log(
      FR.Logging.LogLevel logLevel,
      string message,
      params object[] args)
    {
      this.Log(logLevel, message, args);
    }

    void ILoggingProvider.LogEx(Exception ex) => this.LogEx(ex);

    void ILoggingProvider.LogObject(FR.Logging.LogLevel logLevel, object obj) => this.LogObject(logLevel, obj);

    void ILoggingProvider.ForceLog(
      FR.Logging.LogLevel logLevel,
      string message,
      params object[] args)
    {
      this.ForceLog(logLevel, message, args);
    }

    void ILoggingProvider.ForceLog(Exception ex) => this.ForceLog(ex);

    void ILoggingProvider.CallEntry(FR.Logging.LogLevel logLevel) => this.CallEntry(logLevel);

    void ILoggingProvider.CallLeave(FR.Logging.LogLevel logLevel) => this.CallLeave(logLevel);

    IConfigurationSource IConfigurable.ConfigurationSource
    {
      get => this.ConfigurationSource;
      set
      {
      }
    }

    IConfigurationProperty IConfigurable.ConfigurationRoot
    {
      get => this.ConfigurationRoot;
      set => this.ConfigurationRoot = value;
    }

    [DebuggerNonUserCode]
    ICommandReceiver ICommandReceiver.GetCommandReceiver(
      System.Type commandType)
    {
      return this.GetCommandReceiver(commandType);
    }

    [DebuggerNonUserCode]
    void ICommandReceiver.InitializeCommand(ICommand command) => this.InitializeCommand(command);

    [DebuggerNonUserCode]
    void ICommandReceiver.FinishCommand(ICommand command) => this.FinishCommand(command);

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
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
