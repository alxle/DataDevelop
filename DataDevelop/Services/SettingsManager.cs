using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Deployment.Application;
using DataDevelop.Data;

namespace DataDevelop
{
	static class SettingsManager
	{
		internal static string GetDataDirectory()
		{
			if (ApplicationDeployment.IsNetworkDeployed) {
				return ApplicationDeployment.CurrentDeployment.DataDirectory;
			}
			return Application.StartupPath;
		}

		private static string databasesFileName;

		public static string DatabasesFileName
		{
			get
			{
				if (databasesFileName == null) {
					string directory = GetDataDirectory();
					databasesFileName = Path.Combine(directory, "Databases.xml");
				}
				return databasesFileName;
			}
		}

		private static string dockPropertiesFileName;

		public static string DockPropertiesFileName
		{
			get
			{
				if (dockPropertiesFileName == null) {
					string directory = GetDataDirectory();
					dockPropertiesFileName = Path.Combine(directory, "DockingProperties.xml");
				}
				return dockPropertiesFileName;
			}
		}

		public static IDictionary<string, Database> Databases
		{
			get { return DatabasesManager.Databases; }
		}

		public static void LoadDatabases()
		{
			if (File.Exists(DatabasesFileName)) {
				XmlDocument document = new XmlDocument();
				document.Load(DatabasesFileName);
				foreach (XmlNode node in document.DocumentElement.ChildNodes) {
					string providerName = node.Name;
					string name = (node.Attributes["Name"] != null) ? node.Attributes["Name"].Value : null;
					DbProvider provider = DbProvider.GetProvider(providerName);
					if (provider != null && name != null) {
						string connectionString = node.InnerText;
						if (!DatabasesManager.Contains(name)) {
							Database database = provider.CreateDatabase(name, connectionString);
							DatabasesManager.Add(database);
						}
					}
				}
				DatabasesManager.IsCollectionDirty = false;
			}
		}

		public static void SaveDatabases()
		{
			var error = false;
			if (DatabasesManager.IsCollectionDirty) {
				var settings = new XmlWriterSettings() { Indent = true };
				var writer = XmlWriter.Create(DatabasesFileName + ".saving", settings);
				try {
					writer.WriteStartElement("Databases");
					foreach (var db in Databases.Values) {
						writer.WriteStartElement(db.Provider.Name);
						writer.WriteAttributeString("Name", db.Name);
						writer.WriteString(db.ConnectionString);
						writer.WriteEndElement();
					}
				} catch (Exception ex) {
					error = true;
					var errorLogFile = "ErrorLog-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
					using (var log = new StreamWriter(Path.Combine(GetDataDirectory(), errorLogFile))) {
						log.WriteLine("Exception Saving Databases");
						log.WriteLine("==========================");
						log.WriteLine();
						log.WriteLine("Exception type: " + ex.GetType().FullName);
						log.WriteLine(ex.ToString());
						log.WriteLine();
					}
				} finally {
					if (!error) {
						writer.WriteFullEndElement();
						writer.Close();
						if (File.Exists(DatabasesFileName)) {
							File.Replace(DatabasesFileName + ".saving", DatabasesFileName, null);
						} else {
							File.Move(DatabasesFileName + ".saving", DatabasesFileName);
						}
						DatabasesManager.IsCollectionDirty = false;
					}
				}
			}
		}
	}
}
