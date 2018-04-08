using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataDevelop.Data.Firebird
{
	class FbStoredProcedure : StoredProcedure
	{
		private FbDatabase database;

		public FbStoredProcedure(FbDatabase database, string name)
			: base(database)
		{
			Name = name;
			this.database = database;
		}

		private string GenerateStatement(string createOrAlter)
		{
			using (var procedures = database.Connection.GetSchema("Procedures", new[] { null, null, Name })) {
				if (procedures.Rows.Count != 1)
					return "-- Stored Procedure no longer found in database.";
				var row = procedures.Rows[0];
				var source = (string)row["SOURCE"];
				var sql = new StringBuilder();
				sql.Append(createOrAlter);
				sql.Append(" PROCEDURE ");
				sql.Append(Name);
				sql.AppendLine("(");
				AppendParameters(sql, Parameters.Where(p => !p.IsOutput));
				sql.AppendLine(")");
				if (Parameters.Where(p => p.IsOutput).Any()) {
					sql.AppendLine("RETURNS (");
					AppendParameters(sql, Parameters.Where(p => p.IsOutput));
					sql.AppendLine(")");
				}
				sql.AppendLine(" AS ");
				sql.AppendLine(source);
				return sql.ToString();
			}
		}

		private void AppendParameters(StringBuilder sql, IEnumerable<Parameter> parameters)
		{
			var first = true;
			foreach (var p in parameters) {
				if (first)
					first = false;
				else
					sql.AppendLine(", ");
				sql.Append($"{p.Name} {p.ProviderType}");
			}
			sql.AppendLine();
		}

		public override string GenerateAlterStatement()
		{
			return GenerateStatement("ALTER");
		}

		public override string GenerateCreateStatement()
		{
			return GenerateStatement("CREATE");
		}

		public override string GenerateDropStatement()
		{
			return "DROP PROCEDURE " + Name;
		}

		public override string GenerateExecuteStatement()
		{
			return "EXECUTE PROCEDURE " + Name;
		}

		protected override void PopulateParameters(IList<Parameter> parametersCollection)
		{
			using (var parameters = database.Connection.GetSchema("ProcedureParameters", new[] { null, null, Name })) {
				foreach (DataRow row in parameters.Rows) {
					var p = new Parameter() {
						Name = (string)row["PARAMETER_NAME"],
						IsOutput = (int)row["PARAMETER_DIRECTION"] == 2,
						ProviderType = (string)row["PARAMETER_DATA_TYPE"],
					};
					p.ParameterType = FbProvider.MapType(p.ProviderType);
					if (p.ParameterType == typeof(string)) {
						p.ProviderType += "(" + row["PARAMETER_SIZE"].ToString() + ")";
					}
					parametersCollection.Add(p);
				}
			}
		}
	}
}
