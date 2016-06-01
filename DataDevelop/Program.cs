using System;
using System.Windows.Forms;

namespace DataDevelop
{
	static class Program
	{
		internal const string Homepage = "http://datadevelop.codeplex.com/";
		internal const string Documentation = "http://datadevelop.codeplex.com/documentation";

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
