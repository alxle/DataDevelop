using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace DataDevelop.Data.SqlServer
{
	internal sealed class SqlStoredProcedure : StoredProcedure, ISqlObject
	{
		private SqlDatabase database;

		public SqlStoredProcedure(SqlDatabase database, string schemaName, string procedureName)
			: base(database)
		{
			this.database = database;
			SchemaName = schemaName;
			ProcedureName = procedureName;
			Name = $"{schemaName}.{procedureName}";
		}

		public string SchemaName { get; set; }

		public string ProcedureName { get; set; }

		public string ObjectName => ProcedureName;

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
						"SELECT ISNULL(smsp.definition, ssmsp.definition) AS [Definition] " +
						"FROM sys.all_objects AS sp " +
						"LEFT OUTER JOIN sys.sql_modules AS smsp ON smsp.object_id = sp.object_id " +
						"LEFT OUTER JOIN sys.system_sql_modules AS ssmsp ON ssmsp.object_id = sp.object_id " +
						"WHERE sp.type IN ('P', 'RF', 'PC') " +
						"  AND sp.name = @Name AND SCHEMA_NAME(sp.schema_id) = @Schema";
					select.Parameters.AddWithValue("@Name", ProcedureName);
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
			return "DROP PROCEDURE [" + SchemaName + "].[" + ProcedureName + "]";
		}

		public override string GenerateExecuteStatement()
		{
			var statement = new StringBuilder();
			statement.Append("EXECUTE [");
			statement.Append(SchemaName);
			statement.Append("].[");
			statement.Append(ProcedureName);
			statement.AppendLine("]");
			var first = true;
			foreach (var p in Parameters) {
				statement.Append('\t');
				if (first) {
					first = false;
				} else {
					statement.Append(',');
				}
				statement.AppendLine(p.Name);
			}
			return statement.ToString();
		}
		
		protected override void PopulateParameters(IList<Parameter> parametersCollection)
		{
			using (var parameters = database.Connection.GetSchema("ProcedureParameters", new[] { null, SchemaName, ProcedureName })) {
				parameters.DefaultView.Sort = "ORDINAL_POSITION";
				foreach (DataRowView row in parameters.DefaultView) {
					var p = new Parameter {
						IsOutput = ((string)row["PARAMETER_MODE"] != "IN"),
						Name = (string)row["PARAMETER_NAME"],
						ProviderType = (string)row["DATA_TYPE"]
					};
					parametersCollection.Add(p);
				}
			}
		}
	}
}
