using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataDevelop
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			////ToolStripManager.Renderer = SystemToolStripRenderers.ToolStripSquaredEdgesRenderer;//.ToolStripProfessionalRenderer;
			ToolStripManager.VisualStylesEnabled = false;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(MainForm.Instance);
		}
	}
}