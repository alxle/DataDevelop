using System;
using System.Collections.Generic;
using System.Linq;

namespace DataDevelop.Data.PostgreSql
{
	sealed class PgSqlUserDefinedFunction : UserDefinedFunction
	{
		private readonly PgSqlDatabase db;
		private readonly int oid;
		private readonly string routineName;

		public PgSqlUserDefinedFunction(PgSqlDatabase database, string specificName, string name)
			: base(database)
		{
			this.db = database;
			var lastIndex = specificName.LastIndexOf('_') + 1;
			oid = Convert.ToInt32(specificName.Substring(lastIndex, specificName.Length - lastIndex));
			routineName = name;
			Name = specificName;
		}

		public override string GenerateAlterStatement()
		{
			return GenerateCreateStatement();
		}

		public override string GenerateCreateStatement()
		{
			using (var command = db.Connection.CreateCommand()) {
				command.CommandText =
					"SELECT pg_get_functiondef(p.oid) " +
					"FROM pg_catalog.pg_proc p " +
					"WHERE p.oid = :oid";
				command.Parameters.AddWithValue(":oid", oid);
				return command.ExecuteScalar() as string;
			}
		}

		public override string GenerateDropStatement()
		{
			var parameterTypes = string.Join(", ", Parameters.Select(i => i.ProviderType));
			if (parameterTypes.Length > 0) {
				parameterTypes = $"DROP FUNCTION \"{routineName}\"({parameterTypes})";
			}
			return $"DROP FUNCTION \"{routineName}\"";
		}

		public override string GenerateExecuteStatement()
		{
			var parameterTypes = string.Join(", ", Parameters.Select(i => $"?{i.Name}:{i.ParameterType?.Name ?? "String"}"));
			return $"SELECT \"{routineName}\"({parameterTypes})";
		}

		protected override void PopulateParameters(IList<Parameter> parametersCollection)
		{
			using (var select = db.Connection.CreateCommand()) {
				select.CommandText =
					"SELECT " +
					"  p.parameter_mode, p.parameter_name, p.data_type " +
					"FROM information_schema.parameters p " +
					"WHERE" +
					"  p.specific_name = :Name AND p.specific_schema = 'public' " +
					"ORDER BY p.ordinal_position";
				select.Parameters.AddWithValue(":Name", Name);
				using (var reader = select.ExecuteReader()) {
					while (reader.Read()) {
						var parameter = new Parameter {
							IsOutput = !reader.IsDBNull(0) && reader.GetString(0) == "OUT",
							Name = reader.GetString(1),
							ProviderType = reader.GetString(2),
							ParameterType = PgSqlProvider.MapType(reader.GetString(2))
						};
						parametersCollection.Add(parameter);
					}
				}
			}
		}
	}
}
