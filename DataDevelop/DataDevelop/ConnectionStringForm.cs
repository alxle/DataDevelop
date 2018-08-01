using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Text.RegularExpressions;
using DataDevelop.Data;

namespace DataDevelop
{
	public partial class ConnectionStringForm : Form
	{
		Regex nameValidator = new Regex(@"^[_A-Za-z]\w*$", RegexOptions.Compiled);

		public ConnectionStringForm()
		{
			InitializeComponent();
			propertyGrid1.PropertySort = PropertySort.CategorizedAlphabetical;
		}

		private DbConnectionStringBuilder builder;

		public DbConnectionStringBuilder ConnectionStringBuilder
		{
			get { return builder; }
			set
			{
				builder = value;
				propertyGrid1.SelectedObject = value;
			}
		}

		public string ConnectionString
		{
			get { return (builder != null) ? builder.ConnectionString : String.Empty; }
		}

		public string DatabaseName
		{
			get { return nameTextBox.Text; }
			set { nameTextBox.Text = value; }
		}

		private void nameTextBox_Validating(object sender, CancelEventArgs e)
		{
			string name = nameTextBox.Text.Trim();
			if (String.IsNullOrEmpty(name)) {
				errorProvider.SetError(nameTextBox, "Please specify a name");
				e.Cancel = true;
			} else if (!nameValidator.IsMatch(name)) {
				errorProvider.SetError(nameTextBox, "Invalid identifier name");
				e.Cancel = true;
			} else if (!isUpdate && DatabasesManager.Contains(name)) {
				errorProvider.SetError(nameTextBox, "It already exists a database with the name: " + name);
				e.Cancel = true;
			} else {
				errorProvider.Clear();
			}
		}

		private bool isUpdate;

		public bool IsUpdate
		{
			get { return isUpdate; }
			set
			{
				isUpdate = value;
				nameTextBox.ReadOnly = isUpdate;
			}
		}

		private void ConnectionStringForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK) {
				e.Cancel = !this.Validate();
			}
		}

	}
}