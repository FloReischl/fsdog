// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.CDataGridViewComboBoxCell
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace FR.Windows.Forms {
    public class CDataGridViewComboBoxCell : DataGridViewTextBoxCell {
        private bool _fillList;
        private int _comboButtonWidth;

        public CDataGridViewComboBoxCell() => this._fillList = true;

        public new CDataGridViewComboBoxColumn OwningColumn => (CDataGridViewComboBoxColumn)base.OwningColumn;

        public override System.Type EditType => typeof(CDataGridViewComboBoxEditControl);

        public override System.Type FormattedValueType => this.OwningColumn == null ? typeof(object) : this.OwningColumn.ValueType;

        public override void InitializeEditingControl(
          int rowIndex,
          object initialFormattedValue,
          DataGridViewCellStyle dataGridViewCellStyle) {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            CDataGridViewComboBoxEditControl editingControl = (CDataGridViewComboBoxEditControl)this.DataGridView.EditingControl;
            editingControl.Initializing = true;
            editingControl.AllowOnlyListEntries = this.OwningColumn.AllowOnlyListEntries;
            editingControl.DropDownStyle = this.OwningColumn.DropDownStyle;
            if (!object.Equals((object)editingControl.AutoCompleteCustomSource, (object)this.OwningColumn.AutoCompleteCustomSource) && this.OwningColumn.AutoCompleteCustomSource != null)
                editingControl.AutoCompleteCustomSource = this.OwningColumn.AutoCompleteCustomSource;
            editingControl.AutoCompleteMode = this.OwningColumn.AutoCompleteMode;
            editingControl.AutoCompleteSource = this.OwningColumn.AutoCompleteSource;
            int index = this.OwningColumn.Index;
            DataGridViewColumnInitializeEditingControlArgs e = new DataGridViewColumnInitializeEditingControlArgs(rowIndex, index, initialFormattedValue, dataGridViewCellStyle, (System.Windows.Forms.ComboBox)editingControl);
            this.OwningColumn.OnInitializeEditingControl(e);
            if (!e.ListFilled && this._fillList) {
                this._fillList = false;
                editingControl.BeginUpdate();
                editingControl.Items.Clear();
                IEnumerable objA = (IEnumerable)null;
                if (this.OwningColumn.DataSource is IListSource)
                    objA = (IEnumerable)((IListSource)this.OwningColumn.DataSource).GetList();
                else if (this.OwningColumn.DataSource is IList)
                    objA = (IEnumerable)this.OwningColumn.DataSource;
                else if (this.OwningColumn.DataSource is IEnumerable)
                    objA = (IEnumerable)this.OwningColumn.DataSource;
                if (!object.ReferenceEquals((object)objA, (object)null)) {
                    foreach (object instance in objA) {
                        ICustomTypeDescriptor customTypeDescriptor = !(instance is ICustomTypeDescriptor) ? TypeDescriptor.GetProvider(instance).GetTypeDescriptor(instance) : (ICustomTypeDescriptor)instance;
                        PropertyDescriptorCollection properties = customTypeDescriptor.GetProperties();
                        ComboBoxItem comboBoxItem = new ComboBoxItem();
                        PropertyDescriptor pd1 = properties.Find(this.OwningColumn.DisplayMember, false);
                        if (pd1 != null) {
                            object propertyOwner = customTypeDescriptor.GetPropertyOwner(pd1);
                            object obj = pd1.GetValue(propertyOwner);
                            comboBoxItem.Text = obj == null ? "" : obj.ToString();
                        }
                        PropertyDescriptor pd2 = properties.Find(this.OwningColumn.ValueMember, false);
                        if (pd2 != null) {
                            object propertyOwner = customTypeDescriptor.GetPropertyOwner(pd2);
                            comboBoxItem.Value = pd2.GetValue(propertyOwner);
                        }
                        editingControl.Items.Add((object)comboBoxItem);
                    }
                }
                editingControl.EditingControlFormattedValue = this.GetValue(rowIndex);
                editingControl.EndUpdate();
            }
            else
                this._fillList = true;
            editingControl.Initializing = false;
        }

        protected override Size GetPreferredSize(
          Graphics graphics,
          DataGridViewCellStyle cellStyle,
          int rowIndex,
          Size constraintSize) {
            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            object formattedValue = this.GetFormattedValue(this.GetValue(rowIndex), rowIndex, ref cellStyle, (TypeConverter)null, (TypeConverter)null, DataGridViewDataErrorContexts.PreferredSize);
            if (formattedValue == null)
                return preferredSize;
            SizeF sizeF = graphics.MeasureString(formattedValue.ToString(), cellStyle.Font);
            return new Size((int)sizeF.Width + this._comboButtonWidth, (int)sizeF.Height);
        }

        protected override object GetFormattedValue(
          object value,
          int rowIndex,
          ref DataGridViewCellStyle cellStyle,
          TypeConverter valueTypeConverter,
          TypeConverter formattedValueTypeConverter,
          DataGridViewDataErrorContexts context) {
            return this.OwningColumn.getFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        protected override void Paint(
          Graphics graphics,
          Rectangle clipBounds,
          Rectangle cellBounds,
          int rowIndex,
          DataGridViewElementStates cellState,
          object value,
          object formattedValue,
          string errorText,
          DataGridViewCellStyle cellStyle,
          DataGridViewAdvancedBorderStyle advancedBorderStyle,
          DataGridViewPaintParts paintParts) {
            try {
                if (rowIndex < 0)
                    return;
                Rectangle rectangle = new Rectangle(new Point(cellBounds.Location.X - 1, cellBounds.Location.Y - 1), cellBounds.Size);
                Color color1;
                Color color2;
                if (this.ReadOnly) {
                    if (this.Selected) {
                        color1 = cellStyle.SelectionBackColor;
                        color2 = cellStyle.SelectionForeColor;
                    }
                    else {
                        color1 = SystemColors.Control;
                        color2 = cellStyle.ForeColor;
                    }
                }
                else if (this.Selected) {
                    color1 = cellStyle.SelectionBackColor;
                    color2 = cellStyle.SelectionForeColor;
                }
                else {
                    color1 = cellStyle.BackColor;
                    color2 = cellStyle.ForeColor;
                }
                graphics.FillRectangle((Brush)new SolidBrush(color1), rectangle);
                if (this._comboButtonWidth == 0)
                    this._comboButtonWidth = new System.Windows.Forms.ComboBox().Height;
                string s = formattedValue == null ? "" : formattedValue.ToString();
                Rectangle bounds = new Rectangle(rectangle.Right - this._comboButtonWidth, rectangle.Y, this._comboButtonWidth, rectangle.Height);
                graphics.DrawString(s, cellStyle.Font, (Brush)new SolidBrush(color2), (RectangleF)rectangle, new StringFormat(StringFormatFlags.LineLimit) {
                    LineAlignment = StringAlignment.Center
                });
                graphics.DrawRectangle(new Pen(this.DataGridView.GridColor), rectangle);
                ComboBoxRenderer.DrawDropDownButton(graphics, bounds, ComboBoxState.Normal);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
