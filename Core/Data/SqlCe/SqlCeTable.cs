using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Linq;

namespace DataDevelop.Data.SqlCe
{
	internal sealed class SqlCeTable : Table
	{
		private SqlCeDatabase database;
		private bool isReadOnly;

		public SqlCeTable(SqlCeDatabase database, string name)
			: base(database)
		{
			this.database = database;
			Name = name;
		}

		public override string QuotedName => $"[{Name}]";

		public override bool IsView => false;

		public override bool IsReadOnly => isReadOnly;

		public new SqlCeDatabase Database => (SqlCeDatabase)base.Database;

		private SqlCeConnection Connection => Database.Connection;

		internal void SetReadOnly(bool value)
		{
			isReadOnly = value;
		}

		public override bool Rename(string newName)
		{
			var success = true;
			using (database.CreateConnectionScope()) {
				using (var command = database.Connection.CreateCommand()) {
					var nameUnquoted = Name.Replace("'", "''");
					var newNameUnquoted = newName.Replace("'", "''");
					command.CommandText = $"sp_rename '{nameUnquoted}', '{newNameUnquoted}'";
					try {
						command.ExecuteNonQuery();
					} catch (SqlCeException) {
						success = false;
					}
				}
			}
			if (success) {
				Name = newName;
			}
			return success;
		}

		public override bool Delete()
		{
			var success = true;
			using (database.CreateConnectionScope()) {
				using (var command = database.Connection.CreateCommand()) {
					command.CommandText = $"DROP TABLE {QuotedName}";
					try {
						command.ExecuteNonQuery();
					} catch (SqlCeException) {
						success = false;
					}
				}
			}
			return success;
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var data = new DataTable(Name);
			using (database.CreateConnectionScope()) {
				using (var adapter = database.CreateAdapter(this, filter)) {
					adapter.SelectCommand.CommandText = GetSelectStatement(startIndex, count, filter, sort);
					adapter.Fill(data);
				}
			}
			return data;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (database.CreateConnectionScope()) {
				var columns = Connection.GetSchema("Columns", new[] { null, null, Name });

				var rows = new DataRow[columns.Rows.Count];
				foreach (DataRow row in columns.Rows) {
					var i = Convert.ToInt32(row["ORDINAL_POSITION"]) - 1;
					rows[i] = row;
				}

				var keys = GetPrimaryKeyColumns();
				foreach (var row in rows) {
					var column = new Column(this) {
						Name = row["COLUMN_NAME"].ToString()
					};

					if (keys.Contains(column.Name)) {
						column.InPrimaryKey = true;
					}
					column.ProviderType = (string)row["DATA_TYPE"];
					var maxLength = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
					if (!string.IsNullOrEmpty(maxLength)) {
						column.ProviderType = string.Format("{0}({1})", column.ProviderType, maxLength);
					} else if (column.ProviderType.ToLower() == "numeric") {
						column.ProviderType = string.Format("numeric({0}, {1})", row["NUMERIC_PRECISION"], row["NUMERIC_SCALE"]);
					}
					columnsCollection.Add(column);
				}
				SetColumnTypes(columnsCollection);
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			using (database.CreateConnectionScope()) {
				var dictionary = new Dictionary<string, ForeignKey>();
				using (var command = database.Connection.CreateCommand()) {
					command.CommandText =
						"SELECT c.CONSTRAINT_NAME, C.CONSTRAINT_TABLE_NAME, C.UNIQUE_CONSTRAINT_TABLE_NAME, C.UNIQUE_CONSTRAINT_NAME, " +
						"FKC.TABLE_NAME, FKC.COLUMN_NAME, RK.TABLE_NAME, RK.COLUMN_NAME " +
						"FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C " +
						"INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME " +
						"INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE FKC ON C.CONSTRAINT_NAME = FKC.CONSTRAINT_NAME " +
						"INNER JOIN INFORMATION_SCHEMA.INDEXES RK ON C.UNIQUE_CONSTRAINT_NAME = RK.INDEX_NAME " +
						"           AND C.UNIQUE_CONSTRAINT_TABLE_NAME = RK.TABLE_NAME AND RK.ORDINAL_POSITION = FKC.ORDINAL_POSITION " +
						"WHERE CONSTRAINT_TABLE_NAME = @TableName " +
						"ORDER BY C.CONSTRAINT_TABLE_NAME";
					command.Parameters.AddWithValue("@TableName", Name);
					using (var reader = command.ExecuteReader()) {
						while (reader.Read()) {
							var name = reader.GetString(0);
							ForeignKey key;
							if (dictionary.ContainsKey(name)) {
								key = dictionary[name];
							} else {
								key = new ForeignKey(name, this);
								dictionary.Add(name, key);
								foreignKeysCollection.Add(key);
							}
							key.PrimaryTable = reader.GetString(2);
							key.ChildTable = reader.GetString(1);
							key.Columns.Add(new ColumnsPair(reader.GetString(7), reader.GetString(5)));
						}
					}
				}
			}
		}

		protected override void PopulateIndexes(IList<Index> indexesCollection)
		{
			using (var command = database.Connection.CreateCommand()) {
				command.CommandText =
					"SELECT TABLE_NAME, INDEX_NAME, COLUMN_NAME, "+
					"   ORDINAL_POSITION, PRIMARY_KEY, [UNIQUE] " +
					"FROM INFORMATION_SCHEMA.INDEXES " +
					"WHERE TABLE_NAME = @TableName " +
					"ORDER BY INDEX_NAME, ORDINAL_POSITION";
				command.Parameters.AddWithValue("@TableName", Name);
				var indexes = new Dictionary<string, Index>(StringComparer.OrdinalIgnoreCase);
				using (var reader = command.ExecuteReader()) {
					while (reader.Read()) {
						var indexName = reader.GetString(1);
						var columnName = reader.GetString(2);
						var column = Columns.Single(i => i.Name == columnName);
						if (!indexes.TryGetValue(indexName, out var index)) {
							index = new Index(this, indexName) {
								IsPrimaryKey = reader.GetBoolean(4),
								IsUniqueKey = reader.GetBoolean(5)
							};
							indexes.Add(indexName, index);
							indexesCollection.Add(index);
						}
						index.Columns.Add(new ColumnOrder(column));
					}
				}
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			// Not supported
		}

		private HashSet<string> GetPrimaryKeyColumns()
		{
			var primaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			using (Database.CreateConnectionScope()) {
				using (var select = Connection.CreateCommand()) {
					select.CommandText =
						"SELECT u.COLUMN_NAME, c.CONSTRAINT_NAME, c.TABLE_NAME " +
						"FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS c " +
						"INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS u " +
						"  ON c.CONSTRAINT_NAME = u.CONSTRAINT_NAME AND u.TABLE_NAME = c.TABLE_NAME " +
						"WHERE c.CONSTRAINT_TYPE = 'PRIMARY KEY' " +
						"  AND u.TABLE_NAME = @TableName " +
						"ORDER BY c.CONSTRAINT_NAME";
					select.Parameters.AddWithValue("@TableName", Name);
					using (var reader = select.ExecuteReader()) {
						while (reader.Read()) {
							primaryKeys.Add(reader.GetString(0));
						}
					}
				}
				return primaryKeys;
			}
		}

		private string GetSelectStatement(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var sql = new StringBuilder();
			sql.Append("SELECT ");
			if (startIndex == 0 || count == 0) {
				sql.AppendFormat("TOP {0} ", count);
			}

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

			if (startIndex > 0 && count > 0) {
				sql.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", startIndex, count);
			}
			
			return sql.ToString();
		}
	}
}
