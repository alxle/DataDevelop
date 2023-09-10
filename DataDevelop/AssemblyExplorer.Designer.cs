namespace DataDevelop
{
	partial class AssemblyExplorer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssemblyExplorer));
			this.toolStrip = new DataDevelop.UIComponents.ToolStrip();
			this.addAssemblyButton = new System.Windows.Forms.ToolStripButton();
			this.mainTreeView = new DataDevelop.UIComponents.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAssemblyButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Padding = new System.Windows.Forms.Padding(4, 0, 1, 0);
			this.toolStrip.Size = new System.Drawing.Size(254, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "toolStrip";
			this.toolStrip.Visible = false;
			// 
			// addAssemblyButton
			// 
			this.addAssemblyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.addAssemblyButton.Image = ((System.Drawing.Image)(resources.GetObject("addAssemblyButton.Image")));
			this.addAssemblyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addAssemblyButton.MergeAction = System.Windows.Forms.MergeAction.Replace;
			this.addAssemblyButton.Name = "addAssemblyButton";
			this.addAssemblyButton.Size = new System.Drawing.Size(87, 22);
			this.addAssemblyButton.Text = "Add Assembly";
			this.addAssemblyButton.Click += new System.EventHandler(this.addAssemblyButton_Click);
			// 
			// mainTreeView
			// 
			this.mainTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.mainTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTreeView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.mainTreeView.ImageIndex = 0;
			this.mainTreeView.ImageList = this.imageList;
			this.mainTreeView.LoadOnDemand = true;
			this.mainTreeView.Location = new System.Drawing.Point(0, 0);
			this.mainTreeView.Name = "mainTreeView";
			this.mainTreeView.SelectedImageIndex = 0;
			this.mainTreeView.Size = new System.Drawing.Size(254, 266);
			this.mainTreeView.TabIndex = 1;
			this.mainTreeView.TreeNodePopulate += new System.Windows.Forms.TreeViewEventHandler(this.mainTreeView_TreeNodePopulate);
			this.mainTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.mainTreeView_AfterSelect);
			this.mainTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.mainTreeView_NodeMouseDoubleClick);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList.Images.SetKeyName(0, "assembly");
			this.imageList.Images.SetKeyName(1, "namespace");
			this.imageList.Images.SetKeyName(2, "class");
			this.imageList.Images.SetKeyName(3, "struct");
			this.imageList.Images.SetKeyName(4, "interface");
			this.imageList.Images.SetKeyName(5, "delegate");
			this.imageList.Images.SetKeyName(6, "enum");
			// 
			// AssemblyExplorer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(254, 266);
			this.Controls.Add(this.mainTreeView);
			this.Controls.Add(this.toolStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AssemblyExplorer";
			this.TabText = "Assembly Explorer";
			this.Text = "Assembly Explorer";
			this.Load += new System.EventHandler(this.AssemblyExplorer_Load);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DataDevelop.UIComponents.ToolStrip toolStrip;
		private DataDevelop.UIComponents.TreeView mainTreeView;
		private System.Windows.Forms.ToolStripButton addAssemblyButton;
		private System.Windows.Forms.ImageList imageList;
	}
}
