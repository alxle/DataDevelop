using System;
using System.Drawing;
using System.Windows.Forms;

namespace DataDevelop
{
	using Data;
	using Properties;

	internal partial class FilterDialog : Form
	{
		private readonly TableFilter filter;

		public FilterDialog(TableFilter filter, bool showControlBox = false)
		{
			InitializeComponent();
			this.filter = filter;
			columnFilterBindingSource.DataSource = filter.ColumnFilters;
			if (showControlBox) {
				ControlBox = true;
				ShowIcon = false;
			}

			if (MainForm.DarkMode) {
				SetDarkMode();
			}
			FormExtensions.TrySetSize(this, Settings.Default.FilterDialogSize);
		}

		public TableFilter Filter => filter;

		public void SetDarkMode()
		{
			this.UseImmersiveDarkMode();
			dataGridView.BackgroundColor = VisualStyles.DarkThemeColors.Background;
			dataGridView.GridColor = VisualStyles.DarkThemeColors.BorderLight;
			dataGridView.BorderStyle = BorderStyle.FixedSingle;
			dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

			dataGridView.DefaultCellStyle = new DataGridViewCellStyle(dataGridView.DefaultCellStyle) {
				Font = dataGridView.DefaultCellStyle.Font,
				BackColor = VisualStyles.DarkThemeColors.Background,
				ForeColor = VisualStyles.DarkThemeColors.TextColor,
			};

			dataGridView.EnableHeadersVisualStyles = false;
			dataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle(dataGridView.ColumnHeadersDefaultCellStyle) {
				BackColor = VisualStyles.DarkThemeColors.CellBackColor,
				ForeColor = VisualStyles.DarkThemeColors.TextColor,
			};
			dataGridView.RowHeadersDefaultCellStyle = dataGridView.ColumnHeadersDefaultCellStyle;

			columnName.DefaultCellStyle = dataGridView.ColumnHeadersDefaultCellStyle;
			outputColumn.DefaultCellStyle = dataGridView.ColumnHeadersDefaultCellStyle;

			BackColor = VisualStyles.DarkThemeColors.Background;
			ForeColor = VisualStyles.DarkThemeColors.TextColor;
			
			clearButton.FlatStyle = FlatStyle.Flat;
			clearButton.BackColor = VisualStyles.DarkThemeColors.Control;
			okButton.FlatStyle = FlatStyle.Flat;
			okButton.BackColor = VisualStyles.DarkThemeColors.Control;
			cancelButton.FlatStyle = FlatStyle.Flat;
			cancelButton.BackColor = VisualStyles.DarkThemeColors.Control;
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			foreach (var columnFilter in filter.ColumnFilters) {
				columnFilter.Clear();
			}
			dataGridView.Refresh();
		}

		private void DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (columnName.Index == e.ColumnIndex /*&& e.RowIndex >= 0*/) {
				if ((e.PaintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border) {
					e.Paint(e.ClipBounds, e.PaintParts);
					var pt1 = new Point(e.CellBounds.Right - 1, e.CellBounds.Top);
					var pt2 = new Point(pt1.X, e.CellBounds.Bottom - 1);
					e.Graphics.DrawLine(new Pen(dataGridView.DefaultCellStyle.ForeColor, 1), pt1, pt2);
					e.Handled = true;
				}
			}
		}

		private void FilterDialog_Load(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in dataGridView.Rows) {
				if (!row.IsNewRow) {
					row.Cells[outputColumn.Index].ReadOnly = ((ColumnFilter)row.DataBoundItem).InPrimaryKey;
				}
			}

			dataGridView.AutoResizeColumn(columnName.Index);
		}

		private void SelectAllCheckBox_Click(object sender, EventArgs e)
		{
			var value = (selectAllCheckBox.CheckState != CheckState.Checked);
			foreach (var f in filter.ColumnFilters) {
				if (!f.InPrimaryKey) {
					f.Output = value;
				}
			}
			selectAllCheckBox.CheckState = value ? CheckState.Checked : CheckState.Indeterminate;
			dataGridView.Refresh();
		}

		private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == outputColumn.Index) {
				selectAllCheckBox.CheckState = filter.IsColumnFiltered ? CheckState.Indeterminate : CheckState.Checked;
			}
		}

		private void FilterDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
			Settings.Default.FilterDialogSize = Size;
		}
	}
}
