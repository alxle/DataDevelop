namespace DataDevelop.Dialogs
{
	partial class RowsPerPageDialog
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
			this.rowPerPageNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.rowPerPageNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(62, 72);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(143, 72);
			// 
			// rowPerPageNumericUpDown
			// 
			this.rowPerPageNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.rowPerPageNumericUpDown.Location = new System.Drawing.Point(111, 25);
			this.rowPerPageNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.rowPerPageNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.rowPerPageNumericUpDown.Name = "rowPerPageNumericUpDown";
			this.rowPerPageNumericUpDown.Size = new System.Drawing.Size(97, 20);
			this.rowPerPageNumericUpDown.TabIndex = 2;
			this.rowPerPageNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.rowPerPageNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Rows Per Page:";
			// 
			// RowsPerPageDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(230, 107);
			this.Controls.Add(this.rowPerPageNumericUpDown);
			this.Controls.Add(this.label1);
			this.Name = "RowsPerPageDialog";
			this.Text = "Rows Per Page";
			this.Controls.SetChildIndex(this.okButton, 0);
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.rowPerPageNumericUpDown, 0);
			((System.ComponentModel.ISupportInitialize)(this.rowPerPageNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown rowPerPageNumericUpDown;
		private System.Windows.Forms.Label label1;
	}
}