using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Linq;

namespace DataDevelop.Data.SqlServer
{
	internal sealed class SqlTable : Table, ISqlObject
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

		public string TableName { get; private set; }

		public override string ObjectName => TableName;

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
			isView = value;
		}

		public void SetReadOnly(bool value)
		{
			isReadOnly = value;
		}

		public override void Rename(string newName)
		{
			using (Database.CreateConnectionScope())
			using (var command = Database.Connection.CreateCommand()) {
				command.CommandText = "sp_rename";
				command.CommandType = CommandType.StoredProcedure;
				command.Parameters.AddWithValue("@objname", QuotedName);
				command.Parameters.AddWithValue("@newname", newName);
				command.Parameters.AddWithValue("@objtype", "OBJECT");
				command.ExecuteNonQuery();
				TableName = newName;
				Name = $"{SchemaName}.{newName}";
			}
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var data = new DataTable(Name);
			using (Database.CreateConnectionScope()) {
				using (var adapter = (SqlDataAdapter)Database.CreateAdapter(this, filter)) {
					if (startIndex == 0 || count == 0) {
						adapter.SelectCommand.CommandText = GetSelectTopStatement(count, filter, sort);
					} else if (Convert.ToInt32(Connection.ServerVersion.Split('.')[0]) >= 11) {
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
			using (Database.CreateConnectionScope()) {
				var columns = Connection.GetSchema("Columns", new[] { null, SchemaName, TableName, null });

				// Fix because Column are sorted by Name rather than Ordinal Position
				var rows = new DataRow[columns.Rows.Count];
				foreach (DataRow row in columns.Rows) {
					var i = Convert.ToInt32(row["ORDINAL_POSITION"]) - 1;
					rows[i] = row;
				} // End of Fix

				var keys = GetPrimaryKeyColumns();
				foreach (var row in rows) {
					var name = (string)row["COLUMN_NAME"];
					var column = new Column(this) {
						Name = name,
						InPrimaryKey = keys.Contains(name),
						ProviderType = (string)row["DATA_TYPE"]
					};
					var maxLength = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
					if (!string.IsNullOrEmpty(maxLength)) {
						column.ProviderType = $"{column.ProviderType}({maxLength})";
					} else if (column.ProviderType.ToLower() == "numeric") {
						column.ProviderType = $"numeric({row["NUMERIC_PRECISION"]}, {row["NUMERIC_SCALE"]})";
					}
					columnsCollection.Add(column);
				}
				SetColumnTypes(columnsCollection);
			}
		}

		protected override void PopulateIndexes(IList<Index> indexesCollection)
		{
			using (Database.CreateConnectionScope()) {
				using (var select = Connection.CreateCommand()) {
					select.CommandText =
						"SELECT x.name AS IndexName, x.is_unique AS IsUnique, x.is_primary_key AS IsPrimaryKey, " +
						"       c.name AS ColumnName, xc.is_descending_key AS IsDescendingKey " +
						"FROM sys.objects o " +
						"INNER JOIN sys.indexes x ON x.object_id = o.object_id " +
						"INNER JOIN sys.index_columns xc ON xc.object_id = x.object_id AND xc.index_id = x.index_id " +
						"INNER JOIN sys.columns c ON c.object_id = x.object_id AND xc.column_id = c.column_id " +
						"WHERE o.name = @TableName AND o.name IS NOT NULL " +
						"      AND o.type = 'U' AND schema_name(o.schema_id) = @SchemaName " +
						"ORDER BY x.name, xc.key_ordinal";
					select.Parameters.AddWithValue("@SchemaName", SchemaName);
					select.Parameters.AddWithValue("@TableName", TableName);
					using (var reader = select.ExecuteReader()) {
						var indexes = new Dictionary<string, Index>(StringComparer.OrdinalIgnoreCase);
						while (reader.Read()) {
							var name = reader.GetString(0);
							if (!indexes.TryGetValue(name, out var index)) {
								index = new Index(this, name) {
									Name = name,
									IsUniqueKey = reader.GetBoolean(1),
									IsPrimaryKey = reader.GetBoolean(2),
								};
								indexes.Add(name, index);
								indexesCollection.Add(index);
							}
							var columnName = reader.GetString(3);
							var isDescendingKey = reader.GetBoolean(4);
							var column = Columns.Single(i => i.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
							var columnOrder = new ColumnOrder(column);
							if (isDescendingKey) {
								columnOrder.OrderType = OrderType.Descending;
							}
							index.Columns.Add(columnOrder);
						}
					}
				}
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			using (Database.CreateConnectionScope()) {
				using (var select = Connection.CreateCommand()) {
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
					select.Parameters.AddWithValue("@SchemaName", SchemaName);
					select.Parameters.AddWithValue("@TableName", TableName);
					using (var reader = select.ExecuteReader()) {
						ForeignKey key = null;
						while (reader.Read()) {
							var name = reader.GetString(0);
							if (key == null || key.Name != name) {
								key = new ForeignKey(name, this) {
									Name = name,
									PrimaryTable = reader.GetString(2) + "." + reader.GetString(1),
									ChildTable = reader.GetString(5) + "." + reader.GetString(4)
								};
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
			using (Database.CreateConnectionScope()) {
				using (var select = Connection.CreateCommand()) {
					select.CommandText =
						"select tr.name from sys.triggers tr " +
						"inner join sys.tables ta on tr.parent_id = ta.object_id " +
						"inner join sys.schemas s on ta.schema_id = s.schema_id " +
						"where ta.name = @TableName and s.name = @SchemaName ";
					select.Parameters.AddWithValue("@SchemaName", SchemaName);
					select.Parameters.AddWithValue("@TableName", TableName);
					using (var reader = select.ExecuteReader()) {
						while (reader.Read()) {
							var trigger = new SqlTrigger(this, reader.GetString(0));
							triggersCollection.Add(trigger);
						}
					}
				}
			}
		}

		private HashSet<string> GetPrimaryKeyColumns()
		{
			var primaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			using (Database.CreateConnectionScope()) {
				using (var select = Connection.CreateCommand()) {
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

		private void WriteDefaultOrderBy(StringBuilder sql)
		{
			var columns = new List<string>();
			foreach (var c in Columns) {
				if (c.InPrimaryKey) {
					columns.Add(c.QuotedName);
				}
			}
			if (columns.Count == 0) {
				columns.Add(Columns[0].QuotedName);
			}
			sql.Append(string.Join(", ", columns.ToArray()));
		}

		private string GetSelectRowNumberStatement(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var sql = new StringBuilder();
			sql.Append("WITH Ordered AS (SELECT ROW_NUMBER() OVER (ORDER BY ");

			if (sort != null && sort.IsSorted) {
				sort.WriteOrderBy(sql);
			} else {
				WriteDefaultOrderBy(sql);
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
				WriteDefaultOrderBy(sql);
			}

			sql.Append($" OFFSET {startIndex} ROWS FETCH NEXT {count} ROWS ONLY");

			return sql.ToString();
		}

		private string GetSelectTopStatement(int count, TableFilter filter, TableSort sort)
		{
			var sql = new StringBuilder();
			sql.Append($"SELECT TOP {count} ");
			filter.WriteColumnsProjection(sql);
			sql.Append($" FROM {QuotedName} ");

			if (count > 0) {
				if (filter != null && filter.IsRowFiltered) {
					sql.Append(" WHERE ");
					filter.WriteWhereStatement(sql);
				}

				sql.Append(" ORDER BY ");
				if (sort != null && sort.IsSorted) {
					sort.WriteOrderBy(sql);
				} else {
					WriteDefaultOrderBy(sql);
				}
			}
			return sql.ToString();
		}

		public override string GenerateCreateStatement()
		{
			if (IsView) {
				using (var select = Connection.CreateCommand()) {
					select.CommandText =
						"SELECT Definition FROM sys.sql_modules " +
						"WHERE object_id = OBJECT_ID(@name)";
					select.Parameters.AddWithValue("@name", QuotedName);
					return select.ExecuteScalar() as string;
				}
			} else {
				return null;
			}
		}
	}
}
