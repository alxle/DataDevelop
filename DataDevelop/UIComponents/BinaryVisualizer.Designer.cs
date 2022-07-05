namespace DataDevelop.UIComponents
{
	partial class BinaryVisualizer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinaryVisualizer));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.sizeModeToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stretchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.viewHexButton = new System.Windows.Forms.ToolStripButton();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.picturePanel = new System.Windows.Forms.Panel();
			this.pictureBox = new DataDevelop.UIComponents.PictureBox();
			this.hexSplitContainer = new System.Windows.Forms.SplitContainer();
			this.hexTextBox = new System.Windows.Forms.RichTextBox();
			this.strTextBox = new System.Windows.Forms.RichTextBox();
			this.toolStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.picturePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.hexSplitContainer.Panel1.SuspendLayout();
			this.hexSplitContainer.Panel2.SuspendLayout();
			this.hexSplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator1,
            this.sizeModeToolStripDropDownButton,
            this.toolStripSeparator2,
            this.viewHexButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(684, 25);
			this.toolStrip.TabIndex = 2;
			// 
			// newToolStripButton
			// 
			this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
			this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripButton.Name = "newToolStripButton";
			this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.newToolStripButton.Text = "&New";
			this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
			this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.openToolStripButton.Text = "&Open";
			this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
			// 
			// saveToolStripButton
			// 
			this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
			this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripButton.Name = "saveToolStripButton";
			this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.saveToolStripButton.Text = "&Save";
			this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// copyToolStripButton
			// 
			this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.copyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
			this.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyToolStripButton.Name = "copyToolStripButton";
			this.copyToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.copyToolStripButton.Text = "&Copy";
			this.copyToolStripButton.Click += new System.EventHandler(this.copyToolStripButton_Click);
			// 
			// pasteToolStripButton
			// 
			this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.pasteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
			this.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteToolStripButton.Name = "pasteToolStripButton";
			this.pasteToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.pasteToolStripButton.Text = "&Paste";
			this.pasteToolStripButton.Click += new System.EventHandler(this.pasteToolStripButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// sizeModeToolStripDropDownButton
			// 
			this.sizeModeToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.sizeModeToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.stretchToolStripMenuItem});
			this.sizeModeToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("sizeModeToolStripDropDownButton.Image")));
			this.sizeModeToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.sizeModeToolStripDropDownButton.Name = "sizeModeToolStripDropDownButton";
			this.sizeModeToolStripDropDownButton.Size = new System.Drawing.Size(29, 22);
			this.sizeModeToolStripDropDownButton.Text = "Size Mode";
			// 
			// normalToolStripMenuItem
			// 
			this.normalToolStripMenuItem.Checked = true;
			this.normalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
			this.normalToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.normalToolStripMenuItem.Text = "Normal";
			this.normalToolStripMenuItem.Click += new System.EventHandler(this.normalToolStripMenuItem_Click);
			// 
			// stretchToolStripMenuItem
			// 
			this.stretchToolStripMenuItem.Name = "stretchToolStripMenuItem";
			this.stretchToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.stretchToolStripMenuItem.Text = "Stretch";
			this.stretchToolStripMenuItem.Click += new System.EventHandler(this.stretchToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// viewHexButton
			// 
			this.viewHexButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.viewHexButton.Name = "viewHexButton";
			this.viewHexButton.Size = new System.Drawing.Size(33, 22);
			this.viewHexButton.Text = "HEX";
			this.viewHexButton.Visible = false;
			this.viewHexButton.Click += new System.EventHandler(this.viewHexButton_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 399);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip.Size = new System.Drawing.Size(684, 22);
			this.statusStrip.TabIndex = 3;
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(638, 17);
			this.statusLabel.Spring = true;
			this.statusLabel.Text = "[Status]";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Title = "Open File...";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Title = "Save to file...";
			// 
			// picturePanel
			// 
			this.picturePanel.AutoScroll = true;
			this.picturePanel.Controls.Add(this.pictureBox);
			this.picturePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picturePanel.Location = new System.Drawing.Point(0, 25);
			this.picturePanel.Name = "picturePanel";
			this.picturePanel.Size = new System.Drawing.Size(684, 374);
			this.picturePanel.TabIndex = 5;
			this.picturePanel.VisibleChanged += new System.EventHandler(this.picturePanel_VisibleChanged);
			this.picturePanel.Resize += new System.EventHandler(this.picturePanel_Resize);
			// 
			// pictureBox
			// 
			this.pictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox.BackgroundImage")));
			this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(200, 200);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox.TabIndex = 4;
			this.pictureBox.TabStop = false;
			// 
			// hexSplitContainer
			// 
			this.hexSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hexSplitContainer.Location = new System.Drawing.Point(0, 25);
			this.hexSplitContainer.Name = "hexSplitContainer";
			// 
			// hexSplitContainer.Panel1
			// 
			this.hexSplitContainer.Panel1.Controls.Add(this.hexTextBox);
			// 
			// hexSplitContainer.Panel2
			// 
			this.hexSplitContainer.Panel2.Controls.Add(this.strTextBox);
			this.hexSplitContainer.Size = new System.Drawing.Size(684, 374);
			this.hexSplitContainer.SplitterDistance = 411;
			this.hexSplitContainer.TabIndex = 6;
			// 
			// hexTextBox
			// 
			this.hexTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.hexTextBox.DetectUrls = false;
			this.hexTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hexTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.hexTextBox.Location = new System.Drawing.Point(0, 0);
			this.hexTextBox.Name = "hexTextBox";
			this.hexTextBox.ReadOnly = true;
			this.hexTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.hexTextBox.Size = new System.Drawing.Size(411, 374);
			this.hexTextBox.TabIndex = 0;
			this.hexTextBox.Text = "";
			// 
			// strTextBox
			// 
			this.strTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.strTextBox.DetectUrls = false;
			this.strTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.strTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.strTextBox.Location = new System.Drawing.Point(0, 0);
			this.strTextBox.Name = "strTextBox";
			this.strTextBox.ReadOnly = true;
			this.strTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.strTextBox.Size = new System.Drawing.Size(269, 374);
			this.strTextBox.TabIndex = 1;
			this.strTextBox.Text = "";
			// 
			// BinaryVisualizer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ClientSize = new System.Drawing.Size(684, 421);
			this.Controls.Add(this.picturePanel);
			this.Controls.Add(this.hexSplitContainer);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "BinaryVisualizer";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Binary Visualizer";
			this.Load += new System.EventHandler(this.BinaryVisualizer_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BinaryVisualizer_KeyDown);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.picturePanel.ResumeLayout(false);
			this.picturePanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.hexSplitContainer.Panel1.ResumeLayout(false);
			this.hexSplitContainer.Panel2.ResumeLayout(false);
			this.hexSplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton newToolStripButton;
		private System.Windows.Forms.ToolStripButton openToolStripButton;
		private System.Windows.Forms.ToolStripButton saveToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripButton copyToolStripButton;
		private System.Windows.Forms.ToolStripButton pasteToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private DataDevelop.UIComponents.PictureBox pictureBox;
		private System.Windows.Forms.ToolStripDropDownButton sizeModeToolStripDropDownButton;
		private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stretchToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Panel picturePanel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton viewHexButton;
		private System.Windows.Forms.SplitContainer hexSplitContainer;
		private System.Windows.Forms.RichTextBox hexTextBox;
		private System.Windows.Forms.RichTextBox strTextBox;
	}
}
