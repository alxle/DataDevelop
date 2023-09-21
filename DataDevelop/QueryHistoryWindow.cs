using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DataDevelop.Data;
using DataDevelop.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace DataDevelop
{
	public partial class QueryHistoryWindow : Toolbox
	{
		readonly QueryHistoryManager manager;

		public QueryHistoryWindow()
		{
			InitializeComponent();
			DockAreas = DockAreas.Float | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Document;
			if (MainForm.DarkMode) {
				dataGridView1.SetDarkMode();
			}
			
			manager = QueryHistoryManager.Instance;
			dataGridView1.AutoGenerateColumns = false;
			dataGridView1.DataSource = manager.Data;
			dataGridView1.Sort(queryDateColumn, ListSortDirection.Descending);
		}

		public void AddQuery(Database db, string queryText)
		{
			manager.Insert(db.Name, db.Provider.Name, queryText);
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			manager.Refresh();
		}

		private void UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			var result = MessageBox.Show(this, 
				"Delete selected row?", "Confirmation", MessageBoxButtons.YesNo);
			if (result != DialogResult.Yes) {
				e.Cancel = true;
			}
		}

		private void UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			manager.SaveChanges();
		}

		private void ToggleEnabled(object sender, EventArgs e)
		{
			Settings.Default.QueryHistoryEnabled = !Settings.Default.QueryHistoryEnabled;
			historyEnabledToolStripMenuItem.Checked = Settings.Default.QueryHistoryEnabled;
		}

		private void Settings_DropDownOpening(object sender, EventArgs e)
		{
			historyEnabledToolStripMenuItem.Checked = Settings.Default.QueryHistoryEnabled;
		}
	}
}
