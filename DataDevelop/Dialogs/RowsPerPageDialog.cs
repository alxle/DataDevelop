using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop.Dialogs
{
	public partial class RowsPerPageDialog : BaseDialog
	{
		public RowsPerPageDialog()
		{
			InitializeComponent();
		}

		public int RowsPerPage
		{
			get { return Decimal.ToInt32(rowPerPageNumericUpDown.Value); }
			set { rowPerPageNumericUpDown.Value = value; }
		}
	}
}