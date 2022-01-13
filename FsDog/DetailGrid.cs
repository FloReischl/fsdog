// Decompiled with JetBrains decompiler
// Type: FsDog.DetailGrid
// Assembly: FsDog, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86A1142D-AA42-437E-9D7A-2AF6376C2EE2
// Assembly location: C:\Users\flori\OneDrive\utilities\FR Solutions\FsDog\FsDog.exe

using System;
using System.Windows.Forms;

namespace FsDog
{
  public class DetailGrid : DataGridView
  {
    public event MessageHandler HandleMessage;

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.RowCount == 0)
      {
        base.OnKeyDown(e);
      }
      else
      {
        e.Handled = true;
        e.SuppressKeyPress = true;
        if (e.KeyCode == Keys.Down && e.Shift && e.Control)
          this.OnKeyDown(new KeyEventArgs(Keys.Down | Keys.Shift));
        else if (e.KeyCode == Keys.Down && e.Shift)
        {
          DataGridViewRow currentRow = this.CurrentRow;
          if (currentRow.Index == this.RowCount - 1)
            return;
          if (this.Rows[currentRow.Index + 1].Selected)
          {
            currentRow.Selected = false;
            this.SetCurrentCellAddressCore(0, currentRow.Index + 1, true, true, true);
            this.ScrollToRow(currentRow.Index + 1, false);
          }
          else
          {
            this.Rows[currentRow.Index + 1].Selected = true;
            this.SetCurrentCellAddressCore(0, currentRow.Index + 1, true, true, true);
            this.ScrollToRow(currentRow.Index + 1, false);
          }
        }
        else if (e.KeyCode == Keys.Down && e.Control)
        {
          if (this.CurrentCell.RowIndex >= this.RowCount - 1)
            return;
          int rowIndex = this.CurrentCell.RowIndex + 1;
          this.SetCurrentCellAddressCore(0, rowIndex, true, true, true);
          this.ScrollToRow(rowIndex, false);
        }
        else if (e.KeyCode == Keys.Down)
        {
          DataGridViewRow lastSelectedRow = this.GetLastSelectedRow(this.CurrentRow);
          bool flag = this.SelectedRows.Count != 1;
          this.ClearSelection();
          int rowIndex = flag ? lastSelectedRow.Index : Math.Min(this.RowCount - 1, lastSelectedRow.Index + 1);
          this.SetCurrentCellAddressCore(0, rowIndex, true, true, false);
          this.SetSelectedRowCore(rowIndex, true);
          this.ScrollToRow(rowIndex, false);
        }
        else if (e.KeyCode == Keys.End && e.Shift && e.Control)
        {
          for (int index = this.CurrentRow.Index; index < this.RowCount; ++index)
            this.Rows[index].Selected = true;
          this.SetCurrentCellAddressCore(0, this.RowCount - 1, false, true, true);
          this.FirstDisplayedCell = this.CurrentCell;
        }
        else if (e.KeyCode == Keys.End && e.Shift)
        {
          if (this.CurrentRow.Index == this.RowCount - 1)
            return;
          DataGridViewRow lastSelectedRow = this.GetLastSelectedRow(this.Rows[0]);
          this.ClearSelection();
          for (int index = lastSelectedRow.Index; index < this.RowCount; ++index)
            this.Rows[index].Selected = true;
          this.SetCurrentCellAddressCore(0, this.RowCount - 1, false, true, true);
          this.FirstDisplayedCell = this.CurrentCell;
        }
        else if (e.KeyCode == Keys.End && e.Control)
        {
          this.SetCurrentCellAddressCore(0, this.RowCount - 1, true, true, true);
          this.FirstDisplayedCell = this.CurrentCell;
        }
        else if (e.KeyCode == Keys.Home && e.Shift && e.Control)
        {
          for (int index = 0; index <= this.CurrentRow.Index; ++index)
            this.Rows[index].Selected = true;
          this.SetCurrentCellAddressCore(0, 0, true, false, false);
          this.FirstDisplayedCell = this.Rows[0].Cells[0];
        }
        else if (e.KeyCode == Keys.Home && e.Shift)
        {
          if (this.CurrentRow.Index == 0)
            return;
          DataGridViewRow firstSelectedRow = this.GetFirstSelectedRow(this.Rows[this.RowCount - 1]);
          this.ClearSelection();
          for (int index = 0; index <= firstSelectedRow.Index; ++index)
            this.Rows[index].Selected = true;
          this.SetCurrentCellAddressCore(0, 0, true, false, false);
          this.FirstDisplayedCell = this.Rows[0].Cells[0];
        }
        else if (e.KeyCode == Keys.Home && e.Control)
        {
          this.SetCurrentCellAddressCore(0, 0, true, true, false);
          this.FirstDisplayedCell = this.CurrentCell;
        }
        else if (e.KeyCode == Keys.Next && e.Control && e.Shift)
          this.OnKeyDown(new KeyEventArgs(Keys.Next | Keys.Shift));
        else if (e.KeyCode == Keys.Next && e.Shift)
        {
          int index1 = this.CurrentRow.Index;
          int index2 = this.GetLastSelectedRow(this.CurrentRow).Index;
          int rowIndex1 = this.FirstDisplayedCell.RowIndex;
          int num = this.DisplayedRowCount(false);
          int rowIndex2 = this.CurrentCell.RowIndex == rowIndex1 + num - 1 ? Math.Min(this.RowCount - 1, rowIndex1 + num * 2 - 1) : rowIndex1 + num - 1;
          for (int index3 = index1; index3 <= rowIndex2; ++index3)
          {
            if (index3 < index2)
              this.Rows[index3].Selected = false;
            else
              this.Rows[index3].Selected = true;
          }
          this.SetCurrentCellAddressCore(0, rowIndex2, true, true, false);
          this.ScrollToRow(rowIndex2, false);
        }
        else if (e.KeyCode == Keys.Next && e.Control)
        {
          int rowIndex3 = this.FirstDisplayedCell.RowIndex;
          int num = this.DisplayedRowCount(false);
          int rowIndex4 = this.CurrentCell.RowIndex == rowIndex3 + num - 1 ? Math.Min(this.RowCount - 1, rowIndex3 + num * 2 - 1) : rowIndex3 + num - 1;
          this.SetCurrentCellAddressCore(0, rowIndex4, true, true, false);
          this.ScrollToRow(rowIndex4, false);
        }
        else if (e.KeyCode == Keys.Next)
        {
          int rowIndex = this.FirstDisplayedCell.RowIndex;
          int num = this.DisplayedRowCount(false);
          this.CurrentCell = this.Rows[this.CurrentCell.RowIndex == rowIndex + num - 1 ? Math.Min(this.RowCount - 1, rowIndex + num * 2 - 1) : rowIndex + num - 1].Cells[0];
        }
        else if (e.KeyCode == Keys.Prior && e.Control && e.Shift)
          this.OnKeyDown(new KeyEventArgs(Keys.Prior | Keys.Shift));
        else if (e.KeyCode == Keys.Prior && e.Shift)
        {
          int index4 = this.CurrentRow.Index;
          int index5 = this.GetFirstSelectedRow(this.CurrentRow).Index;
          int rowIndex5 = this.FirstDisplayedCell.RowIndex;
          int num = this.DisplayedRowCount(false);
          int rowIndex6 = this.CurrentCell.RowIndex == rowIndex5 ? Math.Max(0, rowIndex5 - num) : rowIndex5;
          for (int index6 = index4; index6 >= rowIndex6; --index6)
          {
            if (index6 > index5)
              this.Rows[index6].Selected = false;
            else
              this.Rows[index6].Selected = true;
          }
          this.SetCurrentCellAddressCore(0, rowIndex6, true, true, false);
          this.ScrollToRow(rowIndex6, false);
        }
        else if (e.KeyCode == Keys.Prior && e.Control)
        {
          int rowIndex7 = this.FirstDisplayedCell.RowIndex;
          int num = this.DisplayedRowCount(false);
          int rowIndex8 = this.CurrentCell.RowIndex == rowIndex7 ? Math.Max(0, rowIndex7 - num) : rowIndex7;
          this.SetCurrentCellAddressCore(0, rowIndex8, true, true, false);
          this.ScrollToRow(rowIndex8, false);
        }
        else if (e.KeyCode == Keys.Prior)
        {
          int rowIndex = this.FirstDisplayedCell.RowIndex;
          int num = this.DisplayedRowCount(false);
          this.CurrentCell = this.Rows[this.CurrentCell.RowIndex == rowIndex ? Math.Max(0, rowIndex - num) : rowIndex].Cells[0];
        }
        else if (e.KeyCode == Keys.Space && e.Control)
          this.CurrentRow.Selected = !this.CurrentRow.Selected;
        else if (e.KeyCode == Keys.Up && e.Shift && e.Control)
          this.OnKeyDown(new KeyEventArgs(Keys.Up | Keys.Shift));
        else if (e.KeyCode == Keys.Up && e.Shift)
        {
          DataGridViewRow currentRow = this.CurrentRow;
          if (currentRow.Index == 0)
            return;
          if (this.Rows[currentRow.Index - 1].Selected)
          {
            currentRow.Selected = false;
            this.SetCurrentCellAddressCore(0, currentRow.Index - 1, true, true, false);
            this.ScrollToRow(currentRow.Index - 1, false);
          }
          else
          {
            this.Rows[currentRow.Index - 1].Selected = true;
            this.SetCurrentCellAddressCore(0, currentRow.Index - 1, true, true, false);
            this.ScrollToRow(currentRow.Index - 1, false);
          }
        }
        else if (e.KeyCode == Keys.Up && e.Control)
        {
          if (this.CurrentCell.RowIndex <= 0)
            return;
          int rowIndex = this.CurrentCell.RowIndex - 1;
          this.SetCurrentCellAddressCore(0, rowIndex, true, true, false);
          this.ScrollToRow(rowIndex, false);
        }
        else if (e.KeyCode == Keys.Up)
        {
          DataGridViewRow firstSelectedRow = this.GetFirstSelectedRow(this.CurrentRow);
          bool flag = this.SelectedRows.Count != 1;
          this.ClearSelection();
          int rowIndex = flag || firstSelectedRow.Index <= 0 ? firstSelectedRow.Index : Math.Max(0, firstSelectedRow.Index - 1);
          this.SetCurrentCellAddressCore(0, rowIndex, true, true, false);
          this.SetSelectedRowCore(rowIndex, true);
          this.ScrollToRow(rowIndex, false);
          this.CurrentRow.Selected = true;
        }
        else
        {
          e.SuppressKeyPress = false;
          e.Handled = false;
          base.OnKeyDown(e);
        }
      }
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 769)
        this.OnHandleMessage(ref m);
      else if (m.Msg == 768)
        this.OnHandleMessage(ref m);
      else if (m.Msg == 770)
        this.OnHandleMessage(ref m);
      else
        base.WndProc(ref m);
    }

    protected void OnHandleMessage(ref Message m)
    {
      if (this.HandleMessage == null)
        return;
      this.HandleMessage((object) this, ref m);
    }

    private DataGridViewRow GetLastSelectedRow(DataGridViewRow defaultRow)
    {
      int num = -1;
      for (int index = 0; index < this.SelectedRows.Count; ++index)
        num = Math.Max(num, this.SelectedRows[index].Index);
      return num != -1 ? this.Rows[num] : defaultRow;
    }

    private DataGridViewRow GetFirstSelectedRow(DataGridViewRow defaultRow)
    {
      int num = int.MaxValue;
      for (int index = 0; index < this.SelectedRows.Count; ++index)
        num = Math.Min(num, this.SelectedRows[index].Index);
      return num != int.MaxValue ? this.Rows[num] : defaultRow;
    }

    private void ScrollToRow(int rowIndex, bool includePartialRow)
    {
      int num = this.DisplayedRowCount(includePartialRow);
      int rowIndex1 = this.FirstDisplayedCell.RowIndex;
      if (rowIndex < rowIndex1)
      {
        this.FirstDisplayedCell = this.Rows[rowIndex].Cells[0];
      }
      else
      {
        if (rowIndex == rowIndex1 || rowIndex <= rowIndex1 + num - 1)
          return;
        this.FirstDisplayedCell = this.Rows[rowIndex - (num - 1)].Cells[0];
      }
    }
  }
}
