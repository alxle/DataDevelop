using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public class ForeignKey : IDbObject
	{
		private IList<ColumnsPair> columns;

		public ForeignKey(string name, Table table)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Table = table ?? throw new ArgumentNullException(nameof(table));
		}

		[Browsable(false)]
		public Table Table { get; }

		[Browsable(false)]
		public Database Database => Table.Database;

		public string Name { get; set; }

		public string PrimaryTable { get; set; }

		public string ChildTable { get; set; }

		[Browsable(false)]
		public IList<ColumnsPair> Columns => columns ?? (columns = new List<ColumnsPair>());

		[Browsable(true)]
		public string PrimaryColumns
		{
			get
			{
				var str = new StringBuilder();
				for (var i = 0; i < Columns.Count; i++) {
					if (i > 0) {
						str.Append(',');
					}
					str.Append(Columns[i].ParentColumn);
				}
				return str.ToString();
			}
		}

		[Browsable(true)]
		public string ChildColumns
		{
			get
			{
				var str = new StringBuilder();
				for (var i = 0; i < Columns.Count; i++) {
					if (i > 0) {
						str.Append(',');
					}
					str.Append(Columns[i].ChildColumn);
				}
				return str.ToString();
			}
		}

		[Browsable(true)]
		public string ForeignKeyDetails
		{
			get
			{
				var str = new StringBuilder();
				str.Append(ChildTable);
				str.Append('(');
				str.Append(ChildColumns);
				str.Append(')');
				str.Append("->");
				str.Append(PrimaryTable);
				str.Append('(');
				str.Append(PrimaryColumns);
				str.Append(')');
				return str.ToString();
			}
		}

		public static string GenerateSelectStatement(Table table)
		{
			var select = new StringBuilder();
			var t = 'b';

			foreach (var key in table.ForeignKeys) {
				if (key.Columns.Count > 0) {
					if (select.Length == 0) {
						select.Append("SELECT ");
						select.Append("* ");
						select.AppendLine("FROM " + key.ChildTable + " a");
					}
					select.AppendLine("INNER JOIN " + key.PrimaryTable + " " + t);
					select.Append("  ON ");

					for (var i = 0; i < key.Columns.Count; i++) {
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

			if (Columns.Count > 0) {

				select.Append("SELECT ");
				select.Append("* ");
				select.AppendLine("FROM " + ChildTable + " a");
				select.AppendLine("INNER JOIN " + PrimaryTable + " b");
				select.Append("  ON ");

				for (var i = 0; i < Columns.Count; i++) {
					if (i > 0) {
						select.Append(" AND ");
					}
					select.Append("a." + Columns[i].ChildColumn);
					select.Append(" = b." + Columns[i].ParentColumn);
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
			ParentColumn = parentColumn;
			ChildColumn = childColumn;
		}
	}
}
