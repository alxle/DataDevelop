using System.Collections.Generic;
using System.IO;
using System.Text;
using DataDevelop.Data;
using IronPython.Hosting;

namespace DataDevelop.Scripting
{
	public class PythonScriptEngine : ScriptEngine
	{
		readonly Microsoft.Scripting.Hosting.ScriptEngine engine;
		readonly Microsoft.Scripting.Hosting.ScriptScope scope;

		public PythonScriptEngine()
		{
			engine = Python.CreateEngine();
			scope = engine.CreateScope();
		}

		public override string Name => "IronPython";

		public override string Extension => ".py";

		public override void Initialize(Stream output, IDictionary<string, Database> databases)
		{
			var runtime = engine.Runtime;
			runtime.IO.SetOutput(output, Encoding.UTF8);
			scope.SetVariable("_dbs", databases);
			engine.Execute(Properties.Resources.PythonScript, scope);
		}

		public override void Execute(string scriptCode)
		{
			engine.Execute(scriptCode, scope);
		}
	}
}
