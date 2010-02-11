using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataDevelop.Utils
{
	static class StringUtils
	{
		public static IEnumerable<string> GetLines(string text)
		{
			using (StringReader reader = new StringReader(text)) {
				while (reader.Peek() != -1) {
					yield return reader.ReadLine();
				}
			}
		}
	}
}
