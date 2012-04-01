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
			using (NpgsqlCommand alter = this.Connection.CreateCommand()) {
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
			using (NpgsqlCommand drop = this.Connection.CreateCommand()) {
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
			DataTable data = new DataTable(this.Name);

			StringBuilder text = new StringBuilder();
			text.Append("SELECT ");
			filter.WriteColumnsProjection(text);
			text.Append(" FROM ");
			text.Append(this.QuotedName);

			if (filter.IsRowFiltered) {
				text.Append(" WHERE " );
				filter.WriteWhereStatement(text);
			}
			if (sort != null && sort.IsSorted) {
				text.Append(" ORDER BY ");
				sort.WriteOrderBy(text);
			}
			text.AppendFormat(" LIMIT {0} OFFSET {1}", count, startIndex);
			
			NpgsqlCommand select = this.Connection.CreateCommand();
			select.CommandText = text.ToString();
			try {
				Database.Connect();
				using (NpgsqlDataAdapter adapter = (NpgsqlDataAdapter)this.Database.CreateAdapter(this, filter)) {
					adapter.SelectCommand = select;
					adapter.Fill(data);
				}
			} finally {
				Database.Disconnect();
			}
			return data;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (Database.CreateConnectionScope()) {
				
				List<string> primaryKeys = new List<string>();
				if (!IsReadOnly) {
					// TODO: Add Schema
					NpgsqlCommand command = (NpgsqlCommand)this.Database.CreateCommand();
					command.CommandText = "select column_name " +
										  "from information_schema.table_constraints t " +
										  "inner join information_schema.key_column_usage k " +
										  "           on k.constraint_name = t.constraint_name " +
										  "where t.table_catalog = :table_catalog " +
					////                      "      and table_schema = :table_schema " +
										  "      and t.table_name = :table_name " +
										  "      and t.constraint_type = 'PRIMARY KEY'";
					command.Parameters.AddWithValue(":table_catalog", this.Connection.Database);
					////command.Parameters.AddWithValue(":table_schema", null);
					command.Parameters.AddWithValue(":table_name", this.Name);

					using (NpgsqlDataReader reader = command.ExecuteReader()) {
						while (reader.Read()) {
							primaryKeys.Add(reader.GetString(0));
						}
					}
				}

				DataTable columns = this.Connection.GetSchema("Columns", new string[] { this.Connection.Database, null, this.Name });
				columns.DefaultView.Sort = "ordinal_position";
				foreach (DataRowView row in columns.DefaultView) {
					Column column = new Column(this);
					column.Name = row["COLUMN_NAME"].ToString();

					foreach (string key in primaryKeys) {
						if (key == column.Name) {
							column.InPrimaryKey = true;
							break;
						}
					}

					columnsCollection.Add(column);
				}
				this.SetColumnTypes();
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			using (Database.CreateConnectionScope()) {
				////DataTable triggers = this.Connection.GetSchema("Triggers", new string[] { null, this.Connection.Database, this.Name, null });
				////foreach (DataRow row in triggers.Rows) {
				////    PgSqlTrigger trigger = new PgSqlTrigger(this, row);
				////    ////trigger.Name = row["TRIGGER_NAME"].ToString();
				////    triggersCollection.Add(trigger);
				////}
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			////using (Database.CreateConnectionScope()) {
			////    ////DataTable schema = this.Connection.GetSchema();
			////    DataTable keys = this.Connection.GetSchema("Foreign Keys", new string[] { null, this.Connection.Database, this.Name, null });
			////    foreach (DataRow row in keys.Rows) {
			////        ForeignKey key = new ForeignKey();
			////        key.Name = row["CONSTRAINT_NAME"] as string;
			////        key.PrimaryTable = row["REFERENCED_TABLE_NAME"] as string;
			////        key.PrimaryTableColumns = row["REFERENCED_COLUMN_NAME"] as string;
			////        key.ChildTable = row["TABLE_NAME"] as string;
			////        key.ChildTableColumns = row["COLUMN_NAME"] as string;
			////        foreignKeysCollection.Add(key);
			////    }
			////}
		}

		public override string GenerateCreateStatement()
		{
			using (Database.CreateConnectionScope()) {
				DataTable data = Database.ExecuteTable("SHOW CREATE TABLE " + this.QuotedName);
				if (data.Rows.Count > 0 && data.Columns.Count >= 2) {
					return data.Rows[0][1] as string;
				}
				return "Error: Query returned 0 rows.";
			}
		}
	}
}
