using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace DataDevelop.Data.SqlCe
{
	internal sealed class SqlCeTable : Table
	{
		private bool isView;
		private bool isReadOnly;

		public SqlCeTable(SqlCeDatabase database)
			: base(database)
		{
		}

		public string Schema { get; internal set; }

		public override bool IsView => isView;

		public override bool IsReadOnly => isReadOnly;

		public new SqlCeDatabase Database => (SqlCeDatabase)base.Database;

		private SqlCeConnection Connection => Database.Connection;

		public void SetView(bool value)
		{
			isView = value;
		}

		public void SetReadOnly(bool value)
		{
			isReadOnly = value;
		}

		public override bool Rename(string newName)
		{
			throw new NotImplementedException();
		}

		public override bool Delete()
		{
			var success = true;
			using (Database.CreateConnectionScope()) {
				using (var command = Database.Connection.CreateCommand()) {
					command.CommandText = "DROP TABLE [" + Schema + "].[" + Name + "]";
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
			using (Database.CreateConnectionScope()) {
				using (var adapter = (SqlCeDataAdapter)Database.CreateAdapter(this, filter)) {
					adapter.SelectCommand.CommandText = GetSelectStatement(startIndex, count, filter, sort);
					adapter.Fill(data);
				}
			}
			return data;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (Database.CreateConnectionScope()) {
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

					if (InPrimaryKey(keys, column.Name)) {
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
			using (Database.CreateConnectionScope()) {
				var restrictions = new[] { null, null, Name };
				var schema = Connection.GetSchema("ForeignKeys", restrictions);
				foreach (DataRow row in schema.Rows) {
					var name = (string)row["CONSTRAINT_NAME"];
					var key = new ForeignKey(name, this) {
						ChildTable = Name
					};
					foreignKeysCollection.Add(key);
				}
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			// TODO
		}

		private static bool InPrimaryKey(string[] primaryKeys, string columnName)
		{
			foreach (var key in primaryKeys) {
				if (string.Equals(key, columnName, StringComparison.InvariantCultureIgnoreCase)) {
					return true;
				}
			}
			return false;
		}

		private string[] GetPrimaryKeyColumns()
		{
			var primaryKeys = new List<string>();
			using (Database.CreateConnectionScope()) {
				using (var select = Connection.CreateCommand()) {
					select.CommandText =
						"SELECT u.COLUMN_NAME, c.CONSTRAINT_NAME, c.TABLE_NAME " +
						"FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS c INNER JOIN " +
						"INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS u ON c.CONSTRAINT_NAME = u.CONSTRAINT_NAME AND u.TABLE_NAME = c.TABLE_NAME " +
						"where c.CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY c.CONSTRAINT_NAME";
					select.Parameters.AddWithValue("@TableName", Name);
					using (var reader = select.ExecuteReader()) {
						while (reader.Read()) {
							primaryKeys.Add(reader.GetString(0));
						}
					}
				}
				return primaryKeys.ToArray();
			}
		}

		private string GetSelectStatement(int startIndex, int count, TableFilter filter, TableSort sort)
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
			sql.AppendFormat(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", startIndex, count);
			return sql.ToString();
		}
	}
}
