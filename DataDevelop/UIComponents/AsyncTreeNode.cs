using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace DataDevelop.UIComponents
{
	class AsyncTreeNode : TreeNode
	{
		public bool IsBusy { get; private set; }

		public void RunAsyncOperation(string statusText, Action operation, Action completed, object userState)
		{
			if (this.IsBusy) {
				throw new InvalidOperationException("AsyncNode is busy.");
			}
			this.IsBusy = true;
			var originalText = this.Text;
			this.Text = originalText + " (" + statusText + "...)";
			ThreadStart action = delegate
			{
				Exception error = null;
				try {
					operation();
				} catch (Exception exception) {
					error = exception;
				} finally {
					this.IsBusy = false;
				}
				ThreadStart finish = delegate
				{
					this.Text = originalText;
					if (error == null) {
						completed();
					} else {
						this.OnAsyncException(new AsyncCompletedEventArgs(error, false, userState));
					}
				};
				if (this.TreeView != null) {
					this.TreeView.Invoke(finish);
				} else {
					finish();
				}
			};
			action.BeginInvoke(null, null);
		}

		public event AsyncCompletedEventHandler AsyncException;

		public virtual void OnAsyncException(AsyncCompletedEventArgs e)
		{
			if (AsyncException != null) {
				AsyncException(this, e);
			} else {
				throw e.Error;
			}
		}
	}
}
