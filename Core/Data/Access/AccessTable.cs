using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using DataDevelop.Data.OleDb;

namespace DataDevelop.Data.Access
{
	internal sealed class AccessTable : OleDbTable
	{
		public AccessTable(AccessDatabase database)
			: base(database)
		{
		}

		private new OleDbConnection Connection => ((AccessDatabase)Database).Connection;

		public override DataTable GetData(int startIndex, int count, TableFilter filter, TableSort sort)
		{
			using (Database.CreateConnectionScope()) {
				if (sort == null || !sort.IsSorted) {
					var primaryKey = Columns.Where(i => i.InPrimaryKey).Select(i => i.QuotedName).ToArray();
					if (primaryKey.Length > 0) {
						using (var adapter = Database.CreateAdapter(this, filter)) {
							adapter.SelectCommand.CommandText = GetPagingWithKeyStatement(startIndex, count, filter, primaryKey);
							var data = new DataTable(Name);
							adapter.Fill(data);
							return data;
						}
					}
				}
				return GetDataSecuencial(startIndex, count, filter, sort);
			}
		}

		private string GetPagingWithKeyStatement(int startIndex, int count, TableFilter filter, string[] primaryKeyColumns)
		{
			var total = GetRowCount(filter);
			var select = new StringBuilder();
			if (startIndex == 0 || count == 0 || total <= count) {
				select.AppendFormat("SELECT TOP {0} ", count);
				filter.WriteColumnsProjection(select);
				select.Append(" FROM ");
				select.Append(QuotedName);
				if (filter.IsRowFiltered) {
					select.Append(" WHERE (");
					filter.WriteWhereStatement(select);
					select.Append(")");
				}
			} else {
				// SELECT TOP {count} * FROM 
				//  (SELECT TOP {total - startIndex} * FROM {table} ORDER BY {pk} DESC)
				// ORDER BY {pk} ASC
				select.Append("SELECT TOP ");
				select.Append(count);
				select.Append(" * FROM (SELECT TOP ");
				select.Append(Math.Max(total - startIndex, 0));
				select.Append(" ");
				filter.WriteColumnsProjection(select);
				select.Append(" FROM ");
				select.Append(QuotedName);
				if (filter.IsRowFiltered) {
					select.Append(" WHERE (");
					filter.WriteWhereStatement(select);
					select.Append(")");
				}
				select.Append(" ORDER BY ");
				select.Append(string.Join(", ", primaryKeyColumns.Select(i => i + " DESC").ToArray()));
				select.Append(")");
			}
			select.Append(" ORDER BY ");
			select.Append(string.Join(", ", primaryKeyColumns.Select(i => i + " ASC").ToArray()));
			return select.ToString();
		}
	}
}
