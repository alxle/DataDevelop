using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DataDevelop.UIComponents
{
	[ToolStripItemDesignerAvailability]
	public class SortToolStripItem : ToolStripControlHost
	{
		public SortToolStripItem()
			: base(new SortPanel())
		{
		}

		public SortPanel SortPanel
		{
			get { return (SortPanel)Control; }
		}
	}
}
