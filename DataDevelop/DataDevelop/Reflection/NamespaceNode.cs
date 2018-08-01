using System;
using System.Collections.Generic;
using System.Text;

namespace DataDevelop.Reflection
{
	class NamespaceNode
	{
		string name;
		List<TypeNode> types;

		public NamespaceNode(string name)
		{
			this.name = name;
			this.types = new List<TypeNode>();
		}

		public string Name
		{
			get { return name; }
		}

		public IList<TypeNode> Types
		{
			get { return types; }
		}

		public void SortTypes()
		{
			types.Sort(Comparision);
		}

		private int Comparision(TypeNode a, TypeNode b)
		{
			return a.Name.CompareTo(b.Name);
		}
	}
}
