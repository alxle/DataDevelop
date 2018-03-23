using System;
using System.ComponentModel;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public class Parameter
	{
		public string Name { get; set; }

		public string ProviderType { get; set; }

		public Type ParameterType { get; set; }

		public bool IsOutput { get; set; }
	}
}
