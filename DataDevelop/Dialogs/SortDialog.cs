using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop
{
	using Data;
	using Properties;

	internal partial class SortDialog : Form
	{
		public SortDialog(TableSort sort)
		{
			InitializeComponent();
			sortPanel.LoadSort(sort);

			FormExtensions.TrySetSize(this, Settings.Default.SortDialogSize);
		}

		public TableSort Sort
		{
			get { return sortPanel.Sort; }
		}

		private void SortDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
			Properties.Settings.Default.SortDialogSize = this.Size;
		}
	}
}