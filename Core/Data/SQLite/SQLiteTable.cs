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
				IDbCommand command = Database.CreateCommand();
				command.CommandText = "SELECT * FROM " + this.QuotedName;
				using (IDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly)) {
					DataTable columns = this.Connection.GetSchema("Columns", new string[] { null, null, this.Name, null });
					foreach (DataRow row in columns.Rows) {
						Column column = new Column(this);
						column.Name = row["COLUMN_NAME"].ToString();
						if (!this.IsReadOnly) {
							column.InPrimaryKey = (bool)row["PRIMARY_KEY"];
							object type = row["DATA_TYPE"];
							column.ProviderType = (type == DBNull.Value) ? "OBJECT" : (string)type;
						}
						int ordinal = reader.GetOrdinal(column.Name);
						if (ordinal != -1) {
							column.Type = reader.GetFieldType(ordinal);
						}
						columnsCollection.Add(column);
					}
				}
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				SQLiteCommand command = this.Connection.CreateCommand();
				command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'trigger' AND tbl_name = @tbl_name";
				command.Parameters.AddWithValue("@tbl_name", this.Name);
				using (SQLiteDataReader reader = command.ExecuteReader()) {
					while (reader.Read()) {
						SQLiteTrigger trigger = new SQLiteTrigger(this);
						trigger.Name = reader.GetString(0);
						triggersCollection.Add(trigger);
					}
				}
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			////using (this.Database.CreateConnectionScope()) {
			////    SQLiteCommand command = this.Connection.CreateCommand();
			////    command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'index' AND tbl_name = @tbl_name";
			////    command.Parameters.AddWithValue("@tbl_name", this.Name);
			////    using (SQLiteDataReader reader = command.ExecuteReader()) {
			////        while (reader.Read()) {
			////            Key key = new Key(this);
			////            key.Name = reader.GetString(0);
			////            foreignKeysCollection.Add(key);
			////        }
			////    }
			////}
		}

		public override bool Rename(string newName)
		{
			if (this.isView) {
				return false;
			}
			newName = newName.Replace("]", "]]");
			using (IDbCommand rename = Database.CreateCommand()) {
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
			StringBuilder select = new StringBuilder();
			select.Append("SELECT ");
			
			if (!this.IsView && !this.HasPrimaryKey) {
				select.Append("RowId, ");
			}
			
			filter.WriteColumnsProjection(select);
			select.Append(" FROM ");
			select.Append(this.QuotedName);
			return select.ToString();
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{		
			StringBuilder text = new StringBuilder();
			text.Append("SELECT ");

			if (!this.IsView && !this.HasPrimaryKey) {
				text.Append("RowId, ");
			}

			filter.WriteColumnsProjection(text);

			text.Append(" FROM ");
			text.Append(this.QuotedName);

			if (filter.IsRowFiltered) {
				text.Append(" WHERE ");
				filter.WriteWhereStatement(text);
			}

			if (sort != null && sort.IsSorted) {
				text.Append(" ORDER BY ");
				sort.WriteOrderBy(text);
			}
			text.AppendFormat(" LIMIT {0}, {1}", startIndex, count);

			DataTable data = new DataTable(this.Name);
			SQLiteCommand select = this.Connection.CreateCommand();
			select.CommandText = text.ToString();

			using (this.Database.CreateConnectionScope()) {
				SQLiteDataAdapter adapter = (SQLiteDataAdapter)this.Database.CreateAdapter(this, filter);
				adapter.SelectCommand = select;
				adapter.Fill(data);
				////SQLiteDataReader reader = select.ExecuteReader();
				////try {
				////	data.Load(reader, LoadOption.OverwriteChanges);
				////} catch (ConstraintException) {
				////}
				////reader.Close();
			}
			return data;
		}

		public override string GenerateCreateStatement()
		{
			using (SQLiteCommand select = (SQLiteCommand)Database.CreateCommand()) {
				select.CommandText = "SELECT sql FROM sqlite_master WHERE type = @type AND name = @name";
				select.Parameters.AddWithValue("@type", this.isView ? "view" : "table");
				select.Parameters.AddWithValue("@name", this.Name);
				return select.ExecuteScalar() as string;
			}
		}

		public override string GenerateAlterStatement()
		{
			StringBuilder statement = new StringBuilder();
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
				statement.AppendLine("[tmp_AlterTable];");
				statement.Append(this.GenerateCreateStatement());
				statement.AppendLine(";");
				statement.Append("INSERT INTO ");
				statement.Append(this.QuotedName);
				statement.AppendLine(" SELECT * FROM [tmp_AlterTable];");
				statement.AppendLine("DROP TABLE [tmp_AlterTable];");
			}
			if (Triggers.Count > 0) {
				statement.AppendLine("/* RESTORE TRIGGERS */");
				foreach (Trigger trigger in this.Triggers) {
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
