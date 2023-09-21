namespace DataDevelop
{
	partial class QueryHistoryWindow
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridView1 = new DataDevelop.DataGridView();
			this.dbNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dbProviderColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.queryTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.queryDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.elapsedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.errorMessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.toolStrip1 = new DataDevelop.UIComponents.ToolStrip();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.historyEnabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.refreshButton = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dbNameColumn,
            this.dbProviderColumn,
            this.queryTextColumn,
            this.queryDateColumn,
            this.statusColumn,
            this.elapsedColumn,
            this.errorMessageColumn});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridView1.Location = new System.Drawing.Point(0, 25);
			this.dataGridView1.Name = "dataGridView1";
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.InfoText;
			this.dataGridView1.NullCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridView1.Size = new System.Drawing.Size(493, 147);
			this.dataGridView1.StartRowNumber = 1;
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.UserDeletedRow);
			this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.UserDeletingRow);
			// 
			// dbNameColumn
			// 
			this.dbNameColumn.DataPropertyName = "DbName";
			this.dbNameColumn.HeaderText = "Database";
			this.dbNameColumn.Name = "dbNameColumn";
			this.dbNameColumn.ReadOnly = true;
			// 
			// dbProviderColumn
			// 
			this.dbProviderColumn.DataPropertyName = "DbProvider";
			this.dbProviderColumn.HeaderText = "Provider";
			this.dbProviderColumn.Name = "dbProviderColumn";
			this.dbProviderColumn.ReadOnly = true;
			// 
			// queryTextColumn
			// 
			this.queryTextColumn.DataPropertyName = "QueryText";
			this.queryTextColumn.HeaderText = "Query";
			this.queryTextColumn.Name = "queryTextColumn";
			this.queryTextColumn.ReadOnly = true;
			this.queryTextColumn.Width = 200;
			// 
			// queryDateColumn
			// 
			this.queryDateColumn.DataPropertyName = "QueryDate";
			this.queryDateColumn.HeaderText = "Date";
			this.queryDateColumn.Name = "queryDateColumn";
			this.queryDateColumn.ReadOnly = true;
			this.queryDateColumn.Width = 110;
			// 
			// statusColumn
			// 
			this.statusColumn.DataPropertyName = "Status";
			this.statusColumn.HeaderText = "Status";
			this.statusColumn.Name = "statusColumn";
			this.statusColumn.ReadOnly = true;
			// 
			// elapsedColumn
			// 
			this.elapsedColumn.DataPropertyName = "ElapsedSeconds";
			this.elapsedColumn.HeaderText = "Elapsed";
			this.elapsedColumn.Name = "elapsedColumn";
			this.elapsedColumn.ReadOnly = true;
			// 
			// errorMessageColumn
			// 
			this.errorMessageColumn.DataPropertyName = "ErrorMessage";
			this.errorMessageColumn.HeaderText = "Error";
			this.errorMessageColumn.Name = "errorMessageColumn";
			this.errorMessageColumn.ReadOnly = true;
			this.errorMessageColumn.Width = 150;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.refreshButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(493, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.historyEnabledToolStripMenuItem});
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(62, 22);
			this.toolStripDropDownButton1.Text = "Settings";
			this.toolStripDropDownButton1.DropDownOpening += new System.EventHandler(this.Settings_DropDownOpening);
			// 
			// historyEnabledToolStripMenuItem
			// 
			this.historyEnabledToolStripMenuItem.Checked = global::DataDevelop.Properties.Settings.Default.QueryHistoryEnabled;
			this.historyEnabledToolStripMenuItem.Name = "historyEnabledToolStripMenuItem";
			this.historyEnabledToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.historyEnabledToolStripMenuItem.Text = "History Enabled";
			this.historyEnabledToolStripMenuItem.Click += new System.EventHandler(this.ToggleEnabled);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// refreshButton
			// 
			this.refreshButton.Image = global::DataDevelop.Properties.Resources.Refresh_16x;
			this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(66, 22);
			this.refreshButton.Text = "Refresh";
			this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// QueryHistoryWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(493, 172);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "QueryHistoryWindow";
			this.TabText = "Query History";
			this.Text = "Query History";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dbNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dbProviderColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn queryTextColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn queryDateColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn elapsedColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn errorMessageColumn;
		private UIComponents.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton refreshButton;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem historyEnabledToolStripMenuItem;
	}
}
