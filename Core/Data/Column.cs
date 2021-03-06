﻿using System;
using System.ComponentModel;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public class Column : ITableObject
	{
		private string providerType = string.Empty;
		private Type type = typeof(object);

		public Column(Table table)
		{
			Table = table;
		}

		[Browsable(false)]
		public Table Table { get; set; }

		public string Name { get; set; }

		public string QuotedName => Table.Database.QuoteObjectName(Name);

		public bool InPrimaryKey { get; set; }

		public bool IsIdentity { get; set; }

		public string ProviderType
		{
			get => providerType;
			set => providerType = value ?? throw new ArgumentNullException(nameof(value));
		}

		public Type Type
		{
			get => type;
			set => type = value ?? throw new ArgumentNullException(nameof(value));
		}
	}
}
