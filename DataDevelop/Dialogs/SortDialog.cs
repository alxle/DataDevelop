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
	internal partial class SortDialog : Form
	{
		public SortDialog(TableSort sort)
		{
			InitializeComponent();
			sortPanel.LoadSort(sort);
		}

		public TableSort Sort
		{
			get { return sortPanel.Sort; }
		}

		private void SortDialog_Load(object sender, EventArgs e)
		{
			var settingsSize = Properties.Settings.Default.SortDialogSize;
			if (settingsSize.Width >= this.MinimumSize.Width && settingsSize.Height >= settingsSize.Height) {
				this.Size = settingsSize;
			}
		}

		private void SortDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
			Properties.Settings.Default.SortDialogSize = this.Size;
		}
	}
}