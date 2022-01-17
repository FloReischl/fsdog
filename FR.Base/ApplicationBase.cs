// Decompiled with JetBrains decompiler
// Type: FR.ApplicationBase
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using FR.Collections;
using FR.Commands;
using FR.Configuration;
using FR.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;

namespace FR {
    public abstract class ApplicationBase :
      LoggingProvider,
      IDisposable,
      ICommandHandler,
      IConfigurable {
        private XmlDocument _fallbackLoggingDom;
        private ConfigurationFile _fallbackLoggingConfigurationSource;
        private bool _fallbackLoggerCreated;
        private static ApplicationBase _instance;
        private IConfigurationSource _configurationSource;
        private IConfigurationProperty _configurationRoot;
        private CommandLineArgs _commandLineArgs;

        protected ApplicationBase() {
            ApplicationBase._instance = this;
            this.Information = new ApplicationInformation();
        }

        public event EventHandler Disposing;

        public static ApplicationBase Instance {
            [DebuggerNonUserCode]
            get => ApplicationBase._instance;
        }

        public DirectoryInfo ExecutableDirectory => new FileInfo(Assembly.GetEntryAssembly().Location).Directory;

        public FileInfo ExecutableFile => new FileInfo(Assembly.GetEntryAssembly().Location);

        public IConfigurationSource ConfigurationSource {
            get {
                if (this._configurationSource == null && File.Exists(ConfigurationFile.GetDefaultConfigFileName(this.GetType().Assembly)))
                    this._configurationSource = (IConfigurationSource)new ConfigurationFile();
                return this._configurationSource;
            }
            set {
                this.ResetFallbackLogger();
                this._configurationSource = value;
            }
        }

        public IConfigurationProperty ConfigurationRoot {
            get {
                if (this._configurationRoot == null && this.ConfigurationSource != null)
                    this._configurationRoot = this.ConfigurationSource.GetProperty(".", this.GetType().Name, true);
                return this._configurationRoot;
            }
            set => this._configurationRoot = value;
        }

        public override LoggingManager Logger {
            [DebuggerNonUserCode]
            get {
                if (base.Logger == null) {
                    if ((this.ConfigurationSource == null || !this.ConfigurationSource.ExistsProperty(".", "Logging")) && !this._fallbackLoggerCreated) {
                        this._fallbackLoggingDom = new XmlDocument();
                        this._fallbackLoggingDom.AppendChild((XmlNode)this._fallbackLoggingDom.CreateElement("Configuration"));
                        this._fallbackLoggingConfigurationSource = new ConfigurationFile((XmlElement)this._fallbackLoggingDom.SelectSingleNode("Configuration"));
                        this._fallbackLoggingConfigurationSource.SetProperty("Logging/Device", "ClassName", typeof(LoggingDeviceConsole).ToString());
                        this._fallbackLoggerCreated = true;
                        base.Logger = new LoggingManager((IConfigurationProperty)this._fallbackLoggingConfigurationSource.GetProperty(".", "Logging", false));
                    }
                    else if (this.ConfigurationSource.ExistsProperty(".", "Logging"))
                        base.Logger = new LoggingManager(this.ConfigurationSource.GetProperty(".", "Logging", false));
                }
                return base.Logger;
            }
            [DebuggerNonUserCode]
            set {
                this.ResetFallbackLogger();
                base.Logger = value;
            }
        }

        public FileInfo ApplicationFileInfo => new FileInfo(this.GetType().Assembly.Location);

        public ApplicationInformation Information { get; private set; }

        public DirectoryInfo ApplicationDataDirectoryInfo {
            get {
                string path1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (!string.IsNullOrEmpty(this.Information.CompanyName))
                    path1 = Path.Combine(path1, this.Information.CompanyName);
                return new DirectoryInfo(string.IsNullOrEmpty(this.Information.ProductName) ? Path.Combine(path1, this.ApplicationFileInfo.Name.Substring(0, this.ApplicationFileInfo.Name.Length - this.ApplicationFileInfo.Extension.Length)) : Path.Combine(path1, this.Information.ProductName));
            }
        }

        public CommandLineArgs CommandLineArgs => this._commandLineArgs;

        public abstract void Run();

        public virtual void Start() {
            this.Initialize();
            this.Run();
            this.Dispose();
        }

        public virtual void Initialize() {
            this._commandLineArgs = new CommandLineArgs(Environment.GetCommandLineArgs());
            CommandLineArg byArgument = this.CommandLineArgs.FindByArgument("config", false);
            if (byArgument != null)
                this.ConfigurationSource = (IConfigurationSource)new ConfigurationFile(byArgument.Value, ConfigurationFile.FileAccessMode.CreateIfNotExists);
            else if (this.ConfigurationSource == null && File.Exists(ConfigurationFile.GetDefaultConfigFileName(this.GetType().Assembly)))
                this.ConfigurationSource = (IConfigurationSource)new ConfigurationFile();
            if (this.ConfigurationSource != null && this.ConfigurationSource.ExistsProperty(".", "Logging"))
                this.Logger = new LoggingManager(this.ConfigurationSource.GetProperty(".", "Logging", false));
            if (this.ConfigurationSource == null || !this.ConfigurationSource.ExistsProperty(".", this.GetType().Name))
                return;
            this.ConfigurationRoot = this.ConfigurationSource.GetProperty(".", this.GetType().Name, true);
        }

        public virtual void Dispose() {
            if (this.Disposing != null)
                Disposing((object)this, EventArgs.Empty);
            if (this.Logger == null || this.Logger.IsClosed)
                return;
            this.Logger.Flush();
        }

        [DebuggerNonUserCode]
        public virtual bool CanExecuteCommand(Type commandType) {
            if (commandType == null)
                return false;
            Type[] interfaces = commandType.GetInterfaces();
            bool flag = false;
            foreach (Type type in interfaces) {
                if (type == typeof(ICommand)) {
                    flag = true;
                    break;
                }
            }
            return flag && this.GetCommandReceiver(commandType) != null;
        }

        public virtual void ExecuteCommand(ICommand command, DataContext context) {
            try {
                if (command == null)
                    throw ExceptionHelper.GetArgumentNull(nameof(command));
                command.InstanceState = CommandInstanceState.Initializing;
                if (context != null)
                    command.SetContext(context);
                this.InitializeCommand(command);
                if (command.Receiver == null)
                    return;
                command.InstanceState = CommandInstanceState.Executing;
                command.Execute();
                command.InstanceState = CommandInstanceState.Finishing;
                this.FinishCommand(command);
                command.InstanceState = CommandInstanceState.Finished;
            }
            catch (Exception ex) {
                this.LogEx(ex);
                throw ex;
            }
        }

        [DebuggerNonUserCode]
        public virtual void ExecuteCommand(Type commandType, DataContext context) {
            Type[] typeArray = commandType != null ? commandType.GetInterfaces() : throw ExceptionHelper.GetArgumentNull(nameof(commandType));
            bool flag = false;
            foreach (Type type in typeArray) {
                if (type == typeof(ICommand)) {
                    flag = true;
                    break;
                }
            }
            if (!flag)
                throw new InvalidCastException(string.Format("Type '{0}' does not implement interface '{1}'", (object)commandType.GetType(), (object)typeof(ICommand)));
            ConstructorInfo constructor = commandType.GetConstructor(Type.EmptyTypes);
            if (constructor != null) {
                this.ExecuteCommand((ICommand)constructor.Invoke(new object[0]), context);
            }
            else {
                Exception ex = (Exception)new MissingMethodException(string.Format("Type '{0}' does not have an empty constructor to invoke", (object)commandType));
                this.LogEx(ex);
                throw ex;
            }
        }

        [DebuggerNonUserCode]
        public virtual ICommandReceiver GetCommandReceiver(Type commandType) => (ICommandReceiver)null;

        [DebuggerNonUserCode]
        public virtual void InitializeCommand(ICommand command) {
            if (command == null)
                throw ExceptionHelper.GetArgumentNull(nameof(command));
            if (command.Receiver == null)
                command.Receiver = this.GetCommandReceiver(command.GetType());
            if (command.Receiver == null || command.Receiver == this)
                return;
            command.Receiver.InitializeCommand(command);
        }

        [DebuggerNonUserCode]
        public virtual void FinishCommand(ICommand command) {
            if (command == null)
                throw ExceptionHelper.GetArgumentNull(nameof(command));
            if (command.Receiver == this)
                return;
            command.Receiver.FinishCommand(command);
        }

        private void ResetFallbackLogger() {
            if (!this._fallbackLoggerCreated)
                return;
            if (this._fallbackLoggingConfigurationSource != null) {
                if (base.Logger != null && !base.Logger.IsClosed)
                    base.Logger.Close();
                base.Logger = (LoggingManager)null;
                this._fallbackLoggingConfigurationSource = (ConfigurationFile)null;
            }
            if (this._fallbackLoggingDom == null)
                return;
            this._fallbackLoggingDom = (XmlDocument)null;
        }

        void IDisposable.Dispose() => this.Dispose();

        [DebuggerNonUserCode]
        bool ICommandHandler.CanExecuteCommand(Type commandType) => this.CanExecuteCommand(commandType);

        [DebuggerNonUserCode]
        void ICommandHandler.ExecuteCommand(Type commandType, DataContext context) => this.ExecuteCommand(commandType, context);

        [DebuggerNonUserCode]
        void ICommandHandler.ExecuteCommand(ICommand command, DataContext context) => this.ExecuteCommand(command, context);

        IConfigurationSource IConfigurable.ConfigurationSource {
            get => this.ConfigurationSource;
            set => this.ConfigurationSource = value;
        }

        IConfigurationProperty IConfigurable.ConfigurationRoot {
            get => this.ConfigurationRoot;
            set => this.ConfigurationRoot = value;
        }
    }
}
