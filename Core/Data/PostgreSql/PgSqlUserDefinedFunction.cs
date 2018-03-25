using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataDevelop.Data.PostgreSql
{
	sealed class PgSqlUserDefinedFunction : UserDefinedFunction
	{
		private PgSqlDatabase database;

		public PgSqlUserDefinedFunction(PgSqlDatabase database, string name)
			: base(database)
		{
			this.database = database;
			Name = name;
		}

		public override string GenerateAlterStatement()
		{
			return GenerateCreateStatement();
		}

		public override string GenerateCreateStatement()
		{
			using (var command = database.Connection.CreateCommand()) {
				command.CommandText =
					"SELECT pg_get_functiondef(p.oid) " +
					"FROM pg_catalog.pg_proc p " +
					" LEFT JOIN pg_catalog.pg_namespace n ON n.oid = p.pronamespace " +
					"WHERE pg_catalog.pg_function_is_visible(p.oid) " +
					"  AND n.nspname = :schema " +
					"  AND p.proname = :name ";
				command.Parameters.AddWithValue(":schema", "public");
				command.Parameters.AddWithValue(":name", Name);
				return command.ExecuteScalar() as string;
			}
		}

		public override string GenerateDropStatement()
		{
			return null;
		}

		public override string GenerateExecuteStatement()
		{
			return null;
		}

		protected override void PopulateParameters(IList<Parameter> parametersCollection)
		{
			using (var command = database.Connection.CreateCommand()) {
				command.CommandText =
					"SELECT pg_catalog.pg_get_function_arguments(p.oid) as \"Arguments\" " +
					"FROM pg_catalog.pg_proc p " +
					" LEFT JOIN pg_catalog.pg_namespace n ON n.oid = p.pronamespace " +
					"WHERE pg_catalog.pg_function_is_visible(p.oid) " +
					"  AND n.nspname = :schema " +
					"  AND p.proname = :name ";
				command.Parameters.AddWithValue(":schema", "public");
				command.Parameters.AddWithValue(":name", Name);
				if (command.ExecuteScalar() is string arguments) {
					var fields = arguments.Split(',');
					foreach (var field in fields) {
						var parts = field.Trim().Split(' ');
						if (parts.Length > 1) {
							var parameter = new Parameter();
							if (parts[0] == "OUT") {
								parameter.IsOutput = true;
								parameter.Name = parts[1];
								parameter.ProviderType = string.Join(" ", parts.Skip(2).ToArray());
							} else {
								parameter.Name = parts[0];
								parameter.ProviderType = string.Join(" ", parts.Skip(1).ToArray());
							}
							parametersCollection.Add(parameter);
						}
					}
				}
			}
		}
	}
}
