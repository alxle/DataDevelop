using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace DataDevelop.Data
{
	public class DbCommandParser
	{
		public static DbType GetDbType(object value)
		{
			if (value is string) {
				return DbType.String;
			}
			if (value is int) {
				return DbType.Int32;
			}
			if (value is short) {
				return DbType.Int16;
			}
			if (value is long) {
				return DbType.Int64;
			}
			if (value is byte[]) {
				return DbType.Binary;
			}
			if (value is bool) {
				return DbType.Boolean;
			}
			if (value is byte) {
				return DbType.Byte;
			}
			if (value is DateTime) {
				return DbType.DateTime;
			}
			if (value is decimal) {
				return DbType.Currency;
			}
			if (value is float) {
				return DbType.Single;
			}
			if (value is double) {
				return DbType.Double;
			}
			if (value is Guid) {
				return DbType.Guid;
			}
			if (value is ushort) {
				return DbType.UInt16;
			}
			if (value is uint) {
				return DbType.UInt32;
			}
			if (value is ulong) {
				return DbType.UInt64;
			}
			return DbType.Object;
		}

		public static void BindParameters(IDbCommand command, params object[] values)
		{
			foreach (IDataParameter parameter in command.Parameters) {
				int index = Convert.ToInt32(parameter.SourceColumn.Substring(1));
				object value = values[index];

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
				object value = row[parameter.SourceColumn];

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
				object value = parameters[parameter.SourceColumn];

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

			using (var reader = new StringReader(commandText)) {
				var state = State.OnCommandText;
				do {
					int @char = reader.Read();
					switch (state) {
						case State.OnCommandText:
							switch (@char) {
								case -1:
									state = State.End;
									break;
								case '\'':
									result.Append((char)@char);
									state = State.InsideString;
									break;
								case ParamChar:
									if (reader.Peek() == ParamChar) {
										reader.Read();
										result.Append(ParamChar);
									} else {
										var dbType = DbType.Object;
										var paramName = ReadIdentifier(reader);
										if (paramName.Length == 0) {
											throw new FormatException("Parameter Name or Index missing.");
										}
										if ((char)reader.Peek() == ':') {
											reader.Read();
											var dbTypeName = ReadIdentifier(reader);
											if (dbTypeName.Length == 0) {
												throw new FormatException("DbType not specified.");
											} else {
												dbType = (DbType)Enum.Parse(typeof(DbType), dbTypeName, true);
											}
										}

										var p = command.CreateParameter();
										p.ParameterName = database.ParameterPrefix + paramName;
										if (!command.Parameters.Contains(p.ParameterName)) {
											p.SourceColumn = paramName;
											p.DbType = dbType;
											command.Parameters.Add(p);
										} else {
											if (p.DbType != dbType) {
												throw new FormatException(
													String.Format("Parameter {0} already declared with diferent DbType", paramName));
											}
										}
										// Do not use p.ParameterName since some providers remove the prefix
										result.Append(database.ParameterPrefix + paramName);
									}
									break;
								default:
									result.Append((char)@char);
									break;
							}
							break;
						case State.InsideString:
							switch (@char) {
								case -1:
									throw new FormatException("String Literal not closed.");
								case '\'':
									result.Append((char)@char);
									state = State.OnCommandText;
									break;
								default:
									result.Append((char)@char);
									state = State.InsideString;
									break;
							}
							break;
						default:
							throw new InvalidOperationException("Invalid state.");
					}
				} while (state != State.End);
			}

			command.CommandText = result.ToString();
			return command;
		}

		private static string ReadIdentifier(StringReader reader)
		{
			var paramName = new StringBuilder();
			if (IsIdentifierChar((char)reader.Peek(), false)) {
				if (Char.IsDigit((char)reader.Peek())) {
					paramName.Append('p');
				}
				do {
					paramName.Append((char)reader.Read());
				} while (IsIdentifierChar((char)reader.Peek(), false));
			}
			return paramName.ToString();
		}

		private static bool IsIdentifierChar(char c, bool first)
		{
			return (c == '_') || (first ? Char.IsLetter(c) : Char.IsLetterOrDigit(c));
		}

		private static string ReadWhites(StringReader reader)
		{
			while (Char.IsWhiteSpace((char)reader.Peek())) {
				reader.Read();
			}
			return " ";
		}

		private enum State
		{
			OnCommandText,
			InsideString,
			End
		}
	}
}
