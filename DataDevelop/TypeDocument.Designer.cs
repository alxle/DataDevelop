namespace DataDevelop
{
    partial class TypeDocument
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TypeDocument));
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Static Members", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Instance Members", System.Windows.Forms.HorizontalAlignment.Left);
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.mainListView = new DataDevelop.UIComponents.ListView(this.components);
			this.nameColumn = new System.Windows.Forms.ColumnHeader();
			this.typeColumn = new System.Windows.Forms.ColumnHeader();
			this.modifierColumn = new System.Windows.Forms.ColumnHeader();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.typeLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList.Images.SetKeyName(0, "closed");
			this.imageList.Images.SetKeyName(1, "open");
			this.imageList.Images.SetKeyName(2, "interface");
			this.imageList.Images.SetKeyName(3, "method");
			this.imageList.Images.SetKeyName(4, "class");
			this.imageList.Images.SetKeyName(5, "constant");
			this.imageList.Images.SetKeyName(6, "event");
			this.imageList.Images.SetKeyName(7, "field");
			this.imageList.Images.SetKeyName(8, "property");
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.mainListView, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(462, 316);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// mainListView
			// 
			this.mainListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.typeColumn,
            this.modifierColumn});
			this.mainListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainListView.FullRowSelect = true;
			listViewGroup1.Header = "Static Members";
			listViewGroup1.Name = "staticGroup";
			listViewGroup2.Header = "Instance Members";
			listViewGroup2.Name = "instanceGroup";
			this.mainListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
			this.mainListView.Location = new System.Drawing.Point(3, 33);
			this.mainListView.Name = "mainListView";
			this.mainListView.Size = new System.Drawing.Size(456, 280);
			this.mainListView.SmallImageList = this.imageList;
			this.mainListView.TabIndex = 3;
			this.mainListView.UseCompatibleStateImageBehavior = false;
			this.mainListView.View = System.Windows.Forms.View.Details;
			// 
			// nameColumn
			// 
			this.nameColumn.Text = "Name";
			this.nameColumn.Width = 253;
			// 
			// typeColumn
			// 
			this.typeColumn.Text = "DataType";
			this.typeColumn.Width = 127;
			// 
			// modifierColumn
			// 
			this.modifierColumn.Text = "Modifier";
			this.modifierColumn.Width = 129;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
			this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
			this.flowLayoutPanel1.Controls.Add(this.typeLabel);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(456, 24);
			this.flowLayoutPanel1.TabIndex = 2;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// typeLabel
			// 
			this.typeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.typeLabel.AutoSize = true;
			this.typeLabel.Location = new System.Drawing.Point(25, 4);
			this.typeLabel.Name = "typeLabel";
			this.typeLabel.Size = new System.Drawing.Size(31, 13);
			this.typeLabel.TabIndex = 0;
			this.typeLabel.Text = "DataType";
			// 
			// TypeDocument
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(462, 316);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DocumentName = "TypeDocument";
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TypeDocument";
			this.TabText = "TypeDocument";
			this.Text = "TypeDocument";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ImageList imageList;
		private DataDevelop.UIComponents.ListView mainListView;
		private System.Windows.Forms.ColumnHeader nameColumn;
		private System.Windows.Forms.ColumnHeader typeColumn;
		private System.Windows.Forms.ColumnHeader modifierColumn;
    }
}