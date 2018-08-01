using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace DataDevelop.Core.Excel
{
	public class Xlsx
	{
		public static void Save(string fileName, string sheetName, IDataReader reader)
		{
			using (var workbook = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook)) {
				workbook.Write(sheetName, reader);
			}
		}
	}

	static class ExcelExtensions
	{
		struct ColumnDef
		{
			public int Index;
			public string Name;
			public CellValues CellValues;

			public ColumnDef(int index, string name, Type dataType)
			{
				this.Index = index;
				this.Name = name;
				if (dataType == typeof(Int16) ||
					dataType == typeof(Int32) ||
					dataType == typeof(Int64) ||
					dataType == typeof(Single) ||
					dataType == typeof(Double) ||
					dataType == typeof(Decimal)) {
					this.CellValues = CellValues.Number;
				} else if (dataType == typeof(Boolean)) {
					this.CellValues = CellValues.Boolean;
				} else if (dataType == typeof(DateTime)) {
					this.CellValues = CellValues.Date;
				} else {
					this.CellValues = CellValues.String;
				}
			}
		}

		private static Stylesheet GenerateStyleSheet()
		{
			return new Stylesheet(
				new Fonts(
					new Font(                                                               // Index 0 - The default font.
						new FontSize() { Val = 11 },
						new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
						new FontName() { Val = "Calibri" }),
					new Font(                                                               // Index 1 - The bold font.
						new Bold(),
						new FontSize() { Val = 11 },
						new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
						new FontName() { Val = "Calibri" }),
					new Font(                                                               // Index 2 - The Italic font.
						new Italic(),
						new FontSize() { Val = 11 },
						new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
						new FontName() { Val = "Calibri" }),
					new Font(                                                               // Index 2 - The Times Roman font. with 16 size
						new FontSize() { Val = 16 },
						new Color() { Rgb = new HexBinaryValue() { Value = "000000" } },
						new FontName() { Val = "Times New Roman" })
				),
				new Fills(
					new Fill(                                                           // Index 0 - The default fill.
						new PatternFill() { PatternType = PatternValues.None }),
					new Fill(                                                           // Index 1 - The default fill of gray 125 (required)
						new PatternFill() { PatternType = PatternValues.Gray125 }),
					new Fill(                                                           // Index 2 - The yellow fill.
						new PatternFill(
							new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFFFF00" } }
						)
						{ PatternType = PatternValues.Solid })
				),
				new Borders(
					new Border(                                                         // Index 0 - The default border.
						new LeftBorder(),
						new RightBorder(),
						new TopBorder(),
						new BottomBorder(),
						new DiagonalBorder()),
					new Border(                                                         // Index 1 - Applies a Left, Right, Top, Bottom border to a cell
						new LeftBorder(
							new Color() { Auto = true }
						)
						{ Style = BorderStyleValues.Thin },
						new RightBorder(
							new Color() { Auto = true }
						)
						{ Style = BorderStyleValues.Thin },
						new TopBorder(
							new Color() { Auto = true }
						)
						{ Style = BorderStyleValues.Thin },
						new BottomBorder(
							new Color() { Auto = true }
						)
						{ Style = BorderStyleValues.Thin },
						new DiagonalBorder())
				),
				new CellFormats(
					new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 },                          // Index 0 - The default cell style.  If a cell does not have a style index applied it will use this style combination instead
					new CellFormat() { FontId = 1, FillId = 0, BorderId = 0, ApplyFont = true },       // Index 1 - Bold 
					new CellFormat() { FontId = 2, FillId = 0, BorderId = 0, ApplyFont = true },       // Index 2 - Italic
					new CellFormat() { FontId = 3, FillId = 0, BorderId = 0, ApplyFont = true },       // Index 3 - Times Roman
					new CellFormat() { FontId = 0, FillId = 2, BorderId = 0, ApplyFill = true },       // Index 4 - Yellow Fill
					new CellFormat(                                                                   // Index 5 - Alignment
						new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center }
					)
					{ FontId = 0, FillId = 0, BorderId = 0, ApplyAlignment = true },
					new CellFormat() { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true },      // Index 6 - Border
					new CellFormat() { ApplyNumberFormat = true, NumberFormatId = 14 } // Index 7 - Date

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
			var columns = new List<ColumnDef>();
			var rowRead = reader.Read();
			var fieldCount = reader.FieldCount;
			for (var index = 0; index < fieldCount; index++) {
				columns.Add(new ColumnDef(index, reader.GetName(index), reader.GetFieldType(index)));
			}

			//create workbook part
			WorkbookPart wbp = spreadsheet.AddWorkbookPart();
			wbp.Workbook = new Workbook();
			wbp.Workbook.Append(new BookViews(new WorkbookView()));
			Sheets sheets = wbp.Workbook.AppendChild<Sheets>(new Sheets());

			var stylesPart = wbp.AddNewPart<WorkbookStylesPart>();
			stylesPart.Stylesheet = GenerateStyleSheet();
			stylesPart.Stylesheet.Save();

			//create worksheet part, and add it to the sheets collection in workbook
			WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
			Sheet sheet = new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(wsp), SheetId = 1, Name = sheetName };
			sheets.Append(sheet);

			var sheetCount = 1;

			// Start Writing Values
			OpenXmlWriter writer = OpenXmlWriter.Create(wsp);
			writer.WriteStartElement(new DocumentFormat.OpenXml.Spreadsheet.Worksheet());

			WriteFreezeTopRow(writer);
			writer.WriteStartElement(new SheetData());
			WriteColumnsHeader(writer, columns);
			var sheetRowCount = 1;

			if (rowRead) {
				do {
					writer.WriteStartElement(new Row());
					foreach (var col in columns) {
						var cell = new Cell();
						if (!reader.IsDBNull(col.Index)) {
							if (col.CellValues == CellValues.Date) {
								cell.DataType = CellValues.Number;
								cell.CellValue = new CellValue(reader.GetDateTime(col.Index).ToOADate().ToString(CultureInfo.InvariantCulture));
								cell.StyleIndex = 7; // Date (Stylesheet)
							} else {
								cell.DataType = col.CellValues;
								cell.CellValue = new CellValue(reader[col.Index].ToString());
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
						sheet = new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(wsp), SheetId = (uint)sheetCount, Name = sheetName + sheetCount.ToString() };
						sheets.Append(sheet);

						writer = OpenXmlWriter.Create(wsp);
						writer.WriteStartElement(new DocumentFormat.OpenXml.Spreadsheet.Worksheet());
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
			var numBase = (int)'Z' - (int)'A' + 1;
			var num = columnIndex;

			do {
				var digit = num % numBase;
				var symbol = (char)((int)'A' + digit);
				column.Insert(0, symbol);
				num = num / numBase - 1;

			} while (num >= 0);

			return column.ToString();
		}

		private static void WriteAutoFilter(OpenXmlWriter writer, int columnCount)
		{
			var autoFilterRange = "A1:" + GetColumnName(columnCount - 1) + "1";
			writer.WriteElement(new AutoFilter() { Reference = autoFilterRange });
		}

		private static void WriteFreezeTopRow(OpenXmlWriter writer)
		{
			var frozeCell = "A2";
			writer.WriteStartElement(new SheetViews());
			writer.WriteStartElement(new SheetView() { TabSelected = true, WorkbookViewId = 0 });
			writer.WriteElement(new Pane() { VerticalSplit = 1D, TopLeftCell = frozeCell, ActivePane = PaneValues.BottomLeft, State = PaneStateValues.Frozen });
			writer.WriteElement(new Selection() { Pane = PaneValues.BottomLeft, ActiveCell = frozeCell, SequenceOfReferences = new ListValue<StringValue>(new[] { new StringValue(frozeCell) }) });
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		private static void WriteColumnsHeader(OpenXmlWriter writer, List<ColumnDef> columns)
		{
			writer.WriteStartElement(new Row());
			foreach (var column in columns) {
				var cell = new Cell() { DataType = CellValues.String, CellValue = new CellValue(column.Name) };
				cell.StyleIndex = 1;
				writer.WriteElement(cell);
			}
			writer.WriteEndElement(); // end of Row
		}
	}
}
