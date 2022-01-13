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
using System.Windows.Forms;

namespace FsDog
{
  internal class DetailTable : DataTable
  {
    private FsApp _app;

    public DetailTable()
    {
      this._app = FsApp.Instance;
      this.Columns.Add(new DataColumn("SortOrder", typeof (int)));
      this.Columns.Add(new DataColumn("Image", typeof (Image)));
      this.Columns.Add(new DataColumn("Name", typeof (string)));
      this.Columns.Add(new DataColumn("Extension", typeof (string)));
      this.Columns.Add(new DataColumn("Size", typeof (ulong)));
      this.Columns.Add(new DataColumn("TypeName", typeof (string)));
      this.Columns.Add(new DataColumn("DateModified", typeof (DateTime)));
      this.Columns.Add(new DataColumn("DateCreated", typeof (DateTime)));
      this.Columns.Add(new DataColumn("Attributes", typeof (string)));
      this.Columns.Add(new DataColumn("FileSystemInfo", typeof (FileSystemInfo)));
    }

    public DetailItem Add(FileInfo fi)
    {
      DetailItem row = this.NewItem();
      if (!this.Update(row, fi))
        return (DetailItem) null;
      this.Rows.Add((DataRow) row);
      return row;
    }

    public DetailItem Add(DirectoryInfo dir)
    {
      DetailItem row = this.NewItem();
      if (!this.Update(row, dir))
        return (DetailItem) null;
      this.Rows.Add((DataRow) row);
      return row;
    }

    public DataGridViewColumn CreateGridColumn(DataColumn column)
    {
      DataGridViewColumn gridColumn = (DataGridViewColumn) null;
      if (column.ColumnName == "Name")
      {
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        viewTextBoxColumn.HeaderText = "Name";
        gridColumn = (DataGridViewColumn) viewTextBoxColumn;
      }
      else if (column.ColumnName == "DateModified" && this._app.Options.DetailView.ShowModificationDateColumn)
      {
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        viewTextBoxColumn.HeaderText = "Modified";
        gridColumn = (DataGridViewColumn) viewTextBoxColumn;
      }
      else if (column.ColumnName == "DateCreated" && this._app.Options.DetailView.ShowCreationDateColumn)
      {
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        viewTextBoxColumn.HeaderText = "Created";
        gridColumn = (DataGridViewColumn) viewTextBoxColumn;
      }
      else if (column.ColumnName == "Extension" && this._app.Options.DetailView.ShowFileExtensionColumn)
      {
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        viewTextBoxColumn.HeaderText = "Ext";
        gridColumn = (DataGridViewColumn) viewTextBoxColumn;
      }
      else if (column.ColumnName == "Size")
      {
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        viewTextBoxColumn.DefaultCellStyle.Format = "#,##0";
        gridColumn = (DataGridViewColumn) viewTextBoxColumn;
      }
      else if (column.ColumnName == "TypeName")
      {
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        viewTextBoxColumn.HeaderText = "Type";
        gridColumn = (DataGridViewColumn) viewTextBoxColumn;
      }
      else if (column.ColumnName == "Attributes" && this._app.Options.DetailView.ShowAttributesColumn)
      {
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        viewTextBoxColumn.HeaderText = "Attrs";
        gridColumn = (DataGridViewColumn) viewTextBoxColumn;
      }
      else if (column.ColumnName == "Image")
      {
        DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
        gridViewImageColumn.Resizable = DataGridViewTriState.False;
        gridViewImageColumn.HeaderText = "";
        gridColumn = (DataGridViewColumn) gridViewImageColumn;
      }
      if (gridColumn != null)
      {
        gridColumn.Name = column.ColumnName;
        gridColumn.DataPropertyName = column.ColumnName;
      }
      return gridColumn;
    }

    public DetailItem FindItemByName(string name)
    {
      foreach (DetailItem row in (InternalDataCollectionBase) this.Rows)
      {
        if (string.Compare(name, row.Name) == 0)
          return row;
      }
      return (DetailItem) null;
    }

    public bool Update(DetailItem item, FileInfo fi)
    {
      if (item == null)
        return false;
      try
      {
        Image fsiImage = FsApp.Instance.GetFsiImage((FileSystemInfo) fi);
        item.Image = fsiImage;
        item.Name = fi.Name;
        item.Extension = fi.Extension;
        item.FileInfo = fi;
        item.Size = (object) (ulong) fi.Length;
        item.TypeName = FsApp.Instance.GetFsiTypeName((FileSystemInfo) fi);
        item.DateModified = fi.LastWriteTime;
        item.DateCreated = fi.CreationTime;
        item.SortOrder = !FsApp.Instance.Options.DetailView.DirectoriesAlwasOnTop ? 0 : 1;
        FileAttributes attributes = fi.Attributes;
        item.Attributes = "";
        item.Attributes += (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly ? "r" : "-";
        item.Attributes += (attributes & FileAttributes.Archive) == FileAttributes.ReadOnly ? "a" : "-";
        item.Attributes += (attributes & FileAttributes.Hidden) == FileAttributes.ReadOnly ? "h" : "-";
        item.Attributes += (attributes & FileAttributes.System) == FileAttributes.ReadOnly ? "s" : "-";
        return true;
      }
      catch (FileNotFoundException)
      {
        if (item.RowState != DataRowState.Detached)
          this.Rows.Remove((DataRow) item);
        return false;
      }
      catch (Exception ex)
      {
        FormError.ShowException(ex, (IWin32Window) FsApp.Instance.MainForm);
        return false;
      }
    }

    public bool Update(DetailItem item, DirectoryInfo dir)
    {
      if (item == null)
        return false;
      try
      {
        item.Image = FsApp.Instance.GetFsiImage((FileSystemInfo) dir);
        item.Name = dir.Name;
        item.Extension = string.Empty;
        item.DirectoryInfo = dir;
        item.Size = (object) DBNull.Value;
        item.TypeName = FsApp.Instance.GetFsiTypeName((FileSystemInfo) dir);
        item.DateModified = dir.LastWriteTime;
        item.DateCreated = dir.CreationTime;
        item.SortOrder = 0;
        return true;
      }
      catch (DirectoryNotFoundException)
      {
        if (item.RowState != DataRowState.Detached)
          this.Rows.Remove((DataRow) item);
        return false;
      }
      catch (Exception ex)
      {
        FormError.ShowException(ex, (IWin32Window) FsApp.Instance.MainForm);
        return false;
      }
    }

    protected override DataRow NewRowFromBuilder(DataRowBuilder builder) => (DataRow) new DetailItem(builder);

    private DetailItem NewItem() => (DetailItem) this.NewRow();
  }
}
