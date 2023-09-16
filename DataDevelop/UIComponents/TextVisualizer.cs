using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using FastColoredTextBoxNS;

namespace DataDevelop.UIComponents
{
	public partial class TextVisualizer : Form
	{
		private bool textChanged;

		public TextVisualizer()
		{
			InitializeComponent();
			textEditor.Font = Properties.Settings.Default.TextVisualizerFont;
			if (MainForm.DarkMode) {
				statusStrip.BackColor = Color.FromArgb(61, 61, 61);
			}
		}

		public bool TextValueChanged
		{
			get { return textChanged; }
		}

		public bool ReadOnly
		{
			get { return textEditor.ReadOnly; }
			set
			{
				textEditor.ReadOnly = value;
				newToolStripButton.Enabled = !value;
				openToolStripButton.Enabled = !value;
				formatXmlToolStripMenuItem.Enabled = !value;
				cutToolStripButton.Enabled = !value;
				pasteToolStripButton.Enabled = !value;
			}
		}

		public string TextValue
		{
			get { return textEditor.Text; }
			set
			{
				textEditor.Text = value;
				textChanged = false;
				textEditor.ClearUndo();
				if (value != null) {
					DetectLanguage(value);
				}
			}
		}

		private void DetectLanguage(string text)
		{
			var sqlRegex = new Regex(@"^\s*(SELECT|DECLARE|--+)\s+", RegexOptions.IgnoreCase);
			if (sqlRegex.IsMatch(text)) {
				sqlToolStripMenuItem.PerformClick();
				return;
			}
			var xmlRegex = new Regex(@"\s*<xml\s+.*", RegexOptions.IgnoreCase);
			if (xmlRegex.IsMatch(text)) {
				xmlToolStripMenuItem.PerformClick();
				return;
			}
			var htmlRegex = new Regex(@"^\s*(<html|<doctype)(>|\s).*", RegexOptions.IgnoreCase);
			if (htmlRegex.IsMatch(text)) {
				htmlToolStripMenuItem.PerformClick();
				return;
			}
			if (Regex.IsMatch(text, @"^\s*<.+>\s*$", RegexOptions.Multiline)) {
				xmlToolStripMenuItem.PerformClick();
				return;
			}
		}

		private void textEditor_TextChanged(object sender, TextChangedEventArgs e)
		{
			textChanged = true;
			statusLabel.Text = "Text Length: " + textEditor.TextLength;
		}

		private void TextVisualizer_Load(object sender, EventArgs e)
		{
			textEditor.SelectionStart = 0;
			textEditor.SelectionLength = 0;
		}

		private void newToolStripButton_Click(object sender, EventArgs e)
		{
			textEditor.ResetText();
		}

		private void openToolStripButton_Click(object sender, EventArgs e)
		{
			const long MaxLength = (4L * 1024 * 1024); // 4MB
			if (openFileDialog.ShowDialog(this) == DialogResult.OK) {
				var info = new FileInfo(openFileDialog.FileName);
				if (info.Length <= MaxLength ||
					MessageBox.Show(this, "Are you sure to open this large file?") == DialogResult.OK) {
					textEditor.Text = File.ReadAllText(info.FullName);
				}
			}
		}

		private void saveToolStripButton_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
				File.WriteAllText(saveFileDialog.FileName, textEditor.Text);
			}
		}

		private void cutToolStripButton_Click(object sender, EventArgs e)
		{
			textEditor.Cut();
		}

		private void copyToolStripButton_Click(object sender, EventArgs e)
		{
			textEditor.Copy();
		}

		private void pasteToolStripButton_Click(object sender, EventArgs e)
		{
			textEditor.Paste();
		}

		private void toggleWordWrapButton_Click(object sender, EventArgs e)
		{
			textEditor.WordWrap = toggleWordWrapButton.Checked;
		}

		private void changeFontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fontDialog.Font = textEditor.Font;
			if (fontDialog.ShowDialog(this) == DialogResult.OK) {
				textEditor.Font = fontDialog.Font;
				Properties.Settings.Default.TextVisualizerFont = fontDialog.Font;
				Properties.Settings.Default.Save();
			}
		}

		private void formatXmlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try {
				var xd = new XmlDocument();
				xd.LoadXml(textEditor.Text);
				var sb = new StringBuilder();
				var sw = new StringWriter(sb);
				var xtw = new XmlTextWriter(sw);
				xtw.Formatting = Formatting.Indented;
				xd.WriteTo(xtw);
				xtw.Close();
				textEditor.Text = sb.ToString();
			} catch (Exception ex) {
				MessageBox.Show(this, ex.Message, "Error formatting XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A) {
				e.Handled = true;
				e.SuppressKeyPress = true;
				textEditor.SelectAll();
			} else if (e.Modifiers == Keys.None && e.KeyCode == Keys.Escape) {
				e.Handled = true;
				e.SuppressKeyPress = true;
				Close();
			}
		}

		private void SetLanguage(Language lang)
		{
			textEditor.ClearStylesBuffer();
			textEditor.Range.ClearStyle(StyleIndex.All);
			textEditor.Language = lang;
			textEditor.OnSyntaxHighlight(new TextChangedEventArgs(textEditor.Range));
		}

		private void CheckSyntaxItem(ToolStripMenuItem sender)
		{
			foreach (var item in syntaxDropDownButton.DropDownItems) {
				if (item is ToolStripMenuItem menuItem) {
					menuItem.Checked = item == sender;
				}
			}
		}

		private void plainTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetLanguage(Language.Custom);
			CheckSyntaxItem((ToolStripMenuItem)sender);
		}

		private void sqlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetLanguage(Language.SQL);
			CheckSyntaxItem((ToolStripMenuItem)sender);
		}

		private void xmlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetLanguage(Language.XML);
			CheckSyntaxItem((ToolStripMenuItem)sender);
		}

		private void htmlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetLanguage(Language.HTML);
			CheckSyntaxItem((ToolStripMenuItem)sender);
		}

		private void javaScriptToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetLanguage(Language.JS);
			CheckSyntaxItem((ToolStripMenuItem)sender);
		}
	}
}
