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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;

namespace FR {
    public abstract class ApplicationBase : /*LoggingProvider,*/ IDisposable, ICommandHandler/*, IConfigurable */{
        protected ApplicationBase() {
            ApplicationBase.Instance = this;
            this.Information = new ApplicationInformation();
        }

        public static ApplicationBase Instance { get; private set; }

        public ILogger Log { get; } = LoggingProvider.CreateLogger();

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

        public CommandLineArgs CommandLineArgs { get; private set; }

        public abstract void Run();

        public virtual void Start() {
            this.Initialize();
            this.Run();
            this.Dispose();
        }

        public virtual void Initialize() {
            this.CommandLineArgs = new CommandLineArgs(Environment.GetCommandLineArgs());
        }

        public virtual void Dispose() {
            LoggingProvider.Flush();
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
                Log.Debug("Starting Execute Command");
                if (command == null)
                    throw ExceptionHelper.GetArgumentNull(nameof(command));

                Log.Info($"initializing command type {command.GetType()}");
                if (context != null) {
                    command.Context = context;
                }

                this.InitializeCommand(command);
                if (command.Receiver == null) {
                    Log.Warn($"No receiver found for command {command.GetType()}");
                    return;
                }

                Log.Info($"executing command {command.GetType()}");
                command.Execute();

                Log.Debug($"Completing command {command.GetType()}");
                this.FinishCommand(command);

                Log.Info($"Command {command.GetType()} completed");
            }
            catch (Exception ex) {
                this.Log.Ex(ex);
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
                this.Log.Ex(ex);
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

        void IDisposable.Dispose() => this.Dispose();

        [DebuggerNonUserCode]
        bool ICommandHandler.CanExecuteCommand(Type commandType) => this.CanExecuteCommand(commandType);

        [DebuggerNonUserCode]
        void ICommandHandler.ExecuteCommand(Type commandType, DataContext context) => this.ExecuteCommand(commandType, context);

        [DebuggerNonUserCode]
        void ICommandHandler.ExecuteCommand(ICommand command, DataContext context) => this.ExecuteCommand(command, context);
    }
}
