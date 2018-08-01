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
		Microsoft.Scripting.Hosting.ScriptEngine engine;
		Microsoft.Scripting.Hosting.ScriptScope scope;

		public PythonScriptEngine()
		{
			engine = Python.CreateEngine();
			scope = engine.CreateScope();
		}

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
			var runtime = engine.Runtime;
			runtime.IO.SetOutput(output, Encoding.Unicode);
			scope.SetVariable("_dbs", databases);
			engine.Execute(Properties.Resources.PythonScript, scope);
		}

		public override void Execute(string scriptCode)
		{
			engine.Execute(scriptCode, scope);
		}
	}
}
