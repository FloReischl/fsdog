// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.WindowsApplication
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using FR.Commands;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace FR.Windows.Forms {
    public abstract class WindowsApplication : ApplicationBase {
        private System.Type _mainFormType;
        private bool _running;
        private Form _mainForm;
        private ApplicationContext _applicationContext;
        private EmbeddedResourceManager _embeddedResourceManager;
        private Color _validationErrorColor;

        protected WindowsApplication() {
            this._validationErrorColor = Color.Red;
            this._running = false;
        }

        public static new WindowsApplication Instance {
            [DebuggerNonUserCode]
            get => (WindowsApplication)ApplicationBase.Instance;
        }

        public Form MainForm {
            [DebuggerNonUserCode]
            get => this._mainForm;
        }

        public ApplicationContext ApplicationContext => this._applicationContext;

        public EmbeddedResourceManager EmbeddedResourceManager {
            [DebuggerNonUserCode]
            get {
                if (this._embeddedResourceManager == null) {
                    this._embeddedResourceManager = new EmbeddedResourceManager(this.GetType());
                    this._embeddedResourceManager.DefaultMask = string.Format("{0}.{{0}}", (object)this.GetType().Namespace);
                }
                return this._embeddedResourceManager;
            }
            [DebuggerNonUserCode]
            set => this._embeddedResourceManager = value;
        }

        public Color ValidationErrorColor {
            get => this._validationErrorColor;
            set => this._validationErrorColor = value;
        }

        public event EventHandler Running;

        public static WindowsApplication Create(System.Type appType) {
            if (WindowsApplication.Instance == null)
                ((appType.IsSubclassOf(typeof(WindowsApplication)) ? appType.GetConstructor(System.Type.EmptyTypes) : throw new InvalidCastException(string.Format("Specified type '{0}' does not derive from '{1}'", (object)appType, (object)typeof(WindowsApplication)))) ?? throw new MissingMethodException(string.Format("Specified application type '{0}' does not implement an empty constructor", (object)appType))).Invoke(new object[0]);
            return WindowsApplication.Instance;
        }

        public static void Exit() => Application.Exit();

        public override void Start() => base.Start();

        public virtual void Start(System.Type mainFormType) {
            this._mainFormType = mainFormType;
            base.Start();
        }

        public override void Initialize() {
            base.Initialize();
            if (this._mainForm != null || this._mainFormType != null)
                return;
            string typeName = this.ConfigurationRoot.GetSubProperty("MainForm").ToString();
            this._mainFormType = System.Type.GetType(typeName);
            if (this._mainFormType == null)
                throw new InvalidCastException(string.Format("Cannot find type of MainForm by name '{0}'", (object)typeName));
        }

        public override void Run() {
            if (Application.VisualStyleState != VisualStyleState.NoneEnabled)
                Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SynchronizationContext.SetSynchronizationContext(
                new WindowsFormsSynchronizationContext());
            if (this._mainForm == null)
                this._mainForm = (Form)this._mainFormType.GetConstructor(System.Type.EmptyTypes).Invoke(new object[0]);
            this._applicationContext = new ApplicationContext(this._mainForm);
            Application.Run(this.ApplicationContext);
        }

        [DebuggerNonUserCode]
        public Icon GetAppIcon() => Icon.ExtractAssociatedIcon(this.GetType().Assembly.Location);

        [DebuggerNonUserCode]
        public virtual Icon GetEmbeddedIcon(string name, bool useDefaultMask) {
            Stream stream = this.EmbeddedResourceManager.GetStream(name, useDefaultMask);
            return stream == null ? (Icon)null : new Icon(stream);
        }

        [DebuggerNonUserCode]
        public virtual Image GetEmbeddedImage(string name, bool useDefaultMask) {
            Stream stream = this.EmbeddedResourceManager.GetStream(name, useDefaultMask);
            return stream == null ? (Image)null : Image.FromStream(stream);
        }

        [DebuggerNonUserCode]
        public virtual Stream GetEmbeddedStream(string name, bool useDefaultMask) => this.EmbeddedResourceManager.GetStream(name, useDefaultMask);

        [DebuggerNonUserCode]
        public override ICommandReceiver GetCommandReceiver(System.Type commandType) {
            if (commandType == null)
                return (ICommandReceiver)null;
            Form activeForm = Form.ActiveForm;
            if (activeForm != null && activeForm is ICommandReceiver)
                return ((ICommandReceiver)activeForm).GetCommandReceiver(commandType);
            return this.MainForm != null && this.MainForm is ICommandReceiver ? ((ICommandReceiver)this.MainForm).GetCommandReceiver(commandType) : base.GetCommandReceiver(commandType);
        }

        internal void OnRunning() {
            if (this.Running == null || this._running)
                return;
            this._running = true;
            this.Running((object)this, EventArgs.Empty);
        }
    }
}
