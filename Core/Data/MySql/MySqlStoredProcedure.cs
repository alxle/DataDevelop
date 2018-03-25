using System;
using System.Text;
using System.Data;
using System.Collections.Generic;

namespace DataDevelop.Data.MySql
{
	internal class MySqlStoredProcedure : StoredProcedure
	{
		private MySqlDatabase database;
		private DataRow schemaRow;

		public MySqlStoredProcedure(MySqlDatabase database, DataRow schemaRow)
			: base(database)
		{
			this.database = database;
			this.schemaRow = schemaRow;
			Name = (string)schemaRow["SPECIFIC_NAME"];
		}

		public override string GenerateAlterStatement()
		{
			return GenerateDropStatement() + ";" + Environment.NewLine + GenerateCreateStatement();
		}

		public override string GenerateCreateStatement()
		{
			var create = new StringBuilder();
			create.AppendLine("DELIMITER $$");
			create.AppendLine();
			create.Append("CREATE PROCEDURE ");
			create.AppendLine(Name);
			create.Append(schemaRow["ROUTINE_DEFINITION"]);
			create.AppendLine();
			create.AppendLine("END $$");
			create.AppendLine("DELIMITER ;");
			return create.ToString();
		}

		public override string GenerateDropStatement()
		{
			return "DROP PROCEDURE " + Name;
		}

		public override string GenerateExecuteStatement()
		{
			var statement = new StringBuilder();
			statement.Append("CALL ");
			statement.AppendLine(Name);
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
			var connection = database.Connection;
			using (var parameters = connection.GetSchema("Procedure Parameters", new[] { null, connection.Database, Name })) {
				parameters.DefaultView.Sort = "ORDINAL_POSITION";
				foreach (DataRowView row in parameters.DefaultView) {
					parametersCollection.Add(new Parameter() {
						Name = (string)row["PARAMETER_NAME"],
						IsOutput = (string)row["PARAMETER_MODE"] == "OUT",
						ProviderType = (string)row["DATA_TYPE"]
					});
				}
			}
		}
	}
}
