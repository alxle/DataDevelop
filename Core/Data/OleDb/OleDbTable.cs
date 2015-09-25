using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace DataDevelop.Data.OleDb
{
	internal class OleDbTable : Table
	{
		private string schema;
		private bool isView;
		private bool isReadOnly;

		public OleDbTable(OleDbDatabase database)
			: base(database)
		{
		}

		public override string DisplayName
		{
			get
			{
				if (String.IsNullOrEmpty(this.Schema)) {
					return this.Name;
				}
				return String.Format("{0}.{1}", this.Schema, base.Name);
			}
		}

		public override string QuotedName
		{
			get
			{
				if (String.IsNullOrEmpty(this.Schema)) {
					return String.Format("[{0}]", base.Name);
				}
				return String.Format("[{0}].[{1}]", this.Schema, base.Name);
			}
		}

		public string Schema
		{
			get { return this.schema; }
			internal set { this.schema = value; }
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
			get { return ((OleDbDatabase)Database).Connection; }
		}

		public string[] GetPrimaryKey()
		{
			using (this.Database.CreateConnectionScope()) {
				List<string> primaryKey = new List<string>();
				DataTable schema = this.Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, null);
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
			OleDbCommand command = this.Connection.CreateCommand();
			StringBuilder text = new StringBuilder();
			text.Append("SELECT COUNT(*) FROM ");
			text.Append(this.QuotedName);

			if (filter != null && filter.IsRowFiltered) {
				text.Append(" WHERE ");
				filter.WriteWhereStatement(text);
			}

			command.CommandText = text.ToString();
			try {
				this.Database.Connect();
				count = Convert.ToInt32(command.ExecuteScalar());
			} finally {
				this.Database.Disconnect();
			}
			return count;
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			return GetDataSecuencial(startIndex, count, filter, sort);
		}

		private DataTable GetDataSecuencial(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			StringBuilder text = new StringBuilder();
			text.Append("SELECT ");

			if (!this.IsView && !this.HasPrimaryKey) {
				// TODO
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

			var data = new DataTable(this.Name);
			var select = this.Connection.CreateCommand();
			select.CommandText = text.ToString();

			try {
				Database.Connect();
				using (var reader = select.ExecuteReader(CommandBehavior.SequentialAccess)) {
					for (int i = 0; i < reader.FieldCount; i++) {
						data.Columns.Add(new DataColumn(reader.GetName(i), reader.GetFieldType(i)));
					}

					while (startIndex > 0 && reader.Read()) {
						startIndex--;
					}

					if (reader.Read()) {
						do {
							DataRow row = data.NewRow();
							for (int i = 0; i < reader.FieldCount; i++) {
								row[i] = reader[i];
							}
							data.Rows.Add(row);
							count--;
						} while (reader.Read() && count > 0);
					}
					reader.Close();
				}
			} finally {
				Database.Disconnect();
			}
			data.AcceptChanges();
			return data;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				DataTable columns = this.Connection.GetSchema("Columns", new string[] { null, null, this.Name, null });

				// Fix because Column are sorted by Name rather than Ordinal Position
				DataRow[] rows = new DataRow[columns.Rows.Count];
				foreach (DataRow row in columns.Rows) {
					int i = Convert.ToInt32(row["ORDINAL_POSITION"]) - 1;
					rows[i] = row;
				} // End of Fix

				string[] primaryKey = this.GetPrimaryKey();
				foreach (DataRow row in rows) {
					Column column = new Column(this);
					column.Name = row["COLUMN_NAME"].ToString();
					if (InPrimaryKey(primaryKey, column.Name)) {
						column.InPrimaryKey = true;
					}
					column.ProviderType = ((OleDbType)row["DATA_TYPE"]).ToString();
					columnsCollection.Add(column);
				}
				this.SetColumnTypes();
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
			foreach (string column in primaryKey) {
				if (column == columnName) {
					return true;
				}
			}
			return false;
		}
	}
}
