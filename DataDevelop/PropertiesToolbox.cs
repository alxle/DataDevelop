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
		}

		public PropertyGrid PropertyGrid
		{
			get { return propertyGrid; }
		}
	}
}

