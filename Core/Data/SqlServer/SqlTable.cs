using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataDevelop.Data.SqlServer
{
	internal sealed class SqlTable : Table
	{
		private string schema;
		private bool isView;
		private bool isReadOnly;

		public SqlTable(SqlDatabase database)
			: base(database)
		{
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

		public new SqlDatabase Database
		{
			get { return (SqlDatabase)base.Database; }
		}

		private SqlConnection Connection
		{
			get { return ((SqlDatabase)this.Database).Connection; }
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
			bool success = true;
			using (this.Database.CreateConnectionScope()) {
				using (SqlCommand command = this.Database.Connection.CreateCommand()) {
					command.CommandText = "sp_rename";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@objname", this.Name);
					command.Parameters.AddWithValue("@newname", newName);
					////command.Parameters.AddWithValue("@objtype", "Table");
					try {
						command.ExecuteNonQuery();
					} catch (SqlException) {
						success = false;
					}
				}
			}
			if (success) {
				this.Name = newName;
			}
			return success;
		}

		public override bool Delete()
		{
			bool success = true;
			using (this.Database.CreateConnectionScope()) {
				using (SqlCommand command = this.Database.Connection.CreateCommand()) {
					command.CommandText = "DROP TABLE [" + this.Schema + "].[" + this.Name + "]";
					try {
						command.ExecuteNonQuery();
					} catch (SqlException) {
						success = false;
					}
				}
			}
			return success;
		}

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			DataTable data = new DataTable(this.Name);
			using (this.Database.CreateConnectionScope()) {
				SqlDataAdapter adapter = (SqlDataAdapter)this.Database.CreateAdapter(this, filter);
				adapter.SelectCommand.CommandText = this.GetSelectStatement(startIndex, count, filter, sort);
				adapter.Fill(data);
			}
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

				foreach (DataRow row in rows) {
					Column column = new Column(this);
					column.Name = row["COLUMN_NAME"].ToString();
					string[] keys = this.GetPrimaryKeyColumns();
					if (InPrimaryKey(keys, column.Name)) {
						column.InPrimaryKey = true;
					}
					column.ProviderType = (string)row["DATA_TYPE"];
					string maxLength = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
					if (!String.IsNullOrEmpty(maxLength)) {
						column.ProviderType = String.Format("{0}({1})", column.ProviderType, maxLength);
					} else if (column.ProviderType.ToLower() == "numeric") {
						column.ProviderType = String.Format("numeric({0}, {1})", row["NUMERIC_PRECISION"], row["NUMERIC_SCALE"]);
					}
					columnsCollection.Add(column);
				}
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				string[] restrictions = new string[] { null, null, this.Name, null };
				DataTable schema = this.Connection.GetSchema("ForeignKeys", restrictions);
				foreach (DataRow row in schema.Rows) {
					ForeignKey key = new ForeignKey();
					key.Name = (string)row["CONSTRAINT_NAME"];
					key.ChildTable = this.Name;
					////key.ChildTableColumns = (string)row["COLUMN_NAME"];
					foreignKeysCollection.Add(key);
				}
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			using (this.Database.CreateConnectionScope()) {
				using (SqlCommand select = this.Connection.CreateCommand()) {
					select.CommandText = "select tr.name from sys.triggers tr " +
						" inner join sys.tables ta on tr.parent_id = ta.object_id " +
						" where ta.name = @TableName";
					select.Parameters.AddWithValue("@TableName", this.Name);
					using (SqlDataReader reader = select.ExecuteReader()) {
						while (reader.Read()) {
							SqlTrigger trigger = new SqlTrigger(this);
							trigger.Name = reader.GetString(0);
							triggersCollection.Add(trigger);
						}
					}
				}
			}
		}

		private static bool InPrimaryKey(string[] primaryKeys, string columnName)
		{
			foreach (string key in primaryKeys) {
				if (String.Equals(key, columnName, StringComparison.InvariantCultureIgnoreCase)) {
					return true;
				}
			}
			return false;
		}

		private string[] GetPrimaryKeyColumns()
		{
			List<string> primaryKeys = new List<string>();
			using (this.Database.CreateConnectionScope()) {
				using (SqlCommand select = this.Connection.CreateCommand()) {
					select.CommandText = "select c.name from sys.key_constraints k"
						+ " inner join sys.tables t on k.parent_object_id = t.object_id"
						+ " inner join sys.indexes i on k.parent_object_id = i.object_id and k.unique_index_id = i.index_id"
						+ " inner join sys.index_columns ic on ic.object_id = i.object_id and ic.index_id = i.index_id"
						+ " inner join sys.columns c on c.object_id = ic.object_id and c.column_id = ic.column_id"
						+ " where t.name = @TableName and k.type = 'PK'";
					select.Parameters.AddWithValue("@TableName", this.Name);
					using (SqlDataReader reader = select.ExecuteReader()) {
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
			////int total = GetRowCount(filter);
			StringBuilder select = new StringBuilder();

			select.Append("WITH Ordered AS (SELECT ROW_NUMBER() OVER (ORDER BY ");
			
			if (sort != null && sort.IsSorted) {
				sort.WriteOrderBy(select);
			} else {
				// TODO: Project Primary Columns
				select.Append(Columns[0].QuotedName);
			}
			
			select.Append(") AS [Row_Number()], * ");
			////filter.WriteColumnsProjection(select);
			select.Append(" FROM [");
			select.Append(this.schema);
			select.Append("].[");
			select.Append(Name);
			select.Append(']');

			if (filter.IsRowFiltered) {
				select.Append(" WHERE (");
				filter.WriteWhereStatement(select);
				select.Append(")");
			}

			select.Append(") SELECT ");
			filter.WriteColumnsProjection(select);
			select.Append(" FROM Ordered WHERE (([Row_Number()] BETWEEN ");
			select.Append(startIndex + 1);
			select.Append(" AND ");
			select.Append(startIndex + count);
			select.Append(")");

			select.Append(")");
			return select.ToString();
		}
	}
}
