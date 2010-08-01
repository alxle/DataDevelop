using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace DataDevelop.Data
{
	internal class DbCommandParser
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
				int index = Convert.ToInt32(parameter.SourceColumn);
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
			IDbCommand command = database.CreateCommand();
			StringBuilder result = new StringBuilder(commandText.Length * 2);

			using (var reader = new StringReader(commandText)) {
				State state = State.OnCommandText;
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
								case '@':
									if (reader.Peek() == '@') {
										reader.Read();
										result.Append('@');
									} else {
										state = State.OnParameter;
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
						case State.OnParameter:
							if (Char.IsLetterOrDigit((char)@char)) {
								StringBuilder paramName = new StringBuilder();
								paramName.Append((char)@char);
								while (Char.IsLetterOrDigit((char)reader.Peek())) {
									paramName.Append((char)reader.Read());
								}
								IDataParameter p = command.CreateParameter();
								p.ParameterName = database.ParameterPrefix + 'p' + command.Parameters.Count.ToString();
								if (!command.Parameters.Contains(p.ParameterName)) {
									p.SourceColumn = paramName.ToString();
									command.Parameters.Add(p);
								}
								result.Append(p.ParameterName);
								state = State.OnCommandText;
							} else {
								throw new FormatException("Parameter index missing.");
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

		private enum State
		{
			OnCommandText,
			InsideString,
			OnParameter,
			End
		}

		public static void Test()
		{
			var db = new DataDevelop.Data.SQLite.SQLiteDatabase("", "");
			var query1 = Parse(db, "SELECT * FROM Table1 WHERE id = @0");
			var query2 = Parse(db, "SELECT * FROM Table1 WHERE id = '@0'");
			var query3 = Parse(db, "SELECT * FROM Table1 WHERE id = ''@@@0");
			var query4 = Parse(db, "SELECT * FROM Table1 WHERE id = ''@@0");
		}
	}
}
