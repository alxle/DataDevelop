namespace DataDevelop
{
	partial class CommandDocument
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommandDocument));
			this.textEditorControl = new DataDevelop.UIComponents.TextEditor();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.outputTabControl = new System.Windows.Forms.TabControl();
			this.resultsTabPage = new System.Windows.Forms.TabPage();
			this.dataGridView = new DataDevelop.DataGridView();
			this.resultsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.messagesTabPage = new System.Windows.Forms.TabPage();
			this.messageTextBox = new System.Windows.Forms.TextBox();
			this.tabImageList = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.undoButton = new System.Windows.Forms.ToolStripButton();
			this.redoButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.findToolStripButton = new System.Windows.Forms.ToolStripSplitButton();
			this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeAllTabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.executeButton = new System.Windows.Forms.ToolStripSplitButton();
			this.executeQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.executeNonQueryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.executeEachStatementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.executeAndSaveToXslxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.abortButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.showResultPanelToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.fileNameStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.providerStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.databaseStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.elapsedTimeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.totalRowsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.executeWorker = new DataDevelop.Utils.BackgroundWorkerEx();
			this.resultSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
			this.excelWorker = new System.ComponentModel.BackgroundWorker();
			this.executingTimer = new System.Windows.Forms.Timer(this.components);
			this.executeToXlsxWorker = new System.ComponentModel.BackgroundWorker();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.outputTabControl.SuspendLayout();
			this.resultsTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.resultsContextMenuStrip.SuspendLayout();
			this.messagesTabPage.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// textEditorControl
			// 
			this.textEditorControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textEditorControl.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::DataDevelop.Properties.Settings.Default, "TextEditorFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.textEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textEditorControl.Font = global::DataDevelop.Properties.Settings.Default.TextEditorFont;
			this.textEditorControl.HasChanges = false;
			this.textEditorControl.IsReadOnly = false;
			this.textEditorControl.Location = new System.Drawing.Point(0, 0);
			this.textEditorControl.Name = "textEditorControl";
			this.textEditorControl.ShowEOLMarkers = true;
			this.textEditorControl.ShowSpaces = true;
			this.textEditorControl.ShowTabs = true;
			this.textEditorControl.ShowVRuler = false;
			this.textEditorControl.Size = new System.Drawing.Size(473, 174);
			this.textEditorControl.TabIndex = 0;
			this.textEditorControl.FileNameChanged += new System.EventHandler(this.textEditorControl_FileNameChanged);
			this.textEditorControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.textEditorControl_DragDrop);
			this.textEditorControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.textEditorControl_DragEnter);
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 25);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.textEditorControl);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.outputTabControl);
			this.splitContainer.Size = new System.Drawing.Size(473, 348);
			this.splitContainer.SplitterDistance = 174;
			this.splitContainer.TabIndex = 1;
			// 
			// outputTabControl
			// 
			this.outputTabControl.Controls.Add(this.resultsTabPage);
			this.outputTabControl.Controls.Add(this.messagesTabPage);
			this.outputTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputTabControl.ImageList = this.tabImageList;
			this.outputTabControl.Location = new System.Drawing.Point(0, 0);
			this.outputTabControl.Name = "outputTabControl";
			this.outputTabControl.SelectedIndex = 0;
			this.outputTabControl.Size = new System.Drawing.Size(473, 170);
			this.outputTabControl.TabIndex = 4;
			// 
			// resultsTabPage
			// 
			this.resultsTabPage.Controls.Add(this.dataGridView);
			this.resultsTabPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resultsTabPage.ImageIndex = 0;
			this.resultsTabPage.Location = new System.Drawing.Point(4, 23);
			this.resultsTabPage.Name = "resultsTabPage";
			this.resultsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.resultsTabPage.Size = new System.Drawing.Size(465, 143);
			this.resultsTabPage.TabIndex = 0;
			this.resultsTabPage.Text = "Results";
			this.resultsTabPage.UseVisualStyleBackColor = true;
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.ContextMenuStrip = this.resultsContextMenuStrip;
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridView.Location = new System.Drawing.Point(3, 3);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.ReadOnly = true;
			this.dataGridView.Size = new System.Drawing.Size(459, 137);
			this.dataGridView.StartRowNumber = 1;
			this.dataGridView.TabIndex = 4;
			// 
			// resultsContextMenuStrip
			// 
			this.resultsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToFileToolStripMenuItem,
            this.exportToToolStripMenuItem,
            this.printPreviewToolStripMenuItem});
			this.resultsContextMenuStrip.Name = "resultsContextMenuStrip";
			this.resultsContextMenuStrip.Size = new System.Drawing.Size(153, 92);
			// 
			// saveToFileToolStripMenuItem
			// 
			this.saveToFileToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.SaveChanges;
			this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
			this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveToFileToolStripMenuItem.Text = "Save to file...";
			this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.saveToFileToolStripMenuItem_Click);
			// 
			// exportToToolStripMenuItem
			// 
			this.exportToToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.ExcelTable;
			this.exportToToolStripMenuItem.Name = "exportToToolStripMenuItem";
			this.exportToToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exportToToolStripMenuItem.Text = "Export to Excel";
			this.exportToToolStripMenuItem.Click += new System.EventHandler(this.exportToToolStripMenuItem_Click);
			// 
			// printPreviewToolStripMenuItem
			// 
			this.printPreviewToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.PrintPreview;
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.printPreviewToolStripMenuItem.Text = "Print Preview";
			this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
			// 
			// messagesTabPage
			// 
			this.messagesTabPage.Controls.Add(this.messageTextBox);
			this.messagesTabPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.messagesTabPage.ImageIndex = 1;
			this.messagesTabPage.Location = new System.Drawing.Point(4, 23);
			this.messagesTabPage.Name = "messagesTabPage";
			this.messagesTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.messagesTabPage.Size = new System.Drawing.Size(465, 143);
			this.messagesTabPage.TabIndex = 1;
			this.messagesTabPage.Text = "Messages";
			this.messagesTabPage.UseVisualStyleBackColor = true;
			// 
			// messageTextBox
			// 
			this.messageTextBox.BackColor = System.Drawing.Color.White;
			this.messageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.messageTextBox.Location = new System.Drawing.Point(3, 3);
			this.messageTextBox.Multiline = true;
			this.messageTextBox.Name = "messageTextBox";
			this.messageTextBox.ReadOnly = true;
			this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.messageTextBox.Size = new System.Drawing.Size(459, 137);
			this.messageTextBox.TabIndex = 0;
			// 
			// tabImageList
			// 
			this.tabImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImageList.ImageStream")));
			this.tabImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.tabImageList.Images.SetKeyName(0, "results");
			this.tabImageList.Images.SetKeyName(1, "output");
			// 
			// toolStrip
			// 
			this.toolStrip.AutoSize = false;
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.printToolStripButton,
            this.toolStripSeparator3,
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator5,
            this.findToolStripButton,
            this.toolStripSeparator4,
            this.executeButton,
            this.abortButton,
            this.toolStripSeparator1,
            this.showResultPanelToolStripButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.toolStrip.Size = new System.Drawing.Size(473, 25);
			this.toolStrip.TabIndex = 5;
			this.toolStrip.Text = "commandToolStrip";
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newToolStripButton.Image = global::DataDevelop.Properties.Resources.NewDocument;
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newToolStripButton.Text = "&New";
			this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = global::DataDevelop.Properties.Resources.Open;
			this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.openToolStripButton.Text = "&Open";
			this.openToolStripButton.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = global::DataDevelop.Properties.Resources.SaveChanges;
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "&Save";
			this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// printToolStripButton
			// 
			this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.printToolStripButton.Image = global::DataDevelop.Properties.Resources.Print;
			this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.printToolStripButton.Name = "printToolStripButton";
			this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.printToolStripButton.Text = "&Print";
			this.printToolStripButton.Click += new System.EventHandler(this.printToolStripButton_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// undoButton
			// 
			this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.undoButton.Image = global::DataDevelop.Properties.Resources.Undo;
			this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.undoButton.Name = "undoButton";
			this.undoButton.Size = new System.Drawing.Size(23, 22);
			this.undoButton.Text = "Undo";
			this.undoButton.Click += new System.EventHandler(this.Undo);
			// 
			// redoButton
			// 
			this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.redoButton.Image = global::DataDevelop.Properties.Resources.Redo;
			this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.redoButton.Name = "redoButton";
			this.redoButton.Size = new System.Drawing.Size(23, 22);
			this.redoButton.Text = "Redo";
			this.redoButton.Click += new System.EventHandler(this.Redo);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// cutToolStripButton
			// 
			this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cutToolStripButton.Image = global::DataDevelop.Properties.Resources.Cut;
			this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripButton.Name = "cutToolStripButton";
			this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.cutToolStripButton.Text = "C&ut";
			this.cutToolStripButton.Click += new System.EventHandler(this.cutToolStripButton_Click);
			// 
			// copyToolStripButton
			// 
			this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.copyToolStripButton.Image = global::DataDevelop.Properties.Resources.Copy;
			this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripButton.Name = "copyToolStripButton";
			this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.copyToolStripButton.Text = "&Copy";
			this.copyToolStripButton.Click += new System.EventHandler(this.copyToolStripButton_Click);
			// 
			// pasteToolStripButton
			// 
			this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.pasteToolStripButton.Image = global::DataDevelop.Properties.Resources.Paste;
			this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripButton.Name = "pasteToolStripButton";
			this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.pasteToolStripButton.Text = "&Paste";
			this.pasteToolStripButton.Click += new System.EventHandler(this.pasteToolStripButton_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// findToolStripButton
			// 
			this.findToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.findToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem,
            this.removeAllTabsToolStripMenuItem});
			this.findToolStripButton.Image = global::DataDevelop.Properties.Resources.Find;
			this.findToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.findToolStripButton.Name = "findToolStripButton";
			this.findToolStripButton.Size = new System.Drawing.Size(32, 22);
			this.findToolStripButton.Text = "Find";
			this.findToolStripButton.ToolTipText = "Find";
			this.findToolStripButton.ButtonClick += new System.EventHandler(this.Find);
			// 
			// findToolStripMenuItem
			// 
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.findToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.findToolStripMenuItem.Text = "Find...";
			this.findToolStripMenuItem.Click += new System.EventHandler(this.Find);
			// 
			// replaceToolStripMenuItem
			// 
			this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
			this.replaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.replaceToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.replaceToolStripMenuItem.Text = "Replace...";
			this.replaceToolStripMenuItem.Click += new System.EventHandler(this.Replace);
			// 
			// removeAllTabsToolStripMenuItem
			// 
			this.removeAllTabsToolStripMenuItem.Name = "removeAllTabsToolStripMenuItem";
			this.removeAllTabsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.removeAllTabsToolStripMenuItem.Text = "Remove all tabs";
			this.removeAllTabsToolStripMenuItem.Click += new System.EventHandler(this.RemoveAllTabs);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// executeButton
			// 
			this.executeButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.executeQueryToolStripMenuItem,
            this.executeNonQueryToolStripMenuItem,
            this.executeEachStatementToolStripMenuItem,
            this.executeAndSaveToXslxToolStripMenuItem});
			this.executeButton.Image = global::DataDevelop.Properties.Resources.Start;
			this.executeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.executeButton.Name = "executeButton";
			this.executeButton.Size = new System.Drawing.Size(79, 22);
			this.executeButton.Text = "Execute";
			this.executeButton.ButtonClick += new System.EventHandler(this.executeButton_Click);
			// 
			// executeQueryToolStripMenuItem
			// 
			this.executeQueryToolStripMenuItem.Name = "executeQueryToolStripMenuItem";
			this.executeQueryToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
			this.executeQueryToolStripMenuItem.Text = "Execute &Query";
			this.executeQueryToolStripMenuItem.Click += new System.EventHandler(this.executeQueryToolStripMenuItem_Click);
			// 
			// executeNonQueryToolStripMenuItem
			// 
			this.executeNonQueryToolStripMenuItem.Name = "executeNonQueryToolStripMenuItem";
			this.executeNonQueryToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
			this.executeNonQueryToolStripMenuItem.Text = "Execute &Non Query";
			this.executeNonQueryToolStripMenuItem.Click += new System.EventHandler(this.executeNonQueryToolStripMenuItem_Click);
			// 
			// executeEachStatementToolStripMenuItem
			// 
			this.executeEachStatementToolStripMenuItem.Name = "executeEachStatementToolStripMenuItem";
			this.executeEachStatementToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
			this.executeEachStatementToolStripMenuItem.Text = "Execute &Each Statement";
			this.executeEachStatementToolStripMenuItem.Click += new System.EventHandler(this.ExecuteEachStatement);
			// 
			// executeAndSaveToXslxToolStripMenuItem
			// 
			this.executeAndSaveToXslxToolStripMenuItem.Name = "executeAndSaveToXslxToolStripMenuItem";
			this.executeAndSaveToXslxToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
			this.executeAndSaveToXslxToolStripMenuItem.Text = "Execute and Save to Xslx";
			this.executeAndSaveToXslxToolStripMenuItem.Click += new System.EventHandler(this.executeAndSaveToXslxToolStripMenuItem_Click);
			// 
			// abortButton
			// 
			this.abortButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.abortButton.Image = global::DataDevelop.Properties.Resources.Stop;
			this.abortButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.abortButton.Name = "abortButton";
			this.abortButton.Size = new System.Drawing.Size(23, 22);
			this.abortButton.Text = "Abort";
			this.abortButton.Visible = false;
			this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// showResultPanelToolStripButton
			// 
			this.showResultPanelToolStripButton.CheckOnClick = true;
			this.showResultPanelToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.showResultPanelToolStripButton.Image = global::DataDevelop.Properties.Resources.Output;
			this.showResultPanelToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.showResultPanelToolStripButton.Name = "showResultPanelToolStripButton";
			this.showResultPanelToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.showResultPanelToolStripButton.Text = "Show Results Panel";
			this.showResultPanelToolStripButton.CheckedChanged += new System.EventHandler(this.showResultPanelToolStripButton_CheckedChanged);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar,
            this.fileNameStatusLabel,
            this.providerStatusLabel,
            this.databaseStatusLabel,
            this.elapsedTimeStatusLabel,
            this.totalRowsStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 373);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip.ShowItemToolTips = true;
			this.statusStrip.Size = new System.Drawing.Size(473, 24);
			this.statusStrip.SizingGrip = false;
			this.statusStrip.TabIndex = 5;
			// 
			// statusLabel
			// 
			this.statusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(213, 19);
			this.statusLabel.Spring = true;
			this.statusLabel.Text = "Ready.";
			this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// progressBar
			// 
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(100, 18);
			this.progressBar.Visible = false;
			// 
			// fileNameStatusLabel
			// 
			this.fileNameStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.fileNameStatusLabel.Name = "fileNameStatusLabel";
			this.fileNameStatusLabel.Size = new System.Drawing.Size(61, 19);
			this.fileNameStatusLabel.Text = "Unsaved*";
			// 
			// providerStatusLabel
			// 
			this.providerStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.providerStatusLabel.Name = "providerStatusLabel";
			this.providerStatusLabel.Size = new System.Drawing.Size(55, 19);
			this.providerStatusLabel.Text = "Provider";
			// 
			// databaseStatusLabel
			// 
			this.databaseStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.databaseStatusLabel.Name = "databaseStatusLabel";
			this.databaseStatusLabel.Size = new System.Drawing.Size(59, 19);
			this.databaseStatusLabel.Text = "Database";
			// 
			// elapsedTimeStatusLabel
			// 
			this.elapsedTimeStatusLabel.Name = "elapsedTimeStatusLabel";
			this.elapsedTimeStatusLabel.Size = new System.Drawing.Size(70, 19);
			this.elapsedTimeStatusLabel.Text = "00:00:00.000";
			// 
			// totalRowsStatusLabel
			// 
			this.totalRowsStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.totalRowsStatusLabel.Name = "totalRowsStatusLabel";
			this.totalRowsStatusLabel.Size = new System.Drawing.Size(45, 19);
			this.totalRowsStatusLabel.Text = "0 rows";
			this.totalRowsStatusLabel.Visible = false;
			// 
			// executeWorker
			// 
			this.executeWorker.AbortEnabled = true;
			this.executeWorker.WorkerSupportsCancellation = true;
			this.executeWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.executeWorker_DoWork);
			this.executeWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.executeWorker_RunWorkerCompleted);
			// 
			// resultSaveFileDialog
			// 
			this.resultSaveFileDialog.Filter = "XML Data File (*.xml)|*.xml|CSV File (*.csv)|*.csv|Text file (*.txt)|*.txt|Excel " +
    "Spreadsheet (*.xlsx)|*.xlsx";
			this.resultSaveFileDialog.Title = "Save Results File";
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.queryToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(473, 24);
			this.menuStrip.TabIndex = 6;
			this.menuStrip.Text = "menuStrip";
			this.menuStrip.Visible = false;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Open;
			this.openToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.openToolStripMenuItem.MergeIndex = 3;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.openToolStripMenuItem.Text = "&Open Query File...";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.SaveChanges;
			this.saveToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.saveToolStripMenuItem.MergeIndex = 4;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.saveToolStripMenuItem.Text = "&Save Query";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.saveAsToolStripMenuItem.MergeIndex = 5;
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.saveAsToolStripMenuItem.Text = "Save Query &As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.toolStripSeparator2.MergeIndex = 6;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.closeToolStripMenuItem.MergeIndex = 7;
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.closeToolStripMenuItem.Text = "Close Query";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// queryToolStripMenuItem
			// 
			this.queryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.executeToolStripMenuItem});
			this.queryToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.queryToolStripMenuItem.MergeIndex = 2;
			this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
			this.queryToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			this.queryToolStripMenuItem.Text = "&Query";
			// 
			// executeToolStripMenuItem
			// 
			this.executeToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Start;
			this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
			this.executeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.executeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.executeToolStripMenuItem.Text = "Execute";
			this.executeToolStripMenuItem.Click += new System.EventHandler(this.executeButton_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "SQL Files (*.sql)|*.sql|All files|*.*";
			this.openFileDialog.Title = "Open Command File";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "SQL Files (*.sql)|*.sql|All files|*.*";
			this.saveFileDialog.Title = "Save Command File";
			// 
			// printPreviewDialog
			// 
			this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog.ClientSize = new System.Drawing.Size(396, 296);
			this.printPreviewDialog.Enabled = true;
			this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
			this.printPreviewDialog.Name = "printPreviewDialog";
			this.printPreviewDialog.Visible = false;
			// 
			// excelWorker
			// 
			this.excelWorker.WorkerReportsProgress = true;
			this.excelWorker.WorkerSupportsCancellation = true;
			this.excelWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.excelWorker_DoWork);
			// 
			// executingTimer
			// 
			this.executingTimer.Interval = 1000;
			this.executingTimer.Tick += new System.EventHandler(this.executingTimer_Tick);
			// 
			// executeToXlsxWorker
			// 
			this.executeToXlsxWorker.WorkerReportsProgress = true;
			this.executeToXlsxWorker.WorkerSupportsCancellation = true;
			this.executeToXlsxWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.executeToXlsxWorker_DoWork);
			this.executeToXlsxWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.executeToXlsxWorker_ProgressChanged);
			// 
			// CommandDocument
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(473, 397);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip;
			this.Name = "CommandDocument";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommandDocument_FormClosing);
			this.Load += new System.EventHandler(this.CommandDocument_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.textEditorControl_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.textEditorControl_DragEnter);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.outputTabControl.ResumeLayout(false);
			this.resultsTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.resultsContextMenuStrip.ResumeLayout(false);
			this.messagesTabPage.ResumeLayout(false);
			this.messagesTabPage.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DataDevelop.UIComponents.TextEditor textEditorControl;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton showResultPanelToolStripButton;
		private System.Windows.Forms.TabControl outputTabControl;
		private System.Windows.Forms.TabPage resultsTabPage;
		private System.Windows.Forms.TabPage messagesTabPage;
		private System.Windows.Forms.TextBox messageTextBox;
		private DataDevelop.DataGridView dataGridView;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.ToolStripProgressBar progressBar;
		private DataDevelop.Utils.BackgroundWorkerEx executeWorker;
		private System.Windows.Forms.ContextMenuStrip resultsContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog resultSaveFileDialog;
		private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel elapsedTimeStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel totalRowsStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel providerStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel databaseStatusLabel;
		private System.Windows.Forms.ImageList tabImageList;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSplitButton executeButton;
		private System.Windows.Forms.ToolStripMenuItem executeQueryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem executeNonQueryToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton newToolStripButton;
		private System.Windows.Forms.ToolStripButton openToolStripButton;
		private System.Windows.Forms.ToolStripButton saveToolStripButton;
		private System.Windows.Forms.ToolStripButton printToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripButton cutToolStripButton;
		private System.Windows.Forms.ToolStripButton copyToolStripButton;
		private System.Windows.Forms.ToolStripButton pasteToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
		private System.Windows.Forms.ToolStripButton undoButton;
		private System.Windows.Forms.ToolStripButton redoButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSplitButton findToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeAllTabsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem executeEachStatementToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker excelWorker;
		private System.Windows.Forms.Timer executingTimer;
		private System.Windows.Forms.ToolStripButton abortButton;
		private System.Windows.Forms.ToolStripStatusLabel fileNameStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem executeAndSaveToXslxToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker executeToXlsxWorker;
	}
}
