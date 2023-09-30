using System;
using System.ComponentModel;
using System.Threading;

namespace DataDevelop.Components
{
	class BackgroundWorkerEx : BackgroundWorker
	{
		private Thread workerThread;

		[DefaultValue(false)]
		public bool AbortEnabled { get; set; }

		public bool AbortPending { get; private set; }

		protected override void OnDoWork(DoWorkEventArgs e)
		{
			workerThread = Thread.CurrentThread;
			try {
				base.OnDoWork(e);
			} catch (ThreadAbortException) {
				e.Cancel = true;
				Thread.ResetAbort();
			} finally {
				AbortPending = false;
			}
		}

		private void ThrowIfAbortNotEnabled()
		{
			if (!AbortEnabled) {
				throw new InvalidOperationException("Abort is not enabled.");
			}
		}

		public void Abort()
		{
			ThrowIfAbortNotEnabled();
			if (workerThread != null) {
				workerThread.Abort();
				workerThread = null;
			}
		}

		public void AbortAsync()
		{
			ThrowIfAbortNotEnabled();
			if (!AbortPending) {
				AbortPending = true;
				var abortThreadStart = new ThreadStart(Abort);
				abortThreadStart.BeginInvoke(null, null);
			}
		}
	}
}
