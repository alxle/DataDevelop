using System;
using System.Drawing;
using System.Windows.Forms;

namespace DataDevelop
{
	static class FormExtensions
	{
		public static void LeftPositionate(Form form, Control relativeTo)
		{
			form.StartPosition = FormStartPosition.Manual;
			Point point = new Point(0, 0);
			point = relativeTo.PointToScreen(point);
			form.Left = point.X + relativeTo.Width + 2;
			form.Top = point.Y;// +relativeTo.Height + 2;
			EnsureVisibility(form);
		}

		public static void DownPositionate(Form form, Control relativeTo)
		{
			form.StartPosition = FormStartPosition.Manual;
			Point point = new Point(0, 0);
			point = relativeTo.PointToScreen(point);
			form.Left = point.X;// +relativeTo.Width + 2;
			form.Top = point.Y +relativeTo.Height + 2;
			EnsureVisibility(form);
		}

		public static void DownPositionate(Form form, ToolStripItem relativeTo, Control parent)
		{
			form.StartPosition = FormStartPosition.Manual;
			Point point = relativeTo.Bounds.Location;
			point = parent.PointToScreen(point);
			form.Left = point.X;// +relativeTo.Width + 1;
			form.Top = point.Y + relativeTo.Height + 1;
			EnsureVisibility(form);
		}

		private static void EnsureVisibility(Form form)
		{
			Rectangle rect = Screen.FromControl(form).WorkingArea;
			if (rect.Height < form.Height) {
				form.Height = Math.Max(rect.Height, form.MinimumSize.Height);
			}
			if (rect.Width < form.Width) {
				form.Width = Math.Max(rect.Width, form.MinimumSize.Width);
			}
			if (form.Bottom > rect.Bottom) {
				form.Top = rect.Top + rect.Height - form.Height;
			}
			if (form.Right > rect.Right) {
				form.Left = rect.Left + rect.Width - form.Width;
			}
			if (form.Top < rect.Top) {
				form.Top = rect.Top;
			}
			if (form.Left < rect.Left) {
				form.Left = rect.Left;
			}
		}

		public static void TrySetSize(Form form, Size size)
		{
			if (size.Width >= form.MinimumSize.Width && size.Height >= form.MinimumSize.Height) {
				form.Size = size;
			}
		}

		public static bool Close(Form form)
		{
			bool closed = false;
			FormClosedEventHandler handler = delegate { closed = true; };
			form.FormClosed += handler;
			form.Close();
			form.FormClosed -= handler;
			return closed;
		}
	}
}
