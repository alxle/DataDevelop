using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop
{
	public class PropertyGrid : System.Windows.Forms.PropertyGrid
	{
		public PropertyGrid()
		{
			base.ToolStripRenderer = SystemToolStripRenderers.ToolStripSquaredEdgesRenderer;
			base.HelpVisible = false;
			base.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
		}

		//private ToolStrip toolStrip;

		//public ToolStrip ToolStrip
		//{
		//    get
		//    {
		//        if (toolStrip == null) {
		//            toolStrip = GetToolStrip(this);
		//        }
		//        return toolStrip;
		//    }
		//}

		//private ToolStrip GetToolStrip(Control continer)
		//{
		//    foreach (Control c in continer.Controls) {
		//        ToolStrip t = (c as ToolStrip);
		//        if (t != null) {
		//            return t;
		//        } else {
		//            t = GetToolStrip(c);
		//            if (t != null)
		//                return t;
		//        }
		//    }
		//    return null;
		//}

	}
}
