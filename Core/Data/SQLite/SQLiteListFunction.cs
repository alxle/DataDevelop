using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Specialized;
using System.Text;

namespace DataDevelop.Data.SQLite
{
	[SQLiteFunction(Arguments = 1, FuncType = FunctionType.Aggregate, Name = "List")]
	class SQLiteListFunction : SQLiteFunction
	{
		public override void Step(object[] args, int stepNumber, ref object contextData)
		{
			var list = contextData as StringCollection;
			if (list == null) {
				list = new StringCollection();
				contextData = list;
			}
			list.Add(args[0].ToString());
		}

		public override object Final(object contextData)
		{
			var list = contextData as StringCollection;
			if (list == null) {
				return null;
			}
			var builder = new StringBuilder();
			for (int i = 0; i < list.Count; i++) {
				builder.Append(list[i]);
				if (i + 1 < list.Count) {
					builder.Append(", ");
				}
			}
			return builder.ToString();
		}
	}
}
