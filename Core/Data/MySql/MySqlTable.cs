using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
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

		public MySqlConnection Connection
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
			using (MySqlCommand alter = this.Connection.CreateCommand()) {
				alter.CommandText = "RENAME TABLE `" + Name + "` TO `" + newName + "`";
				try {
					alter.ExecuteNonQuery();
					return true;
				} catch (MySqlException) {
					return false;
				}
			}
		}

		public override bool Delete()
		{
			using (MySqlCommand drop = this.Connection.CreateCommand()) {
				drop.CommandText = "DROP TABLE " + this.QuotedName;
				try {
					drop.ExecuteNonQuery();
					return true;
				} catch (MySqlException) {
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
			text.AppendFormat(" LIMIT {0}, {1}", startIndex, count);
			
			MySqlCommand select = this.Connection.CreateCommand();
			select.CommandText = text.ToString();
			try {
				Database.Connect();
				using (MySqlDataAdapter adapter = (MySqlDataAdapter)this.Database.CreateAdapter(this, filter)) {
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
			using (this.Database.CreateConnectionScope()) {
				DataTable columns = this.Connection.GetSchema("Columns", new string[] { null, this.Connection.Database, this.Name, null });
				foreach (DataRow row in columns.Rows) {
					Column column = new Column(this);
					column.Name = row["COLUMN_NAME"].ToString();
					if (!IsReadOnly) {
						column.InPrimaryKey = row["COLUMN_KEY"].ToString() == "PRI";
					}
					column.ProviderType = row["COLUMN_TYPE"].ToString();
					columnsCollection.Add(column);
				}
				this.SetColumnTypes();
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			using (Database.CreateConnectionScope()) {
				DataTable triggers = this.Connection.GetSchema("Triggers", new string[] { null, this.Connection.Database, this.Name, null });
				foreach (DataRow row in triggers.Rows) {
					MySqlTrigger trigger = new MySqlTrigger(this, row);
					////trigger.Name = row["TRIGGER_NAME"].ToString();
					triggersCollection.Add(trigger);
				}
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			using (Database.CreateConnectionScope()) {
				////DataTable schema = this.Connection.GetSchema();
				DataTable keys = this.Connection.GetSchema("Foreign Keys", new string[] { null, this.Connection.Database, this.Name, null });
				foreach (DataRow row in keys.Rows) {
					ForeignKey key = new ForeignKey();
					key.Name = row["CONSTRAINT_NAME"] as string;
					key.PrimaryTable = row["REFERENCED_TABLE_NAME"] as string;
					key.PrimaryTableColumns = row["REFERENCED_COLUMN_NAME"] as string;
					key.ChildTable = row["TABLE_NAME"] as string;
					key.ChildTableColumns = row["COLUMN_NAME"] as string;
					foreignKeysCollection.Add(key);
				}
			}
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
