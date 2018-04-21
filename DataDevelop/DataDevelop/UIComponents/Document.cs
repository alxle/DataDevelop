using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DataDevelop
{
	public partial class Document : DockContent
	{
		public Document()
		{
			InitializeComponent();
		}

		public virtual string DocumentName
		{
			get { return TabText; }
			set { TabText = value; }
		}

		public new void Show(DockPanel dockPanel)
		{
			this.MdiParent = dockPanel.Parent as Form;
			if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi) {
				base.Show();
			} else {
				base.Show(dockPanel);
			}
		}

		private ToolStrip mainToolStrip;

		public ToolStrip MainToolStrip
		{
			get { return mainToolStrip; }
			set { mainToolStrip = value; }
		}

	}
}