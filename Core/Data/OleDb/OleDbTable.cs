﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DataDevelop.Data.OleDb
{
	internal class OleDbTable : Table
	{
		private bool isView;
		private bool isReadOnly;

		public OleDbTable(OleDbDatabase database)
			: base(database)
		{
		}

		public override string DisplayName => string.IsNullOrEmpty(Schema) ? Name : $"{Schema}.{Name}";

		public override string QuotedName => string.IsNullOrEmpty(Schema) ? $"[{Name}]" : $"[{Schema}].[{Name}]";

		public virtual string Schema { get; internal set; }

		public override bool IsView => isView;

		public override bool IsReadOnly => isReadOnly;

		protected OleDbConnection Connection => ((OleDbDatabase)Database).Connection;

		protected virtual string[] GetPrimaryKey()
		{
			using (Database.CreateConnectionScope()) {
				var schema = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new[] { null, Schema, Name });
				var primaryKey = new string[schema.Rows.Count];
				foreach (DataRowView row in schema.DefaultView) {
					var i = Convert.ToInt32(row["ORDINAL"]) - 1;
					primaryKey[i] = (string)row["COLUMN_NAME"];
				}
				return primaryKey;
			}
		}

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
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		public override int GetRowCount(TableFilter filter)
		{
			var count = -1;
			using (var command = Connection.CreateCommand()) {
				var sql = new StringBuilder();
				sql.Append("SELECT COUNT(*) FROM ");
				sql.Append(QuotedName);

				if (filter != null && filter.IsRowFiltered) {
					sql.Append(" WHERE ");
					filter.WriteWhereStatement(sql);
				}

				command.CommandText = sql.ToString();
				using (Database.CreateConnectionScope()) {
					count = Convert.ToInt32(command.ExecuteScalar());
				}
			}
			return count;
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			return GetDataSequential(startIndex, count, filter, sort);
		}

		protected virtual DataTable GetDataSequential(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var sql = new StringBuilder();
			sql.Append("SELECT ");
			filter.WriteColumnsProjection(sql);
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

			using (var select = Connection.CreateCommand()) {
				select.CommandText = sql.ToString();

				using (Database.CreateConnectionScope())
				using (var reader = select.ExecuteReader(CommandBehavior.SequentialAccess)) {
					var data = new DataTable(Name);
					data.BeginLoadData();
					for (var i = 0; i < reader.FieldCount; i++) {
						data.Columns.Add(new DataColumn(reader.GetName(i), reader.GetFieldType(i)));
					}

					while (startIndex > 0 && reader.Read()) {
						startIndex--;
					}

					if (reader.Read()) {
						do {
							var row = data.NewRow();
							for (var i = 0; i < reader.FieldCount; i++) {
								row[i] = reader[i];
							}
							data.Rows.Add(row);
							count--;
						} while (reader.Read() && count > 0);
					}
					data.EndLoadData();
					data.AcceptChanges();
					return data;
				}
			}
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (Database.CreateConnectionScope()) {
				var columns = Connection.GetSchema("Columns", new[] { null, Schema, Name, null });
				var rows = new DataRow[columns.Rows.Count];
				foreach (DataRow row in columns.Rows) {
					var i = Convert.ToInt32(row["ORDINAL_POSITION"]) - 1;
					rows[i] = row;
				}

				var primaryKey = GetPrimaryKey();
				foreach (var row in rows) {
					var column = new Column(this) {
						Name = row["COLUMN_NAME"].ToString()
					};
					if (primaryKey.Contains(column.Name, StringComparer.OrdinalIgnoreCase)) {
						column.InPrimaryKey = true;
					}
					var maxLength = row["CHARACTER_MAXIMUM_LENGTH"];
					if (maxLength != null && maxLength != DBNull.Value) {
						column.Size = Convert.ToInt32(maxLength);
					}
					var isNullable = row["IS_NULLABLE"];
					if (isNullable != null && isNullable != DBNull.Value) {
						column.IsNullable = Convert.ToBoolean(isNullable);
					}
					var precision = row["NUMERIC_PRECISION"];
					if (precision != null && precision != DBNull.Value) {
						column.Precision = Convert.ToInt32(precision);
					}
					var scale = row["NUMERIC_SCALE"];
					if (scale != null && scale != DBNull.Value) {
						column.Scale = Convert.ToInt32(scale);
					}
					column.ProviderType = ((OleDbType)row["DATA_TYPE"]).ToString();
					columnsCollection.Add(column);
				}
				SetColumnTypes(columnsCollection);
			}
		}

		protected override void PopulateIndexes(IList<Index> indexesCollection)
		{
			using (Database.CreateConnectionScope()) {
				var schema = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, new[] { null, Schema });
				schema.DefaultView.Sort = "INDEX_NAME, ORDINAL_POSITION";
				var indexes = new Dictionary<string, Index>();
				foreach (DataRowView row in schema.DefaultView) {
					var schemaName = row["TABLE_SCHEMA"] as string;
					var tableName = row["TABLE_NAME"] as string;
					if (tableName == Name && schemaName == Schema) {
						var indexName = (string)row["INDEX_NAME"];
						if (!indexes.TryGetValue(indexName, out var index)) {
							index = new Index(this, indexName) {
								IsPrimaryKey = (bool)row["PRIMARY_KEY"],
								IsUniqueKey = (bool)row["UNIQUE"]
							};
							indexes.Add(indexName, index);
							indexesCollection.Add(index);
						}
						var columnName = (string)row["COLUMN_NAME"];
						var column = Columns.Single(i => i.Name == columnName);
						index.Columns.Add(new ColumnOrder(column));
					}
				}
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			using (Database.CreateConnectionScope()) {
				var schema = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, new[] { null, null, null, null, Schema, Name });
				schema.DefaultView.Sort = "FK_NAME, ORDINAL";
				ForeignKey key = null;
				foreach (DataRowView row in schema.DefaultView) {
					var name = (string)row["FK_NAME"];
					if (key == null || key.Name != name) {
						key = new ForeignKey(name, this) {
							PrimaryTable = (string)row["PK_TABLE_NAME"],
							ChildTable = (string)row["FK_TABLE_NAME"],
						};
						foreignKeysCollection.Add(key);
					}
					key.Columns.Add(new ColumnsPair((string)row["PK_COLUMN_NAME"], (string)row["FK_COLUMN_NAME"]));
				}
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
		}
	}
}
