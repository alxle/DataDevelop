namespace DataDevelop.Dialogs
{
	partial class ParamsDialog
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
			this.paramsDataGridView = new DataDevelop.DataGridView();
			this.parameterNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dbTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.iDataParameterBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.paramsDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iDataParameterBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(241, 165);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(322, 165);
			// 
			// paramsDataGridView
			// 
			this.paramsDataGridView.AllowUserToAddRows = false;
			this.paramsDataGridView.AllowUserToDeleteRows = false;
			this.paramsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.paramsDataGridView.AutoGenerateColumns = false;
			this.paramsDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.paramsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.paramsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.paramsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.parameterNameDataGridViewTextBoxColumn,
            this.dbTypeDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn});
			this.paramsDataGridView.DataSource = this.iDataParameterBindingSource;
			this.paramsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.paramsDataGridView.Location = new System.Drawing.Point(12, 12);
			this.paramsDataGridView.Name = "paramsDataGridView";
			this.paramsDataGridView.Size = new System.Drawing.Size(385, 147);
			this.paramsDataGridView.StartRowNumber = 1;
			this.paramsDataGridView.TabIndex = 2;
			// 
			// parameterNameDataGridViewTextBoxColumn
			// 
			this.parameterNameDataGridViewTextBoxColumn.DataPropertyName = "ParameterName";
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			this.parameterNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.parameterNameDataGridViewTextBoxColumn.HeaderText = "Name";
			this.parameterNameDataGridViewTextBoxColumn.Name = "parameterNameDataGridViewTextBoxColumn";
			this.parameterNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// dbTypeDataGridViewTextBoxColumn
			// 
			this.dbTypeDataGridViewTextBoxColumn.DataPropertyName = "DbType";
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
			this.dbTypeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.dbTypeDataGridViewTextBoxColumn.HeaderText = "Type";
			this.dbTypeDataGridViewTextBoxColumn.Name = "dbTypeDataGridViewTextBoxColumn";
			this.dbTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dbTypeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.dbTypeDataGridViewTextBoxColumn.Width = 80;
			// 
			// valueDataGridViewTextBoxColumn
			// 
			this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
			this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
			this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
			this.valueDataGridViewTextBoxColumn.Width = 140;
			// 
			// iDataParameterBindingSource
			// 
			this.iDataParameterBindingSource.DataSource = typeof(System.Data.IDataParameter);
			// 
			// ParamsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(409, 200);
			this.Controls.Add(this.paramsDataGridView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.Name = "ParamsDialog";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Parameters";
			this.Controls.SetChildIndex(this.paramsDataGridView, 0);
			this.Controls.SetChildIndex(this.okButton, 0);
			this.Controls.SetChildIndex(this.cancelButton, 0);
			((System.ComponentModel.ISupportInitialize)(this.paramsDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iDataParameterBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DataGridView paramsDataGridView;
		private System.Windows.Forms.BindingSource iDataParameterBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn parameterNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn dbTypeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;

	}
}
