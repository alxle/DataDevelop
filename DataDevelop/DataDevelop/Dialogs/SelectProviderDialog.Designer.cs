namespace DataDevelop.Dialogs
{
	partial class SelectProviderDialog
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
			this.providersListBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(92, 137);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(173, 137);
			// 
			// providersListBox
			// 
			this.providersListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.providersListBox.FormattingEnabled = true;
			this.providersListBox.Location = new System.Drawing.Point(12, 12);
			this.providersListBox.Name = "providersListBox";
			this.providersListBox.Size = new System.Drawing.Size(236, 108);
			this.providersListBox.TabIndex = 2;
			this.providersListBox.SelectedIndexChanged += new System.EventHandler(this.providersListBox_SelectedIndexChanged);
			this.providersListBox.DoubleClick += new System.EventHandler(this.providersListBox_DoubleClick);
			// 
			// SelectProviderDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(260, 172);
			this.Controls.Add(this.providersListBox);
			this.Name = "SelectProviderDialog";
			this.Text = "Select Data Provider";
			this.Load += new System.EventHandler(this.SelectProviderDialog_Load);
			this.Controls.SetChildIndex(this.providersListBox, 0);
			this.Controls.SetChildIndex(this.okButton, 0);
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox providersListBox;

	}
}