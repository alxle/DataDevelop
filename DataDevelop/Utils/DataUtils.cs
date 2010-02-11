using System;
using System.Text;
using System.IO;
using System.Data;

namespace DataDevelop.Utils
{
	static class DataUtils
	{
		public static void WriteToFile(string fileName, DataTable dataTable, char rowSeparator)
		{
			using (FileStream file = File.OpenWrite(fileName)) {
				using (StreamWriter stream = new StreamWriter(file, Encoding.Default)) {
					bool first = true;
					foreach (DataColumn column in dataTable.Columns) {
						if (first) {
							first = false;
						} else {
							stream.Write(rowSeparator);
						}
						stream.Write(column.ColumnName);
					}
					stream.WriteLine();
					foreach (DataRow row in dataTable.Rows) {
						object[] values = row.ItemArray;
						for (int i = 0; i < values.Length; i++) {
							if (i != 0) {
								stream.Write(rowSeparator);
							}
							stream.Write(values[i].ToString());
						}
						stream.WriteLine();
					}
				}
			}
		}
	}
}
