using System;
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
			this.sorting = new ColumnOrder[table.Columns.Count];
			for (int i = 0; i < this.sorting.Length; i++) {
				this.sorting[i] = new ColumnOrder(table.Columns[i]);
			}
		}

		private TableSort(TableSort baseSort)
		{
			this.table = baseSort.table;
			this.sorting = new ColumnOrder[baseSort.sorting.Length];
			for (int i = 0; i < this.sorting.Length; i++) {
				this.sorting[i] = baseSort.sorting[i].Clone();
			}
		}

		public bool IsSorted
		{
			get
			{
				foreach (var order in sorting) {
					if (order.OrderType != OrderType.None) {
						return true;
					}
				}
				return false;
			}
		}

		public ColumnOrder[] GetColumnOrders()
		{
			return this.sorting;
		}

		public void WriteOrderBy(StringBuilder builder)
		{
			bool first = true;
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
			foreach (var order in this.sorting) {
				order.Clear();
			}
		}

		#region ICloneable Members

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public TableSort Clone()
		{
			return new TableSort(this);
		}

		#endregion
	}
}
