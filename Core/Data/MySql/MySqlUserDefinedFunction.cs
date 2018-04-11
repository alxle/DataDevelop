using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataDevelop.Data.MySql
{
	class MySqlUserDefinedFunction : UserDefinedFunction
	{
		private MySqlDatabase database;

		public MySqlUserDefinedFunction(MySqlDatabase database, string name)
			: base(database)
		{
			this.database = database;
			Name = name;
		}

		public override string GenerateAlterStatement()
		{
			return GenerateDropStatement() + ";" + Environment.NewLine + GenerateCreateStatement();
		}

		public override string GenerateCreateStatement()
		{
			var connection = database.Connection;
			var procedures = connection.GetSchema("Procedures", new[] { null, database.Connection.Database, Name });
			if (procedures.Rows.Count != 1)
				return "-- Function not found.";
			var procedure = procedures.Rows[0];
			var parameters = connection.GetSchema("Procedure Parameters", new[] { null, connection.Database, Name });
			parameters.DefaultView.Sort = "ORDINAL_POSITION";
			var create = new StringBuilder();
			create.Append("CREATE FUNCTION ");
			create.AppendLine(Name);
			create.AppendLine("(");
			string returnType = null;
			var first = true;
			foreach (DataRowView parameter in parameters.DefaultView) {
				var name = (string)parameter["PARAMETER_NAME"];
				var mode = (string)parameter["PARAMETER_MODE"];
				var type = (string)parameter["DTD_IDENTIFIER"];
				if (string.Equals(name, "RETURN_VALUE", StringComparison.OrdinalIgnoreCase)) {
					returnType = type;
					continue;
				}
				create.Append("  ");
				if (first)
					first = false;
				else
					create.Append(',');
				if (string.Equals(mode, "IN", StringComparison.OrdinalIgnoreCase))
					create.AppendLine($"{name} {type}");
				else
					create.AppendLine($"{mode} {name} {type}");
			}
			if (returnType == null) {
				return "-- Error: return_value not found in function.";
			}
			create.AppendLine($") RETURNS {returnType}");
			var definition = (string)procedure["ROUTINE_DEFINITION"];
			create.Append(definition);
			create.AppendLine();
			return create.ToString();
		}

		public override string GenerateDropStatement()
		{
			return "DROP FUNCTION " + Name;
		}

		public override string GenerateExecuteStatement()
		{
			var statement = new StringBuilder();
			statement.Append("SELECT ");
			statement.AppendLine(Name);
			statement.AppendLine("(");
			var first = true;
			foreach (var p in Parameters) {
				if (!p.IsOutput) {
					if (first) {
						first = false;
					} else {
						statement.Append(',');
						statement.AppendLine();
					}
					statement.Append('\t');
					statement.Append(database.ParameterPrefix);
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
			var parameters = connection.GetSchema("Procedure Parameters", new[] { null, connection.Database, Name });
			parameters.DefaultView.Sort = "ORDINAL_POSITION";
			foreach (DataRowView row in parameters.DefaultView) {
				if (!string.Equals((string)row["PARAMETER_NAME"], "RETURN_VALUE", StringComparison.OrdinalIgnoreCase)) {
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
