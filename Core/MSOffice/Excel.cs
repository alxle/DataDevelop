using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using ExcelApplication = Microsoft.Office.Interop.Excel.Application;
using ExcelWorkbook = Microsoft.Office.Interop.Excel.Workbook;
using ExcelWorksheet = Microsoft.Office.Interop.Excel.Worksheet;
using System.Runtime.InteropServices;

namespace DataDevelop.Core.MSOffice
{
	public enum Agregate
	{
		None, Count, Sum, Average, Max, Min
	}

	public class ColumnDef
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

	public class Worksheet : IDisposable
	{
		private ExcelApplication app;
		private ExcelWorkbook book;
		private ExcelWorksheet sheet;
		private int columns;
		private int rows;

		internal Worksheet(ExcelApplication app, ExcelWorkbook book, ExcelWorksheet sheet, int columns, int rows)
		{
			this.app = app;
			this.book = book;
			this.sheet = sheet;
			this.columns = columns;
			this.rows = rows;
		}

		internal Range this[int rowIndex, int columnIndex]
		{
			get { return Excel.GetCell(this.sheet, rowIndex, columnIndex); }
			set { Excel.SetCell(this.sheet, rowIndex, columnIndex, value); }
		}

		public void OpenInExcel()
		{
			this.sheet.Application.UserControl = true;
			this.sheet.Application.Visible = true;
		}

		public void SaveAs(string fileName)
		{
			this.book.SaveAs(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
				XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
		}

		public void Close()
		{
			this.app.Quit();
		}

		#region IDisposable Members

		public void Dispose()
		{
			this.app.Quit();
			Marshal.FinalReleaseComObject(this.app);
		}

		#endregion
	}
	
	public static class Excel
	{
		public static Worksheet CreateWorksheet(string caption, DataTable data, BackgroundWorker worker)
		{
			var columns = new List<ColumnDef>(data.Columns.Count);
			foreach (DataColumn column in data.Columns) {
				columns.Add(new ColumnDef(column.ColumnName, column, "", Agregate.None));
			}
			return CreateWorksheet(caption, data, columns, worker);
		}

		public static Worksheet CreateWorksheet(string caption, DataTable data, IList<ColumnDef> columns, BackgroundWorker worker)
		{
			if (worker != null) {
				worker.ReportProgress(0, "Initializing...");
			}
			var watch = new System.Diagnostics.Stopwatch();
			watch.Start();
			var excel = new ExcelApplication();
			var book = excel.Workbooks.Add(Missing.Value);
			var sheet = (ExcelWorksheet)book.Worksheets[1];
			
			excel.Visible = (worker == null);
			excel.Caption = caption;
			if (!String.IsNullOrEmpty(data.TableName)) {
				sheet.Name = data.TableName;
			}
			excel.Cursor = XlMousePointer.xlWait;
			
			var titles = new List<object>();
			for (int i = 0; i < columns.Count; i++) {
				//sheet.Cells[1, i + 1] = columns[i].Title;
				titles.Add(columns[i].Title);
			}

			sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, columns.Count]).FormulaArray = titles.ToArray();
				//.set_Value(Missing.Value, titles.ToArray());

			Range headers = sheet.get_Range(sheet.Cells[1, 1], sheet.Cells[1, columns.Count]);
			headers.Font.Bold = true;

			GetCell(sheet, 2, 1).Select();
			excel.ActiveWindow.FreezePanes = true;
			
			var rowValues = new object[columns.Count];
			//var tableValues = new object[data.Rows.Count + 1, columns.Count];

			var chrono = new System.Diagnostics.Stopwatch();
			if (worker != null) {
				chrono.Start();
			}
			for (int rowIndex = 0; rowIndex < data.Rows.Count + 1; rowIndex++) {
				
				if (worker != null) {
					var milliseconds = chrono.ElapsedMilliseconds;
					if (chrono.ElapsedMilliseconds - milliseconds > 200) {
						worker.ReportProgress(100 * rowIndex / data.Rows.Count, String.Format("Exporting Row ({0} of {1})", rowIndex, data.Rows.Count));
					}
					if (worker.CancellationPending) {
						excel.DisplayAlerts = false;
						excel.ActiveWindow.Close(false, Missing.Value, Missing.Value);
						excel.Quit();
						Marshal.FinalReleaseComObject(excel);
						return null;
					}
				}
				
				for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++) {
					var column = columns[columnIndex];
					//var cell = (Range)sheet.Cells[rowIndex + 2, columnIndex + 1];
					object value = null;
					
					if (rowIndex == data.Rows.Count) {
						var cell = (Range)sheet.Cells[rowIndex + 2, columnIndex + 1];
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
								value = "'" + (string)value;
							}
							var error = data.Rows[rowIndex].GetColumnError(column.Column.Ordinal);
							if (!String.IsNullOrEmpty(error)) {
								var cell = (Range)sheet.Cells[rowIndex + 2, columnIndex + 1];
								cell.NoteText(error, Missing.Value, Missing.Value);
							}
						}
					}
					//cell.Formula = value ?? String.Empty;
					if (!String.IsNullOrEmpty(column.Format)) {
						var cell = (Range)sheet.Cells[rowIndex + 2, columnIndex + 1];
						cell.NumberFormat = column.Format;
					}
					////cell.Auto = false;
					rowValues[columnIndex] = value;
					//tableValues[rowIndex, columnIndex] = value;
				}
				SetRow(sheet, rowIndex + 2, 1, rowValues);
			}
			if (worker != null) {
				worker.ReportProgress(0, "Sending data to Excel...");
			}
			//SetTable(sheet, 2, 1, tableValues);
			////headers.EntireColumn.AutoFit();
			excel.Cursor = XlMousePointer.xlDefault;
			watch.Stop();
			Console.WriteLine("Elapsed: {0}", watch.Elapsed);
			return new Worksheet(excel, book, sheet, data.Columns.Count, data.Rows.Count);
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
