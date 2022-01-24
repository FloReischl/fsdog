// Decompiled with JetBrains decompiler
// Type: FsDog.CopyItem
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR;
using FR.Drawing;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace FsDog {
    public class CopyItem : INotifyPropertyChanged {
        private static Image _emptyImage = (Image)new Bitmap(16, 16);
        private bool _done;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FileInfo _destinationFile;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DirectoryInfo _destinationDirectory;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FileInfo _sourceFile;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DirectoryInfo _sourceDirectory;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private CopyErrorType _errorType;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private CopyErrorHandling _errorHandling;

        public CopyItem(FileInfo sourceFile, FileInfo destinationFile) {
            this._sourceFile = sourceFile;
            this._destinationFile = destinationFile;
            if (sourceFile.FullName == destinationFile.FullName) {
                this._errorType = CopyErrorType.LocationEqual;
            }
            else {
                if (!destinationFile.Exists)
                    return;
                this._errorType = CopyErrorType.AlreadyExists;
            }
        }

        public CopyItem(DirectoryInfo sourceDirectory, DirectoryInfo destinationDirectory) {
            this._sourceDirectory = sourceDirectory;
            this._destinationDirectory = destinationDirectory;
            if (this.SourceDirectory.FullName == this.DestinationDirectory.FullName) {
                this._errorType = CopyErrorType.LocationEqual;
            }
            else {
                if (!this.DestinationDirectory.Exists)
                    return;
                this._errorType = CopyErrorType.AlreadyExists;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [Browsable(true)]
        public Image Info {
            get {
                if (this.ErrorType == CopyErrorType.None)
                    return CopyItem._emptyImage;
                if (BaseHelper.InList(this.ErrorHandling, CopyErrorHandling.Handle, CopyErrorHandling.Skip))
                    return CopyItem._emptyImage;
                return this._done ? CommonImages.GetImage(CommonImageType.Accept) : CommonImages.GetImage(CommonImageType.Exclamation);
            }
        }

        [Browsable(true)]
        [DisplayName("Source/Destination")]
        public string DetailDescription {
            [DebuggerNonUserCode]
            get => this.SourceFile != null ? string.Format("{0}\r\n{1}", this.SourceFile.FullName, this.DestinationFile.FullName) : string.Format("{0}\r\n{1}", this.SourceDirectory.FullName, this.DestinationDirectory.FullName);
        }

        [Browsable(false)]
        public FileInfo DestinationFile {
            [DebuggerNonUserCode]
            get => this._destinationFile;
        }

        [Browsable(false)]
        public DirectoryInfo DestinationDirectory {
            [DebuggerNonUserCode]
            get => this._destinationDirectory;
        }

        [Browsable(false)]
        public FileInfo SourceFile {
            [DebuggerNonUserCode]
            get => this._sourceFile;
        }

        [Browsable(false)]
        public DirectoryInfo SourceDirectory {
            [DebuggerNonUserCode]
            get => this._sourceDirectory;
        }

        public string SourceFullName => this.SourceDirectory == null ? this.SourceFile.FullName : this.SourceDirectory.FullName;

        public string SourceParentName => this.SourceDirectory == null ? this.SourceFile.DirectoryName : this.SourceDirectory.Parent.Name;

        [Browsable(false)]
        public CopyErrorType ErrorType {
            [DebuggerNonUserCode]
            get => this._errorType;
        }

        [Browsable(false)]
        public bool Error => this.ErrorType != CopyErrorType.None && this.ErrorHandling == CopyErrorHandling.Unknown;

        [DisplayName("Error Description")]
        public string ErrorText {
            [DebuggerNonUserCode]
            get => this.GetErrorText();
        }

        [Browsable(false)]
        public CopyErrorHandling ErrorHandling {
            [DebuggerNonUserCode]
            get => this._errorHandling;
            [DebuggerNonUserCode]
            set {
                this._errorHandling = value;
                this.OnPropertyChanged(nameof(ErrorHandling));
                this.OnPropertyChanged("ErrorHandlingText");
                this.OnPropertyChanged("Error");
                this.OnPropertyChanged("Info");
            }
        }

        [DisplayName("Handling")]
        public string ErrorHandlingText => this.ErrorType != CopyErrorType.None ? this.ErrorHandling.ToString() : (string)null;

        public void DoAction(bool move) {
            if (this.SourceFile != null) {
                if (this.ErrorType == CopyErrorType.None) {
                    if (!this.DestinationFile.Directory.Exists)
                        this.DestinationFile.Directory.Create();
                    if (move)
                        this.SourceFile.MoveTo(this.DestinationFile.FullName);
                    else
                        this.SourceFile.CopyTo(this.DestinationFile.FullName);
                }
                else if (this.ErrorType == CopyErrorType.AlreadyExists) {
                    if (this.ErrorHandling != CopyErrorHandling.Skip && this.ErrorHandling == CopyErrorHandling.Handle) {
                        this.DestinationFile.Delete();
                        this.SourceFile.MoveTo(this.DestinationFile.FullName);
                    }
                }
                else if (this.ErrorType != CopyErrorType.LocationEqual) {
                }
            }
            else if (this.ErrorType == CopyErrorType.None) {
                if (!this.DestinationDirectory.Exists) {
                    if (move)
                        this.SourceDirectory.MoveTo(this.DestinationDirectory.FullName);
                }
                else if (move)
                    this.SourceDirectory.Delete();
            }
            else if (this.ErrorType == CopyErrorType.AlreadyExists) {
                if (this.ErrorHandling != CopyErrorHandling.Skip && this.ErrorHandling == CopyErrorHandling.Handle) {
                    if (!this.DestinationDirectory.Exists) {
                        if (move)
                            this.SourceDirectory.MoveTo(this.DestinationDirectory.FullName);
                    }
                    else if (move)
                        this.SourceDirectory.Delete();
                }
            }
            else {
                int errorType = (int)this.ErrorType;
            }
            this._done = true;
            this.OnPropertyChanged("Info");
        }

        private string GetErrorText() {
            switch (this.ErrorType) {
                case CopyErrorType.None:
                    return (string)null;
                case CopyErrorType.LocationEqual:
                    return "Source and destination are equal.";
                case CopyErrorType.AlreadyExists:
                    return "File or directory already exists.";
                default:
                    throw new Exception("Unknown error type");
            }
        }

        private void OnPropertyChanged(string propertyName) {
            if (this.PropertyChanged == null)
                return;
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
            [DebuggerNonUserCode]
            add => this.PropertyChanged += value;
            [DebuggerNonUserCode]
            remove => this.PropertyChanged -= value;
        }
    }
}
