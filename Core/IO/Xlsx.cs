using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DataDevelop.IO
{
	public class Xlsx
	{
		public static void Save(string fileName, string sheetName, IDataReader reader)
		{
			using (var workbook = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook)) {
				workbook.Write(sheetName, reader);
			}
		}

		public static void OpenInTemp(DataTable dataTable)
		{
			var sheetName = !string.IsNullOrEmpty(dataTable.TableName) ? dataTable.TableName : "Data";
			OpenInTemp(sheetName, dataTable);
		}

		public static void OpenInTemp(string sheetName, DataTable dataTable)
		{
			using (var reader = dataTable.CreateDataReader()) {
				OpenInTemp(sheetName, reader);
			}
		}

		public static void OpenInTemp(string sheetName, IDataReader reader)
		{
			var tempFile = Path.Combine(Path.GetTempPath(), $"DataDevelop-{DateTime.Now:yyMMddHHmmss}.xlsx");
			using (var workbook = SpreadsheetDocument.Create(tempFile, SpreadsheetDocumentType.Workbook)) {
				workbook.Write(sheetName, reader);
			}
			File.SetAttributes(tempFile, FileAttributes.ReadOnly);
			Process.Start(tempFile);
		}
	}

	static class ExcelExtensions
	{
		struct ExcelColumn
		{
			public int Index;
			public string Name;
			public CellValues CellValues;

			public ExcelColumn(int index, string name, Type dataType)
			{
				Index = index;
				Name = name;
				if (dataType == typeof(short) ||
					dataType == typeof(int) ||
					dataType == typeof(long) ||
					dataType == typeof(float) ||
					dataType == typeof(double) ||
					dataType == typeof(decimal)) {
					CellValues = CellValues.Number;
				} else if (dataType == typeof(bool)) {
					CellValues = CellValues.Boolean;
				} else if (dataType == typeof(DateTime)) {
					CellValues = CellValues.Date;
				} else {
					CellValues = CellValues.String;
				}
			}
		}

		private static Stylesheet GenerateStyleSheet()
		{
			return new Stylesheet(
				new Fonts(
					new Font(                                                               // Index 0 - The default font.
						new FontSize { Val = 11 },
						new Color { Rgb = new HexBinaryValue { Value = "000000" } },
						new FontName { Val = "Calibri" }),
					new Font(                                                               // Index 1 - The bold font.
						new Bold(),
						new FontSize { Val = 11 },
						new Color { Rgb = new HexBinaryValue() { Value = "000000" } },
						new FontName { Val = "Calibri" }),
					new Font(                                                               // Index 2 - The Italic font.
						new Italic(),
						new FontSize { Val = 11 },
						new Color { Rgb = new HexBinaryValue { Value = "000000" } },
						new FontName { Val = "Calibri" }),
					new Font(                                                               // Index 2 - The Times Roman font. with 16 size
						new FontSize { Val = 16 },
						new Color { Rgb = new HexBinaryValue { Value = "000000" } },
						new FontName { Val = "Times New Roman" })
				),
				new Fills(
					new Fill(                                                           // Index 0 - The default fill.
						new PatternFill { PatternType = PatternValues.None }),
					new Fill(                                                           // Index 1 - The default fill of gray 125 (required)
						new PatternFill { PatternType = PatternValues.Gray125 }),
					new Fill(                                                           // Index 2 - The yellow fill.
						new PatternFill(
							new ForegroundColor { Rgb = new HexBinaryValue { Value = "FFFFFF00" } }
						) { PatternType = PatternValues.Solid })
				),
				new Borders(
					new Border(                                                         // Index 0 - The default border.
						new LeftBorder(),
						new RightBorder(),
						new TopBorder(),
						new BottomBorder(),
						new DiagonalBorder()),
					new Border(                                                         // Index 1 - Applies a Left, Right, Top, Bottom border to a cell
						new LeftBorder(new Color { Auto = true }) { Style = BorderStyleValues.Thin },
						new RightBorder(new Color { Auto = true }) { Style = BorderStyleValues.Thin },
						new TopBorder(new Color { Auto = true }) { Style = BorderStyleValues.Thin },
						new BottomBorder(new Color { Auto = true }) { Style = BorderStyleValues.Thin },
						new DiagonalBorder())
				),
				new CellFormats(
					new CellFormat { FontId = 0, FillId = 0, BorderId = 0 },                          // Index 0 - The default cell style.  If a cell does not have a style index applied it will use this style combination instead
					new CellFormat { FontId = 1, FillId = 0, BorderId = 0, ApplyFont = true },       // Index 1 - Bold 
					new CellFormat { FontId = 2, FillId = 0, BorderId = 0, ApplyFont = true },       // Index 2 - Italic
					new CellFormat { FontId = 3, FillId = 0, BorderId = 0, ApplyFont = true },       // Index 3 - Times Roman
					new CellFormat { FontId = 0, FillId = 2, BorderId = 0, ApplyFill = true },       // Index 4 - Yellow Fill
					new CellFormat(                                                                   // Index 5 - Alignment
						new Alignment { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
					) { FontId = 0, FillId = 0, BorderId = 0, ApplyAlignment = true },
					new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true },      // Index 6 - Border
					new CellFormat { ApplyNumberFormat = true, NumberFormatId = 14 } // Index 7 - Date

				// NumberFormatId
				//0   General
				//1   0
				//2   0.00
				//3   #,##0
				//4   #,##0.00
				//9   0%
				//10  0.00%
				//11  0.00E+00
				//12  # ?/?
				//13  # ??/??
				//14  d/m/yyyy
				//15  d-mmm-yy
				//16  d-mmm
				//17  mmm-yy
				//18  h:mm tt
				//19  h:mm:ss tt
				//20  H:mm
				//21  H:mm:ss
				//22  m/d/yyyy H:mm
				//37  #,##0 ;(#,##0)
				//38  #,##0 ;[Red](#,##0)
				//39  #,##0.00;(#,##0.00)
				//40  #,##0.00;[Red](#,##0.00)
				//45  mm:ss
				//46  [h]:mm:ss
				//47  mmss.0
				//48  ##0.0E+0
				//49  @
				)
			); // return
		}

		public static void Write(this SpreadsheetDocument spreadsheet, string sheetName, IDataReader reader)
		{
			var fieldCount = reader.FieldCount;
			var columns = new List<ExcelColumn>(fieldCount);
			var rowRead = reader.Read();
			for (var index = 0; index < fieldCount; index++) {
				columns.Add(new ExcelColumn(index, reader.GetName(index), reader.GetFieldType(index)));
			}

			//create workbook part
			var wbp = spreadsheet.AddWorkbookPart();
			wbp.Workbook = new Workbook();
			wbp.Workbook.Append(new BookViews(new WorkbookView()));
			var sheets = wbp.Workbook.AppendChild<Sheets>(new Sheets());

			var stylesPart = wbp.AddNewPart<WorkbookStylesPart>();
			stylesPart.Stylesheet = GenerateStyleSheet();
			stylesPart.Stylesheet.Save();

			//create worksheet part, and add it to the sheets collection in workbook
			var wsp = wbp.AddNewPart<WorksheetPart>();
			var sheet = new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(wsp), SheetId = 1, Name = sheetName };
			sheets.Append(sheet);

			var sheetCount = 1;

			// Start Writing Values
			var writer = OpenXmlWriter.Create(wsp);
			writer.WriteStartElement(new Worksheet());

			WriteFreezeTopRow(writer);
			writer.WriteStartElement(new SheetData());
			WriteColumnsHeader(writer, columns);
			var sheetRowCount = 1;

			if (rowRead) {
				do {
					writer.WriteStartElement(new Row());
					foreach (var col in columns) {
						var cell = new Cell { DataType = col.CellValues };
						if (col.CellValues == CellValues.Date) {
							cell.DataType = CellValues.Number;
							cell.StyleIndex = 7; // Date (Stylesheet)
						}
						if (!reader.IsDBNull(col.Index)) {
							if (col.CellValues == CellValues.Date) {
								cell.CellValue = new CellValue(reader.GetDateTime(col.Index).ToOADate().ToString(CultureInfo.InvariantCulture));
							} else if (col.CellValues == CellValues.Boolean) {
								cell.CellValue = new CellValue(reader.GetBoolean(col.Index) ? "1" : "0");
							} else {
								var str = reader[col.Index].ToString().Replace("\a", "");
								cell.CellValue = new CellValue(str);
							}
						}
						writer.WriteElement(cell);
					}
					writer.WriteEndElement(); // end of Row

					sheetRowCount++;

					if (sheetRowCount == 1048576) { // MAX ROW

						writer.WriteEndElement(); //end of SheetData
						WriteAutoFilter(writer, columns.Count);
						writer.WriteEndElement(); //end of Worksheet
						writer.Close();

						sheetCount++;

						wsp = wbp.AddNewPart<WorksheetPart>();
						sheet = new Sheet { Id = spreadsheet.WorkbookPart.GetIdOfPart(wsp), SheetId = (uint)sheetCount, Name = sheetName + sheetCount.ToString() };
						sheets.Append(sheet);

						writer = OpenXmlWriter.Create(wsp);
						writer.WriteStartElement(new Worksheet());
						WriteFreezeTopRow(writer);
						writer.WriteStartElement(new SheetData());
						WriteColumnsHeader(writer, columns);

						sheetRowCount = 1;
					}
				} while (reader.Read());
			}

			writer.WriteEndElement(); //end of SheetData

			if (sheetRowCount > 0) {
				WriteAutoFilter(writer, columns.Count);
			}
			writer.WriteEndElement(); //end of worksheet
			writer.Close();
		}

		internal static string GetColumnName(int columnIndex)
		{
			var column = new StringBuilder();
			var numBase = 1 + 'Z' - 'A';
			var num = columnIndex;

			do {
				var digit = num % numBase;
				var symbol = (char)('A' + digit);
				column.Insert(0, symbol);
				num = num / numBase - 1;

			} while (num >= 0);

			return column.ToString();
		}

		private static void WriteAutoFilter(OpenXmlWriter writer, int columnCount)
		{
			var autoFilterRange = "A1:" + GetColumnName(columnCount - 1) + "1";
			writer.WriteElement(new AutoFilter { Reference = autoFilterRange });
		}

		private static void WriteFreezeTopRow(OpenXmlWriter writer)
		{
			var frozeCell = "A2";
			writer.WriteStartElement(new SheetViews());
			writer.WriteStartElement(new SheetView { TabSelected = true, WorkbookViewId = 0 });
			writer.WriteElement(new Pane { VerticalSplit = 1D, TopLeftCell = frozeCell, ActivePane = PaneValues.BottomLeft, State = PaneStateValues.Frozen });
			writer.WriteElement(new Selection { Pane = PaneValues.BottomLeft, ActiveCell = frozeCell, SequenceOfReferences = new ListValue<StringValue>(new[] { new StringValue(frozeCell) }) });
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		private static void WriteColumnsHeader(OpenXmlWriter writer, IEnumerable<ExcelColumn> columns)
		{
			writer.WriteStartElement(new Row());
			foreach (var column in columns) {
				var cell = new Cell {
					DataType = CellValues.String,
					CellValue = new CellValue(column.Name),
					StyleIndex = 1
				};
				writer.WriteElement(cell);
			}
			writer.WriteEndElement(); // end of Row
		}
	}
}
