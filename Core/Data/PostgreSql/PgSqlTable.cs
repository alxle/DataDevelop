using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Npgsql;

namespace DataDevelop.Data.PostgreSql
{
	internal class PgSqlTable : Table
	{
		private PgSqlDatabase database;
		private bool isView;

		public PgSqlTable(PgSqlDatabase database)
			: base(database)
		{
			this.database = database;
		}

		public NpgsqlConnection Connection
		{
			get { return this.database.Connection; }
		}

		public override bool IsView
		{
			get { return this.isView; }
		}

		public void SetView(bool value)
		{
			this.isView = value;
		}

		public override bool Rename(string newName)
		{
			using (var alter = this.Connection.CreateCommand()) {
				alter.CommandText = "RENAME TABLE \"" + Name + "\" TO \"" + newName + "\"";
				try {
					alter.ExecuteNonQuery();
					return true;
				} catch (NpgsqlException) {
					return false;
				}
			}
		}

		public override bool Delete()
		{
			using (var drop = this.Connection.CreateCommand()) {
				drop.CommandText = "DROP TABLE " + this.QuotedName;
				try {
					drop.ExecuteNonQuery();
					return true;
				} catch (NpgsqlException) {
					return false;
				}
			}
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			var data = new DataTable(this.Name);

			var sql = new StringBuilder();
			sql.Append("SELECT ");
			filter.WriteColumnsProjection(sql);
			sql.Append(" FROM ");
			sql.Append(this.QuotedName);

			if (filter.IsRowFiltered) {
				sql.Append(" WHERE " );
				filter.WriteWhereStatement(sql);
			}
			if (sort != null && sort.IsSorted) {
				sql.Append(" ORDER BY ");
				sort.WriteOrderBy(sql);
			}
			sql.AppendFormat(" LIMIT {0} OFFSET {1}", count, startIndex);

			using (var select = this.Connection.CreateCommand()) {
				select.CommandText = sql.ToString();
				using (this.Database.CreateConnectionScope()) {
					using (var adapter = (NpgsqlDataAdapter)this.Database.CreateAdapter(this, filter)) {
						adapter.SelectCommand = select;
						adapter.Fill(data);
					}
				}
			}
			return data;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				var primaryKeys = new List<string>();
				if (!IsReadOnly) {
					// TODO: Add Schema
					using (var command = (NpgsqlCommand)this.Database.CreateCommand()) {
						command.CommandText = "select column_name " +
											  "from information_schema.table_constraints t " +
											  "inner join information_schema.key_column_usage k " +
											  "           on k.constraint_name = t.constraint_name " +
											  "where t.table_catalog = :table_catalog " +
											  ////"      and table_schema = :table_schema " +
											  "      and t.table_name = :table_name " +
											  "      and t.constraint_type = 'PRIMARY KEY'";
						command.Parameters.AddWithValue(":table_catalog", this.Connection.Database);
						////command.Parameters.AddWithValue(":table_schema", null);
						command.Parameters.AddWithValue(":table_name", this.Name);

						using (var reader = command.ExecuteReader()) {
							while (reader.Read()) {
								primaryKeys.Add(reader.GetString(0));
							}
						}
					}
				}

				var columns = this.Connection.GetSchema("Columns", new string[] { this.Connection.Database, null, this.Name });
				columns.DefaultView.Sort = "ordinal_position";
				foreach (DataRowView row in columns.DefaultView) {
					var column = new Column(this);
					column.Name = row["COLUMN_NAME"].ToString();

					foreach (string key in primaryKeys) {
						if (key == column.Name) {
							column.InPrimaryKey = true;
							break;
						}
					}

					columnsCollection.Add(column);
				}
				this.SetColumnTypes(columnsCollection);
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			// TODO
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			// TODO
		}

		public override string GenerateCreateStatement()
		{
			using (this.Database.CreateConnectionScope()) {
				var data = Database.ExecuteTable("SHOW CREATE TABLE " + this.QuotedName);
				if (data.Rows.Count > 0 && data.Columns.Count >= 2) {
					return data.Rows[0][1] as string;
				}
				return "Error: Query returned 0 rows.";
			}
		}
	}
}
