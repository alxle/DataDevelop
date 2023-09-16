using System;
using Microsoft.Win32;

namespace DataDevelop
{
	internal class WindowsVersion
	{
		static string productName;
		static Version version;

		public static bool IsWindows10OrGreater(int build = -1)
		{
			////return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
			return Version.Major >= 10 && Version.Build >= build;
		}

		private static void ReadFromRegistry()
		{
			using (var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion")) {
				productName = reg.GetValue("ProductName").ToString();
				var major = Convert.ToInt32(reg.GetValue("CurrentMajorVersionNumber"));
				var minor = Convert.ToInt32(reg.GetValue("CurrentMinorVersionNumber"));
				var build = Convert.ToInt32(reg.GetValue("CurrentBuildNumber"));
				var revision = Convert.ToInt32(reg.GetValue("UBR"));
				if (major == 0 && minor == 0) {
					var currentVersion = reg.GetValue("CurrentVersion").ToString().Split('.');
					if (currentVersion.Length > 0) major = Convert.ToInt32(currentVersion[0]);
					if (currentVersion.Length > 1) minor = Convert.ToInt32(currentVersion[1]);
				}
				version = new Version(major, minor, build, revision);
				if (version.Major >= 10 && version.Build >= 22000) {
					productName = productName.Replace("Windows 10", "Windows 11");
				}
			}
		}

		public static string Name
		{
			get
			{
				if (productName == null) {
					ReadFromRegistry();
				}
				return productName;
			}
		}

		public static Version Version
		{
			get
			{
				if (version == null) {
					ReadFromRegistry();
				}
				return version;
			}
		}
	}
}
