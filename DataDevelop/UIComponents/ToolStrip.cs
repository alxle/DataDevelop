using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop.UIComponents
{
	public partial class ToolStrip : System.Windows.Forms.ToolStrip
	{
		public ToolStrip()
		{
			InitializeComponent();
		}

		private bool drawHorizontalMargin;

		[Category("Appearance")]
		[DefaultValue(false)]
		public bool DrawHorizontalMargin
		{
			get { return drawHorizontalMargin; }
			set
			{
				if (drawHorizontalMargin != value) {
					drawHorizontalMargin = value;
					this.Invalidate();
				}
			}
		}

		private bool drawVerticalMargin;

		[Category("Appearance")]
		[DefaultValue(false)]
		public bool DrawVerticalMargin
		{
			get { return drawVerticalMargin; }
			set
			{
				if (drawVerticalMargin != value) {
					drawVerticalMargin = value;
					this.Invalidate();
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			int height = this.Height - 1;
			int width = this.Width - 1;

			if (drawVerticalMargin) {
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, 0, 0, 0, height);
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, width, 0, width, height);
			}
			if (drawHorizontalMargin) {
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, 0, 0, width, 0);
				e.Graphics.DrawLine(SystemPens.ControlDarkDark, 0, height, width, height);
			}
		}

		protected override void OnItemAdded(ToolStripItemEventArgs e)
		{
			e.Item.MergeAction = MergeAction.Replace;
			base.OnItemAdded(e);
		}

		private MergeAction defaultMergeAction = MergeAction.Replace;

		[DefaultValue(MergeAction.Replace)]
		public MergeAction DefaultMergeAction
		{
			get { return defaultMergeAction; }
			set { defaultMergeAction = value; }
		}

	}
}
