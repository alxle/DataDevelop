using System;

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
				if (!disposed) {
					disposed = true;
					database.Disconnect();
					GC.SuppressFinalize(this);
				}
			}
		}
	}
}
