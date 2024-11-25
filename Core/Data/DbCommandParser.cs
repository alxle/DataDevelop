using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using static System.Char;

namespace DataDevelop.Data
{
	public class DbCommandParser
	{
		public static DbType GetDbType(object value)
		{
			if (value is string)
				return DbType.String;
			if (value is int)
				return DbType.Int32;
			if (value is short)
				return DbType.Int16;
			if (value is long)
				return DbType.Int64;
			if (value is byte[])
				return DbType.Binary;
			if (value is bool)
				return DbType.Boolean;
			if (value is byte)
				return DbType.Byte;
			if (value is DateTime)
				return DbType.DateTime;
			if (value is decimal)
				return DbType.Currency;
			if (value is float)
				return DbType.Single;
			if (value is double)
				return DbType.Double;
			if (value is Guid)
				return DbType.Guid;
			if (value is ushort)
				return DbType.UInt16;
			if (value is uint)
				return DbType.UInt32;
			if (value is ulong)
				return DbType.UInt64;
			return DbType.Object;
		}

		private static void BindParameter(IDataParameter parameter, object value)
		{
			if (value == null) {
				parameter.DbType = DbType.Object;
				parameter.Value = DBNull.Value;
			} else {
				parameter.DbType = GetDbType(value);
				parameter.Value = value;
			}
		}

		public static void BindParameters(IDbCommand command, params object[] values)
		{
			foreach (IDataParameter parameter in command.Parameters) {
				var index = Convert.ToInt32(parameter.SourceColumn.Substring(1));
				var value = values[index];
				BindParameter(parameter, value);
			}
		}

		public static void BindParameters(IDbCommand command, DataRow row)
		{
			foreach (IDataParameter parameter in command.Parameters) {
				var value = row[parameter.SourceColumn];
				BindParameter(parameter, value);
			}
		}

		public static void BindParameters(IDbCommand command, Dictionary<string, object> parameters)
		{
			foreach (IDataParameter parameter in command.Parameters) {
				var value = parameters[parameter.SourceColumn];
				BindParameter(parameter, value);
			}
		}

		public static IDbCommand Parse(Database database, string commandText)
		{
			if (database == null)
				throw new ArgumentNullException(nameof(database));
			if (commandText == null)
				throw new ArgumentNullException(nameof(commandText));
			var command = database.CreateCommand();
			var result = new StringBuilder(commandText.Length * 2);
			const char ParamChar = '?';
			var parameters = new Dictionary<string, DbParameter>(StringComparer.OrdinalIgnoreCase);
			var state = State.OnCommandText;
			var scanner = new Scanner(commandText);
			while (scanner.Read()) {
				var ch = scanner.Current;
				switch (state) {
					case State.OnCommandText:
						switch (ch) {
							case '\'':
								result.Append(ch);
								state = State.InSingleQuoteString;
								break;
							case '"':
								result.Append(ch);
								state = State.InDoubleQuoteString;
								break;
							case '-':
								result.Append(ch);
								if (scanner.Peek() == '-') {
									result.Append(scanner.ReadNext());
									state = State.InSingleLineComment;
								}
								break;
							case '/':
								result.Append(ch);
								if (scanner.Peek() == '*') {
									result.Append(scanner.ReadNext());
									state = State.InMultiLineComment;
								}
								if (scanner.Peek() == '/') {
									result.Append(scanner.ReadNext());
									state = State.InSingleLineComment;
								}
								break;
							case ParamChar:
								var dbType = DbType.Object;
								var paramName = ReadIdentifier(scanner, allowDigitFirst: true);
								if (paramName.Length == 0) {
									throw new FormatException($"Parameter Name or Index missing, line {scanner.Line}, column {scanner.Column}.");
								}
								if (IsDigit(paramName[0])) {
									paramName = "p" + paramName;
								}
								if (scanner.Peek() == ':') {
									scanner.Read();
									var dbTypeName = ReadIdentifier(scanner, allowDigitFirst: false);
									if (dbTypeName.Length == 0) {
										throw new FormatException($"DbType not specified, line {scanner.Line}, column {scanner.Column}.");
									}
									if (!Enum.TryParse(dbTypeName, ignoreCase: true, out dbType)) {
										throw new FormatException($"Invalid DbType: \"{dbTypeName}\", line {scanner.Line}, column {scanner.Column}.");
									}
								}
								if (!parameters.ContainsKey(paramName)) {
									var p = command.CreateParameter();
									p.ParameterName = paramName;
									p.SourceColumn = paramName;
									p.DbType = dbType;
									parameters.Add(paramName, p);
								} else {
									var p = parameters[paramName];
									if (dbType != DbType.Object) {
										if (p.DbType != dbType) {
											throw new FormatException($"Parameter {paramName} already declared with different DbType, line {scanner.Line}, column {scanner.Column}.");
										}
									}
								}
								result.Append(database.ParameterPrefix);
								result.Append(paramName);
								break;
							default:
								result.Append(ch);
								if (ch.ToString() == database.QuotePrefix)
									state = State.InQuotedIdentifier;
								break;
						}
						break;
					case State.InSingleQuoteString:
						result.Append(ch);
						if (ch == '\'')
							state = State.OnCommandText;
						break;
					case State.InDoubleQuoteString:
						result.Append(ch);
						if (ch == '"')
							state = State.OnCommandText;
						break;
					case State.InSingleLineComment:
						result.Append(ch);
						if (ch == '\n')
							state = State.OnCommandText;
						break;
					case State.InMultiLineComment:
						result.Append(ch);
						if (ch == '*' && scanner.Peek() == '/') {
							result.Append(scanner.ReadNext());
							state = State.OnCommandText;
						}
						break;
					case State.InQuotedIdentifier:
						result.Append(ch);
						if (ch.ToString() == database.QuoteSuffix)
							state = State.OnCommandText;
						break;
				}
			}

			if (state == State.InSingleQuoteString || state == State.InDoubleQuoteString)
				throw new FormatException("String Literal not closed.");

			foreach (var p in parameters.Values) {
				if (p.DbType == DbType.Object) {
					p.DbType = DbType.String;
				}
				command.Parameters.Add(p);
			}
			command.CommandText = result.ToString();
			return command;
		}

		public static int IndexOfLastCommand(Database database, string command)
		{
			var state = State.OnCommandText;
			var lastCommandStart = -1;
			var currentCommandStart = -1;

			var scanner = new Scanner(command);
			while (scanner.Read()) {
				var ch = scanner.Current;

				switch (state) {
					case State.OnCommandText:
						if (ch == '-' && scanner.Peek() == '-') {
							state = State.InSingleLineComment;
							scanner.Read(); // Consume '-'
							continue;
						}
						if (ch == '/' && scanner.Peek() == '*') {
							state = State.InMultiLineComment;
							scanner.Read(); // Consume '*'
							continue;
						}
						if (ch == '\'') {
							state = State.InSingleQuoteString;
							continue;
						}
						if (ch == '"') {
							state = State.InDoubleQuoteString;
							continue;
						}
						if (ch.ToString() == database.QuotePrefix) {
							state = State.InQuotedIdentifier;
							continue;
						}
						if (ch == ';') {
							lastCommandStart = currentCommandStart;
							currentCommandStart = -1;
						} else if (!IsWhiteSpace(ch)) {
							if (currentCommandStart == -1) {
								currentCommandStart = scanner.Index;
							}
						}
						break;
					case State.InSingleLineComment:
						if (ch == '\n') {
							state = State.OnCommandText;
						}
						break;
					case State.InMultiLineComment:
						if (ch == '*' && scanner.Peek() == '/') {
							scanner.Read(); // Consume '/'
							state = State.OnCommandText;
						}
						break;
					case State.InSingleQuoteString:
						if (ch == '\'') {
							state = State.OnCommandText;
						}
						break;
					case State.InDoubleQuoteString:
						if (ch == '"') {
							state = State.OnCommandText;
						}
						break;
					case State.InQuotedIdentifier:
						if (ch.ToString() == database.QuoteSuffix) {
							state = State.OnCommandText;
						}
						break;
				}
			}
			return Math.Max(currentCommandStart, lastCommandStart);
		}

		public static string LastCommandKeyword(Database database, string command)
		{
			var index = IndexOfLastCommand(database, command);
			if (index >= 0) {
				var scanner = new Scanner(command, index);
				return ReadIdentifier(scanner);
			}
			return null;
		}

		class Scanner
		{
			private readonly string str;
			private int index = -1;
			private int line = 1;
			private int column = 1;
			private char current = default;

			public Scanner(string str, int startIndex = 0)
			{
				this.str = str ?? throw new ArgumentNullException(nameof(str));
				index = startIndex - 1;
			}

			public int Index => index;
			public int Line => line;
			public int Column => column;
			public char Current => current;

			public bool Read()
			{
				if (index + 1 < str.Length) {
					var ch = str[++index];
					current = ch;
					if (ch == '\n') {
						line++;
						column = 1;
					} else {
						column++;
					}
					return true;
				}
				current = default;
				return false;
			}

			public int Peek()
			{
				if (index + 1 < str.Length)
					return str[index + 1];
				return -1;
			}

			public char ReadNext()
			{
				Read();
				return Current;
			}
		}

		private static string ReadIdentifier(Scanner reader, bool allowDigitFirst = false)
		{
			var identifier = new StringBuilder();
			var first = (char)reader.Peek();
			if (IsIdentifierChar(first, !allowDigitFirst)) {
				do {
					identifier.Append(reader.ReadNext());
				} while (IsIdentifierChar((char)reader.Peek(), false));
			}
			return identifier.ToString();
		}

		private static bool IsIdentifierChar(char c, bool first)
		{
			return (c == '_') || (first ? IsLetter(c) : IsLetterOrDigit(c));
		}

		private enum State
		{
			OnCommandText,
			InSingleLineComment,
			InMultiLineComment,
			InSingleQuoteString,
			InDoubleQuoteString,
			InQuotedIdentifier,
		}
	}
}
