using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DataDevelop.Data;
using System.Data.Common;
using System.Data;

namespace DataDevelop.Scripting
{
	public class JavascriptEngine : ScriptEngine
	{
		private Jint.Engine engine = new Jint.Engine(cfg => cfg.AllowClr());
		private Stream output;

		public override string Name
		{
			get { return "Jint"; }
		}

		public override string Extension
		{
			get { return ".js"; }
		}

		private void Print(object obj)
		{
			var str = (obj == null) ? "null" : obj.ToString();
			str += Environment.NewLine;
			byte[] buffer = Encoding.Unicode.GetBytes(str);
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

		sealed class JTableAdapter : IDisposable
		{
			private Table table;
			private DbDataAdapter adapter;
			private DataTable data = new DataTable();

			public JTableAdapter(Table table)
			{
				if (table == null) {
					throw new ArgumentNullException("table");
				}
				this.table = table;
				this.adapter = table.Database.CreateAdapter(table);
				this.adapter.Fill(0, 0, data);
			}

			public DataRow NewRow()
			{
				return this.data.NewRow();
			}

			public void AddRow(DataRow row)
			{
				this.data.Rows.Add(row);
			}

			public JTableAdapter Each(Action<DataRow> action)
			{
				var rows = new DataTable();
				this.adapter.Fill(rows);

				foreach (DataRow row in rows.Rows) {
					action(row);
					if (row.RowState != DataRowState.Unchanged) {
						data.ImportRow(row);
					}
				}
				return this;
			}

			public int SaveChanges()
			{
				int rowsAffected = this.adapter.Update(this.data);
				this.data.Rows.Clear();
				return rowsAffected;
			}

			public void ClearChanges()
			{
				this.data.Rows.Clear();
			}

			public void Dispose()
			{
				data.Dispose();
				adapter.Dispose();
			}
		}

		sealed class JDatabase
		{
			private Database database;

			public JDatabase(Database database)
			{
				this.database = database;
			}

			public JTableAdapter Table(string name)
			{
				var table = this.database.Tables[name];
				return new JTableAdapter(table);
			}
        }

		public override void Initialize(Stream output, IDictionary<string, Database> databases)
		{
			this.output = output;
			this.engine.SetValue("print", new Action<object>(Print));
			this.engine.SetValue("dir", new Func<object, string>(Dir));
			this.engine.SetValue("Database", new Func<string, JDatabase>(name => new JDatabase(databases[name])));
			this.engine.SetValue("get", new Func<DataRow, string, object>((row, column) => row[column]));
			this.engine.SetValue("set", new Action<DataRow, string, object>((row, column, value) => row[column] = value));
		}

		public override void Execute(string scriptCode)
		{
			var result = engine.Execute(scriptCode);
		}
	}
}
