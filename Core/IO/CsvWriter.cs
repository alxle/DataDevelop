using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataDevelop.IO
{
	public sealed class CsvWriter : IDisposable
	{
		readonly TextWriter writer;

		public CsvWriter(TextWriter writer) => this.writer = writer;

		public CsvWriter(string path, bool append = false, Encoding encoding = null)
			=> writer = new StreamWriter(path, append, encoding ?? Encoding.Default);

		public CsvWriter(Stream stream, Encoding encoding = null)
			=> writer = new StreamWriter(stream, encoding ?? Encoding.Default);

		public char Delimiter { get; set; } = ',';

		public void Dispose() => writer.Dispose();

		public void WriteRow(string[] values)
		{
			var escapeChars = new[] { Delimiter, '\r', '\n', '"' };
			var index = 0;
			foreach (var value in values) {
				if (index++ > 0) {
					writer.Write(Delimiter);
				}
				if (value != null) {
					if (escapeChars.Any(ch => value.Contains(ch))) {
						writer.Write('"');
						writer.Write(value.Replace("\"", "\"\""));
						writer.Write('"');
					} else {
						writer.Write(value);
					}
				}
			}
			writer.WriteLine();
		}

		public void WriteRows(IEnumerable<string[]> rows)
		{
			foreach (var row in rows) {
				WriteRow(row);
			}
		}
	}
}
