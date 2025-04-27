namespace DataDevelop
{
	partial class ScriptDocument
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptDocument));
			this.dataSet = new System.Data.DataSet();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.executeButton = new System.Windows.Forms.ToolStripButton();
			this.stopButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.outputFullExceptionDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.showResultPanelToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.statusLabel = new System.Windows.Forms.ToolStripLabel();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openRecentFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.emptyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.textEditorControl = new DataDevelop.UIComponents.TextEditor();
			this.backgroundWorker = new DataDevelop.Components.BackgroundWorkerEx();
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
			this.toolStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataSet
			// 
			this.dataSet.DataSetName = "NewDataSet";
			// 
			// toolStrip
			// 
			this.toolStrip.AutoSize = false;
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator10,
            this.executeButton,
            this.stopButton,
            this.toolStripSeparator6,
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.showResultPanelToolStripButton,
            this.toolStripSeparator8,
            this.statusLabel});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.toolStrip.Size = new System.Drawing.Size(475, 25);
			this.toolStrip.TabIndex = 5;
			this.toolStrip.Text = "commandToolStrip";
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newToolStripButton.Image = global::DataDevelop.Properties.Resources.Document_16x;
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newToolStripButton.Text = "&New";
			this.newToolStripButton.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = global::DataDevelop.Properties.Resources.OpenFolder_16x;
			this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.openToolStripButton.Text = "&Open";
			this.openToolStripButton.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = global::DataDevelop.Properties.Resources.Save_16x;
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "&Save";
			this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
			// 
			// executeButton
			// 
			this.executeButton.Image = global::DataDevelop.Properties.Resources.Run_16x;
			this.executeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.executeButton.Name = "executeButton";
			this.executeButton.Size = new System.Drawing.Size(67, 22);
			this.executeButton.Text = "Execute";
			this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
			// 
			// stopButton
			// 
			this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.stopButton.Enabled = false;
			this.stopButton.Image = global::DataDevelop.Properties.Resources.Stop_16x;
			this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(23, 22);
			this.stopButton.Text = "Abort";
			this.stopButton.Visible = false;
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.outputFullExceptionDetailsToolStripMenuItem});
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(62, 22);
			this.toolStripDropDownButton1.Text = "Options";
			// 
			// outputFullExceptionDetailsToolStripMenuItem
			// 
			this.outputFullExceptionDetailsToolStripMenuItem.CheckOnClick = true;
			this.outputFullExceptionDetailsToolStripMenuItem.Name = "outputFullExceptionDetailsToolStripMenuItem";
			this.outputFullExceptionDetailsToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
			this.outputFullExceptionDetailsToolStripMenuItem.Text = "Output Full Exception Details";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// showResultPanelToolStripButton
			// 
			this.showResultPanelToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("showResultPanelToolStripButton.Image")));
			this.showResultPanelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.showResultPanelToolStripButton.Name = "showResultPanelToolStripButton";
			this.showResultPanelToolStripButton.Size = new System.Drawing.Size(65, 22);
			this.showResultPanelToolStripButton.Text = "Output";
			this.showResultPanelToolStripButton.Click += new System.EventHandler(this.showResultPanelToolStripButton_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(48, 22);
			this.statusLabel.Text = "Ready...";
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.editToolStripMenuItem1,
            this.scriptToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(475, 24);
			this.menuStrip.TabIndex = 6;
			this.menuStrip.Text = "menuStrip1";
			this.menuStrip.Visible = false;
			// 
			// fileToolStripMenuItem1
			// 
			this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.openRecentFileToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator3,
            this.closeToolStripMenuItem,
            this.toolStripSeparator7});
			this.fileToolStripMenuItem1.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
			this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem1.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Document_16x;
			this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.newToolStripMenuItem.MergeIndex = 3;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.OpenFolder_16x;
			this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.openToolStripMenuItem.MergeIndex = 4;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// openRecentFileToolStripMenuItem
			// 
			this.openRecentFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyToolStripMenuItem1});
			this.openRecentFileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.openRecentFileToolStripMenuItem.MergeIndex = 5;
			this.openRecentFileToolStripMenuItem.Name = "openRecentFileToolStripMenuItem";
			this.openRecentFileToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.openRecentFileToolStripMenuItem.Text = "Open Recent File";
			this.openRecentFileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.openRecentFileToolStripMenuItem_DropDownOpening);
			// 
			// emptyToolStripMenuItem1
			// 
			this.emptyToolStripMenuItem1.Name = "emptyToolStripMenuItem1";
			this.emptyToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.emptyToolStripMenuItem1.Text = "Empty";
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator.MergeIndex = 6;
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(183, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Save_16x;
			this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.saveToolStripMenuItem.MergeIndex = 7;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.saveAsToolStripMenuItem.MergeIndex = 8;
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator2.MergeIndex = 9;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(183, 6);
			// 
			// printToolStripMenuItem
			// 
			this.printToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Print_16x;
			this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.printToolStripMenuItem.MergeIndex = 10;
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.printToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.printToolStripMenuItem.Text = "&Print";
			// 
			// printPreviewToolStripMenuItem
			// 
			this.printPreviewToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.PrintPreview_16x;
			this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printPreviewToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.printPreviewToolStripMenuItem.MergeIndex = 11;
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator3.MergeIndex = 12;
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(183, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.closeToolStripMenuItem.MergeIndex = 13;
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.closeToolStripMenuItem.Text = "&Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator7.MergeIndex = 14;
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(183, 6);
			// 
			// editToolStripMenuItem1
			// 
			this.editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator4,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator5,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator9,
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem});
			this.editToolStripMenuItem1.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.editToolStripMenuItem1.MergeIndex = 1;
			this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
			this.editToolStripMenuItem1.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem1.Text = "&Edit";
			this.editToolStripMenuItem1.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem1_DropDownOpening);
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Undo_16x;
			this.undoToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.undoToolStripMenuItem.Text = "&Undo";
			this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Redo_16x;
			this.redoToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.redoToolStripMenuItem.Text = "&Redo";
			this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(143, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Cut_16x;
			this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.cutToolStripMenuItem.Text = "Cu&t";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Copy_16x;
			this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Paste_16x;
			this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.pasteToolStripMenuItem.Text = "&Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(143, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(143, 6);
			// 
			// findToolStripMenuItem
			// 
			this.findToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.QuickFind_16x;
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.findToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.findToolStripMenuItem.Text = "Find...";
			this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
			// 
			// replaceToolStripMenuItem
			// 
			this.replaceToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.QuickReplace_16x;
			this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
			this.replaceToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.replaceToolStripMenuItem.Text = "R&eplace...";
			this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
			// 
			// scriptToolStripMenuItem
			// 
			this.scriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.executeToolStripMenuItem});
			this.scriptToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.scriptToolStripMenuItem.MergeIndex = 3;
			this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
			this.scriptToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
			this.scriptToolStripMenuItem.Text = "&Script";
			// 
			// executeToolStripMenuItem
			// 
			this.executeToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Run_16x;
			this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
			this.executeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.executeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.executeToolStripMenuItem.Text = "Execute";
			this.executeToolStripMenuItem.Click += new System.EventHandler(this.executeButton_Click);
			// 
			// textEditorControl
			// 
			this.textEditorControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textEditorControl.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::DataDevelop.Properties.Settings.Default, "TextEditorFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textEditorControl.Font = global::DataDevelop.Properties.Settings.Default.TextEditorFont;
			this.textEditorControl.HasChanges = false;
			this.textEditorControl.IsReadOnly = false;
			this.textEditorControl.Location = new System.Drawing.Point(0, 25);
			this.textEditorControl.Name = "textEditorControl";
			this.textEditorControl.ShowEOLMarkers = true;
			this.textEditorControl.ShowSpaces = true;
			this.textEditorControl.ShowTabs = true;
			this.textEditorControl.Size = new System.Drawing.Size(475, 374);
			this.textEditorControl.TabIndex = 8;
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.AbortEnabled = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			// 
			// ScriptDocument
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(475, 399);
			this.Controls.Add(this.textEditorControl);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.menuStrip);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.Name = "ScriptDocument";
			this.Text = "Scripting Console";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptDocument_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Data.DataSet dataSet;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton executeButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton showResultPanelToolStripButton;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
		private DataDevelop.UIComponents.TextEditor textEditorControl;
		private DataDevelop.Components.BackgroundWorkerEx backgroundWorker;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripLabel statusLabel;
		private System.Windows.Forms.ToolStripButton stopButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton newToolStripButton;
		private System.Windows.Forms.ToolStripButton openToolStripButton;
		private System.Windows.Forms.ToolStripButton saveToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem outputFullExceptionDetailsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem openRecentFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem1;
	}
}
