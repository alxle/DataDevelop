using System;

namespace DataDevelop.Data
{
	[System.ComponentModel.ReadOnly(true)]
	public class Parameter
	{
		private string name;
		private string providerType;
		private Type parameterType;
		private bool isOutput;

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public string ProviderType
		{
			get { return this.providerType; }
			set { this.providerType = value; }
		}

		public Type ParameterType
		{
			get { return this.parameterType; }
			set { this.parameterType = value; }
		}

		public bool IsOutput
		{
			get { return this.isOutput; }
			set { this.isOutput = value; }
		}
	}
}
