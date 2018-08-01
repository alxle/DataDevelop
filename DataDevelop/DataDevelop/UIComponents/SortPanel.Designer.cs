namespace DataDevelop.UIComponents
{
	partial class SortPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SortPanel));
			this.downButton = new System.Windows.Forms.Button();
			this.upButton = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.columnOrderBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.resetButton = new System.Windows.Forms.Button();
			this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.orderTypeComboBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.columnOrderBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// downButton
			// 
			this.downButton.Image = ((System.Drawing.Image)(resources.GetObject("downButton.Image")));
			this.downButton.Location = new System.Drawing.Point(3, 32);
			this.downButton.Name = "downButton";
			this.downButton.Size = new System.Drawing.Size(23, 23);
			this.downButton.TabIndex = 10;
			this.downButton.UseVisualStyleBackColor = true;
			this.downButton.Click += new System.EventHandler(this.downButton_Click);
			// 
			// upButton
			// 
			this.upButton.Enabled = false;
			this.upButton.Image = ((System.Drawing.Image)(resources.GetObject("upButton.Image")));
			this.upButton.Location = new System.Drawing.Point(3, 3);
			this.upButton.Name = "upButton";
			this.upButton.Size = new System.Drawing.Size(23, 23);
			this.upButton.TabIndex = 9;
			this.upButton.UseVisualStyleBackColor = true;
			this.upButton.Click += new System.EventHandler(this.upButton_Click);
			// 
			// dataGridView
			// 
			this.dataGridView.AllowUserToAddRows = false;
			this.dataGridView.AllowUserToDeleteRows = false;
			this.dataGridView.AllowUserToResizeRows = false;
			this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView.AutoGenerateColumns = false;
			this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.orderTypeComboBoxColumn});
			this.dataGridView.DataSource = this.columnOrderBindingSource;
			this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dataGridView.Location = new System.Drawing.Point(32, 3);
			this.dataGridView.MultiSelect = false;
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView.Size = new System.Drawing.Size(249, 164);
			this.dataGridView.TabIndex = 8;
			this.dataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_RowsAdded);
			this.dataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_RowsRemoved);
			this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
			// 
			// columnOrderBindingSource
			// 
			this.columnOrderBindingSource.DataSource = typeof(DataDevelop.Data.ColumnOrder);
			// 
			// resetButton
			// 
			this.resetButton.Image = global::DataDevelop.Properties.Resources.ClearAll;
			this.resetButton.Location = new System.Drawing.Point(3, 61);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(23, 23);
			this.resetButton.TabIndex = 11;
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// ColumnName
			// 
			this.ColumnName.DataPropertyName = "ColumnName";
			this.ColumnName.Frozen = true;
			this.ColumnName.HeaderText = "Column";
			this.ColumnName.Name = "ColumnName";
			this.ColumnName.ReadOnly = true;
			// 
			// orderTypeComboBoxColumn
			// 
			this.orderTypeComboBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.orderTypeComboBoxColumn.DataPropertyName = "OrderType";
			this.orderTypeComboBoxColumn.HeaderText = "Order";
			this.orderTypeComboBoxColumn.Name = "orderTypeComboBoxColumn";
			this.orderTypeComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.orderTypeComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// SortPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.downButton);
			this.Controls.Add(this.upButton);
			this.Controls.Add(this.dataGridView);
			this.Name = "SortPanel";
			this.Size = new System.Drawing.Size(284, 170);
			this.Load += new System.EventHandler(this.SortPanel_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.columnOrderBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button downButton;
		private System.Windows.Forms.Button upButton;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.BindingSource columnOrderBindingSource;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
		private System.Windows.Forms.DataGridViewComboBoxColumn orderTypeComboBoxColumn;

	}
}
