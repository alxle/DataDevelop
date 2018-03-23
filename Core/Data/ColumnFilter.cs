using System;

namespace DataDevelop.Data
{
	public class ColumnFilter : ICloneable
	{
		private Column column;
		private bool output = true;

		public ColumnFilter(Column column)
		{
			this.column = column;
		}

		public string ColumnName => column.Name;

		public string QuotedName => column.QuotedName;

		public string Filter { get; set; }

		public bool IsEmpty => string.IsNullOrEmpty(Filter);

		public bool InPrimaryKey => column.InPrimaryKey;

		public bool Output
		{
			get => output;
			set
			{
				if (!value && column.InPrimaryKey) {
					throw new InvalidOperationException("All columns in primary key must be outputted");
				}
				output = value;
			}
		}

		public void Clear()
		{
			Filter = null;
		}

		public ColumnFilter Clone()
		{
			var columnFilter = new ColumnFilter(column) {
				output = output,
				Filter = Filter
			};
			return columnFilter;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}
	}
}
