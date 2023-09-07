using System;
using System.Collections.Generic;
using System.Linq;

namespace DataDevelop.Data.PostgreSql
{
	internal class PgSqlStoredProcedure : StoredProcedure
	{
		public PgSqlStoredProcedure(PgSqlDatabase database, string name)
			: base(database)
		{
			Name = name;
		}

		public new PgSqlDatabase Database => (PgSqlDatabase)base.Database;

		public override string GenerateAlterStatement()
		{
			var definition = GetRoutineDefinition();
			if (definition != null) {
				return "CREATE OR REPLACE " + definition;
			}
			return "-- Not supported for this routine.";
		}

		public override string GenerateCreateStatement()
		{
			var definition = GetRoutineDefinition();
			if (definition != null) {
				return "CREATE " + definition;
			}
			return "-- Not supported for this routine.";
		}

		public override string GenerateDropStatement()
		{
			return $"DROP PROCEDURE \"{Name}\";";
		}

		public override string GenerateExecuteStatement()
		{
			var parameters = string.Join(", ", Parameters.Select(p => $"?{p.Name.Replace(' ', '_')}"));
			return $"CALL \"{Name}\" ({parameters})";
		}

		protected override void PopulateParameters(IList<Parameter> parametersCollection)
		{
			using (Database.CreateConnectionScope())
			using (var select = Database.Connection.CreateCommand()) {
				select.CommandText =
					"SELECT " +
					"  p.parameter_mode, p.parameter_name, p.data_type " +
					"FROM information_schema.routines r " +
					"LEFT JOIN information_schema.parameters p ON r.specific_name = p.specific_name " +
					"WHERE" +
					"  r.routine_name = :Name AND r.routine_schema = 'public' " +
					"ORDER BY ordinal_position";
				select.Parameters.AddWithValue(":Name", Name);
				using (var reader = select.ExecuteReader()) {
					while (reader.Read()) {
						var parameter = new Parameter {
							IsOutput = reader.GetString(0) == "OUT",
							Name = reader.GetString(1),
							ProviderType = reader.GetString(2),
						};
						parametersCollection.Add(parameter);
					}
				}

			}
		}

		private string GetRoutineDefinition()
		{
			using (Database.CreateConnectionScope()) {
				RefreshParameters();
				var parameters = 
					string.Join(",\n  ", Parameters.Select(p => $"\"{p.Name}\" {p.ProviderType}"));

				using (var select = Database.Connection.CreateCommand()) {
					select.CommandText =
						"SELECT routine_definition, external_language " +
						"FROM information_schema.routines " +
						"WHERE routine_name = :Name AND routine_schema = 'public'";
					select.Parameters.AddWithValue(":Name", Name);
					using (var reader = select.ExecuteReader()) {
						if (reader.Read()) {
							var definition = reader.IsDBNull(0) ? null : reader.GetString(0);
							var language = reader.IsDBNull(1) ? null : reader.GetString(1);
							if (definition != null) { }
							return
$@"PROCEDURE ""{Name}"" (
  {parameters}
)
LANGUAGE {language}
AS $${definition}$$
";
						}
					}
				}
			}
			return null;
		}
	}
}
