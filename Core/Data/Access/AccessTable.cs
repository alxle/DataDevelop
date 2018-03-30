using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DataDevelop.Data.Access
{
	internal sealed class AccessTable : Table
	{
		private bool isView;
		private bool isReadOnly;

		public AccessTable(AccessDatabase database)
			: base(database)
		{
		}

		public override bool IsView => isView;

		public override bool IsReadOnly => isReadOnly;

		private OleDbConnection Connection => ((AccessDatabase)Database).Connection;

		private string[] GetPrimaryKey()
		{
			using (Database.CreateConnectionScope()) {
				var schema = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new[] { null, null, Name });
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

		public override bool Rename(string newName)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		public override bool Delete()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		public override int GetRowCount(TableFilter filter)
		{
			var count = -1;
			var sql = new StringBuilder();
			sql.Append("SELECT COUNT(*) FROM ");
			sql.Append(QuotedName);

			if (filter != null && filter.IsRowFiltered) {
				sql.Append(" WHERE ");
				filter.WriteWhereStatement(sql);
			}
			using (var command = Connection.CreateCommand()) {
				command.CommandText = sql.ToString();
				using (Database.CreateConnectionScope()) {
					count = Convert.ToInt32(command.ExecuteScalar());
				}
			}
			return count;
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			using (Database.CreateConnectionScope()) {
				if (sort == null || !sort.IsSorted) {
					var primaryKey = Columns.Where(i => i.InPrimaryKey).Select(i => i.QuotedName).ToArray();
					if (primaryKey.Length > 0) {
						using (var adapter = Database.CreateAdapter(this, filter)) {
							adapter.SelectCommand.CommandText = GetPagingWithKeyStatement(startIndex, count, filter, primaryKey);
							var data = new DataTable(Name);
							adapter.Fill(data);
							return data;
						}
					}
				}
				return GetDataSecuencial(startIndex, count, filter, sort);
			}
		}

		private string GetPagingWithKeyStatement(int startIndex, int count, TableFilter filter, string[] primaryKeyColumns)
		{
			var total = GetRowCount(filter);
			var select = new StringBuilder();
			if (startIndex == 0 || count == 0 || total <= count) {
				select.AppendFormat("SELECT TOP {0} ", count);
				filter.WriteColumnsProjection(select);
				select.Append(" FROM ");
				select.Append(QuotedName);
				if (filter.IsRowFiltered) {
					select.Append(" WHERE (");
					filter.WriteWhereStatement(select);
					select.Append(")");
				}
			} else {
				// SELECT TOP {count} * FROM 
				//  (SELECT TOP {?} * FROM {table} ORDER BY {pk} ASC)
				// ORDER BY {pk} DESC
				select.Append("SELECT TOP ");
				select.Append(count);
				select.Append(" * FROM (SELECT TOP ");
				select.Append(Math.Max(total - startIndex, 0));
				select.Append(" ");
				filter.WriteColumnsProjection(select);
				select.Append(" FROM ");
				select.Append(QuotedName);
				if (filter.IsRowFiltered) {
					select.Append(" WHERE (");
					filter.WriteWhereStatement(select);
					select.Append(")");
				}
				select.Append(" ORDER BY ");
				select.Append(string.Join(", ", primaryKeyColumns.Select(i => i + " DESC").ToArray()));
				select.Append(")");
			}
			select.Append(" ORDER BY ");
			select.Append(string.Join(", ", primaryKeyColumns.Select(i => i + " ASC").ToArray()));
			return select.ToString();
		}

		private DataTable GetDataSecuencial(int startIndex, int count, TableFilter filter, TableSort sort)
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

			var data = new DataTable(Name);
			using (var select = Connection.CreateCommand()) {
				select.CommandText = sql.ToString();

				using (Database.CreateConnectionScope()) {
					using (var reader = select.ExecuteReader(CommandBehavior.SequentialAccess)) {
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
					}
				}
			}
			data.AcceptChanges();
			return data;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (Database.CreateConnectionScope()) {
				var columns = Connection.GetSchema("Columns", new[] { null, null, Name, null });
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
					column.ProviderType = ((OleDbType)row["DATA_TYPE"]).ToString();
					columnsCollection.Add(column);
				}
				SetColumnTypes(columnsCollection);
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
		}
	}
}
