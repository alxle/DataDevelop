using System;
using System.Drawing;
using System.Text;

namespace DataDevelop
{
	public partial class OutputWindow : Toolbox
	{
		public OutputWindow()
		{
			InitializeComponent();
			if (MainForm.DarkMode) {
				this.UseImmersiveDarkMode();
				outputTextBox.ForeColor = VisualStyles.DarkThemeColors.TextColor;
				outputTextBox.BackColor = VisualStyles.DarkThemeColors.Background;
				outputTextBox.MessageTextColor = VisualStyles.DarkThemeColors.TextColor;
				outputTextBox.InfoTextColor = VisualStyles.DarkThemeColors.InfoTextColor;
				outputTextBox.ErrorTextColor = VisualStyles.DarkThemeColors.ErrorTextColor;
			}
		}

		public void WriteOutput(byte[] buffer, int offset, int count, Encoding encoding)
		{
			var text = encoding.GetString(buffer, offset, count);
			Invoke(new Action<string>(AppendText), text);
		}

		public void AppendText(string text)
		{
			EnsureControlInstance();
			outputTextBox.AppendText(text);
			outputTextBox.GoEnd();
		}

		public void AppendMessage(string text)
		{
			EnsureControlInstance();
			outputTextBox.AppendText(text);
			outputTextBox.AppendText(Environment.NewLine);
			outputTextBox.GoEnd();
		}

		public void AppendInfo(string text)
		{
			EnsureControlInstance();
			outputTextBox.LogInfo(text);
		}

		public void AppendError(string text)
		{
			EnsureControlInstance();
			outputTextBox.LogError(text);
		}

		public void FocusOutput()
		{
			EnsureControlInstance();
			outputTextBox.GoEnd();
			outputTextBox.Focus();
		}

		private void EnsureControlInstance()
		{
			if (!Created) {
				CreateControlsInstance();
			}
		}

		private void ClearAllButton_Click(object sender, EventArgs e)
		{
			outputTextBox.Clear();
		}

		private void ToggleWordWrapButton_CheckedChanged(object sender, EventArgs e)
		{
			outputTextBox.WordWrap = wordWrapToolStripMenuItem.Checked;
		}

		private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			outputTextBox.Copy();
		}
	}
}
