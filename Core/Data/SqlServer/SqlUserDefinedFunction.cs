using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DataDevelop.Data.SqlServer
{
	sealed class SqlUserDefinedFunction : UserDefinedFunction
	{
		private SqlDatabase database;
		private string schema;

		public SqlUserDefinedFunction(SqlDatabase database)
			: base(database)
		{
			this.database = database;
		}

		public string Schema
		{
			get { return this.schema ?? String.Empty; }
			internal set { this.schema = value; }
		}

		public override string GenerateAlterStatement()
		{
			var create = this.GenerateCreateStatement();
			if (!String.IsNullOrEmpty(create)) {
				return Regex.Replace(create, @"^\s*CREATE\s+", "ALTER ");
			}
			return null;
		}

		public override string GenerateCreateStatement()
		{
			using (this.database.CreateConnectionScope()) {
				using (var select = this.database.Connection.CreateCommand()) {
					select.CommandText = @"SELECT ISNULL(smsp.definition, ssmsp.definition) AS [Definition]"
						+ " FROM sys.all_objects AS sp"
						+ " LEFT OUTER JOIN sys.sql_modules AS smsp ON smsp.object_id = sp.object_id"
						+ " LEFT OUTER JOIN sys.system_sql_modules AS ssmsp ON ssmsp.object_id = sp.object_id"
						+ " WHERE (sp.type in ('FN', 'IF', 'TF')) "
						+ "   AND (sp.name = @Name and SCHEMA_NAME(sp.schema_id) = @Schema)";
					select.Parameters.AddWithValue("@Name", this.Name);
					select.Parameters.AddWithValue("@Schema", this.Schema);
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
			return "DROP FUNCTION [" + this.Schema + "].[" + this.Name + "]";
		}

		public override string GenerateExecuteStatement()
		{
			var statement = new StringBuilder();
			statement.Append("SELECT [");
			statement.Append(this.Schema);
			statement.Append("].[");
			statement.Append(this.Name);
			statement.Append("](");
			bool first = true;
			foreach (var p in this.Parameters) {
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
			using (this.database.CreateConnectionScope()) {
				using (var select = (SqlCommand)this.database.CreateCommand()) {
					select.CommandText =
						"SELECT p.Name, type_name(p.system_type_id) as Type " +
						"FROM sys.parameters p " +
						"INNER JOIN sys.objects o ON p.object_id = o.object_id " +
						"WHERE o.name = @Name AND schema_name(o.schema_id) = @Schema AND p.parameter_id > 0" +
						"ORDER BY p.parameter_id";
					select.Parameters.AddWithValue("@Name", this.Name);
					select.Parameters.AddWithValue("@Schema", this.Schema);
					using (var reader = select.ExecuteReader()) {
						while (reader.Read()) {
							var parameter = new Parameter();
							parameter.IsOutput = false;
							parameter.Name = reader.GetString(0);
							parameter.ParameterType = typeof(object);
							parameter.ProviderType = reader.GetString(1);
							parametersCollection.Add(parameter);
						}
					}
				}
			}
		}
	}
}
