using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DataDevelop.Dialogs
{
	public partial class ProgressDialog : DataDevelop.Dialogs.BaseDialog
	{
		private BackgroundWorker worker;
		private object argument;
		private object result;
		private bool closeOnFinish;
		private Stopwatch stopwatch = new Stopwatch();

		private ProgressDialog(BackgroundWorker worker, object argument)
		{
			InitializeComponent();
			this.worker = worker;
			this.argument = argument;

			if (worker.WorkerSupportsCancellation) {
				this.cancelButton.Enabled = true;
			}

			worker.RunWorkerCompleted += Completed;
			worker.ProgressChanged += Progress;
		}

		public static object Run(Form owner, string caption, BackgroundWorker worker, bool closeOnFinish)
		{
			return Run(owner,caption, worker, closeOnFinish, null);
		}

		public static object Run(Form owner, string caption, BackgroundWorker worker, bool closeOnFinish, object argument)
		{
			using (var dialog = new ProgressDialog(worker, argument)) {
				dialog.Text = caption ?? "Progress...";
				dialog.closeOnFinish = closeOnFinish;
				dialog.ShowDialog(owner);
				return dialog.result;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			this.worker.RunWorkerAsync(argument);
			this.stopwatch.Start();
			this.elapsedTimer.Start();
			this.argument = null;
			base.OnLoad(e);
		}

		private void Cancel(object sender, EventArgs e)
		{
			if (worker.WorkerSupportsCancellation) {
				worker.CancelAsync();
				cancelButton.Enabled = false;
			}
		}

		private void Progress(object sender, ProgressChangedEventArgs e)
		{
			if (e.UserState != null) {
				this.progressTextBox.Text = e.UserState.ToString();
			}
			if (e.ProgressPercentage == 0) {
				if (this.progressBar.Style != ProgressBarStyle.Marquee) {
					this.progressBar.Style = ProgressBarStyle.Marquee;
				}
			} else if (this.progressBar.Value != e.ProgressPercentage) {
				if (this.progressBar.Style != ProgressBarStyle.Continuous) {
					this.progressBar.Style = ProgressBarStyle.Continuous;
				}
				this.progressBar.Value = e.ProgressPercentage;
			}
		}

		private void Completed(object sender, RunWorkerCompletedEventArgs e)
		{
			worker.RunWorkerCompleted -= Completed;
			worker.ProgressChanged -= Progress;
			
			this.okButton.Enabled = true;

			this.progressBar.Style = ProgressBarStyle.Continuous;

			this.stopwatch.Stop();

			if (e.Error != null) {
				this.progressTextBox.ForeColor = Color.Red;
				this.progressTextBox.Text = "Error: " + e.Error.Message;
			} else if (e.Cancelled) {
				this.progressBar.Value = 0;
				this.progressTextBox.Text = "Cancelled.";
			} else {
				this.progressBar.Value = this.progressBar.Maximum;
				this.result = e.Result;
				this.progressTextBox.Text = "Done.";
				if (closeOnFinish) {
					this.Close();
				}
			}
		}

		private void ProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.worker.IsBusy) {
				e.Cancel = true;
			}
		}

		private void RefreshElapsedTime(object sender, EventArgs e)
		{
			if (this.stopwatch.IsRunning) {
				var elapsed = this.stopwatch.Elapsed;
				this.elapsedLabel.Text = String.Format("Elapsed time: {0:00}:{1:00}:{2:00}", 
					Math.Floor(elapsed.TotalHours), elapsed.Minutes, elapsed.Seconds);
			} else {
				this.elapsedTimer.Stop();
			}
		}
	}
}
