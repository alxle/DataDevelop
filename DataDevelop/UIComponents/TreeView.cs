using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop.UIComponents
{
	public partial class TreeView : System.Windows.Forms.TreeView
	{
		public TreeView()
		{
			InitializeComponent();
		}

		protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;
			this.SelectedNode = node;
			if (loadOnDemand && ShouldLoadNodes(node)) {
				node.Nodes.Clear();
				OnTreeNodePopulate(new TreeViewEventArgs(node));
			}
			base.OnBeforeExpand(e);
		}

		private bool loadOnDemand;

		[DefaultValue(false)]
		public bool LoadOnDemand
		{
			get { return loadOnDemand; }
			set { loadOnDemand = value; }
		}

		protected virtual void OnTreeNodePopulate(TreeViewEventArgs e)
		{
			if (TreeNodePopulate != null) {
				TreeNodePopulate(this, e);
			}
		}

		public event TreeViewEventHandler TreeNodePopulate;

		public void RegisterToLoadOnDemand(TreeNode treeNode)
		{
			if (treeNode.TreeView == this) {
				treeNode.Nodes.Clear();
				treeNode.Nodes.Add(String.Empty);
			}
		}

		private static bool ShouldLoadNodes(TreeNode node)
		{
			return (node.Nodes.Count == 1 && node.Nodes[0].Text.Length == 0);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right) {
				this.SelectedNode = this.GetNodeAt(e.X, e.Y);
			}
			base.OnMouseDown(e);
		}
	}
}
