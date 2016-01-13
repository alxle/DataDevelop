using System;
using System.Data;
using System.Text;

namespace DataDevelop.Data.MySql
{
	internal class MySqlTrigger : Trigger
	{
		private MySqlTable table;
		private DataRow schemaRow;

		public MySqlTrigger(MySqlTable table, DataRow schemaRow)
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
