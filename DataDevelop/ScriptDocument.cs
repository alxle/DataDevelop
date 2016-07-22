using System;
using System.ComponentModel;
using System.Windows.Forms;
using DataDevelop.Data;
using DataDevelop.Dialogs;
using DataDevelop.Scripting;
using DataDevelop.UIComponents;
using DataDevelop.Utils;

namespace DataDevelop
{
	public partial class ScriptDocument : Document
	{
		private ScriptEngine engine;
		private OutputWindow output;
		private FindAndReplaceDialog findDialog = new FindAndReplaceDialog();

		public ScriptDocument(OutputWindow output, ScriptEngine engine)
		{
			InitializeComponent();
			this.output = output;

			var isPython = (engine is PythonScriptEngine);
			var highlighter = isPython ? Highlighters.Python : Highlighters.Javascript;
			var outputStream = new StreamWriteDelegator(this.Output.WriteUnicode);

			textEditorControl.Document.HighlightingStrategy = highlighter;
			this.Text = String.Concat(highlighter.Name, " Console");

			textEditorControl.ShowEOLMarkers = false;
			textEditorControl.ShowSpaces = false;
			textEditorControl.ShowInvalidLines = false;

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
				EnableUI(false, "Executing...");

				foreach (string line in StringUtils.GetLines(code)) {
					output.AppendInfo(">>> " + line);
				}
				output.FocusOutput();

				backgroundWorker.RunWorkerAsync(code);
			}
		}

		private void EnableUI(bool value, string status)
		{
			executeToolStripMenuItem.Enabled = value;
			executeButton.Enabled = value;
			stopButton.Enabled = !value;
			stopButton.Visible = !value;
			textEditorControl.IsReadOnly = !value;
			statusLabel.Text = status;
		}

		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string code = (string)e.Argument;
			engine.Execute(code);
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			EnableUI(true, "Ready...");

			if (e.Error != null || e.Cancelled) {
				output.AppendMessage("");
				if (e.Error != null) {
					var syntaxError = e.Error as Microsoft.Scripting.SyntaxErrorException;
					if (syntaxError != null) {
						output.AppendError("Syntax Error: " + e.Error.Message);
						output.AppendError(String.Format("Error Code: {0}, Line: {1}, Column: {2}", syntaxError.ErrorCode, syntaxError.Line, syntaxError.Column));
					} else {
						output.AppendError(e.Error.ToString());
					}
				} else {
					output.AppendError("Script execution was cancelled.");
				}
			}
			output.Show();
			textEditorControl.Focus();
		}

		private void ShowOutput()
		{
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

		private void showResultPanelToolStripButton_Click(object sender, EventArgs e)
		{
			this.ShowOutput();
		}

		private void ScriptDocument_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing) {
				if (this.backgroundWorker.IsBusy) {
					MessageBox.Show(this, "Script is executing, please wait...", this.ProductName);
					e.Cancel = true;
					return;
				}
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

		private void stopButton_Click(object sender, EventArgs e)
		{
			this.stopButton.Enabled = false;
			this.statusLabel.Text = "Aborting...";
			this.backgroundWorker.AbortAsync();
		}

		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			findDialog.ShowFor(this.textEditorControl, false);
		}

		private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			findDialog.ShowFor(this.textEditorControl, true);
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (textEditorControl.HasChanges) {
				var result = MessageBox.Show(this, "Save Changes?", "Confirmation", MessageBoxButtons.YesNoCancel);
				if (result == DialogResult.Yes) {
					Save();
					if (this.textEditorControl.HasChanges) {
						return;
					}
				}
				if (result == DialogResult.Cancel) {
					return;
				}
			}
			textEditorControl.Visible = false;
			textEditorControl.FileName = null;
			textEditorControl.ResetText();
			textEditorControl.Visible = true;
		}
	}
}

