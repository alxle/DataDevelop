using System;
using System.ComponentModel;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public class Column : ITableObject
	{
		public Column(Table table, string name = null, Type type = null)
		{
			Table = table;
			Name = name;
			Type = type;
		}

		public Column(string name = null, Type type = null) : this(null, name, type) { }

		[Browsable(false)]
		public Table Table { get; set; }

		public string Name { get; set; }

		public string QuotedName => Table.Database.QuoteObjectName(Name);

		public bool InPrimaryKey { get; set; }

		public bool IsIdentity { get; set; }

		public string ProviderType { get; set; } = string.Empty;

		public Type Type { get; set; } = typeof(object);

		public bool? IsNullable { get; set; }

		public int? Size { get; set; }

		public int? Precision { get; set; }

		public int? Scale { get; set; }
	}
}
