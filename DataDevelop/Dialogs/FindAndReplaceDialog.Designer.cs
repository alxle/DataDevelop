using DataDevelop.UIComponents;

namespace DataDevelop.Dialogs
{
	partial class FindAndReplaceDialog
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
			if (disposing && (components != null))
			{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindAndReplaceDialog));
			this.matchToolStrip = new System.Windows.Forms.ToolStrip();
			this.matchLabel = new System.Windows.Forms.ToolStripLabel();
			this.matchCaseCheckButton = new System.Windows.Forms.ToolStripButton();
			this.matchWholeWordCheckButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.highlightAllButton = new System.Windows.Forms.ToolStripButton();
			this.findToolStrip = new System.Windows.Forms.ToolStrip();
			this.findLabel = new System.Windows.Forms.ToolStripLabel();
			this.findTextBox = new DataDevelop.UIComponents.ToolStripSpringTextBox();
			this.findNextButton = new System.Windows.Forms.ToolStripSplitButton();
			this.findPreviousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeButton = new System.Windows.Forms.ToolStripButton();
			this.replaceToolStrip = new System.Windows.Forms.ToolStrip();
			this.replaceLabel = new System.Windows.Forms.ToolStripLabel();
			this.replaceTextBox = new DataDevelop.UIComponents.ToolStripSpringTextBox();
			this.replaceButton = new System.Windows.Forms.ToolStripButton();
			this.replaceAllButton = new System.Windows.Forms.ToolStripButton();
			this.matchToolStrip.SuspendLayout();
			this.findToolStrip.SuspendLayout();
			this.replaceToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// matchToolStrip
			// 
			this.matchToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.matchToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.matchLabel,
            this.matchCaseCheckButton,
            this.matchWholeWordCheckButton,
            this.toolStripSeparator1,
            this.highlightAllButton});
			this.matchToolStrip.Location = new System.Drawing.Point(0, 50);
			this.matchToolStrip.Name = "matchToolStrip";
			this.matchToolStrip.Size = new System.Drawing.Size(333, 25);
			this.matchToolStrip.TabIndex = 2;
			this.matchToolStrip.Text = "toolStrip1";
			// 
			// matchLabel
			// 
			this.matchLabel.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
			this.matchLabel.Name = "matchLabel";
			this.matchLabel.Size = new System.Drawing.Size(44, 22);
			this.matchLabel.Text = "Match:";
			// 
			// matchCaseCheckButton
			// 
			this.matchCaseCheckButton.CheckOnClick = true;
			this.matchCaseCheckButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.matchCaseCheckButton.Image = global::DataDevelop.Properties.Resources.CaseSensitive_16x;
			this.matchCaseCheckButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.matchCaseCheckButton.Name = "matchCaseCheckButton";
			this.matchCaseCheckButton.Size = new System.Drawing.Size(23, 22);
			this.matchCaseCheckButton.Text = "Match case";
			// 
			// matchWholeWordCheckButton
			// 
			this.matchWholeWordCheckButton.CheckOnClick = true;
			this.matchWholeWordCheckButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.matchWholeWordCheckButton.Image = ((System.Drawing.Image)(resources.GetObject("matchWholeWordCheckButton.Image")));
			this.matchWholeWordCheckButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.matchWholeWordCheckButton.Name = "matchWholeWordCheckButton";
			this.matchWholeWordCheckButton.Size = new System.Drawing.Size(23, 22);
			this.matchWholeWordCheckButton.Text = "Match whole word";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// highlightAllButton
			// 
			this.highlightAllButton.Image = global::DataDevelop.Properties.Resources.HighlightText_16x;
			this.highlightAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.highlightAllButton.Name = "highlightAllButton";
			this.highlightAllButton.Size = new System.Drawing.Size(94, 22);
			this.highlightAllButton.Text = "Highlight All";
			// 
			// findToolStrip
			// 
			this.findToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.findToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findLabel,
            this.findTextBox,
            this.findNextButton,
            this.closeButton});
			this.findToolStrip.Location = new System.Drawing.Point(0, 0);
			this.findToolStrip.Name = "findToolStrip";
			this.findToolStrip.Size = new System.Drawing.Size(333, 25);
			this.findToolStrip.TabIndex = 0;
			this.findToolStrip.Text = "toolStrip2";
			// 
			// findLabel
			// 
			this.findLabel.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
			this.findLabel.Name = "findLabel";
			this.findLabel.Size = new System.Drawing.Size(56, 22);
			this.findLabel.Text = "Find text:";
			// 
			// findTextBox
			// 
			this.findTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.findTextBox.Name = "findTextBox";
			this.findTextBox.Size = new System.Drawing.Size(180, 25);
			this.findTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindTextBox_KeyDown);
			// 
			// findNextButton
			// 
			this.findNextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.findNextButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findPreviousToolStripMenuItem});
			this.findNextButton.Image = global::DataDevelop.Properties.Resources.FindNext_16x;
			this.findNextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.findNextButton.Name = "findNextButton";
			this.findNextButton.Size = new System.Drawing.Size(32, 22);
			this.findNextButton.Text = "Find next";
			this.findNextButton.ButtonClick += new System.EventHandler(this.FindNext_Click);
			// 
			// findPreviousToolStripMenuItem
			// 
			this.findPreviousToolStripMenuItem.Image = global::DataDevelop.Properties.Resources.FindPrevious_16x;
			this.findPreviousToolStripMenuItem.Name = "findPreviousToolStripMenuItem";
			this.findPreviousToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
			this.findPreviousToolStripMenuItem.Text = "Find previous";
			this.findPreviousToolStripMenuItem.Click += new System.EventHandler(this.FindPrevious_Click);
			// 
			// closeButton
			// 
			this.closeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.closeButton.Image = global::DataDevelop.Properties.Resources.Close_16x;
			this.closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(23, 22);
			this.closeButton.Text = "Close";
			this.closeButton.ToolTipText = "Close";
			this.closeButton.Visible = false;
			this.closeButton.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// replaceToolStrip
			// 
			this.replaceToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.replaceToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.replaceLabel,
            this.replaceTextBox,
            this.replaceButton,
            this.replaceAllButton});
			this.replaceToolStrip.Location = new System.Drawing.Point(0, 25);
			this.replaceToolStrip.Name = "replaceToolStrip";
			this.replaceToolStrip.Size = new System.Drawing.Size(333, 25);
			this.replaceToolStrip.TabIndex = 1;
			this.replaceToolStrip.Text = "toolStrip3";
			// 
			// replaceLabel
			// 
			this.replaceLabel.Margin = new System.Windows.Forms.Padding(8, 1, 0, 2);
			this.replaceLabel.Name = "replaceLabel";
			this.replaceLabel.Size = new System.Drawing.Size(77, 22);
			this.replaceLabel.Text = "Replace with:";
			// 
			// replaceTextBox
			// 
			this.replaceTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.replaceTextBox.Name = "replaceTextBox";
			this.replaceTextBox.Size = new System.Drawing.Size(168, 25);
			// 
			// replaceButton
			// 
			this.replaceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.replaceButton.Image = global::DataDevelop.Properties.Resources.QuickReplace_16x;
			this.replaceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.replaceButton.Name = "replaceButton";
			this.replaceButton.Size = new System.Drawing.Size(23, 22);
			this.replaceButton.Text = "Replace";
			this.replaceButton.Click += new System.EventHandler(this.Replace_Click);
			// 
			// replaceAllButton
			// 
			this.replaceAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.replaceAllButton.Image = global::DataDevelop.Properties.Resources.ReplaceAll_16x;
			this.replaceAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.replaceAllButton.Name = "replaceAllButton";
			this.replaceAllButton.Size = new System.Drawing.Size(23, 22);
			this.replaceAllButton.Text = "Replace All";
			this.replaceAllButton.Click += new System.EventHandler(this.ReplaceAll_Click);
			// 
			// FindAndReplaceDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(333, 74);
			this.Controls.Add(this.matchToolStrip);
			this.Controls.Add(this.replaceToolStrip);
			this.Controls.Add(this.findToolStrip);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FindAndReplaceDialog";
			this.ShowIcon = false;
			this.Text = "Find and replace";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindAndReplaceForm_FormClosing);
			this.Load += new System.EventHandler(this.FindAndReplaceDialog_Load);
			this.matchToolStrip.ResumeLayout(false);
			this.matchToolStrip.PerformLayout();
			this.findToolStrip.ResumeLayout(false);
			this.findToolStrip.PerformLayout();
			this.replaceToolStrip.ResumeLayout(false);
			this.replaceToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip matchToolStrip;
		private System.Windows.Forms.ToolStripButton matchCaseCheckButton;
		private System.Windows.Forms.ToolStripButton matchWholeWordCheckButton;
		private System.Windows.Forms.ToolStripButton highlightAllButton;
		private System.Windows.Forms.ToolStrip findToolStrip;
		private System.Windows.Forms.ToolStripSplitButton findNextButton;
		private System.Windows.Forms.ToolStripButton closeButton;
		private System.Windows.Forms.ToolStrip replaceToolStrip;
		private System.Windows.Forms.ToolStripButton replaceButton;
		private System.Windows.Forms.ToolStripButton replaceAllButton;
		private System.Windows.Forms.ToolStripMenuItem findPreviousToolStripMenuItem;
		private System.Windows.Forms.ToolStripLabel matchLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel findLabel;
		private ToolStripSpringTextBox findTextBox;
		private System.Windows.Forms.ToolStripLabel replaceLabel;
		private ToolStripSpringTextBox replaceTextBox;
	}
}
