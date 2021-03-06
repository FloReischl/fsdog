using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FsDog.Detail {
    static class GridColumnFactory {
        private static FsApp App { get => FsApp.Instance; }
        public static ReadOnlyCollection<string> SupportedColumNames = new ReadOnlyCollection<string>(new[] { "Image", "Name", "Size", "Extension", "TypeName", "DateModified", "DateCreated", "ParentPath" });

        public static DataGridViewColumn CreateGridColumn(string columnName) {
            DataGridViewColumn gridColumn = (DataGridViewColumn)null;
            if (columnName == "Name") {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Name";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (columnName == "DateModified" && App.Config.Options.DetailView.ShowModificationDateColumn) {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Modified";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (columnName == "DateCreated" && App.Config.Options.DetailView.ShowCreationDateColumn) {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Created";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (columnName == "Extension" && App.Config.Options.DetailView.ShowFileExtensionColumn) {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Ext";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (columnName == "Size") {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                viewTextBoxColumn.DefaultCellStyle.Format = "#,##0";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (columnName == "TypeName") {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Type";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (columnName == "Attributes" && App.Config.Options.DetailView.ShowAttributesColumn) {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Attrs";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (columnName == "Image") {
                DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
                gridViewImageColumn.Resizable = DataGridViewTriState.False;
                gridViewImageColumn.HeaderText = "";
                gridColumn = (DataGridViewColumn)gridViewImageColumn;
            }
            else if (columnName == "ParentPath") {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Parent Path";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else {
                throw new NotSupportedException($"Unsupported column name '{columnName}.");
            }

            gridColumn.Name = columnName;
            gridColumn.DataPropertyName = columnName;

            return gridColumn;
        }

        //public static DataGridViewColumn CreateGridColumn(string columnName) {
        //    DataGridViewColumn gridColumn = (DataGridViewColumn)null;
        //    if (columnName == "Name") {
        //        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        //        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //        viewTextBoxColumn.HeaderText = "Name";
        //        viewTextBoxColumn.Width = 300;
        //        gridColumn = (DataGridViewColumn)viewTextBoxColumn;
        //    }
        //    else if (columnName == "DateModified") {
        //        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        //        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //        viewTextBoxColumn.HeaderText = "Modified";
        //        viewTextBoxColumn.Width = 150;
        //        gridColumn = (DataGridViewColumn)viewTextBoxColumn;
        //    }
        //    else if (columnName == "DateCreated") {
        //        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        //        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //        viewTextBoxColumn.HeaderText = "Created";
        //        viewTextBoxColumn.Width = 150;
        //        gridColumn = (DataGridViewColumn)viewTextBoxColumn;
        //    }
        //    else if (columnName == "Extension") {
        //        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        //        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //        viewTextBoxColumn.HeaderText = "Ext";
        //        viewTextBoxColumn.Width = 150;
        //        gridColumn = (DataGridViewColumn)viewTextBoxColumn;
        //    }
        //    else if (columnName == "Size") {
        //        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        //        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //        viewTextBoxColumn.DefaultCellStyle.Format = "#,##0";
        //        viewTextBoxColumn.Width = 150;
        //        gridColumn = (DataGridViewColumn)viewTextBoxColumn;
        //    }
        //    else if (columnName == "TypeName") {
        //        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        //        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //        viewTextBoxColumn.HeaderText = "Type";
        //        viewTextBoxColumn.Width = 150;
        //        gridColumn = (DataGridViewColumn)viewTextBoxColumn;
        //    }
        //    else if (columnName == "Attributes") {
        //        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        //        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //        viewTextBoxColumn.HeaderText = "Attrs";
        //        viewTextBoxColumn.Width = 150;
        //        gridColumn = (DataGridViewColumn)viewTextBoxColumn;
        //    }
        //    else if (columnName == "Image") {
        //        DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
        //        gridViewImageColumn.Resizable = DataGridViewTriState.False;
        //        gridViewImageColumn.HeaderText = "";
        //        gridViewImageColumn.Width = 24;
        //        gridColumn = (DataGridViewColumn)gridViewImageColumn;
        //    }
        //    else if (columnName == "ParentPath") {
        //        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        //        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //        viewTextBoxColumn.HeaderText = "Parent Path";
        //        viewTextBoxColumn.Width = 150;
        //        gridColumn = (DataGridViewColumn)viewTextBoxColumn;
        //    }

        //    if (gridColumn != null) {
        //        gridColumn.Name = columnName;
        //        gridColumn.DataPropertyName = columnName;
        //    }
        //    return gridColumn;
        //}
    }
}
