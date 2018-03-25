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
		private bool isReadOnly;

		public PgSqlTable(PgSqlDatabase database, string name)
			: base(database)
		{
			this.database = database;
			Name = name;
		}

		public PgSqlTable(PgSqlDatabase database, string name, bool isView, bool isReadOnly)
			: this(database, name)
		{
			this.isView = isView;
			this.isReadOnly = isReadOnly;
		}

		public NpgsqlConnection Connection => database.Connection;

		public override bool IsReadOnly => isReadOnly;

		public override bool IsView => isView;

		public void SetView(bool value)
		{
			isView = value;
		}

		public override bool Rename(string newName)
		{
			using (var alter = Connection.CreateCommand()) {
				alter.CommandText = $@"RENAME TABLE ""{Name}"" TO ""{newName}""";
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
			using (var drop = Connection.CreateCommand()) {
				drop.CommandText = "DROP TABLE " + QuotedName;
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
			var data = new DataTable(Name);

			var sql = new StringBuilder();
			sql.Append("SELECT ");
			filter.WriteColumnsProjection(sql);
			sql.Append(" FROM ");
			sql.Append(QuotedName);

			if (filter.IsRowFiltered) {
				sql.Append(" WHERE ");
				filter.WriteWhereStatement(sql);
			}
			if (sort != null && sort.IsSorted) {
				sql.Append(" ORDER BY ");
				sort.WriteOrderBy(sql);
			}
			sql.AppendFormat(" LIMIT {0} OFFSET {1}", count, startIndex);

			using (var select = Connection.CreateCommand()) {
				select.CommandText = sql.ToString();
				using (Database.CreateConnectionScope()) {
					using (var adapter = (NpgsqlDataAdapter)Database.CreateAdapter(this, filter)) {
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
				var primaryKeys = new HashSet<string>();
				if (!IsReadOnly) {
					using (var command = Connection.CreateCommand()) {
						command.CommandText =
							"select column_name " +
							"from information_schema.table_constraints t " +
							"inner join information_schema.key_column_usage k " +
							"           on k.constraint_name = t.constraint_name " +
							"where t.table_catalog = :table_catalog " +
							"      and t.table_schema = :table_schema " +
							"      and t.table_name = :table_name " +
							"      and t.constraint_type = 'PRIMARY KEY'";
						command.Parameters.AddWithValue(":table_catalog", Connection.Database);
						command.Parameters.AddWithValue(":table_schema", "public");
						command.Parameters.AddWithValue(":table_name", Name);
						using (var reader = command.ExecuteReader()) {
							while (reader.Read()) {
								primaryKeys.Add(reader.GetString(0));
							}
						}
					}
				}

				var columns = Connection.GetSchema("Columns", new[] { Connection.Database, "public", Name });
				columns.DefaultView.Sort = "ordinal_position";
				foreach (DataRowView row in columns.DefaultView) {
					var column = new Column(this) {
						Name = (string)row["column_name"],
						ProviderType = (string)row["data_type"],
					};
					if (column.ProviderType.Equals("varchar", StringComparison.OrdinalIgnoreCase) ||
						column.ProviderType.Equals("bpchar", StringComparison.OrdinalIgnoreCase)) {
						column.ProviderType += "(" + row["character_maximum_length"].ToString() + ")";
					}
					column.InPrimaryKey = primaryKeys.Contains(column.Name);
					columnsCollection.Add(column);
				}
				SetColumnTypes(columnsCollection);
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			using (Database.CreateConnectionScope()) {
				using (var command = Connection.CreateCommand()) {
					command.CommandText =
						"select trigger_name " +
						"from information_schema.triggers t " +
						"where t.event_object_schema = :table_schema " +
						"      and t.event_object_table = :table_name ";
					command.Parameters.AddWithValue(":table_catalog", Connection.Database);
					command.Parameters.AddWithValue(":table_schema", "public");
					command.Parameters.AddWithValue(":table_name", Name);
					using (var reader = command.ExecuteReader()) {
						var triggers = new HashSet<string>();
						while (reader.Read()) {
							var triggerName = reader.GetString(0);
							if (!triggers.Contains(triggerName)) {
								triggers.Add(triggerName);
								triggersCollection.Add(new PgSqlTrigger(this, triggerName));
							}
						}
					}
				}
			}
		}

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			using (Database.CreateConnectionScope()) {
				using (var command = Connection.CreateCommand()) {
					command.CommandText =
						"select conname, " +
						"    att2.attname as child_column, " +
						"    cl.relname as parent_table, " +
						"    att.attname as parent_column " +
						"from " +
						"   (select" +
						"       unnest(con1.conkey) as parent, " +
						"       unnest(con1.confkey) as child, " +
						"       con1.confrelid, " +
						"       con1.conrelid, " +
						"       con1.conname " +
						"   from " +
						"       pg_class cl " +
						"       join pg_namespace ns on cl.relnamespace = ns.oid " +
						"       join pg_constraint con1 on con1.conrelid = cl.oid " +
						"   where " +
						"       cl.relname = :TableName " +
						"       and ns.nspname = 'public' " +
						"       and con1.contype = 'f' " +
						"   ) con " +
						"inner join pg_attribute att on att.attrelid = con.confrelid and att.attnum = con.child " +
						"inner join pg_class cl on cl.oid = con.confrelid " +
						"inner join pg_attribute att2 on att2.attrelid = con.conrelid and att2.attnum = con.parent";
					command.Parameters.AddWithValue(":TableName", Name);
					using (var reader = command.ExecuteReader()) {
						ForeignKey key = null;
						while (reader.Read()) {
							var name = reader.GetString(0);
							if (key == null || key.Name != name) {
								key = new ForeignKey(name, this) {
									Name = name,
									PrimaryTable = reader.GetString(2),
									ChildTable = Name
								};
								foreignKeysCollection.Add(key);
							}
							key.Columns.Add(new ColumnsPair(reader.GetString(3), reader.GetString(1)));
						}
					}
				}
			}
		}

		public override string GenerateCreateStatement()
		{
			if (IsView) {
				using (Database.CreateConnectionScope()) {
					using (var command = Connection.CreateCommand()) {
						command.CommandText = "select pg_get_viewdef(:ViewName)";
						command.Parameters.AddWithValue(":ViewName", Name);
						var viewStatement = command.ExecuteScalar() as string;
						if (!string.IsNullOrEmpty(viewStatement)) {
							return "CREATE OR REPLACE VIEW " + QuotedName + " AS " +
								Environment.NewLine + viewStatement;
						}
					}
				}
			}
			return null;
		}

		public override string GenerateAlterStatement()
		{
			return GenerateCreateStatement();
		}

		public override string GenerateDropStatement()
		{
			return $"DROP {(IsView ? "VIEW" : "TABLE")} {QuotedName}";
		}
	}
}
