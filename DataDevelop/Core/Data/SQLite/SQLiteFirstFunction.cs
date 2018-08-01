using System;
using System.Data.SQLite;

namespace DataDevelop.Data.SQLite
{
	[SQLiteFunction(Arguments = 1, FuncType = FunctionType.Aggregate, Name = "First")]
	class SQLiteFirstFunction : SQLiteFunction
	{
		public override void Step(object[] args, int stepNumber, ref object contextData)
		{
			if (stepNumber == 1) {
				contextData = args[0];
			}
		}

		public override object Final(object contextData)
		{
			return contextData;
		}
	}
}
