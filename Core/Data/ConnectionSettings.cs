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
	[ReadOnly(true)]
	public struct ConnectionSettings : IXmlSerializable
	{
		private string providerName;
		private string databaseName;
		private string connectionString;

		public string ProviderName
		{
			get { return this.providerName; }
			set { this.providerName = value; }
		}

		public string DatabaseName
		{
			get { return this.databaseName; }
			set { this.databaseName = value; }
		}

		public string ConnectionString
		{
			get { return this.connectionString; }
			set { this.connectionString = value; }
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

		#region IXmlSerializable Members

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		public void ReadXml(XmlReader reader)
		{
			reader.ReadStartElement("ConnectionSettings");
			this.databaseName = reader.GetAttribute("Name");
			this.providerName = reader.GetAttribute("Provider");
			this.connectionString = reader.ReadString();
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("ConnectionSettings");
			writer.WriteAttributeString("Name", this.databaseName);
			writer.WriteAttributeString("Provider", this.providerName);
			writer.WriteString(this.connectionString);
			writer.WriteEndElement();
		}

		#endregion

		public override bool Equals(object obj)
		{
			if (!(obj is ConnectionSettings)) {
				return false;
			}
			return Equals(this, (ConnectionSettings)obj);
		}

		private static bool Equals(ConnectionSettings x, ConnectionSettings y)
		{
			return x.connectionString == y.connectionString
				&& x.databaseName == y.databaseName
				&& x.providerName == y.providerName;
		}
	}
}
