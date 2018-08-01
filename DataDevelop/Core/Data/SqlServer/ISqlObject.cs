using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataDevelop.Data.SqlServer
{
	interface ISqlObject : IDbObject
	{
		string SchemaName { get; }

		string ObjectName { get; }
	}
}
