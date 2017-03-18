using System;
using System.Collections.Generic;
using System.Data;

namespace DataDevelop.Dialogs
{
	public partial class ParamsDialog : BaseDialog
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
			get { return this.paramsDataGridView.DataSource as IList<IDataParameter>; }
			set { this.paramsDataGridView.DataSource = value; }
		}
	}
}
