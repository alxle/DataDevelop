using System;
using System.Collections.Generic;
using System.Text;

namespace DataDevelop.Data
{
	internal sealed class ConnectionScope : IDisposable
	{
		private Database database;
		private bool disposed;

		public ConnectionScope(Database database)
		{
			database.Connect();
			this.database = database;
		}

		public void Dispose()
		{
			lock (this) {
				if (!this.disposed) {
					this.disposed = true;
					this.database.Disconnect();
					GC.SuppressFinalize(this);
				}
			}
		}
	}
}
