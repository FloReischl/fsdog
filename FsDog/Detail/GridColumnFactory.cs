using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FsDog.Detail {
    static class GridColumnFactory {
        private static FsApp App { get => FsApp.Instance; }

        public static DataGridViewColumn CreateGridColumn(DataColumn column) {
            DataGridViewColumn gridColumn = (DataGridViewColumn)null;
            if (column.ColumnName == "Name") {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Name";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (column.ColumnName == "DateModified" && App.Options.DetailView.ShowModificationDateColumn) {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Modified";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (column.ColumnName == "DateCreated" && App.Options.DetailView.ShowCreationDateColumn) {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Created";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (column.ColumnName == "Extension" && App.Options.DetailView.ShowFileExtensionColumn) {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Ext";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (column.ColumnName == "Size") {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                viewTextBoxColumn.DefaultCellStyle.Format = "#,##0";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (column.ColumnName == "TypeName") {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Type";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (column.ColumnName == "Attributes" && App.Options.DetailView.ShowAttributesColumn) {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderText = "Attrs";
                gridColumn = (DataGridViewColumn)viewTextBoxColumn;
            }
            else if (column.ColumnName == "Image") {
                DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
                gridViewImageColumn.Resizable = DataGridViewTriState.False;
                gridViewImageColumn.HeaderText = "";
                gridColumn = (DataGridViewColumn)gridViewImageColumn;
            }
            if (gridColumn != null) {
                gridColumn.Name = column.ColumnName;
                gridColumn.DataPropertyName = column.ColumnName;
            }
            return gridColumn;
        }
    }
}
