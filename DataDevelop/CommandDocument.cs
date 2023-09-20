using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DataDevelop
{
	using Data;
	using DataDevelop.Properties;
	using Dialogs;
	using IO;
	using Printing;
	using UIComponents;

	public partial class CommandDocument : Document, IDbObject
	{
		enum CommandType
		{
			Unknown, Query, NonQuery
		}

		private class CommandArgs
		{
			public CommandType CommandType = CommandType.Unknown;
			public bool ExecuteEach = false;
			public string RawCommandText;
			public IDbCommand DbCommand;
		}

		private class CommandResult
		{
			public int RowsAffected;
			public DataTable Data;
		}

		private Database database;
		private Stopwatch stopwatch = new Stopwatch();
		private IList<IDataParameter> parameters = new List<IDataParameter>();
		FindAndReplaceDialog findDialog = new FindAndReplaceDialog();
		private QueryHistoryManager queryHistory;
		private long? lastQueryId = null;

		public CommandDocument(Database database)
		{
			InitializeComponent();
			this.database = database;
			this.databaseStatusLabel.Text = database.Name;
			this.providerStatusLabel.Text = database.Provider.Name;

			//this.command = database.CreateCommand();

			splitContainer.Panel2Collapsed = true;

			textEditorControl.Document.HighlightingStrategy = Highlighters.Sql;

			textEditorControl.ShowEOLMarkers = false;
			textEditorControl.ShowSpaces = false;
			textEditorControl.ShowInvalidLines = false;
			//textEditorControl.ActiveTextAreaControl.AllowDrop = true;

			//this.textEditorControl.HasChangesChanged += delegate { statusLabel.Text = textEditorControl.HasChanges ? "Changes" : "No changes"; };
			messageTextBox.Font = new Font(FontFamily.GenericMonospace, 10F);

			if (MainForm.DarkMode) {
				this.UseImmersiveDarkMode();
				dataGridView.SetDarkMode();
				statusStrip.ForeColor = VisualStyles.DarkThemeColors.TextColor;
				statusStrip.BackColor = VisualStyles.DarkThemeColors.StatusBackColor;

				messageTextBox.ForeColor = VisualStyles.DarkThemeColors.TextColor;
				messageTextBox.BackColor = VisualStyles.DarkThemeColors.Background;
				messageTextBox.BorderStyle = BorderStyle.FixedSingle;
			}
			if (Settings.Default.QueryHistoryEnabled) {
				queryHistory = new QueryHistoryManager(Path.Combine(SettingsManager.DataDirectory, "DataDevelop.db"));
			}
		}

		public string CommandText
		{
			get { return textEditorControl.Text; }
			set { textEditorControl.Text = value; }
		}

		//static Regex regex = new Regex(@"^\s*(--.*\$|/\*.\*/)*\s*(?<query>)\s+.*", RegexOptions.Multiline | RegexOptions.Compiled);
		private static Regex matchSelectCommand = null;

		private static bool IsSelect(string commandText)
		{
			if (commandText == null) {
				return false;
			}

			if (matchSelectCommand == null) {
				matchSelectCommand = new Regex(@"^\s*SELECT\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			}

			var commands = commandText.Split(';');
			var lastCommand = commands[commands.Length - 1].Trim();

			if (lastCommand.Length == 0) {
				if (commands.Length >= 2) {
					lastCommand = commands[commands.Length - 2];
				}
			}

			return matchSelectCommand.IsMatch(lastCommand);
		}

		private void EnableUI(bool value)
		{
			executeButton.Enabled = value;
			abortButton.Enabled = !value;
			abortButton.Visible = !value;
			//useTransactionToolStripButton.Enabled = value;
			textEditorControl.IsReadOnly = !value;
			progressBar.Visible = !value;
			progressBar.Style = progressBar.Visible ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
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

		private void Execute(CommandType commandType)
		{
			this.Execute(commandType, false);
		}

		private void Execute(CommandType commandType, bool executeEach)
		{
			if (executeWorker.IsBusy) {
				MessageBox.Show(this, "A command is already in execution, please wait...", this.ProductName);
			} else {
				try {
					var args = new CommandArgs();
					args.CommandType = commandType;
					args.ExecuteEach = executeEach;

					IDbCommand command = DbCommandParser.Parse(this.database, SelectedText);
					if (command.Parameters.Count > 0) {
						using (ParamsDialog dialog = new ParamsDialog()) {
							IList<IDataParameter> newParameters = MergeParameters(command.Parameters);
							dialog.Parameters = newParameters;
							if (dialog.ShowDialog(this) == DialogResult.OK) {
								this.parameters = newParameters;
								command.Parameters.Clear();
								foreach (IDataParameter p in newParameters) {
									command.Parameters.Add(p);
								}
							} else {
								command.Dispose();
								return;
							}
						}
					}
					command.CommandTimeout = 0;
					args.DbCommand = command;
					args.RawCommandText = this.SelectedText;

					dataGridView.DataSource = null;
					ClearMessages();
					statusLabel.Text = "Executing...";

					lastQueryId = queryHistory?.Insert(database.Name, args.RawCommandText);

					EnableUI(false);
					stopwatch.Reset();
					executingTimer.Start();
					executeWorker.RunWorkerAsync(args);
				} catch (Exception ex) {
					ShowMessage("Exception:");
					AppendMessage(ex.Message);
					executingTimer.Stop();
				}
			}
		}

		private IList<IDataParameter> MergeParameters(IDataParameterCollection newParameters)
		{
			IList<IDataParameter> mergedParameters = new List<IDataParameter>(newParameters.Count);
			foreach (IDataParameter p in newParameters) {
				foreach (IDataParameter oldParam in this.parameters) {
					if (String.Equals(oldParam.ParameterName, p.ParameterName, StringComparison.OrdinalIgnoreCase)) {
						if (p.DbType == DbType.Object) {
							p.DbType = oldParam.DbType;
						}
						p.Value = oldParam.Value;
					}
				}
				mergedParameters.Add(p);
			}
			return mergedParameters;
		}

		private int ExecuteSubcommand(StringBuilder lines)
		{
			var sql = lines.ToString().Trim();
			lines.Length = 0;
			if (sql.Length > 0) {
				IDbCommand command = DbCommandParser.Parse(this.database, sql);
				foreach (IDataParameter p in command.Parameters) {
					foreach (IDataParameter parameter in this.parameters) {
						if (p.ParameterName == parameter.ParameterName) {
							p.DbType = parameter.DbType;
							p.Value = parameter.Value;
							break;
						}
					}
				}
				return command.ExecuteNonQuery();
			}
			return 0;
		}

		private void executeWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try {
				stopwatch.Reset();
				stopwatch.Start();

				database.Connect();
				CommandArgs args = (CommandArgs)e.Argument;
				CommandResult result = new CommandResult();

				var commandType = args.CommandType;
				if (commandType == CommandType.Unknown) {
					if (IsSelect(args.RawCommandText)) {
						commandType = CommandType.Query;
					} else {
						commandType = CommandType.NonQuery;
					}
				}
				if (args.ExecuteEach) {
					var reader = new StringReader(args.RawCommandText);
					var lines = new StringBuilder();
					string line;
					while ((line = reader.ReadLine()) != null) {
						if (line.Trim() == "GO") {
							result.RowsAffected += ExecuteSubcommand(lines);
						} else {
							lines.AppendLine(line);
						}
					}
					if (lines.Length > 0) {
						result.RowsAffected += ExecuteSubcommand(lines);
					}
				} else {
					if (commandType == CommandType.Query) {
						DataSet set = new DataSet("ResultsSet");
						DataTable table = set.Tables.Add("Results");
						set.EnforceConstraints = false;
						using (IDataReader reader = args.DbCommand.ExecuteReader()) {
							table.Load(reader, LoadOption.OverwriteChanges, delegate { });
						}
						result.Data = table;
					} else {
						result.RowsAffected = args.DbCommand.ExecuteNonQuery();
					}
				}
				e.Result = result;
			} finally {
				database.Disconnect();
				stopwatch.Stop();
			}
		}

		private void ShowFullElapsedTime(TimeSpan time)
		{
			int hours = time.Days * 24 + time.Hours;
			elapsedTimeStatusLabel.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", hours, time.Minutes, time.Seconds, time.Milliseconds);
		}

		private void ShowElapsedTime(TimeSpan time)
		{
			int hours = time.Days * 24 + time.Hours;
			elapsedTimeStatusLabel.Text = String.Format("{0:00}:{1:00}:{2:00}", hours, time.Minutes, time.Seconds);
		}

		private void executeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			executingTimer.Stop();
			ShowFullElapsedTime(stopwatch.Elapsed);
			if (e.Error != null) {
				var errorMessage = new StringBuilder();
				var error = e.Error;
				do {
					errorMessage.AppendLine(error.Message);
				} while ((error = error.InnerException) != null);
				if (lastQueryId != null) {
					queryHistory.Update(lastQueryId.Value, stopwatch.Elapsed, "Error", errorMessage.ToString());
				}
				ShowMessage(errorMessage.ToString());
				statusLabel.Text = "Error.";
			} else if (e.Cancelled) { // Aborted
				ShowMessage("Execution was aborted.");
				statusLabel.Text = "Aborted.";
				this.database.Reconnect();
				messagesTabPage.Select();
				if (lastQueryId != null) {
					queryHistory.Update(lastQueryId.Value, stopwatch.Elapsed, "Cancelled");
				}
			} else {
				CommandResult result = e.Result as CommandResult;
				if (result != null) {
					ResultsPanelVisible = true;
					if (result.Data != null) {
						dataGridView.Columns.Clear();
						dataGridView.DataSource = result.Data;
						statusLabel.Text = String.Format("Rows returned: {0}", result.Data.Rows.Count);

					} else {
						ShowMessage(String.Format("Success: {0} rows affected.", result.RowsAffected));
						statusLabel.Text = "Ready.";
						messagesTabPage.Select();
					}
				}
				if (lastQueryId != null) {
					queryHistory.Update(lastQueryId.Value, stopwatch.Elapsed, "Success");
				}
			}
			outputTabControl.SelectTab((dataGridView.DataSource == null) ? messagesTabPage : resultsTabPage);
			AppendMessage(String.Format("Elapsed time: {0}", stopwatch.Elapsed));
			EnableUI(true);
		}

		private bool ResultsPanelVisible
		{
			get { return !splitContainer.Panel2Collapsed; }
			set
			{
				splitContainer.Panel2Collapsed = !value;
				showResultPanelToolStripButton.Checked = value;
			}
		}

		private void ShowMessage(string message)
		{
			messageTextBox.Text = message;
			ResultsPanelVisible = true;
			outputTabControl.SelectedIndex = 1;
			messageTextBox.DeselectAll();
		}

		private void ClearMessages()
		{
			messageTextBox.Text = String.Empty;
		}

		private void AppendMessage(string message)
		{
			messageTextBox.AppendText(Environment.NewLine);
			messageTextBox.AppendText(message);
			ResultsPanelVisible = true;
			messageTextBox.DeselectAll();
		}

		private void showResultPanelToolStripButton_CheckedChanged(object sender, EventArgs e)
		{
			ResultsPanelVisible = showResultPanelToolStripButton.Checked;
		}

		private DataTable DataTable
		{
			get { return dataGridView.DataSource as DataTable; }
		}


		private static void SaveToFile(string fileName, DataTable dataTable, char rowSeparator)
		{
			using (var writer = new CsvWriter(fileName) { Delimiter = rowSeparator }) {
				var headerRow = dataTable.Columns.Cast<DataColumn>().Select(i => i.ColumnName).ToArray();
				writer.WriteRow(headerRow);
				foreach (DataRow row in dataTable.Rows) {
					var rowValues = row.ItemArray.Select(i => (i ?? "").ToString()).ToArray();
					writer.WriteRow(rowValues);
				}
			}
		}

		private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var table = DataTable;
			if (table == null) {
				return;
			}
			if (resultSaveFileDialog.ShowDialog(this) == DialogResult.OK) {
				var fileName = resultSaveFileDialog.FileName;
				var ext = Path.GetExtension(fileName).ToLower();
				switch (ext) {
					case ".txt":
						SaveToFile(fileName, table, '\t');
						break;
					case ".csv":
						SaveToFile(fileName, table, ',');
						break;
					case ".xml":
						if (string.IsNullOrEmpty(table.TableName))
							table.TableName = Path.GetFileNameWithoutExtension(fileName);
						table.WriteXml(fileName);
						break;
					case ".xlsx":
						using (var reader = table.CreateDataReader()) {
							Xlsx.Save(fileName, "Result", reader);
						}
						break;
				}
			}
		}

		private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataTable table = DataTable;
			if (table == null) {
				return;
			}
			using (DataTablePrintDocument doc = new DataTablePrintDocument()) {
				doc.DataTable = table;
				using (PrintPreviewDialog dialog = new PrintPreviewDialog()) {
					Form parent = dialog.PrintPreviewControl.Parent as Form;
					parent.WindowState = FormWindowState.Maximized;
					dialog.Document = doc;
					dialog.ShowDialog(this);
				}
			}
		}

		public Database Database
		{
			get { return database; }
		}

		private void textEditorControl_DragDrop(object sender, DragEventArgs e)
		{
			try {
				string[] obj = e.Data.GetData(DataFormats.FileDrop) as string[];
				if (obj != null) {
					string fileName = obj[0];
					this.Activate();
					textEditorControl.LoadFile(fileName);
				}
			} catch (Exception ex) {
				MessageBox.Show("Error in DragDrop function: " + ex.Message);
			}
		}

		private void textEditorControl_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.All;
			} else {
				e.Effect = DragDropEffects.None;
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (AskToSaveChanges()) {
				if (openFileDialog.ShowDialog(this) == DialogResult.OK) {
					textEditorControl.LoadFile(openFileDialog.FileName);
				}
			}
		}

		private void SaveAs()
		{
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK) {
				textEditorControl.SaveFile(saveFileDialog.FileName);
			}
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveAs();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Save();
		}

		private void Save()
		{
			if (textEditorControl.FileName != null) {
				textEditorControl.SaveFile(textEditorControl.FileName);
			} else {
				SaveAs();
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void executeButton_Click(object sender, EventArgs e)
		{
			this.Execute(CommandType.Unknown);
		}

		private void executeQueryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Execute(CommandType.Query);
		}

		private void executeNonQueryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Execute(CommandType.NonQuery);
		}

		private void newToolStripButton_Click(object sender, EventArgs e)
		{
			if (AskToSaveChanges()) {
				textEditorControl.Visible = false;
				textEditorControl.FileName = null;
				textEditorControl.ResetText();
				textEditorControl.Visible = true;
			}
		}

		private void printToolStripButton_Click(object sender, EventArgs e)
		{
			printPreviewDialog.Document = textEditorControl.PrintDocument;
			printPreviewDialog.ShowDialog(this);
		}

		private void cutToolStripButton_Click(object sender, EventArgs e)
		{
			new ICSharpCode.TextEditor.Actions.Cut().Execute(textEditorControl.ActiveTextAreaControl.TextArea);
		}

		private void copyToolStripButton_Click(object sender, EventArgs e)
		{
			new ICSharpCode.TextEditor.Actions.Copy().Execute(textEditorControl.ActiveTextAreaControl.TextArea);
		}

		private void pasteToolStripButton_Click(object sender, EventArgs e)
		{
			new ICSharpCode.TextEditor.Actions.Paste().Execute(textEditorControl.ActiveTextAreaControl.TextArea);
		}

		private void Undo(object sender, EventArgs e)
		{
			textEditorControl.Undo();
		}

		private void Redo(object sender, EventArgs e)
		{
			textEditorControl.Redo();
		}

		private void CommandDocument_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing) {
				if (this.executeWorker.IsBusy) {
					MessageBox.Show(this, "Command is executing, please wait...", this.ProductName);
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
				this.Activate();
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

		private void CommandDocument_Load(object sender, EventArgs e)
		{
			this.textEditorControl.HasChanges = false;
		}

		private void RemoveAllTabs(object sender, EventArgs e)
		{
			var removed = 0;
			var text = textEditorControl.Text;
			var doc = textEditorControl.ActiveTextAreaControl.Document;
			try {
				doc.UndoStack.StartUndoGroup();

				for (int i = 0; i < text.Length; i++) {
					if (text[i] == '\t') {
						doc.Remove(i - removed, 1);
						removed++;
					}
				}
			} finally {
				doc.UndoStack.EndUndoGroup();
			}
			MessageBox.Show(this, String.Format("{0} tabs removed.", removed), Application.ProductName, MessageBoxButtons.OK);
		}

		private void Find(object sender, EventArgs e)
		{
			FormExtensions.PositionDown(this.findDialog, this.findToolStripButton, this);
			this.findDialog.ShowFor(this.textEditorControl, false);
		}

		private void Replace(object sender, EventArgs e)
		{
			FormExtensions.PositionDown(this.findDialog, this.findToolStripButton, this);
			this.findDialog.ShowFor(this.textEditorControl, true);
		}

		private void ExecuteEachStatement(object sender, EventArgs e)
		{
			this.Execute(CommandType.NonQuery, true);
		}

		private void exportToToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProgressDialog.Run(this, "Export to Excel", excelWorker, true);
		}

		private void excelWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var data = this.DataTable;
			if (data != null) {
				var sheet = ExcelInterop.CreateWorksheet("", data, excelWorker);
				if (sheet != null) {
					excelWorker.ReportProgress(100, "Loading Excel...");
					sheet.OpenInExcel();
				} else {
					e.Cancel = true;
				}
			}
		}

		private void executingTimer_Tick(object sender, EventArgs e)
		{
			ShowElapsedTime(stopwatch.Elapsed);
		}

		private void abortButton_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show(this, "Aborting may cause losing of data and disconnection from database." + "\r\n"
				+ "Are you sure you want to abort execution?", "Confirmation",
				MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
			if (result == DialogResult.Yes) {
				Abort();
			}
		}

		private void Abort()
		{
			if (this.executeWorker.IsBusy) {
				this.abortButton.Enabled = false;
				this.statusLabel.Text = "Aborting...";
				this.executeWorker.AbortAsync();
			}
		}

		private void textEditorControl_FileNameChanged(object sender, EventArgs e)
		{
			if (textEditorControl.FileName == null) {
				fileNameStatusLabel.ToolTipText = null;
				fileNameStatusLabel.Text = "Unsaved*";
			} else {
				fileNameStatusLabel.ToolTipText = textEditorControl.FileName;
				fileNameStatusLabel.Text = Path.GetFileName(textEditorControl.FileName);
			}
		}

		class ExecuteToXlsxArgs
		{
			public IDbCommand Command { get; set; }
			public string FileName { get; set; }
		}

		private void ExecuteAndSaveToXlsxToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var command = DbCommandParser.Parse(this.database, SelectedText);
			if (command.Parameters.Count > 0) {
				using (ParamsDialog dialog = new ParamsDialog()) {
					IList<IDataParameter> newParameters = MergeParameters(command.Parameters);
					dialog.Parameters = newParameters;
					if (dialog.ShowDialog(this) == DialogResult.OK) {
						this.parameters = newParameters;
						command.Parameters.Clear();
						foreach (IDataParameter p in newParameters) {
							command.Parameters.Add(p);
						}
					} else {
						command.Dispose();
						return;
					}
				}
			}

			using (var xlsxDialog = new SaveFileDialog()) {
				xlsxDialog.Filter = "Excel Spreadsheet Document (*.xlsx)|*.xlsx";
				if (xlsxDialog.ShowDialog(this) == DialogResult.OK) {
					var args = new ExecuteToXlsxArgs() {
						Command = command,
						FileName = xlsxDialog.FileName
					};
					ProgressDialog.Run(this, "Executing...", executeToXlsxWorker, true, args);
				}
			}
		}

		private void executeToXlsxWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var args = (ExecuteToXlsxArgs)e.Argument;
			using (var command = args.Command) {
				using (var reader = command.ExecuteReader()) {
					Xlsx.Save(args.FileName, "Result", reader);
				}
			}
		}

		private void executeToXlsxWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{

		}
	}
}
