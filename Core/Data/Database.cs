using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DataDevelop.Data
{
	public abstract class Database
	{
		private int connectionCount;
		private DbObjectCollection<Table> tablesCollection;
		private DbObjectCollection<StoredProcedure> storedProceduresCollection;
		private DbObjectCollection<UserDefinedFunction> userDefinedFunctionsCollection;

		public abstract string Name
		{
			get;
		}

		public DbObjectCollection<Table> Tables
		{
			get
			{
				if (this.tablesCollection == null) {
					this.tablesCollection = new DbObjectCollection<Table>();
					this.PopulateTables(this.tablesCollection);
				}
				return this.tablesCollection;
			}
		}

		public DbObjectCollection<StoredProcedure> StoredProcedures
		{
			get
			{
				if (this.storedProceduresCollection == null) {
					this.storedProceduresCollection = new DbObjectCollection<StoredProcedure>();
					this.PopulateStoredProcedures(this.storedProceduresCollection);
				}
				return this.storedProceduresCollection;
			}
		}

		public DbObjectCollection<UserDefinedFunction> UserDefinedFunctions
		{
			get
			{
				if (this.userDefinedFunctionsCollection == null) {
					this.userDefinedFunctionsCollection = new DbObjectCollection<UserDefinedFunction>();
					this.PopulateUserDefinedFunctions(this.userDefinedFunctionsCollection);
				}
				return this.userDefinedFunctionsCollection;
			}
		}

		public virtual string ParameterPrefix
		{
			get { return "@"; }
		}

		public virtual string QuotePrefix
		{
			get { return "["; }
		}

		public virtual string QuoteSuffix
		{
			get { return "]"; }
		}

		public abstract DbProvider Provider
		{
			get;
		}

		public abstract string ConnectionString
		{
			get;
		}

		public virtual bool SupportStoredProcedures
		{
			get { return false; }
		}

		public virtual bool SupportUserDefinedFunctions
		{
			get { return false; }
		}

		public bool IsConnected
		{
			get { return this.connectionCount > 0; }
		}

		public void Connect()
		{
			lock (this) {
				if (this.connectionCount == 0) {
					this.DoConnect();
				}
				this.connectionCount++;
			}
		}

		public void Disconnect()
		{
			lock (this) {
				if (this.connectionCount != 0) {
					this.connectionCount--;
					if (this.connectionCount == 0) {
						this.DoDisconnect();
						this.RefreshTables();
						if (this.SupportStoredProcedures) {
							this.RefreshStoredProcedures();
						}
					}
				}
			}
		}

		public void DisconnectAll()
		{
			lock (this) {
				this.DoDisconnect();
				this.tablesCollection = null;
				this.storedProceduresCollection = null;
				this.connectionCount = 0;
			}
		}

		public void Reconnect()
		{
			lock (this) {
				try {
					this.DoDisconnect();
				} catch (Exception) {
					// Ignore error because we will try to reconnect
				}
				this.DoConnect();
			}
		}

		public IEnumerable<Table> GetViews()
		{
			foreach (Table t in this.Tables) {
				if (t.IsView) {
					yield return t;
				}
			}
		}

		public IEnumerable<Table> GetTables()
		{
			foreach (Table t in this.Tables) {
				if (!t.IsView) {
					yield return t;
				}
			}
		}

		public virtual string QuoteObjectName(string name)
		{
			return this.QuotePrefix + name + this.QuoteSuffix;
		}

		public DataTable Query(string commandText)
		{
			using (IDbCommand command = DbCommandParser.Parse(this, commandText)) {
				return ExecuteTable(command);
			}
		}

		public DataTable Query(string commandText, params object[] values)
		{
			using (IDbCommand command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, values);
				return ExecuteTable(command);
			}
		}

		public DataTable Query(string commandText, DataRow row)
		{
			using (IDbCommand command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, row);
				return ExecuteTable(command);
			}
		}

		public DataTable Query(string commandText, Dictionary<string, object> parameters)
		{
			using (IDbCommand command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, parameters);
				return ExecuteTable(command);
			}
		}

		public int NonQuery(string commandText)
		{
			using (IDbCommand command = DbCommandParser.Parse(this, commandText)) {
				return command.ExecuteNonQuery();
			}
		}

		public int NonQuery(string commandText, params object[] values)
		{
			using (IDbCommand command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, values);
				return command.ExecuteNonQuery();
			}
		}

		public int NonQuery(string commandText, DataRow row)
		{
			using (IDbCommand command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, row);
				return command.ExecuteNonQuery();
			}
		}

		public int NonQuery(string commandText, Dictionary<string, object> parameters)
		{
			using (IDbCommand command = DbCommandParser.Parse(this, commandText)) {
				DbCommandParser.BindParameters(command, parameters);
				return command.ExecuteNonQuery();
			}
		}

		public abstract int ExecuteNonQuery(string commandText);

		public abstract int ExecuteNonQuery(string commandText, DbTransaction transaction);

		public abstract DataTable ExecuteTable(string commandText);

		protected static DataTable ExecuteTable(IDbCommand command)
		{
			DataTable table = new DataTable();
			using (IDataReader reader = command.ExecuteReader()) {
				table.Load(reader);
			}
			return table;
		}

		public virtual DbDataAdapter CreateAdapter(Table table)
		{
			return this.CreateAdapter(table, new TableFilter(table));
		}
		
		public abstract DbDataAdapter CreateAdapter(Table table, TableFilter filter);
		
		public abstract DbCommand CreateCommand();
		
		public abstract DbTransaction BeginTransaction();

		public abstract void ChangeConnectionString(string newConnectionString);

		public void RefreshTables()
		{
			this.tablesCollection = null;
		}

		public void RefreshStoredProcedures()
		{
			this.storedProceduresCollection = null;
		}

		public void RefreshUserDefinedFunctions()
		{
			this.userDefinedFunctionsCollection = null;
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
			if (this.SupportUserDefinedFunctions) {
				throw new NotSupportedException();
			}
			throw new NotImplementedException();
		}

		public event EventHandler<DatabaseEventArgs> Connected;

		public event EventHandler<DatabaseEventArgs> Disconnected;

		protected virtual void OnConnected(DatabaseEventArgs args)
		{
			if (Connected != null) {
				Connected(this, new DatabaseEventArgs(this));
			}
		}

		protected virtual void OnDisconnected(DatabaseEventArgs args)
		{
			if (Disconnected != null) {
				Disconnected(this, new DatabaseEventArgs(this));
			}
		}
	}
}
