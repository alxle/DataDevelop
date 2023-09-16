using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DataDevelop
{
	class NativeMethods
	{
		[DllImport("dwmapi.dll")]
		private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

		private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
		private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

		internal static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
		{
			if (WindowsVersion.IsWindows10OrGreater(17763)) {
				var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
				if (WindowsVersion.IsWindows10OrGreater(18985)) {
					attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
				}
				int useImmersiveDarkMode = enabled ? 1 : 0;
				return DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
			}
			return false;
		}
	}
}
