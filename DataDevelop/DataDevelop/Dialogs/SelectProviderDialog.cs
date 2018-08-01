using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataDevelop.Data;

namespace DataDevelop.Dialogs
{
	public partial class SelectProviderDialog : BaseDialog
	{
		public SelectProviderDialog()
		{
			InitializeComponent();
		}

		public DbProvider Provider
		{
			get { return providersListBox.SelectedItem as DbProvider; }
		}

		private void SelectProviderDialog_Load(object sender, EventArgs e)
		{
			okButton.Enabled = false;
			foreach (DbProvider provider in DbProvider.Providers.Values) {
				providersListBox.Items.Add(provider);
			}
		}

		private void providersListBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			okButton.Enabled = providersListBox.SelectedIndex >= 0;
		}

		private void providersListBox_DoubleClick(object sender, EventArgs e)
		{
			if (providersListBox.SelectedIndex >= 0) {
				okButton.PerformClick();
			}
		}
	}
}