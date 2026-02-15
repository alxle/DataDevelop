using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataDevelop.Data.SqlServer
{
	internal class SqlProvider : DbProvider
	{
		private static SqlProvider provider;

		private SqlProvider() { }

		public static SqlProvider Instance => provider ?? (provider = new SqlProvider());

		public override string Name => "SqlServer";

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new SqlDatabase(name, connectionString);
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqlConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "Microsoft SQL Server";
		}

		private static string GetColumnType(Column column)
		{
			// Reference: https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings
			var type = column.Type;
			if (type == typeof(long))
				return "bigint";
			if (type == typeof(byte[])) {
				if (column.Size == null)
					return "varbinary(MAX)";
				return $"varbinary({column.Size})";
			}
			if (type == typeof(bool))
				return "bit";
			if (type == typeof(char))
				return "char(1)";
			if (type == typeof(DateTime)) {
				if (column.ProviderType?.ToLower() == "date")
					return "date"; // SQL Server 2008 and later
				return "datetime";
			}
			if (type == typeof(DateTimeOffset))
				return "datetimeoffset"; // SQL Server 2008 and later
			if (type == typeof(decimal)) {
				if (column.Precision != null) {
					if (column.Scale != null)
						return $"decimal({column.Precision}, {column.Scale})";
					return $"decimal({column.Precision})";
				}
				return "decimal";
			}
			if (type == typeof(double))
				return "float";
			if (type == typeof(int))
				return "int";
			if (type == typeof(string)) {
				if (column.Size == null || column.Size < 0)
					return "nvarchar(MAX)";
				return $"nvarchar({column.Size})";
			}
			if (type == typeof(float))
				return "real";
			if (type == typeof(short))
				return "smallint";
			if (type == typeof(TimeSpan))
				return "time"; // SQL Server 2008 and later
			if (type == typeof(byte))
				return "tinyint";
			if (type == typeof(Guid))
				return "uniqueidentifier";
			throw new NotSupportedException($"No mapping for Type {type}");
		}

		public static string GenerateCreateTableScript(string schemaName, string tableName, params Column[] columns)
		{
			var primaryKey = columns.Where(c => c.InPrimaryKey).ToArray();
			var create = new StringBuilder();
			create.AppendFormat("CREATE TABLE [{0}].[{1}] (", schemaName, tableName);
			var i = 0;
			foreach (var column in columns) {
				;
				create.AppendLine(i++ > 0 ? "," : "");
				if (column.IsIdentity && column.InPrimaryKey && primaryKey.Length == 1) {
					create.AppendFormat("  [{0}] INTEGER PRIMARY KEY", column.Name);
					continue;
				}
				var columnType = column.ProviderType ?? GetColumnType(column);
				create.AppendFormat("  [{0}] {1}", column.Name, columnType);
				if (column.IsNullable.HasValue && !column.IsNullable.Value) {
					create.Append(" NOT NULL");
				}
				if (column.InPrimaryKey && primaryKey.Length == 1) {
					create.Append(" PRIMARY KEY");
				}
			}
			if (primaryKey.Length > 1) {
				create.AppendLine(",");
				var keys = string.Join(", ", primaryKey.Select(c => "[" + c.Name + "]"));
				create.AppendFormat("PRIMARY KEY ({0})", keys);
			}
			create.AppendLine();
			create.AppendLine(")");
			return create.ToString();
		}
	}
}
