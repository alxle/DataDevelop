﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public abstract class Table : DbObject
	{
		private IList<Column> columnsCollection;
		private IList<ForeignKey> keysCollection;
		private IList<Trigger> triggersCollection;
		private IList<Index> indexesCollection;

		protected Table(Database database)
			: base(database)
		{
		}

		[Browsable(false)]
		public IList<Column> Columns
		{
			get
			{
				if (columnsCollection == null) {
					var columns = new List<Column>();
					PopulateColumns(columns);
					columnsCollection = columns;
				}
				return columnsCollection;
			}
		}

		[Browsable(false)]
		public IList<ForeignKey> ForeignKeys
		{
			get
			{
				if (keysCollection == null) {
					var keys = new List<ForeignKey>();
					PopulateForeignKeys(keys);
					keysCollection = keys;
				}
				return keysCollection;
			}
		}

		[Browsable(false)]
		public IList<Trigger> Triggers
		{
			get
			{
				if (triggersCollection == null) {
					var triggers = new List<Trigger>();
					PopulateTriggers(triggers);
					triggersCollection = triggers;
				}
				return triggersCollection;
			}
		}

		[Browsable(false)]
		public IList<Index> Indexes
		{
			get
			{
				if (indexesCollection == null) {
					var indexes = new List<Index>();
					PopulateIndexes(indexes);
					indexesCollection = indexes;
				}
				return indexesCollection;
			}
		}

		public virtual bool IsView => false;

		protected bool HasPrimaryKey => Columns.Any(c => c.InPrimaryKey);

		public virtual bool IsReadOnly => false;

		public virtual string DisplayName => Name;

		public virtual string QuotedName => Database.QuoteObjectName(Name);

		public virtual string ObjectName => Name;

		public void Refresh()
		{
			columnsCollection = null;
			triggersCollection = null;
			keysCollection = null;
			indexesCollection = null;
		}

		public abstract void Rename(string newName);

		public int GetRowCount()
		{
			return GetRowCount(null);
		}

		public virtual int GetRowCount(TableFilter filter)
		{
			var count = -1;
			using (var command = Database.CreateCommand()) {
				var sql = new StringBuilder();
				sql.Append("SELECT COUNT(*) FROM ");
				sql.Append(QuotedName);

				if (filter != null && filter.IsRowFiltered) {
					sql.Append(" WHERE ");
					filter.WriteWhereStatement(sql);
				}

				command.CommandText = sql.ToString();
				using (Database.CreateConnectionScope()) {
					count = Convert.ToInt32(command.ExecuteScalar());
				}
			}
			return count;
		}

		public DataTable GetData()
		{
			return GetData(new TableFilter(this));
		}

		public DataTable GetData(TableFilter filter)
		{
			var select = GetBaseSelectCommandText(filter);
			return Database.ExecuteTable(select);
		}

		public DataTable GetData(int startIndex, int count)
		{
			return GetData(startIndex, count, new TableFilter(this));
		}

		public DataTable GetData(int startIndex, int count, TableFilter filter)
		{
			return GetData(startIndex, count, filter, null);
		}

		public abstract DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort);

		public virtual string GetBaseSelectCommandText(TableFilter filter, bool excludeWhere)
		{
			var select = new StringBuilder();
			select.Append("SELECT ");
			if (filter != null || filter.IsColumnFiltered) {
				filter.WriteColumnsProjection(select);
			} else {
				select.Append('*');
			}
			select.Append(" FROM ");
			select.Append(QuotedName);
			if (filter != null && filter.IsRowFiltered && !excludeWhere) {
				select.Append(" WHERE ");
				filter.WriteWhereStatement(select);
			}
			return select.ToString();
		}

		public string GetBaseSelectCommandText(TableFilter filter)
		{
			return GetBaseSelectCommandText(filter, false);
		}

		public string GenerateSelectStatement()
		{
			return GenerateSelectStatement(false);
		}

		public virtual string GenerateSelectStatement(bool singleResult)
		{
			var select = new StringBuilder();
			select.AppendLine("SELECT ");
			var first = true;
			foreach (var column in Columns) {
				select.Append('\t');
				if (first) {
					first = false;
				} else {
					select.Append(',');
				}
				select.AppendLine(column.QuotedName);
			}
			select.Append("FROM ");
			select.Append(QuotedName);
			if (singleResult) {
				select.Append(" WHERE ");
				InsertWhere(select);
			}
			return select.ToString();
		}

		public virtual string GenerateInsertStatement()
		{
			var insert = new StringBuilder();
			insert.Append("INSERT INTO ");
			insert.Append(Name);
			insert.AppendLine();

			for (var i = 0; i < Columns.Count; i++) {
				insert.Append('\t');
				if (i == 0) {
					insert.Append('(');
				} else {
					insert.Append(',');
				}
				insert.Append(Columns[i].Name);
				if (i + 1 >= Columns.Count) {
					insert.Append(')');
				}
				insert.AppendLine();
			}

			insert.AppendLine("VALUES");

			for (var i = 0; i < Columns.Count; i++) {
				insert.Append('\t');
				if (i == 0) {
					insert.Append('(');
				} else {
					insert.Append(',');
				}
				insert.Append(Database.ParameterPrefix);
				insert.Append(Columns[i].Name);
				if (i + 1 >= Columns.Count) {
					insert.Append(')');
				}
				insert.AppendLine();
			}
			return insert.ToString();
		}

		public virtual string GenerateUpdateStatement()
		{
			var update = new StringBuilder();
			update.Append("UPDATE ");
			update.AppendLine(Name);

			var first = true;
			foreach (var column in Columns) {
				update.Append('\t');
				if (first) {
					update.Append("SET ");
					first = false;
				} else {
					update.Append(',');
				}
				update.Append(column.Name);
				update.Append(" = ");
				update.Append(Database.ParameterPrefix);
				update.AppendLine(column.Name);
			}
			update.AppendLine("WHERE");
			InsertWhere(update);
			return update.ToString();
		}

		public virtual string GenerateDeleteStatement()
		{
			var delete = new StringBuilder();
			delete.Append("DELETE FROM ");
			delete.Append(Name);
			delete.Append(" WHERE ");
			InsertWhere(delete);
			return delete.ToString();
		}

		public virtual string GenerateCreateStatement()
		{
			return null;
		}

		public IEnumerable<string> GetColumnNames()
		{
			foreach (var column in Columns) {
				yield return column.Name;
			}
		}

		public virtual string GenerateAlterStatement()
		{
			return null;
		}

		public virtual string GenerateDropStatement()
		{
			var type = IsView ? "VIEW" : "TABLE";
			return "DROP " + type + " " + QuotedName;
		}

		protected virtual void InsertWhere(StringBuilder builder)
		{
			var first = true;
			foreach (var column in Columns) {
				if (column.InPrimaryKey) {
					builder.Append('\t');
					if (first) {
						first = false;
					} else {
						builder.Append(" AND ");
					}
					builder.Append(column.Name);
					builder.Append(" = ");
					builder.Append(Database.ParameterPrefix);
					builder.AppendLine(column.Name);
				}
			}
		}

		protected virtual void SetColumnTypes(IList<Column> columns)
		{
			using (var command = Database.CreateCommand()) {
				command.CommandText = "SELECT * FROM " + QuotedName;
				using (var reader = command.ExecuteReader(CommandBehavior.SchemaOnly)) {
					foreach (var column in columns) {
						var ordinal = reader.GetOrdinal(column.Name);
						if (ordinal != -1) {
							column.Type = reader.GetFieldType(ordinal);
						}
					}
				}
			}
		}

		protected abstract void PopulateColumns(IList<Column> columnsCollection);

		protected abstract void PopulateTriggers(IList<Trigger> triggersCollection);

		protected abstract void PopulateForeignKeys(IList<ForeignKey> foreignKeysCollection);

		protected abstract void PopulateIndexes(IList<Index> indexesCollection);
	}
}
