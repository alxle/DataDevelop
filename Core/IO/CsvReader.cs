using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataDevelop.IO
{
	public sealed class CsvReader : IDisposable
	{
		readonly TextReader reader;

		public CsvReader(TextReader reader) => this.reader = reader;

		public CsvReader(string path, Encoding encoding = null)
			=> reader = new StreamReader(path, encoding ?? Encoding.Default);

		public CsvReader(Stream stream, Encoding encoding = null)
			=> reader = new StreamReader(stream, encoding ?? Encoding.Default);

		public char Delimiter { get; set; } = ',';

		public void Dispose() => reader.Dispose();

		public string[] ReadRow()
		{
			if (reader.Peek() < 0) {
				return null;
			}
			var values = new List<string>();
			var state = State.Initial;
			var currentValue = new StringBuilder();
			while (reader.Peek() >= 0) {
				var ch = (char)reader.Read();
				if (ch == '\r' && reader.Peek() == '\n') {
					ch = (char)reader.Read();
				}
				switch (state) {
					case State.Initial:
						if (ch == '"') {
							state = State.QuotedValue;
						} else if (ch == Delimiter) {
							values.Add(string.Empty);
						} else if (ch == '\n') {
							state = State.EndLine;
						} else {
							currentValue.Append(ch);
							state = State.UnquotedValue;
						}
						break;
					case State.UnquotedValue:
						if (ch == Delimiter) {
							values.Add(currentValue.ToString());
							currentValue.Length = 0;
							state = State.Initial;
						} else if (ch == '\n') {
							state = State.EndLine;
						} else {
							currentValue.Append(ch);
						}
						break;
					case State.QuotedValue:
						if (ch == '"') {
							state = State.EndQuote;
						} else if (ch == '\n') {
							currentValue.AppendLine();
						} else {
							currentValue.Append(ch);
						}
						break;
					case State.EndQuote:
						if (ch == '"') {
							currentValue.Append(ch);
							state = State.QuotedValue;
						} else if (ch == Delimiter) {
							values.Add(currentValue.ToString());
							currentValue.Length = 0;
							state = State.Initial;
						} else if (ch == '\n') {
							state = State.EndLine;
						} else {
							currentValue.Append(ch);
							state = State.UnquotedValue;
						}
						break;
				}
				if (state == State.EndLine) {
					values.Add(currentValue.ToString());
					currentValue.Length = 0;
					break;
				}
			}
			if (state != State.EndLine) {
				values.Add(currentValue.ToString());
			}
			return values.ToArray();
		}

		public IEnumerable<string[]> ReadRows()
		{
			string[] row;
			while ((row = ReadRow()) != null) {
				yield return row;
			}
		}

		public string[][] ReadAllRows()
		{
			return ReadRows().ToArray();
		}

		enum State
		{
			Initial, UnquotedValue, QuotedValue, EndQuote, EndLine
		}
	}
}
