using System;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace DataDevelop.Data.PostgreSql
{
	internal class PgSqlStoredProcedure : StoredProcedure
	{
		private PgSqlDatabase database;
		private DataRow schemaRow;

		public PgSqlStoredProcedure(PgSqlDatabase database, DataRow schemaRow)
			: base(database)
		{
			this.database = database;
			this.schemaRow = schemaRow;
			this.Name = (string)schemaRow["SPECIFIC_NAME"];
		}

		public override string GenerateAlterStatement()
		{
			return this.GenerateDropStatement() + ";" + Environment.NewLine + this.GenerateCreateStatement();
		}

		public override string GenerateCreateStatement()
		{
			// TODO
			return null;
		}

		public override string GenerateDropStatement()
		{
			return "DROP PROCEDURE " + this.Name;
		}

		public override string GenerateExecuteStatement()
		{
			var statement = new StringBuilder();
			statement.Append("EXECUTE PROCEDURE ");
			statement.AppendLine(this.Name);
			statement.AppendLine("(");
			bool first = true;
			foreach (var p in Parameters) {
				if (!p.IsOutput) {
					if (first) {
						first = false;
					} else {
						statement.Append(',');
						statement.AppendLine();
					}
					statement.Append('\t');
					statement.Append(this.database.ParameterPrefix);
					statement.Append(p.Name);
				}
			}
			statement.AppendLine();
			statement.AppendLine(")");
			return statement.ToString();
		}

		protected override void PopulateParameters(IList<Parameter> parametersCollection)
		{
			// TODO
		}
	}
}
