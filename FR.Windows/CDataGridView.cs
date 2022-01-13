// Decompiled with JetBrains decompiler
// Type: FR.Windows.Forms.CDataGridView
// Assembly: FR.Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d2bed8779bb74884
// MVID: F25FFB78-EF25-4145-9FAC-9691AC19A809
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FR.Windows.dll

using System.Drawing;
using System.Windows.Forms;

namespace FR.Windows.Forms
{
  public class CDataGridView : DataGridView
  {
    private bool _showRowNumber;

    public bool ShowRowNumber
    {
      get => this._showRowNumber;
      set => this._showRowNumber = value;
    }

    public DataGridViewCell GetCell(int columnIndex, int rowIndex) => this.Rows[rowIndex].Cells[columnIndex];

    protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
    {
      bool flag = false;
      if (e.ColumnIndex < 0)
        flag = true;
      if (flag && e.RowIndex >= 0 && this.ShowRowNumber)
      {
        Graphics graphics = e.Graphics;
        bool selected = this.Rows[e.RowIndex].Selected;
        e.PaintBackground(e.ClipBounds, selected);
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        if (selected)
          graphics.DrawString(string.Format("{0}", (object) (e.RowIndex + 1)), e.CellStyle.Font, (Brush) new SolidBrush(e.CellStyle.SelectionForeColor), (RectangleF) e.CellBounds, format);
        else
          graphics.DrawString(string.Format("{0}", (object) (e.RowIndex + 1)), e.CellStyle.Font, (Brush) new SolidBrush(e.CellStyle.ForeColor), (RectangleF) e.CellBounds, format);
        e.Handled = true;
      }
      base.OnCellPainting(e);
    }
  }
}
