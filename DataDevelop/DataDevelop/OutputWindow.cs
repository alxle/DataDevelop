using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop
{
	public partial class OutputWindow : Toolbox
	{
		public OutputWindow()
		{
			InitializeComponent();
		}

		public void WriteUTF8(byte[] buffer, int offset, int count)
		{
			string text = Encoding.UTF8.GetString(buffer, offset, count);
			if (count == 3 && buffer[offset] == 0xEF && buffer[offset + 1] == 0xBB && buffer[offset + 2] == 0xBF) { // Byte order mark 
				return;
			}
			if (!this.Created) {
				CreateControlsInstance();
			}
			this.Invoke(new Action<string>(this.AppendText), text);
		}

		public void WriteUnicode(byte[] buffer, int offset, int count)
		{
			string text = Encoding.Unicode.GetString(buffer, offset, count);
			if (text == "\uFEFF") { // Byte order mark 
				return;
			}
			if (!this.Created) {
				CreateControlsInstance();
			}
			this.Invoke(new Action<string>(this.AppendText), text);
		}

		public void AppendText(string text)
		{
			outputTextBox.AppendText(text);
			outputTextBox.GoEnd();
		}

		public void AppendMessage(string text)
		{
			outputTextBox.AppendText(text);
			outputTextBox.AppendText(Environment.NewLine);
			outputTextBox.GoEnd();
		}

		public void AppendInfo(string text)
		{
			outputTextBox.LogInfo(text);
		}

		public void AppendError(string text)
		{
			outputTextBox.LogError(text);
		}

		public void FocusOutput()
		{
			outputTextBox.GoEnd();
			outputTextBox.Focus();
		}

		private void clearAllButton_Click(object sender, EventArgs e)
		{
			outputTextBox.Clear();
		}

		private void toggleWordWrapButton_CheckedChanged(object sender, EventArgs e)
		{
			outputTextBox.WordWrap = wordWrapToolStripMenuItem.Checked;
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			outputTextBox.Copy();
		}
	}
}