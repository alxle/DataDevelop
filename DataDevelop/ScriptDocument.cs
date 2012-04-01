using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DataDevelop.Scripting;
using DataDevelop.UIComponents;
using DataDevelop.Utils;
using WeifenLuo.WinFormsUI.Docking;
using DataDevelop.Data;

namespace DataDevelop
{
	public partial class ScriptDocument : Document
	{
		private ScriptEngine engine;
		private OutputWindow output;

		public ScriptDocument(OutputWindow output, ScriptEngine engine)
		{
			InitializeComponent();
			this.output = output;
			
			this.toolStrip.Renderer = SystemToolStripRenderers.ToolStripSquaredEdgesRenderer;

			var isPython = (engine is PythonScriptEngine);
            var highlighter = isPython ? Highlighters.Python : Highlighters.Javascript;
			var outputStream = new StreamWriteDelegator(this.Output.WriteUTF8);
			
			////if (!isPython) {
			////    outputStream = new StreamWriteDelegator(this.Output.WriteUnicode);
			////}

            textEditorControl.Document.HighlightingStrategy = highlighter;
			this.Text = String.Concat(highlighter.Name, " Console");

			textEditorControl.ShowEOLMarkers = false;
			textEditorControl.ShowSpaces = false;
			textEditorControl.ShowInvalidLines = false;

			//output.OutputFont = new Font(FontFamily.GenericMonospace, 8F);
            
            this.engine = engine;
			this.engine.Initialize(outputStream, DatabasesManager.Databases);
		}

		public string SelectedText
		{
			get
			{
				if (textEditorControl.ActiveTextAreaControl.SelectionManager.HasSomethingSelected) {
					return textEditorControl.ActiveTextAreaControl.SelectionManager.SelectedText;
				} else {
					return textEditorControl.Text;
				}
			}
		}

		private void executeButton_Click(object sender, EventArgs e)
		{
			string code = SelectedText;
			if (code.Length > 0) {
				executeToolStripMenuItem.Enabled = false;
				executeButton.Enabled = false;
				textEditorControl.IsReadOnly = true;
				statusLabel.Text = "Executing...";

				output.CurrentTextColor = Color.DarkBlue;
				foreach (string line in StringUtils.GetLines(code)) {
					output.AppendMessage(">>> " + line);
				}
				output.FocusOutput();
				output.ResetTextColor();

				backgroundWorker.RunWorkerAsync(code);
			}
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string code = (string)e.Argument;
			engine.Execute(code);
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			executeToolStripMenuItem.Enabled = true;
			executeButton.Enabled = true;
			textEditorControl.IsReadOnly = false;
			statusLabel.Text = "Ready...";

			if (e.Error != null) {
				output.CurrentTextColor = Color.Red;
				//output.AppendMessage("IronPython engine complained: ");
				output.AppendMessage(e.Error.Message);
				output.ResetTextColor();
			}
			output.Show();
			textEditorControl.Focus();
		}

		private void ShowOutput()
		{
			////output.DockTo(this.DockPanel, DockStyle.Fill);
			////output.Show(this.Pane, DockAlignment.Bottom, 0.3);
			output.Show();
			this.Focus();
		}

		public OutputWindow Output
		{
			get { return output; }
		}

		private void editToolStripMenuItem1_DropDownOpening(object sender, EventArgs e)
		{
			this.undoToolStripMenuItem.Enabled = textEditorControl.EnableUndo;
			this.redoToolStripMenuItem.Enabled = textEditorControl.EnableRedo;
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textEditorControl.Undo();
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textEditorControl.Redo();
		}

		private void cutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(sender, e);
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(sender, e);
		}

		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(sender, e);
		}
		
		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll(sender, e);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog()) {
				dialog.Filter = String.Format("{0} Script (*{1})|*{1}|All Files (*.*)|*.*", engine.Name, engine.Extension);
				dialog.Title = "Select File to open...";
				dialog.Multiselect = false;
				if (dialog.ShowDialog(this) == DialogResult.OK) {
					textEditorControl.LoadFile(dialog.FileName, false, true);
					textEditorControl.FileName = dialog.FileName;
				}
			}
		}

		private void Save()
		{
			if (textEditorControl.FileName != null) {
				textEditorControl.SaveFile(textEditorControl.FileName);
			} else {
				SaveAs();
			}
		}

		private void SaveAs()
		{
			using (SaveFileDialog dialog = new SaveFileDialog()) {
				dialog.Filter = String.Format("{0} Script (*{1})|*{1}|All Files (*.*)|*.*", engine.Name, engine.Extension);
				dialog.FilterIndex = 0;
				if (DialogResult.OK == dialog.ShowDialog()) {
					textEditorControl.SaveFile(dialog.FileName);
					textEditorControl.FileName = dialog.FileName;
				}
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveAs();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ScriptDocument_Load(object sender, EventArgs e)
		{
			////Application.DoEvents();
			////this.ShowOutput();
			////this.Activate();
		}

		private void ScriptDocument_Shown(object sender, EventArgs e)
		{
			
		}

		private void showResultPanelToolStripButton_Click(object sender, EventArgs e)
		{
			this.ShowOutput();
		}

		private void ScriptDocument_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing) {
				if (textEditorControl.HasChanges) {
					this.Activate();
					switch (MessageBox.Show(this, "Save Changes?", "Confirmation", MessageBoxButtons.YesNoCancel)) {
						case DialogResult.Yes:
							Save();
							e.Cancel = textEditorControl.HasChanges;
							break;

						case DialogResult.Cancel:
							e.Cancel = true;
							break;
					}
				}
			}
		}
	}
}

