using System.Text;

namespace DataDevelop.Data.Firebird
{
	class FbTrigger : Trigger
	{
		private FbTable table;

		public FbTrigger(FbTable table, string name)
			: base(table)
		{
			Name = name;
			this.table = table;
		}

		public override string GenerateAlterStatement()
		{
			return GenerateStatement("ALTER");
		}

		public override string GenerateCreateStatement()
		{
			return GenerateStatement("CREATE");
		}

		private string GenerateStatement(string createOrAlter)
		{
			using (var triggers = table.Connection.GetSchema("Triggers", new[] { null, null, table.Name, Name })) {
				if (triggers.Rows.Count != 1)
					return "-- Trigger not found.";
				var row = triggers.Rows[0];
				var sql = new StringBuilder();
				sql.Append(createOrAlter);
				sql.Append(" TRIGGER ");
				sql.Append(Name);
				sql.AppendLine();
				sql.Append(row["SOURCE"]);
				return sql.ToString();
			}
		}

		public override string GenerateDropStatement()
		{
			return "DROP TRIGGER " + Name;
		}
	}
}
