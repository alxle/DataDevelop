using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DataDevelop.Data.Access
{
	internal class AccessTable : Table
	{
		private bool isView;
		private bool isReadOnly;

		public AccessTable(AccessDatabase database)
			: base(database)
		{
		}

		public override bool IsView
		{
			get { return this.isView; }
		}

		public override bool IsReadOnly
		{
			get { return this.isReadOnly; }
		}

		private OleDbConnection Connection
		{
			get { return ((AccessDatabase)Database).Connection; }
		}

		public string[] GetPrimaryKey()
		{
			using (this.Database.CreateConnectionScope()) {
				var primaryKey = new List<string>();
				var schema = this.Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, null);
				foreach (DataRow row in schema.Rows) {
					if ((string)row["TABLE_NAME"] == this.Name) {
						if ((bool)row["PRIMARY_KEY"]) {
							primaryKey.Add((string)row["COLUMN_NAME"]);
						}
					}
				}
				return primaryKey.ToArray();
			}
		}

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
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		public override bool Delete()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		public override int GetRowCount(TableFilter filter)
		{
			int count = -1;
			var sql = new StringBuilder();
			sql.Append("SELECT COUNT(*) FROM ");
			sql.Append(this.QuotedName);

			if (filter != null && filter.IsRowFiltered) {
				sql.Append(" WHERE ");
				filter.WriteWhereStatement(sql);
			}
			using (var command = this.Connection.CreateCommand()) {
				command.CommandText = sql.ToString();
				using (this.Database.CreateConnectionScope()) {
					count = Convert.ToInt32(command.ExecuteScalar());
				}
			}
			return count;
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var data = new DataTable(this.Name);
			using (this.Database.CreateConnectionScope()) {
				using (var adapter = (OleDbDataAdapter)this.Database.CreateAdapter(this, filter)) {
					adapter.SelectCommand.CommandText = this.GetSelectStatement(startIndex, count, filter, sort);
					adapter.Fill(data);
				}
			}
			return data;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				var columns = this.Connection.GetSchema("Columns", new string[] { null, null, this.Name, null });

				// Fix because Column are sorted by Name rather than Ordinal Position
				var rows = new DataRow[columns.Rows.Count];
				foreach (DataRow row in columns.Rows) {
					int i = Convert.ToInt32(row["ORDINAL_POSITION"]) - 1;
					rows[i] = row;
				} // End of Fix

				string[] primaryKey = this.GetPrimaryKey();
				foreach (DataRow row in rows) {
					var column = new Column(this);
					column.Name = row["COLUMN_NAME"].ToString();
					if (InPrimaryKey(primaryKey, column.Name)) {
						column.InPrimaryKey = true;
					}
					column.ProviderType = ((OleDbType)row["DATA_TYPE"]).ToString();
					columnsCollection.Add(column);
				}
				this.SetColumnTypes(columnsCollection);
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
		}

		private static bool InPrimaryKey(string[] primaryKey, string columnName)
		{
			foreach (var column in primaryKey) {
				if (column == columnName) {
					return true;
				}
			}
			return false;
		}

		private string GetSelectStatement(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			// SELECT TOP {count} * FROM 
			//  (SELECT TOP {?} * FROM {table} ORDER BY {pk} ASC)
			// ORDER BY {pk} DESC
			int total = this.GetRowCount(filter);
			StringBuilder select = new StringBuilder();
			if (total <= count) {
				select.Append("SELECT ");
				filter.WriteColumnsProjection(select);
				select.Append(" FROM ");
				select.Append(this.QuotedName);
				if (filter.IsRowFiltered) {
					select.Append(" WHERE (");
					filter.WriteWhereStatement(select);
					select.Append(")");
				}
				if (sort != null && sort.IsSorted) {
					select.Append(" ORDER BY ");
					sort.WriteOrderBy(select);
				}
			} else {
				select.Append("SELECT TOP ");
				select.Append(count);
				select.Append(" * FROM (SELECT TOP ");
				select.Append(total - startIndex);
				select.Append(" ");
				filter.WriteColumnsProjection(select);
				select.Append(" FROM ");
				select.Append(this.QuotedName);
				if (filter.IsRowFiltered) {
					select.Append(" WHERE (");
					filter.WriteWhereStatement(select);
					select.Append(")");
				}
				select.Append(" ORDER BY ");
				select.Append(Columns[0].QuotedName);
				select.Append(" DESC) ORDER BY ");
				select.Append(sort);
				select.Append(" ASC");
			}
			return select.ToString();
		}
	}
}
