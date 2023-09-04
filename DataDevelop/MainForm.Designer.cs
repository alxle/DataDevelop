namespace DataDevelop
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.databaseExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.assemblyExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.pythonConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.javascriptConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.outputWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeAllDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeAllButCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetWindowLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fileToolStripMenuItem,
			this.viewToolStripMenuItem,
			this.windowToolStripMenuItem,
			this.helpToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.MdiWindowListItem = this.windowToolStripMenuItem;
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(604, 24);
			this.menuStrip.TabIndex = 1;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.newDatabaseToolStripMenuItem,
			this.openDatabaseToolStripMenuItem,
			this.toolStripSeparator2,
			this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newDatabaseToolStripMenuItem
			// 
			this.newDatabaseToolStripMenuItem.Name = "newDatabaseToolStripMenuItem";
			this.newDatabaseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.newDatabaseToolStripMenuItem.Text = "&New Database";
			this.newDatabaseToolStripMenuItem.Click += new System.EventHandler(this.NewDatabaseToolStripMenuItem_Click);
			// 
			// openDatabaseToolStripMenuItem
			// 
			this.openDatabaseToolStripMenuItem.Name = "openDatabaseToolStripMenuItem";
			this.openDatabaseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.openDatabaseToolStripMenuItem.Text = "&Open Database";
			this.openDatabaseToolStripMenuItem.Click += new System.EventHandler(this.OpenDatabaseToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.databaseExplorerToolStripMenuItem,
			this.propertiesWindowToolStripMenuItem,
			this.assemblyExplorerToolStripMenuItem,
			this.outputWindowToolStripMenuItem,
			this.toolStripSeparator1,
			this.pythonConsoleToolStripMenuItem,
			this.javascriptConsoleToolStripMenuItem,
			this.toolStripSeparator5,
			this.preferencesToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "&View";
			// 
			// databaseExplorerToolStripMenuItem
			// 
			this.databaseExplorerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("databaseExplorerToolStripMenuItem.Image")));
			this.databaseExplorerToolStripMenuItem.Name = "databaseExplorerToolStripMenuItem";
			this.databaseExplorerToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.databaseExplorerToolStripMenuItem.Text = "&Database Explorer";
			this.databaseExplorerToolStripMenuItem.Click += new System.EventHandler(this.DatabaseExplorerToolStripMenuItem_Click);
			// 
			// propertiesWindowToolStripMenuItem
			// 
			this.propertiesWindowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("propertiesWindowToolStripMenuItem.Image")));
			this.propertiesWindowToolStripMenuItem.Name = "propertiesWindowToolStripMenuItem";
			this.propertiesWindowToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.propertiesWindowToolStripMenuItem.Text = "&Properties Window";
			this.propertiesWindowToolStripMenuItem.Click += new System.EventHandler(this.PropertiesWindowToolStripMenuItem_Click);
			// 
			// assemblyExplorerToolStripMenuItem
			// 
			this.assemblyExplorerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("assemblyExplorerToolStripMenuItem.Image")));
			this.assemblyExplorerToolStripMenuItem.Name = "assemblyExplorerToolStripMenuItem";
			this.assemblyExplorerToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.assemblyExplorerToolStripMenuItem.Text = "Assembly Explorer";
			this.assemblyExplorerToolStripMenuItem.Click += new System.EventHandler(this.AssemblyExplorerToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(201, 6);
			// 
			// pythonConsoleToolStripMenuItem
			// 
			this.pythonConsoleToolStripMenuItem.Name = "pythonConsoleToolStripMenuItem";
			this.pythonConsoleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
			this.pythonConsoleToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.pythonConsoleToolStripMenuItem.Text = "Python Console";
			this.pythonConsoleToolStripMenuItem.Click += new System.EventHandler(this.PythonConsole_Click);
			// 
			// javascriptConsoleToolStripMenuItem
			// 
			this.javascriptConsoleToolStripMenuItem.Name = "javascriptConsoleToolStripMenuItem";
			this.javascriptConsoleToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.javascriptConsoleToolStripMenuItem.Text = "Javascript Console";
			this.javascriptConsoleToolStripMenuItem.Click += new System.EventHandler(this.JavascriptConsole_Click);
			// 
			// outputWindowToolStripMenuItem
			// 
			this.outputWindowToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Output;
			this.outputWindowToolStripMenuItem.Name = "outputWindowToolStripMenuItem";
			this.outputWindowToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.outputWindowToolStripMenuItem.Text = "Output Window";
			this.outputWindowToolStripMenuItem.Click += new System.EventHandler(this.OutputWindowToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(201, 6);
			// 
			// preferencesToolStripMenuItem
			// 
			this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
			this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.preferencesToolStripMenuItem.Text = "&Preferences...";
			this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.PreferencesToolStripMenuItem_Click);
			// 
			// windowToolStripMenuItem
			// 
			this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.closeAllDocumentToolStripMenuItem,
			this.closeAllButCurrentToolStripMenuItem,
			this.resetWindowLayoutToolStripMenuItem,
			this.toolStripSeparator6});
			this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
			this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
			this.windowToolStripMenuItem.Text = "&Window";
			// 
			// closeAllDocumentToolStripMenuItem
			// 
			this.closeAllDocumentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("closeAllDocumentToolStripMenuItem.Image")));
			this.closeAllDocumentToolStripMenuItem.Name = "closeAllDocumentToolStripMenuItem";
			this.closeAllDocumentToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.closeAllDocumentToolStripMenuItem.Text = "C&lose All Documents";
			this.closeAllDocumentToolStripMenuItem.Click += new System.EventHandler(this.CloseAllDocumentToolStripMenuItem_Click);
			// 
			// closeAllButCurrentToolStripMenuItem
			// 
			this.closeAllButCurrentToolStripMenuItem.Name = "closeAllButCurrentToolStripMenuItem";
			this.closeAllButCurrentToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.closeAllButCurrentToolStripMenuItem.Text = "Close All &But Current";
			this.closeAllButCurrentToolStripMenuItem.Click += new System.EventHandler(this.CloseAllButCurrentToolStripMenuItem_Click);
			// 
			// resetWindowLayoutToolStripMenuItem
			// 
			this.resetWindowLayoutToolStripMenuItem.Name = "resetWindowLayoutToolStripMenuItem";
			this.resetWindowLayoutToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			this.resetWindowLayoutToolStripMenuItem.Text = "&Reset Window Layout";
			this.resetWindowLayoutToolStripMenuItem.Click += new System.EventHandler(this.ResetWindowLayoutToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(185, 6);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.documentationToolStripMenuItem,
			this.checkForUpdatesToolStripMenuItem,
			this.toolStripSeparator3,
			this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// documentationToolStripMenuItem
			// 
			this.documentationToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Web;
			this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
			this.documentationToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.documentationToolStripMenuItem.Text = "&Documentation";
			this.documentationToolStripMenuItem.Click += new System.EventHandler(this.DocumentationToolStripMenuItem_Click);
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.Enabled = false;
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.checkForUpdatesToolStripMenuItem.Text = "Check for updates...";
			this.checkForUpdatesToolStripMenuItem.Visible = false;
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(176, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
			this.aboutToolStripMenuItem.Text = "&About DataDevelop";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
			// 
			// dockPanel
			// 
			this.dockPanel.BackColor = System.Drawing.SystemColors.Control;
			this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel.DockBackColor = System.Drawing.SystemColors.AppWorkspace;
			this.dockPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dockPanel.Location = new System.Drawing.Point(0, 24);
			this.dockPanel.Name = "dockPanel";
			this.dockPanel.Size = new System.Drawing.Size(604, 377);
			this.dockPanel.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(604, 401);
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.menuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "DataDevelop";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem databaseExplorerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem propertiesWindowToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newDatabaseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openDatabaseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pythonConsoleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem assemblyExplorerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem outputWindowToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeAllDocumentToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetWindowLayoutToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem javascriptConsoleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeAllButCurrentToolStripMenuItem;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
	}
}

