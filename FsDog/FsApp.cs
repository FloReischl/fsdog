// Decompiled with JetBrains decompiler
// Type: FsDog.FsApp
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR;
using FR.Configuration;
using FR.Drawing;
using FR.IO;
using FR.Logging;
using FR.Windows.Forms;
using FsDog.Commands;
using FsDog.Configuration;
using FsDog.Dialogs;
using FsDog.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace FsDog {
    public class FsApp : WindowsApplication {
        private Dictionary<string, Image> _fileImages;
        private Dictionary<string, string> _fileTypeNames;
        private HashSet<Type> _myCommands = new HashSet<Type> { typeof(CmdAppSaveConfig) };

        [STAThread]
        private static void Main() {
            FsApp fsApp = new FsApp();
            fsApp.Start(typeof(FormMain));
            fsApp.Config.Save();
        }

        private FsApp() {
        }

        public static new FsApp Instance => (FsApp)WindowsApplication.Instance;

        public FsDogConfig Config { get; set; }

        public Dictionary<string, ScriptingHostConfiguration> ScriptingHosts { get; set; }

        public new FormMain MainForm => (FormMain)base.MainForm;

        public string DefaultDirectoryName => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public void ClearImageCache() => this._fileImages.Clear();

        public ILogger CreateLogger() => LoggingProvider.CreateLogger();

        public override void Initialize() {
            Application.ThreadException += new ThreadExceptionEventHandler(this.Application_ThreadException);
            Config = FsDogConfig.Load();
            base.Initialize();
            _fileImages = new Dictionary<string, Image>();
            _fileTypeNames = new Dictionary<string, string>();
            ReloadScriptingHosts();
        }

        public override bool CanExecuteCommand(Type commandType) {
            return _myCommands.Contains(commandType) || base.CanExecuteCommand(commandType);
        }

        public void ExecuteFile(FileInfo fi) => new Process() {
            StartInfo = {
                WorkingDirectory = fi.DirectoryName,
                FileName = fi.FullName,
                UseShellExecute = true
            }
        }.Start();

        public List<FileInfo> GetFiles(DirectoryInfo dir) {
            FileInfo[] files1 = dir.GetFiles();
            List<FileInfo> files2 = new List<FileInfo>(files1.Length);
            foreach (FileInfo fsi in files1) {
                if (this.IsValidToShow((FileSystemInfo)fsi))
                    files2.Add(fsi);
            }
            return files2;
        }

        public Image GetFsiImage(FileSystemInfo fsi) {
            switch (fsi) {
                case DirectoryInfo _:
                    return (Image)Resources.DirectoryClosed;
                case FileInfo fileInfo:
                    string lower = fileInfo.Extension.ToLower();
                    Image associatedImage;
                    if (BaseHelper.InList((object)lower, (object)".exe", (object)".scr", (object)".lnk", (object)".ico", (object)".cur")) {
                        if (!this._fileImages.TryGetValue(fileInfo.FullName, out associatedImage)) {
                            associatedImage = ImageHelper.ExtractAssociatedImage(fileInfo.FullName, true);
                            if (this.Config.Options.General.CacheImages)
                                _fileImages.Add(fileInfo.FullName, associatedImage);
                        }
                    }
                    else if (!this._fileImages.TryGetValue(lower, out associatedImage)) {
                        associatedImage = ImageHelper.ExtractAssociatedImage(fileInfo.FullName, true);
                        if (this.Config.Options.General.CacheImages)
                            _fileImages.Add(lower, associatedImage);
                    }
                    return associatedImage;
                default:
                    throw new Exception("Unknown fsi type");
            }
        }

        public string GetFsiTypeName(FileSystemInfo fsi) {
            switch (fsi) {
                case DirectoryInfo _:
                    return "Directory";
                case FileInfo fileInfo:
                    string lower = fileInfo.Extension.ToLower();
                    string typeName;
                    if (!this._fileTypeNames.TryGetValue(lower, out typeName)) {
                        typeName = FileHelper.GetTypeName(fileInfo.FullName);
                        _fileTypeNames.Add(lower, typeName);
                    }
                    return typeName;
                default:
                    throw new Exception("Unknown fsi type");
            }
        }

        public List<DirectoryInfo> GetSubDirectories(DirectoryInfo dir) {
            DirectoryInfo[] directories = dir.GetDirectories();
            List<DirectoryInfo> subDirectories = new List<DirectoryInfo>(directories.Length);
            foreach (DirectoryInfo fsi in directories) {
                if (IsValidToShow((FileSystemInfo)fsi))
                    subDirectories.Add(fsi);
            }
            return subDirectories;
        }

        public bool IsValidToShow(FileSystemInfo fsi) => ((fsi.Attributes & FileAttributes.System) == (FileAttributes)0 || this.Config.Options.Navigation.ShowSystemFiles) && ((fsi.Attributes & FileAttributes.Hidden) == (FileAttributes)0 || this.Config.Options.Navigation.ShowHiddenFiles);

        public void ReloadScriptingHosts() {
            ScriptingHosts = new Dictionary<string, ScriptingHostConfiguration>();
            foreach (ScriptingHostConfiguration scriptingHost in CommandHelper.GetScriptingHosts())
                ScriptingHosts.Add(scriptingHost.Name, scriptingHost);
        }

        public void ShowInfoMessage(string format, params object[] args) => this.MainForm.ShowInfoMessage(format, args);

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
            try {
                FormError.ShowException(e.Exception, (IWin32Window)null);
            }
            catch {
                int num = (int)MessageBox.Show(ExceptionHelper.GetCompleteMessage(e.Exception), "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally {
                WindowsApplication.Exit();
            }
        }
    }
}
