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
			if (Properties.Settings.Default.CallUpgrade) {
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.CallUpgrade = false;
				Properties.Settings.Default.Save();
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(MainForm.Instance);
		}
	}

	public delegate void Action();
}
