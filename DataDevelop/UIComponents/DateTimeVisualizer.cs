using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop.UIComponents
{
	public partial class DateTimeVisualizer : Form
	{
		private bool valueChanged;

		public DateTimeVisualizer()
		{
			InitializeComponent();
		}

		public bool ValueChanged
		{
			get { return valueChanged; }
		}

		public DateTime? Value
		{
			get { return isNullCheckBox.Checked ? (DateTime?)null : GetDateTime(); }
			set
			{
				this.isNullCheckBox.Checked = !value.HasValue;
				this.datePicker.Value = value ?? DateTime.Today;
				this.timePicker.Value = value ?? DateTime.Now;
				this.valueChanged = false;
			}
		}

		private DateTime GetDateTime()
		{
			DateTime date = datePicker.Value;
			DateTime time = timePicker.Value;
			return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
		}

		private void isNullCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.valueChanged = true;
			this.valueGroupBox.Enabled = !this.isNullCheckBox.Checked;
		}

		private void datePicker_ValueChanged(object sender, EventArgs e)
		{
			this.valueChanged = true;
		}

		private void timePicker_ValueChanged(object sender, EventArgs e)
		{
			this.valueChanged = true;
		}

		private void DateTimeVisualizer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape) {
				e.Handled = true;
				this.Close();
			}
		}
	}
}
