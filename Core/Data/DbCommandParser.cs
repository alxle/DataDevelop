using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
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

		public static void BindParameters(IDbCommand command, params object[] values)
		{
			foreach (IDataParameter parameter in command.Parameters) {
				var index = Convert.ToInt32(parameter.SourceColumn.Substring(1));
				var value = values[index];

				if (value == null) {
					parameter.DbType = DbType.Object;
					parameter.Value = DBNull.Value;
				} else {
					parameter.DbType = GetDbType(value);
					parameter.Value = value;
				}
			}
		}

		public static void BindParameters(IDbCommand command, DataRow row)
		{
			foreach (IDataParameter parameter in command.Parameters) {
				var value = row[parameter.SourceColumn];

				if (value == null) {
					parameter.DbType = DbType.Object;
					parameter.Value = DBNull.Value;
				} else {
					parameter.DbType = GetDbType(value);
					parameter.Value = value;
				}
			}
		}

		public static void BindParameters(IDbCommand command, Dictionary<string, object> parameters)
		{
			foreach (IDataParameter parameter in command.Parameters) {
				var value = parameters[parameter.SourceColumn];

				if (value == null) {
					parameter.DbType = DbType.Object;
					parameter.Value = DBNull.Value;
				} else {
					parameter.DbType = GetDbType(value);
					parameter.Value = value;
				}
			}
		}

		public static IDbCommand Parse(Database database, string commandText)
		{
			var command = database.CreateCommand();
			var result = new StringBuilder(commandText.Length * 2);
			const char ParamChar = '?';
			var parameters = new Dictionary<string, DbParameter>(StringComparer.OrdinalIgnoreCase);
			var lineNumber = 1;
			var columnNumber = 1;
			using (var reader = new StringReader(commandText)) {
				var state = State.OnCommandText;
				do {
					var ch = reader.Read();
					if (ch == '\n') {
						lineNumber++;
						columnNumber = 1;
					} else {
						columnNumber++;
					}
					switch (state) {
						case State.OnCommandText:
							switch (ch) {
								case -1:
									state = State.End;
									break;
								case '\'':
									result.Append((char)ch);
									state = State.InsideString;
									break;
								case '-':
									if (reader.Peek() == '-') {
										result.Append((char)ch);
										result.Append((char)reader.Read());
										columnNumber++;
										state = State.InSingleLineComment;
									}
									break;
								case '/':
									if (reader.Peek() == '*') {
										result.Append((char)ch);
										result.Append((char)reader.Read());
										state = State.InMultiLineComment;
									}
									break;
								case ParamChar:
									var dbType = DbType.Object;
									var paramName = ReadIdentifier(reader, allowDigitFirst: true);
									if (paramName.Length == 0) {
										throw new FormatException($"Parameter Name or Index missing, line {lineNumber}, column {columnNumber}.");
									}
									columnNumber += paramName.Length;
									if (IsDigit(paramName[0])) {
										paramName = "p" + paramName;
									}
									if ((char)reader.Peek() == ':') {
										reader.Read();
										columnNumber++;
										var dbTypeName = ReadIdentifier(reader, allowDigitFirst: false);
										if (dbTypeName.Length == 0) {
											throw new FormatException($"DbType not specified, line {lineNumber}, column {columnNumber}.");
										}
										if (!Enum.TryParse(dbTypeName, ignoreCase: true, out dbType)) {
											throw new FormatException($"Invalid DbType: \"{dbTypeName}\", line {lineNumber}, column {columnNumber}.");
										}
										columnNumber += dbTypeName.Length;
									}
									var parameterName = database.ParameterPrefix + paramName;
									if (!parameters.ContainsKey(paramName)) {
										var p = command.CreateParameter();
										p.ParameterName = parameterName;
										p.SourceColumn = paramName;
										p.DbType = dbType;
										parameters.Add(paramName, p);
									} else {
										var p = parameters[paramName];
										if (dbType != DbType.Object) {
											if (p.DbType != dbType) {
												throw new FormatException($"Parameter {paramName} already declared with different DbType, line {lineNumber}, column {columnNumber}.");
											}
										}
									}
									// Do not use p.ParameterName since some providers remove the prefix
									result.Append(parameterName);
									break;
								default:
									result.Append((char)ch);
									break;
							}
							break;
						case State.InsideString:
							switch (ch) {
								case -1:
									throw new FormatException("String Literal not closed.");
								case '\'':
									result.Append((char)ch);
									state = State.OnCommandText;
									break;
								default:
									result.Append((char)ch);
									state = State.InsideString;
									break;
							}
							break;
						case State.InSingleLineComment:
							if (ch == -1)
								state = State.End;
							result.Append((char)ch);
							if (ch == '\n')
								state = State.OnCommandText;
							break;
						case State.InMultiLineComment:
							if (ch == -1)
								state = State.End;
							result.Append((char)ch);
							if (ch == '*' && reader.Peek() == '/') {
								result.Append((char)reader.Read());
								columnNumber++;
								state = State.OnCommandText;
							}
							break;
						default:
							throw new InvalidOperationException("Invalid state.");
					}
				} while (state != State.End);
			}
			foreach (var p in parameters.Values) {
				if (p.DbType == DbType.Object) {
					p.DbType = DbType.String;
				}
				command.Parameters.Add(p);
			}
			command.CommandText = result.ToString();
			return command;
		}

		private static string ReadIdentifier(TextReader reader, bool allowDigitFirst = false)
		{
			var identifier = new StringBuilder();
			var first = (char)reader.Peek();
			if (IsIdentifierChar(first, !allowDigitFirst)) {
				do {
					identifier.Append((char)reader.Read());
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
			InsideString,
			InSingleLineComment,
			InMultiLineComment,
			End,
		}
	}
}
