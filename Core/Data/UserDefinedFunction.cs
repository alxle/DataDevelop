using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public abstract class UserDefinedFunction : StoredProcedure
	{
		protected UserDefinedFunction(Database database)
			: base(database)
		{
		}

		public string ReturnType { get; set; }
	}
}
