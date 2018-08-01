using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataDevelop.Data.PostgreSql
{
	internal class PgSqlTrigger : Trigger
	{
		private PgSqlTable table;

		public PgSqlTrigger(PgSqlTable table, string name)
			: base(table)
		{
			this.table = table;
			Name = name;
		}

		public override string GenerateCreateStatement()
		{
			using (Database.CreateConnectionScope()) {
				using (var command = table.Connection.CreateCommand()) {
					command.CommandText =
						"select event_manipulation, action_statement, " +
						"       action_orientation, action_timing " +
						"from information_schema.triggers t " +
						"where t.trigger_name = :trigger_name " +
						"      and t.event_object_schema = :table_schema " +
						"      and t.event_object_table = :table_name ";
					command.Parameters.AddWithValue(":trigger_name", Name);
					command.Parameters.AddWithValue(":table_schema", "public");
					command.Parameters.AddWithValue(":table_name", table.Name);
					using (var reader = command.ExecuteReader()) {
						if (reader.Read()) {
							var events = new List<string>() { reader.GetString(0) };
							var actionStatement = reader.GetString(1);
							var actionOrientation = reader.GetString(2);
							var actionTiming = reader.GetString(3);
							while (reader.Read()) {
								events.Add(reader.GetString(0));
							}
							var create = new StringBuilder();
							create.Append($"CREATE TRIGGER {Name} ");
							create.AppendLine($"{actionTiming} {string.Join(" OR ", events.ToArray())} ");
							create.Append($"  ON {table.QuotedName} ");
							if (actionOrientation == "ROW") {
								create.Append("FOR EACH ");
							}
							create.AppendLine(actionOrientation);
							create.AppendLine(actionStatement);
							return create.ToString();
						}
					}
				}
			}
			return null;
		}

		public override string GenerateAlterStatement()
		{
			var create = GenerateCreateStatement();
			if (create == null) {
				return null;
			}
			return GenerateDropStatement() + ";" + Environment.NewLine + create;
		}

		public override string GenerateDropStatement()
		{
			return "DROP TRIGGER " + Name + " ON " + table.QuotedName;
		}
	}
}
