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
		}

		public byte[] BinaryData
		{
			get { return Value as byte[]; }
			set { Value = value; }
		}

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
