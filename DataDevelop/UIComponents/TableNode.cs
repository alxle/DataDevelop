using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataDevelop.Data;

namespace DataDevelop
{
	class TableNode : TreeNode
	{
		Table table;
		const string tableImageKey = "table";
		const string viewImageKey = "view";

		public TableNode(Table table)
			: base(table.DisplayName)
		{
			this.table = table;
			if (table.IsView) {
				ImageKey = SelectedImageKey = viewImageKey;
			} else {
				ImageKey = SelectedImageKey = tableImageKey;
			}
		}

		public Table Table
		{
			get { return table; }
		}
	}
}
