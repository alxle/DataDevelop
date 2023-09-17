using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace DataDevelop.Data.PostgreSql
{
	public class PgSqlProvider : DbProvider
	{
		private static PgSqlProvider provider;
		static Dictionary<string, Type> mapTypes;

		public static PgSqlProvider Instance => provider ?? (provider = new PgSqlProvider());

		private PgSqlProvider() { }

		public override string Name => "PgSql";

		public override Database CreateDatabase(string name, string connectionString)
		{
			return new PgSqlDatabase(name, connectionString);
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new NpgsqlConnectionStringBuilder();
		}

		public override string ToString()
		{
			return "PostgreSQL";
		}

		public static Type MapType(string providerType)
		{
			if (mapTypes == null) {
				// Source: https://www.npgsql.org/doc/types/basic.html
				mapTypes = new Dictionary<string, Type> {
					{"boolean", typeof(bool)},
					{"smallint", typeof(short)},
					{"integer", typeof(int)},
					{"bigint", typeof(long)},
					{"real", typeof(float)},
					{"double precision", typeof(double)},
					{"numeric", typeof(decimal)},
					{"money", typeof(decimal)},
					{"text", typeof(string)},
					{"character varying", typeof(string)},
					{"character", typeof(string)},
					{"citext", typeof(string)},
					{"json", typeof(string)},
					{"xml", typeof(string)},
					{"uuid", typeof(Guid)},
					{"bytea", typeof(byte[])},
					{"timestamp with time zone", typeof(DateTimeOffset)},
					{"timestamp without time zone", typeof(DateTime)},
					{"date", typeof(DateTime)},
					{"time without time zone", typeof(TimeSpan)},
					{"time with time zone", typeof(DateTimeOffset)},
					{"interval", typeof(TimeSpan)},
				};
			}
			if (mapTypes.TryGetValue(providerType, out var type)) {
				return type;
			}
			return null;
		}
	}
}
