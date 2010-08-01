using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IronPython.Hosting;
using DataDevelop.Data;

namespace DataDevelop.Scripting
{
	public class PythonScriptEngine : ScriptEngine
	{
		PythonEngine engine = new PythonEngine();

		public override string Name
		{
			get { return "IronPython"; }
		}

		public override string Extension
		{
			get { return ".py"; }
		}

		public override void Initialize(Stream output, IDictionary<string, Database> databases)
		{
			engine.SetStandardOutput(output);
			engine.Globals["_dbs"] = databases;
			engine.Execute(Properties.Resources.PythonScript);
		}

		public override void Execute(string scriptCode)
		{
			engine.Execute(scriptCode);
		}
	}
}
