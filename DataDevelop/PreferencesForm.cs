using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using DataDevelop.Properties;

namespace DataDevelop
{
	public partial class PreferencesForm : Form
	{
		private bool changed;

		public PreferencesForm()
		{
			InitializeComponent();
			applicationPathTextBox.Text = Application.ExecutablePath;
			dataDirectoryTextBox.Text = SettingsManager.GetDataDirectory();
			editorFontTextBox.Text = Settings.Default.TextEditorFont.ToString();
			visualizerFontTextBox.Text = Settings.Default.TextVisualizerFont.ToString();
			rowsPerPageNumericUpDown.Value = Settings.Default.RowsPerPage;
			visualStyleComboBox.SelectedItem = Settings.Default.VisualStyle;
			requiresRestartLabel.Visible = false;
		}

		protected override void OnLoad(EventArgs e)
		{
			Settings.Default.PropertyChanged += PropertyChanged;
			base.OnLoad(e);
		}

		private void PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			this.changed = true;
		}

		protected override void OnClosed(EventArgs e)
		{
			Settings.Default.PropertyChanged -= PropertyChanged;
			base.OnClosed(e);
		}

		private void openDataDirectoryButton_Click(object sender, EventArgs e)
		{
			if (Directory.Exists(dataDirectoryTextBox.Text)) {
				Process.Start(dataDirectoryTextBox.Text);
			}
		}

		private void openApplicationPathButton_Click(object sender, EventArgs e)
		{
			string file = applicationPathTextBox.Text;
			string dir = Path.GetDirectoryName(file);
			if (Directory.Exists(dir)) {
				Process.Start("explorer", String.Format("/select,\"{0}\"", file));
			}
		}

		private void openApplicationPathButton_MouseEnter(object sender, EventArgs e)
		{
			openApplicationPathButton.FlatStyle = FlatStyle.Standard;
		}

		private void openApplicationPathButton_MouseLeave(object sender, EventArgs e)
		{
			openApplicationPathButton.FlatStyle = FlatStyle.Flat;
		}

		private void changeEditorFontButton_Click(object sender, EventArgs e)
		{
			fontDialog.Font = Settings.Default.TextEditorFont;
			if (fontDialog.ShowDialog(this) == DialogResult.OK) {
				Settings.Default.TextEditorFont = fontDialog.Font;
				editorFontTextBox.Text = fontDialog.Font.ToString();
			}
		}

		private void changeVisualizerFontButton_Click(object sender, EventArgs e)
		{
			fontDialog.Font = Settings.Default.TextVisualizerFont;
			if (fontDialog.ShowDialog(this) == DialogResult.OK) {
				Settings.Default.TextVisualizerFont = fontDialog.Font;
				visualizerFontTextBox.Text = fontDialog.Font.ToString();
			}
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (this.changed) {
				Settings.Default.Save();
			}
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			if (this.changed) {
				Settings.Default.Reload();
			}
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			Settings.Default.Reset();
		}

		private void rowsPerPageNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			Settings.Default.RowsPerPage = Decimal.ToInt32(rowsPerPageNumericUpDown.Value);
		}

		private void visualStyleComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			requiresRestartLabel.Visible = true;
			Settings.Default.VisualStyle = visualStyleComboBox.SelectedItem.ToString();
		}
	}
}