namespace DataDevelop
{
	partial class FilterDialog
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
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.dataGridView = new System.Windows.Forms.DataGridView();
			this.outputColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.filterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnFilterBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.clearButton = new System.Windows.Forms.Button();
			this.selectAllCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.columnFilterBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(342, 298);
			this.okButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(88, 27);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(436, 298);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(88, 27);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
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
            this.outputColumn,
            this.columnName,
            this.filterColumn});
			this.dataGridView.DataSource = this.columnFilterBindingSource;
			this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dataGridView.Location = new System.Drawing.Point(1, 1);
			this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.Size = new System.Drawing.Size(539, 292);
			this.dataGridView.TabIndex = 2;
			this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGridView_CellPainting);
			this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
			// 
			// outputColumn
			// 
			this.outputColumn.DataPropertyName = "Output";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.NullValue = false;
			this.outputColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.outputColumn.Frozen = true;
			this.outputColumn.HeaderText = "";
			this.outputColumn.Name = "outputColumn";
			this.outputColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.outputColumn.ToolTipText = "Output Column";
			this.outputColumn.Width = 20;
			// 
			// columnName
			// 
			this.columnName.DataPropertyName = "ColumnName";
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			this.columnName.DefaultCellStyle = dataGridViewCellStyle2;
			this.columnName.Frozen = true;
			this.columnName.HeaderText = "Column";
			this.columnName.Name = "columnName";
			this.columnName.ReadOnly = true;
			// 
			// filterColumn
			// 
			this.filterColumn.DataPropertyName = "Filter";
			this.filterColumn.HeaderText = "Filter";
			this.filterColumn.Name = "filterColumn";
			this.filterColumn.Width = 400;
			// 
			// columnFilterBindingSource
			// 
			this.columnFilterBindingSource.DataSource = typeof(DataDevelop.Data.ColumnFilter);
			// 
			// clearButton
			// 
			this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.clearButton.Location = new System.Drawing.Point(114, 298);
			this.clearButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(88, 27);
			this.clearButton.TabIndex = 3;
			this.clearButton.Text = "Clear &Filter";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// selectAllCheckBox
			// 
			this.selectAllCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.selectAllCheckBox.AutoCheck = false;
			this.selectAllCheckBox.AutoSize = true;
			this.selectAllCheckBox.Checked = true;
			this.selectAllCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.selectAllCheckBox.Location = new System.Drawing.Point(14, 303);
			this.selectAllCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.selectAllCheckBox.Name = "selectAllCheckBox";
			this.selectAllCheckBox.Size = new System.Drawing.Size(74, 19);
			this.selectAllCheckBox.TabIndex = 4;
			this.selectAllCheckBox.Text = "Select &All";
			this.selectAllCheckBox.UseVisualStyleBackColor = true;
			this.selectAllCheckBox.Click += new System.EventHandler(this.SelectAllCheckBox_Click);
			// 
			// FilterDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(541, 328);
			this.ControlBox = false;
			this.Controls.Add(this.selectAllCheckBox);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(347, 228);
			this.Name = "FilterDialog";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FilterDialog_FormClosed);
			this.Load += new System.EventHandler(this.FilterDialog_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.columnFilterBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.DataGridView dataGridView;
		private System.Windows.Forms.BindingSource columnFilterBindingSource;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.CheckBox selectAllCheckBox;
		private System.Windows.Forms.DataGridViewCheckBoxColumn outputColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
		private System.Windows.Forms.DataGridViewTextBoxColumn filterColumn;
	}
}
