using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace DataDevelop.UIComponents
{
	class AlphaPanel : Panel
	{
		private byte alphaChannel = 255;

		public AlphaPanel()
		{
			////this.SetStyle(ControlStyles.Opaque, false);
			////////this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			////this.SetStyle(ControlStyles.UserPaint, true);
			////////this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			////////this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		[DefaultValue((byte)255)]
		public byte AlphaChannel
		{
			get { return this.alphaChannel; }
			set
			{
				if (this.alphaChannel != value) {
					this.alphaChannel = value;
					this.Invalidate();
				}
			}
		}

		////protected override CreateParams CreateParams
		////{
		////    get
		////    {
		////        CreateParams cp = base.CreateParams;
		////        cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
		////        return cp;
		////    }
		////}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			////if (alphaChannel == 255) {
				base.OnPaintBackground(e);
			////} else {
			////    using (Brush brush = new SolidBrush(Color.FromArgb(alphaChannel, this.BackColor))) {
			////        e.Graphics.FillRectangle(brush, this.ClientRectangle);
			////    }
			////}
		}
	}
}
