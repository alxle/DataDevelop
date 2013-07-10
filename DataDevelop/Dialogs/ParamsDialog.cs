using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDevelop.Dialogs
{
	public partial class ParamsDialog : DataDevelop.Dialogs.BaseDialog
	{
		public ParamsDialog()
		{
			InitializeComponent();
			foreach (object value in Enum.GetValues(typeof(DbType))) {
				this.dbTypeDataGridViewTextBoxColumn.Items.Add(value);
			}
		}

		public IList<IDataParameter> Parameters
		{
			get { return this.dataGridView1.DataSource as IList<IDataParameter>; }
			set { this.dataGridView1.DataSource = value; }
		}
	}
}
