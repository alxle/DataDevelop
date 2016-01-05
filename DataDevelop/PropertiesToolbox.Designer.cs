namespace DataDevelop
{
	partial class PropertiesToolbox
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesToolbox));
			this.propertyGrid = new DataDevelop.PropertyGrid();
			this.SuspendLayout();
			// 
			// propertyGrid
			// 
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.HelpVisible = false;
			this.propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.propertyGrid.Size = new System.Drawing.Size(235, 243);
			this.propertyGrid.TabIndex = 0;
			this.propertyGrid.ToolbarVisible = false;
			// 
			// PropertiesToolbox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(235, 243);
			this.Controls.Add(this.propertyGrid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PropertiesToolbox";
			this.Padding = new System.Windows.Forms.Padding(0);
			this.TabText = "Properties";
			this.Text = "Properties";
			this.ResumeLayout(false);

		}

		#endregion

		private PropertyGrid propertyGrid;
	}
}
