using System;
using System.Drawing;
using System.Windows.Forms;

namespace DataDevelop
{
	static class FormExtensions
	{
		public static bool UseImmersiveDarkMode(this Form form)
		{
			return NativeMethods.UseImmersiveDarkMode(form.Handle, true);
		}

		public static void PositionLeft(this Form form, Control relativeTo)
		{
			form.StartPosition = FormStartPosition.Manual;
			var point = relativeTo.PointToScreen(Point.Empty);
			form.Left = point.X + relativeTo.Width + 2;
			form.Top = point.Y;
			EnsureVisibility(form);
		}

		public static void PositionDown(this Form form, Control relativeTo)
		{
			form.StartPosition = FormStartPosition.Manual;
			var point = relativeTo.PointToScreen(Point.Empty);
			form.Left = point.X;
			form.Top = point.Y + relativeTo.Height + 2;
			EnsureVisibility(form);
		}

		public static void PositionDown(this Form form, ToolStripItem relativeTo, Control parent)
		{
			form.StartPosition = FormStartPosition.Manual;
			var point = relativeTo.Bounds.Location;
			point = parent.PointToScreen(point);
			form.Left = point.X;
			form.Top = point.Y + relativeTo.Height + 1;
			EnsureVisibility(form);
		}

		private static void EnsureVisibility(Form form)
		{
			var rect = Screen.FromControl(form).WorkingArea;
			if (rect.Height < form.Height) 
				form.Height = Math.Max(rect.Height, form.MinimumSize.Height);
			if (rect.Width < form.Width) 
				form.Width = Math.Max(rect.Width, form.MinimumSize.Width);
			if (form.Bottom > rect.Bottom) 
				form.Top = rect.Top + rect.Height - form.Height;
			if (form.Right > rect.Right) 
				form.Left = rect.Left + rect.Width - form.Width;
			if (form.Top < rect.Top) 
				form.Top = rect.Top;
			if (form.Left < rect.Left) 
				form.Left = rect.Left;
		}

		public static bool TrySetSize(this Form form, Size size)
		{
			if (size.Width >= form.MinimumSize.Width && size.Height >= form.MinimumSize.Height) {
				form.Size = size;
				return true;
			}
			return false;
		}

		public static bool TryClose(this Form form)
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
