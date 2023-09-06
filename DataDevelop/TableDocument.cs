using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop
{
	using Data;
	using Dialogs;
	using IO;
	using Properties;

	public partial class TableDocument : Document, IDbObject
	{
		private Table table;
		private DataTable data;
		////private DbDataAdapter adapter;
		private TableFilter filter;
		private TableSort sort;

		private int count = 0;
		private int currentPage = 0;
		private int rowsPerPage = 100;

		private bool refreshDataNeeded = false;

		public TableDocument(Table table) :
			this(table, null)
		{
		}

		public TableDocument(Table table, TableFilter filter)
		{
			InitializeComponent();

			this.tableToolStripMenuItem.DropDown = this.dataGridView.MainMenu;
			this.autoResizeColumnsDropDownButton.DropDown = this.dataGridView.AutoResizeColumnsMenu;

			this.rowsPerPage = Settings.Default.RowsPerPage;
			this.rowsPerPageButton.Text = this.rowsPerPage.ToString();

			this.table = table;

			////this.adapter = table.Database.CreateAdapter(table);
			this.filter = filter ?? new TableFilter(table);
			this.sort = new TableSort(table);

			////sort.SortPanel.LoadColumns(table.GetColumnNames());
			////this.sortToolStripButton.DropDown.Items.Add(sort);

			if (table.IsReadOnly) {
				SetReadOnlyTable();
			}

			SetupLoadingPanel();
			HideLoadingPanel();
			dataGridView.AutoGenerateColumns = true;
		}

		private void SetupLoadingPanel()
		{
			loadingPanel.Location = new Point(0, 0);
			loadingPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
			loadingPanel.Font = new Font(loadingPanel.Font, FontStyle.Bold);
		}

		private void ShowLoadingPanel()
		{
			this.progressBar1.Show();
			this.Controls.Add(loadingPanel);
			loadingPanel.Size = this.ClientSize;
			loadingPanel.BringToFront();
			loadingPanel.Focus();
		}

		private void HideLoadingPanel()
		{
			this.Controls.Remove(loadingPanel);
			this.progressBar1.Hide();
		}

		private void TableDocument_Load(object sender, EventArgs e)
		{
			try {
				this.UpdateDataSet();
			} catch (Exception ex) {
				MessageBox.Show(this, ex.Message, "Error opening table", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Enabled = false;
			}
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
		}

		private void TableDocument_Shown(object sender, EventArgs e)
		{
			dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
			foreach (DataGridViewColumn column in dataGridView.Columns) {
				if (column.Width > 400) {
					column.Width = 400;
				}
			}
		}

		private bool IsFiltered
		{
			get { return this.filter.IsRowFiltered || this.filter.IsColumnFiltered; }
		}

		private bool IsSorted
		{
			get { return this.sort.IsSorted; }
		}

		public void UpdateDataSet()
		{
			this.UpdateDataSet(false);
		}

		public void UpdateDataSet(bool conserveScroll)
		{
			this.ShowLoadingPanel();
			this.backgroundWorker.RunWorkerAsync();
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (this.filter.IsRowFiltered) {
				this.count = table.GetRowCount(this.filter);
			} else {
				this.count = table.GetRowCount();
			}
			if (currentPage * rowsPerPage >= count) {
				currentPage = 0;
			}
			this.data = table.GetData(currentPage * rowsPerPage, rowsPerPage, filter, IsSorted ? sort : null);
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.HideLoadingPanel();

			if (e.Error == null) {
				this.refreshDataNeeded = false;
				this.dataGridView.DataSource = data;
				this.UpdateLocation();
			} else {
				throw e.Error;
			}
		}

		private int FirstDisplayedRowIndex
		{
			get
			{
				foreach (DataGridViewRow row in dataGridView.Rows) {
					if (row.Displayed) {
						return row.Index;
					}
				}
				return 0;
			}
		}

		private void UpdateLocation()
		{
			int startRow = currentPage * rowsPerPage + 1;
			int lastRow = startRow + rowsPerPage - 1;
			if (lastRow > count) {
				lastRow = count;
			}

			filterToolStripButton.Checked = IsFiltered;
			filterToolStripButton.ToolTipText = IsFiltered ? filter.ToString() : "Filter";
			sortToolStripButton.Checked = IsSorted;
			sortToolStripButton.ToolTipText = IsSorted ? sort.ToString() : "Sort";

			if (IsFiltered) {
				locationLabel.Text = String.Format("{0:0000} to {1:0000} of {2:0000}*", startRow, lastRow, count);
			} else {
				locationLabel.Text = String.Format("{0:0000} to {1:0000} of {2:0000}", startRow, lastRow, count);
			}

			if (currentPage == 0) {
				firstButton.Enabled = false;
				prevButton.Enabled = false;
			} else {
				firstButton.Enabled = true;
				prevButton.Enabled = true;
			}

			if (lastRow == count) {
				nextButton.Enabled = false;
				lastButton.Enabled = false;
				dataGridView.AllowUserToAddRows = true;
			} else {
				nextButton.Enabled = true;
				lastButton.Enabled = true;
				dataGridView.AllowUserToAddRows = false;
			}

			dataGridView.StartRowNumber = startRow;
		}

		public void SetReadOnlyTable()
		{
			saveChangesButton.Enabled = false;
			discardChangesButton.Enabled = false;
			dataGridView.ReadOnly = true;
			dataGridView.AllowUserToAddRows = false;
		}

		private void saveChangesButton_Click(object sender, EventArgs e)
		{
			DataTable changes = this.GetChanges();
			if (changes != null) {
				if (SaveChanges(changes)) {
					UpdateDataSet();
				}
			}
		}

		private DataTable GetChanges()
		{
			this.Validate();
			this.dataGridView.EndEdit();
			// data can be null if an error occurred
			if (this.data == null) {
				return null;
			}
			return this.data.GetChanges();
		}

		/// <summary></summary>
		/// <returns>Returns True if the program can procced, otherwise False.</returns>
		private bool AskAndSave(DataTable changes)
		{
			this.Activate();
			DialogResult result = MessageBox.Show(this
				, Properties.Resources.UnsavedChanges
				, this.Text
				, MessageBoxButtons.YesNoCancel);
			if (result == DialogResult.Yes) {
				return SaveChanges(changes);
			} else if (result == DialogResult.No) {
				return true;
			}
			return false;
		}

		/// <summary></summary>
		/// <returns>Returns True if updating was successful, otherwise false.</returns>
		private bool SaveChanges(DataTable changes)
		{
			try {
				using (DbDataAdapter adapter = table.Database.CreateAdapter(table, filter)) {
					adapter.ContinueUpdateOnError = false;
					adapter.Update(changes);
					this.data.AcceptChanges();
					this.refreshDataNeeded = true;
					////UpdateDataSet(true);
				}
			} catch (Exception ex) {
				MessageBox.Show(this, ex.Message, ex.GetType().ToString());
				return false;
			}
			return true;
		}

		private void discardChangesButton_Click(object sender, EventArgs e)
		{
			this.Validate();
			this.data.RejectChanges();
		}

		private bool CanContinue()
		{
			DataTable changes = this.GetChanges();
			if (changes != null) {
				if (!this.AskAndSave(changes)) {
					return false;
				}
			}
			return true;
		}

		private void firstButton_Click(object sender, EventArgs e)
		{
			if (this.CanContinue()) {
				this.currentPage = 0;
				this.UpdateDataSet();
			}
		}

		private void prevButton_Click(object sender, EventArgs e)
		{
			if (this.CanContinue()) {
				this.currentPage--;
				this.UpdateDataSet();
			}
		}

		private void nextButton_Click(object sender, EventArgs e)
		{
			if (this.CanContinue()) {
				this.currentPage++;
				this.UpdateDataSet();
			}
		}

		private void lastButton_Click(object sender, EventArgs e)
		{
			if (this.CanContinue()) {
				this.currentPage = (count - 1) / rowsPerPage;
				this.UpdateDataSet();
			}
		}

		private void refreshButton_Click(object sender, EventArgs e)
		{
			if (this.CanContinue()) {
				this.UpdateDataSet(true);
			}
		}

		private void SelectNewRow()
		{
			this.dataGridView.ClearSelection();
			this.dataGridView.Rows[dataGridView.NewRowIndex].Selected = true;
			this.dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.NewRowIndex;
		}

		private void SelectNewRow(object sender, RunWorkerCompletedEventArgs e)
		{
			SelectNewRow();
			this.backgroundWorker.RunWorkerCompleted -= SelectNewRow;
		}

		private void newRowButton_Click(object sender, EventArgs e)
		{
			int lastPage = count / rowsPerPage;
			if (currentPage == lastPage) {
				SelectNewRow();
			} else {
				if (this.CanContinue()) {
					this.currentPage = lastPage;
					this.backgroundWorker.RunWorkerCompleted += SelectNewRow;
					this.UpdateDataSet();
				}
			}
		}

		#region Printing

		private void printPreviewButton_Click(object sender, EventArgs e)
		{
			dataTablePrintDocument.DataTable = data;

			Form parent = printPreviewDialog.PrintPreviewControl.Parent as Form;
			parent.WindowState = FormWindowState.Maximized;

			printPreviewDialog.ShowDialog();
		}

		private void fontButton_Click(object sender, EventArgs e)
		{
			fontDialog.Font = dataTablePrintDocument.Font;
			if (fontDialog.ShowDialog(this) == DialogResult.OK) {
				dataTablePrintDocument.Font = fontDialog.Font;
			}
		}

		private void pageSetupButton_Click(object sender, EventArgs e)
		{
			pageSetupDialog.Document = dataTablePrintDocument;
			pageSetupDialog.ShowDialog();
		}

		#endregion

		private void filterToolStripButton_Click(object sender, EventArgs e)
		{
			if (!CanContinue()) {
				return;
			}
			using (FilterDialog filterDialog = new FilterDialog(this.filter.Clone())) {
				FormExtensions.PositionDown(filterDialog, filterToolStripButton, this);
				if (filterDialog.ShowDialog(this) == DialogResult.OK) {
					TableFilter lastGood = this.filter;
					this.filter = filterDialog.Filter;

					if (IsFiltered) {
						this.currentPage = 0;
						this.filterToolStripButton.Checked = true;
					} else {
						this.filterToolStripButton.Checked = false;
					}
					this.filterToolStripButton.ToolTipText = IsFiltered ? filter.ToString() : "Filter";

					try {
						this.UpdateDataSet();
					} catch (Exception ex) {
						this.filter = lastGood;
						MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK);
						this.UpdateDataSet();
					}
				} else {
					if (this.refreshDataNeeded) {
						this.UpdateDataSet();
					}
				}
			}
		}

		private void sortToolStripButton_Click(object sender, EventArgs e)
		{
			if (!CanContinue()) {
				return;
			}
			using (SortDialog sortDialog = new SortDialog(this.sort)) {
				FormExtensions.PositionDown(sortDialog, sortToolStripButton, this);
				if (sortDialog.ShowDialog(this) == DialogResult.OK) {
					TableSort lastGood = this.sort;
					this.sort = sortDialog.Sort;
					this.sortToolStripButton.Checked = IsSorted;
					this.sortToolStripButton.ToolTipText = IsSorted ? sort.ToString() : "Sort";

					try {
						UpdateDataSet();
					} catch (Exception ex) {
						MessageBox.Show(this, ex.Message, this.Text, MessageBoxButtons.OK);
						this.sort = lastGood;
						UpdateDataSet();
					}
				} else {
					if (this.refreshDataNeeded) {
						this.UpdateDataSet();
					}
				}
			}
		}

		private void TableDocument_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing) {
				if (this.backgroundWorker.IsBusy) {
					MessageBox.Show(this, "Reading table data, please wait...", this.ProductName);
					e.Cancel = true;
					return;
				}
				DataTable changes = this.GetChanges();
				if (changes != null) {
					if (!this.AskAndSave(changes)) {
						e.Cancel = true;
					}
				}
			}
		}

		private void viewSqlToolStripButton_Click(object sender, EventArgs e)
		{
			StringBuilder sql = new StringBuilder();
			sql.Append("SELECT ");
			filter.WriteColumnsProjection(sql);
			sql.AppendLine();
			sql.Append("FROM ");
			sql.Append(table.QuotedName);
			if (filter.IsRowFiltered) {
				sql.AppendLine();
				sql.Append("WHERE ");
				filter.WriteWhereStatement(sql);
			}
			if (IsSorted) {
				sql.AppendLine();
				sql.Append("ORDER BY ");
				sort.WriteOrderBy(sql);
			}

			CommandDocument doc = new CommandDocument(table.Database);
			doc.Text = String.Format(Resources.QueryDocumentTitle, table.Database.Name);
			doc.CommandText = sql.ToString();
			doc.Show(this.DockPanel);
		}

		public Database Database
		{
			get { return table.Database; }
		}

		private void allCellsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
		}

		private void allCellsButHeadersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
		}

		private void columnsHeaderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
		}

		private void dataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
		{
			e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
		}

		private void rowsPerPageButton_Click(object sender, EventArgs e)
		{
			if (!CanContinue()) {
				return;
			}
			using (RowsPerPageDialog dialog = new RowsPerPageDialog()) {
				dialog.RowsPerPage = rowsPerPage;
				FormExtensions.PositionDown(dialog, rowsPerPageButton, this);
				if (dialog.ShowDialog(this) == DialogResult.OK) {
					if (rowsPerPage != dialog.RowsPerPage) {
						rowsPerPage = dialog.RowsPerPage;
						rowsPerPageButton.Text = rowsPerPage.ToString();
						this.UpdateDataSet();
					}
				} else {
					if (this.refreshDataNeeded) {
						this.UpdateDataSet();
					}
				}
			}
		}

		private void exportAllToExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProgressDialog.Run(this, "Export to Excel", excelWorker, true, true);
		}

		private void exportCurrentPageToExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProgressDialog.Run(this, "Export to Excel", excelWorker, true, false);
		}

		private void excelWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var allData = (bool)e.Argument;
			DataTable dataToExport;

			if (allData) {
				dataToExport = table.GetData(this.filter);
			} else {
				dataToExport = this.data;
			}

			if (dataToExport != null) {
				var sheet = ExcelInterop.CreateWorksheet("", dataToExport, excelWorker);
				if (sheet != null) {
					excelWorker.ReportProgress(100, "Loading Excel...");
					sheet.OpenInExcel();
				} else {
					e.Cancel = true;
				}
			}
		}

		private void PasteButton_Click(object sender, EventArgs e)
		{
			if (dataGridView.NewRowIndex >= 0) {
				if (Clipboard.ContainsText()) {
					var text = Clipboard.GetText();
					var dataTable = dataGridView.DataSource as DataTable;
					using (var reader = new CsvReader(new System.IO.StringReader(text))) {
						reader.Delimiter = '\t';
						string[] row;
						while ((row = reader.ReadRow()) != null) {
							var newRow = dataTable.NewRow();
							for (var i = 0; i < row.Length; i++) {
								if (!string.IsNullOrEmpty(row[i]))
									newRow[i] = row[i];
							}
							dataTable.Rows.Add(newRow);
						}
					}
				}
			}
		}
	}
}
