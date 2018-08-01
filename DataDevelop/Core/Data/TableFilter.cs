using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataDevelop.Data
{
	public class TableFilter : ICloneable
	{
		private Table table;
		private List<ColumnFilter> filters = new List<ColumnFilter>();

		public TableFilter(Table table)
		{
			this.table = table;

			foreach (var column in table.Columns) {
				filters.Add(new ColumnFilter(column));
			}
		}

		private TableFilter(TableFilter baseFilter)
		{
			table = baseFilter.table;

			foreach (var filter in baseFilter.filters) {
				filters.Add(filter.Clone());
			}
		}

		public IList<ColumnFilter> ColumnFilters => filters;

		public bool IsColumnFiltered => filters.Any(f => !f.Output);

		public bool IsRowFiltered => filters.Any(f => !f.IsEmpty);

		public IEnumerable<string> GetOutputColumns()
		{
			foreach (var filter in ColumnFilters) {
				if (filter.Output) {
					yield return filter.QuotedName;
				}
			}
		}

		public void WriteColumnsProjection(StringBuilder b)
		{
			var first = true;
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
			var first = true;
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
			return Clone();
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
