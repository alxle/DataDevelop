namespace DataDevelop
{
	partial class OutputWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputWindow));
			this.outputMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.wordWrapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.outputTextBox = new DataDevelop.UIComponents.LogTextBox();
			this.outputMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.outputTextBox)).BeginInit();
			this.SuspendLayout();
			// 
			// outputMenuStrip
			// 
			this.outputMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem,
            this.wordWrapToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyToolStripMenuItem});
			this.outputMenuStrip.Name = "outputMenuStrip";
			this.outputMenuStrip.Size = new System.Drawing.Size(135, 76);
			// 
			// clearAllToolStripMenuItem
			// 
			this.clearAllToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.CleanData_16x;
			this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
			this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
			this.clearAllToolStripMenuItem.Text = "Clear All";
			this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.ClearAllButton_Click);
			// 
			// wordWrapToolStripMenuItem
			// 
			this.wordWrapToolStripMenuItem.CheckOnClick = true;
			this.wordWrapToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.WordWrap_16x;
			this.wordWrapToolStripMenuItem.Name = "wordWrapToolStripMenuItem";
			this.wordWrapToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
			this.wordWrapToolStripMenuItem.Text = "Word Wrap";
			this.wordWrapToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ToggleWordWrapButton_CheckedChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.Copy_16x;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
			// 
			// outputTextBox
			// 
			this.outputTextBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
			this.outputTextBox.AutoScrollMinSize = new System.Drawing.Size(2, 12);
			this.outputTextBox.BackBrush = null;
			this.outputTextBox.CharHeight = 12;
			this.outputTextBox.CharWidth = 6;
			this.outputTextBox.ContextMenuStrip = this.outputMenuStrip;
			this.outputTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.outputTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputTextBox.ErrorTextColor = System.Drawing.Color.Red;
			this.outputTextBox.Font = new System.Drawing.Font("Consolas", 8.25F);
			this.outputTextBox.InfoTextColor = System.Drawing.Color.DarkBlue;
			this.outputTextBox.IsReplaceMode = false;
			this.outputTextBox.Location = new System.Drawing.Point(0, 0);
			this.outputTextBox.MessageTextColor = System.Drawing.Color.Black;
			this.outputTextBox.Name = "outputTextBox";
			this.outputTextBox.Paddings = new System.Windows.Forms.Padding(0);
			this.outputTextBox.ReadOnly = true;
			this.outputTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.outputTextBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("outputTextBox.ServiceColors")));
			this.outputTextBox.ShowLineNumbers = false;
			this.outputTextBox.Size = new System.Drawing.Size(490, 170);
			this.outputTextBox.TabIndex = 1;
			this.outputTextBox.Zoom = 100;
			// 
			// OutputWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ClientSize = new System.Drawing.Size(490, 170);
			this.Controls.Add(this.outputTextBox);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OutputWindow";
			this.TabText = "Output";
			this.Text = "Output";
			this.outputMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.outputTextBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip outputMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wordWrapToolStripMenuItem;
		private UIComponents.LogTextBox outputTextBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
	}
}