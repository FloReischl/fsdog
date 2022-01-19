// Decompiled with JetBrains decompiler
// Type: FsDog.DetailTable
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using FR.Windows.Forms;
using System;
using System.Data;
using System.Drawing;
using System.IO;
//using System.Windows.Forms;

namespace FsDog.Detail {
    internal class DetailTable : DataTable {
        private readonly FsApp _app;

        public DetailTable() {
            this._app = FsApp.Instance;
            this.Columns.Add(new DataColumn("SortOrder", typeof(int)));
            this.Columns.Add(new DataColumn("Image", typeof(Image)));
            this.Columns.Add(new DataColumn("Name", typeof(string)));
            this.Columns.Add(new DataColumn("Extension", typeof(string)));
            this.Columns.Add(new DataColumn("Size", typeof(ulong)));
            this.Columns.Add(new DataColumn("TypeName", typeof(string)));
            this.Columns.Add(new DataColumn("DateModified", typeof(DateTime)));
            this.Columns.Add(new DataColumn("DateCreated", typeof(DateTime)));
            this.Columns.Add(new DataColumn("Attributes", typeof(string)));
            this.Columns.Add(new DataColumn("FileSystemInfo", typeof(FileSystemInfo)));
            this.Columns.Add(new DataColumn("ParentPath", typeof(string)));
        }

        public DetailItem Add(FileInfo fi) {
            DetailItem row = this.NewItem();
            if (!this.Update(row, fi))
                return (DetailItem)null;
            this.Rows.Add((DataRow)row);
            return row;
        }

        public DetailItem Add(DirectoryInfo dir) {
            DetailItem row = this.NewItem();
            if (!this.Update(row, dir))
                return (DetailItem)null;
            this.Rows.Add((DataRow)row);
            return row;
        }

        public DetailItem FindItemByName(string name) {
            foreach (DetailItem row in (InternalDataCollectionBase)this.Rows) {
                if (string.Compare(name, row.Name) == 0)
                    return row;
            }
            return (DetailItem)null;
        }

        public bool Update(DetailItem item, FileInfo fi) {
            if (item == null)
                return false;
            try {
                Image fsiImage = FsApp.Instance.GetFsiImage((FileSystemInfo)fi);
                item.Image = fsiImage;
                item.Name = fi.Name;
                item.Extension = fi.Extension;
                item.FileInfo = fi;
                item.Size = (object)(ulong)fi.Length;
                item.TypeName = FsApp.Instance.GetFsiTypeName((FileSystemInfo)fi);
                item.DateModified = fi.LastWriteTime;
                item.DateCreated = fi.CreationTime;
                item.SortOrder = !FsApp.Instance.Config.Options.DetailView.DirectoriesAlwasOnTop ? 0 : 1;
                FileAttributes attributes = fi.Attributes;
                item.Attributes = "";
                item.Attributes += (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly ? "r" : "-";
                item.Attributes += (attributes & FileAttributes.Archive) == FileAttributes.ReadOnly ? "a" : "-";
                item.Attributes += (attributes & FileAttributes.Hidden) == FileAttributes.ReadOnly ? "h" : "-";
                item.Attributes += (attributes & FileAttributes.System) == FileAttributes.ReadOnly ? "s" : "-";
                return true;
            }
            catch (FileNotFoundException) {
                if (item.RowState != DataRowState.Detached)
                    this.Rows.Remove((DataRow)item);
                return false;
            }
            catch (Exception ex) {
                FormError.ShowException(ex, FsApp.Instance.MainForm);
                return false;
            }
        }

        public bool Update(DetailItem item, DirectoryInfo dir) {
            if (item == null)
                return false;
            try {
                item.Image = FsApp.Instance.GetFsiImage((FileSystemInfo)dir);
                item.Name = dir.Name;
                item.Extension = string.Empty;
                item.DirectoryInfo = dir;
                item.Size = (object)DBNull.Value;
                item.TypeName = FsApp.Instance.GetFsiTypeName((FileSystemInfo)dir);
                item.DateModified = dir.LastWriteTime;
                item.DateCreated = dir.CreationTime;
                item.SortOrder = 0;
                return true;
            }
            catch (DirectoryNotFoundException) {
                if (item.RowState != DataRowState.Detached)
                    this.Rows.Remove((DataRow)item);
                return false;
            }
            catch (Exception ex) {
                FormError.ShowException(ex, FsApp.Instance.MainForm);
                return false;
            }
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow)new DetailItem(builder);

        private DetailItem NewItem() => (DetailItem)this.NewRow();
    }
}
