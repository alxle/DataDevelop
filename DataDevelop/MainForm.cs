using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DataDevelop.Scripting;
using WeifenLuo.WinFormsUI.Docking;

namespace DataDevelop
{
	public partial class MainForm : Form
	{
		private AssemblyExplorer assemblyExplorer;
		private DatabaseExplorer databaseExplorer;
		private PropertiesToolbox propertiesToolbox;
		private OutputWindow outputWindow;
		private static MainForm instance;

		private MainForm()
		{
			InitializeComponent();
			this.ApplyVisualStyle(Properties.Settings.Default.VisualStyle);
			this.databaseExplorer = new DatabaseExplorer();
			this.assemblyExplorer = new AssemblyExplorer();
			this.propertiesToolbox = new PropertiesToolbox();
			this.outputWindow = new OutputWindow();
			this.databaseExplorer.ShowProperties += ShowProperties;
			this.assemblyExplorer.ShowProperties += ShowProperties;
			this.checkForUpdatesToolStripMenuItem.Enabled = ApplicationDeployment.IsNetworkDeployed;
		}

		public void ApplyVisualStyle(string visualStyle)
		{
			if (visualStyle == "VS2005") {
				this.dockPanel.Theme = new VS2005Theme();
				this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
				this.dockPanel.Skin.DockPaneStripSkin.DocumentGradient.DockStripGradient.StartColor
					= Color.FromArgb(
					(SystemColors.Control.R + SystemColors.ControlDark.R) / 2,
					(SystemColors.Control.G + SystemColors.ControlDark.G) / 2,
					(SystemColors.Control.B + SystemColors.ControlDark.B) / 2);
				topBar.Visible = bottomBar.Visible = false;
				topBar.BackColor = bottomBar.BackColor = SystemColors.Control;
				ToolStripManager.Renderer = VisualStyles.SystemToolStripRenderers.ToolStripSquaredEdgesRenderer;
				ToolStripManager.VisualStylesEnabled = false;
			} else if (visualStyle == "VS2003") {
				this.dockPanel.Theme = new VS2003Theme();
				topBar.Visible = bottomBar.Visible = false;
				topBar.BackColor = bottomBar.BackColor = SystemColors.Control;
				ToolStripManager.Renderer = VisualStyles.SystemToolStripRenderers.ToolStripSquaredEdgesRenderer;
				ToolStripManager.VisualStylesEnabled = false;
			} else if (visualStyle == "VS2012 Light") {
				this.dockPanel.Theme = new VS2012LightTheme();
				topBar.Visible = bottomBar.Visible = false;
				topBar.BackColor = bottomBar.BackColor = SystemColors.Control;
				ToolStripManager.VisualStylesEnabled = true;
				ToolStripManager.Renderer = new VisualStyles.VS2012ToolStripRenderer();
			} else {
				this.dockPanel.Theme = new VS2015BlueTheme();
				var darkBlue = Color.FromArgb(0xFF, 41, 57, 85);
				dockPanel.BackColor = topBar.BackColor = bottomBar.BackColor = darkBlue;
				dockPanel.Skin.AutoHideStripSkin.DockStripBackground.StartColor = darkBlue;
				dockPanel.Skin.AutoHideStripSkin.DockStripBackground.EndColor = darkBlue;
				dockPanel.Skin.AutoHideStripSkin.DockStripGradient.StartColor = darkBlue;
				dockPanel.Skin.AutoHideStripSkin.DockStripGradient.EndColor = darkBlue;
				topBar.Visible = bottomBar.Visible = true;
				topBar.BackColor = bottomBar.BackColor = darkBlue;
				ToolStripManager.VisualStylesEnabled = true;
				ToolStripManager.Renderer = new VisualStyles.VS2015ToolStripRenderer();
			} 
		}

		public static MainForm Instance
		{
			get { return instance ?? (instance = new MainForm()); }
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.DockPropertiesFileName)) {
				try {
					dockPanel.LoadFromXml(SettingsManager.DockPropertiesFileName, GetContentFromPersistString);
				} catch {
					ShowDefaultToolboxes();
				}
			} else {
				ShowDefaultToolboxes();
			}
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (CloseAllDocuments()) {
				databaseExplorer.SaveDatabases();
				dockPanel.SaveAsXml(SettingsManager.DockPropertiesFileName);
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

		public void ShowToolbox(Toolbox toolbox, DockState dockState)
		{
			if (toolbox.DockPanel == null) {
				toolbox.Show(this.dockPanel, dockState);
			} else {
				toolbox.Show();
			}
		}

		private IDockContent GetContentFromPersistString(string name)
		{
			if (name == typeof(AssemblyExplorer).ToString()) {
				return assemblyExplorer;
			}
			if (name == typeof(DatabaseExplorer).ToString()) {
				return databaseExplorer;
			}
			if (name == typeof(PropertiesToolbox).ToString()) {
				return propertiesToolbox;
			}
			if (name == typeof(OutputWindow).ToString()) {
				return outputWindow;
			}
			return null;
		}

		private Document[] GetDocuments()
		{
			var documents = new List<Document>(dockPanel.DocumentsCount);
			foreach (object obj in dockPanel.Contents) {
				Document doc = obj as Document;
				if (doc != null) {
					documents.Add(doc);
				}
			}
			return documents.ToArray();
		}

		private void CloseAllButCurrent()
		{
			var current = dockPanel.ActiveDocument as Document;
			if (current != null) {
				foreach (var document in this.GetDocuments()) {
					if (document != current) {
						document.Close();
					}
				}
			}
		}

		private bool CloseAllDocuments()
		{
			bool allClosed = true;
			for (int i = 0; i < dockPanel.Contents.Count;) {
				Document doc = dockPanel.Contents[i] as Document;
				if (doc != null) {
					if (!FormExtensions.Close(doc)) {
						allClosed = false;
						i++;
					}
				} else {
					i++;
				}
			}
			return allClosed;
		}

		private void databaseExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowToolbox(databaseExplorer, DockState.DockLeft);
		}

		private void propertiesWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowToolbox(propertiesToolbox, DockState.DockRight);
		}

		private void assemblyExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowToolbox(assemblyExplorer, DockState.DockLeft);
		}

		private void outputWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowToolbox(outputWindow, DockState.DockBottomAutoHide);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CloseAllDocuments()) {
				databaseExplorer.SaveDatabases();
				dockPanel.SaveAsXml(SettingsManager.DockPropertiesFileName);
				Application.Exit();
			}
		}

		private void aboutSQLiteStudioToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (AboutBox aboutBox = new AboutBox()) {
				aboutBox.ShowDialog(this);
			}
		}

		private void newDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!databaseExplorer.Visible) {
				ShowToolbox(databaseExplorer, DockState.DockLeft);
			}
			databaseExplorer.NewDatabase();
		}

		private void openDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!databaseExplorer.Visible) {
				ShowToolbox(databaseExplorer, DockState.DockLeft);
			}
			databaseExplorer.OpenDatabase();
		}

        private void pythonConsole_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            ScriptDocument script = new ScriptDocument(outputWindow, new PythonScriptEngine());
            script.Show(dockPanel);
            Application.DoEvents();
        }

		private void javascriptConsole_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            ScriptDocument script = new ScriptDocument(outputWindow, new JavascriptEngine());
            script.Show(dockPanel);
            Application.DoEvents();
		}

		private void updates_ApplicationRestarting(object sender, CancelEventArgs e)
		{
			e.Cancel = !CloseAllDocuments() || !databaseExplorer.DisconnectAll();
		}

		private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (PreferencesForm preferences = new PreferencesForm()) {
				preferences.ShowDialog(this);
			}
		}

		private void closeAllDocumentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseAllDocuments();
		}

		private void resetWindowLayoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowDefaultToolboxes();
		}

		private void closeAllButCurrentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseAllButCurrent();
		}
	}
}