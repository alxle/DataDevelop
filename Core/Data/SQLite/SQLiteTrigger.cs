using System;

namespace DataDevelop.Data.SQLite
{
	internal class SQLiteTrigger : Trigger
	{
		private SQLiteDatabase database;

		public SQLiteTrigger(SQLiteTable table)
			: base(table)
		{
			database = (SQLiteDatabase)table.Database;
		}

		public override string GenerateCreateStatement()
		{
			using (database.CreateConnectionScope()) {
				using (var command = database.Connection.CreateCommand()) {
					command.CommandText = "SELECT sql FROM sqlite_master WHERE type = 'trigger' AND tbl_name = @tbl_name AND name = @name";
					command.Parameters.AddWithValue("@tbl_name", Table.Name);
					command.Parameters.AddWithValue("@name", Name);
					return command.ExecuteScalar() as string;
				}
			}
		}

		public override string GenerateAlterStatement()
		{
			return GenerateDropStatement() + ";" + Environment.NewLine + GenerateCreateStatement();
		}

		public override string GenerateDropStatement()
		{
			return "DROP TRIGGER [" + Name + "]";
		}
	}
}
