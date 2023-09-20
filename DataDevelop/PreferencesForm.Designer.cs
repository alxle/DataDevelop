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
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.applicationPathTextBox = new System.Windows.Forms.TextBox();
			this.openApplicationPathButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.generalTab = new System.Windows.Forms.TabPage();
			this.rowsPerPageNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.appearanceTab = new System.Windows.Forms.TabPage();
			this.requiresRestartLabel = new System.Windows.Forms.Label();
			this.visualStyleComboBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.changeVisualizerFontButton = new System.Windows.Forms.Button();
			this.visualizerFontTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.changeEditorFontButton = new System.Windows.Forms.Button();
			this.editorFontTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.resetButton = new System.Windows.Forms.Button();
			this.queryHistoryCheckBox = new System.Windows.Forms.CheckBox();
			this.tabControl1.SuspendLayout();
			this.generalTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rowsPerPageNumericUpDown)).BeginInit();
			this.appearanceTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataDirectoryLabel
			// 
			this.dataDirectoryLabel.AutoSize = true;
			this.dataDirectoryLabel.Location = new System.Drawing.Point(14, 92);
			this.dataDirectoryLabel.Name = "dataDirectoryLabel";
			this.dataDirectoryLabel.Size = new System.Drawing.Size(78, 13);
			this.dataDirectoryLabel.TabIndex = 0;
			this.dataDirectoryLabel.Text = "Data Directory:";
			// 
			// openDataDirectoryButton
			// 
			this.openDataDirectoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.openDataDirectoryButton.Location = new System.Drawing.Point(361, 106);
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
			this.dataDirectoryTextBox.Location = new System.Drawing.Point(17, 108);
			this.dataDirectoryTextBox.Name = "dataDirectoryTextBox";
			this.dataDirectoryTextBox.ReadOnly = true;
			this.dataDirectoryTextBox.Size = new System.Drawing.Size(338, 20);
			this.dataDirectoryTextBox.TabIndex = 2;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(297, 189);
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
			this.cancelButton.Location = new System.Drawing.Point(378, 189);
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
			this.applicationPathTextBox.Location = new System.Drawing.Point(17, 69);
			this.applicationPathTextBox.Name = "applicationPathTextBox";
			this.applicationPathTextBox.ReadOnly = true;
			this.applicationPathTextBox.Size = new System.Drawing.Size(338, 20);
			this.applicationPathTextBox.TabIndex = 8;
			// 
			// openApplicationPathButton
			// 
			this.openApplicationPathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.openApplicationPathButton.Location = new System.Drawing.Point(361, 67);
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
			this.label3.Location = new System.Drawing.Point(14, 53);
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
			this.tabControl1.Controls.Add(this.appearanceTab);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(441, 171);
			this.tabControl1.TabIndex = 9;
			// 
			// generalTab
			// 
			this.generalTab.Controls.Add(this.queryHistoryCheckBox);
			this.generalTab.Controls.Add(this.rowsPerPageNumericUpDown);
			this.generalTab.Controls.Add(this.label2);
			this.generalTab.Controls.Add(this.label3);
			this.generalTab.Controls.Add(this.openApplicationPathButton);
			this.generalTab.Controls.Add(this.applicationPathTextBox);
			this.generalTab.Controls.Add(this.dataDirectoryLabel);
			this.generalTab.Controls.Add(this.openDataDirectoryButton);
			this.generalTab.Controls.Add(this.dataDirectoryTextBox);
			this.generalTab.Location = new System.Drawing.Point(4, 22);
			this.generalTab.Name = "generalTab";
			this.generalTab.Padding = new System.Windows.Forms.Padding(3);
			this.generalTab.Size = new System.Drawing.Size(433, 145);
			this.generalTab.TabIndex = 0;
			this.generalTab.Text = "General";
			this.generalTab.UseVisualStyleBackColor = true;
			// 
			// rowsPerPageNumericUpDown
			// 
			this.rowsPerPageNumericUpDown.Location = new System.Drawing.Point(104, 21);
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
			this.rowsPerPageNumericUpDown.TabIndex = 10;
			this.rowsPerPageNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.rowsPerPageNumericUpDown.ValueChanged += new System.EventHandler(this.rowsPerPageNumericUpDown_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Rows Per Page:";
			// 
			// appearanceTab
			// 
			this.appearanceTab.Controls.Add(this.requiresRestartLabel);
			this.appearanceTab.Controls.Add(this.visualStyleComboBox);
			this.appearanceTab.Controls.Add(this.label5);
			this.appearanceTab.Controls.Add(this.changeVisualizerFontButton);
			this.appearanceTab.Controls.Add(this.visualizerFontTextBox);
			this.appearanceTab.Controls.Add(this.label1);
			this.appearanceTab.Controls.Add(this.changeEditorFontButton);
			this.appearanceTab.Controls.Add(this.editorFontTextBox);
			this.appearanceTab.Controls.Add(this.label4);
			this.appearanceTab.Location = new System.Drawing.Point(4, 22);
			this.appearanceTab.Name = "appearanceTab";
			this.appearanceTab.Size = new System.Drawing.Size(433, 145);
			this.appearanceTab.TabIndex = 2;
			this.appearanceTab.Text = "Appearance";
			this.appearanceTab.UseVisualStyleBackColor = true;
			// 
			// requiresRestartLabel
			// 
			this.requiresRestartLabel.AutoSize = true;
			this.requiresRestartLabel.Location = new System.Drawing.Point(211, 23);
			this.requiresRestartLabel.Name = "requiresRestartLabel";
			this.requiresRestartLabel.Size = new System.Drawing.Size(175, 13);
			this.requiresRestartLabel.TabIndex = 16;
			this.requiresRestartLabel.Text = "(change requires restart application)";
			// 
			// visualStyleComboBox
			// 
			this.visualStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.visualStyleComboBox.FormattingEnabled = true;
			this.visualStyleComboBox.Items.AddRange(new object[] {
            "Classic",
            "Light",
            "Blue",
            "Dark"});
			this.visualStyleComboBox.Location = new System.Drawing.Point(84, 20);
			this.visualStyleComboBox.Name = "visualStyleComboBox";
			this.visualStyleComboBox.Size = new System.Drawing.Size(121, 21);
			this.visualStyleComboBox.TabIndex = 15;
			this.visualStyleComboBox.SelectedIndexChanged += new System.EventHandler(this.visualStyleComboBox_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 23);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 13);
			this.label5.TabIndex = 14;
			this.label5.Text = "Visual Style:";
			// 
			// changeVisualizerFontButton
			// 
			this.changeVisualizerFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.changeVisualizerFontButton.Location = new System.Drawing.Point(340, 106);
			this.changeVisualizerFontButton.Name = "changeVisualizerFontButton";
			this.changeVisualizerFontButton.Size = new System.Drawing.Size(75, 23);
			this.changeVisualizerFontButton.TabIndex = 13;
			this.changeVisualizerFontButton.Text = "Change...";
			this.changeVisualizerFontButton.UseVisualStyleBackColor = true;
			this.changeVisualizerFontButton.Click += new System.EventHandler(this.changeVisualizerFontButton_Click);
			// 
			// visualizerFontTextBox
			// 
			this.visualizerFontTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.visualizerFontTextBox.Location = new System.Drawing.Point(17, 108);
			this.visualizerFontTextBox.Name = "visualizerFontTextBox";
			this.visualizerFontTextBox.ReadOnly = true;
			this.visualizerFontTextBox.Size = new System.Drawing.Size(317, 20);
			this.visualizerFontTextBox.TabIndex = 12;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 92);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(102, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Text Visualizer Font:";
			// 
			// changeEditorFontButton
			// 
			this.changeEditorFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.changeEditorFontButton.Location = new System.Drawing.Point(340, 67);
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
			this.editorFontTextBox.Location = new System.Drawing.Point(17, 69);
			this.editorFontTextBox.Name = "editorFontTextBox";
			this.editorFontTextBox.ReadOnly = true;
			this.editorFontTextBox.Size = new System.Drawing.Size(317, 20);
			this.editorFontTextBox.TabIndex = 9;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Text Editor Font:";
			// 
			// resetButton
			// 
			this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.resetButton.Location = new System.Drawing.Point(12, 189);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(75, 23);
			this.resetButton.TabIndex = 10;
			this.resetButton.Text = "&Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// queryHistoryCheckBox
			// 
			this.queryHistoryCheckBox.AutoSize = true;
			this.queryHistoryCheckBox.Checked = global::DataDevelop.Properties.Settings.Default.QueryHistoryEnabled;
			this.queryHistoryCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::DataDevelop.Properties.Settings.Default, "QueryHistoryEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.queryHistoryCheckBox.Location = new System.Drawing.Point(247, 22);
			this.queryHistoryCheckBox.Name = "queryHistoryCheckBox";
			this.queryHistoryCheckBox.Size = new System.Drawing.Size(125, 17);
			this.queryHistoryCheckBox.TabIndex = 11;
			this.queryHistoryCheckBox.Text = "Enable Query History";
			this.queryHistoryCheckBox.UseVisualStyleBackColor = true;
			// 
			// PreferencesForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(465, 224);
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
			this.tabControl1.ResumeLayout(false);
			this.generalTab.ResumeLayout(false);
			this.generalTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.rowsPerPageNumericUpDown)).EndInit();
			this.appearanceTab.ResumeLayout(false);
			this.appearanceTab.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label dataDirectoryLabel;
		private System.Windows.Forms.Button openDataDirectoryButton;
		private System.Windows.Forms.TextBox dataDirectoryTextBox;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.PageSetupDialog pageSetupDialog;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox applicationPathTextBox;
		private System.Windows.Forms.Button openApplicationPathButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage generalTab;
		private System.Windows.Forms.TabPage appearanceTab;
		private System.Windows.Forms.Button changeEditorFontButton;
		private System.Windows.Forms.TextBox editorFontTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.NumericUpDown rowsPerPageNumericUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox visualStyleComboBox;
		private System.Windows.Forms.Button changeVisualizerFontButton;
		private System.Windows.Forms.TextBox visualizerFontTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label requiresRestartLabel;
		private System.Windows.Forms.CheckBox queryHistoryCheckBox;
	}
}