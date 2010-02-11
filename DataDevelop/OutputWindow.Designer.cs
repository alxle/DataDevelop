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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputWindow));
			this.results = new System.Windows.Forms.RichTextBox();
			this.mainToolStrip = new DataDevelop.UIComponents.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.outputsComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.clearAllButton = new System.Windows.Forms.ToolStripButton();
			this.toggleWordWrapButton = new System.Windows.Forms.ToolStripButton();
			this.mainToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// results
			// 
			this.results.BackColor = System.Drawing.SystemColors.Window;
			this.results.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.results.Dock = System.Windows.Forms.DockStyle.Fill;
			this.results.Location = new System.Drawing.Point(1, 26);
			this.results.Name = "results";
			this.results.ReadOnly = true;
			this.results.Size = new System.Drawing.Size(488, 143);
			this.results.TabIndex = 0;
			this.results.Text = "";
			this.results.WordWrap = false;
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.outputsComboBox,
            this.clearAllButton,
            this.toggleWordWrapButton});
			this.mainToolStrip.Location = new System.Drawing.Point(1, 1);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(488, 25);
			this.mainToolStrip.TabIndex = 1;
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Enabled = false;
			this.toolStripLabel1.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(101, 22);
			this.toolStripLabel1.Text = "Show Output From:";
			// 
			// outputsComboBox
			// 
			this.outputsComboBox.Enabled = false;
			this.outputsComboBox.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.outputsComboBox.Name = "outputsComboBox";
			this.outputsComboBox.Size = new System.Drawing.Size(121, 25);
			// 
			// clearAllButton
			// 
			this.clearAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.clearAllButton.Image = ((System.Drawing.Image)(resources.GetObject("clearAllButton.Image")));
			this.clearAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.clearAllButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.clearAllButton.Name = "clearAllButton";
			this.clearAllButton.Size = new System.Drawing.Size(23, 22);
			this.clearAllButton.Text = "Clear All Output";
			this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
			// 
			// toggleWordWrapButton
			// 
			this.toggleWordWrapButton.CheckOnClick = true;
			this.toggleWordWrapButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toggleWordWrapButton.Image = ((System.Drawing.Image)(resources.GetObject("toggleWordWrapButton.Image")));
			this.toggleWordWrapButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toggleWordWrapButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.toggleWordWrapButton.Name = "toggleWordWrapButton";
			this.toggleWordWrapButton.Size = new System.Drawing.Size(23, 22);
			this.toggleWordWrapButton.Text = "Toggle Word Wrap";
			this.toggleWordWrapButton.CheckedChanged += new System.EventHandler(this.toggleWordWrapButton_CheckedChanged);
			// 
			// OutputWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ClientSize = new System.Drawing.Size(490, 170);
			this.Controls.Add(this.results);
			this.Controls.Add(this.mainToolStrip);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OutputWindow";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.TabText = "Output";
			this.Text = "Output";
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox results;
		private DataDevelop.UIComponents.ToolStrip mainToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox outputsComboBox;
		private System.Windows.Forms.ToolStripButton clearAllButton;
		private System.Windows.Forms.ToolStripButton toggleWordWrapButton;
	}
}