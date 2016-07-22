using System;
using System.Text;
using System.IO;
using System.Data;

namespace DataDevelop.Utils
{
	public static class DataUtils
	{
		public static void WriteToFile(string fileName, DataTable dataTable, char rowSeparator)
		{
			WriteToFile(fileName, dataTable, rowSeparator, '"');
		}

		public static void WriteToFile(string fileName, DataTable dataTable, char rowSeparator, char textQualifier)
		{
			string rowSep = rowSeparator.ToString();
			using (StreamWriter stream = new StreamWriter(fileName, false, Encoding.Default)) {
				bool first = true;
				foreach (DataColumn column in dataTable.Columns) {
					if (first) {
						first = false;
					} else {
						stream.Write(rowSeparator);
					}
					if (column.ColumnName.Contains(rowSep)) {
						stream.Write(textQualifier);
						stream.Write(column.ColumnName);
						stream.Write(textQualifier);
					} else {
						stream.Write(column.ColumnName);
					}
				}
				stream.WriteLine();
				foreach (DataRow row in dataTable.Rows) {
					object[] values = row.ItemArray;
					for (int i = 0; i < values.Length; i++) {
						if (i != 0) {
							stream.Write(rowSeparator);
						}
						var value = values[i].ToString();
						if (value.Contains(rowSep)) {
							stream.Write(textQualifier);
							stream.Write(value);
							stream.Write(textQualifier);
						} else {
							stream.Write(value);
						}
					}
					stream.WriteLine();
				}
			}
		}
	}
}
