using System;
using System.Collections.Generic;
using System.Text;

namespace DataDevelop.Data
{
	internal interface ITableObject
	{
		Table Table { get; set; }

		string Name { get; set; }
	}
}
