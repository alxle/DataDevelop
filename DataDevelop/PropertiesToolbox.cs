using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop
{
	public partial class PropertiesToolbox : DataDevelop.Toolbox
	{
		public PropertiesToolbox()
		{
			InitializeComponent();
			//propertyGrid.BackColor = SystemColors.ControlDarkDark;
			//propertyGrid.ToolStrip.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
			//propertyGrid.ToolStrip.Left += 1;
			//propertyGrid.ToolStrip.Width -= 2;
		}

		public PropertyGrid PropertyGrid
		{
			get { return propertyGrid; }
		}
	}
}

