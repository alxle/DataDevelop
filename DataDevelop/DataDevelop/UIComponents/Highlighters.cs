using DataDevelop.Properties;
using ICSharpCode.TextEditor.Document;
using System.Xml;

namespace DataDevelop.UIComponents
{
	static class Highlighters
	{
		private static DefaultHighlightingStrategy python;
		private static DefaultHighlightingStrategy sql;
		private static DefaultHighlightingStrategy javascript;

		public static DefaultHighlightingStrategy Python
		{
			get
			{
				if (python == null) {
					XmlDocument lexer = new XmlDocument();
					lexer.LoadXml(Resources.PythonLexer);
					python = GetHighlightingStrategy("Python", lexer);
				}
				return python;
			}
		}

		public static DefaultHighlightingStrategy Sql
		{
			get
			{
				if (sql == null) {
					XmlDocument lexer = new XmlDocument();
					lexer.LoadXml(Resources.SqlLexer);
					sql = GetHighlightingStrategy("SQL", lexer);
				}
				return sql;
			}
		}

		public static DefaultHighlightingStrategy Javascript
		{
			get
			{
				if (javascript == null) {
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(Resources.JavascriptSyntaxDefinition);
					javascript = GetHighlightingStrategy("Javascript", doc);
				}
				return javascript;
			}
		}

		private static DefaultHighlightingStrategy GetHighlightingStrategy(string name, XmlDocument doc)
		{
			DefaultHighlightingStrategy highlighter = new DefaultHighlightingStrategy(name);

			if (doc.DocumentElement.HasAttribute("extensions")) {
				highlighter.Extensions = doc.DocumentElement.GetAttribute("extensions").Split(new char[] { ';', '|' });
			}

			XmlElement environment = doc.DocumentElement["Environment"];
			if (environment != null) {
				foreach (XmlNode node in environment.ChildNodes) {
					if (node is XmlElement) {
						XmlElement el = (XmlElement)node;
						if (el.Name == "Custom") {
							highlighter.SetColorFor(el.GetAttribute("name"), el.HasAttribute("bgcolor") ? new ICSharpCode.TextEditor.Document.HighlightBackground(el) : new ICSharpCode.TextEditor.Document.HighlightColor(el));
						} else {
							highlighter.SetColorFor(el.Name, el.HasAttribute("bgcolor") ? new ICSharpCode.TextEditor.Document.HighlightBackground(el) : new ICSharpCode.TextEditor.Document.HighlightColor(el));
						}
					}
				}
			}
			if (doc.DocumentElement["Properties"] != null) {
				foreach (XmlElement propertyElement in doc.DocumentElement["Properties"].ChildNodes) {
					highlighter.Properties[propertyElement.Attributes["name"].InnerText] = propertyElement.Attributes["value"].InnerText;
				}
			}
			if (doc.DocumentElement["Digits"] != null) {
				highlighter.DigitColor = new ICSharpCode.TextEditor.Document.HighlightColor(doc.DocumentElement["Digits"]);
			}
			XmlNodeList nodes = doc.DocumentElement.GetElementsByTagName("RuleSet");
			foreach (XmlElement element in nodes) {
				highlighter.AddRuleSet(new ICSharpCode.TextEditor.Document.HighlightRuleSet(element));
			}
			highlighter.ResolveReferences();
			return highlighter;
		}
	}
}
