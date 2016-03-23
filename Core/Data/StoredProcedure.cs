using System;
using System.Collections.Generic;
using System.Text;
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
				if (this.parameters == null) {
					var parametersList = new List<Parameter>();
					this.PopulateParameters(parametersList);
					this.parameters = parametersList;
				}
				return this.parameters;
			}
		}
		
		public void RefreshParameters()
		{
			this.parameters = null;
		}

		public abstract string GenerateAlterStatement();

		public abstract string GenerateCreateStatement();

		public abstract string GenerateDropStatement();

		public abstract string GenerateExecuteStatement();

		protected abstract void PopulateParameters(IList<Parameter> parametersCollection);
	}
}
