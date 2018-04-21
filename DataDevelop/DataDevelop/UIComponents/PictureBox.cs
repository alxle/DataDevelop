using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace DataDevelop.UIComponents
{
	class PictureBox : System.Windows.Forms.PictureBox
	{
		protected override void OnPaint(PaintEventArgs pe)
		{
			if (Image != null && SizeMode == PictureBoxSizeMode.StretchImage && proportional) {
				RectangleF rect = new RectangleF();
				float w = (float)Width / Image.Width;
				float h = (float)Height / Image.Height;

				if (w > h) {
					rect.Width = Image.Width * h - 2;
					rect.Height = Image.Height * h - 2;
					rect.X = (Width - rect.Width) / 2;
					//rect.Y = (Height - rect.Height) / 2;
				} else {
					rect.Width = Image.Width * w - 2;
					rect.Height = Image.Height * w - 2;
					//rect.X = (Width - rect.Width) / 2;
					rect.Y = (Height - rect.Height) / 2;
				}
				pe.Graphics.DrawImage(Image, rect);
			} else {
				base.OnPaint(pe);
			}
		}

		private bool proportional = true;

		[DefaultValue(true)]
		public bool Proportional
		{
			get { return proportional; }
			set { proportional = value; }
		}
	}
}
