using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DataDevelop.Data
{
	public abstract class Database
	{
		private int connectionCount;
		private DbObjectCollection<Table> tablesCollection;
		private DbObjectCollection<StoredProcedure> storedProceduresCollection;
		private DbObjectCollection<UserDefinedFunction> userDefinedFunctionsCollection;

		public abstract string Name { get; }

		public DbObjectCollection<Table> Tables
		{
			get
			{
				if (tablesCollection == null) {
					var tables = new DbObjectCollection<Table>();
					PopulateTables(tables);
					tablesCollection = tables;
				}
				return tablesCollection;
			}
		}

		public DbObjectCollection<StoredProcedure> StoredProcedures
		{
			get
			{
				if (storedProceduresCollection == null) {
					var procedures = new DbObjectCollection<StoredProcedure>();
					PopulateStoredProcedures(procedures);
					storedProceduresCollection = procedures;
				}
				return storedProceduresCollection;
			}
		}

		public DbObjectCollection<UserDefinedFunction> UserDefinedFunctions
		{
			get
			{
				if (userDefinedFunctionsCollection == null) {
					var functions = new DbObjectCollection<UserDefinedFunction>();
					PopulateUserDefinedFunctions(functions);
					userDefinedFunctionsCollection = functions;
				}
				return userDefinedFunctionsCollection;
			}
		}

		public virtual string ParameterPrefix => "@";

		public virtual string QuotePrefix => "[";

		public virtual string QuoteSuffix => "]";

		public abstract DbProvider Provider { get; }

		public abstract string ConnectionString { get; }

		public virtual bool SupportStoredProcedures => false;

		public virtual bool SupportUserDefinedFunctions => false;

		public bool IsConnected => connectionCount > 0;

		public void Connect()
		{
			lock (this) {
				if (connectionCount == 0) {
					DoConnect();
				}
				connectionCount++;
			}
		}

		public void Disconnect()
		{
			lock (this) {
				if (connectionCount != 0) {
					connectionCount--;
					if (connectionCount == 0) {
						DoDisconnect();
						RefreshTables();
						if (SupportStoredProcedures) {
							RefreshStoredProcedures();
						}
					}
				}
			}
		}

		public void DisconnectAll()
		{
			lock (this) {
				DoDisconnect();
				tablesCollection = null;
				storedProceduresCollection = null;
				connectionCount = 0;
			}
		}

		public void Reconnect()
		{
			lock (this) {
				try {
					DoDisconnect();
				} catch (Exception) {
					// Ignore error because we will try to reconnect
				}
				DoConnect();
			}
		}

		public IEnumerable<Table> GetViews()
		{
			foreach (var t in Tables) {
				if (t.IsView) {
					yield return t;
				}
			}
		}

		public IEnumerable<Table> GetTables()
		{
			foreach (var t in Tables) {
				if (!t.IsView) {
					yield return t;
				}
			}
		}

		public virtual Table GetTable(string name)
		{
			if (Tables.Contains(name)) {
				return Tables[name];
			}
			return null;
		}

		public virtual string QuoteObjectName(string name)
		{
			return QuotePrefix + name + QuoteSuffix;
		}

		public DataTable Query(string commandText)
		{
			using (var command = DbCommandParser.Parse(this, commandText)) {
				return ExecuteTable(command);
			}
		}

		public DataTable Query(string commandText, params object[] values)
		{
			using (var command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, values);
				return ExecuteTable(command);
			}
		}

		public DataTable Query(string commandText, DataRow row)
		{
			using (var command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, row);
				return ExecuteTable(command);
			}
		}

		public DataTable Query(string commandText, Dictionary<string, object> parameters)
		{
			using (var command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, parameters);
				return ExecuteTable(command);
			}
		}

		public int NonQuery(string commandText)
		{
			using (var command = DbCommandParser.Parse(this, commandText)) {
				return command.ExecuteNonQuery();
			}
		}

		public int NonQuery(string commandText, params object[] values)
		{
			using (var command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, values);
				return command.ExecuteNonQuery();
			}
		}

		public int NonQuery(string commandText, DataRow row)
		{
			using (var command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, row);
				return command.ExecuteNonQuery();
			}
		}

		public int NonQuery(string commandText, Dictionary<string, object> parameters)
		{
			using (var command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, parameters);
				return command.ExecuteNonQuery();
			}
		}

		public abstract int ExecuteNonQuery(string commandText);

		public abstract int ExecuteNonQuery(string commandText, DbTransaction transaction);

		public abstract DataTable ExecuteTable(string commandText);

		protected static DataTable ExecuteTable(IDbCommand command)
		{
			var table = new DataTable();
			using (var reader = command.ExecuteReader()) {
				table.Load(reader);
			}
			return table;
		}

		public virtual DbDataAdapter CreateAdapter(Table table)
		{
			return CreateAdapter(table, new TableFilter(table));
		}
		
		public abstract DbDataAdapter CreateAdapter(Table table, TableFilter filter);
		
		public abstract DbCommand CreateCommand();
		
		public abstract DbTransaction BeginTransaction();

		public abstract void ChangeConnectionString(string newConnectionString);

		public void RefreshTables()
		{
			tablesCollection = null;
		}

		public void RefreshStoredProcedures()
		{
			storedProceduresCollection = null;
		}

		public void RefreshUserDefinedFunctions()
		{
			userDefinedFunctionsCollection = null;
		}

		public IDisposable CreateConnectionScope()
		{
			return new ConnectionScope(this);
		}

		protected abstract void DoConnect();

		protected abstract void DoDisconnect();
		
		protected abstract void PopulateTables(DbObjectCollection<Table> tablesCollection);

		protected abstract void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection);

		protected virtual void PopulateUserDefinedFunctions(DbObjectCollection<UserDefinedFunction> userDefinedFunctionsCollection)
		{
			if (SupportUserDefinedFunctions) {
				throw new NotSupportedException();
			}
			throw new NotImplementedException();
		}

		public event EventHandler<DatabaseEventArgs> Connected;

		public event EventHandler<DatabaseEventArgs> Disconnected;

		protected virtual void OnConnected(DatabaseEventArgs args)
		{
			Connected?.Invoke(this, new DatabaseEventArgs(this));
		}

		protected virtual void OnDisconnected(DatabaseEventArgs args)
		{
			Disconnected?.Invoke(this, new DatabaseEventArgs(this));
		}
	}
}
