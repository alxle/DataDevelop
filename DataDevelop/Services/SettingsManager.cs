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
			if (DatabasesManager.IsCollectionDirty) {
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				XmlWriter writer = XmlWriter.Create(DatabasesFileName, settings);
				try {
					writer.WriteStartElement("Databases");
					foreach (Database db in Databases.Values) {
						writer.WriteStartElement(db.Provider.Name);
						writer.WriteAttributeString("Name", db.Name);
						writer.WriteString(db.ConnectionString);
						writer.WriteEndElement();
					}
				} catch (XmlException) {
				} finally {
					writer.WriteFullEndElement();
					writer.Close();
					DatabasesManager.IsCollectionDirty = false;
				}
			}
		}
	}
}
