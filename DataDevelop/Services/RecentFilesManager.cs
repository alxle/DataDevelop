using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DataDevelop.Services
{
	[Serializable]
	public class RecentFileEntry
	{
		public string Name { get; set; }
		public List<string> Files { get; set; }
	}

	static class RecentFilesManager
	{
		static XmlSerializer serializer;
		static List<RecentFileEntry> recentFiles = [];
		static DateTime? loadTime;
		const int MaxItems = 25;

		static XmlSerializer Serializer => serializer ??= new XmlSerializer(typeof(List<RecentFileEntry>));

		public static string[] GetRecentFiles(string name)
		{
			Load(SettingsManager.RecentFilesXmlPath);
			var entry = recentFiles.Find(i => i.Name == name);
			if (entry == null) {
				return [];
			}
			return [.. entry.Files];
		}

		public static void AddRecentFile(string name, string file)
		{
			Load(SettingsManager.RecentFilesXmlPath);
			var entry = recentFiles.Find(i => i.Name == name);
			if (entry == null) {
				entry = new RecentFileEntry {
					Name = name,
					Files = []
				};
				recentFiles.Add(entry);
			}
			entry.Files.Remove(file);
			entry.Files.Insert(0, file);
			if (entry.Files.Count > MaxItems) {
				entry.Files.RemoveAt(MaxItems);
			}
			Save(SettingsManager.RecentFilesXmlPath);
		}

		public static void RemoveRecentFile(string name, string file)
		{
			Load(SettingsManager.RecentFilesXmlPath);
			var entry = recentFiles.Find(i => i.Name == name);
			if (entry != null) {
				if (entry.Files.Remove(file)) {
					Save(SettingsManager.RecentFilesXmlPath);
				}
			}
		}

		static void Save(string path)
		{
			try {
				var serializer = Serializer;
				using (var writer = new StreamWriter(path)) {
					serializer.Serialize(writer, recentFiles);
				}
				loadTime = DateTime.Now;
			} catch (Exception ex) {
				LogManager.LogError("Error saving recent files", ex);
			}
		}

		static void Load(string path)
		{
			try {
				var file = new FileInfo(path);
				if (file.Exists && (loadTime == null || loadTime < file.LastWriteTime)) {
					var serializer = Serializer;
					using (var reader = new StreamReader(path)) {
						recentFiles = (List<RecentFileEntry>)serializer.Deserialize(reader);
					}
					loadTime = DateTime.Now;
				}
			} catch (Exception ex) {
				LogManager.LogError("Error loading recent files", ex);
			}
		}
	}
}
