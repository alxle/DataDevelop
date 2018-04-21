using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DataDevelop.UIComponents
{
	public partial class ListView : System.Windows.Forms.ListView
	{
		public ListView()
		{
			InitializeComponent();
			this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
		}

		public ListView(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}
	}
}
