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
		////private IDictionary<string, string> outputs;
		private Color currentColor;

		public OutputWindow()
		{
			InitializeComponent();
			////outputs = new Dictionary<string, string>();
			currentColor = results.ForeColor;
		}

		public Color CurrentTextColor
		{
			get { return currentColor; }
			set
			{
				if (currentColor != value) {
					results.SelectionStart = results.TextLength;
					currentColor = value;
					results.SelectionColor = value;
				}
			}
		}

		public Font OutputFont
		{
			get { return results.Font; }
			set { results.Font = value; }
		}

		public void WriteUTF8(byte[] buffer, int offset, int count)
		{
			string text = Encoding.UTF8.GetString(buffer, offset, count);
			if (!this.Created) {
				CreateControlsInstance();
			}
			results.Invoke(new Action<string>(results.AppendText), text);
		}

		public void WriteUnicode(byte[] buffer, int offset, int count)
		{
			string text = Encoding.Unicode.GetString(buffer, offset, count);
			if (!this.Created) {
				CreateControlsInstance();
			}
			results.Invoke(new Action<string>(results.AppendText), text);
		}

		public void AppendText(string text)
		{
			results.SelectionColor = currentColor;
			results.SelectionStart = results.TextLength;
			results.SelectedText = text;
		}

		public void AppendMessage(string message)
		{
			AppendText(message);
			AppendText(Environment.NewLine);
			////results.ScrollToCaret();
		}

		public void FocusOutput()
		{
			results.Focus();
		}

		public void ResetTextColor()
		{
			CurrentTextColor = results.ForeColor;
		}

		private void clearAllButton_Click(object sender, EventArgs e)
		{
			results.Clear();
		}

		private void toggleWordWrapButton_CheckedChanged(object sender, EventArgs e)
		{
			results.WordWrap = toggleWordWrapButton.Checked;
		}
	}
}