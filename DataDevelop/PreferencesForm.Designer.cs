namespace DataDevelop
{
	partial class PreferencesForm
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
			this.dataDirectoryLabel = new System.Windows.Forms.Label();
			this.openDataDirectoryButton = new System.Windows.Forms.Button();
			this.dataDirectoryTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.changePrintFontButton = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.applicationPathTextBox = new System.Windows.Forms.TextBox();
			this.openApplicationPathButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.generalTab = new System.Windows.Forms.TabPage();
			this.textEditorTab = new System.Windows.Forms.TabPage();
			this.changeEditorFontButton = new System.Windows.Forms.Button();
			this.editorFontTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tablesTab = new System.Windows.Forms.TabPage();
			this.rowsPerPageNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.printingTab = new System.Windows.Forms.TabPage();
			this.resetButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.generalTab.SuspendLayout();
			this.textEditorTab.SuspendLayout();
			this.tablesTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rowsPerPageNumericUpDown)).BeginInit();
			this.printingTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataDirectoryLabel
			// 
			this.dataDirectoryLabel.AutoSize = true;
			this.dataDirectoryLabel.Location = new System.Drawing.Point(15, 56);
			this.dataDirectoryLabel.Name = "dataDirectoryLabel";
			this.dataDirectoryLabel.Size = new System.Drawing.Size(78, 13);
			this.dataDirectoryLabel.TabIndex = 0;
			this.dataDirectoryLabel.Text = "Data Directory:";
			// 
			// openDataDirectoryButton
			// 
			this.openDataDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.openDataDirectoryButton.Location = new System.Drawing.Point(362, 70);
			this.openDataDirectoryButton.Name = "openDataDirectoryButton";
			this.openDataDirectoryButton.Size = new System.Drawing.Size(54, 23);
			this.openDataDirectoryButton.TabIndex = 1;
			this.openDataDirectoryButton.Text = "Open";
			this.openDataDirectoryButton.UseVisualStyleBackColor = true;
			this.openDataDirectoryButton.Click += new System.EventHandler(this.openDataDirectoryButton_Click);
			// 
			// dataDirectoryTextBox
			// 
			this.dataDirectoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataDirectoryTextBox.Location = new System.Drawing.Point(18, 72);
			this.dataDirectoryTextBox.Name = "dataDirectoryTextBox";
			this.dataDirectoryTextBox.ReadOnly = true;
			this.dataDirectoryTextBox.Size = new System.Drawing.Size(338, 20);
			this.dataDirectoryTextBox.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.changePrintFontButton);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Enabled = false;
			this.groupBox1.Location = new System.Drawing.Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(423, 103);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Printing Preferences";
			// 
			// changePrintFontButton
			// 
			this.changePrintFontButton.Location = new System.Drawing.Point(342, 20);
			this.changePrintFontButton.Name = "changePrintFontButton";
			this.changePrintFontButton.Size = new System.Drawing.Size(75, 23);
			this.changePrintFontButton.TabIndex = 2;
			this.changePrintFontButton.Text = "Change";
			this.changePrintFontButton.UseVisualStyleBackColor = true;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(118, 22);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(218, 20);
			this.textBox1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Default printer font:";
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(297, 173);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(378, 173);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// applicationPathTextBox
			// 
			this.applicationPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.applicationPathTextBox.Location = new System.Drawing.Point(18, 33);
			this.applicationPathTextBox.Name = "applicationPathTextBox";
			this.applicationPathTextBox.ReadOnly = true;
			this.applicationPathTextBox.Size = new System.Drawing.Size(338, 20);
			this.applicationPathTextBox.TabIndex = 8;
			// 
			// openApplicationPathButton
			// 
			this.openApplicationPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.openApplicationPathButton.Location = new System.Drawing.Point(362, 31);
			this.openApplicationPathButton.Name = "openApplicationPathButton";
			this.openApplicationPathButton.Size = new System.Drawing.Size(54, 23);
			this.openApplicationPathButton.TabIndex = 7;
			this.openApplicationPathButton.Text = "Find";
			this.openApplicationPathButton.UseVisualStyleBackColor = true;
			this.openApplicationPathButton.Click += new System.EventHandler(this.openApplicationPathButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(87, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Application Path:";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.generalTab);
			this.tabControl1.Controls.Add(this.textEditorTab);
			this.tabControl1.Controls.Add(this.tablesTab);
			this.tabControl1.Controls.Add(this.printingTab);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(441, 155);
			this.tabControl1.TabIndex = 9;
			// 
			// generalTab
			// 
			this.generalTab.Controls.Add(this.label3);
			this.generalTab.Controls.Add(this.openApplicationPathButton);
			this.generalTab.Controls.Add(this.applicationPathTextBox);
			this.generalTab.Controls.Add(this.dataDirectoryLabel);
			this.generalTab.Controls.Add(this.openDataDirectoryButton);
			this.generalTab.Controls.Add(this.dataDirectoryTextBox);
			this.generalTab.Location = new System.Drawing.Point(4, 22);
			this.generalTab.Name = "generalTab";
			this.generalTab.Padding = new System.Windows.Forms.Padding(3);
			this.generalTab.Size = new System.Drawing.Size(433, 129);
			this.generalTab.TabIndex = 0;
			this.generalTab.Text = "General";
			this.generalTab.UseVisualStyleBackColor = true;
			// 
			// textEditorTab
			// 
			this.textEditorTab.Controls.Add(this.changeEditorFontButton);
			this.textEditorTab.Controls.Add(this.editorFontTextBox);
			this.textEditorTab.Controls.Add(this.label4);
			this.textEditorTab.Location = new System.Drawing.Point(4, 22);
			this.textEditorTab.Name = "textEditorTab";
			this.textEditorTab.Size = new System.Drawing.Size(433, 129);
			this.textEditorTab.TabIndex = 2;
			this.textEditorTab.Text = "Text Editor";
			this.textEditorTab.UseVisualStyleBackColor = true;
			// 
			// changeEditorFontButton
			// 
			this.changeEditorFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.changeEditorFontButton.Location = new System.Drawing.Point(341, 31);
			this.changeEditorFontButton.Name = "changeEditorFontButton";
			this.changeEditorFontButton.Size = new System.Drawing.Size(75, 23);
			this.changeEditorFontButton.TabIndex = 10;
			this.changeEditorFontButton.Text = "Change...";
			this.changeEditorFontButton.UseVisualStyleBackColor = true;
			this.changeEditorFontButton.Click += new System.EventHandler(this.changeEditorFontButton_Click);
			// 
			// editorFontTextBox
			// 
			this.editorFontTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.editorFontTextBox.Location = new System.Drawing.Point(18, 33);
			this.editorFontTextBox.Name = "editorFontTextBox";
			this.editorFontTextBox.ReadOnly = true;
			this.editorFontTextBox.Size = new System.Drawing.Size(317, 20);
			this.editorFontTextBox.TabIndex = 9;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Text Editor Font:";
			// 
			// tablesTab
			// 
			this.tablesTab.Controls.Add(this.rowsPerPageNumericUpDown);
			this.tablesTab.Controls.Add(this.label5);
			this.tablesTab.Location = new System.Drawing.Point(4, 22);
			this.tablesTab.Name = "tablesTab";
			this.tablesTab.Padding = new System.Windows.Forms.Padding(3);
			this.tablesTab.Size = new System.Drawing.Size(433, 129);
			this.tablesTab.TabIndex = 1;
			this.tablesTab.Text = "Tables";
			this.tablesTab.UseVisualStyleBackColor = true;
			// 
			// rowsPerPageNumericUpDown
			// 
			this.rowsPerPageNumericUpDown.Location = new System.Drawing.Point(105, 15);
			this.rowsPerPageNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.rowsPerPageNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.rowsPerPageNumericUpDown.Name = "rowsPerPageNumericUpDown";
			this.rowsPerPageNumericUpDown.Size = new System.Drawing.Size(96, 20);
			this.rowsPerPageNumericUpDown.TabIndex = 1;
			this.rowsPerPageNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.rowsPerPageNumericUpDown.ValueChanged += new System.EventHandler(this.rowsPerPageNumericUpDown_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 17);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Rows Per Page:";
			// 
			// printingTab
			// 
			this.printingTab.Controls.Add(this.groupBox1);
			this.printingTab.Location = new System.Drawing.Point(4, 22);
			this.printingTab.Name = "printingTab";
			this.printingTab.Padding = new System.Windows.Forms.Padding(3);
			this.printingTab.Size = new System.Drawing.Size(433, 129);
			this.printingTab.TabIndex = 3;
			this.printingTab.Text = "Printing";
			this.printingTab.UseVisualStyleBackColor = true;
			// 
			// resetButton
			// 
			this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.resetButton.Location = new System.Drawing.Point(12, 173);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(75, 23);
			this.resetButton.TabIndex = 10;
			this.resetButton.Text = "&Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// PreferencesForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(465, 208);
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PreferencesForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Preferences";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.generalTab.ResumeLayout(false);
			this.generalTab.PerformLayout();
			this.textEditorTab.ResumeLayout(false);
			this.textEditorTab.PerformLayout();
			this.tablesTab.ResumeLayout(false);
			this.tablesTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rowsPerPageNumericUpDown)).EndInit();
			this.printingTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label dataDirectoryLabel;
		private System.Windows.Forms.Button openDataDirectoryButton;
		private System.Windows.Forms.TextBox dataDirectoryTextBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button changePrintFontButton;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.PageSetupDialog pageSetupDialog;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox applicationPathTextBox;
		private System.Windows.Forms.Button openApplicationPathButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage generalTab;
		private System.Windows.Forms.TabPage tablesTab;
		private System.Windows.Forms.TabPage textEditorTab;
		private System.Windows.Forms.TabPage printingTab;
		private System.Windows.Forms.Button changeEditorFontButton;
		private System.Windows.Forms.TextBox editorFontTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown rowsPerPageNumericUpDown;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button resetButton;
	}
}