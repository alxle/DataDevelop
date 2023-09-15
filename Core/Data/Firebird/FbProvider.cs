using System;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;

namespace DataDevelop.Data.Firebird
{
	public class FbProvider : DbProvider
	{
		private static FbProvider provider;

		public static FbProvider Instance => provider ?? (provider = new FbProvider());

		private FbProvider() { }

		public override string Name => "Firebird";

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new FbDatabase(name, connectionString);
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new FbConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "Firebird";
		}

		internal static Type MapType(string dataType)
		{
			if (dataType == null) {
				throw new ArgumentNullException(nameof(dataType));
			}
			dataType = dataType.ToLowerInvariant();
			if (dataType == "char" || dataType == "character" || 
				dataType == "varchar" || dataType == "char varying" || 
				dataType == "character varying")
				return typeof(string);
			if (dataType == "numeric" || dataType == "decimal")
				return typeof(decimal);
			if (dataType == "integer" || dataType == "int")
				return typeof(int);
			if (dataType == "smallint")
				return typeof(short);
			if (dataType == "boolean")
				return typeof(bool);
			if (dataType == "float")
				return typeof(float);
			if (dataType == "double" || dataType == "double precision")
				return typeof(double);
			if (dataType == "date" || dataType == "timestamp")
				return typeof(DateTime);
			if (dataType == "blob" || dataType == "blob sub_type 0")
				return typeof(byte[]);
			if (dataType == "blob sub_type 1")
				return typeof(string);
			return typeof(object);
		}
	}
}
