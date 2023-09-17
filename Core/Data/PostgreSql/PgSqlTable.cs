using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
				alter.CommandText = $@"ALTER TABLE ""{Name}"" RENAME TO ""{newName}""";
				try {
					alter.ExecuteNonQuery();
					Name = newName;
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
				using (var adapter = (NpgsqlDataAdapter)Database.CreateAdapter(this, filter)) {
					adapter.SelectCommand = select;
					adapter.Fill(data);
				}
			}
			return data;
		}

		protected override void PopulateColumns(IList<Column> columnsCollection)
		{
			using (var command = Connection.CreateCommand()) {
				command.CommandText = @"
					select
					  c.column_name, c.is_nullable, c.data_type, c.character_maximum_length,
					  c.numeric_precision, c.numeric_scale, c.is_identity,
					  (case when pk.column_name is null then 'NO' else 'YES' end) in_primary_key
					from information_schema.columns c
					left join (
					  select k.column_name
					  from information_schema.table_constraints tc
					  inner join information_schema.key_column_usage k on
					    tc.constraint_schema = k.constraint_schema and 
					    tc.constraint_name = k.constraint_name
					  where tc.constraint_schema = :schema and tc.table_name = :table
					  and tc.constraint_name = k.constraint_name and tc.constraint_type = 'PRIMARY KEY'
					  ) pk on c.column_name = pk.column_name 
					where c.table_schema = :schema and c.table_name = :table
					order by c.ordinal_position
					";
				command.Parameters.AddWithValue(":schema", "public");
				command.Parameters.AddWithValue(":table", Name);
				using (var reader = command.ExecuteReader()) {
					while (reader.Read()) {
						var column = new Column(this) {
							Name = reader.GetString(0),
							ProviderType = reader.GetString(2),
							Type = PgSqlProvider.MapType(reader.GetString(2)),
							IsIdentity = reader.GetString(6) == "YES",
							InPrimaryKey = reader.GetString(7) == "YES",
						};
						columnsCollection.Add(column);
					}
				}
			}
			//SetColumnTypes(columnsCollection);
		}

		protected override void PopulateIndexes(IList<Index> indexesCollection)
		{
			using (var select = database.Connection.CreateCommand()) {
				select.CommandText = @"
					select
					  t.relname as table_name,
					  i.relname as index_name,
					  a.attname as column_name,
					  ix.indisunique as is_unique,
					  ix.indisprimary as is_primary,
					  ix.indisclustered as is_clustered,
					  ix.indisvalid as is_valid,
					  case (ix.indoption)[0] & 1 when 1 then 'DESC' else 'ASC' end column_order
					from pg_index ix
					inner join pg_class t on t.oid = ix.indrelid and t.relkind = 'r'
					inner join pg_namespace n on t.relnamespace = n.oid 
					inner join pg_class i on i.oid = ix.indexrelid
					inner join pg_attribute a on a.attrelid = t.oid and a.attnum = ANY(ix.indkey)
					where n.nspname = :schema and t.relname = :table
					order by t.relname, i.relname
					";
				select.Parameters.AddWithValue(":schema", "public");
				select.Parameters.AddWithValue(":table", Name);
				var data = new DataTable();
				using (var reader = select.ExecuteReader()) {
					data.Load(reader);
				}
				var indexes = data.Rows.Cast<DataRow>().GroupBy(r => (string)r["index_name"]);
				foreach (var index in indexes) {
					var i = new Index(this, index.Key);
					var first = index.First();
					i.IsUniqueKey = (bool)first["is_unique"];
					i.IsPrimaryKey = (bool)first["is_primary"];
					foreach (var indexColumn in index) {
						var columnName = (string)indexColumn["column_name"];
						var columnOrder = (string)indexColumn["column_order"];
						var column = Columns.Single(c => c.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));
						i.Columns.Add(new ColumnOrder(column) {
							OrderType = columnOrder == "DESC" ? OrderType.Descending : OrderType.Ascending
						});
					}
					indexesCollection.Add(i);
				}
			}
		}

		protected override void PopulateTriggers(IList<Trigger> triggersCollection)
		{
			using (var command = Connection.CreateCommand()) {
				command.CommandText = @"
						select trigger_name
						from information_schema.triggers t
						where
						  t.event_object_schema = :table_schema and
						  t.event_object_table = :table_name
						";
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

		protected override void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection)
		{
			using (var command = Connection.CreateCommand()) {
				command.CommandText = @"
					select
					  con.conname,
					  att2.attname as child_column,
					  cl.relname as parent_table,
					  att.attname as parent_column
					from
					   (select
						   unnest(con1.conkey) as parent,
						   unnest(con1.confkey) as child,
						   con1.confrelid,
						   con1.conrelid,
						   con1.conname
					   from pg_class cl
					   inner join pg_namespace ns on cl.relnamespace = ns.oid
					   inner join pg_constraint con1 on con1.conrelid = cl.oid
					   where
						 cl.relname = :table_name and
						 ns.nspname = :schema_name and
						 con1.contype = 'f'
					   ) con
					inner join pg_attribute att on att.attrelid = con.confrelid and att.attnum = con.child
					inner join pg_class cl on cl.oid = con.confrelid
					inner join pg_attribute att2 on att2.attrelid = con.conrelid and att2.attnum = con.parent";
				command.Parameters.AddWithValue(":schema_name", "public");
				command.Parameters.AddWithValue(":table_name", Name);
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

		public override string GenerateCreateStatement()
		{
			if (IsView) {
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
