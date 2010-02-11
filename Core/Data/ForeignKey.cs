using System;
using System.ComponentModel;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public class ForeignKey
	{
		private string name;
		private string primaryTable;
		private string primaryTableColumns;
		private string childTable;
		private string childTableColumns;

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public string PrimaryTable
		{
			get { return this.primaryTable; }
			set { this.primaryTable = value; }
		}

		public string PrimaryTableColumns
		{
			get { return this.primaryTableColumns; }
			set { this.primaryTableColumns = value; }
		}

		public string ChildTable
		{
			get { return this.childTable; }
			set { this.childTable = value; }
		}

		public string ChildTableColumns
		{
			get { return this.childTableColumns; }
			set { this.childTableColumns = value; }
		}
	}
}
