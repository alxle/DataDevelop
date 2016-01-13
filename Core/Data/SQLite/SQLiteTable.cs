using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;

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

		public override bool IsReadOnly
		{
			get { return this.isReadOnly; }
		}

		public override bool IsView
		{
			get { return this.isView; }
		}

		private SQLiteConnection Connection
		{
			get { return ((SQLiteDatabase)Database).Connection; }
		}

		public void SetView(bool value)
		{
			this.isView = this.isReadOnly = value;
		}

		public void SetReadOnly()
		{
			this.isReadOnly = true;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				var columns = this.Connection.GetSchema("Columns", new string[] { null, null, this.Name, null });
				foreach (DataRow row in columns.Rows) {
					var column = new Column(this);
					column.Name = row["COLUMN_NAME"].ToString();
					if (!this.IsReadOnly) {
						column.InPrimaryKey = (bool)row["PRIMARY_KEY"];
						object type = row["DATA_TYPE"];
						column.ProviderType = (type == DBNull.Value) ? "OBJECT" : (string)type;
					}

					columnsCollection.Add(column);
				}
				this.SetColumnTypes();
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				using (var command = this.Connection.CreateCommand()) {
					command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'trigger' AND tbl_name = @tbl_name";
					command.Parameters.AddWithValue("@tbl_name", this.Name);
					using (var reader = command.ExecuteReader()) {
						while (reader.Read()) {
							var trigger = new SQLiteTrigger(this);
							trigger.Name = reader.GetString(0);
							triggersCollection.Add(trigger);
						}
					}
				}
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			var ids = new Dictionary<int, ForeignKey>();
			using (this.Database.CreateConnectionScope()) {
				var keys = this.Connection.GetSchema("ForeignKeys", new string[] { null, null, this.Name, null });
				foreach (DataRow row in keys.Rows) {
					var name = row["CONSTRAINT_NAME"] as string;
					if (name != null) {
						var id = (int)row["FKEY_ID"];
						ForeignKey key;
						if (!ids.TryGetValue(id, out key)) {
							key = new ForeignKey(String.Format("FK_{0}_{1}", this.Name, id), this);
							key.PrimaryTable = row["FKEY_TO_TABLE"] as string;
							key.ChildTable = row["TABLE_NAME"] as string;
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

		public override bool Rename(string newName)
		{
			if (this.isView) {
				return false;
			}
			newName = newName.Replace("]", "]]");
			using (var rename = Database.CreateCommand()) {
				rename.CommandText = "ALTER TABLE " + this.QuotedName + " RENAME TO [" + newName + "]";
				try {
					rename.ExecuteNonQuery();
					this.Name = newName;
					return true;
				} catch (SQLiteException) {
					return false;
				}
			}
		}

		public override string GetBaseSelectCommandText(TableFilter filter)
		{
			var select = new StringBuilder();
			select.Append("SELECT ");
			
			if (!this.IsView && !this.HasPrimaryKey) {
				select.Append("RowId, ");
			}
			
			filter.WriteColumnsProjection(select);
			select.Append(" FROM ");
			select.Append(this.QuotedName);
			if (filter.IsRowFiltered) {
				select.Append(" WHERE ");
				filter.WriteWhereStatement(select);
			}
			return select.ToString();
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{		
			var sql = new StringBuilder();
			sql.Append("SELECT ");

			if (!this.IsView && !this.HasPrimaryKey) {
				sql.Append("RowId, ");
			}

			filter.WriteColumnsProjection(sql);

			sql.Append(" FROM ");
			sql.Append(this.QuotedName);

			if (filter.IsRowFiltered) {
				sql.Append(" WHERE ");
				filter.WriteWhereStatement(sql);
			}

			if (sort != null && sort.IsSorted) {
				sql.Append(" ORDER BY ");
				sort.WriteOrderBy(sql);
			}
			sql.AppendFormat(" LIMIT {0}, {1}", startIndex, count);

			var data = new DataTable(this.Name);
			using (var select = this.Connection.CreateCommand()) {
				select.CommandText = sql.ToString();
				using (this.Database.CreateConnectionScope()) {
					using (var adapter = (SQLiteDataAdapter)this.Database.CreateAdapter(this, filter)) {
						adapter.SelectCommand = select;
						adapter.Fill(data);
					}
				}
			}
			return data;
		}

		public override string GenerateCreateStatement()
		{
			using (var select = (SQLiteCommand)this.Database.CreateCommand()) {
				select.CommandText = "SELECT sql FROM sqlite_master WHERE type = @type AND name = @name";
				select.Parameters.AddWithValue("@type", this.isView ? "view" : "table");
				select.Parameters.AddWithValue("@name", this.Name);
				return select.ExecuteScalar() as string;
			}
		}

		public override string GenerateAlterStatement()
		{
			var statement = new StringBuilder();
			statement.AppendLine("BEGIN;");
			if (this.IsView) {
				statement.Append("DROP VIEW ");
				statement.Append(QuotedName);
				statement.AppendLine(";");
				statement.Append(this.GenerateCreateStatement());
				statement.AppendLine(";");
			} else {
				statement.Append("ALTER TABLE ");
				statement.Append(QuotedName);
				statement.Append(" RENAME TO ");
				statement.AppendLine("[" + this.Name + "_AlterTableTemp];");
				statement.Append(this.GenerateCreateStatement());
				statement.AppendLine(";");
				statement.AppendLine();
				statement.Append("INSERT INTO ");
				statement.AppendLine(this.QuotedName);
				statement.Append("      (");
				var filter = new TableFilter(this);
				filter.WriteColumnsProjection(statement);
				statement.AppendLine(")");
				statement.Append("SELECT ");
				filter.WriteColumnsProjection(statement);
				statement.AppendLine();
				statement.AppendLine("FROM [" + this.Name + "_AlterTableTemp];");
				statement.AppendLine();
				statement.AppendLine("DROP TABLE [" + this.Name + "_AlterTableTemp];");
			}
			if (Triggers.Count > 0) {
				statement.AppendLine("/* RESTORE TRIGGERS */");
				foreach (var trigger in this.Triggers) {
					statement.Append(trigger.GenerateCreateStatement());
					statement.AppendLine(";");
				}
			}
			statement.AppendLine("END;");
			return statement.ToString();
		}

		public override string GenerateDropStatement()
		{
			if (this.IsView) {
				return "DROP VIEW " + this.QuotedName;
			}
			return "DROP TABLE " + this.QuotedName;
		}
	}
}
