using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataDevelop.Data;

namespace DataDevelop
{
	internal partial class FilterDialog : Form
	{
		private TableFilter filter;

		public FilterDialog(TableFilter filter)
		{
			InitializeComponent();
			this.filter = filter;
			columnFilterBindingSource.DataSource = this.filter.ColumnFilters;

			var settingsSize = Properties.Settings.Default.FilterDialogSize;
			if (settingsSize.Width >= this.MinimumSize.Width && settingsSize.Height >= settingsSize.Height) {
				this.Size = settingsSize;
			}
		}

		public TableFilter Filter
		{
			get { return filter; }
		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			foreach (ColumnFilter columnFilter in this.filter.ColumnFilters) {
				columnFilter.Clear();
			}
			dataGridView.Refresh();
		}

		private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			//if (dataGridView.Columns[e.ColumnIndex].Frozen) {
			if (columnName.Index == e.ColumnIndex && e.RowIndex >= 0) {
				if ((e.PaintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border) {
					e.Paint(e.ClipBounds, e.PaintParts);
					Point pt1 = new Point(e.CellBounds.Right - 1, e.CellBounds.Top);
					Point pt2 = new Point(pt1.X, e.CellBounds.Bottom - 1);
					e.Graphics.DrawLine(SystemPens.ControlText, pt1, pt2);
					e.Handled = true;
				}
			}
		}

		private void FilterDialog_Load(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in dataGridView.Rows) {
				if (!row.IsNewRow) {
					row.Cells[Output.Index].ReadOnly = ((ColumnFilter)row.DataBoundItem).InPrimaryKey;
				}
			}
		}

		private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
		}

		private void selectAllCheckBox_Click(object sender, EventArgs e)
		{
			bool value = (selectAllCheckBox.CheckState != CheckState.Checked);
			foreach (ColumnFilter f in filter.ColumnFilters) {
				if (!f.InPrimaryKey) {
					f.Output = value;
				}
			}
			selectAllCheckBox.CheckState = value ? CheckState.Checked : CheckState.Indeterminate;
			dataGridView.Refresh();
		}

		private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			try {
				if (e.ColumnIndex == Output.Index) {
					selectAllCheckBox.CheckState = filter.IsColumnFiltered ? CheckState.Indeterminate : CheckState.Checked;
				}
			} catch {
			}
		}

		private void FilterDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
			Properties.Settings.Default.FilterDialogSize = this.Size;
		}
	}
}