using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DataDevelop.Data;
using DataDevelop.Dialogs;
using DataDevelop.Scripting;
using DataDevelop.UIComponents;

namespace DataDevelop
{
	public partial class ScriptDocument : Document
	{
		private ScriptEngine engine;
		private OutputWindow output;
		private FindAndReplaceDialog findDialog = new FindAndReplaceDialog();
		private Stopwatch watch;

		public ScriptDocument(OutputWindow output, ScriptEngine engine)
		{
			InitializeComponent();
			if (MainForm.DarkMode) {
				this.UseImmersiveDarkMode();
			}
			this.output = output;

			var isPython = (engine is PythonScriptEngine);
			var highlighter = isPython ? Highlighters.Python : Highlighters.Javascript;
			var outputStream = new IO.StreamWriteDelegator(Output.WriteOutput, engine.OutputEncoding);

			textEditorControl.Document.HighlightingStrategy = highlighter;
			Text = $"{highlighter.Name} Console";

			textEditorControl.ShowEOLMarkers = false;
			textEditorControl.ShowSpaces = false;
			textEditorControl.ShowInvalidLines = false;

			this.engine = engine;
			this.engine.Initialize(outputStream, DatabasesManager.Databases);
			this.engine.SetOutputWrite(str => Output.Invoke(Output.AppendText, str));
		}

		public OutputWindow Output => output;

		public string SelectedText
		{
			get
			{
				if (textEditorControl.ActiveTextAreaControl.SelectionManager.HasSomethingSelected) {
					return textEditorControl.ActiveTextAreaControl.SelectionManager.SelectedText;
				}
				return textEditorControl.Text;
			}
		}

		private bool ShowFullException => outputFullExceptionDetailsToolStripMenuItem.Checked;

		private void executeButton_Click(object sender, EventArgs e)
		{
			var code = SelectedText;
			if (code.Length > 0) {
				EnableUI(false, "Executing...");

				var lineNumber = 1;
				string line = null;
				using (var reader = new StringReader(code)) {
					while ((line = reader.ReadLine()) != null) {
						output.AppendInfo($"{lineNumber++,-3}|" + line);
					}
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
			watch = Stopwatch.StartNew();
			var code = (string)e.Argument;
			engine.Execute(code);
		}

		private void OutputError(Exception error)
		{
			if (error is ScriptSyntaxException syntaxError) {
				output.AppendError(">>> Syntax Error: " + syntaxError.Message);
				var errorMessage = ">>> ";
				if (syntaxError.ErrorCode != null) {
					errorMessage += ($"Error Code: {syntaxError.ErrorCode}, ");
				}
				errorMessage += $"Line: {syntaxError.Line}, Column: {syntaxError.Column}";
				output.AppendError(errorMessage);
			} else if (ShowFullException) {
				output.AppendError($">>> {error}");
			} else {
				output.AppendError($">>> {error.GetType().Name}: {error.Message}");
			}
		}

		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			watch?.Stop();
			EnableUI(true, "Ready...");

			output.AppendMessage(""); // Append New Line
			if (e.Error != null) {
				OutputError(e.Error);
			}
			if (e.Cancelled) {
				output.AppendError(">>> Script execution was cancelled.");
			}
			if (watch != null) {
				output.AppendInfo($">>> Elapsed: {watch.Elapsed}");
			}
			output.Show();
			textEditorControl.Focus();
		}

		private void ShowOutput()
		{
			output.Show();
			Focus();
		}

		private void editToolStripMenuItem1_DropDownOpening(object sender, EventArgs e)
		{
			undoToolStripMenuItem.Enabled = textEditorControl.EnableUndo;
			redoToolStripMenuItem.Enabled = textEditorControl.EnableRedo;
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
			if (AskToSaveChanges()) {
				using (var dialog = new OpenFileDialog()) {
					dialog.Filter = string.Format("{0} Script (*{1})|*{1}|All Files (*.*)|*.*", engine.Name, engine.Extension);
					dialog.Title = "Select File to open...";
					dialog.Multiselect = false;
					if (dialog.ShowDialog(this) == DialogResult.OK) {
						textEditorControl.LoadFile(dialog.FileName);
						textEditorControl.FileName = dialog.FileName;
					}
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
			using (var dialog = new SaveFileDialog()) {
				dialog.Filter = string.Format("{0} Script (*{1})|*{1}|All Files (*.*)|*.*", engine.Name, engine.Extension);
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
			Close();
		}

		private void showResultPanelToolStripButton_Click(object sender, EventArgs e)
		{
			ShowOutput();
		}

		private void ScriptDocument_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing) {
				if (backgroundWorker.IsBusy) {
					MessageBox.Show(this, "Script is executing, please wait...", ProductName);
					e.Cancel = true;
					return;
				}
				e.Cancel = !AskToSaveChanges();
			}
		}

		/// <summary>
		/// If there are changes it asks user to save changes and Save changes.
		/// </summary>
		/// <returns>Returns true if can continue or false if users cancels</returns>
		private bool AskToSaveChanges()
		{
			if (textEditorControl.HasChanges) {
				Activate();
				switch (MessageBox.Show(this, "Save Changes?", "Confirmation", MessageBoxButtons.YesNoCancel)) {
					case DialogResult.Yes:
						Save();
						return !textEditorControl.HasChanges;
					case DialogResult.Cancel:
						return false;
				}
			}
			return true;
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			stopButton.Enabled = false;
			statusLabel.Text = "Aborting...";
			backgroundWorker.AbortAsync();
		}

		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			findDialog.ShowFor(textEditorControl, false);
		}

		private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			findDialog.ShowFor(textEditorControl, true);
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (AskToSaveChanges()) {
				textEditorControl.Visible = false;
				textEditorControl.FileName = null;
				textEditorControl.ResetText();
				textEditorControl.Visible = true;
			}
		}
	}
}

