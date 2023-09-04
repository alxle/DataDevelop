using System;
using System.Windows.Forms;

namespace DataDevelop
{
	static class Program
	{
		internal const string Homepage = "https://github.com/alxle/DataDevelop";
		internal const string Documentation = "https://github.com/alxle/DataDevelop/wiki";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var settings = Properties.Settings.Default;
			if (settings.CallUpgrade) {
				settings.Upgrade();
				settings.CallUpgrade = false;
				settings.Save();
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(MainForm.Instance);
		}
	}
}
