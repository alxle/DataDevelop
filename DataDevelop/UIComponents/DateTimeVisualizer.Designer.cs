namespace DataDevelop.UIComponents
{
	partial class DateTimeVisualizer
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
			this.isNullCheckBox = new System.Windows.Forms.CheckBox();
			this.datePicker = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.timePicker = new System.Windows.Forms.DateTimePicker();
			this.valueGroupBox = new System.Windows.Forms.GroupBox();
			this.valueGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// isNullCheckBox
			// 
			this.isNullCheckBox.AutoSize = true;
			this.isNullCheckBox.Location = new System.Drawing.Point(18, 17);
			this.isNullCheckBox.Name = "isNullCheckBox";
			this.isNullCheckBox.Size = new System.Drawing.Size(55, 17);
			this.isNullCheckBox.TabIndex = 0;
			this.isNullCheckBox.Text = "Is Null";
			this.isNullCheckBox.UseVisualStyleBackColor = true;
			this.isNullCheckBox.CheckedChanged += new System.EventHandler(this.isNullCheckBox_CheckedChanged);
			// 
			// datePicker
			// 
			this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.datePicker.Location = new System.Drawing.Point(55, 23);
			this.datePicker.Name = "datePicker";
			this.datePicker.Size = new System.Drawing.Size(106, 20);
			this.datePicker.TabIndex = 1;
			this.datePicker.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Date:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Time:";
			// 
			// timePicker
			// 
			this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.timePicker.Location = new System.Drawing.Point(55, 49);
			this.timePicker.Name = "timePicker";
			this.timePicker.ShowUpDown = true;
			this.timePicker.Size = new System.Drawing.Size(106, 20);
			this.timePicker.TabIndex = 4;
			this.timePicker.ValueChanged += new System.EventHandler(this.timePicker_ValueChanged);
			// 
			// valueGroupBox
			// 
			this.valueGroupBox.Controls.Add(this.label1);
			this.valueGroupBox.Controls.Add(this.timePicker);
			this.valueGroupBox.Controls.Add(this.datePicker);
			this.valueGroupBox.Controls.Add(this.label2);
			this.valueGroupBox.Location = new System.Drawing.Point(12, 18);
			this.valueGroupBox.Name = "valueGroupBox";
			this.valueGroupBox.Size = new System.Drawing.Size(179, 86);
			this.valueGroupBox.TabIndex = 5;
			this.valueGroupBox.TabStop = false;
			// 
			// DateTimeVisualizer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(203, 116);
			this.Controls.Add(this.isNullCheckBox);
			this.Controls.Add(this.valueGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DateTimeVisualizer";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "DateTime Visualizer";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DateTimeVisualizer_KeyDown);
			this.valueGroupBox.ResumeLayout(false);
			this.valueGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox isNullCheckBox;
		private System.Windows.Forms.DateTimePicker datePicker;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker timePicker;
		private System.Windows.Forms.GroupBox valueGroupBox;
	}
}