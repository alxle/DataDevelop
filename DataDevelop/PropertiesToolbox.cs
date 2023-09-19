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
				propertyGrid.ViewBackColor = VisualStyles.DarkThemeColors.Background;
				propertyGrid.ViewForeColor = VisualStyles.DarkThemeColors.TextColor;
				propertyGrid.ViewBorderColor = VisualStyles.DarkThemeColors.BorderDark;
				propertyGrid.LineColor = VisualStyles.DarkThemeColors.BorderDark;
			}
		}

		public PropertyGrid PropertyGrid => propertyGrid;
	}
}

