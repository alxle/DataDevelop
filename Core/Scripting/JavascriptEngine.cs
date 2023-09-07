using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using DataDevelop.Data;
using Jint;

namespace DataDevelop.Scripting
{
	public class JavascriptEngine : ScriptEngine
	{
		private readonly Engine engine = new Engine(cfg => cfg.AllowClr(
			typeof(Stopwatch).Assembly, // System
			typeof(DataTable).Assembly, // System.Data
			typeof(XmlDocument).Assembly, // System.Xml
			typeof(Database).Assembly // DataDevelop.Core
			));
		private Stream output;
		private Action<string> writer;

		public override string Name => "Jint";

		public override string Extension => ".js";

		public override void SetOutputWrite(Action<string> outputWrite)
		{
			writer = outputWrite;
		}

		private void Print(object obj)
		{
			var str = (obj == null) ? "null" : obj.ToString();
			str += Environment.NewLine;
			if (writer != null) {
				writer(str);
				return;
			}
			byte[] buffer = OutputEncoding.GetBytes(str);
			output.Write(buffer, 0, buffer.Length);
		}

		private string Dir(object obj)
		{
			if (obj == null) {
				return "null";
			}
			var builder = new StringBuilder();
			var type = obj.GetType();
			foreach (var prop in type.GetProperties()) {
				builder.AppendFormat("property {0} : {1}", prop.Name, prop.PropertyType);
				builder.AppendLine();
			}

			foreach (var method in type.GetMethods()) {
				builder.Append("method ");
				builder.Append(method.Name);
				builder.Append("(");
				bool first = true;
				foreach (var param in method.GetParameters()) {
					if (first) {
						first = false;
					} else {
						builder.Append(", ");
					}
					builder.Append(param.ParameterType);
				}
				builder.Append(") : ");
				builder.Append(method.ReturnType);
				builder.AppendLine();
			}
			return builder.ToString();
		}

		sealed class JTableAdapter : IDisposable, IEnumerable<DataRow>
		{
			private readonly Table table;
			private readonly DbDataAdapter adapter;
			private readonly DataTable data = new DataTable();

			public JTableAdapter(Table table)
			{
				this.table = table ?? throw new ArgumentNullException(nameof(table));
				adapter = table.Database.CreateAdapter(table);
				adapter.Fill(0, 0, data);
			}

			public Table @base => table;

			public DataRow NewRow()
			{
				return data.NewRow();
			}

			public void AddRow(DataRow row)
			{
				data.Rows.Add(row);
			}

			public IEnumerator<DataRow> GetEnumerator()
			{
				var rows = new DataTable();
				adapter.Fill(rows);

				foreach (DataRow row in rows.Rows) {
					yield return row;
					if (row.RowState != DataRowState.Unchanged) {
						data.ImportRow(row);
					}
				}
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public JTableAdapter Each(Action<DataRow> action)
			{
				foreach (var row in this) {
					action(row);
				}
				return this;
			}

			public void Import(DataRow row)
			{
				var newRow = data.NewRow();
				newRow.ItemArray = row.ItemArray;
				data.Rows.Add(newRow);
				SaveChanges();
			}

			public void ImportAll(DataTable fromTable)
			{
				foreach (DataRow row in fromTable.Rows) {
					Import(row);
				}
			}

			public int SaveChanges()
			{
				int rowsAffected = adapter.Update(data);
				data.Rows.Clear();
				return rowsAffected;
			}

			public void ClearChanges()
			{
				data.Rows.Clear();
			}

			public void Dispose()
			{
				data.Dispose();
				adapter.Dispose();
			}
		}

		sealed class JDatabase
		{
			private readonly Database database;

			public JDatabase(Database database)
			{
				this.database = database;
			}

			public Database @base => database;

			public JTableAdapter this[string name]
			{
				get
				{
					var table = @base.GetTable(name);
					if (table == null)
						table = @base.GetTable(name.Replace('_', ' '));
					if (table != null)
						return new JTableAdapter(table);
					return null;
				}
			}

			public DataTable Query(string command, params object[] values)
			{
				return database.Query(command, values);
			}

			public int Execute(string command, params object[] values)
			{
				return database.NonQuery(command, values);
			}

			public object Scalar(string command, params object[] values)
			{
				var table = database.Query(command, values);
				if (table.Rows.Count > 0) {
					return table.Rows[0][0];
				}
				return null;
			}
		}

		public override void Initialize(Stream output, IDictionary<string, Database> databases)
		{
			this.output = output;
			engine.SetValue("print", new Action<object>(Print));
			engine.SetValue("dir", new Func<object, string>(Dir));
			engine.SetValue("Database", new Func<string, JDatabase>(name => new JDatabase(databases[name])));
		}

		public override void Execute(string scriptCode)
		{
			engine.Execute(scriptCode);
		}
	}
}
