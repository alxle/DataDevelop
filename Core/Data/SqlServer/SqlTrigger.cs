using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
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
			using (this.table.Database.CreateConnectionScope()) {
				using (var select = this.table.Database.Connection.CreateCommand()) {
					select.CommandText = @"SELECT
SCHEMA_NAME(tbl.schema_id) AS [Table_Schema],
tbl.name AS [Table_Name],
tr.name AS [Name],
trr.is_instead_of_trigger AS [InsteadOf],
CAST(ISNULL(tei.object_id,0) AS bit) AS [Insert],
CAST(ISNULL(ted.object_id,0) AS bit) AS [Delete],
CAST(ISNULL(teu.object_id,0) AS bit) AS [Update],
CASE WHEN tr.type = N'TR' THEN 1 WHEN tr.type = N'TA' THEN 2 ELSE 1 END AS [ImplementationType],
CAST(OBJECTPROPERTYEX(tr.object_id,N'ExecIsAnsiNullsOn') AS bit) AS [AnsiNullsStatus],
CAST(OBJECTPROPERTYEX(tr.object_id,N'ExecIsQuotedIdentOn') AS bit) AS [QuotedIdentifierStatus],
NULL AS [Text],
CAST(tr.is_ms_shipped AS bit) AS [IsSystemObject],
CASE WHEN ted.is_first = 1 THEN 0 WHEN ted.is_last = 1 THEN 2 ELSE 1 END AS [DeleteOrder],
CASE WHEN tei.is_first = 1 THEN 0 WHEN tei.is_last = 1 THEN 2 ELSE 1 END AS [InsertOrder],
CASE WHEN teu.is_first = 1 THEN 0 WHEN teu.is_last = 1 THEN 2 ELSE 1 END AS [UpdateOrder],
ISNULL(smtr.definition, ssmtr.definition) AS [Definition]
FROM
sys.tables AS tbl
INNER JOIN sys.objects AS tr ON (tr.type in ('TR', 'TA')) AND (tr.parent_object_id=tbl.object_id)
LEFT OUTER JOIN sys.assembly_modules AS mod ON mod.object_id = tr.object_id
INNER JOIN sys.triggers AS trr ON trr.object_id = tr.object_id
LEFT OUTER JOIN sys.trigger_events AS tei ON tei.object_id = tr.object_id and tei.type=1
LEFT OUTER JOIN sys.trigger_events AS ted ON ted.object_id = tr.object_id and ted.type=3
LEFT OUTER JOIN sys.trigger_events AS teu ON teu.object_id = tr.object_id and teu.type=2
LEFT OUTER JOIN sys.sql_modules AS smtr ON smtr.object_id = tr.object_id
LEFT OUTER JOIN sys.system_sql_modules AS ssmtr ON ssmtr.object_id = tr.object_id
WHERE
(tbl.name=@TableName and SCHEMA_NAME(tbl.schema_id)=@TableSchema)
ORDER BY
[Table_Schema] ASC,[Table_Name] ASC,[Name] ASC";
					select.Parameters.AddWithValue("@TableName", this.table.Name);
					select.Parameters.AddWithValue("@TableSchema", this.table.Schema);
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
			var create = this.GenerateCreateStatement();
			if (!String.IsNullOrEmpty(create)) {
				return Regex.Replace(create, @"^\s*CREATE\s+", "ALTER ");
			}
			return null;
		}

		public override string GenerateDropStatement()
		{
			return "DROP TRIGGER " + this.Name;
		}
	}
}
