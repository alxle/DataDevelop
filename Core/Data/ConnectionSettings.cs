using System;
using System.Collections.Generic;
using System.Text;
////using System.Runtime.Serialization;
using System.Data.Common;
using System.Xml.Serialization;
using System.Xml;
using System.ComponentModel;

namespace DataDevelop.Data
{
	////[ReadOnly(true)]
	public struct ConnectionSettings ////: IXmlSerializable
	{
		private Database database;

		public ConnectionSettings(Database database)
		{
			if (database == null) {
				throw new ArgumentNullException("database");
			}
			this.database = database;
		}

		public string ProviderName
		{
			get { return this.database.Provider.Name; }
		}

		public string DatabaseName
		{
			get { return this.database.Name; }
		}

		public string ConnectionString
		{
			get { return this.database.ConnectionString; }
		}

		public static bool operator ==(ConnectionSettings x, ConnectionSettings y)
		{
			return Equals(x, y);
		}

		public static bool operator !=(ConnectionSettings x, ConnectionSettings y)
		{
			return !Equals(x, y);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		////#region IXmlSerializable Members

		////System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
		////{
		////    throw new NotImplementedException("The method or operation is not implemented.");
		////}

		////public void ReadXml(XmlReader reader)
		////{
		////    reader.ReadStartElement("ConnectionSettings");
		////    this.databaseName = reader.GetAttribute("Name");
		////    this.providerName = reader.GetAttribute("Provider");
		////    this.connectionString = reader.ReadString();
		////    reader.ReadEndElement();
		////}

		////public void WriteXml(XmlWriter writer)
		////{
		////    writer.WriteStartElement("ConnectionSettings");
		////    writer.WriteAttributeString("Name", this.databaseName);
		////    writer.WriteAttributeString("Provider", this.providerName);
		////    writer.WriteString(this.connectionString);
		////    writer.WriteEndElement();
		////}

		////#endregion

		public override bool Equals(object obj)
		{
			if (!(obj is ConnectionSettings)) {
				return false;
			}
			return Equals(this, (ConnectionSettings)obj);
		}

		private static bool Equals(ConnectionSettings x, ConnectionSettings y)
		{
			return x.database == y.database;
		}
	}
}
