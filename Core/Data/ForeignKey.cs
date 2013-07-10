using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public class ForeignKey : IDbObject
	{
		private Table table;
		private string name;
		private string primaryTable;
		private string childTable;
		private IList<ColumnsPair> columns;

		public ForeignKey(string name, Table table)
		{
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			if (table == null) {
				throw new ArgumentNullException("table");
			}

			this.name = name;
			this.table = table;
		}

		public Table Table
		{
			get { return this.table; }
		}

		public Database Database
		{
			get { return this.table.Database; }
		}

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

		public string ChildTable
		{
			get { return this.childTable; }
			set { this.childTable = value; }
		}

		public IList<ColumnsPair> Columns
		{
			get
			{
				if (this.columns == null) {
					this.columns = new List<ColumnsPair>();
				}
				return this.columns;
			}
		}

		public static string GenerateSelectStatement(Table table)
		{
			var select = new StringBuilder();
			char t = 'b';

			foreach (var key in table.ForeignKeys) {
				if (key.Columns.Count > 0) {
					if (select.Length == 0) {
						select.Append("SELECT ");
						select.Append("* ");
						select.AppendLine("FROM " + key.ChildTable + " a");
					}
					select.AppendLine("INNER JOIN " + key.PrimaryTable + " " + t);
					select.Append("  ON ");

					for (int i = 0; i < key.Columns.Count; i++) {
						if (i > 0) {
							select.Append(" AND ");
						}
						select.Append("a." + key.Columns[i].ChildColumn);
						select.Append(" = " + t + "." + key.Columns[i].ParentColumn);
					}
					select.AppendLine();
					t++;
				}
			}

			if (select.Length == 0) {
				select.Append("-- This feature is not implemented for the current provider");
			}

			return select.ToString();
		}

		public virtual string GenerateSelectStatement()
		{
			var select = new StringBuilder();

			if (this.Columns.Count > 0) {

				select.Append("SELECT ");
				select.Append("* ");
				select.AppendLine("FROM " + this.ChildTable + " a");
				select.AppendLine("INNER JOIN " + this.PrimaryTable + " b");
				select.Append("  ON ");

				for (int i = 0; i < this.Columns.Count; i++) {
					if (i > 0) {
						select.Append(" AND ");
					}
					select.Append("a." + this.Columns[i].ChildColumn);
					select.Append(" = b." + this.Columns[i].ParentColumn);
				}
			} else {
				select.Append("-- This feature is not implemented for the current provider");
			}

			return select.ToString();
		}
	}

	public class ColumnsPair
	{
		public string ParentColumn { get; set; }

		public string ChildColumn { get; set; }

		public ColumnsPair(string parentColumn, string childColumn)
		{
			this.ParentColumn = parentColumn;
			this.ChildColumn = childColumn;
		}
	}
}
