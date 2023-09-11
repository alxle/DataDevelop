using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using DataDevelop.Scripting;
using DataDevelop.Properties;

namespace DataDevelop
{
	public partial class MainForm : Form
	{
		private static MainForm instance;
		private static bool darkMode;
		private readonly AssemblyExplorer assemblyExplorer;
		private readonly DatabaseExplorer databaseExplorer;
		private readonly PropertiesToolbox propertiesToolbox;
		private readonly OutputWindow outputWindow;

		private MainForm()
		{
			InitializeComponent();
			ApplyVisualStyle(Settings.Default.VisualStyle);
			databaseExplorer = new DatabaseExplorer();
			assemblyExplorer = new AssemblyExplorer();
			propertiesToolbox = new PropertiesToolbox();
			outputWindow = new OutputWindow();
			databaseExplorer.ShowProperties += ShowProperties;
			assemblyExplorer.ShowProperties += ShowProperties;
		}

		public static MainForm Instance => instance ?? (instance = new MainForm());
		
		public static bool DarkMode => darkMode;

		private void ApplyVisualStyle(string visualStyle)
		{
			if (visualStyle == "Classic") {
				var theme = new VS2005Theme();
				dockPanel.Theme = theme;
				theme.ApplyTo(dockPanel);
				ToolStripManager.Renderer = VisualStyles.SystemToolStripRenderers.ToolStripSquaredEdgesRenderer;
				ToolStripManager.VisualStylesEnabled = false;
			} else if (visualStyle == "Light") {
				var theme = new VS2015LightTheme();
				dockPanel.Theme = theme;
				theme.ApplyTo(dockPanel);
				ToolStripManager.VisualStylesEnabled = true;
				ToolStripManager.Renderer = new VisualStyles.VS2012ToolStripRenderer();
			} else if (visualStyle == "Blue") {
				var theme = new VS2015BlueTheme();
				dockPanel.Theme = theme;
				theme.ApplyTo(dockPanel);
				ToolStripManager.VisualStylesEnabled = true;
				ToolStripManager.Renderer = new VisualStyles.VS2015ToolStripRenderer();
			} else if (visualStyle == "Dark") {
				var theme = new VS2015BlueTheme();
				dockPanel.Theme = theme;
				theme.ApplyTo(dockPanel);
				ToolStripManager.VisualStylesEnabled = true;
				ToolStripManager.Renderer = new VisualStyles.DarkToolStripRenderer();
				darkMode = true;
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Size = Settings.Default.MainFormSize;
			Location = Settings.Default.MainFormLocation;
			WindowState = Settings.Default.MainFormState;

			if (File.Exists(SettingsManager.DockPropertiesFileName)) {
				try {
					dockPanel.LoadFromXml(SettingsManager.DockPropertiesFileName,
						delegate (string persistString)
						{
							if (persistString == typeof(AssemblyExplorer).ToString())
								return assemblyExplorer;
							if (persistString == typeof(DatabaseExplorer).ToString())
								return databaseExplorer;
							if (persistString == typeof(PropertiesToolbox).ToString())
								return propertiesToolbox;
							if (persistString == typeof(OutputWindow).ToString())
								return outputWindow;
							return null;
						});
				} catch (Exception ex) {
					LogManager.LogError("Loading DockingProperties.xml", ex);
					ShowDefaultToolboxes();
				}
			} else {
				ShowDefaultToolboxes();
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (CloseAllDocuments()) {
				SettingsManager.SaveDatabases();
				dockPanel.SaveAsXml(SettingsManager.DockPropertiesFileName);
				Settings.Default.MainFormState = WindowState;
				if (WindowState == FormWindowState.Normal) {
					Settings.Default.MainFormSize = Size;
					Settings.Default.MainFormLocation = Location;
				} else {
					Settings.Default.MainFormSize = RestoreBounds.Size;
					Settings.Default.MainFormLocation = RestoreBounds.Location;
				}
				Settings.Default.Save();
			} else {
				e.Cancel = true;
			}
		}

		private void ShowProperties(object sender, OpenPropertiesEventArgs args)
		{
			propertiesToolbox.PropertyGrid.SelectedObject = args.Object;
			if (args.Focus) {
				propertiesToolbox.Activate();
			}
		}

		private void ShowDefaultToolboxes()
		{
			ShowToolbox(databaseExplorer, DockState.DockLeft);
			databaseExplorer.Show();
			propertiesToolbox.Show(databaseExplorer.Pane, DockAlignment.Bottom, 0.25);
			ShowToolbox(outputWindow, DockState.DockBottomAutoHide);
		}

		private void ShowToolbox(Toolbox toolbox, DockState dockState)
		{
			if (toolbox.DockPanel == null) {
				toolbox.Show(dockPanel, dockState);
			} else {
				toolbox.Show();
			}
		}

		private Document[] GetDocuments()
		{
			var documents = new List<Document>(dockPanel.DocumentsCount);
			foreach (object obj in dockPanel.Contents) {
				if (obj is Document doc) {
					documents.Add(doc);
				}
			}
			return documents.ToArray();
		}

		private void CloseAllButCurrent()
		{
			if (dockPanel.ActiveDocument is Document current) {
				foreach (var document in GetDocuments()) {
					if (document != current) {
						document.Close();
					}
				}
			}
		}

		private bool CloseAllDocuments()
		{
			var allClosed = true;
			for (var i = 0; i < dockPanel.Contents.Count;) {
				if (dockPanel.Contents[i] is Document doc) {
					if (!doc.TryClose()) {
						allClosed = false;
						i++;
					}
				} else {
					i++;
				}
			}
			return allClosed;
		}

		private void DatabaseExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowToolbox(databaseExplorer, DockState.DockLeft);
		}

		private void PropertiesWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowToolbox(propertiesToolbox, DockState.DockRight);
		}

		private void AssemblyExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowToolbox(assemblyExplorer, DockState.DockLeft);
		}

		private void OutputWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowToolbox(outputWindow, DockState.DockBottomAutoHide);
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.TryClose()) {
				Application.Exit();
			}
		}

		private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var aboutBox = new AboutBox()) {
				aboutBox.ShowDialog(this);
			}
		}

		private void NewDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!databaseExplorer.Visible) {
				ShowToolbox(databaseExplorer, DockState.DockLeft);
			}
			databaseExplorer.NewDatabase();
		}

		private void OpenDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!databaseExplorer.Visible) {
				ShowToolbox(databaseExplorer, DockState.DockLeft);
			}
			databaseExplorer.OpenDatabase();
		}

		private void PythonConsole_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			var script = new ScriptDocument(outputWindow, new PythonScriptEngine());
			script.Show(dockPanel);
			Application.DoEvents();
		}

		private void JavascriptConsole_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			var script = new ScriptDocument(outputWindow, new JavascriptEngine());
			script.Show(dockPanel);
			Application.DoEvents();
		}

		private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var preferences = new PreferencesForm()) {
				preferences.ShowDialog(this);
			}
		}

		private void CloseAllDocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseAllDocuments();
		}

		private void ResetWindowLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowDefaultToolboxes();
		}

		private void CloseAllButCurrentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseAllButCurrent();
		}

		private void DocumentationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start(Program.Documentation);
		}
	}
}
