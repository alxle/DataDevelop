namespace DataDevelop
{
	partial class TableDocument
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableDocument));
			this.tableToolStrip = new System.Windows.Forms.ToolStrip();
			this.firstButton = new System.Windows.Forms.ToolStripButton();
			this.prevButton = new System.Windows.Forms.ToolStripButton();
			this.locationLabel = new System.Windows.Forms.ToolStripLabel();
			this.nextButton = new System.Windows.Forms.ToolStripButton();
			this.lastButton = new System.Windows.Forms.ToolStripButton();
			this.newRowButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.refreshButton = new System.Windows.Forms.ToolStripButton();
			this.saveChangesButton = new System.Windows.Forms.ToolStripButton();
			this.discardChangesButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.filterToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.sortToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.autoResizeColumnsDropDownButton = new System.Windows.Forms.ToolStripSplitButton();
			this.rowsPerPageButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.exportToExcelToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.exportAllToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportCurrentPageToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.viewSqlToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.PasteButton = new System.Windows.Forms.ToolStripButton();
			this.dataGridView = new DataDevelop.DataGridView();
			this.dataTablePrintDocument = new DataDevelop.Components.DataTablePrintDocument();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
			this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.tableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadingPanel = new DataDevelop.UIComponents.AlphaPanel();
			this.loadingInnerPanel = new DataDevelop.UIComponents.AlphaPanel();
			this.loadingLabel = new System.Windows.Forms.Label();
			this.loadingProgressBar = new System.Windows.Forms.ProgressBar();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.excelWorker = new System.ComponentModel.BackgroundWorker();
			this.tableToolStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			this.menuStrip.SuspendLayout();
			this.loadingPanel.SuspendLayout();
			this.loadingInnerPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableToolStrip
			// 
			this.tableToolStrip.AutoSize = false;
			this.tableToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tableToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firstButton,
            this.prevButton,
            this.locationLabel,
            this.nextButton,
            this.lastButton,
            this.newRowButton,
            this.toolStripSeparator1,
            this.refreshButton,
            this.saveChangesButton,
            this.discardChangesButton,
            this.toolStripSeparator2,
            this.filterToolStripButton,
            this.sortToolStripButton,
            this.toolStripSeparator4,
            this.autoResizeColumnsDropDownButton,
            this.rowsPerPageButton,
            this.toolStripSeparator3,
            this.exportToExcelToolStripDropDownButton,
            this.toolStripDropDownButton1,
            this.toolStripSeparator5,
            this.viewSqlToolStripButton,
            this.toolStripSeparator6,
            this.PasteButton});
			this.tableToolStrip.Location = new System.Drawing.Point(0, 24);
			this.tableToolStrip.Name = "tableToolStrip";
			this.tableToolStrip.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.tableToolStrip.Size = new System.Drawing.Size(612, 25);
			this.tableToolStrip.TabIndex = 2;
			// 
			// firstButton
			// 
			this.firstButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.firstButton.Image = global::DataDevelop.Properties.Resources.GoToFirst_16x;
			this.firstButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.firstButton.Name = "firstButton";
			this.firstButton.Size = new System.Drawing.Size(23, 22);
			this.firstButton.Text = "First";
			this.firstButton.Click += new System.EventHandler(this.FirstButton_Click);
			// 
			// prevButton
			// 
			this.prevButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.prevButton.Image = global::DataDevelop.Properties.Resources.Previous_16x;
			this.prevButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.prevButton.Name = "prevButton";
			this.prevButton.Size = new System.Drawing.Size(23, 22);
			this.prevButton.Text = "Previous";
			this.prevButton.Click += new System.EventHandler(this.PrevButton_Click);
			// 
			// locationLabel
			// 
			this.locationLabel.Name = "locationLabel";
			this.locationLabel.Size = new System.Drawing.Size(59, 22);
			this.locationLabel.Text = "1 to 1 of 1";
			// 
			// nextButton
			// 
			this.nextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.nextButton.Image = global::DataDevelop.Properties.Resources.Next_16x;
			this.nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(23, 22);
			this.nextButton.Text = "Next";
			this.nextButton.Click += new System.EventHandler(this.NextButton_Click);
			// 
			// lastButton
			// 
			this.lastButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.lastButton.Image = global::DataDevelop.Properties.Resources.GoToLast_16x;
			this.lastButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.lastButton.Name = "lastButton";
			this.lastButton.Size = new System.Drawing.Size(23, 22);
			this.lastButton.Text = "Last";
			this.lastButton.Click += new System.EventHandler(this.LastButton_Click);
			// 
			// newRowButton
			// 
			this.newRowButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newRowButton.Image = global::DataDevelop.Properties.Resources.NewRow_16x;
			this.newRowButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newRowButton.Name = "newRowButton";
			this.newRowButton.Size = new System.Drawing.Size(23, 22);
			this.newRowButton.Text = "New Row";
			this.newRowButton.Click += new System.EventHandler(this.NewRowButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// refreshButton
			// 
			this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.refreshButton.Image = global::DataDevelop.Properties.Resources.Refresh_16x;
			this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(23, 22);
			this.refreshButton.Text = "Refresh";
			this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// saveChangesButton
			// 
			this.saveChangesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveChangesButton.Image = global::DataDevelop.Properties.Resources.Save_16x;
			this.saveChangesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveChangesButton.Name = "saveChangesButton";
			this.saveChangesButton.Size = new System.Drawing.Size(23, 22);
			this.saveChangesButton.Text = "Save Changes";
			this.saveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
			// 
			// discardChangesButton
			// 
			this.discardChangesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.discardChangesButton.Image = global::DataDevelop.Properties.Resources.Restart_16x;
			this.discardChangesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.discardChangesButton.Name = "discardChangesButton";
			this.discardChangesButton.Size = new System.Drawing.Size(23, 22);
			this.discardChangesButton.Text = "Discard Changes";
			this.discardChangesButton.Click += new System.EventHandler(this.DiscardChangesButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// filterToolStripButton
			// 
			this.filterToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.filterToolStripButton.Image = global::DataDevelop.Properties.Resources.Filter_16x;
			this.filterToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.filterToolStripButton.Name = "filterToolStripButton";
			this.filterToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.filterToolStripButton.Text = "Filter";
			this.filterToolStripButton.Click += new System.EventHandler(this.FilterToolStripButton_Click);
			// 
			// sortToolStripButton
			// 
			this.sortToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.sortToolStripButton.Image = global::DataDevelop.Properties.Resources.SortAscending_16x;
			this.sortToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.sortToolStripButton.Name = "sortToolStripButton";
			this.sortToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.sortToolStripButton.Text = "Sort";
			this.sortToolStripButton.Click += new System.EventHandler(this.SortToolStripButton_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// autoResizeColumnsDropDownButton
			// 
			this.autoResizeColumnsDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.autoResizeColumnsDropDownButton.Image = global::DataDevelop.Properties.Resources.AutosizeFixedWidth_16x;
			this.autoResizeColumnsDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.autoResizeColumnsDropDownButton.Name = "autoResizeColumnsDropDownButton";
			this.autoResizeColumnsDropDownButton.Size = new System.Drawing.Size(32, 22);
			this.autoResizeColumnsDropDownButton.Text = "Auto Size Columns";
			this.autoResizeColumnsDropDownButton.ButtonClick += new System.EventHandler(this.AllCellsToolStripMenuItem_Click);
			// 
			// rowsPerPageButton
			// 
			this.rowsPerPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.rowsPerPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.rowsPerPageButton.Name = "rowsPerPageButton";
			this.rowsPerPageButton.Size = new System.Drawing.Size(29, 22);
			this.rowsPerPageButton.Text = "100";
			this.rowsPerPageButton.ToolTipText = "Rows Per Page";
			this.rowsPerPageButton.Click += new System.EventHandler(this.RowsPerPageButton_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// exportToExcelToolStripDropDownButton
			// 
			this.exportToExcelToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.exportToExcelToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAllToExcelToolStripMenuItem,
            this.exportCurrentPageToExcelToolStripMenuItem});
			this.exportToExcelToolStripDropDownButton.Image = global::DataDevelop.Properties.Resources.ExportToExcel_16x;
			this.exportToExcelToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.exportToExcelToolStripDropDownButton.Name = "exportToExcelToolStripDropDownButton";
			this.exportToExcelToolStripDropDownButton.Size = new System.Drawing.Size(29, 22);
			this.exportToExcelToolStripDropDownButton.Text = "Export to Excel";
			// 
			// exportAllToExcelToolStripMenuItem
			// 
			this.exportAllToExcelToolStripMenuItem.Name = "exportAllToExcelToolStripMenuItem";
			this.exportAllToExcelToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.exportAllToExcelToolStripMenuItem.Text = "Export All to Excel";
			this.exportAllToExcelToolStripMenuItem.Click += new System.EventHandler(this.ExportAllToExcelToolStripMenuItem_Click);
			// 
			// exportCurrentPageToExcelToolStripMenuItem
			// 
			this.exportCurrentPageToExcelToolStripMenuItem.Name = "exportCurrentPageToExcelToolStripMenuItem";
			this.exportCurrentPageToExcelToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.exportCurrentPageToExcelToolStripMenuItem.Text = "Export Page to Excel";
			this.exportCurrentPageToExcelToolStripMenuItem.Click += new System.EventHandler(this.ExportCurrentPageToExcelToolStripMenuItem_Click);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printPreviewToolStripMenuItem,
            this.changeFontToolStripMenuItem,
            this.pageSetupToolStripMenuItem});
			this.toolStripDropDownButton1.Image = global::DataDevelop.Properties.Resources.Print_16x;
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
			this.toolStripDropDownButton1.Text = "Printing";
			// 
			// printPreviewToolStripMenuItem
			// 
			this.printPreviewToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.PrintPreview_16x;
			this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
			this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.printPreviewToolStripMenuItem.Text = "Print Preview";
			this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.PrintPreviewButton_Click);
			// 
			// changeFontToolStripMenuItem
			// 
			this.changeFontToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Font_16x;
			this.changeFontToolStripMenuItem.Name = "changeFontToolStripMenuItem";
			this.changeFontToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.changeFontToolStripMenuItem.Text = "Change Font";
			this.changeFontToolStripMenuItem.Click += new System.EventHandler(this.FontButton_Click);
			// 
			// pageSetupToolStripMenuItem
			// 
			this.pageSetupToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.PrintSetup_16x;
			this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
			this.pageSetupToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.pageSetupToolStripMenuItem.Text = "Page Setup";
			this.pageSetupToolStripMenuItem.Click += new System.EventHandler(this.PageSetupButton_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// viewSqlToolStripButton
			// 
			this.viewSqlToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.viewSqlToolStripButton.Image = global::DataDevelop.Properties.Resources.SQL_16x;
			this.viewSqlToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.viewSqlToolStripButton.Name = "viewSqlToolStripButton";
			this.viewSqlToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.viewSqlToolStripButton.Text = "View SQL";
			this.viewSqlToolStripButton.Click += new System.EventHandler(this.ViewSqlToolStripButton_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// PasteButton
			// 
			this.PasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PasteButton.Image = global::DataDevelop.Properties.Resources.Paste_16x;
			this.PasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PasteButton.Name = "PasteButton";
			this.PasteButton.Size = new System.Drawing.Size(23, 22);
			this.PasteButton.Text = "Paste";
			this.PasteButton.Click += new System.EventHandler(this.PasteButton_Click);
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToOrderColumns = true;
			this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridView.Location = new System.Drawing.Point(0, 49);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.Size = new System.Drawing.Size(612, 331);
			this.dataGridView.StartRowNumber = 1;
			this.dataGridView.TabIndex = 3;
			this.dataGridView.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataGridView_ColumnAdded);
			// 
			// dataTablePrintDocument
			// 
			this.dataTablePrintDocument.DataTable = null;
			this.dataTablePrintDocument.Font = new System.Drawing.Font("Courier New", 8F);
			// 
			// printPreviewDialog
			// 
			this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog.Document = this.dataTablePrintDocument;
			this.printPreviewDialog.Enabled = true;
			this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
			this.printPreviewDialog.Name = "printPreviewDialog";
			this.printPreviewDialog.Visible = false;
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tableToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(612, 24);
			this.menuStrip.TabIndex = 7;
			this.menuStrip.Text = "menuStrip1";
			this.menuStrip.Visible = false;
			// 
			// tableToolStripMenuItem
			// 
			this.tableToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.tableToolStripMenuItem.MergeIndex = 2;
			this.tableToolStripMenuItem.Name = "tableToolStripMenuItem";
			this.tableToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.tableToolStripMenuItem.Text = "&Table";
			// 
			// loadingPanel
			// 
			this.loadingPanel.AlphaChannel = ((byte)(128));
			this.loadingPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(233)))), ((int)(((byte)(237)))));
			this.loadingPanel.Controls.Add(this.loadingInnerPanel);
			this.loadingPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.loadingPanel.Location = new System.Drawing.Point(216, 146);
			this.loadingPanel.Name = "loadingPanel";
			this.loadingPanel.Size = new System.Drawing.Size(200, 57);
			this.loadingPanel.TabIndex = 8;
			// 
			// loadingInnerPanel
			// 
			this.loadingInnerPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.loadingInnerPanel.BackColor = System.Drawing.SystemColors.Window;
			this.loadingInnerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.loadingInnerPanel.Controls.Add(this.loadingLabel);
			this.loadingInnerPanel.Controls.Add(this.loadingProgressBar);
			this.loadingInnerPanel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.loadingInnerPanel.Location = new System.Drawing.Point(3, 3);
			this.loadingInnerPanel.Name = "loadingInnerPanel";
			this.loadingInnerPanel.Size = new System.Drawing.Size(194, 51);
			this.loadingInnerPanel.TabIndex = 0;
			// 
			// loadingLabel
			// 
			this.loadingLabel.AutoSize = true;
			this.loadingLabel.Location = new System.Drawing.Point(6, 6);
			this.loadingLabel.Name = "loadingLabel";
			this.loadingLabel.Size = new System.Drawing.Size(85, 15);
			this.loadingLabel.TabIndex = 1;
			this.loadingLabel.Text = "Loading data...";
			// 
			// loadingProgressBar
			// 
			this.loadingProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.loadingProgressBar.Location = new System.Drawing.Point(3, 28);
			this.loadingProgressBar.MarqueeAnimationSpeed = 50;
			this.loadingProgressBar.Name = "loadingProgressBar";
			this.loadingProgressBar.Size = new System.Drawing.Size(186, 18);
			this.loadingProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.loadingProgressBar.TabIndex = 0;
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
			// 
			// excelWorker
			// 
			this.excelWorker.WorkerReportsProgress = true;
			this.excelWorker.WorkerSupportsCancellation = true;
			this.excelWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ExcelWorker_DoWork);
			// 
			// TableDocument
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(612, 380);
			this.Controls.Add(this.loadingPanel);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.tableToolStrip);
			this.Controls.Add(this.menuStrip);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TableDocument";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TableDocument_FormClosing);
			this.Load += new System.EventHandler(this.TableDocument_Load);
			this.Shown += new System.EventHandler(this.TableDocument_Shown);
			this.tableToolStrip.ResumeLayout(false);
			this.tableToolStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.loadingPanel.ResumeLayout(false);
			this.loadingInnerPanel.ResumeLayout(false);
			this.loadingInnerPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tableToolStrip;
		private System.Windows.Forms.ToolStripButton firstButton;
		private System.Windows.Forms.ToolStripButton prevButton;
		private System.Windows.Forms.ToolStripLabel locationLabel;
		private System.Windows.Forms.ToolStripButton nextButton;
		private System.Windows.Forms.ToolStripButton lastButton;
		private System.Windows.Forms.ToolStripButton newRowButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton refreshButton;
		private System.Windows.Forms.ToolStripButton saveChangesButton;
		private System.Windows.Forms.ToolStripButton discardChangesButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeFontToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pageSetupToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton filterToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private DataGridView dataGridView;
		private DataDevelop.Components.DataTablePrintDocument dataTablePrintDocument;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.PageSetupDialog pageSetupDialog;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
		private System.Windows.Forms.ToolStripButton viewSqlToolStripButton;
		private System.Windows.Forms.ToolStripButton sortToolStripButton;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem tableToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton rowsPerPageButton;
		private DataDevelop.UIComponents.AlphaPanel loadingPanel;
		private DataDevelop.UIComponents.AlphaPanel loadingInnerPanel;
		private System.Windows.Forms.Label loadingLabel;
		private System.Windows.Forms.ProgressBar loadingProgressBar;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.ToolStripSplitButton autoResizeColumnsDropDownButton;
		private System.Windows.Forms.ToolStripDropDownButton exportToExcelToolStripDropDownButton;
		private System.Windows.Forms.ToolStripMenuItem exportAllToExcelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportCurrentPageToExcelToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker excelWorker;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton PasteButton;
	}
}
