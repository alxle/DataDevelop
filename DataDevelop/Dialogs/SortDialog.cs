using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataDevelop.Data;

namespace DataDevelop
{
	internal partial class SortDialog : Form
	{
		public SortDialog(TableSort sort)
		{
			InitializeComponent();
			sortPanel.LoadSort(sort);
		}

		public TableSort Sort
		{
			get { return sortPanel.Sort; }
		}
	}
}