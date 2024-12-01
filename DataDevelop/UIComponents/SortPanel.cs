using System;
using System.Windows.Forms;
using DataDevelop.Data;

namespace DataDevelop.UIComponents
{
	public partial class SortPanel : UserControl
	{
		private TableSort sort;
		
		public SortPanel()
		{
			InitializeComponent();
			orderTypeComboBoxColumn.Items.Add(OrderType.None);
			orderTypeComboBoxColumn.Items.Add(OrderType.Ascending);
			orderTypeComboBoxColumn.Items.Add(OrderType.Descending);
		}

		public TableSort Sort => sort;

		public void LoadSort(TableSort sort)
		{
			this.sort = sort;
			columnOrderBindingSource.DataSource = sort.GetColumnOrders();
			dataGridView.Refresh();
			UpdateButtonsState();
		}

		public void SetDarkMode()
		{
			dataGridView.BackgroundColor = VisualStyles.DarkThemeColors.Background;
			dataGridView.GridColor = VisualStyles.DarkThemeColors.BorderLight;
			dataGridView.BorderStyle = BorderStyle.FixedSingle;
			dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

			dataGridView.DefaultCellStyle = new DataGridViewCellStyle(dataGridView.DefaultCellStyle) {
				BackColor = VisualStyles.DarkThemeColors.Background,
				ForeColor = VisualStyles.DarkThemeColors.TextColor,
			};

			dataGridView.EnableHeadersVisualStyles = false;
			dataGridView.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle(dataGridView.ColumnHeadersDefaultCellStyle) {
				BackColor = VisualStyles.DarkThemeColors.CellBackColor,
				ForeColor = VisualStyles.DarkThemeColors.TextColor,
			};
			dataGridView.RowHeadersDefaultCellStyle = dataGridView.ColumnHeadersDefaultCellStyle;

			BackColor = VisualStyles.DarkThemeColors.Background;
			ForeColor = VisualStyles.DarkThemeColors.TextColor;

			upButton.FlatStyle = FlatStyle.Flat;
			upButton.BackColor = VisualStyles.DarkThemeColors.Control;
			downButton.FlatStyle = FlatStyle.Flat;
			downButton.BackColor = VisualStyles.DarkThemeColors.Control;
			resetButton.FlatStyle = FlatStyle.Flat;
			resetButton.BackColor = VisualStyles.DarkThemeColors.Control;
		}

		private void UpdateButtonsState()
		{
			if (dataGridView.SelectedRows.Count == 1) {
				upButton.Enabled = dataGridView.SelectedRows[0].Index != 0;
				downButton.Enabled = dataGridView.SelectedRows[0].Index != dataGridView.Rows.Count - 1;
			}
		}

		private void SortPanel_Load(object sender, EventArgs e)
		{
			UpdateButtonsState();
			dataGridView.AutoResizeColumn(ColumnName.Index);
		}

		private void dataGridView_SelectionChanged(object sender, EventArgs e)
		{
			UpdateButtonsState();
		}

		private void Swap(int index1, int index2)
		{
			sort.Swap(index1, index2);

			dataGridView.DataSource = sort.GetColumnOrders();
			dataGridView.Rows[index2].Selected = true;
			dataGridView.Refresh();
		}

		private void upButton_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1) {
				int index = dataGridView.SelectedRows[0].Index;
				Swap(index, index - 1);
			}
		}

		private void downButton_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1) {
				int index = dataGridView.SelectedRows[0].Index;
				Swap(index, index + 1);
			}
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			sort.Reset();
			dataGridView.Refresh();
		}

		private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			UpdateButtonsState();
		}

		private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			UpdateButtonsState();
		}
	}
}
