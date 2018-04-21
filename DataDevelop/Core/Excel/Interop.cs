using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using DataTable = System.Data.DataTable;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using ExcelWorkbook = Microsoft.Office.Interop.Excel.Workbook;
using ExcelWorksheet = Microsoft.Office.Interop.Excel.Worksheet;
using Microsoft.CSharp;

namespace DataDevelop.Core.Excel
{
	enum Agregate
	{
		None, Count, Sum, Average, Max, Min
	}

	class ColumnDef
	{
		public string Title { get; set; }
		
		public DataColumn Column { get; set; }
		
		public string Formula { get; set; }
		
		public string Format { get; set; }
		
		public Agregate Summary { get; set; }
		
		public bool IsFormula
		{
			get { return this.Column == null && this.Format != null; }
		}

		public ColumnDef(string title, DataColumn column, string format, Agregate agregate)
		{
			if (column == null) {
				throw new ArgumentNullException("column");
			}
			this.Title = title;
			this.Column = column;
			this.Format = format;
			this.Summary = agregate;
		}

		public ColumnDef(string title, string formula, string format, Agregate agregate)
		{
			if (formula == null) {
				throw new ArgumentNullException("formula");
			}
			this.Title = title;
			this.Formula = formula;
			this.Format = format;
			this.Summary = agregate;
		}
	}

	public class InteropWorksheet : IDisposable
	{
		private ExcelApplication app;
		private ExcelWorkbook book;
		private ExcelWorksheet sheet;

		private List<object[]> rowsBuffer;
		private int lastRowIndex;
		private int columnsCount;

		internal InteropWorksheet(ExcelApplication app, ExcelWorkbook book, ExcelWorksheet sheet)
		{
			this.app = app;
			this.book = book;
			this.sheet = sheet;
			this.RowsBufferSize = 100;
			this.rowsBuffer = new List<object[]>(this.RowsBufferSize);
		}

		public int RowsBufferSize { get; set; }
		public int ColumnOffset { get; set; }
		public int RowOffset { get; set; }

		public string Name
		{
			get { return this.sheet.Name; }
			set { this.sheet.Name = value; }
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
			this.rowsBuffer.Add(values);

			if (values.Length > this.columnsCount) {
				this.columnsCount = values.Length;
			}

			if (this.rowsBuffer.Count >= this.RowsBufferSize) {
				this.Flush();
			}
		}

		public void Flush()
		{
			if (this.rowsBuffer.Count > 0 && this.columnsCount > 0) {

				var table = new object[this.rowsBuffer.Count, this.columnsCount];
				this.columnsCount = 0;

				for (var i = 0; i < this.rowsBuffer.Count; i++) {
					var row = this.rowsBuffer[i];
					for (var j = 0; j < row.Length; j++) {
						table[i, j] = row[j];
					}
				}

				if (this.lastRowIndex == 0) {
					this.lastRowIndex = this.RowOffset;
				}

				SetTable(this.lastRowIndex, this.ColumnOffset, table);
				this.lastRowIndex += this.rowsBuffer.Count;
				this.rowsBuffer.Clear();
			}
		}

		internal Range this[int rowIndex, int columnIndex]
		{
			get { return this.GetCell(rowIndex, columnIndex); }
			set { this.SetCell(rowIndex, columnIndex, value); }
		}

		public void OpenInExcel()
		{
			this.Flush();
			this.sheet.Application.UserControl = true;
			this.sheet.Application.Visible = true;
		}

		public void SaveAs(string fileName)
		{
			this.Flush();
			this.book.SaveAs(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
				XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
		}

		public void Close()
		{
			this.app.Quit();
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
				Marshal.FinalReleaseComObject(this.app);
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
				columns.Add(new ColumnDef(column.ColumnName, column, "", Agregate.None));
			}
			return CreateWorksheet(caption, data, columns, worker);
		}

		internal static InteropWorksheet CreateWorksheet(string caption, DataTable data, IList<ColumnDef> columns, BackgroundWorker worker)
		{
			if (worker != null) {
				worker.ReportProgress(0, "Initializing...");
			}
			var watch = new System.Diagnostics.Stopwatch();
			watch.Start();

			var excel = new ExcelApplication();
			var book = excel.Workbooks.Add(Missing.Value);
			var worksheet = new InteropWorksheet(excel, book, (ExcelWorksheet)book.Worksheets[1]);
			
			excel.Visible = (worker == null);
			excel.Caption = caption;
			if (!String.IsNullOrEmpty(data.TableName)) {
				worksheet.Name = data.TableName;
			}
			excel.Cursor = XlMousePointer.xlWait;
			
			var titles = new List<object>();
			for (int i = 0; i < columns.Count; i++) {
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
			for (int rowIndex = 0; rowIndex < data.Rows.Count + 1; rowIndex++) {
				
				var rowValues = new object[data.Columns.Count];

				if (worker != null) {
					
					if (chrono.ElapsedMilliseconds - milliseconds > 200) {
						milliseconds = chrono.ElapsedMilliseconds;
						worker.ReportProgress(100 * rowIndex / data.Rows.Count, String.Format("Exporting Row ({0} of {1})", rowIndex, data.Rows.Count));
					}
					if (worker.CancellationPending) {
						excel.DisplayAlerts = false;
						excel.ActiveWindow.Close(false, Missing.Value, Missing.Value);
						// Worksheet.Dispose releases Excel Application
						worksheet.Dispose();
						return null;
					}
				}
				
				for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++) {
					var column = columns[columnIndex];
					object value = null;
					
					if (rowIndex == data.Rows.Count) {
						var cell = worksheet.GetCell(rowIndex + 1, columnIndex);
						cell.Font.Bold = true;
						if (column.Summary != Agregate.None) {
							value = String.Format("={0}({1}2:{1}{2})", column.Summary, (char)(columnIndex + 0x41), data.Rows.Count + 1);
						}
					} else {
						if (column.IsFormula) {
							value = "=" + String.Format(column.Formula, rowIndex + 2);
						} else {

							value = data.Rows[rowIndex][column.Column];
							if (value == null || value == DBNull.Value) {
								value = String.Empty;
							}
							
							if (value is string) {
								var strValue = (string)value;
								if (strValue.Length > 255) {
									value = "'" + strValue.Substring(0, 251) + "...";
									// TODO: Show a warning that the string was cutted
								} else {
									value = "'" + strValue;
								}
							}

							if (value is byte[]) {
								// TODO: Show more explicit error messages
								value = "'";
							}

							var error = data.Rows[rowIndex].GetColumnError(column.Column.Ordinal);
							if (!String.IsNullOrEmpty(error)) {
								var cell = worksheet.GetCell(rowIndex + 1, columnIndex);
								cell.NoteText(error, Missing.Value, Missing.Value);
							}
						}
					}
					if (!String.IsNullOrEmpty(column.Format)) {
						var cell = worksheet.GetCell(rowIndex + 1, columnIndex);
						cell.NumberFormat = column.Format;
					}
					rowValues[columnIndex] = value;
				}
				worksheet.AddRow(rowValues);
			}
			if (worker != null) {
				worker.ReportProgress(0, "Sending data to Excel...");
			}

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
			cell.Formula = "=" + String.Format(formula, data.Rows.Count + 2);
			cell.NumberFormat = format;
			cell.Font.Bold = true;
			return cell;
		}
	}
}
