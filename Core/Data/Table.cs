using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.ComponentModel;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public abstract class Table : DbObject
	{
		private IList<Column> columnsCollection;
		private IList<ForeignKey> keysCollection;
		private IList<Trigger> triggersCollection;
		////private DateTime creationDate;
		////private DateTime lastModifiedDate;

		protected Table(Database database)
			: base(database)
		{
		}

		[Browsable(false)]
		public IList<Column> Columns
		{
			get
			{
				if (this.columnsCollection == null) {
					this.columnsCollection = new List<Column>();
					this.PopulateColumns(this.columnsCollection);
				}
				return this.columnsCollection;
			}
		}

		[Browsable(false)]
		public IList<ForeignKey> ForeignKeys
		{
			get
			{
				if (this.keysCollection == null) {
					this.keysCollection = new List<ForeignKey>();
					this.PopulateForeignKeys(this.keysCollection);
				}
				return this.keysCollection;
			}
		}

		[Browsable(false)]
		public IList<Trigger> Triggers
		{
			get
			{
				if (this.triggersCollection == null) {
					this.triggersCollection = new List<Trigger>();
					this.PopulateTriggers(this.triggersCollection);
				}
				return this.triggersCollection;
			}
		}

		public virtual bool IsView
		{
			get { return false; }
		}

		protected bool HasPrimaryKey
		{
			get
			{
				foreach (Column c in this.Columns) {
					if (c.InPrimaryKey) {
						return true;
					}
				}
				return false;
			}
		}

		public virtual bool IsReadOnly
		{
			get { return false; }
		}

		public virtual string DisplayName
		{
			get { return this.Name; }
		}

		public virtual string QuotedName
		{
			get { return this.Database.QuoteObjectName(this.Name); }
		}

		public void RefreshColumns()
		{
			this.columnsCollection = null;
		}

		public abstract bool Rename(string newName);

		public virtual bool Delete()
		{
			using (IDbCommand create = Database.CreateCommand()) {
				create.CommandText = "DROP " + (this.IsView ? "VIEW " : "TABLE ") + this.QuotedName;
				try {
					create.ExecuteNonQuery();
					return true;
				} catch {
					return false;
				}
			}
		}

		public int GetRowCount()
		{
			return this.GetRowCount(null);
		}

		public virtual int GetRowCount(TableFilter filter)
		{
			int count = -1;
			IDbCommand command = this.Database.CreateCommand();
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

		public DataTable GetData()
		{
			return this.GetData(new TableFilter(this));
		}

		public DataTable GetData(TableFilter filter)
		{
			using (var adapter = this.Database.CreateAdapter(this, filter)) {
				var data = new DataTable(this.Name);
				adapter.Fill(data);
				return data;
			}
		}

		public DataTable GetData(int startIndex, int count)
		{
			return this.GetData(startIndex, count, new TableFilter(this));
		}

		public DataTable GetData(int startIndex, int count, TableFilter filter)
		{
			return this.GetData(startIndex, count, filter, null);
		}

		public abstract DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort);

		public virtual string GetBaseSelectCommandText(TableFilter filter)
		{
			StringBuilder select = new StringBuilder();
			select.Append("SELECT ");
			if (filter != null || filter.IsColumnFiltered) {
				filter.WriteColumnsProjection(select);
			} else {
				select.Append('*');
			}
			select.Append(" FROM ");
			select.Append(this.QuotedName);
			if (filter.IsRowFiltered) {
				select.Append(" WHERE ");
				filter.WriteWhereStatement(select);
			}
			return select.ToString();
		}

		public string GenerateSelectStatement()
		{
			return this.GenerateSelectStatement(false);
		}

		public virtual string GenerateSelectStatement(bool singleResult)
		{
			StringBuilder select = new StringBuilder();
			select.AppendLine("SELECT ");
			bool first = true;
			foreach (Column column in this.Columns) {
				select.Append('\t');
				if (first) {
					first = false;
				} else {
					select.Append(',');
				}
				select.AppendLine(column.QuotedName);
			}
			select.Append("FROM ");
			select.Append(this.QuotedName);
			if (singleResult) {
				select.Append(" WHERE ");
				this.InsertWhere(select);
			}
			return select.ToString();
		}

		public virtual string GenerateInsertStatement()
		{
			StringBuilder insert = new StringBuilder();
			insert.Append("INSERT INTO ");
			insert.Append(this.Name);
			insert.AppendLine();

			for (int i = 0; i < this.Columns.Count; i++) {
				insert.Append('\t');
				if (i == 0) {
					insert.Append('(');
				} else {
					insert.Append(',');
				}
				insert.Append(this.Columns[i].Name);
				if (i + 1 >= this.Columns.Count) {
					insert.Append(')');
				}
				insert.AppendLine();
			}

			insert.AppendLine("VALUES");

			for (int i = 0; i < this.Columns.Count; i++) {
				insert.Append('\t');
				if (i == 0) {
					insert.Append('(');
				} else {
					insert.Append(',');
				}
				insert.Append(this.Database.ParameterPrefix);
				insert.Append(this.Columns[i].Name);
				if (i + 1 >= this.Columns.Count) {
					insert.Append(')');
				}
				insert.AppendLine();
			}
			return insert.ToString();
		}

		public virtual string GenerateUpdateStatement()
		{
			StringBuilder update = new StringBuilder();
			update.Append("UPDATE ");
			update.AppendLine(this.Name);

			bool first = true;
			foreach (Column column in this.Columns) {
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
			this.InsertWhere(update);
			return update.ToString();
		}

		public virtual string GenerateDeleteStatement()
		{
			StringBuilder delete = new StringBuilder();
			delete.Append("DELETE FROM ");
			delete.Append(this.Name);
			delete.Append(" WHERE ");
			this.InsertWhere(delete);
			return delete.ToString();
		}
		
		public virtual string GenerateCreateStatement()
		{
			return null;
		}

		public IEnumerable<string> GetColumnNames()
		{
			foreach (Column column in this.Columns) {
				yield return column.Name;
			}
		}

		public virtual string GenerateAlterStatement()
		{
			return null;
		}

		public virtual string GenerateDropStatement()
		{
			return null;
		}

		protected virtual void InsertWhere(StringBuilder builder)
		{
			bool first = true;
			foreach (Column column in this.Columns) {
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

		protected virtual void SetColumnTypes()
		{
			using (IDbCommand command = Database.CreateCommand()) {
				command.CommandText = "SELECT * FROM " + this.QuotedName;
				using (IDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly)) {
					foreach (Column column in this.Columns) {
						int ordinal = reader.GetOrdinal(column.Name);
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

	}
}
