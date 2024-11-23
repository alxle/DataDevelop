using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

		public override Encoding OutputEncoding => Encoding.UTF7;

		public override void Initialize(Stream output, IDictionary<string, Database> databases)
		{
			var runtime = engine.Runtime;
			runtime.IO.SetErrorOutput(output, OutputEncoding);
			runtime.IO.SetOutput(output, OutputEncoding);
			scope.SetVariable("_dbs", databases);
			scope.SetVariable("_providers", DbProvider.GetProviders().ToDictionary(i => i.Name, StringComparer.OrdinalIgnoreCase));
			engine.Execute(Properties.Resources.PythonScript, scope);
		}

		public override void Execute(string scriptCode)
		{
			try {
				engine.Execute(scriptCode, scope);
			} catch (Microsoft.Scripting.SyntaxErrorException e) {
				throw new ScriptSyntaxException(e);
			}
		}
	}
}
