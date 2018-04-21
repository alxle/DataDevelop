using System.Collections.Generic;
using System.ComponentModel;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public abstract class StoredProcedure : DbObject
	{
		private IList<Parameter> parameters;

		protected StoredProcedure(Database database)
			: base(database)
		{
		}

		[Browsable(false)]
		public IList<Parameter> Parameters
		{
			get
			{
				if (parameters == null) {
					var parametersList = new List<Parameter>();
					PopulateParameters(parametersList);
					parameters = parametersList;
				}
				return parameters;
			}
		}

		public void RefreshParameters()
		{
			parameters = null;
		}

		public abstract string GenerateAlterStatement();

		public abstract string GenerateCreateStatement();

		public abstract string GenerateDropStatement();

		public abstract string GenerateExecuteStatement();

		protected abstract void PopulateParameters(IList<Parameter> parametersCollection);
	}
}
