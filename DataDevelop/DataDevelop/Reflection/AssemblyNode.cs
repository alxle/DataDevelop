using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace DataDevelop.Reflection
{
	class AssemblyNode
	{
		Assembly assembly;
		AssemblyName assemblyName;
		List<NamespaceNode> namespaces;

		public AssemblyNode(Assembly assembly)
		{
			this.assembly = assembly;
		}

		public string Name
		{
			get
			{
				if (assemblyName == null) {
					assemblyName = assembly.GetName();
				}
				return assemblyName.Name;
			}
		}

		public Version Version
		{
			get
			{
				if (assemblyName == null) {
					assemblyName = assembly.GetName();
				}
				return assemblyName.Version;
			}
		}

		[Browsable(false)]
		public ICollection<NamespaceNode> Namespaces
		{
			get
			{
				if (namespaces == null) {
					namespaces = new List<NamespaceNode>();
					PopulateTypes();
				}
				return namespaces;
			}
		}

		private void PopulateTypes()
		{
			foreach (Type type in assembly.GetTypes()) {
				if (!type.IsVisible) continue;
				string name = type.Namespace ?? "-";
				NamespaceNode node = GetNamespace(name);
				if (node == null) {
					node = new NamespaceNode(name);
					namespaces.Add(node);
				}
				node.Types.Add(new TypeNode(type));
			}
			namespaces.Sort(Compare);
		}

		private int Compare(NamespaceNode a, NamespaceNode b)
		{
			return a.Name.CompareTo(b.Name);
		}

		private NamespaceNode GetNamespace(string name)
		{
			foreach (NamespaceNode space in namespaces) {
				if (space.Name == name) {
					return space;
				}
			}
			return null;
		}

		public override string ToString()
		{
			return assembly.FullName;
		}
	}
}
