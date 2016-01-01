namespace DataDevelop
{
	partial class DatabaseExplorer
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseExplorer));
			this.toolStrip = new DataDevelop.UIComponents.ToolStrip();
			this.createDatabaseToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.addDatabaseToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.actionsButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.disconnectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sortDatabasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.newFolderToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.treeView = new DataDevelop.UIComponents.TreeView();
			this.nodesImageList = new System.Windows.Forms.ImageList(this.components);
			this.databaseContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.modifyConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeDatabaseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tableContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openTableDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openDataWithFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scriptAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.joinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.alterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.procedureContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.scriptAsExecuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scriptAsCreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scriptAsAlterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scriptAsDropToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.foreignKeyMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.joinQueryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip.SuspendLayout();
			this.databaseContextMenu.SuspendLayout();
			this.tableContextMenu.SuspendLayout();
			this.procedureContextMenu.SuspendLayout();
			this.foreignKeyMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDatabaseToolStripButton,
            this.addDatabaseToolStripButton,
            this.toolStripSeparator2,
            this.refreshToolStripButton,
            this.toolStripSeparator3,
            this.actionsButton});
			this.toolStrip.Location = new System.Drawing.Point(1, 1);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Padding = new System.Windows.Forms.Padding(4, 0, 1, 0);
			this.toolStrip.Size = new System.Drawing.Size(234, 25);
			this.toolStrip.TabIndex = 0;
			// 
			// createDatabaseToolStripButton
			// 
			this.createDatabaseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.createDatabaseToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("createDatabaseToolStripButton.Image")));
			this.createDatabaseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.createDatabaseToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.createDatabaseToolStripButton.Name = "createDatabaseToolStripButton";
			this.createDatabaseToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.createDatabaseToolStripButton.Text = "Create Database";
			this.createDatabaseToolStripButton.Click += new System.EventHandler(this.createDatabaseToolStripButton_Click);
			// 
			// addDatabaseToolStripButton
			// 
			this.addDatabaseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addDatabaseToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("addDatabaseToolStripButton.Image")));
			this.addDatabaseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addDatabaseToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.addDatabaseToolStripButton.Name = "addDatabaseToolStripButton";
			this.addDatabaseToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.addDatabaseToolStripButton.Text = "Add Existing Database";
			this.addDatabaseToolStripButton.Click += new System.EventHandler(this.addDatabaseToolStripButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// refreshToolStripButton
			// 
			this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.refreshToolStripButton.Enabled = false;
			this.refreshToolStripButton.Image = global::DataDevelop.Properties.Resources.Refresh;
			this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.refreshToolStripButton.Name = "refreshToolStripButton";
			this.refreshToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.refreshToolStripButton.Text = "Refresh";
			this.refreshToolStripButton.Click += new System.EventHandler(this.refreshToolStripButton_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// actionsButton
			// 
			this.actionsButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disconnectAllToolStripMenuItem,
            this.saveConnectionsToolStripMenuItem,
            this.loadConnectionsToolStripMenuItem,
            this.sortDatabasesToolStripMenuItem});
			this.actionsButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.actionsButton.Name = "actionsButton";
			this.actionsButton.Size = new System.Drawing.Size(60, 22);
			this.actionsButton.Text = "Actions";
			// 
			// disconnectAllToolStripMenuItem
			// 
			this.disconnectAllToolStripMenuItem.Name = "disconnectAllToolStripMenuItem";
			this.disconnectAllToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.disconnectAllToolStripMenuItem.Text = "&Disconnect all";
			this.disconnectAllToolStripMenuItem.Click += new System.EventHandler(this.disconnectAllToolStripMenuItem_Click);
			// 
			// saveConnectionsToolStripMenuItem
			// 
			this.saveConnectionsToolStripMenuItem.Name = "saveConnectionsToolStripMenuItem";
			this.saveConnectionsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.saveConnectionsToolStripMenuItem.Text = "&Save connections";
			this.saveConnectionsToolStripMenuItem.Click += new System.EventHandler(this.saveConnectionsToolStripMenuItem_Click);
			// 
			// loadConnectionsToolStripMenuItem
			// 
			this.loadConnectionsToolStripMenuItem.Name = "loadConnectionsToolStripMenuItem";
			this.loadConnectionsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.loadConnectionsToolStripMenuItem.Text = "&Load connections";
			this.loadConnectionsToolStripMenuItem.Click += new System.EventHandler(this.loadConnectionsToolStripMenuItem_Click);
			// 
			// sortDatabasesToolStripMenuItem
			// 
			this.sortDatabasesToolStripMenuItem.Name = "sortDatabasesToolStripMenuItem";
			this.sortDatabasesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.sortDatabasesToolStripMenuItem.Text = "S&ort Databases";
			this.sortDatabasesToolStripMenuItem.Click += new System.EventHandler(this.SortDatabases);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 23);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// newFolderToolStripButton
			// 
			this.newFolderToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newFolderToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newFolderToolStripButton.Image")));
			this.newFolderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newFolderToolStripButton.Name = "newFolderToolStripButton";
			this.newFolderToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newFolderToolStripButton.Text = "New Folder";
			// 
			// treeView
			// 
			this.treeView.AllowDrop = true;
			this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.ImageIndex = 0;
			this.treeView.ImageList = this.nodesImageList;
			this.treeView.LoadOnDemand = true;
			this.treeView.Location = new System.Drawing.Point(1, 26);
			this.treeView.Margin = new System.Windows.Forms.Padding(0);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = 0;
			this.treeView.Size = new System.Drawing.Size(234, 239);
			this.treeView.TabIndex = 2;
			this.treeView.TreeNodePopulate += new System.Windows.Forms.TreeViewEventHandler(this.treeView_TreeNodePopulate);
			this.treeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_BeforeLabelEdit);
			this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
			// 
			// nodesImageList
			// 
			this.nodesImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("nodesImageList.ImageStream")));
			this.nodesImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.nodesImageList.Images.SetKeyName(0, "folder");
			this.nodesImageList.Images.SetKeyName(1, "db");
			this.nodesImageList.Images.SetKeyName(2, "db2");
			this.nodesImageList.Images.SetKeyName(3, "table");
			this.nodesImageList.Images.SetKeyName(4, "column");
			this.nodesImageList.Images.SetKeyName(5, "primaryKey");
			this.nodesImageList.Images.SetKeyName(6, "view");
			this.nodesImageList.Images.SetKeyName(7, "trigger");
			this.nodesImageList.Images.SetKeyName(8, "storedProcedure");
			this.nodesImageList.Images.SetKeyName(9, "foreingKey");
			this.nodesImageList.Images.SetKeyName(10, "function");
			// 
			// databaseContextMenu
			// 
			this.databaseContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newQueryToolStripMenuItem,
            this.connectToolStripMenuItem,
            this.reconnectToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.modifyConnectionToolStripMenuItem,
            this.removeDatabaseMenuItem,
            this.toolStripSeparator5,
            this.propertiesToolStripMenuItem});
			this.databaseContextMenu.Name = "dbContextMenu";
			this.databaseContextMenu.Size = new System.Drawing.Size(178, 164);
			this.databaseContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.databaseContextMenu_Opening);
			this.databaseContextMenu.Opened += new System.EventHandler(this.databaseContextMenu_Opened);
			// 
			// newQueryToolStripMenuItem
			// 
			this.newQueryToolStripMenuItem.Name = "newQueryToolStripMenuItem";
			this.newQueryToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.newQueryToolStripMenuItem.Text = "New Query";
			this.newQueryToolStripMenuItem.Click += new System.EventHandler(this.newQueryToolStripMenuItem_Click);
			// 
			// connectToolStripMenuItem
			// 
			this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
			this.connectToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.connectToolStripMenuItem.Text = "Connect";
			this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
			// 
			// reconnectToolStripMenuItem
			// 
			this.reconnectToolStripMenuItem.Name = "reconnectToolStripMenuItem";
			this.reconnectToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.reconnectToolStripMenuItem.Text = "Reconnect";
			this.reconnectToolStripMenuItem.Click += new System.EventHandler(this.reconnectToolStripMenuItem_Click);
			// 
			// disconnectToolStripMenuItem
			// 
			this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
			this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.disconnectToolStripMenuItem.Text = "Disconnect";
			this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
			// 
			// modifyConnectionToolStripMenuItem
			// 
			this.modifyConnectionToolStripMenuItem.Name = "modifyConnectionToolStripMenuItem";
			this.modifyConnectionToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.modifyConnectionToolStripMenuItem.Text = "Modify Connection";
			this.modifyConnectionToolStripMenuItem.Click += new System.EventHandler(this.modifyConnectionToolStripMenuItem_Click);
			// 
			// removeDatabaseMenuItem
			// 
			this.removeDatabaseMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeDatabaseMenuItem.Image")));
			this.removeDatabaseMenuItem.Name = "removeDatabaseMenuItem";
			this.removeDatabaseMenuItem.Size = new System.Drawing.Size(177, 22);
			this.removeDatabaseMenuItem.Text = "Remove";
			this.removeDatabaseMenuItem.Click += new System.EventHandler(this.removeDatabaseMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(174, 6);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.propertiesToolStripMenuItem.Text = "Properties";
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
			// 
			// tableContextMenu
			// 
			this.tableContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTableDataMenuItem,
            this.openDataWithFilterToolStripMenuItem,
            this.exportDataToolStripMenuItem,
            this.scriptAsToolStripMenuItem,
            this.renameToolStripMenuItem});
			this.tableContextMenu.Name = "tableContextMenu";
			this.tableContextMenu.Size = new System.Drawing.Size(195, 114);
			// 
			// openTableDataMenuItem
			// 
			this.openTableDataMenuItem.Name = "openTableDataMenuItem";
			this.openTableDataMenuItem.Size = new System.Drawing.Size(194, 22);
			this.openTableDataMenuItem.Text = "Open Table Data";
			this.openTableDataMenuItem.Click += new System.EventHandler(this.openTableDataMenuItem_Click);
			// 
			// openDataWithFilterToolStripMenuItem
			// 
			this.openDataWithFilterToolStripMenuItem.Name = "openDataWithFilterToolStripMenuItem";
			this.openDataWithFilterToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.openDataWithFilterToolStripMenuItem.Text = "Open Data with Filter...";
			this.openDataWithFilterToolStripMenuItem.Click += new System.EventHandler(this.openDataWithFilterToolStripMenuItem_Click);
			// 
			// exportDataToolStripMenuItem
			// 
			this.exportDataToolStripMenuItem.Enabled = false;
			this.exportDataToolStripMenuItem.Name = "exportDataToolStripMenuItem";
			this.exportDataToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.exportDataToolStripMenuItem.Text = "Export Data...";
			// 
			// scriptAsToolStripMenuItem
			// 
			this.scriptAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.joinToolStripMenuItem,
            this.insertToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator6,
            this.createToolStripMenuItem,
            this.alterToolStripMenuItem,
            this.dropToolStripMenuItem});
			this.scriptAsToolStripMenuItem.Name = "scriptAsToolStripMenuItem";
			this.scriptAsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.scriptAsToolStripMenuItem.Text = "Script Table as";
			// 
			// selectToolStripMenuItem
			// 
			this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
			this.selectToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.selectToolStripMenuItem.Text = "SELECT";
			this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
			// 
			// joinToolStripMenuItem
			// 
			this.joinToolStripMenuItem.Name = "joinToolStripMenuItem";
			this.joinToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.joinToolStripMenuItem.Text = "JOIN";
			this.joinToolStripMenuItem.Click += new System.EventHandler(this.joinToolStripMenuItem_Click);
			// 
			// insertToolStripMenuItem
			// 
			this.insertToolStripMenuItem.Name = "insertToolStripMenuItem";
			this.insertToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.insertToolStripMenuItem.Text = "INSERT";
			this.insertToolStripMenuItem.Click += new System.EventHandler(this.insertToolStripMenuItem_Click);
			// 
			// updateToolStripMenuItem
			// 
			this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
			this.updateToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.updateToolStripMenuItem.Text = "UPDATE";
			this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.deleteToolStripMenuItem.Text = "DELETE";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(114, 6);
			// 
			// createToolStripMenuItem
			// 
			this.createToolStripMenuItem.Name = "createToolStripMenuItem";
			this.createToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.createToolStripMenuItem.Text = "CREATE";
			this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
			// 
			// alterToolStripMenuItem
			// 
			this.alterToolStripMenuItem.Name = "alterToolStripMenuItem";
			this.alterToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.alterToolStripMenuItem.Text = "ALTER";
			this.alterToolStripMenuItem.Click += new System.EventHandler(this.alterToolStripMenuItem_Click);
			// 
			// dropToolStripMenuItem
			// 
			this.dropToolStripMenuItem.Name = "dropToolStripMenuItem";
			this.dropToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.dropToolStripMenuItem.Text = "DROP";
			this.dropToolStripMenuItem.Click += new System.EventHandler(this.dropToolStripMenuItem_Click);
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "db";
			this.saveFileDialog.FileName = "NewDatabase.db";
			// 
			// procedureContextMenu
			// 
			this.procedureContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptAsExecuteToolStripMenuItem,
            this.scriptAsCreateToolStripMenuItem,
            this.scriptAsAlterToolStripMenuItem,
            this.scriptAsDropToolStripMenuItem});
			this.procedureContextMenu.Name = "procedureContextMenu";
			this.procedureContextMenu.Size = new System.Drawing.Size(162, 92);
			// 
			// scriptAsExecuteToolStripMenuItem
			// 
			this.scriptAsExecuteToolStripMenuItem.Name = "scriptAsExecuteToolStripMenuItem";
			this.scriptAsExecuteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.scriptAsExecuteToolStripMenuItem.Text = "Script as Execute";
			this.scriptAsExecuteToolStripMenuItem.Click += new System.EventHandler(this.scriptAsExecuteToolStripMenuItem_Click);
			// 
			// scriptAsCreateToolStripMenuItem
			// 
			this.scriptAsCreateToolStripMenuItem.Name = "scriptAsCreateToolStripMenuItem";
			this.scriptAsCreateToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.scriptAsCreateToolStripMenuItem.Text = "Script as Create";
			this.scriptAsCreateToolStripMenuItem.Click += new System.EventHandler(this.scriptAsCreateToolStripMenuItem_Click);
			// 
			// scriptAsAlterToolStripMenuItem
			// 
			this.scriptAsAlterToolStripMenuItem.Name = "scriptAsAlterToolStripMenuItem";
			this.scriptAsAlterToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.scriptAsAlterToolStripMenuItem.Text = "Script as Alter";
			this.scriptAsAlterToolStripMenuItem.Click += new System.EventHandler(this.scriptAsAlterToolStripMenuItem_Click);
			// 
			// scriptAsDropToolStripMenuItem
			// 
			this.scriptAsDropToolStripMenuItem.Name = "scriptAsDropToolStripMenuItem";
			this.scriptAsDropToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.scriptAsDropToolStripMenuItem.Text = "Script as Drop";
			this.scriptAsDropToolStripMenuItem.Click += new System.EventHandler(this.scriptAsDropToolStripMenuItem_Click);
			// 
			// foreignKeyMenuStrip
			// 
			this.foreignKeyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.joinQueryMenuItem});
			this.foreignKeyMenuStrip.Name = "procedureContextMenu";
			this.foreignKeyMenuStrip.Size = new System.Drawing.Size(163, 26);
			// 
			// joinQueryMenuItem
			// 
			this.joinQueryMenuItem.Name = "joinQueryMenuItem";
			this.joinQueryMenuItem.Size = new System.Drawing.Size(162, 22);
			this.joinQueryMenuItem.Text = "Open Join Query";
			this.joinQueryMenuItem.Click += new System.EventHandler(this.joinQueryMenuItem_Click);
			// 
			// DatabaseExplorer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(236, 266);
			this.Controls.Add(this.treeView);
			this.Controls.Add(this.toolStrip);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DatabaseExplorer";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.TabText = "Database Explorer";
			this.Text = "Database Explorer";
			this.Load += new System.EventHandler(this.DatabaseExplorer_Load);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.databaseContextMenu.ResumeLayout(false);
			this.tableContextMenu.ResumeLayout(false);
			this.procedureContextMenu.ResumeLayout(false);
			this.foreignKeyMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DataDevelop.UIComponents.ToolStrip toolStrip;
		private DataDevelop.UIComponents.TreeView treeView;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton newFolderToolStripButton;
		private System.Windows.Forms.ImageList nodesImageList;
		private System.Windows.Forms.ContextMenuStrip databaseContextMenu;
		private System.Windows.Forms.ToolStripMenuItem newQueryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeDatabaseMenuItem;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip tableContextMenu;
		private System.Windows.Forms.ToolStripMenuItem openTableDataMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportDataToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripButton addDatabaseToolStripButton;
		private System.Windows.Forms.ToolStripButton createDatabaseToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton refreshToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem scriptAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem modifyConnectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem alterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dropToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip procedureContextMenu;
		private System.Windows.Forms.ToolStripMenuItem scriptAsExecuteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scriptAsCreateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scriptAsAlterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem scriptAsDropToolStripMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton actionsButton;
		private System.Windows.Forms.ToolStripMenuItem disconnectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveConnectionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConnectionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sortDatabasesToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip foreignKeyMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem joinQueryMenuItem;
		private System.Windows.Forms.ToolStripMenuItem joinToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reconnectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openDataWithFilterToolStripMenuItem;

	}
}