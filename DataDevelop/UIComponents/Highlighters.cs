using System.Xml;
using DataDevelop.Properties;
using ICSharpCode.TextEditor.Document;

namespace DataDevelop.UIComponents
{
	static class Highlighters
	{
		private static DefaultHighlightingStrategy python;
		private static DefaultHighlightingStrategy sql;
		private static DefaultHighlightingStrategy javascript;

		public static DefaultHighlightingStrategy Python
			=> python ?? (python = GetHighlightingStrategy(MainForm.DarkMode ? Resources.PythonModeDark : Resources.PythonMode));

		public static DefaultHighlightingStrategy Sql
			=> sql ?? (sql = GetHighlightingStrategy(MainForm.DarkMode ? Resources.SqlModeDark : Resources.SqlMode));

		public static DefaultHighlightingStrategy Javascript
			=> javascript ?? (javascript = GetHighlightingStrategy(MainForm.DarkMode ? Resources.JavascriptModeDark : Resources.JavascriptMode));

		private static DefaultHighlightingStrategy GetHighlightingStrategy(string xml)
		{
			var doc = new XmlDocument();
			doc.LoadXml(xml);
			var name = doc.DocumentElement.HasAttribute("name") ? doc.DocumentElement.GetAttribute("name") : "default";
			var highlighter = new DefaultHighlightingStrategy(name);
			if (doc.DocumentElement.HasAttribute("extensions")) {
				highlighter.Extensions = doc.DocumentElement.GetAttribute("extensions").Split(';', '|');
			}
			var environment = doc.DocumentElement["Environment"];
			if (environment != null) {
				foreach (XmlNode node in environment.ChildNodes) {
					if (node is XmlElement el) {
						var itemName = (el.Name == "Custom") ? el.GetAttribute("name") : el.Name;
						var itemColor = el.HasAttribute("bgcolor") ? new HighlightBackground(el) : new HighlightColor(el);
						highlighter.SetColorFor(itemName, itemColor);
					}
				}
			}
			var propertiesElement = doc.DocumentElement["Properties"];
			if (propertiesElement != null) {
				foreach (XmlElement propertyElement in propertiesElement.ChildNodes) {
					var propertyName = propertyElement.Attributes["name"].InnerText;
					var propertyValue = propertyElement.Attributes["value"].InnerText;
					highlighter.Properties[propertyName] = propertyValue;
				}
			}
			var digitsElement = doc.DocumentElement["Digits"];
			if (digitsElement != null) {
				highlighter.DigitColor = new HighlightColor(digitsElement);
			}
			var nodes = doc.DocumentElement.GetElementsByTagName("RuleSet");
			foreach (XmlElement element in nodes) {
				highlighter.AddRuleSet(new HighlightRuleSet(element));
			}
			highlighter.ResolveReferences();
			return highlighter;
		}
	}
}
