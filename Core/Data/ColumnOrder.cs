using System;
using System.Collections.Generic;
using System.Text;
using DataDevelop.Data;

namespace DataDevelop.Data
{
	public class ColumnOrder : ICloneable
	{
		private Column column;
		private OrderType orderType = OrderType.None;

		public ColumnOrder(Column column)
		{
			this.column = column;
		}

		public string ColumnName
		{
			get { return column.Name; }
		}

		public string QuotedName
		{
			get { return column.QuotedName; }
		}

		public OrderType OrderType
		{
			get { return orderType; }
			set { orderType = value; }
		}

		public ColumnOrder Clone()
		{
			var order = new ColumnOrder(this.column);
			order.orderType = this.orderType;
			return order;
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public void Clear()
		{
			this.orderType = OrderType.None;
		}
	}
}
