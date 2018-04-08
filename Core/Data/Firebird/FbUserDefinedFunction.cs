using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataDevelop.Data.Firebird
{
	class FbUserDefinedFunction : UserDefinedFunction
	{
		public FbUserDefinedFunction(FbDatabase database, string name)
			: base(database)
		{
			Name = name;
		}

		public override string GenerateAlterStatement()
		{
			throw new NotImplementedException();
		}

		public override string GenerateCreateStatement()
		{
			throw new NotImplementedException();
		}

		public override string GenerateDropStatement()
		{
			throw new NotImplementedException();
		}

		public override string GenerateExecuteStatement()
		{
			throw new NotImplementedException();
		}

		protected override void PopulateParameters(IList<Parameter> parametersCollection)
		{
			throw new NotImplementedException();
		}
	}
}
