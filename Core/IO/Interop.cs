using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using ExcelWorkbook = Microsoft.Office.Interop.Excel.Workbook;
using ExcelWorksheet = Microsoft.Office.Interop.Excel.Worksheet;

namespace DataDevelop.IO
{
	enum Aggregate
	{
		None, Count, Sum, Average, Max, Min
	}

	class ColumnDef
	{
		public string Title { get; set; }
		
		public DataColumn Column { get; set; }
		
		public string Formula { get; set; }
		
		public string Format { get; set; }
		
		public Aggregate Summary { get; set; }

		public bool IsFormula => Column == null && Format != null;

		public ColumnDef(string title, DataColumn column, string format, Aggregate aggregate)
		{
			Title = title;
			Column = column ?? throw new ArgumentNullException("column");
			Format = format;
			Summary = aggregate;
		}

		public ColumnDef(string title, string formula, string format, Aggregate aggregate)
		{
			Title = title;
			Formula = formula ?? throw new ArgumentNullException("formula");
			Format = format;
			Summary = aggregate;
		}
	}

	public class InteropWorksheet : IDisposable
	{
		private readonly ExcelApplication app;
		private readonly ExcelWorkbook book;
		private readonly ExcelWorksheet sheet;

		private readonly List<object[]> rowsBuffer;
		private int lastRowIndex;
		private int columnsCount;

		internal InteropWorksheet(ExcelApplication app, ExcelWorkbook book, ExcelWorksheet sheet)
		{
			this.app = app;
			this.book = book;
			this.sheet = sheet;
			RowsBufferSize = 100;
			rowsBuffer = new List<object[]>(RowsBufferSize);
		}

		public int RowsBufferSize { get; set; }
		public int ColumnOffset { get; set; }
		public int RowOffset { get; set; }

		public string Name
		{
			get { return sheet.Name; }
			set { sheet.Name = value; }
		}

		internal Range this[int rowIndex, int columnIndex]
		{
			get { return GetCell(rowIndex, columnIndex); }
			set { SetCell(rowIndex, columnIndex, value); }
		}

		internal Range GetCell(int rowIndex, int columnIndex)
		{
			return (Range)sheet.Cells[rowIndex + 1, columnIndex + 1];
		}

		internal void SetCell(int rowIndex, int columnIndex, object value)
		{
			GetCell(rowIndex, columnIndex).Formula = value;
		}

		internal Range GetRow(int rowIndex, int columnIndex, int length)
		{
			return sheet.get_Range(GetCell(rowIndex, columnIndex), GetCell(rowIndex, columnIndex + length - 1));
		}

		internal void SetRow(int rowIndex, int columnIndex, object[] values)
		{
			GetRow(rowIndex, columnIndex, values.Length).FormulaArray = values;
		}

		private Range GetTable(int rowIndex, int columnIndex, int rows, int columns)
		{
			return sheet.get_Range(GetCell(rowIndex, columnIndex), GetCell(rowIndex + rows - 1, columnIndex + columns - 1));
		}

		private void SetTable(int rowIndex, int columnIndex, object[,] values)
		{
			GetTable(rowIndex, columnIndex, values.GetLength(0), values.GetLength(1)).FormulaArray = values;
		}

		public void AddRow(object[] values)
		{
			rowsBuffer.Add(values);

			if (values.Length > columnsCount) {
				columnsCount = values.Length;
			}

			if (rowsBuffer.Count >= RowsBufferSize) {
				Flush();
			}
		}

		public void Flush()
		{
			if (rowsBuffer.Count > 0 && columnsCount > 0) {

				var table = new object[rowsBuffer.Count, columnsCount];
				columnsCount = 0;

				for (var i = 0; i < rowsBuffer.Count; i++) {
					var row = rowsBuffer[i];
					for (var j = 0; j < row.Length; j++) {
						table[i, j] = row[j];
					}
				}

				if (lastRowIndex == 0) {
					lastRowIndex = RowOffset;
				}

				SetTable(lastRowIndex, ColumnOffset, table);
				lastRowIndex += rowsBuffer.Count;
				rowsBuffer.Clear();
			}
		}

		public void OpenInExcel()
		{
			Flush();
			sheet.Application.UserControl = true;
			sheet.Application.Visible = true;
		}

		public void SaveAs(string fileName)
		{
			Flush();
			book.SaveAs(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
				XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
		}

		public void Close()
		{
			app.Quit();
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue) {
				if (disposing) {
					// Dispose managed state (managed objects).
				}

				// Free unmanaged resources (unmanaged objects).
				// Set large fields to null.
				Marshal.FinalReleaseComObject(app);
				disposedValue = true;
			}
		}

		~InteropWorksheet()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

	}
	
	public static class ExcelInterop
	{
		public static InteropWorksheet CreateWorksheet(string caption, DataTable data, BackgroundWorker worker)
		{
			var columns = new List<ColumnDef>(data.Columns.Count);
			foreach (DataColumn column in data.Columns) {
				columns.Add(new ColumnDef(column.ColumnName, column, "", Aggregate.None));
			}
			return CreateWorksheet(caption, data, columns, worker);
		}

		internal static InteropWorksheet CreateWorksheet(string caption, DataTable data, IList<ColumnDef> columns, BackgroundWorker worker)
		{
			worker?.ReportProgress(0, "Initializing...");
			var watch = new Stopwatch();
			watch.Start();

			var excel = new ExcelApplication();
			var book = excel.Workbooks.Add(Missing.Value);
			var worksheet = new InteropWorksheet(excel, book, (ExcelWorksheet)book.Worksheets[1]);
			
			excel.Visible = (worker == null);
			excel.Caption = caption;
			if (!string.IsNullOrEmpty(data.TableName)) {
				worksheet.Name = data.TableName;
			}
			excel.Cursor = XlMousePointer.xlWait;
			
			var titles = new List<object>();
			for (var i = 0; i < columns.Count; i++) {
				titles.Add(columns[i].Title);
			}

			var headerRow = worksheet.GetRow(0, 0, columns.Count);
			headerRow.FormulaArray = titles.ToArray();
			headerRow.Font.Bold = true;

			worksheet.GetCell(1, 0).Select();
			excel.ActiveWindow.FreezePanes = true;

			worksheet.RowOffset = 1;
			
			var chrono = new Stopwatch();
			if (worker != null) {
				chrono.Start();
			}
			var milliseconds = chrono.ElapsedMilliseconds;
			for (var rowIndex = 0; rowIndex < data.Rows.Count + 1; rowIndex++) {
				
				var rowValues = new object[data.Columns.Count];

				if (worker != null) {
					
					if (chrono.ElapsedMilliseconds - milliseconds > 200) {
						milliseconds = chrono.ElapsedMilliseconds;
						worker.ReportProgress(100 * rowIndex / data.Rows.Count, string.Format("Exporting Row ({0} of {1})", rowIndex, data.Rows.Count));
					}
					if (worker.CancellationPending) {
						excel.DisplayAlerts = false;
						excel.ActiveWindow.Close(false, Missing.Value, Missing.Value);
						// Worksheet.Dispose releases Excel Application
						worksheet.Dispose();
						return null;
					}
				}
				
				for (var columnIndex = 0; columnIndex < columns.Count; columnIndex++) {
					var column = columns[columnIndex];
					object value = null;
					
					if (rowIndex == data.Rows.Count) {
						var cell = worksheet.GetCell(rowIndex + 1, columnIndex);
						cell.Font.Bold = true;
						if (column.Summary != Aggregate.None) {
							value = $"={column.Summary}({(char)(columnIndex + 0x41)}2:{(char)(columnIndex + 0x41)}{data.Rows.Count + 1})";
						}
					} else {
						if (column.IsFormula) {
							value = $"={string.Format(column.Formula, rowIndex + 2)}";
						} else {

							value = data.Rows[rowIndex][column.Column];
							if (value == null || value == DBNull.Value) {
								value = string.Empty;
							}

							if (value is string strValue) {
								if (strValue.Length > 255) {
									value = "'" + strValue.Substring(0, 251) + "...";
									// TODO: Show a warning that the string was cut
								} else {
									value = "'" + strValue;
								}
							}

							if (value is byte[]) {
								// TODO: Show more explicit error messages
								value = "'";
							}

							var error = data.Rows[rowIndex].GetColumnError(column.Column.Ordinal);
							if (!string.IsNullOrEmpty(error)) {
								var cell = worksheet.GetCell(rowIndex + 1, columnIndex);
								cell.NoteText(error, Missing.Value, Missing.Value);
							}
						}
					}
					if (!string.IsNullOrEmpty(column.Format)) {
						var cell = worksheet.GetCell(rowIndex + 1, columnIndex);
						cell.NumberFormat = column.Format;
					}
					rowValues[columnIndex] = value;
				}
				worksheet.AddRow(rowValues);
			}
			worker?.ReportProgress(0, "Sending data to Excel...");

			worksheet.Flush();
			excel.Cursor = XlMousePointer.xlDefault;
			watch.Stop();
			Console.WriteLine("Elapsed: {0}", watch.Elapsed);
			return worksheet;
		}
		
		internal static Range GetCell(ExcelWorksheet worksheet, int rowIndex, int columnIndex)
		{
			return (Range)worksheet.Cells[rowIndex, columnIndex];
		}

		internal static void SetCell(ExcelWorksheet worksheet, int rowIndex, int columnIndex, object value)
		{
			GetCell(worksheet, rowIndex, columnIndex).Formula = value;
		}

		internal static Range GetRow(ExcelWorksheet worksheet, int rowIndex, int columnIndex, int length)
		{
			return worksheet.get_Range(GetCell(worksheet, rowIndex, columnIndex), GetCell(worksheet, rowIndex, columnIndex + length - 1));
		}

		internal static void SetRow(ExcelWorksheet worksheet, int rowIndex, int columnIndex, object[] values)
		{
			GetRow(worksheet, rowIndex, columnIndex, values.Length).FormulaArray = values;
		}

		internal static Range GetTable(ExcelWorksheet worksheet, int rowIndex, int columnIndex, int rows, int columns)
		{
			return worksheet.get_Range(GetCell(worksheet, rowIndex, columnIndex), GetCell(worksheet, rowIndex + rows - 1, columnIndex + columns - 1));
		}

		internal static void SetTable(ExcelWorksheet worksheet, int rowIndex, int columnIndex, object[,] values)
		{
			GetTable(worksheet, rowIndex, columnIndex, values.GetLength(0), values.GetLength(1)).FormulaArray = values;
		}

		internal static Range FooterFormula(ExcelWorksheet worksheet, DataTable data, int columnIndex, string formula, string format)
		{
			var cell = GetCell(worksheet, data.Rows.Count + 2, columnIndex + 1);
			cell.Formula = "=" + string.Format(formula, data.Rows.Count + 2);
			cell.NumberFormat = format;
			cell.Font.Bold = true;
			return cell;
		}
	}
}
