namespace DataDevelop
{
	partial class DataGridView
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.mainContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.autoSizeColumnsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.autoSizeColumnsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.resizeColumnsAllCellsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resizeColumnsAllCellsExceptHeaderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resizeColumnsColumnHeaderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.autoSizeRowsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.autoSizeRowsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.resizeRowsAllCellsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resizeRowsRowHeaderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainContextMenuStrip.SuspendLayout();
			this.autoSizeColumnsContextMenuStrip.SuspendLayout();
			this.autoSizeRowsContextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// mainContextMenuStrip
			// 
			this.mainContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoSizeColumnsMenuItem,
            this.autoSizeRowsMenuItem});
			this.mainContextMenuStrip.Name = "contextMenuStrip";
			this.mainContextMenuStrip.Size = new System.Drawing.Size(175, 48);
			// 
			// autoSizeColumnsMenuItem
			// 
			this.autoSizeColumnsMenuItem.DropDown = this.autoSizeColumnsContextMenuStrip;
			this.autoSizeColumnsMenuItem.Image = global::DataDevelop.Properties.Resources.AutoResizeColumns;
			this.autoSizeColumnsMenuItem.Name = "autoSizeColumnsMenuItem";
			this.autoSizeColumnsMenuItem.Size = new System.Drawing.Size(174, 22);
			this.autoSizeColumnsMenuItem.Text = "Auto Size Columns";
			// 
			// autoSizeColumnsContextMenuStrip
			// 
			this.autoSizeColumnsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resizeColumnsAllCellsMenuItem,
            this.resizeColumnsAllCellsExceptHeaderMenuItem,
            this.resizeColumnsColumnHeaderMenuItem});
			this.autoSizeColumnsContextMenuStrip.Name = "autoSizeColumnsContextMenuStrip";
			this.autoSizeColumnsContextMenuStrip.OwnerItem = this.autoSizeColumnsMenuItem;
			this.autoSizeColumnsContextMenuStrip.Size = new System.Drawing.Size(184, 70);
			// 
			// resizeColumnsAllCellsMenuItem
			// 
			this.resizeColumnsAllCellsMenuItem.Name = "resizeColumnsAllCellsMenuItem";
			this.resizeColumnsAllCellsMenuItem.Size = new System.Drawing.Size(183, 22);
			this.resizeColumnsAllCellsMenuItem.Text = "All Cells";
			// 
			// resizeColumnsAllCellsExceptHeaderMenuItem
			// 
			this.resizeColumnsAllCellsExceptHeaderMenuItem.Name = "resizeColumnsAllCellsExceptHeaderMenuItem";
			this.resizeColumnsAllCellsExceptHeaderMenuItem.Size = new System.Drawing.Size(183, 22);
			this.resizeColumnsAllCellsExceptHeaderMenuItem.Text = "All Cells but Headers";
			// 
			// resizeColumnsColumnHeaderMenuItem
			// 
			this.resizeColumnsColumnHeaderMenuItem.Name = "resizeColumnsColumnHeaderMenuItem";
			this.resizeColumnsColumnHeaderMenuItem.Size = new System.Drawing.Size(183, 22);
			this.resizeColumnsColumnHeaderMenuItem.Text = "Columns Header";
			// 
			// autoSizeRowsMenuItem
			// 
			this.autoSizeRowsMenuItem.DropDown = this.autoSizeRowsContextMenuStrip;
			this.autoSizeRowsMenuItem.Image = global::DataDevelop.Properties.Resources.AutoResizeRows;
			this.autoSizeRowsMenuItem.Name = "autoSizeRowsMenuItem";
			this.autoSizeRowsMenuItem.Size = new System.Drawing.Size(174, 22);
			this.autoSizeRowsMenuItem.Text = "Auto Size Rows";
			// 
			// autoSizeRowsContextMenuStrip
			// 
			this.autoSizeRowsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resizeRowsAllCellsMenuItem,
            this.resizeRowsRowHeaderMenuItem});
			this.autoSizeRowsContextMenuStrip.Name = "autoSizeColumnsContextMenuStrip";
			this.autoSizeRowsContextMenuStrip.OwnerItem = this.autoSizeRowsMenuItem;
			this.autoSizeRowsContextMenuStrip.Size = new System.Drawing.Size(164, 48);
			// 
			// resizeRowsAllCellsMenuItem
			// 
			this.resizeRowsAllCellsMenuItem.Name = "resizeRowsAllCellsMenuItem";
			this.resizeRowsAllCellsMenuItem.Size = new System.Drawing.Size(163, 22);
			this.resizeRowsAllCellsMenuItem.Text = "All Cells";
			// 
			// resizeRowsRowHeaderMenuItem
			// 
			this.resizeRowsRowHeaderMenuItem.Name = "resizeRowsRowHeaderMenuItem";
			this.resizeRowsRowHeaderMenuItem.Size = new System.Drawing.Size(163, 22);
			this.resizeRowsRowHeaderMenuItem.Text = "Columns Header";
			// 
			// DataGridView
			// 
			this.BackgroundColor = System.Drawing.SystemColors.Window;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ContextMenuStrip = this.mainContextMenuStrip;
			this.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView_DataError);
			this.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DataGridView_ColumnAdded);
			this.mainContextMenuStrip.ResumeLayout(false);
			this.autoSizeColumnsContextMenuStrip.ResumeLayout(false);
			this.autoSizeRowsContextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip mainContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem autoSizeColumnsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem autoSizeRowsMenuItem;
		private System.Windows.Forms.ContextMenuStrip autoSizeColumnsContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem resizeColumnsAllCellsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resizeColumnsAllCellsExceptHeaderMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resizeColumnsColumnHeaderMenuItem;
		private System.Windows.Forms.ContextMenuStrip autoSizeRowsContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem resizeRowsAllCellsMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resizeRowsRowHeaderMenuItem;
	}
}
