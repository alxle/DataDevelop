﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace DataDevelop.Data.SQLite
{
	public sealed class SQLiteTable : Table
	{
		private bool isReadOnly;

		private bool isView;

		public SQLiteTable(SQLiteDatabase database)
			: base(database)
		{
		}

		public override bool IsReadOnly => isReadOnly;

		public override bool IsView => isView;

		private SQLiteConnection Connection => ((SQLiteDatabase)Database).Connection;

		public void SetView(bool value)
		{
			isView = isReadOnly = value;
		}

		public void SetReadOnly()
		{
			isReadOnly = true;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (Database.CreateConnectionScope()) {
				var columns = Connection.GetSchema("Columns", new[] { null, null, Name });
				foreach (DataRow row in columns.Rows) {
					var column = new Column(this) {
						Name = row["COLUMN_NAME"].ToString()
					};
					//if (!IsReadOnly) {
						column.InPrimaryKey = (bool)row["PRIMARY_KEY"];
						var type = row["DATA_TYPE"];
						column.ProviderType = (type == DBNull.Value) ? "OBJECT" : (string)type;
						column.IsNullable = (bool)row["IS_NULLABLE"];
					var maxLength = row["CHARACTER_MAXIMUM_LENGTH"];
					column.Size = (int)maxLength;
					//}

					columnsCollection.Add(column);
				}
				SetColumnTypes(columnsCollection);
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			using (Database.CreateConnectionScope()) {
				using (var command = Connection.CreateCommand()) {
					command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'trigger' AND tbl_name = @tbl_name";
					command.Parameters.AddWithValue("@tbl_name", Name);
					using (var reader = command.ExecuteReader()) {
						while (reader.Read()) {
							var trigger = new SQLiteTrigger(this) {
								Name = reader.GetString(0)
							};
							triggersCollection.Add(trigger);
						}
					}
				}
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			var ids = new Dictionary<int, ForeignKey>();
			using (Database.CreateConnectionScope()) {
				var keys = Connection.GetSchema("ForeignKeys", new[] { null, null, Name });
				foreach (DataRow row in keys.Rows) {
					if (row["CONSTRAINT_NAME"] is string name) {
						var id = (int)row["FKEY_ID"];
						if (!ids.TryGetValue(id, out var key)) {
							key = new ForeignKey(name, this) {
								PrimaryTable = row["FKEY_TO_TABLE"] as string,
								ChildTable = row["TABLE_NAME"] as string
							};
							foreignKeysCollection.Add(key);
							ids.Add(id, key);
						}
						var fromColumn = row["FKEY_FROM_COLUMN"] as string;
						var toColumn = row["FKEY_TO_COLUMN"] as string;
						key.Columns.Add(new ColumnsPair(toColumn, fromColumn));
					}
				}
			}
		}

		protected override void PopulateIndexes(IList<Index> indexesCollection)
		{
			using (Database.CreateConnectionScope()) {
				var indexes = Connection.GetSchema("Indexes", new[] { null, null, Name });
				var indexesDictionary = new Dictionary<string, Index>();
				foreach (DataRow row in indexes.Rows) {
					var index = new Index(this, (string)row["INDEX_NAME"]) {
						IsPrimaryKey = (bool)row["PRIMARY_KEY"],
						IsUniqueKey = (bool)row["UNIQUE"]
					};
					indexesCollection.Add(index);
					indexesDictionary.Add(index.Name, index);
				}
				var indexColumns = Connection.GetSchema("IndexColumns", new[] { null, null, Name });
				indexColumns.DefaultView.Sort = "INDEX_NAME, ORDINAL_POSITION";
				foreach (DataRowView row in indexColumns.DefaultView) {
					var indexName = (string)row["INDEX_NAME"];
					if (indexesDictionary.TryGetValue(indexName, out var index)) {
						var columnName = (string)row["COLUMN_NAME"];
						var sortMode = (string)row["SORT_MODE"];
						var column = Columns.Single(c => c.Name == columnName);
						var columnOrder = new ColumnOrder(column) { OrderType = (sortMode == "ASC") ? OrderType.Ascending : OrderType.Descending };
						index.Columns.Add(columnOrder);
					}
				}
			}
		}

		public override void Rename(string newName)
		{
			if (isView) {
				throw new NotSupportedException("Renaming Views is not supported in SQLite.");
			}
			var quotedNewName = "[" + newName.Replace("]", "]]") + "]";
			using (var rename = Database.CreateCommand()) {
				rename.CommandText = "ALTER TABLE " + QuotedName + " RENAME TO " + quotedNewName;
				rename.ExecuteNonQuery();
				Name = newName;
			}
		}

		public override string GetBaseSelectCommandText(TableFilter filter, bool excludeWhere)
		{
			var select = new StringBuilder();
			select.Append("SELECT ");

			if (!IsView && !HasPrimaryKey) {
				select.Append("RowId, ");
			}

			filter.WriteColumnsProjection(select);
			select.Append(" FROM ");
			select.Append(QuotedName);
			if (filter != null && filter.IsRowFiltered && !excludeWhere) {
				select.Append(" WHERE ");
				filter.WriteWhereStatement(select);
			}
			return select.ToString();
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var sql = new StringBuilder();
			sql.Append("SELECT ");

			if (!IsView && !HasPrimaryKey) {
				sql.Append("RowId, ");
			}

			if (filter == null) {
				sql.Append('*');
			} else {
				filter.WriteColumnsProjection(sql);
			}

			sql.Append(" FROM ");
			sql.Append(QuotedName);

			if (filter != null && filter.IsRowFiltered) {
				sql.Append(" WHERE ");
				filter.WriteWhereStatement(sql);
			}

			if (sort != null && sort.IsSorted) {
				sql.Append(" ORDER BY ");
				sort.WriteOrderBy(sql);
			}
			sql.AppendFormat(" LIMIT {0}, {1}", startIndex, count);

			var data = new DataTable(Name);
			using (var select = Connection.CreateCommand()) {
				select.CommandText = sql.ToString();
				using (Database.CreateConnectionScope()) {
					using (var adapter = (SQLiteDataAdapter)Database.CreateAdapter(this, filter)) {
						adapter.SelectCommand = select;
						adapter.Fill(data);
					}
				}
			}
			return data;
		}

		public override string GenerateCreateStatement()
		{
			using (var select = (SQLiteCommand)Database.CreateCommand()) {
				select.CommandText = "SELECT sql FROM sqlite_master WHERE type = @type AND name = @name";
				select.Parameters.AddWithValue("@type", isView ? "view" : "table");
				select.Parameters.AddWithValue("@name", Name);
				return select.ExecuteScalar() as string;
			}
		}

		public override string GenerateAlterStatement()
		{
			var statement = new StringBuilder();
			statement.AppendLine("BEGIN;");
			if (IsView) {
				statement.Append("DROP VIEW ");
				statement.Append(QuotedName);
				statement.AppendLine(";");
				statement.Append(GenerateCreateStatement());
				statement.AppendLine(";");
			} else {
				// https://www.sqlite.org/lang_altertable.html#altertabrename
				statement.AppendLine("PRAGMA legacy_alter_table=ON;");
				statement.Append("ALTER TABLE ");
				statement.Append(QuotedName);
				statement.Append(" RENAME TO ");
				statement.AppendLine("[" + Name + "_AlterTableTemp];");
				statement.Append(GenerateCreateStatement());
				statement.AppendLine(";");
				statement.AppendLine();
				statement.Append("INSERT INTO ");
				statement.AppendLine(QuotedName);
				statement.Append("      (");
				var filter = new TableFilter(this);
				filter.WriteColumnsProjection(statement);
				statement.AppendLine(")");
				statement.Append("SELECT ");
				filter.WriteColumnsProjection(statement);
				statement.AppendLine();
				statement.AppendLine("FROM [" + Name + "_AlterTableTemp];");
				statement.AppendLine();
				statement.AppendLine("DROP TABLE [" + Name + "_AlterTableTemp];");
			}
			if (Triggers.Count > 0) {
				statement.AppendLine("/* RESTORE TRIGGERS */");
				foreach (var trigger in Triggers) {
					statement.Append(trigger.GenerateCreateStatement());
					statement.AppendLine(";");
				}
			}
			statement.AppendLine("PRAGMA legacy_alter_table=OFF;");
			statement.AppendLine("END;");
			return statement.ToString();
		}

		public override string GenerateDropStatement()
		{
			if (IsView) {
				return "DROP VIEW " + QuotedName;
			}
			return "DROP TABLE " + QuotedName;
		}

		public long GetLastInsertRowId()
		{
			return Connection.LastInsertRowId;
		}
	}
}
