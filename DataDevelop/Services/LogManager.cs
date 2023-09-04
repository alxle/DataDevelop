using System;
using System.IO;

namespace DataDevelop
{
	internal static class LogManager
	{
		public static void LogError(string headerMessage, Exception ex)
		{
			var errorLogFile = "ErrorLog-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
			using (var log = new StreamWriter(SettingsManager.DataFilePath(errorLogFile))) {
				log.WriteLine(headerMessage);
				log.WriteLine("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				log.WriteLine("==========================");
				log.WriteLine();
				log.WriteLine("Exception type: " + ex.GetType().FullName);
				log.WriteLine(ex.ToString());
				log.WriteLine();
			}
		}
	}
}
