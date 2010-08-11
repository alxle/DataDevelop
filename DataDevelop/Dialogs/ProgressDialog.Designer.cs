namespace DataDevelop.Dialogs
{
	partial class ProgressDialog
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
			this.progressTextBox = new System.Windows.Forms.TextBox();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Enabled = false;
			this.okButton.Location = new System.Drawing.Point(170, 82);
			// 
			// cancelButton
			// 
			this.cancelButton.Enabled = false;
			this.cancelButton.Location = new System.Drawing.Point(251, 82);
			this.cancelButton.Click += new System.EventHandler(this.Cancel);
			// 
			// progressTextBox
			// 
			this.progressTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.progressTextBox.Location = new System.Drawing.Point(13, 12);
			this.progressTextBox.Multiline = true;
			this.progressTextBox.Name = "progressTextBox";
			this.progressTextBox.ReadOnly = true;
			this.progressTextBox.Size = new System.Drawing.Size(313, 32);
			this.progressTextBox.TabIndex = 2;
			this.progressTextBox.TabStop = false;
			this.progressTextBox.WordWrap = false;
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(13, 50);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(313, 17);
			this.progressBar.TabIndex = 3;
			// 
			// ProgressDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(338, 117);
			this.Controls.Add(this.progressTextBox);
			this.Controls.Add(this.progressBar);
			this.Name = "ProgressDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Progress...";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProgressDialog_FormClosing);
			this.Controls.SetChildIndex(this.progressBar, 0);
			this.Controls.SetChildIndex(this.progressTextBox, 0);
			this.Controls.SetChildIndex(this.okButton, 0);
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox progressTextBox;
		private System.Windows.Forms.ProgressBar progressBar;
	}
}
