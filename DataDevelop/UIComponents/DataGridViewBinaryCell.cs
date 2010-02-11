using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DataDevelop.UIComponents
{
	public class DataGridViewBinaryCell : System.Windows.Forms.DataGridViewImageCell
	{
		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if (value == null || value == DBNull.Value || rowIndex == this.DataGridView.NewRowIndex) {
				return null;
			}
			return Properties.Resources.BinaryImage;
			//return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
			//Rectangle bounds = new Rectangle(0, 0, 64, 16);
			//Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
			//Graphics g = Graphics.FromImage(bitmap);
			//StringFormat sf = new StringFormat();
			//sf.LineAlignment = StringAlignment.Center;
			//sf.Alignment = StringAlignment.Center;
			//sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
			//byte[] data = (byte[])value;
			//g.Clear(SystemColors.Window);
			//g.DrawRectangle(SystemPens.ControlText, 0, 0, bounds.Width - 1, bounds.Height - 1);
			//g.DrawString(GetDataLength(data.Length), DataGridView.Font, SystemBrushes.ControlText, bounds, sf);
			//return bitmap;
		}

		public byte[] BinaryData
		{
			get { return Value as byte[]; }
			set { Value = value; }
		}

		//protected override void OnContentDoubleClick(DataGridViewCellEventArgs e)
		//{
		//    if (Value is byte[] || Value == null || Value == DBNull.Value) {
		//        BinaryVisualizer.Show(null, this);
		//    }
		//}

		private static string GetDataLength(double length)
		{
			string[] units = { "b", "Kb", "Mb", "Gb" };
			int unit = 0;
			while (length > 1024 && unit < 4) {
				length /= 1024;
				unit++;
			}
			return String.Format("{0:0.##} {1}", length, units[unit]);
		}

		protected override void OnDoubleClick(DataGridViewCellEventArgs e)
		{
			if (Value is byte[] || Value == null || Value == DBNull.Value) {
				BinaryVisualizer.Show(null, this);
			}
			base.OnDoubleClick(e);
		}
	}
}
