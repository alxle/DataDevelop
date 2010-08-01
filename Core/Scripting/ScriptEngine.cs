using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataDevelop.Data;

namespace DataDevelop.Scripting
{
	public abstract class ScriptEngine
	{
		public abstract string Name { get; }

		public abstract string Extension { get; }

		public abstract void Initialize(Stream output, IDictionary<string, Database> databases);

		public abstract void Execute(string scriptCode);
	}
}
