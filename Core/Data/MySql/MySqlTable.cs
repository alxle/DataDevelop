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
			get { return database.Connection; }
		}

		public override bool IsView
		{
			get { return isView; }
		}

		public void SetView(bool value)
		{
			isView = value;
		}

		public override bool Rename(string newName)
		{
			using (var alter = Connection.CreateCommand()) {
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
			using (var drop = Connection.CreateCommand()) {
				drop.CommandText = "DROP TABLE " + QuotedName;
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
			var data = new DataTable(Name);

			var text = new StringBuilder();
			text.Append("SELECT ");
			filter.WriteColumnsProjection(text);
			text.Append(" FROM ");
			text.Append(QuotedName);

			if (filter.IsRowFiltered) {
				text.Append(" WHERE " );
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
						adapter.Fill(data);
					}
				}
			}
			return data;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (Database.CreateConnectionScope()) {
				var columns = Connection.GetSchema("Columns", new [] { null, Connection.Database, Name });
				foreach (DataRow row in columns.Rows) {
					var column = new Column(this);
					column.Name = row["COLUMN_NAME"].ToString();
					if (!IsReadOnly) {
						column.InPrimaryKey = row["COLUMN_KEY"].ToString() == "PRI";
					}
					column.ProviderType = row["COLUMN_TYPE"].ToString();
					columnsCollection.Add(column);
				}
				SetColumnTypes(columnsCollection);
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
