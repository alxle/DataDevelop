namespace DataDevelop
{
	partial class WebBrowser
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebBrowser));
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.backButton = new System.Windows.Forms.ToolStripButton();
			this.forwardButton = new System.Windows.Forms.ToolStripButton();
			this.stopButton = new System.Windows.Forms.ToolStripButton();
			this.refreshButton = new System.Windows.Forms.ToolStripButton();
			this.addressComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.goButton = new System.Windows.Forms.ToolStripButton();
			this.loadingToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.browserProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.browser = new System.Windows.Forms.WebBrowser();
			this.mainToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.forwardButton,
            this.stopButton,
            this.refreshButton,
            this.addressComboBox,
            this.goButton,
            this.loadingToolStripLabel,
            this.browserProgressBar});
			this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(555, 25);
			this.mainToolStrip.TabIndex = 0;
			this.mainToolStrip.Text = "toolStrip1";
			// 
			// backButton
			// 
			this.backButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.backButton.Enabled = false;
			this.backButton.Image = ((System.Drawing.Image)(resources.GetObject("backButton.Image")));
			this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.backButton.Name = "backButton";
			this.backButton.Size = new System.Drawing.Size(23, 22);
			this.backButton.Text = "Back";
			this.backButton.Click += new System.EventHandler(this.backButton_Click);
			// 
			// forwardButton
			// 
			this.forwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.forwardButton.Enabled = false;
			this.forwardButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardButton.Image")));
			this.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.forwardButton.Name = "forwardButton";
			this.forwardButton.Size = new System.Drawing.Size(23, 22);
			this.forwardButton.Text = "Forward";
			this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
			// 
			// stopButton
			// 
			this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.stopButton.Enabled = false;
			this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
			this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(23, 22);
			this.stopButton.Text = "Stop";
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// refreshButton
			// 
			this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
			this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(23, 22);
			this.refreshButton.Text = "Refresh";
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// addressComboBox
			// 
			this.addressComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.addressComboBox.Name = "addressComboBox";
			this.addressComboBox.Size = new System.Drawing.Size(200, 25);
			this.addressComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressComboBox_KeyDown);
			// 
			// goButton
			// 
			this.goButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.goButton.Image = global::DataDevelop.Properties.Resources._0118;
			this.goButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.goButton.Name = "goButton";
			this.goButton.Size = new System.Drawing.Size(23, 22);
			this.goButton.Text = "Go";
			this.goButton.Click += new System.EventHandler(this.goButton_Click);
			// 
			// loadingToolStripLabel
			// 
			this.loadingToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.loadingToolStripLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.loadingToolStripLabel.Image = ((System.Drawing.Image)(resources.GetObject("loadingToolStripLabel.Image")));
			this.loadingToolStripLabel.Name = "loadingToolStripLabel";
			this.loadingToolStripLabel.Size = new System.Drawing.Size(16, 22);
			this.loadingToolStripLabel.Text = "Loading...";
			this.loadingToolStripLabel.Visible = false;
			// 
			// browserProgressBar
			// 
			this.browserProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.browserProgressBar.Name = "browserProgressBar";
			this.browserProgressBar.Size = new System.Drawing.Size(100, 22);
			this.browserProgressBar.Visible = false;
			// 
			// browser
			// 
			this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.browser.Location = new System.Drawing.Point(0, 25);
			this.browser.MinimumSize = new System.Drawing.Size(20, 20);
			this.browser.Name = "browser";
			this.browser.Size = new System.Drawing.Size(555, 387);
			this.browser.TabIndex = 2;
			this.browser.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.browser_ProgressChanged);
			this.browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.browser_Navigating);
			this.browser.NewWindow += new System.ComponentModel.CancelEventHandler(this.browser_NewWindow);
			this.browser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.browser_Navigated);
			// 
			// WebBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(555, 412);
			this.Controls.Add(this.browser);
			this.Controls.Add(this.mainToolStrip);
			this.DocumentName = "Web Browser";
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "WebBrowser";
			this.TabText = "Web Browser";
			this.Text = "Web Browser";
			this.Load += new System.EventHandler(this.WebBrowser_Load);
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.WebBrowser browser;
		private System.Windows.Forms.ToolStripComboBox addressComboBox;
		private System.Windows.Forms.ToolStripButton backButton;
		private System.Windows.Forms.ToolStripButton forwardButton;
		private System.Windows.Forms.ToolStripButton refreshButton;
		private System.Windows.Forms.ToolStripButton goButton;
		private System.Windows.Forms.ToolStripLabel loadingToolStripLabel;
		private System.Windows.Forms.ToolStripProgressBar browserProgressBar;
		private System.Windows.Forms.ToolStripButton stopButton;
	}
}
