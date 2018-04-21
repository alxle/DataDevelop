using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DataDevelop.UIComponents
{
	public partial class TextVisualizer : Form
	{
		private bool textChanged;

		public TextVisualizer()
		{
			InitializeComponent();
			this.textBox.Font = Properties.Settings.Default.TextVisualizerFont;
		}

		public bool TextValueChanged
		{
			get { return this.textChanged; }
		}

		public bool ReadOnly
		{
			get { return this.textBox.ReadOnly; }
			set
			{
				this.textBox.ReadOnly = value;
				this.newToolStripButton.Enabled = !value;
				this.openToolStripButton.Enabled = !value;
				this.formatXmlToolStripMenuItem.Enabled = !value;
				this.cutToolStripButton.Enabled = !value;
				this.pasteToolStripButton.Enabled = !value;
			}
		}

		public string TextValue
		{
			get { return this.textBox.Text; }
			set
			{
				this.textBox.Text = value;
				this.textChanged = false;
			}
		}

		private void textBox_TextChanged(object sender, EventArgs e)
		{
			this.textChanged = true;
			this.statusLabel.Text = "Text Length: " + this.textBox.TextLength;
		}

		private void TextVisualizer_Load(object sender, EventArgs e)
		{
			this.textBox.SelectionStart = 0;
			this.textBox.SelectionLength = 0;
		}

		private void newToolStripButton_Click(object sender, EventArgs e)
		{
			this.textBox.Clear();
		}

		private void openToolStripButton_Click(object sender, EventArgs e)
		{
			const long MaxLength = (2L * 1024 * 1024); // 2MB
			if (openFileDialog.ShowDialog(this) == DialogResult.OK) {
				var info = new FileInfo(openFileDialog.FileName);
				if (info.Length <= MaxLength) { 
					this.textBox.Text = File.ReadAllText(info.FullName);
				} else {
					MessageBox.Show(this, "File Exceeds the Max Length allowed (2MB)");
				}
			}
		}

		private void saveToolStripButton_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
				File.WriteAllText(saveFileDialog.FileName, this.textBox.Text);
			}
		}

		private void cutToolStripButton_Click(object sender, EventArgs e)
		{
			this.textBox.Cut();
		}

		private void copyToolStripButton_Click(object sender, EventArgs e)
		{
			this.textBox.Copy();
		}

		private void pasteToolStripButton_Click(object sender, EventArgs e)
		{
			this.textBox.Paste();
		}

		private void toggleWordWrapButton_Click(object sender, EventArgs e)
		{
			this.textBox.WordWrap = toggleWordWrapButton.Checked;
		}

		private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.fontDialog.Font = this.textBox.Font;
			if (fontDialog.ShowDialog(this) == DialogResult.OK) {
				this.textBox.Font = this.fontDialog.Font;
				Properties.Settings.Default.TextVisualizerFont = this.fontDialog.Font;
				Properties.Settings.Default.Save();
			}
		}

		private void formatXmlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try {
				XmlDocument xd = new XmlDocument();
				xd.LoadXml(this.textBox.Text);
				StringBuilder sb = new StringBuilder();
				StringWriter sw = new StringWriter(sb);
				XmlTextWriter xtw = new XmlTextWriter(sw);
				xtw.Formatting = Formatting.Indented;
				xd.WriteTo(xtw);
				xtw.Close();
				this.textBox.Text = sb.ToString();
			} catch (Exception ex) {
				MessageBox.Show(this, ex.Message, "Error formatting XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void textBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A) {
				e.Handled = true;
				e.SuppressKeyPress = true;
				textBox.SelectAll();
			} else if (e.Modifiers == Keys.None && e.KeyCode == Keys.Escape) {
				e.Handled = true;
				e.SuppressKeyPress = true;
				this.Close();
			}
		}
	}
}