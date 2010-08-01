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
		private DbObjectCollection<StoredProcedure> storedProceduresCollection;
		private DbObjectCollection<Table> tablesCollection;

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

		public ConnectionSettings ConnectionSettings
		{
			get
			{
				ConnectionSettings settings = new ConnectionSettings();
				settings.ProviderName = this.Provider.Name;
				settings.DatabaseName = this.Name;
				settings.ConnectionString = this.ConnectionString;
				return settings;
			}
		}

		public virtual bool SupportStoredProcedures
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

		public IDisposable CreateConnectionScope()
		{
			return new ConnectionScope(this);
		}

		protected abstract void DoConnect();

		protected abstract void DoDisconnect();
		
		protected abstract void PopulateTables(DbObjectCollection<Table> tablesCollection);

		protected abstract void PopulateStoredProcedures(DbObjectCollection<StoredProcedure> storedProceduresCollection);

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
