using System;
using System.Data;
using System.Text;

namespace DataDevelop.Data.PostgreSql
{
	internal class PgSqlTrigger : Trigger
	{
		private PgSqlTable table;
		private DataRow schemaRow;

		public PgSqlTrigger(PgSqlTable table, DataRow schemaRow)
			: base(table)
		{
			this.table = table;
			this.schemaRow = schemaRow;
			this.Name = (string)schemaRow["TRIGGER_NAME"];
		}

		public override string GenerateCreateStatement()
		{
			var create = new StringBuilder();
			create.Append("CREATE TRIGGER ");
			create.Append(this.Name);
			create.Append(' ');
			create.Append(this.schemaRow["ACTION_TIMING"]);
			create.Append(' ');
			create.Append(this.schemaRow["EVENT_MANIPULATION"]);
			create.Append(" ON ");
			create.Append(this.Table.QuotedName);
			create.Append(this.schemaRow["ACTION_STATEMENT"]);
			return create.ToString();
		}

		public override string GenerateAlterStatement()
		{
			return this.GenerateDropStatement() + ";" + Environment.NewLine + this.GenerateCreateStatement();
		}

		public override string GenerateDropStatement()
		{
			return "DROP TRIGGER " + this.Name;
		}
	}
}
