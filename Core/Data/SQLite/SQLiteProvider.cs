using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace DataDevelop.Data.SQLite
{
	public class SQLiteProvider : DbProvider
	{
		private static SQLiteProvider provider;

		public static SQLiteProvider Instance => provider ?? (provider = new SQLiteProvider());

		private SQLiteProvider() { }

		public override string Name => "SQLite";

		public override bool IsFileBased => true;

		public override string CreateDatabaseFile(string fileName)
		{
			using (var db = File.Create(fileName)) {
				// let 'using' dispose file
			}
			return "Data Source=" + fileName;
		}

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new SQLiteDatabase(name, connectionString);
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SQLiteConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "SQLite " + SQLiteConnection.SQLiteVersion;
		}

		private static string GetColumnType(Type type)
		{
			if (type == typeof(string) || type == typeof(char) || type == typeof(Guid))
				return "TEXT";
			if (type == typeof(byte) || type == typeof(short) || type == typeof(int) || type == typeof(long)
				|| type == typeof(sbyte) || type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong))
				return "INT";
			if (type == typeof(long) || type == typeof(ulong))
				return "INTEGER";
			if (type == typeof(bool))
				return "BOOLEAN";
			if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
				return "REAL";
			if (type == typeof(decimal))
				return "NUMERIC";
			if (type == typeof(DateTime))
				return "DATETIME";
			if (type == typeof(byte[]))
				return "BLOB";
			return "NULL";
		}

		public static string GenerateCreateTableScript(string tableName, params Column[] columns)
		{
			var primaryKey = columns.Where(c => c.InPrimaryKey).ToArray();
			var create = new StringBuilder();
			create.AppendFormat("CREATE TABLE [{0}] (", tableName);
			var i = 0;
			foreach (var column in columns) {
				;
				create.AppendLine(i++ > 0 ? "," : "");
				if (column.IsIdentity && column.InPrimaryKey && primaryKey.Length == 1) {
					create.AppendFormat("  [{0}] INTEGER PRIMARY KEY", column.Name);
					continue;
				}
				var columnType = GetColumnType(column.Type);
				create.AppendFormat("  [{0}] {1}", column.Name, columnType);
				if (column.InPrimaryKey && primaryKey.Length == 1) {
					create.Append(" PRIMARY KEY");
				}
			}
			if (primaryKey.Length > 1) {
				create.AppendLine(",");
				var keys = string.Join(", ", primaryKey.Select(c => "[" + c.Name + "]"));
				create.AppendFormat("PRIMARY KEY ({0})", keys);
			}
			create.AppendLine(")");
			return create.ToString();
		}

		public static string GenerateCreateTableScript(string tableName, IDataReader reader)
		{
			var create = new StringBuilder();
			create.AppendFormat("CREATE TABLE [{0}] (", tableName);
			for (var i = 0; i < reader.FieldCount; i++) {
				var name = reader.GetName(i);
				var type = reader.GetFieldType(i);
				var columnType = GetColumnType(type);
				create.AppendLine(i > 0 ? "," : "");
				create.AppendFormat("  [{0}] {1}", name, columnType);
			}
			create.AppendLine(")");
			return create.ToString();
		}
	}
}
