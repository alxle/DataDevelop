using System.Text.RegularExpressions;

namespace DataDevelop.Data.SqlServer
{
	internal sealed class SqlTrigger : Trigger
	{
		private SqlTable table;

		public SqlTrigger(SqlTable table)
			: base(table)
		{
			this.table = table;
		}

		public override string GenerateCreateStatement()
		{
			using (table.Database.CreateConnectionScope()) {
				using (var select = table.Database.Connection.CreateCommand()) {
					select.CommandText =
						"SELECT ISNULL(smtr.definition, ssmtr.definition) AS [Definition] " +
						"FROM sys.tables AS tbl " +
						"INNER JOIN sys.objects AS tr ON tr.type in ('TR', 'TA') AND tr.parent_object_id = tbl.object_id " +
						"INNER JOIN sys.triggers AS trr ON trr.object_id = tr.object_id " +
						"LEFT OUTER JOIN sys.sql_modules AS smtr ON smtr.object_id = tr.object_id " +
						"LEFT OUTER JOIN sys.system_sql_modules AS ssmtr ON ssmtr.object_id = tr.object_id " +
						"WHERE tbl.name = @TableName AND SCHEMA_NAME(tbl.schema_id) = @TableSchema AND tr.name = @Name";
					select.Parameters.AddWithValue("@TableName", table.TableName);
					select.Parameters.AddWithValue("@TableSchema", table.SchemaName);
					select.Parameters.AddWithValue("@Name", Name);
					using (var reader = select.ExecuteReader()) {
						if (reader.Read()) {
							return reader.GetString(reader.GetOrdinal("Definition"));
						}
					}
				}
			}
			return null;
		}

		public override string GenerateAlterStatement()
		{
			var create = GenerateCreateStatement();
			if (!string.IsNullOrEmpty(create)) {
				return Regex.Replace(create, @"^\s*CREATE\s+", "ALTER ");
			}
			return null;
		}

		public override string GenerateDropStatement()
		{
			return "DROP TRIGGER [" + table.SchemaName + "].[" + Name + "]";
		}
	}
}
