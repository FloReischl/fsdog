// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.FileTypeAssociator
// Assembly: FR.Base, Version=1.0.0.0, Culture=neutral, PublicKeyToken=3960b0ad2b864944
// MVID: E4325E6A-7973-47D1-9B4E-B328A6EAD270
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Base.dll

using System.IO;

namespace Microsoft.Win32 {
    public class FileTypeAssociator {
        private string _fileExtension;
        private string _shellOpenDescription;
        private string _alias;
        private string _fileDescription;
        private string _openCommand;

        public string FileExtension {
            get => this._fileExtension;
            set => this._fileExtension = value;
        }

        public string ShellOpenDescription {
            get => this._shellOpenDescription;
            set => this._shellOpenDescription = value;
        }

        public string Alias {
            get => this._alias;
            set => this._alias = value;
        }

        public string FileDescription {
            get => this._fileDescription;
            set => this._fileDescription = value;
        }

        public string OpenCommand {
            get => this._openCommand;
            set => this._openCommand = value;
        }

        public FileTypeAssociator() {
            this.FileExtension = "";
            this.ShellOpenDescription = "";
            this.Alias = "";
            this.FileDescription = "";
            this.OpenCommand = "";
        }

        public void Associate() {
            if (string.IsNullOrEmpty(this.FileExtension))
                throw new FileTypeAssociatorException("FileExtension cannot be empty", new object[0]);
            if (Registry.ClassesRoot.OpenSubKey(this.FileExtension) != null)
                throw new FileTypeAssociatorException("File extension '{0}' already exists", new object[1]
                {
          (object) this.FileExtension
                });
            if (string.IsNullOrEmpty(this.ShellOpenDescription))
                this.ShellOpenDescription = "Open";
            if (string.IsNullOrEmpty(this.Alias))
                this.Alias = string.Format("{0}_file", (object)this.FileExtension.ToLower());
            if (Registry.ClassesRoot.OpenSubKey(this.Alias) != null)
                throw new FileTypeAssociatorException("File alias '{0}' already exists", new object[1]
                {
          (object) this.Alias
                });
            if (string.IsNullOrEmpty(this.FileDescription))
                this.FileDescription = string.Format("{0} File", (object)this.FileExtension);
            if (string.IsNullOrEmpty(this.OpenCommand))
                throw new FileTypeAssociatorException("Open command cannot be empty", new object[0]);
            if (!this.OpenCommand.Contains("%1") && !File.Exists(this.OpenCommand))
                throw new FileTypeAssociatorException("Open command '{0}' does not exist wild-card %1 for associated files and does not exists", new object[0]);
            if (!this.OpenCommand.Contains("%1") && File.Exists(this.OpenCommand))
                this.OpenCommand = string.Format("{0} \"%1\"", (object)this.OpenCommand);
            Registry.ClassesRoot.CreateSubKey(this.FileExtension).SetValue((string)null, (object)this.Alias, RegistryValueKind.String);
            RegistryKey subKey1 = Registry.ClassesRoot.CreateSubKey(this.Alias);
            subKey1.SetValue((string)null, (object)this.FileDescription);
            RegistryKey subKey2 = subKey1.CreateSubKey("Shell").CreateSubKey("open");
            subKey2.SetValue((string)null, (object)this.ShellOpenDescription, RegistryValueKind.String);
            subKey2.CreateSubKey("command").SetValue((string)null, (object)this.OpenCommand, RegistryValueKind.String);
        }
    }
}
