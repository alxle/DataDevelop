using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.ComponentModel;

namespace DataDevelop.Printing
{
	class DataTablePrintDocument : PrintDocument
	{
		const int padding = 2;
		float[] columnWidths = null;
		int currentRow = 0;
		int yOffset = 0;
		int rowHeight = 0;

		Font f;
		DataTable dataTable = null;

		public DataTablePrintDocument(DataTable table, Font font)
		{
			this.DataTable = table;
			this.Font = font;
		}

		public DataTablePrintDocument(DataTable table)
			: this(table, null)
		{
		}

		public DataTablePrintDocument()
			: this(null, null)
		{
		}

		public DataTable DataTable
		{
			get { return this.dataTable; }
			set
			{
				if (!object.ReferenceEquals(this.dataTable, value)) {
					this.dataTable = value;
					this.columnWidths = null;
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(null)]
		public Font Font
		{
			get { return this.f; }
			set
			{
				if (value == null) {
					this.f = new Font(FontFamily.GenericMonospace, 8F);
				} else {
					this.f = value;
				}
			}
		}

		private void MeasureWidths(Graphics g, bool includeHeader)
		{
			columnWidths = new float[dataTable.Columns.Count];

			if (includeHeader) {
				Font headerFont = new Font(this.Font, FontStyle.Bold);
				for (int i = 0; i < dataTable.Columns.Count; i++) {
					SizeF size = g.MeasureString(dataTable.Columns[i].ColumnName, headerFont);
					columnWidths[i] = size.Width;
				}
			}

			for (int i = 0; i < dataTable.Columns.Count; i++) {
				for (int j = 0; j < dataTable.Rows.Count; j++) {
					if (dataTable.Rows[j].RowState == DataRowState.Deleted) continue;
					SizeF size = g.MeasureString(dataTable.Rows[j][i].ToString(), f);
					if (size.Width > columnWidths[i]) columnWidths[i] = size.Width;
					if (rowHeight == 0) rowHeight = (int)Math.Ceiling(size.Height);
				}
			}
		}

		private void DrawSeparator(Graphics g, Rectangle marginBounds)
		{
			g.DrawLine(Pens.Black, marginBounds.X, yOffset + marginBounds.Y, marginBounds.X + marginBounds.Width, yOffset + marginBounds.Y);
			this.yOffset += padding;
		}

		private void DrawCurrentRow(Graphics g, Rectangle marginBounds)
		{
			float xOffset = 0;

			for (int j = 0; j < dataTable.Columns.Count; j++) {
				PointF p = new PointF(xOffset + marginBounds.X, yOffset + marginBounds.Y);
				if (p.X + columnWidths[j] > marginBounds.Right) {
					break;
				}
				g.DrawLine(Pens.Black, p.X, p.Y - padding, p.X, p.Y + rowHeight + padding);
				p.X += padding / 2;
				g.DrawString(dataTable.Rows[currentRow][j].ToString(), f, Brushes.Black, p);
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

			Font headerFont = new Font(this.Font, FontStyle.Bold);

			for (int j = 0; j < dataTable.Columns.Count; j++) {
				PointF p = new PointF(xOffset + marginBounds.X, yOffset + marginBounds.Y);
				if (p.X + columnWidths[j] > marginBounds.Right) {
					break;
				}
				g.DrawLine(Pens.Black, p.X, p.Y - padding, p.X, p.Y + rowHeight + padding);
				p.X += 2;
				g.DrawString(dataTable.Columns[j].ColumnName, headerFont, Brushes.Black, p);
				xOffset += columnWidths[j] + padding;
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

			if (currentRow < dataTable.Rows.Count) {
				e.HasMorePages = true;
				yOffset = 0;
			}

		}

	}
}
