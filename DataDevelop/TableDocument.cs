using System;
using System.ComponentModel;
using System.Data;
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
		private readonly Table table;
		private DataTable data;
		////private DbDataAdapter adapter;
		private TableFilter filter;
		private TableSort sort;

		private int count = 0;
		private int currentPage = 0;
		private int rowsPerPage = 100;

		private bool refreshDataNeeded = false;

		public TableDocument(Table table, TableFilter filter = null)
		{
			InitializeComponent();

			tableToolStripMenuItem.DropDown = dataGridView.MainMenu;
			autoResizeColumnsDropDownButton.DropDown = dataGridView.AutoResizeColumnsMenu;

			rowsPerPage = Settings.Default.RowsPerPage;
			rowsPerPageButton.Text = rowsPerPage.ToString();

			this.table = table;

			////this.adapter = table.Database.CreateAdapter(table);
			this.filter = filter ?? new TableFilter(table);
			sort = new TableSort(table);

			////sort.SortPanel.LoadColumns(table.GetColumnNames());
			////this.sortToolStripButton.DropDown.Items.Add(sort);

			if (table.IsReadOnly) {
				SetReadOnlyTable();
			}

			SetupLoadingPanel();
			HideLoadingPanel();
			dataGridView.AutoGenerateColumns = true;

			if (MainForm.DarkMode) {
				this.UseImmersiveDarkMode();
				dataGridView.SetDarkMode();
				loadingPanel.BackColor = VisualStyles.DarkThemeColors.Control;
			}
		}

		private bool IsFiltered => filter.IsRowFiltered || filter.IsColumnFiltered;

		private bool IsSorted => sort.IsSorted;

		public Database Database => table.Database;

		private void SetupLoadingPanel()
		{
			loadingPanel.Location = new Point(0, 0);
			loadingPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
			loadingPanel.Font = new Font(loadingPanel.Font, FontStyle.Bold);
		}

		private void ShowLoadingPanel()
		{
			loadingProgressBar.Show();
			Controls.Add(loadingPanel);
			loadingPanel.Size = ClientSize;
			loadingPanel.BringToFront();
			loadingPanel.Focus();
		}

		private void HideLoadingPanel()
		{
			Controls.Remove(loadingPanel);
			loadingProgressBar.Hide();
		}

		private void TableDocument_Load(object sender, EventArgs e)
		{
			try {
				UpdateDataSet();
			} catch (Exception ex) {
				MessageBox.Show(this, ex.Message, "Error opening table", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Enabled = false;
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

		public void UpdateDataSet()
		{
			ShowLoadingPanel();
			backgroundWorker.RunWorkerAsync();
		}

		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (filter.IsRowFiltered) {
				count = table.GetRowCount(filter);
			} else {
				count = table.GetRowCount();
			}
			if (currentPage * rowsPerPage >= count) {
				currentPage = 0;
			}
			data = table.GetData(currentPage * rowsPerPage, rowsPerPage, filter, IsSorted ? sort : null);
		}

		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			HideLoadingPanel();

			if (e.Error == null) {
				refreshDataNeeded = false;
				dataGridView.DataSource = data;
				UpdateLocation();
			} else {
				LogManager.LogError($"TableDocument BackgroundWorker (Db: {table.Database.Name} Table: {table.Name})", e.Error);
				MessageBox.Show(this, e.Error.Message, $"Error: {e.Error.GetType().Name}", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void UpdateLocation()
		{
			var startRow = currentPage * rowsPerPage + 1;
			var lastRow = startRow + rowsPerPage - 1;
			if (lastRow > count) {
				lastRow = count;
			}

			filterToolStripButton.Checked = IsFiltered;
			filterToolStripButton.ToolTipText = IsFiltered ? filter.ToString() : "Filter";
			sortToolStripButton.Checked = IsSorted;
			sortToolStripButton.ToolTipText = IsSorted ? sort.ToString() : "Sort";

			locationLabel.Text = $"{startRow:0000} to {lastRow:0000} of {count:0000}" + (IsFiltered ? "*" : "");

			firstButton.Enabled = currentPage > 0;
			prevButton.Enabled = currentPage > 0;
			nextButton.Enabled = lastRow < count;
			lastButton.Enabled = lastRow < count;
			dataGridView.AllowUserToAddRows = true;
			dataGridView.StartRowNumber = startRow;
		}

		public void SetReadOnlyTable()
		{
			saveChangesButton.Enabled = false;
			discardChangesButton.Enabled = false;
			dataGridView.ReadOnly = true;
			dataGridView.AllowUserToAddRows = false;
		}

		private void SaveChangesButton_Click(object sender, EventArgs e)
		{
			var changes = GetChanges();
			if (changes != null) {
				if (SaveChanges(changes)) {
					UpdateDataSet();
				}
			}
		}

		private DataTable GetChanges()
		{
			Validate();
			dataGridView.EndEdit();
			// data can be null if an error occurred
			if (data == null) {
				return null;
			}
			return data.GetChanges();
		}

		/// <summary></summary>
		/// <returns>Returns True if the program can procced, otherwise False.</returns>
		private bool AskAndSave(DataTable changes)
		{
			Activate();
			var result = MessageBox.Show(this, Resources.UnsavedChanges, Text, MessageBoxButtons.YesNoCancel);
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
				using (var adapter = table.Database.CreateAdapter(table, filter)) {
					adapter.ContinueUpdateOnError = false;
					adapter.Update(changes);
					data.AcceptChanges();
					refreshDataNeeded = true;
					////UpdateDataSet(true);
				}
			} catch (Exception ex) {
				MessageBox.Show(this, ex.Message, ex.GetType().ToString());
				return false;
			}
			return true;
		}

		private void DiscardChangesButton_Click(object sender, EventArgs e)
		{
			Validate();
			data.RejectChanges();
		}

		private bool CanContinue()
		{
			var changes = GetChanges();
			if (changes != null) {
				if (!AskAndSave(changes)) {
					return false;
				}
			}
			return true;
		}

		private void FirstButton_Click(object sender, EventArgs e)
		{
			if (CanContinue()) {
				currentPage = 0;
				UpdateDataSet();
			}
		}

		private void PrevButton_Click(object sender, EventArgs e)
		{
			if (CanContinue()) {
				currentPage--;
				UpdateDataSet();
			}
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			if (CanContinue()) {
				currentPage++;
				UpdateDataSet();
			}
		}

		private void LastButton_Click(object sender, EventArgs e)
		{
			if (CanContinue()) {
				currentPage = (count - 1) / rowsPerPage;
				UpdateDataSet();
			}
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			if (CanContinue()) {
				UpdateDataSet();
			}
		}

		private void SelectNewRow()
		{
			dataGridView.ClearSelection();
			dataGridView.Rows[dataGridView.NewRowIndex].Selected = true;
			dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.NewRowIndex;
		}

		private void NewRowButton_Click(object sender, EventArgs e)
		{
			SelectNewRow();
		}

		#region Printing

		private void PrintPreviewButton_Click(object sender, EventArgs e)
		{
			dataTablePrintDocument.DataTable = data;

			var parent = printPreviewDialog.PrintPreviewControl.Parent as Form;
			parent.WindowState = FormWindowState.Maximized;

			printPreviewDialog.ShowDialog();
		}

		private void FontButton_Click(object sender, EventArgs e)
		{
			fontDialog.Font = dataTablePrintDocument.Font;
			if (fontDialog.ShowDialog(this) == DialogResult.OK) {
				dataTablePrintDocument.Font = fontDialog.Font;
			}
		}

		private void PageSetupButton_Click(object sender, EventArgs e)
		{
			pageSetupDialog.Document = dataTablePrintDocument;
			pageSetupDialog.ShowDialog();
		}

		#endregion

		private void FilterToolStripButton_Click(object sender, EventArgs e)
		{
			if (!CanContinue()) {
				return;
			}
			using (var filterDialog = new FilterDialog(filter.Clone())) {
				filterDialog.PositionDown(filterToolStripButton, this);
				if (filterDialog.ShowDialog(this) == DialogResult.OK) {
					var lastGood = filter;
					filter = filterDialog.Filter;

					if (IsFiltered) {
						currentPage = 0;
					}
					filterToolStripButton.Checked = IsFiltered;
					filterToolStripButton.ToolTipText = IsFiltered ? filter.ToString() : "Filter";

					try {
						UpdateDataSet();
					} catch (Exception ex) {
						filter = lastGood;
						MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK);
						UpdateDataSet();
					}
				} else {
					if (refreshDataNeeded) {
						UpdateDataSet();
					}
				}
			}
		}

		private void SortToolStripButton_Click(object sender, EventArgs e)
		{
			if (!CanContinue()) {
				return;
			}
			using (var sortDialog = new SortDialog(sort)) {
				sortDialog.PositionDown(sortToolStripButton, this);
				if (sortDialog.ShowDialog(this) == DialogResult.OK) {
					var lastGood = sort;
					sort = sortDialog.Sort;
					sortToolStripButton.Checked = IsSorted;
					sortToolStripButton.ToolTipText = IsSorted ? sort.ToString() : "Sort";

					try {
						UpdateDataSet();
					} catch (Exception ex) {
						MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK);
						sort = lastGood;
						UpdateDataSet();
					}
				} else {
					if (refreshDataNeeded) {
						UpdateDataSet();
					}
				}
			}
		}

		private void TableDocument_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing) {
				if (backgroundWorker.IsBusy) {
					MessageBox.Show(this, "Reading table data, please wait...", ProductName);
					e.Cancel = true;
					return;
				}
				var changes = GetChanges();
				if (changes != null) {
					if (!AskAndSave(changes)) {
						e.Cancel = true;
					}
				}
			}
		}

		private void ViewSqlToolStripButton_Click(object sender, EventArgs e)
		{
			var sql = new StringBuilder();
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

			var doc = new CommandDocument(table.Database) {
				Text = string.Format(Resources.QueryDocumentTitle, table.Database.Name),
				CommandText = sql.ToString()
			};
			doc.Show(DockPanel);
		}

		private void AllCellsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
		}

		private void AllCellsButHeadersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
		}

		private void ColumnsHeaderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
		}

		private void DataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
		{
			e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
		}

		private void RowsPerPageButton_Click(object sender, EventArgs e)
		{
			if (!CanContinue()) {
				return;
			}
			using (var dialog = new RowsPerPageDialog()) {
				dialog.RowsPerPage = rowsPerPage;
				dialog.PositionDown(rowsPerPageButton, this);
				if (dialog.ShowDialog(this) == DialogResult.OK) {
					if (rowsPerPage != dialog.RowsPerPage) {
						rowsPerPage = dialog.RowsPerPage;
						rowsPerPageButton.Text = rowsPerPage.ToString();
						UpdateDataSet();
					}
				} else {
					if (refreshDataNeeded) {
						UpdateDataSet();
					}
				}
			}
		}

		private void ExportAllToExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProgressDialog.Run(this, "Export to Excel", excelWorker, true, true);
		}

		private void ExportCurrentPageToExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProgressDialog.Run(this, "Export to Excel", excelWorker, true, false);
		}

		private void ExcelWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var allData = (bool)e.Argument;
			var dataToExport = allData ? table.GetData(filter) : data;

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

		private void CopyWithHeaders_Click(object sender, EventArgs e)
		{
			dataGridView.CopyDataFromGrid(includeHeaders: true);
		}

		private void CopyDataOnly_Click(object sender, EventArgs e)
		{
			dataGridView.CopyDataFromGrid(includeHeaders: false);
		}
	}
}
