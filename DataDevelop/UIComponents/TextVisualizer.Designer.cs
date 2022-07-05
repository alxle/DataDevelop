namespace DataDevelop.UIComponents
{
	partial class TextVisualizer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextVisualizer));
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toggleWordWrapButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.actionsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.changeFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.formatXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.syntaxDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.plainTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.htmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.javaScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.xmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.textEditor = new FastColoredTextBoxNS.FastColoredTextBox();
			this.toolStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditor)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripSeparator1,
            this.toggleWordWrapButton,
            this.toolStripSeparator2,
            this.actionsToolStripDropDownButton,
            this.toolStripSeparator3,
            this.syntaxDropDownButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(684, 25);
			this.toolStrip.TabIndex = 3;
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
			// cutToolStripButton
			// 
			this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
			this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cutToolStripButton.Name = "cutToolStripButton";
			this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.cutToolStripButton.Text = "C&ut";
			this.cutToolStripButton.Click += new System.EventHandler(this.cutToolStripButton_Click);
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
			this.toggleWordWrapButton.Click += new System.EventHandler(this.toggleWordWrapButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// actionsToolStripDropDownButton
			// 
			this.actionsToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.actionsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeFontToolStripMenuItem,
            this.formatXmlToolStripMenuItem});
			this.actionsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.actionsToolStripDropDownButton.Name = "actionsToolStripDropDownButton";
			this.actionsToolStripDropDownButton.Size = new System.Drawing.Size(60, 22);
			this.actionsToolStripDropDownButton.Text = "Actions";
			// 
			// changeFontToolStripMenuItem
			// 
			this.changeFontToolStripMenuItem.Name = "changeFontToolStripMenuItem";
			this.changeFontToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.changeFontToolStripMenuItem.Text = "Change Font...";
			this.changeFontToolStripMenuItem.Click += new System.EventHandler(this.changeFontToolStripMenuItem_Click);
			// 
			// formatXmlToolStripMenuItem
			// 
			this.formatXmlToolStripMenuItem.Name = "formatXmlToolStripMenuItem";
			this.formatXmlToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.formatXmlToolStripMenuItem.Text = "Format XML";
			this.formatXmlToolStripMenuItem.Click += new System.EventHandler(this.formatXmlToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// syntaxDropDownButton
			// 
			this.syntaxDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.syntaxDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plainTextToolStripMenuItem,
            this.toolStripSeparator4,
            this.htmlToolStripMenuItem,
            this.javaScriptToolStripMenuItem,
            this.sqlToolStripMenuItem,
            this.xmlToolStripMenuItem});
			this.syntaxDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.syntaxDropDownButton.Name = "syntaxDropDownButton";
			this.syntaxDropDownButton.Size = new System.Drawing.Size(55, 22);
			this.syntaxDropDownButton.Text = "Syntax";
			// 
			// plainTextToolStripMenuItem
			// 
			this.plainTextToolStripMenuItem.Checked = true;
			this.plainTextToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.plainTextToolStripMenuItem.Name = "plainTextToolStripMenuItem";
			this.plainTextToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.plainTextToolStripMenuItem.Text = "Plain Text";
			this.plainTextToolStripMenuItem.Click += new System.EventHandler(this.plainTextToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(123, 6);
			// 
			// htmlToolStripMenuItem
			// 
			this.htmlToolStripMenuItem.Name = "htmlToolStripMenuItem";
			this.htmlToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.htmlToolStripMenuItem.Text = "HTML";
			this.htmlToolStripMenuItem.Click += new System.EventHandler(this.htmlToolStripMenuItem_Click);
			// 
			// javaScriptToolStripMenuItem
			// 
			this.javaScriptToolStripMenuItem.Name = "javaScriptToolStripMenuItem";
			this.javaScriptToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.javaScriptToolStripMenuItem.Text = "JavaScript";
			this.javaScriptToolStripMenuItem.Click += new System.EventHandler(this.javaScriptToolStripMenuItem_Click);
			// 
			// sqlToolStripMenuItem
			// 
			this.sqlToolStripMenuItem.Name = "sqlToolStripMenuItem";
			this.sqlToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.sqlToolStripMenuItem.Text = "SQL";
			this.sqlToolStripMenuItem.Click += new System.EventHandler(this.sqlToolStripMenuItem_Click);
			// 
			// xmlToolStripMenuItem
			// 
			this.xmlToolStripMenuItem.Name = "xmlToolStripMenuItem";
			this.xmlToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.xmlToolStripMenuItem.Text = "XML";
			this.xmlToolStripMenuItem.Click += new System.EventHandler(this.xmlToolStripMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 399);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip.Size = new System.Drawing.Size(684, 22);
			this.statusStrip.TabIndex = 4;
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(669, 17);
			this.statusLabel.Spring = true;
			this.statusLabel.Text = "Text Length: 0";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Title = "Save to file...";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Title = "Open File...";
			// 
			// textEditor
			// 
			this.textEditor.AutoCompleteBracketsList = new char[] {
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
			this.textEditor.AutoIndentExistingLines = false;
			this.textEditor.AutoScrollMinSize = new System.Drawing.Size(27, 14);
			this.textEditor.BackBrush = null;
			this.textEditor.CharHeight = 14;
			this.textEditor.CharWidth = 8;
			this.textEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.textEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.textEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textEditor.IsReplaceMode = false;
			this.textEditor.Location = new System.Drawing.Point(0, 25);
			this.textEditor.Name = "textEditor";
			this.textEditor.Paddings = new System.Windows.Forms.Padding(0);
			this.textEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.textEditor.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("textEditor.ServiceColors")));
			this.textEditor.Size = new System.Drawing.Size(684, 374);
			this.textEditor.TabIndex = 6;
			this.textEditor.Zoom = 100;
			// 
			// TextVisualizer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 421);
			this.Controls.Add(this.textEditor);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TextVisualizer";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Text Visualizer";
			this.Load += new System.EventHandler(this.TextVisualizer_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.textEditor)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripButton newToolStripButton;
		private System.Windows.Forms.ToolStripButton openToolStripButton;
		private System.Windows.Forms.ToolStripButton saveToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripButton cutToolStripButton;
		private System.Windows.Forms.ToolStripButton copyToolStripButton;
		private System.Windows.Forms.ToolStripButton pasteToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toggleWordWrapButton;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ToolStripDropDownButton actionsToolStripDropDownButton;
		private System.Windows.Forms.ToolStripMenuItem changeFontToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem formatXmlToolStripMenuItem;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripDropDownButton syntaxDropDownButton;
		private System.Windows.Forms.ToolStripMenuItem plainTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem htmlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem javaScriptToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sqlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem xmlToolStripMenuItem;
		private FastColoredTextBoxNS.FastColoredTextBox textEditor;
	}
}
