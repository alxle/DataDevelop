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
			this.Output = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
			this.okButton.Location = new System.Drawing.Point(293, 258);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(374, 258);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
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
            this.Output,
            this.columnName,
            this.filterColumn});
			this.dataGridView.DataSource = this.columnFilterBindingSource;
			this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dataGridView.Location = new System.Drawing.Point(1, 1);
			this.dataGridView.Name = "dataGridView";
			this.dataGridView.RowHeadersVisible = false;
			this.dataGridView.Size = new System.Drawing.Size(462, 253);
			this.dataGridView.TabIndex = 2;
			this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting);
			this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
			this.dataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView_DataBindingComplete);
			// 
			// Output
			// 
			this.Output.DataPropertyName = "Output";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.NullValue = false;
			this.Output.DefaultCellStyle = dataGridViewCellStyle1;
			this.Output.Frozen = true;
			this.Output.HeaderText = "";
			this.Output.Name = "Output";
			this.Output.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Output.ToolTipText = "Output Column";
			this.Output.Width = 20;
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
			this.filterColumn.Width = 300;
			// 
			// columnFilterBindingSource
			// 
			this.columnFilterBindingSource.DataSource = typeof(DataDevelop.Data.ColumnFilter);
			// 
			// clearButton
			// 
			this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.clearButton.Location = new System.Drawing.Point(98, 258);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(75, 23);
			this.clearButton.TabIndex = 3;
			this.clearButton.Text = "Clear &Filter";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
			// 
			// selectAllCheckBox
			// 
			this.selectAllCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.selectAllCheckBox.AutoCheck = false;
			this.selectAllCheckBox.AutoSize = true;
			this.selectAllCheckBox.Checked = true;
			this.selectAllCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.selectAllCheckBox.Location = new System.Drawing.Point(12, 262);
			this.selectAllCheckBox.Name = "selectAllCheckBox";
			this.selectAllCheckBox.Size = new System.Drawing.Size(70, 17);
			this.selectAllCheckBox.TabIndex = 4;
			this.selectAllCheckBox.Text = "Select &All";
			this.selectAllCheckBox.UseVisualStyleBackColor = true;
			this.selectAllCheckBox.Click += new System.EventHandler(this.selectAllCheckBox_Click);
			// 
			// FilterDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(464, 284);
			this.ControlBox = false;
			this.Controls.Add(this.selectAllCheckBox);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.dataGridView);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 200);
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
		private System.Windows.Forms.DataGridViewCheckBoxColumn Output;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
		private System.Windows.Forms.DataGridViewTextBoxColumn filterColumn;
		private System.Windows.Forms.CheckBox selectAllCheckBox;
	}
}