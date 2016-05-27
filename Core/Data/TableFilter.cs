using System;
using System.Collections.Generic;
using System.Text;

namespace DataDevelop.Data
{
	public class TableFilter : ICloneable
	{
		private Table table;
		private List<ColumnFilter> filters;

		public TableFilter(Table table)
		{
			this.table = table;
			this.filters = new List<ColumnFilter>();

			foreach (var column in table.Columns) {
				filters.Add(new ColumnFilter(column));
			}
		}

		private TableFilter(TableFilter baseFilter)
		{
			this.table = baseFilter.table;
			this.filters = new List<ColumnFilter>();

			foreach (var filter in baseFilter.filters) {
				this.filters.Add(filter.Clone());
			}
		}

		public IList<ColumnFilter> ColumnFilters
		{
			get { return filters; }
		}

		public bool IsColumnFiltered
		{
			get
			{
				foreach (var f in filters) {
					if (!f.Output) {
						return true;
					}
				}
				return false;
			}
		}

		public bool IsRowFiltered
		{
			get
			{
				foreach (var f in filters) {
					if (!f.IsEmpty) {
						return true;
					}
				}
				return false;
			}
		}

		public IEnumerable<string> GetOutputColumns()
		{
			foreach (var filter in this.ColumnFilters) {
				if (filter.Output) {
					yield return filter.QuotedName;
				}
			}
		}

		public void WriteColumnsProjection(StringBuilder b)
		{
			bool first = true;
			foreach (var f in filters) {
				if (f.InPrimaryKey || f.Output) {
					if (first) {
						first = false;
					} else {
						b.Append(", ");
					}
					b.Append(f.QuotedName);
				}
			}
		}

		public void WriteWhereStatement(StringBuilder b)
		{
			bool first = true;
			foreach (var f in filters) {
				if (!f.IsEmpty) {
					if (first) {
						first = false;
					} else {
						b.Append(" AND ");
					}
					b.Append('(');
					b.Append(f.QuotedName);
					b.Append(' ');
					b.Append(f.Filter);
					b.Append(')');
				}
			}
		}

		#region ICloneable Members

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public TableFilter Clone()
		{
			return new TableFilter(this);
		}

		#endregion

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.AppendLine("FILTER:");
			WriteWhereStatement(builder);
			builder.AppendLine();
			builder.AppendLine();
			builder.AppendLine("COLUMNS:");
			WriteColumnsProjection(builder);
			return builder.ToString();
		}
	}
}
