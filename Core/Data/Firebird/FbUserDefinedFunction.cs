using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace DataDevelop.Data.Firebird
{
	class FbUserDefinedFunction : UserDefinedFunction
	{
		private static Dictionary<short, string> types;

		public FbUserDefinedFunction(FbDatabase database, string name)
			: base(database)
		{
			Name = name;
		}

		public override string GenerateAlterStatement()
		{
			return GetFunctionDefinition("ALTER") ?? "-- Not supported.";
		}

		public override string GenerateCreateStatement()
		{
			return GetFunctionDefinition("CREATE") ?? "-- Not supported.";
		}

		public override string GenerateDropStatement()
		{
			return $"DROP FUNCTION \"{Name}\"";
		}

		public override string GenerateExecuteStatement()
		{
			return 
				$"SELECT \"{Name}\"(" + 
				string.Join(", ", Parameters.Select(i => $"?{i.Name}:{i.ParameterType.Name}")) +
				") FROM RDB$DATABASE";
		}

		protected override void PopulateParameters(IList<Parameter> parametersCollection)
		{
			using (var command = (FbCommand)Database.CreateCommand()) {
				command.CommandText =
					"select trim(a.rdb$argument_name), f.rdb$field_type " +
					"from rdb$function_arguments a " +
					"inner join rdb$fields f on a.rdb$field_source = f.rdb$field_name " +
					"where a.rdb$function_name = @Name " +
					"order by a.rdb$argument_position ";
				command.Parameters.AddWithValue("Name", Name);
				using (var reader = command.ExecuteReader()) {
					while (reader.Read()) {
						var name = reader.GetString(0);
						if (string.IsNullOrEmpty(name))
							continue;
						var fieldType = reader.IsDBNull(1) ? (short)0 : reader.GetInt16(1);
						var p = new Parameter {
							Name = name,
							ProviderType = GetTypeName(fieldType)
						};
						p.ParameterType = FbProvider.MapType(p.ProviderType);
						parametersCollection.Add(p);
					}
				}
			}
		}

		private static string GetTypeName(short fieldType)
		{
			if (types == null) {
				types = new Dictionary<short, string> {
					{7, "SMALLINT"},
					{8, "INTEGER" },
					{12, "DATE" },
					{13, "TIME" },
					{14, "CHAR" },
					{16, "BIGINT" },
					{27, "DOUBLE PRECISION" },
					{35, "TIMESTAMP" },
					{37, "VARCHAR" },
					{40, "CSTRING" },
					{45, "BLOB_ID" },
					{261, "BLOB" },
				};
			}
			if (types.TryGetValue(fieldType, out var typeName)) {
				return typeName;
			}
			return null;
		}

		private string GetFunctionDefinition(string createOrAlter)
		{
			using (var command = (FbCommand)Database.CreateCommand()) {
				command.CommandText =
					"select f.rdb$function_source, ff.rdb$field_type " +
					"from rdb$functions f " +
					"inner join rdb$function_arguments a " +
					"  on f.rdb$function_name = a.rdb$function_name and f.rdb$return_argument = a.rdb$argument_position " +
					"inner join rdb$fields ff on a.rdb$field_source = ff.rdb$field_name " +
					"where f.rdb$function_name = @Name";
				command.Parameters.AddWithValue("Name", Name);
				using (var reader = command.ExecuteReader()) {
					if (!reader.Read()) {
						return null;
					}
					var body = reader.IsDBNull(0) ? null : reader.GetString(0);
					if (body == null) {
						return null;
					}
					var returnType = reader.IsDBNull(0) ? (short)0 : reader.GetInt16(1);
					var typeName = GetTypeName(returnType);
					var definition = new StringBuilder();
					definition.AppendLine($"{createOrAlter} FUNCTION \"{Name}\" (");
					var position = 0;
					foreach (var p in Parameters) {
						var comma = (++position == Parameters.Count) ? "" : ",";
						definition.AppendLine($"  {p.Name} {p.ProviderType}" + comma);
					}
					definition.AppendLine(")");
					if (typeName != null) {
						definition.AppendLine($"RETURNS {typeName}");
					}
					definition.AppendLine("AS");
					definition.AppendLine(body);
					return definition.ToString();
				}
			}
		}
	}
}
