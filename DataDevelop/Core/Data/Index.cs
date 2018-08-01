using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public class Index : ITableObject
	{
		public Index(Table table, string name)
		{
			Table = table;
			Name = name;
			Columns = new List<ColumnOrder>();
		}

		[Browsable(false)]
		public Table Table { get;  set; }

		public string Name { get; set; }

		public bool IsPrimaryKey { get; set; }

		public bool IsUniqueKey { get; set; }

		[Browsable(false)]
		public IList<ColumnOrder> Columns { get; }

		[Browsable(true), DisplayName("Columns")]
		public string IndexDetails => string.Join(", ", Columns.Select(i => i.ColumnName + GetOrderBy(i.OrderType)).ToArray());

		private static string GetOrderBy(OrderType orderType)
		{
			if (orderType == OrderType.Ascending)
				return " ASC";
			if (orderType == OrderType.Descending)
				return " DESC";
			return "";
		}
	}
}
