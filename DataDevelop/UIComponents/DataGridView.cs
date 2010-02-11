using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataDevelop.UIComponents;

namespace DataDevelop
{
	public partial class DataGridView : System.Windows.Forms.DataGridView
	{
		private int startRowNumber = 1;

		public DataGridView()
		{
			InitializeComponent();
			//nullStyle = new DataGridViewCellStyle();
			//nullStyle.BackColor = SystemColors.Info;
			//nullStyle.ForeColor = SystemColors.InfoText;
			//this.DefaultCellStyle.NullValue = "NULL";

			this.resizeColumnsAllCellsMenuItem.Click += delegate { this.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells); };
			this.resizeColumnsAllCellsExceptHeaderMenuItem.Click += delegate { this.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader); };
			this.resizeColumnsColumnHeaderMenuItem.Click += delegate { this.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader); };

			this.resizeRowsAllCellsMenuItem.Click += delegate { this.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells); };
			this.resizeRowsRowHeaderMenuItem.Click += delegate { this.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllHeaders); };
			
			this.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
		}

		[Browsable(false)]
		public ContextMenuStrip MainMenu
		{
			get { return this.mainContextMenuStrip; }
		}

		[Browsable(false)]
		public ContextMenuStrip AutoResizeColumnsMenu
		{
			get { return this.autoSizeColumnsContextMenuStrip; }
		}

		[Browsable(false)]
		public ContextMenuStrip AutoResizeRowsMenu
		{
			get { return this.autoSizeRowsContextMenuStrip; }
		}

		[Browsable(false)]
		public int StartRowNumber
		{
			get { return startRowNumber; }
			set { startRowNumber = value; }
		}

		#region EditProgramatically

		protected override void OnCellEnter(DataGridViewCellEventArgs e)
		{
			if (this.Focused) {
				if (!this.CurrentRow.Selected && !this.CurrentCell.IsInEditMode) {
					this.BeginEdit(true);
				}
			}
			
			base.OnCellEnter(e);
		}

		protected override void OnCellClick(DataGridViewCellEventArgs e)
		{
			if (this.Focused) {
				if (e.ColumnIndex > 0 && e.ColumnIndex < this.ColumnCount &&
					e.RowIndex > 0 && e.RowIndex < this.RowCount) {
					if (!this[e.ColumnIndex, e.RowIndex].IsInEditMode) {
						this.BeginEdit(true);
					}
				}
			}

			base.OnCellClick(e);
		}

		protected override void OnSelectionChanged(EventArgs e)
		{
			if (this.CurrentRow == null || this.CurrentCell == null) {
				return;
			}
			if (this.CurrentRow.Selected) {
				if (this.CurrentCell.IsInEditMode) {
					this.EndEdit();
				}
			}

			base.OnSelectionChanged(e);
		}

		#endregion

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right) {
				HitTestInfo info = this.HitTest(e.X, e.Y);
				if (info.Type == DataGridViewHitTestType.Cell) {
					if (!this.Rows[info.RowIndex].Selected) {
						this.ClearSelection();
						this.Rows[info.RowIndex].Selected = true;
					}
				}
			}
		}

		sealed class NullStyle : IDisposable
		{
			private static NullStyle _default;

			public static NullStyle Default
			{
				get
				{
					if (_default == null) {
						_default = new NullStyle();
					}
					return _default;
				}
				set { _default = value; }
			}

			private Font font;

			public Font Font
			{
				get { return font; }
				set { font = value; }
			}

			private StringFormat format;

			public StringFormat StringFormat
			{
				get { return format; }
				set { format = value; }
			}

			private SolidBrush back;

			public Color BackColor
			{
				get { return back.Color; }
				set { back.Color = value; }
			}

			private SolidBrush fore;

			public Color ForeColor
			{
				get { return fore.Color; }
				set { fore.Color = value; }
			}

			public Brush ForeBrush
			{
				get { return fore; }
			}

			public NullStyle()
			{
				this.font = new Font(SystemFonts.DefaultFont, FontStyle.Italic);
				this.format = new StringFormat();
				this.format.LineAlignment = StringAlignment.Center;
				this.format.Trimming = StringTrimming.EllipsisCharacter;
				this.format.FormatFlags |= StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
				this.back = new SolidBrush(SystemColors.Info);
				this.fore = new SolidBrush(SystemColors.InfoText);
			}

			#region IDisposable Members

			public void Dispose()
			{
				if (back != null) {
					back.Dispose();
				}
				if (fore != null) {
					fore.Dispose();
				}
				GC.SuppressFinalize(this);
			}

			#endregion
		}

		protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
			if (this.Rows[e.RowIndex].IsNewRow) return;
			//DataGridViewCell cell = this[e.ColumnIndex, e.RowIndex];
			if (e.Value == null || e.Value == DBNull.Value) {
				bool isSelected = (e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
				if (isSelected) {
					e.PaintBackground(e.CellBounds, true);
				} else {
					e.Graphics.FillRectangle(SystemBrushes.Info, e.CellBounds);
				}
				NullStyle style = NullStyle.Default;
				e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Background);
				Type cellType = this.Columns[e.ColumnIndex].CellType;
				if (cellType == typeof(DataGridViewTextBoxCell) || cellType.IsSubclassOf(typeof(DataGridViewTextBoxCell))) {
					e.Graphics.DrawString("NULL", style.Font, (isSelected) ? new SolidBrush(DefaultCellStyle.SelectionForeColor) : style.ForeBrush, e.CellBounds, style.StringFormat);
				}
				e.Handled = true;
			}
			base.OnCellPainting(e);
		}

		private void DataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			if (e.Exception is ArgumentException && Columns[e.ColumnIndex] is DataGridViewImageColumn) {
				Columns[e.ColumnIndex].CellTemplate = new DataDevelop.UIComponents.DataGridViewBinaryCell();
			} else {
				this[e.ColumnIndex, e.RowIndex].ErrorText = e.Exception.Message;
				//e.ThrowException = false;
			}
		}

		private void DataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
		{
			if (e.Column is DataGridViewImageColumn) {
				e.Column.CellTemplate = new DataDevelop.UIComponents.DataGridViewBinaryCell();
			} else if (e.Column.ValueType == typeof(string)) {
				e.Column.CellTemplate = new TextCell();
			} else if (e.Column.ValueType == typeof(DateTime)) {
				////e.Column.CellTemplate = new SQLiteStudio.UIComponents.CalendarCell();
				e.Column.CellTemplate = new DateTimeCell();
			}
		}

		protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
		{
			//store a string representation of the row number in 'strRowNumber'
			string strRowNumber = (startRowNumber + e.RowIndex).ToString();

			//prepend leading zeros to the string if necessary to improve
			//appearance. For example, if there are ten rows in the grid,
			//row seven will be numbered as "07" instead of "7". Similarly, if 
			//there are 100 rows in the grid, row seven will be numbered as "007".
			while (strRowNumber.Length < this.RowCount.ToString().Length) strRowNumber = "0" + strRowNumber;

			//determine the display size of the row number string using
			//the DataGridView's current font.
			SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);

			//adjust the width of the column that contains the row header cells 
			//if necessary
			if (this.RowHeadersWidth < (int)(size.Width + 20)) this.RowHeadersWidth = (int)(size.Width + 20);

			//this brush will be used to draw the row number string on the
			//row header cell using the system's current ControlText color
			Brush b = SystemBrushes.ControlText;

			//draw the row number string on the current row header cell using
			//the brush defined above and the DataGridView's default font
			e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

			base.OnRowPostPaint(e);
		}

		private class TextCell : DataGridViewTextBoxCell
		{
			protected override void OnDoubleClick(DataGridViewCellEventArgs e)
			{
				base.OnDoubleClick(e);
				using (TextVisualizer dialog = new TextVisualizer()) {
					dialog.TextValue = this.Value as string;
					dialog.ReadOnly = this.DataGridView.ReadOnly || this.OwningColumn.ReadOnly;
					dialog.ShowDialog(null);
					if (dialog.TextValueChanged) {
						this.Value = dialog.TextValue;
						this.DataGridView.EndEdit();
					}
				}
			}
		}

		private class DateTimeCell : DataGridViewTextBoxCell
		{
			protected override void OnDoubleClick(DataGridViewCellEventArgs e)
			{
				base.OnDoubleClick(e);
				if (!this.DataGridView.ReadOnly && !this.OwningColumn.ReadOnly) {
					using (DateTimeVisualizer dialog = new DateTimeVisualizer()) {
						dialog.Value = this.Value as DateTime?;
						dialog.ShowDialog(null);
						if (dialog.ValueChanged) {
							if (dialog.Value == null) {
								this.Value = DBNull.Value;
							} else {
								this.Value = dialog.Value.Value;
							}
							this.DataGridView.EndEdit();
						}
					}
				}
			}
		}
	}
}
