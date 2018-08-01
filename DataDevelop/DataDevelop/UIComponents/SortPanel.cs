using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
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

		public void LoadSort(TableSort sort)
		{
			this.sort = sort;
			columnOrderBindingSource.DataSource = sort.GetColumnOrders();
			dataGridView.Refresh();
			UpdateButtonsState();
		}

		public TableSort Sort
		{
			get { return sort; }
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
