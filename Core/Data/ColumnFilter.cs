using System;

namespace DataDevelop.Data
{
	public class ColumnFilter : ICloneable
	{
		private Column column;
		private string filter;
		private bool output = true;

		public ColumnFilter(Column column)
		{
			this.column = column;
		}

		public string ColumnName
		{
			get { return this.column.Name; }
		}

		public string QuotedName
		{
			get { return this.column.QuotedName; }
		}

		public string Filter
		{
			get { return this.filter; }
			set { this.filter = value; }
		}

		public bool IsEmpty
		{
			get { return String.IsNullOrEmpty(this.filter); }
		}

		public bool InPrimaryKey
		{
			get { return this.column.InPrimaryKey; }
		}

		public bool Output
		{
			get { return this.output; }
			set
			{
				if (!value && this.column.InPrimaryKey) {
					throw new InvalidOperationException("All columns in primary key must be outputted");
				}
				this.output = value;
			}
		}

		public void Clear()
		{
			this.filter = null;
		}

		public ColumnFilter Clone()
		{
			var columnFilter = new ColumnFilter(this.column);
			columnFilter.output = this.output;
			columnFilter.filter = this.filter;
			return columnFilter;
		}

		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}