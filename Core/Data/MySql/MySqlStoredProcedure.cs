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
			this.Name = (string)schemaRow["SPECIFIC_NAME"];
		}

		public override string GenerateAlterStatement()
		{
			return this.GenerateDropStatement() + ";" + Environment.NewLine + this.GenerateCreateStatement();
		}

		public override string GenerateCreateStatement()
		{
			var create = new StringBuilder();
			create.AppendLine("DELIMITER $$");
			create.AppendLine();
			create.Append("CREATE PROCEDURE ");
			create.AppendLine(this.Name);
			create.Append(this.schemaRow["ROUTINE_DEFINITION"]);
			create.AppendLine();
			create.AppendLine("END $$");
			create.AppendLine("DELIMITER ;");
			return create.ToString();
		}

		public override string GenerateDropStatement()
		{
			return "DROP PROCEDURE " + this.Name;
		}

		public override string GenerateExecuteStatement()
		{
			var statement = new StringBuilder();
			statement.Append("CALL ");
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
			var connection = this.database.Connection;
			var parameters = connection.GetSchema("Procedure Parameters", 
				new string[] { null, connection.Database, this.Name });
			var view = new DataView(parameters);
			view.Sort = "ORDINAL_POSITION";
			foreach (DataRow row in view.ToTable().Rows) {
				parametersCollection.Add(new Parameter()
				{
					Name = (string)row["PARAMETER_NAME"],
					IsOutput = (string)row["PARAMETER_MODE"] == "OUT",
					ProviderType = (string)row["DATA_TYPE"]
				});
			}
		}
	}
}
