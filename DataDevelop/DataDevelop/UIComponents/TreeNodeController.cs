using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop.UIComponents
{
	class TreeNodeController
	{
		private object _tag;

		public TreeNodeController()
		{

		}

		public TreeNodeController(object tag)
		{
			Tag = tag;
		}

		public object Tag
		{
			get { return _tag; }
			set { _tag = value; }
		}

		public event TreeViewEventHandler Populate;

		public event TreeViewEventHandler Refresh;

		public event TreeViewEventHandler DoubleClick;

		public void PerformPopulate(TreeNode node)
		{
			if (Populate != null) {
				Populate(this, new TreeViewEventArgs(node));
			}
		}

		public void PerformRefresh(TreeNode node)
		{
			if (Refresh != null) {
				Refresh(this, new TreeViewEventArgs(node));
			}
		}

		public void PerformDoubleClick(TreeNode node)
		{
			if (DoubleClick != null) {
				DoubleClick(this, new TreeViewEventArgs(node));
			}
		}

		public bool SupportsRefresh
		{
			get { return Refresh != null; }
		}

		public bool SupportsDoubleClick
		{
			get { return DoubleClick != null; }
		}
	}
}
