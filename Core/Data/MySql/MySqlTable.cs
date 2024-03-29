﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace DataDevelop.Data.MySql
{
	internal class MySqlTable : Table
	{
		private MySqlDatabase database;
		private bool isView;

		public MySqlTable(MySqlDatabase database)
			: base(database)
		{
			this.database = database;
		}

		public MySqlConnection Connection => database.Connection;

		public override bool IsView => isView;

		public void SetView(bool value)
		{
			isView = value;
		}

		public override void Rename(string newName)
		{
			using (var alter = Connection.CreateCommand()) {
				alter.CommandText = "RENAME TABLE `" + Name + "` TO `" + newName + "`";
				alter.ExecuteNonQuery();
				Name = newName;
			}
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var text = new StringBuilder();
			text.Append("SELECT ");
			filter.WriteColumnsProjection(text);
			text.Append(" FROM ");
			text.Append(QuotedName);

			if (filter != null && filter.IsRowFiltered) {
				text.Append(" WHERE ");
				filter.WriteWhereStatement(text);
			}
			if (sort != null && sort.IsSorted) {
				text.Append(" ORDER BY ");
				sort.WriteOrderBy(text);
			}
			text.AppendFormat(" LIMIT {0}, {1}", startIndex, count);

			using (var select = Connection.CreateCommand()) {
				select.CommandText = text.ToString();
				using (Database.CreateConnectionScope()) {
					using (var adapter = (MySqlDataAdapter)Database.CreateAdapter(this, filter)) {
						adapter.SelectCommand = select;
						var data = new DataTable(Name);
						adapter.Fill(data);
						return data;
					}
				}
			}
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (Database.CreateConnectionScope()) {
				var columns = Connection.GetSchema("Columns", new[] { null, Connection.Database, Name });
				foreach (DataRow row in columns.Rows) {
					var column = new Column(this) {
						Name = (string)row["COLUMN_NAME"]
					};
					if (!IsReadOnly) {
						column.InPrimaryKey = (string)row["COLUMN_KEY"] == "PRI";
					}
					column.ProviderType = (string)row["COLUMN_TYPE"];
					column.IsNullable = (string)row["Is_NULLABLE"] == "YES";
					var maxLength = row["CHARACTER_MAXIMUM_LENGTH"];
					if (maxLength != null && maxLength != DBNull.Value) {
						column.Size = Convert.ToInt32(maxLength);
					}
					var numericPrecision = row["NUMERIC_PRECISION"];
					if (numericPrecision != null && numericPrecision != DBNull.Value) {
						column.Precision = Convert.ToInt32(numericPrecision);
					}
					var numericScale = row["NUMERIC_SCALE"];
					if (numericScale != null &&  numericScale != DBNull.Value) {
						column.Scale = Convert.ToInt32(numericScale);
					}
					columnsCollection.Add(column);
				}
				SetColumnTypes(columnsCollection);
			}
		}

		protected override void PopulateIndexes(IList<Index> indexesCollection)
		{
			using (Database.CreateConnectionScope()) {
				var indexes = Connection.GetSchema("Indexes", new[] { null, Connection.Database, Name });
				var indexesDictionary = new Dictionary<string, Index>();
				foreach (DataRow row in indexes.Rows) {
					var indexName = (string)row["INDEX_NAME"];
					var index = new Index(this, indexName) {
						IsPrimaryKey = (bool)row["PRIMARY"],
						IsUniqueKey = (bool)row["UNIQUE"]
					};
					indexesDictionary.Add(indexName, index);
					indexesCollection.Add(index);
				}
				var indexColumns = Connection.GetSchema("IndexColumns", new[] { null, Connection.Database, Name });
				indexColumns.DefaultView.Sort = "INDEX_NAME, ORDINAL_POSITION";
				foreach (DataRowView row in indexColumns.DefaultView) {
					var indexName = (string)row["INDEX_NAME"];
					if (indexesDictionary.TryGetValue(indexName, out var index)) {
						var columnName = (string)row["COLUMN_NAME"];
						var sortMode = (string)row["SORT_ORDER"];
						var column = Columns.Single(c => c.Name == columnName);
						var columnOrder = new ColumnOrder(column);
						index.Columns.Add(columnOrder);
					}
				}
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			using (Database.CreateConnectionScope()) {
				var triggers = Connection.GetSchema("Triggers", new[] { null, Connection.Database, Name });
				foreach (DataRow row in triggers.Rows) {
					var trigger = new MySqlTrigger(this, (string)row["TRIGGER_NAME"]);
					triggersCollection.Add(trigger);
				}
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			using (Database.CreateConnectionScope()) {
				var keys = Connection.GetSchema("Foreign Keys", new[] { null, Connection.Database, Name });
				foreach (DataRow row in keys.Rows) {
					var name = row["CONSTRAINT_NAME"] as string;
					var key = new ForeignKey(name, this) {
						PrimaryTable = row["REFERENCED_TABLE_NAME"] as string,
						ChildTable = row["TABLE_NAME"] as string
					};
					foreignKeysCollection.Add(key);
				}
			}
		}

		public override string GenerateCreateStatement()
		{
			using (Database.CreateConnectionScope()) {
				var data = Database.ExecuteTable("SHOW CREATE TABLE " + QuotedName);
				if (data.Rows.Count > 0 && data.Columns.Count >= 2) {
					return data.Rows[0][1] as string;
				}
				return "Error: Query returned 0 rows.";
			}
		}
	}
}
