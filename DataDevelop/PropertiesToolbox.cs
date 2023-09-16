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
			if (MainForm.DarkMode) {
				this.UseImmersiveDarkMode();
				propertyGrid.ViewBackColor = Color.FromArgb(31, 31, 31);
				propertyGrid.ViewForeColor = Color.FromArgb(250, 250, 250);
				propertyGrid.ViewBorderColor = Color.FromArgb(46, 46, 46);
				propertyGrid.LineColor = Color.FromArgb(46, 46, 46);
			}
		}

		public PropertyGrid PropertyGrid => propertyGrid;
	}
}

