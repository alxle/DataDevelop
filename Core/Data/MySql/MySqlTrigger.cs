using System;
using System.Text;

namespace DataDevelop.Data.MySql
{
	internal class MySqlTrigger : Trigger
	{
		private MySqlTable table;

		public MySqlTrigger(MySqlTable table, string name)
			: base(table)
		{
			this.table = table;
			Name = name;
		}

		public override string GenerateCreateStatement()
		{
			var triggers = table.Connection.GetSchema("Triggers", new[] { null, table.Connection.Database, table.Name, Name });
			if (triggers.Rows.Count != 1)
				return "-- Trigger not found in database.";
			var row = triggers.Rows[0];
			var create = new StringBuilder();
			create.Append("CREATE TRIGGER ");
			create.Append(Name);
			create.Append(' ');
			create.Append(row["ACTION_TIMING"]);
			create.Append(' ');
			create.Append(row["EVENT_MANIPULATION"]);
			create.Append(" ON ");
			create.Append(Table.QuotedName);
			create.Append(row["ACTION_STATEMENT"]);
			return create.ToString();
		}

		public override string GenerateAlterStatement()
		{
			return GenerateDropStatement() + ";" + Environment.NewLine + GenerateCreateStatement();
		}

		public override string GenerateDropStatement()
		{
			return "DROP TRIGGER " + Name;
		}
	}
}
