using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace DataDevelop.Data.SqlServer
{
	internal sealed class SqlTable : Table
	{
		private bool isView;
		private bool isReadOnly;

		public SqlTable(SqlDatabase database, string schemaName, string tableName)
			: base(database)
		{
			SchemaName = schemaName;
			TableName = tableName;
			Name = $"{schemaName}.{tableName}";
		}

		public override string DisplayName => Name;

		public override string QuotedName => $"[{SchemaName}].[{TableName}]";

		public string SchemaName { get; internal set; }

		public string TableName { get; internal set; }

		public override bool IsView => isView;

		public override bool IsReadOnly => isReadOnly;

		[ReadOnly(true)]
		public long TotalRows { get; set; }

		[ReadOnly(true)]
		public decimal TotalSizeKB { get; set; }

		[ReadOnly(true)]
		public decimal TotalUsedKB { get; set; }

		[ReadOnly(true)]
		public decimal TotalUnusedKB => TotalSizeKB - TotalUsedKB;

		public new SqlDatabase Database => (SqlDatabase)base.Database;

		private SqlConnection Connection => Database.Connection;

		public void SetView(bool value)
		{
			this.isView = value;
		}

		public void SetReadOnly(bool value)
		{
			this.isReadOnly = value;
		}

		public override bool Rename(string newName)
		{
			bool success = true;
			using (this.Database.CreateConnectionScope()) {
				using (var command = this.Database.Connection.CreateCommand()) {
					command.CommandText = "sp_rename";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@objname", this.QuotedName);
					command.Parameters.AddWithValue("@newname", newName);
					command.Parameters.AddWithValue("@objtype", "OBJECT");
					try {
						command.ExecuteNonQuery();
					} catch (SqlException) {
						success = false;
					}
				}
			}
			if (success) {
				TableName = newName;
				Name = $"{SchemaName}.{TableName}";
			}
			return success;
		}

		public override bool Delete()
		{
			bool success = true;
			using (this.Database.CreateConnectionScope()) {
				using (var command = this.Database.Connection.CreateCommand()) {
					command.CommandText = $"DROP TABLE {QuotedName}";
					try {
						command.ExecuteNonQuery();
					} catch (SqlException) {
						success = false;
					}
				}
			}
			return success;
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var data = new DataTable(this.Name);
			using (this.Database.CreateConnectionScope()) {
				using (var adapter = (SqlDataAdapter)this.Database.CreateAdapter(this, filter)) {
					if (Convert.ToInt32(Connection.ServerVersion.Split('.')[0]) >= 11) {
						adapter.SelectCommand.CommandText = GetSelectOffsetFetchStatement(startIndex, count, filter, sort);
					} else {
						adapter.SelectCommand.CommandText = GetSelectRowNumberStatement(startIndex, count, filter, sort);
					}
					adapter.Fill(data);
				}
			}
			return data;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				var columns = this.Connection.GetSchema("Columns", new string[] { null, this.SchemaName, this.TableName, null });

				// Fix because Column are sorted by Name rather than Ordinal Position
				var rows = new DataRow[columns.Rows.Count];
				foreach (DataRow row in columns.Rows) {
					int i = Convert.ToInt32(row["ORDINAL_POSITION"]) - 1;
					rows[i] = row;
				} // End of Fix

				var keys = this.GetPrimaryKeyColumns();
				foreach (DataRow row in rows) {
					var column = new Column(this);
					column.Name = row["COLUMN_NAME"].ToString();
					column.InPrimaryKey = keys.Contains(column.Name);
					column.ProviderType = (string)row["DATA_TYPE"];
					string maxLength = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
					if (!String.IsNullOrEmpty(maxLength)) {
						column.ProviderType = String.Format("{0}({1})", column.ProviderType, maxLength);
					} else if (column.ProviderType.ToLower() == "numeric") {
						column.ProviderType = String.Format("numeric({0}, {1})", row["NUMERIC_PRECISION"], row["NUMERIC_SCALE"]);
					}
					columnsCollection.Add(column);
				}
				this.SetColumnTypes(columnsCollection);
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				using (var select = this.Connection.CreateCommand()) {
					select.CommandText =
						"select object_name(fk.constraint_object_id) as ForeignKeyName, " +
						"       t1.name as ParentTable, " +
						"       s1.name as ParentSchema, " +
						"       c1.name as ParentColumn, " +
						"       t.name as ChildTable, " +
						"       s.name as ChildSchema, " +
						"       c.name AS ChildColumn " +
						"from sys.foreign_key_columns as fk " +
						"inner join sys.tables as t on fk.parent_object_id = t.object_id " +
						"inner join sys.schemas as s on t.schema_id = s.schema_id " +
						"inner join sys.columns as c on fk.parent_object_id = c.object_id and fk.parent_column_id = c.column_id " +
						"inner join sys.tables as t1 on fk.referenced_object_id = t1.object_id " +
						"inner join sys.schemas as s1 on t1.schema_id = s1.schema_id " +
						"inner join sys.columns as c1 on fk.referenced_object_id = c1.object_id and fk.referenced_column_id = c1.column_id " +
						"where t.name = @TableName and s.name = @SchemaName " +
						"order by fk.constraint_object_id, fk.constraint_column_id";
					select.Parameters.AddWithValue("@SchemaName", this.SchemaName);
					select.Parameters.AddWithValue("@TableName", this.TableName);
					using (var reader = select.ExecuteReader()) {
						ForeignKey key = null;
						while (reader.Read()) {
							string name = reader.GetString(0);
							if (key == null || key.Name != name) {
								key = new ForeignKey(name, this);
								key.Name = name;
								key.PrimaryTable = reader.GetString(2) + "." + reader.GetString(1);
								key.ChildTable = reader.GetString(5) + "." + reader.GetString(4);
								foreignKeysCollection.Add(key);
							}
							key.Columns.Add(new ColumnsPair(reader.GetString(3), reader.GetString(6)));
						}
					}
				}
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				using (var select = this.Connection.CreateCommand()) {
					select.CommandText =
						"select tr.name from sys.triggers tr " +
						"inner join sys.tables ta on tr.parent_id = ta.object_id " +
						"inner join sys.schemas s on ta.schema_id = s.schema_id " +
						"where ta.name = @TableName and s.name = @SchemaName ";
					select.Parameters.AddWithValue("@SchemaName", SchemaName);
					select.Parameters.AddWithValue("@TableName", TableName);
					using (var reader = select.ExecuteReader()) {
						while (reader.Read()) {
							var trigger = new SqlTrigger(this);
							trigger.Name = reader.GetString(0);
							triggersCollection.Add(trigger);
						}
					}
				}
			}
		}

		private HashSet<string> GetPrimaryKeyColumns()
		{
			var primaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			using (this.Database.CreateConnectionScope()) {
				using (var select = this.Connection.CreateCommand()) {
					select.CommandText =
						"select c.name from sys.key_constraints k " +
						"inner join sys.tables t on k.parent_object_id = t.object_id " +
						"inner join sys.schemas s on t.schema_id = s.schema_id " +
						"inner join sys.indexes i on k.parent_object_id = i.object_id and k.unique_index_id = i.index_id " +
						"inner join sys.index_columns ic on ic.object_id = i.object_id and ic.index_id = i.index_id " +
						"inner join sys.columns c on c.object_id = ic.object_id and c.column_id = ic.column_id " +
						"where t.name = @TableName and s.name = @SchemaName and k.type = 'PK' ";
					select.Parameters.AddWithValue("@SchemaName", SchemaName);
					select.Parameters.AddWithValue("@TableName", TableName);
					using (var reader = select.ExecuteReader()) {
						while (reader.Read()) {
							primaryKeys.Add(reader.GetString(0));
						}
					}
				}
				return primaryKeys;
			}
		}

		private string GetSelectRowNumberStatement(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var sql = new StringBuilder();
			sql.Append("WITH Ordered AS (SELECT ROW_NUMBER() OVER (ORDER BY ");

			if (sort != null && sort.IsSorted) {
				sort.WriteOrderBy(sql);
			} else {
				var columns = new List<string>();
				foreach (var c in this.Columns) {
					if (c.InPrimaryKey) {
						columns.Add(c.QuotedName);
					}
				}
				if (columns.Count == 0) {
					columns.Add(this.Columns[0].QuotedName);
				}
				sql.Append(string.Join(", ", columns.ToArray()));
			}

			sql.Append(") AS [Row_Number()], * ");
			sql.Append(" FROM ");
			sql.Append(QuotedName);

			if (filter.IsRowFiltered) {
				sql.Append(" WHERE (");
				filter.WriteWhereStatement(sql);
				sql.Append(")");
			}

			sql.Append(") SELECT ");
			filter.WriteColumnsProjection(sql);
			sql.Append(" FROM Ordered WHERE (([Row_Number()] BETWEEN ");
			sql.Append(startIndex + 1);
			sql.Append(" AND ");
			sql.Append(startIndex + count);
			sql.Append(")");

			sql.Append(")");
			return sql.ToString();
		}

		private string GetSelectOffsetFetchStatement(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var sql = new StringBuilder();
			sql.Append("SELECT ");
			filter.WriteColumnsProjection(sql);
			sql.Append(" FROM ");
			sql.Append(QuotedName);

			if (filter.IsRowFiltered) {
				sql.Append(" WHERE ");
				filter.WriteWhereStatement(sql);
			}

			sql.Append(" ORDER BY ");
			if (sort != null && sort.IsSorted) {
				sort.WriteOrderBy(sql);
			} else {
				var columns = new List<string>();
				foreach (var c in this.Columns) {
					if (c.InPrimaryKey) {
						columns.Add(c.QuotedName);
					}
				}
				if (columns.Count == 0) {
					columns.Add(this.Columns[0].QuotedName);
				}
				sql.Append(string.Join(", ", columns.ToArray()));
			}

			sql.Append($" OFFSET {startIndex} ROWS FETCH NEXT {count} ROWS ONLY");

			return sql.ToString();
		}

		public override string GenerateCreateStatement()
		{
			if (this.IsView) {
				using (var select = this.Connection.CreateCommand()) {
					select.CommandText =
						"SELECT Definition FROM sys.sql_modules " +
						"WHERE object_id = OBJECT_ID(@name)";
					select.Parameters.AddWithValue("@name", this.QuotedName);
					return select.ExecuteScalar() as string;
				}
			} else {
				return null;
			}
		}
	}
}
