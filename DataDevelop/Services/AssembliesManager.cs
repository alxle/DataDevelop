using System;
using System.Collections.Generic;
using System.Reflection;
using DataDevelop.Data;
using DataDevelop.Collections;

namespace DataDevelop.Services
{
	public static class AssembliesManager
	{
		private static bool isCollectionDirty = false;

		private static Dictionary<string, Assembly> collection = new Dictionary<string, Assembly>(StringComparer.OrdinalIgnoreCase);

		private static ReadOnlyDictionary<string, Assembly> readOnlyCollection;

		public static IDictionary<string, Assembly> Assemblies
		{
			get
			{
				if (readOnlyCollection == null) {
					readOnlyCollection = new ReadOnlyDictionary<string, Assembly>(collection);
				}
				return readOnlyCollection;
			}
		}

		public static bool Contains(Assembly assembly)
		{
			return collection.ContainsKey(assembly.FullName);
		}

		public static bool Contains(string assemblyName)
		{
			return collection.ContainsKey(assemblyName);
		}

		public static void Add(Assembly assembly)
		{
			if (Contains(assembly.FullName)) {
				throw new ArgumentException("Already exists a Assembly with name: " + assembly.GetName(), "assembly");
			}
			collection.Add(assembly.FullName, assembly);
			isCollectionDirty = true;
			if (AssemblyAdded != null) {
				AssemblyAdded(Assemblies, new AssemblyEventArgs(assembly));
			}
		}

		public static bool Remove(Assembly assembly)
		{
			if (collection.Remove(assembly.FullName)) {
				isCollectionDirty = true;
				if (AssemblyRemoved != null) {
					AssemblyRemoved(Assemblies, new AssemblyEventArgs(assembly));
				}
				return true;
			}
			return false;
		}

		public static bool IsCollectionDirty
		{
			get { return isCollectionDirty; }
			internal set { isCollectionDirty = value; }
		}

		public static event EventHandler<AssemblyEventArgs> AssemblyAdded;

		public static event EventHandler<AssemblyEventArgs> AssemblyRemoved;
	}

	public class AssemblyEventArgs : EventArgs
	{
		private Assembly assembly;

		public AssemblyEventArgs(Assembly assembly)
		{
			this.assembly = assembly;
		}

		public Assembly Assembly
		{
			get { return assembly; }
		}
	}
}
