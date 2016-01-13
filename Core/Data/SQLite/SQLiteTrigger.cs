using System;
using System.Data.SQLite;

namespace DataDevelop.Data.SQLite
{
	internal class SQLiteTrigger : Trigger
	{
		private SQLiteDatabase database;

		public SQLiteTrigger(SQLiteTable table)
			: base(table)
		{
			this.database = (SQLiteDatabase)table.Database;
		}

		public override string GenerateCreateStatement()
		{
			using (this.database.CreateConnectionScope()) {
				using (var command = this.database.Connection.CreateCommand()) {
					command.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'trigger' AND tbl_name = @tbl_name AND name = @name";
					command.Parameters.AddWithValue("@tbl_name", Table.Name);
					command.Parameters.AddWithValue("@name", this.Name);
					string statement = command.ExecuteScalar() as string;
					return statement;
				}
			}
		}

		public override string GenerateAlterStatement()
		{
			return this.GenerateDropStatement() + ";\r\n" + this.GenerateCreateStatement();
		}

		public override string GenerateDropStatement()
		{
			return "DROP TRIGGER [" + this.Name + "]";
		}
	}
}
