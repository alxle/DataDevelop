using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DataDevelop.UIComponents;

namespace DataDevelop
{
	public partial class DataGridView : System.Windows.Forms.DataGridView
	{
		public DataGridView()
		{
			InitializeComponent();

			DoubleBuffered = true;

			resizeColumnsAllCellsMenuItem.Click += delegate { AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells); };
			resizeColumnsAllCellsExceptHeaderMenuItem.Click += delegate { AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader); };
			resizeColumnsColumnHeaderMenuItem.Click += delegate { AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader); };

			resizeRowsAllCellsMenuItem.Click += delegate { AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells); };
			resizeRowsRowHeaderMenuItem.Click += delegate { AutoResizeRows(DataGridViewAutoSizeRowsMode.AllHeaders); };

			//this.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
			NullCellStyle = GetDefaultNullStyle();
		}

		[Browsable(false)]
		public ContextMenuStrip MainMenu => mainContextMenuStrip;

		[Browsable(false)]
		public ContextMenuStrip AutoResizeColumnsMenu => autoSizeColumnsContextMenuStrip;

		[Browsable(false)]
		public ContextMenuStrip AutoResizeRowsMenu => autoSizeRowsContextMenuStrip;

		[Browsable(false)]
		public int StartRowNumber { get; set; } = 1;

		[Browsable(false)]
		public DataGridViewCellStyle NullCellStyle { get; set; }

		public void SetDarkMode()
		{
			BackgroundColor = VisualStyles.DarkThemeColors.Background;
			GridColor = VisualStyles.DarkThemeColors.BorderLight;
			BorderStyle = BorderStyle.FixedSingle;
			ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

			DefaultCellStyle = new DataGridViewCellStyle {
				BackColor = VisualStyles.DarkThemeColors.Background,
				ForeColor = VisualStyles.DarkThemeColors.TextColor,
			};

			EnableHeadersVisualStyles = false;
			ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle {
				BackColor = VisualStyles.DarkThemeColors.CellBackColor,
				ForeColor = VisualStyles.DarkThemeColors.TextColor,
			};
			RowHeadersDefaultCellStyle = ColumnHeadersDefaultCellStyle;

			NullCellStyle.ForeColor = VisualStyles.DarkThemeColors.TextColor;
			NullCellStyle.BackColor = VisualStyles.DarkThemeColors.NullBackColor;
		}

		private DataGridViewCellStyle GetDefaultNullStyle()
		{
			var style = new DataGridViewCellStyle {
				Font = new Font(SystemFonts.DefaultFont, FontStyle.Italic),
				BackColor = SystemColors.Info,
				ForeColor = SystemColors.InfoText,
			};
			var stringFormat = new StringFormat {
				LineAlignment = StringAlignment.Center,
				Trimming = StringTrimming.EllipsisCharacter
			};
			stringFormat.FormatFlags |= StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
			style.Tag = stringFormat;
			return style;
		}

		#region EditProgramatically

		protected override void OnCellEnter(DataGridViewCellEventArgs e)
		{
			if (Focused) {
				if (!CurrentRow.Selected && !CurrentCell.IsInEditMode) {
					BeginEdit(true);
				}
			}

			base.OnCellEnter(e);
		}

		protected override void OnCellClick(DataGridViewCellEventArgs e)
		{
			if (Focused) {
				if (e.ColumnIndex > 0 && e.ColumnIndex < ColumnCount &&
					e.RowIndex > 0 && e.RowIndex < RowCount) {
					if (!this[e.ColumnIndex, e.RowIndex].IsInEditMode) {
						BeginEdit(true);
					}
				}
			}

			base.OnCellClick(e);
		}

		protected override void OnSelectionChanged(EventArgs e)
		{
			if (CurrentRow == null || CurrentCell == null) {
				return;
			}
			if (CurrentRow.Selected) {
				if (CurrentCell.IsInEditMode) {
					EndEdit();
				}
			}

			base.OnSelectionChanged(e);
		}

		#endregion

		protected override void OnMouseClick(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right) {
				var info = HitTest(e.X, e.Y);
				if (info.Type == DataGridViewHitTestType.Cell) {
					if (!Rows[info.RowIndex].Selected) {
						ClearSelection();
						Rows[info.RowIndex].Selected = true;
					}
				}
			}
		}

		protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
			if (Rows[e.RowIndex].IsNewRow) return;
			//DataGridViewCell cell = this[e.ColumnIndex, e.RowIndex];
			if (e.Value == null || e.Value == DBNull.Value) {
				var style = NullCellStyle;
				var isSelected = (e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
				if (isSelected) {
					e.PaintBackground(e.CellBounds, true);
				} else {
					using (var brush = new SolidBrush(style.BackColor)) {
						e.Graphics.FillRectangle(brush, e.CellBounds);
					}
				}
				e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Background);
				var cellType = Columns[e.ColumnIndex].CellType;
				if (cellType == typeof(DataGridViewTextBoxCell) || cellType.IsSubclassOf(typeof(DataGridViewTextBoxCell))) {
					using (var brush = new SolidBrush(style.ForeColor)) {
						var stringFormat = (StringFormat)style.Tag;
						e.Graphics.DrawString("NULL", style.Font, brush, e.CellBounds, stringFormat);
					}
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
			var strRowNumber = (StartRowNumber + e.RowIndex).ToString();
			var strLastRowNumber = (StartRowNumber + Rows.Count - 1).ToString();
			if (strRowNumber.Length < strLastRowNumber.Length) {
				strRowNumber = strRowNumber.PadLeft(strLastRowNumber.Length, '0');
			}

			var size = e.Graphics.MeasureString(strRowNumber, Font);

			if (RowHeadersWidth < (int)(size.Width + 20)) {
				RowHeadersWidth = (int)(size.Width + 20);
			}

			using (var b = new SolidBrush(e.InheritedRowStyle.ForeColor)) {
				e.Graphics.DrawString(strRowNumber, e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
			}
			base.OnRowPostPaint(e);
		}

		private void DeleteRowsMenuItem_Click(object sender, EventArgs e)
		{
			ProcessDeleteKey(Keys.Delete);
		}

		private class TextCell : DataGridViewTextBoxCell
		{
			protected override void OnDoubleClick(DataGridViewCellEventArgs e)
			{
				base.OnDoubleClick(e);
				var mouse = MousePosition;
				var cell = DataGridView[e.ColumnIndex, e.RowIndex];
				if (cell.IsInEditMode) {
					DataGridView.EndEdit();
				}
				using (var dialog = new TextVisualizer()) {
					dialog.TextValue = Value as string;
					dialog.ReadOnly = DataGridView.ReadOnly || OwningColumn.ReadOnly;
					dialog.PositionByMouse(mouse);
					dialog.ShowDialog(null);
					if (dialog.TextValueChanged) {
						Value = dialog.TextValue;
						DataGridView.EndEdit();
					}
				}
			}
		}

		private class DateTimeCell : DataGridViewTextBoxCell
		{
			protected override void OnDoubleClick(DataGridViewCellEventArgs e)
			{
				base.OnDoubleClick(e);
				var mouse = MousePosition;
				if (!DataGridView.ReadOnly && !OwningColumn.ReadOnly) {
					using (var dialog = new DateTimeVisualizer()) {
						dialog.Value = Value as DateTime?;
						dialog.PositionByMouse(mouse);
						dialog.ShowDialog(null);
						if (dialog.ValueChanged) {
							if (dialog.Value == null) {
								Value = DBNull.Value;
							} else {
								Value = dialog.Value.Value;
							}
							DataGridView.EndEdit();
						}
					}
				}
			}
		}

		public void CopyDataFromGrid(bool includeHeaders = false)
		{
			var mode = ClipboardCopyMode;
			var allowAdd = AllowUserToAddRows;
			var isRowHeaderVisible = RowHeadersVisible;

			ClipboardCopyMode = includeHeaders
				? DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
				: DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			RowHeadersVisible = false;
			AllowUserToAddRows = false;
			SelectAll();
			var dataObj = GetClipboardContent();
			if (dataObj != null) {
				Clipboard.SetDataObject(dataObj);
			}
			ClearSelection();
			
			AllowUserToAddRows = allowAdd;
			RowHeadersVisible = isRowHeaderVisible;
			ClipboardCopyMode = mode;
		}
	}
}
