using System;
using System.Linq;
using System.Text;

namespace DataDevelop.Data
{
	public class TableSort : ICloneable
	{
		private Table table;
		private ColumnOrder[] sorting;

		public TableSort(Table table)
		{
			this.table = table;
			sorting = new ColumnOrder[table.Columns.Count];
			for (var i = 0; i < sorting.Length; i++) {
				sorting[i] = new ColumnOrder(table.Columns[i]);
			}
		}

		private TableSort(TableSort baseSort)
		{
			table = baseSort.table;
			sorting = new ColumnOrder[baseSort.sorting.Length];
			for (var i = 0; i < sorting.Length; i++) {
				sorting[i] = baseSort.sorting[i].Clone();
			}
		}

		public bool IsSorted => sorting.Any(i => i.OrderType != OrderType.None);

		public ColumnOrder[] GetColumnOrders()
		{
			return sorting;
		}

		public void WriteOrderBy(StringBuilder builder)
		{
			var first = true;
			foreach (var order in sorting) {
				if (order.OrderType != OrderType.None) {
					if (first) {
						first = false;
					} else {
						builder.Append(", ");
					}
					builder.Append(order.QuotedName);
					if (order.OrderType == OrderType.Descending) {
						builder.Append(" DESC");
					}
				}
			}
		}

		public void Swap(int index1, int index2)
		{
			var temp = sorting[index1];
			sorting[index1] = sorting[index2];
			sorting[index2] = temp;
		}

		public void Reset()
		{
			foreach (var order in sorting) {
				order.Clear();
			}
		}

		#region ICloneable Members

		object ICloneable.Clone()
		{
			return Clone();
		}

		public TableSort Clone()
		{
			return new TableSort(this);
		}

		#endregion

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.AppendLine("SORT:");
			WriteOrderBy(builder);
			return builder.ToString();
		}
	}
}
