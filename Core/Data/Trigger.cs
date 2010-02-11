using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public abstract class Trigger : ITableObject, IDbObject
	{
		private Table table;
		private string name;

		protected Trigger(Table table)
		{
			this.table = table;
		}

		public string Name
		{
			get { return this.name ?? String.Empty; }
			set { this.name = value; }
		}

		public Table Table
		{
			get { return this.table; }
			set { this.table = value; }
		}

		public Database Database
		{
			get { return this.table.Database; }
		}
		
		public abstract string GenerateCreateStatement();

		public abstract string GenerateAlterStatement();

		public abstract string GenerateDropStatement();
	}
}
