using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DataDevelop.Data.SqlServer
{
	sealed class SqlUserDefinedFunction : UserDefinedFunction, ISqlObject
	{
		private SqlDatabase database;

		public SqlUserDefinedFunction(SqlDatabase database, string schemaName, string functionName)
			: base(database)
		{
			this.database = database;
			SchemaName = schemaName;
			FunctionName = functionName;
			Name = $"{SchemaName}.{FunctionName}";
		}

		public string SchemaName { get; set; }

		public string FunctionName { get; set; }

		public string ObjectName => FunctionName;

		public override string GenerateAlterStatement()
		{
			var create = GenerateCreateStatement();
			if (!string.IsNullOrEmpty(create)) {
				return Regex.Replace(create, @"^\s*CREATE\s+", "ALTER ");
			}
			return null;
		}

		public override string GenerateCreateStatement()
		{
			using (database.CreateConnectionScope()) {
				using (var select = database.Connection.CreateCommand()) {
					select.CommandText = 
						"SELECT ISNULL(smsp.definition, ssmsp.definition) AS [Definition] "+
						"FROM sys.all_objects AS fn "+
						"LEFT OUTER JOIN sys.sql_modules AS smsp ON smsp.object_id = fn.object_id "+
						"LEFT OUTER JOIN sys.system_sql_modules AS ssmsp ON ssmsp.object_id = fn.object_id "+
						"WHERE fn.type in ('FN', 'IF', 'TF') "+
						"  AND fn.name = @Name AND SCHEMA_NAME(fn.schema_id) = @Schema";
					select.Parameters.AddWithValue("@Name", FunctionName);
					select.Parameters.AddWithValue("@Schema", SchemaName);
					var obj = select.ExecuteScalar();
					if (obj != null && obj != DBNull.Value) {
						return (string)obj;
					}
					return null;
				}
			}
		}

		public override string GenerateDropStatement()
		{
			return "DROP FUNCTION [" + SchemaName + "].[" + FunctionName + "]";
		}

		public override string GenerateExecuteStatement()
		{
			var statement = new StringBuilder();
			statement.Append("SELECT [");
			statement.Append(SchemaName);
			statement.Append("].[");
			statement.Append(FunctionName);
			statement.Append("](");
			var first = true;
			foreach (var p in Parameters) {
				if (first) {
					first = false;
				} else {
					statement.Append(", ");
				}
				statement.Append(p.Name);
			}
			statement.AppendLine(")");
			return statement.ToString();
		}

		protected override void PopulateParameters(IList<Parameter> parametersCollection)
		{
			using (database.CreateConnectionScope()) {
				using (var select = (SqlCommand)database.CreateCommand()) {
					select.CommandText =
						"SELECT p.Name, type_name(p.system_type_id) as Type " +
						"FROM sys.parameters p " +
						"INNER JOIN sys.objects o ON p.object_id = o.object_id " +
						"WHERE o.name = @Name AND schema_name(o.schema_id) = @Schema AND p.parameter_id > 0" +
						"ORDER BY p.parameter_id";
					select.Parameters.AddWithValue("@Name", FunctionName);
					select.Parameters.AddWithValue("@Schema", SchemaName);
					using (var reader = select.ExecuteReader()) {
						while (reader.Read()) {
							var parameter = new Parameter {
								IsOutput = false,
								Name = reader.GetString(0),
								ParameterType = typeof(object),
								ProviderType = reader.GetString(1)
							};
							parametersCollection.Add(parameter);
						}
					}
				}
			}
		}
	}
}
