using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;

namespace DataDevelop.Printing
{
	class DataTablePrintDocument : PrintDocument
	{
		const int padding = 2;
		float[] columnWidths = null;
		int currentRow = 0;
		int yOffset = 0;
		int rowHeight = 0;
		int lastColumn = 0;

		int pageFirstRow = 0;
		int pageFirstColumn = 0;

		Font f;
		DataTable dataTable = null;

		public DataTablePrintDocument(DataTable table = null, Font font = null)
		{
			DataTable = table;
			Font = font;
		}

		public DataTable DataTable
		{
			get => dataTable;
			set
			{
				if (!ReferenceEquals(dataTable, value)) {
					dataTable = value;
					columnWidths = null;
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(null)]
		public Font Font
		{
			get => f;
			set => f = value ?? new Font(FontFamily.GenericMonospace, 8F);
		}

		private void MeasureWidths(Graphics g, bool includeHeader)
		{
			columnWidths = new float[dataTable.Columns.Count];

			if (includeHeader) {
				var headerFont = new Font(Font, FontStyle.Bold);
				for (var i = 0; i < dataTable.Columns.Count; i++) {
					var size = g.MeasureString(dataTable.Columns[i].ColumnName, headerFont);
					columnWidths[i] = size.Width;
				}
			}

			for (var i = 0; i < dataTable.Columns.Count; i++) {
				for (var j = 0; j < dataTable.Rows.Count; j++) {
					if (dataTable.Rows[j].RowState == DataRowState.Deleted) continue;
					var value = GetFirstLine(dataTable.Rows[j][i].ToString());
					var size = g.MeasureString(value, f);
					if (size.Width > columnWidths[i]) columnWidths[i] = size.Width;
					if (rowHeight == 0) rowHeight = (int)Math.Ceiling(size.Height);
				}
			}
		}

		private void DrawSeparator(Graphics g, Rectangle marginBounds)
		{
			g.DrawLine(Pens.Black, marginBounds.X, yOffset + marginBounds.Y, marginBounds.X + marginBounds.Width, yOffset + marginBounds.Y);
			yOffset += padding;
		}

		private static string GetFirstLine(string value)
		{
			if (value.Contains("\n")) {
				value = value.Substring(0, value.IndexOf("\n")) + "...";
			}
			return value;
		}

		private void DrawCurrentRow(Graphics g, Rectangle marginBounds)
		{
			float xOffset = 0;

			for (var j = pageFirstColumn; j < dataTable.Columns.Count; j++) {
				var p = new PointF(xOffset + marginBounds.X, yOffset + marginBounds.Y);
				if (p.X + columnWidths[j] > marginBounds.Right) {
					break;
				}
				g.DrawLine(Pens.Black, p.X, p.Y - padding, p.X, p.Y + rowHeight + padding);
				p.X += padding / 2;
				var value = GetFirstLine(dataTable.Rows[currentRow][j].ToString());
				g.DrawString(value, f, Brushes.Black, p);
				xOffset += columnWidths[j] + padding;
			}

			g.DrawLine(Pens.Black, marginBounds.Right, yOffset - padding + marginBounds.Y,
				marginBounds.Right, yOffset + rowHeight + padding + marginBounds.Y);

			yOffset += rowHeight + padding;
			DrawSeparator(g, marginBounds);
		}

		private void DrawHeaderRow(Graphics g, Rectangle marginBounds, Font font)
		{
			float xOffset = 0;
			DrawSeparator(g, marginBounds);

			var headerFont = font ?? new Font(Font, FontStyle.Bold);

			for (var j = pageFirstColumn; j < dataTable.Columns.Count; j++) {
				var p = new PointF(xOffset + marginBounds.X, yOffset + marginBounds.Y);
				if (p.X + columnWidths[j] > marginBounds.Right) {
					break;
				}
				g.DrawLine(Pens.Black, p.X, p.Y - padding, p.X, p.Y + rowHeight + padding);
				p.X += 2;
				g.DrawString(dataTable.Columns[j].ColumnName, headerFont, Brushes.Black, p);
				xOffset += columnWidths[j] + padding;
				lastColumn = j;
			}

			g.DrawLine(Pens.Black, marginBounds.Right, yOffset - padding + marginBounds.Y,
				marginBounds.Right, yOffset + rowHeight + padding + marginBounds.Y);

			yOffset += rowHeight + padding;
			DrawSeparator(g, marginBounds);
			yOffset += padding;
		}

		protected override void OnBeginPrint(PrintEventArgs e)
		{
			if (dataTable == null) {
				throw new InvalidOperationException("Cannot print with a null DataTable");
			}
			base.OnBeginPrint(e);
			columnWidths = null;
			currentRow = 0;
			yOffset = 0;
			pageFirstRow = 0;
			pageFirstColumn = 0;
		}

		protected override void OnPrintPage(PrintPageEventArgs e)
		{
			base.OnPrintPage(e);

			if (columnWidths == null) {
				MeasureWidths(e.Graphics, true);
			}

			while (currentRow < dataTable.Rows.Count && yOffset + rowHeight + 8 <= e.MarginBounds.Height) {
				if (dataTable.Rows[currentRow].RowState != DataRowState.Deleted) {
					if (yOffset == 0) {
						DrawHeaderRow(e.Graphics, e.MarginBounds, new Font(f, FontStyle.Bold));
						if (dataTable.Rows.Count > 0) {
							DrawSeparator(e.Graphics, e.MarginBounds);
						}
					}
					DrawCurrentRow(e.Graphics, e.MarginBounds);
				}
				currentRow++;
			}

			if (lastColumn + 1 < dataTable.Columns.Count) {
				e.HasMorePages = true;
				pageFirstColumn = lastColumn + 1;
				currentRow = pageFirstRow;
				yOffset = 0;
				return;
			}

			if (currentRow < dataTable.Rows.Count) {
				e.HasMorePages = true;
				pageFirstColumn = 0;
				pageFirstRow = currentRow;
				yOffset = 0;
			}
		}
	}
}
