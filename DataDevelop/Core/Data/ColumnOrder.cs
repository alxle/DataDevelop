using System;

namespace DataDevelop.Data
{
	public class ColumnOrder : ICloneable
	{
		private Column column;

		public ColumnOrder(Column column)
		{
			this.column = column;
		}

		public string ColumnName => column.Name;

		public string QuotedName => column.QuotedName;

		public OrderType OrderType { get; set; } = OrderType.None;

		public ColumnOrder Clone()
		{
			return new ColumnOrder(column) {
				OrderType = OrderType
			};
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		public void Clear()
		{
			OrderType = OrderType.None;
		}
	}
}
