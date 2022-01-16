// Decompiled with JetBrains decompiler
// Type: FsDog.DetailItem
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace FsDog {
    internal class DetailItem : DataRow {
        public DetailItem(DataRowBuilder builder)
          : base(builder) {
        }

        public int SortOrder {
            [DebuggerNonUserCode]
            get => (int)this[nameof(SortOrder)];
            [DebuggerNonUserCode]
            set => this[nameof(SortOrder)] = (object)value;
        }

        public Image Image {
            [DebuggerNonUserCode]
            get => (Image)this[nameof(Image)];
            [DebuggerNonUserCode]
            set => this[nameof(Image)] = (object)value;
        }

        public string Name {
            [DebuggerNonUserCode]
            get => (string)this[nameof(Name)];
            [DebuggerNonUserCode]
            set => this[nameof(Name)] = (object)value;
        }

        public string Extension {
            [DebuggerNonUserCode]
            get => (string)this[nameof(Extension)];
            [DebuggerNonUserCode]
            set => this[nameof(Extension)] = (object)value;
        }

        public DateTime DateModified {
            [DebuggerNonUserCode]
            get => (DateTime)this[nameof(DateModified)];
            [DebuggerNonUserCode]
            set => this[nameof(DateModified)] = (object)value;
        }

        public DateTime DateCreated {
            [DebuggerNonUserCode]
            get => (DateTime)this[nameof(DateCreated)];
            [DebuggerNonUserCode]
            set => this[nameof(DateCreated)] = (object)value;
        }

        public string TypeName {
            [DebuggerNonUserCode]
            get => (string)this[nameof(TypeName)];
            [DebuggerNonUserCode]
            set => this[nameof(TypeName)] = (object)value;
        }

        public object Size {
            [DebuggerNonUserCode]
            get => this[nameof(Size)];
            [DebuggerNonUserCode]
            set => this[nameof(Size)] = value;
        }

        public string Attributes {
            [DebuggerNonUserCode]
            get => (string)this[nameof(Attributes)];
            [DebuggerNonUserCode]
            set => this[nameof(Attributes)] = (object)value;
        }

        public FileSystemInfo FileSystemInfo {
            [DebuggerNonUserCode]
            get => (FileSystemInfo)this[nameof(FileSystemInfo)];
            [DebuggerNonUserCode]
            set => this[nameof(FileSystemInfo)] = (object)value;
        }

        public FileInfo FileInfo {
            [DebuggerNonUserCode]
            get => this.FileSystemInfo as FileInfo;
            [DebuggerNonUserCode]
            set => this.FileSystemInfo = (FileSystemInfo)value;
        }

        public DirectoryInfo DirectoryInfo {
            [DebuggerNonUserCode]
            get => this.FileSystemInfo as DirectoryInfo;
            [DebuggerNonUserCode]
            set => this.FileSystemInfo = (FileSystemInfo)value;
        }

        public string ParentPath {
            get => (string)this[nameof(ParentPath)];
            set => this[nameof(ParentPath)] = value;
        }
    }
}
