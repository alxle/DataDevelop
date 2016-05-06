using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace DataDevelop.Data
{
	[ReadOnly(true)]
	public class Column : ITableObject
	{
		private Table table;
		private string name;
		private string quotedName;
		private bool isIdentity;
		private bool inPrimaryKey;
		private string providerType = String.Empty;
		private Type type;

		public Column(Table table)
		{
			this.table = table;
		}

		[Browsable(false)]
		public Table Table
		{
			get { return this.table; }
			set { this.table = value; }
		}

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public string QuotedName
		{
			get
			{
				if (this.quotedName == null) {
					this.quotedName = this.Table.Database.QuoteObjectName(this.name);
				}
				return quotedName;
			}
		}

		public bool InPrimaryKey
		{
			get { return this.inPrimaryKey; }
			set { this.inPrimaryKey = value; }
		}

		public bool IsIdentity
		{
			get { return this.isIdentity; }
			set { this.isIdentity = value; }
		}

		public string ProviderType
		{
			get
			{
				return this.providerType;
			}
			set
			{
				if (value == null) {
					throw new ArgumentNullException("value");
				}
				this.providerType = value;
			}
		}

		public Type Type
		{
			get { return this.type ?? typeof(object); }
			set { this.type = value; }
		}
	}
}
