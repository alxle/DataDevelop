using System;
using System.ComponentModel;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public abstract class Trigger : ITableObject, IDbObject
	{
		protected Trigger(Table table)
		{
			Table = table;
		}

		public string Name { get; set; }

		[Browsable(false)]
		public Table Table { get; set; }

		[Browsable(false)]
		public Database Database => Table.Database;

		public abstract string GenerateCreateStatement();

		public abstract string GenerateAlterStatement();

		public abstract string GenerateDropStatement();
	}
}
