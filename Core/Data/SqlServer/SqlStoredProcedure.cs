using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DataDevelop.Data.SqlServer
{
	internal sealed class SqlStoredProcedure : StoredProcedure
	{
		private SqlDatabase database;
		private string schema;

		public SqlStoredProcedure(SqlDatabase database)
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
			string create = this.GenerateCreateStatement();
			if (!String.IsNullOrEmpty(create)) {
				return Regex.Replace(create, @"^\s*CREATE\s+", "ALTER ");
			}
			return null;
		}

		public override string GenerateCreateStatement()
		{
			using (this.database.CreateConnectionScope()) {
				using (SqlCommand select = this.database.Connection.CreateCommand()) {
					select.CommandText = @"SELECT ISNULL(smsp.definition, ssmsp.definition) AS [Definition]"
						+ " FROM sys.all_objects AS sp"
						+ " LEFT OUTER JOIN sys.sql_modules AS smsp ON smsp.object_id = sp.object_id"
						+ " LEFT OUTER JOIN sys.system_sql_modules AS ssmsp ON ssmsp.object_id = sp.object_id"
						+ " WHERE (sp.type = N'P' OR sp.type = N'RF' OR sp.type='PC') "
						+ "    and(sp.name=@Name and SCHEMA_NAME(sp.schema_id)=@Schema)";
					select.Parameters.AddWithValue("@Name", this.Name);
					select.Parameters.AddWithValue("@Schema", this.Schema);
					object obj = select.ExecuteScalar();
					if (obj != null && obj != DBNull.Value) {
						return (string)obj;
					}
					return null;
				}
			}
		}

		public override string GenerateDropStatement()
		{
			return "DROP PROCEDURE [" + this.Schema + "].[" + this.Name + "]";
		}

		public override string GenerateExecuteStatement()
		{
			StringBuilder statement = new StringBuilder();
			statement.Append("EXECUTE [");
			statement.Append(this.Schema);
			statement.Append("].[");
			statement.Append(this.Name);
			statement.AppendLine("]");
			bool first = true;
			foreach (Parameter p in Parameters) {
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
			DataTable parameters = this.database.Connection.GetSchema("ProcedureParameters", new string[] { null, null, this.Name, null });
			DataView view = new DataView(parameters);
			view.Sort = "ORDINAL_POSITION";
			foreach (DataRow row in view.ToTable().Rows) {
				Parameter p = new Parameter();
				p.IsOutput = ((string)row["PARAMETER_MODE"] != "IN");
				p.Name = (string)row["PARAMETER_NAME"];
				p.ProviderType = (string)row["DATA_TYPE"];
				parametersCollection.Add(p);
			}
		}
	}
}
