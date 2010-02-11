using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataDevelop.Data
{
	[System.ComponentModel.ReadOnly(true)]
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

		////private DbType dbType = DbType.Object;

		////public DbType DbType
		////{
		////    get { return dbType; }
		////    set { dbType = value; }
		////}

		////private Type clrType = typeof(object);

		////public Type ClrType
		////{
		////    get
		////    {
		////        switch (dbType) {
		////            case DbType.AnsiString:
		////            case DbType.AnsiStringFixedLength:
		////            case DbType.String:
		////            case DbType.StringFixedLength:
		////                return typeof(string);
		////            case DbType.Binary:
		////                return typeof(byte[]);
		////            case DbType.Boolean:
		////                return typeof(bool);
		////            case DbType.Byte:
		////                return typeof(byte);
		////            case DbType.Currency:
		////            case DbType.Decimal:
		////                return typeof(decimal);
		////            case DbType.Date:
		////            case DbType.DateTime:
		////            case DbType.Time:
		////                return typeof(DateTime);
		////            case DbType.Double:
		////                return typeof(double);
		////            case DbType.Guid:
		////                return typeof(Guid);
		////            case DbType.Int16:
		////                return typeof(short);
		////            case DbType.Int32:
		////                return typeof(int);
		////            case DbType.Int64:
		////                return typeof(long);
		////            case DbType.SByte:
		////                return typeof(sbyte);
		////            case DbType.Single:
		////                return typeof(float);
		////            case DbType.UInt16:
		////                return typeof(ushort);
		////            case DbType.UInt32:
		////                return typeof(uint);
		////            case DbType.UInt64:
		////                return typeof(ulong);
		////            case DbType.VarNumeric:
		////                return typeof(decimal);
		////            case DbType.Xml:
		////                return typeof(System.Xml.XmlDocument);
		////            default:
		////                return typeof(object);
		////        }
		////    }
		////}

		////public abstract void Rename(string newName);
	}
}
