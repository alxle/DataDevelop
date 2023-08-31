using System;
using System.Deployment.Application;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DataDevelop.Data;

namespace DataDevelop
{
	static class SettingsManager
	{
		private static string databasesFileName;
		private static string dockPropertiesFileName;

		public static string DataDirectory
			=> ApplicationDeployment.IsNetworkDeployed ?
				ApplicationDeployment.CurrentDeployment.DataDirectory : Application.StartupPath;

		public static string DatabasesFileName
			=> databasesFileName ?? (databasesFileName = DataFilePath("Databases.xml"));

		public static string DockPropertiesFileName
			=> dockPropertiesFileName ?? (dockPropertiesFileName = DataFilePath("DockingProperties.xml"));

		public static string DataFilePath(string fileName) => Path.Combine(DataDirectory, fileName);

		public static void LoadDatabases()
		{
			var databasesFile = DatabasesFileName;
			if (File.Exists(databasesFile)) {
				var document = new XmlDocument();
				document.Load(databasesFile);
				foreach (XmlNode node in document.DocumentElement.ChildNodes) {
					var providerName = node.Name;
					var name = node.Attributes["Name"]?.Value;
					var provider = DbProvider.GetProvider(providerName);
					if (provider != null && name != null) {
						var connectionString = node.InnerText;
						if (!DatabasesManager.Contains(name)) {
							try {
								var database = provider.CreateDatabase(name, connectionString);
								DatabasesManager.Add(database);
							} catch (Exception ex) {
								LogError("Error Loading Databases", ex);
							}
						}
					}
				}
				DatabasesManager.IsCollectionDirty = false;
			}
			if (DatabasesManager.Databases.Count == 0) {
				var worldDbFile = new FileInfo(DataFilePath("World.db"));
				if (ApplicationDeployment.IsNetworkDeployed) {
					if (!worldDbFile.Exists) {
						var deployWorldDb = new FileInfo(Path.Combine(Application.StartupPath, "World.db"));
						if (deployWorldDb.Exists) {
							deployWorldDb.CopyTo(worldDbFile.FullName);
						}
					}
				}
				if (worldDbFile.Exists) {
					var sqliteProvider = DbProvider.GetProvider("SQLite");
					var connectionStringBuilder = sqliteProvider.CreateConnectionStringBuilder();
					connectionStringBuilder["Data Source"] = worldDbFile.FullName;
					connectionStringBuilder["Fail If Missing"] = true;
					var worldDb = sqliteProvider.CreateDatabase("World", connectionStringBuilder.ToString());
					DatabasesManager.Add(worldDb);
				}
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
					foreach (var db in DatabasesManager.Databases.Values) {
						writer.WriteStartElement(db.Provider.Name);
						writer.WriteAttributeString("Name", db.Name);
						writer.WriteString(db.ConnectionString);
						writer.WriteEndElement();
					}
				} catch (Exception ex) {
					error = true;
					LogError("Error Saving Databases", ex);
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

		private static void LogError(string headerMessage, Exception ex)
		{
			var errorLogFile = "ErrorLog-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
			using (var log = new StreamWriter(DataFilePath(errorLogFile))) {
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
