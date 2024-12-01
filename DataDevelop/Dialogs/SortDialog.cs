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
			
			if (MainForm.DarkMode) {
				this.UseImmersiveDarkMode();
				sortPanel.SetDarkMode();
				BackColor = VisualStyles.DarkThemeColors.Background;
				ForeColor = VisualStyles.DarkThemeColors.TextColor;

				okButton.FlatStyle = FlatStyle.Flat;
				okButton.BackColor = VisualStyles.DarkThemeColors.Control;
				cancelButton.FlatStyle = FlatStyle.Flat;
				cancelButton.BackColor = VisualStyles.DarkThemeColors.Control;
			}
		}

		public TableSort Sort => sortPanel.Sort;

		private void SortDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
			Settings.Default.SortDialogSize = Size;
		}
	}
}
